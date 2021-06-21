// NetStream.cpp: implementation of the CNetStream class.
//
// Written by Marat Bedretdinov (maratb@hotmail.com)
// Copyright (c) 2000.
//
// This code may be used in compiled form in any way you desire. This
// file may be redistributed unmodified by any means PROVIDING it is 
// not sold for profit without the authors written consent, and 
// providing that this notice and the authors name is included. 
//
// If the source code in  this file is used in any commercial application 
// then acknowledgement must be made to the author of this file 
// and permissions to use this file are requested from the author
//
// (in whatever form you wish).// This file is provided "as is" with no expressed or implied warranty.
// The author accepts no liability if it causes any damage whatsoever.
// It's free - so you get what you pay for.//

#include "stdafx.h"

#include "NetConnection.h"
#include "NetFileConnector.h"
#include "NetMessage.h"
#include "NetStream.h"

#ifdef _WIN32
  #ifdef _DEBUG
  #undef THIS_FILE
  static char THIS_FILE[]=__FILE__;
  #define new DEBUG_NEW
  #endif
#else
  #define Sleep sleep  
#endif  


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetStream::CNetStream() :
m_pConnector(0),
m_pFactory(0)
{
  m_pFactory = g_getDefaultMsgFactory();
}

CNetStream::~CNetStream()
{
// if there is a connection, close it and release the object
  _DELETE(m_pConnector)
}

//////////////////////////////////////////////////////////////////////
// this method lets the stream to redirect its data flow through a 
// differnet connector the return value is the previously used connector 

CNetConnection* CNetStream::Attach(CNetConnection* pConnector)
{
  CNetConnection* pPrevConnector = m_pConnector;
// assign new connection to the stream
  m_pConnector = pConnector;
  return pPrevConnector;
}

//////////////////////////////////////////////////////////////////////
// this method lets the stream to handle different type of messages
// the return value is the previsouly used message factory 
// there is no Deatch(CMsgFactory*) method, based on assumtion
// that factories are created statically

CMsgFactory* CNetStream::Attach(CMsgFactory* pFactory)
{
  CMsgFactory* pPrevFactory = m_pFactory;
// assign new msg factory to the stream
  m_pFactory = pFactory;
  return pPrevFactory;
}

//////////////////////////////////////////////////////////////////////
// this method makes the stream unusable, however the connector is
// preserved and may be reassociated with a different stream

CNetConnection* CNetStream::Detach()
{
  CNetConnection* pPrevConnector = m_pConnector;
  m_pConnector = 0;
  return pPrevConnector;
}

// returns true if underlaying protocol is connected
bool CNetStream::IsOpen() const
{
  return m_pConnector && m_pConnector->IsConnected();
}

bool CNetStream::IsThereData()
{
  return IsOpen() && m_pConnector->CanRead();
}

// reads data from the CNetConnector and constructs a CNetMsg out of it
CNetStream& CNetStream::operator>>(CNetMsg*& pMsg)
{
  pMsg = 0;
// if not connected - exception
  if (!m_pConnector || !m_pConnector->IsConnected()) 
    throw CNetException(ERR_NET_NOT_CONNECTED, "bool CNetStream::Read()");

  try {
// wait until there is data. Note that if connection is broken CanRead() throws an exception
   while (!m_pConnector->CanRead())
     Sleep(0);
// read the message from the stream
   CNetMsg::Read(this, pMsg);
  } catch (CNetException e) {
	_DELETE(pMsg);
	pMsg = 0;
	throw e;
  }
  return *this;
}

// write data from CNetMsg into the CNetConnector
CNetStream& CNetStream::operator<<(CNetMsg& msg)
{
// if not connected - exception
  if (!m_pConnector || !m_pConnector->IsConnected()) 
	throw CNetException(ERR_NET_NOT_CONNECTED, "bool CNetStream::Read()");
// wait until I can write in. Note that if connection is broken CanWrite() throws an exception
  try {
    while (!m_pConnector->CanWrite())
      Sleep(0);
// write the member variables (header + message body) into void* and send it
    msg.Write(this);
  } catch (CNetException e) {
	throw e;
  }
  return *this;
}

// this sends only with a single message at a time and only its "simple" data
// a CNetMsg derivable takes care of its children
void CNetStream::Write(CNetMsg& msg)
{
// get the message header
  CNetMsgHdr* pHdr = msg.GetMsgHdr();
// write the message header data into the socket stream
  m_pConnector->Write(pHdr->GetData(), pHdr->GetSize_Write());
// now write the message body data into the socket stream
  m_pConnector->Write(msg.GetData(), msg.GetSize_Write());
// the void* buffer is not needed any longer
  pHdr->EmptyBuffer();
  msg.EmptyBuffer();
}

CNetMsg* CNetStream::CreateMessage(long nClassId)
{
  if (!m_pFactory)  
    throw CNetMsgException(ERR_NET_NO_MSG_FACTORY, "CNetMsg* CNetStream::CreateMessage()");

  return m_pFactory->CreateMessage(nClassId);
}

/*
// read the header data. CNetMsgHdr::m_pData now points to the data
   m_pConnector->Read(pHeader->AllocateData(), pHeader->GetSize_Read());
// parse out the member variables from m_pData
   pHeader->Read();
// create the message class based on the message class name
   //pMsg = dynamic_cast<CNetMsg*>(CREATE(g_MessageClassNames[pHeader->GetMSG().m_nClassId].m_strClassName));
   pMsg = CreateMessage(pHeader->GetMSG().m_nClassId);
   if (!pMsg) 
     throw CNetMsgException(ERR_NET_MSG_UKNOWN_TYPE, "CNetStream& CNetStream::operator>>(CNetMsg*&)");
// now read the message body data. CNetMsg[derived]::m_pData now points to the data
   m_pConnector->Read(pMsg->AllocateData(pHeader->GetMsgSize()), pHeader->GetMsgSize());
// release header.m_pData, because if I don't, then the CNetMsgHdr::operator=()
// will allocate a new m_pData for pMsg->m_header::m_pData and copy 
// the context. That would be an overkill. I don't need this info anymore, 
// since the member variables are properly populated already
   pHeader->EmptyBuffer();
// copy header's member variables
   pMsg->GetMsgHdr()->operator=(*pHeader);
// parse out the member variables from m_pData
   pMsg->ReadBody();
   if (pMsg->HasChildren())
	 pMsg->ReadChildren(this);
// release the message's m_Data
   pMsg->EmptyBuffer();
   delete pHeader;
*/

// reads data from the CNetConnector and constructs a CNetMsg out of it
CNetStream& CFlatStream::operator>>(CNetMsg*& pMsg)
{
  pMsg = 0;
  char* pBuff = 0;
// if not connected - exception
  if (!m_pConnector || !m_pConnector->IsConnected()) 
    throw CNetException(ERR_NET_NOT_CONNECTED, "bool CNetStream::Read()");

  try {
   CNetFileConnector* pConnector = dynamic_cast<CNetFileConnector*>(m_pConnector);
   if (!pConnector)
	 throw CNetMsgException(ERR_NET_BAD_CONNECTION_HANDLER, "CNetStream& CPacketStream::operator>>(CNetMsg*& pMsg)");
// wait until there is data. Note that if connection is broken CanRead() throws an exception
   while (!m_pConnector->CanRead())
     Sleep(0);
// find out how much I can read.
// request as mach as I can, return will have the max possible value
   ulong nLen = pConnector->CanRead(CNetPacket::GetMaxSize_Packet());
// read the message from the stream
   pMsg = new CNetPacket;
   pBuff = new char[nLen];
   pConnector->Read(pBuff, nLen);

// figure out the sequence number of this packet
   ulong nSeqN = (pConnector->GetCurrentPos() + CNetPacket::GetMaxSize_Packet() - 1) / CNetPacket::GetMaxSize_Packet();
   ((CNetPacket*)pMsg)->SetData(pBuff, nLen, nSeqN);

  } catch (CNetException e) {
	_DELETE(pMsg);
	_DELETE(pBuff);
	throw e;
  }
  delete pBuff;
  return *this;
}

// write data from CNetMsg into the CNetConnector
CNetStream& CFlatStream::operator<<(CNetMsg& msg)
{
// if not connected - exception
  if (!m_pConnector || !m_pConnector->IsConnected()) 
	throw CNetException(ERR_NET_NOT_CONNECTED, "bool CNetStream::Read()");
// wait until I can write in. Note that if connection is broken CanWrite() throws an exception
  try {
   CNetFileConnector* pConnector = dynamic_cast<CNetFileConnector*>(m_pConnector);
   while (!pConnector->CanWrite())
      Sleep(0);

// just write the body of the packet
    CNetPacket& packet = dynamic_cast<CNetPacket&>(msg);
	pConnector->Write(packet.GetData(), packet.GetSize());

  } catch (bad_cast e) {
	throw CNetMsgException(ERR_NET_MSG_UKNOWN_TYPE, "CNetStream& CPacketStream::operator<<(CNetMsg& msg)");
  } catch (CNetException e) {
	throw e;
  }
  return *this;
}

