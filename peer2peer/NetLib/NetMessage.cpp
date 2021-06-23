// NetMsg.cpp: implementation of the CNetMsg class.
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
#include "NetMessage.h"
#include "NetStream.h"
#include "NetConnection.h"

#ifdef _WIN32
  #ifdef _DEBUG
  #undef THIS_FILE
  static char THIS_FILE[]=__FILE__;
  #define new DEBUG_NEW
  #endif
#endif  

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetObject::CNetObject():
m_pData(0),
m_nSeekRead(0),
m_nSeekWrite(0)
{
}

CNetObject::~CNetObject() 
{
// the m_pData is released with the object
  EmptyBuffer();
}

void CNetObject::EmptyBuffer()
{
  m_nSeekRead   = 0;
  m_nSeekWrite  = 0;

  if (m_pData) {
    delete (char*)m_pData;
    m_pData = 0;  
  }
}

CNetObject& CNetObject::operator=(const CNetObject& obj)
{
  EmptyBuffer();

// if the buffer is empty just copy the member variables
  if (!obj.m_pData) 
	return (*this).operator=(obj);

// reallocate the buffer
  AllocateData(obj.GetSize_Write());
// and copy the context
  memcpy(m_pData, obj.m_pData, obj.GetSize_Write());

  m_nSeekRead   = obj.m_nSeekRead;
  m_nSeekWrite  = obj.m_nSeekWrite;
// copy the member variables
  return operator=(obj);
}

bool CNetObject::operator==(const CNetObject& obj)
{
  return operator==(obj);
}

void* CNetObject::AllocateData(const ulong nSz /* = 0*/)
{
// delete the data
  EmptyBuffer();
// allocate it, using nSz, or GetSize() if nSz == 0
  if (nSz)
    m_pData = malloc(nSz);
  else
    m_pData = malloc(GetSize_Write());
// return it
  return m_pData;
}

bool CNetObject::IsBufferEmpty() const
{
  if (!m_pData) return true;
  return false;
}

bool CNetObject::CheckReadBound(ulong nSz) const
{
  if (m_nSeekRead + nSz <= GetSize_Read()) return true;
  return false;
}

bool CNetObject::CheckWriteBound(ulong nSz) const
{
  if (m_nSeekWrite + nSz <= GetSize_Write()) return true;
  return false;
}

bool CNetObject::Get(ulong& Out)
{
  if (!CheckReadBound(GetSize_ulong())) return false;
  memcpy(&Out, (char*)m_pData + m_nSeekRead, GetSize_ulong());
  Out = ntohl(Out);
  m_nSeekRead += GetSize_ulong();
  return true;
}

bool CNetObject::Get(ushort& Out)
{
  if (!CheckReadBound(GetSize_ushort())) return false;
  memcpy(&Out, (char*)m_pData + m_nSeekRead, GetSize_ushort());
  Out = ntohs(Out);
  m_nSeekRead += GetSize_ushort();
  return true;
}

bool CNetObject::Get(bool& Out)
{
  if (!CheckReadBound(GetSize_bool())) return false;
  memcpy(&Out, (char*)m_pData + m_nSeekRead, GetSize_bool());
  m_nSeekRead += GetSize_bool();
  return true;
}

bool CNetObject::Get(string& Out, ulong nSz)
{
  if (!CheckReadBound(nSz)) return false;
// the buffer does not have a NULL character
  char* pBuff = new char[nSz+1];
  memcpy(pBuff, (char*)m_pData + m_nSeekRead, nSz);
// so add it
  pBuff[nSz] = NULL;
  Out = pBuff;
  delete pBuff;
  m_nSeekRead += nSz;
  return true;
}

bool CNetObject::Get(char* Out, ulong nSz)
{
  if (!CheckReadBound(nSz)) return false;
// the buffer does not have a NULL character
  char* pBuff = new char[nSz+1];
  memcpy(pBuff, (char*)m_pData + m_nSeekRead, nSz);
// so add it
//  pBuff[nSz] = NULL;
  memcpy(Out, pBuff, nSz);
  delete pBuff;
  m_nSeekRead += nSz;
  return true;
}

bool CNetObject::Put(const ulong In)
{
  if (!CheckWriteBound(GetSize_ulong())) return false;
  ulong nul = htonl(In);
  memcpy((char*)m_pData + m_nSeekWrite, &nul, GetSize_ulong());
  m_nSeekWrite += GetSize_ulong();
  return true;
}

bool CNetObject::Put(const ushort In)
{
  if (!CheckWriteBound(GetSize_ushort())) return false;
  ushort nus = htons(In);
  memcpy((char*)m_pData + m_nSeekWrite, &nus, GetSize_ushort());
  m_nSeekWrite += GetSize_ushort();
  return true;
}

bool CNetObject::Put(const bool In)
{
  if (!CheckWriteBound(GetSize_bool())) return false;
  memcpy((char*)m_pData + m_nSeekWrite, &In, GetSize_bool());
  m_nSeekWrite += GetSize_bool();
  return true;
}

bool CNetObject::Put(const string& In, ulong nSz)
{
  if (!CheckWriteBound(nSz)) return false;
// the NULL character is excluded
  memcpy((char*)m_pData + m_nSeekWrite, In.c_str(), nSz);
  m_nSeekWrite += nSz;
  return true;
}

bool CNetObject::Put(const char* In, ulong nSz)
{
  if (!CheckWriteBound(nSz)) return false;
// the NULL character is excluded
  memcpy((char*)m_pData + m_nSeekWrite, In, nSz);
  m_nSeekWrite += nSz;
  return true;
}

////////////////////////// CNetMsgHdr ////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetMsgHdr::CNetMsgHdr():
CNetObject()
{
  m_data.m_strSignature = NET_SIGNATURE;
  m_data.m_nHost = 0;
  m_data.m_nRemote = 0;
  m_data.m_nClassId = 0;
  m_data.m_nMsgSize = 0;
}

CNetMsgHdr::CNetMsgHdr(const CNetMsgHdr& obj):
CNetObject()
{
  operator=(obj);
}

CNetObject& CNetMsgHdr::operator=(const CNetObject& obj) 
{
  const CNetMsgHdr& msg = dynamic_cast<const CNetMsgHdr&>(obj);
// copies the MSG structure
  m_data.m_strSignature = msg.m_data.m_strSignature;
  m_data.m_nHost                = msg.m_data.m_nHost;
  m_data.m_nRemote              = msg.m_data.m_nRemote;
  m_data.m_nClassId             = msg.m_data.m_nClassId;
  m_data.m_nMsgSize             = msg.m_data.m_nMsgSize;

  return *this;
}

bool CNetMsgHdr::operator==(const CNetObject& obj)
{
  const CNetMsgHdr& msg = dynamic_cast<const CNetMsgHdr&>(obj);
// compares the MSG structure
  if ( !m_data.m_strSignature.compare(msg.m_data.m_strSignature) &&
       m_data.m_nHost            == msg.m_data.m_nHost &&
           m_data.m_nRemote              == msg.m_data.m_nRemote &&
           m_data.m_nClassId     == msg.m_data.m_nClassId &&
           m_data.m_nMsgSize     == msg.m_data.m_nMsgSize)
    return true;
  return false;
}

CNetMsgHdr::~CNetMsgHdr()
{
}

void CNetMsgHdr::Read()
{
  if 
  (
        !Get(m_data.m_strSignature, SIGNATURE_SIZE)                                     ||
        !Get(m_data.m_nHost)                                                                            ||
        !Get(m_data.m_nRemote)                                                                          ||
        !Get(m_data.m_nClassId)                                                                         ||
        !Get(m_data.m_nMsgSize) 
 )
// if something went wrong
        throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetMsgHdr::Read()");

// check the signature
  if (m_data.m_strSignature.compare(NET_SIGNATURE))
        throw CNetMsgException(ERR_NET_WRONG_SIGNATURE, "void CNetMsgHdr::Read()");
}

void CNetMsgHdr::Write()
{
  if
  (
   !Put(m_data.m_strSignature, SIGNATURE_SIZE)                                     ||
   !Put(m_data.m_nHost)                                                                            ||
   !Put(m_data.m_nRemote)                                                                          ||
   !Put(m_data.m_nClassId)                                                                         ||
   !Put(m_data.m_nMsgSize)
  )
// if something went wrong
    throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetMsgHdr::Write()");
}

void CNetMsgHdr::SetHost(const char* strAddr)
{
  ulong nAddr = g_addr_stu(strAddr);
  m_data.m_nHost = nAddr;
}

void CNetMsgHdr::SetRemote(const char* strAddr)
{
  ulong nAddr = g_addr_stu(strAddr);
  m_data.m_nRemote = nAddr;
}

void CNetMsgHdr::SetHost(ulong nAddr)
{
  m_data.m_nHost = nAddr;
}

void CNetMsgHdr::SetRemote(ulong nAddr)
{
  m_data.m_nRemote = nAddr;
}

const string CNetMsgHdr::GetHost() const
{
  string strAddr = g_addr_uts(m_data.m_nHost);
  return strAddr;
}

const string CNetMsgHdr::GetRemote() const
{
  string strAddr = g_addr_uts(m_data.m_nRemote);
  return strAddr;
}

void CNetMsgHdr::Dump(ostream& os)
{
  // save old seek position
  ulong nSavPos = m_nSeekRead;

  ulong data = 0;
  os << endl << "Start Dump for CNetMsgHdr:" << endl << endl;

  os << "Dump for the buffer:" << endl << endl;
  if (Get(data)) 
        os << "CNetMsgHdr::m_data::m_strSignature = " << data << ";" << endl;
  if (Get(data)) 
        os << "CNetMsgHdr::m_data::m_nHost        = " << data << ";" << endl;
  if (Get(data)) 
        os << "CNetMsgHdr::m_data::m_nRemote  = " << data << ";" << endl;
  if (Get(data)) 
        os << "CNetMsgHdr::m_data::m_nClassId     = " << data << ";" << endl;
  if (Get(data)) 
        os << "CNetMsgHdr::m_data::m_nMsgSize     = " << data << ";" << endl;

// restore old seek position
  m_nSeekRead = nSavPos;

  os << "Dump for the member variables:" << endl << endl;
  os << "CNetMsgHdr::m_data::m_strSignature = " <<  m_data.m_strSignature.c_str() << ";" << endl;
  os << "CNetMsgHdr::m_data::m_nHost      = " <<  m_data.m_nHost << ";" << endl;
  os << "CNetMsgHdr::m_data::m_nRemote    = " <<  m_data.m_nRemote << ";" << endl;
  os << "CNetMsgHdr::m_data::m_nClassId   = " <<  m_data.m_nClassId << ";" << endl;
  os << "CNetMsgHdr::m_data::m_nMsgSize   = " <<  m_data.m_nMsgSize << ";" << endl;

  os << endl << "End Dump for CNetMsgHdr:" << endl << endl;
}

//////////////////////////// CNetMsg /////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////


CNetMsg::CNetMsg()
{
}

CNetMsg::CNetMsg(const CNetMsg& obj):
CNetObject()
{
  operator=(obj);
}

CNetObject& CNetMsg::operator=(const CNetObject& obj) 
{
//  CNetObject::operator=(obj);
// copies the CNetMsgHdr data and the data buffer
  const CNetMsg& msg = dynamic_cast<const CNetMsg&>(obj);
  m_header = msg.m_header;
//  return operator=(msg);
  return *this;
}

bool CNetMsg::operator==(const CNetObject& obj)
{
// compares the CNetMsgHdr data and the data buffers
  const CNetMsg& msg = dynamic_cast<const CNetMsg&>(obj);
  if (!(m_header == msg.m_header)) return false;
// compare the rest of the message data
  return operator==(msg);
}

CNetMsg::~CNetMsg()
{
}

void CNetMsg::EmptyBuffer()
{
  CNetObject::EmptyBuffer();
}
/*
void CNetMsg::Read()
{
// read header's data from void*
  m_header.Read();
// read message data from void*
  ReadBody();
}
*/
// reads m_header from CNetStream*, then creates a message
// and reads data of the message from the stream
void CNetMsg::Read(CNetStream* pStream, CNetMsg*& pMsg)
{
  CNetMsgHdr header;
// read the header data. CNetMsgHdr::m_pData now points to the data
  pStream->GetConnection()->Read(header.AllocateData(), header.GetSize_Read());
// parse out the member variables from m_pData
  header.Read();
// create the message class based on the message class name
  pMsg = pStream->CreateMessage(header.GetMSG().m_nClassId);
  if (!pMsg) 
    throw CNetMsgException(ERR_NET_MSG_UKNOWN_TYPE, "CNetMsg*  CNetMsg::Read(CNetStream* pStream)");
// release header.m_pData, because if I don't, then the CNetMsgHdr::operator=()
// will allocate a new m_pData for pMsg->m_header::m_pData and copy 
// the context. That would be an overkill. I don't need this info anymore, 
// since the member variables are properly populated already
   header.EmptyBuffer();
// copy header's member variables
   pMsg->GetMsgHdr()->operator=(header);
// now read the message body data. CNetMsg[derived]::m_pData now points to the data
   pStream->GetConnection()->Read(pMsg->AllocateData(header.GetMsgSize()), header.GetMsgSize());
// parse out the member variables from m_pData
   pMsg->ReadBody();
   if (pMsg->HasChildren())
	 pMsg->ReadChildren(pStream);
// release the message's m_Data
   pMsg->EmptyBuffer();
}

void CNetMsg::Write(CNetStream* pStream)
{
// update the size of the message body (excluding the header data size)
  m_header.SetMsgSize(GetSize_Write());
// let the header know about the message type it's part of
  m_header.SetClassId(GetClassId());
// preallocate the buffer for header's data
  m_header.AllocateData();
// write header's data into void*
// its data will be sent when the message data is sent
  m_header.Write();
// preallocate the buffer for message's body data
  AllocateData();
// write message data into void*
  WriteBody();
// now write the header data and the body data into the socket stream
  pStream->Write(*this);
// if this is a complex object process its children
  if (HasChildren())
	WriteChildren(pStream);
}

ulong CNetMsg::GetSize_Read() const
{ 
  return m_header.GetMsgSize(); 
}
 
ulong CNetMsg::GetSize_Write() const
{
  return 0;
}

void CNetMsg::ReadBody()
{
  // Does nothing here. This has no body
}

void CNetMsg::ReadBody(const CNetMsg& obj)
{
  // Does nothing here. This has no body
}

void CNetMsg::WriteBody()
{
  // Does nothing here. This has no body
}

void CNetMsg::WriteChildren(CNetStream*)
{
  // Does nothing here. This has no children
}

void CNetMsg::ReadChildren(CNetStream*)
{
  // Does nothing here. This has no children
}

//////////////////////////// CNetList /////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetList::CNetList():
m_pfnOutOfData(0),
m_pCaller(0)
{
}

CNetList::CNetList(const CNetList& obj):
m_pfnOutOfData(0),
m_pCaller(0)
{
  operator=(obj);
}

CNetList::~CNetList()
{
  DeleteAllElem();
}

CNetObject& CNetList::operator=(const CNetObject& obj)
{
// copies the header data
  CNetMsg::operator=(obj);
  const CNetList& list = dynamic_cast<const CNetList&>(obj);
// copies the MSG structure
  m_data.m_nElemN = list.m_data.m_nElemN;

  DeleteAllElem();

  for (int i=0; i<(int)list.m_vecElem.size(); i++) {
    CNetMsg* pMsg = list.m_vecElem[i]->Clone();
	*pMsg = *list.m_vecElem[i];
	m_vecElem.push_back(pMsg);
  }
  return *this;
}

bool CNetList::operator==(const CNetObject& obj)
{
  CNetMsg::operator==(obj);
  const CNetList& list = dynamic_cast<const CNetList&>(obj);
  if (m_data.m_nElemN != list.m_data.m_nElemN)
	return false;
  ulong nSz = m_vecElem.size();
  if ( nSz != list.m_vecElem.size())
	return false;
  for (int i=0; i< (int)nSz; i++)
	if (!(m_vecElem[i] == list.m_vecElem[i]))
	  return false;
  return true;
}

void CNetList::DeleteAllElem()
{
  for (int i=0; i< (int)m_vecElem.size(); i++)
	delete m_vecElem[i];
  m_vecElem.clear();
}

// if this is a complex class (aggregates other CNetMsg derivables) return true
bool CNetList::HasChildren() const
{
  if (!m_data.m_nElemN)
	return false;
  return true;
}

void CNetList::ReadBody()
{
  if (!Get(m_data.m_nElemN))
// if something went wrong
    throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetList::ReadBody()");
}

void CNetList::WriteBody()
{
  if (!Put(m_data.m_nElemN))
// if something went wrong
    throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetList::WriteBody()");
}

// I have to change this! For large files I can not store all
// the packets in the vector. I have to ged rid of them assson as
// all packets in the vector are sent and I'm about to request more data
void CNetList::WriteChildren(CNetStream* pStream)
{
  if (!m_data.m_nElemN) return;
  if (!m_pfnOutOfData && m_data.m_nElemN > m_vecElem.size())
	throw CNetMsgException(ERR_NET_NO_CALLBACK_REGISTERED, "void CNetList::WriteChildren()");

// okay it must have data, start sending it
  ulong nElem = 0;
  while (nElem < m_data.m_nElemN) {
// if must get more data from the data provider, request it
	if (nElem == m_vecElem.size() && m_data.m_nElemN > m_vecElem.size())
	  if (!m_pfnOutOfData(m_pCaller, this))
// if the request fails generate an error message and send it
// ... TO DO
	    return;
// send a packet
    *pStream << *m_vecElem[nElem++];
  }
}

void CNetList::ReadChildren(CNetStream* pStream)
{
  ulong nElem = 0;
  while (nElem++ < m_data.m_nElemN) {
	CNetMsg* pMsg;
	*pStream >> pMsg;
	m_vecElem.push_back(pMsg);
  }
}

bool CNetList::AddElem(CNetMsg* pMsg)
{
  if (m_data.m_nElemN < m_vecElem.size())
	return false;
  m_vecElem.push_back(pMsg);
  return true;
}

bool CNetList::SetDataRequestCallback(void* pCaller, DataCallBack pfnOutOfData)
{
  if (!pfnOutOfData || !pCaller) return false;
  m_pfnOutOfData = pfnOutOfData;
  m_pCaller      = pCaller;
  return true;
}

bool CNetList::SetDataArrivedCallback(void* pCaller, DataCallBack pfnOutOfData)
{
  return false;
}

void CNetList::Dump(ostream& os)
{
  ulong sz = 0;
  os << endl << "Start Dump for CNetList:" << endl << endl;

  os << "Dump for the buffer:" << endl << endl;
  if (Get(sz)) 
    os << "CNetList::m_data::m_nElemN = " << sz << ";" << endl;
  
  os << "Dump for the member variables:" << endl << endl;
  os << "CNetList::m_data::m_nElemN = " << m_data.m_nElemN << ";" << endl;
  
  os << endl << "End Dump for CNetList." << endl << endl;
}

/////////////////////////// CNetPacket /////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetPacket::CNetPacket()
{
  m_data.m_nSize = 0;
  m_data.m_szPacket[0] = 0;
  m_data.m_nSeqN = 0;
}

CNetPacket::CNetPacket(ulong hostId, ulong remoteId, CNetPacket::MSG msg_data)
{
  SetHost(hostId);
  SetRemote(remoteId);
// message data
  SetData(msg_data.m_szPacket, msg_data.m_nSize, msg_data.m_nSeqN);
}

CNetPacket::CNetPacket(const char* szPacket, ulong nSize, ulong nSeqN)
{
  SetData(szPacket, nSize, nSeqN);
}

void CNetPacket::ReadBody()
{
// read the packet itself
  if (!Get(m_data.m_nSize) ||
	  !Get(m_data.m_szPacket, m_data.m_nSize) ||
	  !Get(m_data.m_nSeqN))
// if something went wrong
    throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetPacket::ReadBody()");
}

void CNetPacket::ReadBody(const CNetMsg& obj)
{
  const CNetPacket& packet = dynamic_cast<const CNetPacket&>(obj);
  m_data.m_nSize = packet.m_data.m_nSize;
  memcpy(m_data.m_szPacket, packet.m_data.m_szPacket, m_data.m_nSize);
  m_data.m_nSeqN = packet.m_data.m_nSeqN;
}

void CNetPacket::WriteBody()
{
// write the packet
  if (!Put(m_data.m_nSize) ||
	  !Put(m_data.m_szPacket, m_data.m_nSize) ||
	  !Put(m_data.m_nSeqN))
// if something went wrong
    throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetText::WriteBody()");
}

ulong CNetPacket::GetSize_Write() const
{
// the total size = sizeof(ulong) + len(m_szPacket)
  return GetSize_ulong() + m_data.m_nSize + GetSize_ulong();
}

void CNetPacket::SetData(const char* szPacket, ulong nSize, ulong nSeqN)
{
  m_data.m_nSize = nSize;
  memcpy(m_data.m_szPacket, szPacket, m_data.m_nSize);
  m_data.m_nSeqN = nSeqN;
}

CNetObject& CNetPacket::operator=(const CNetObject& obj) 
{
// copies the header data
  CNetMsg::operator=(obj);
  const CNetPacket& packet = dynamic_cast<const CNetPacket&>(obj);
// copies the MSG structure
  SetData(packet.m_data.m_szPacket, packet.m_data.m_nSize, packet.m_data.m_nSeqN);
  return *this;
}

bool CNetPacket::operator==(const CNetObject& obj)
{
  const CNetPacket& packet = dynamic_cast<const CNetPacket&>(obj);
  if (!memcmp(m_data.m_szPacket, packet.m_data.m_szPacket, m_data.m_nSize) &&
	   m_data.m_nSeqN == packet.m_data.m_nSeqN)
    return true;
  return false;
}

void CNetPacket::Dump(ostream& os)
{
  char data[MAX_NET_PACKET];
  ulong sz = 0;
  
  os << endl << "Start Dump for CNetPacket:" << endl << endl;

  os << "Dump for the buffer:" << endl << endl;
  if (Get(sz)) 
    os << "CNetPacket::m_data::m_nSize = " << sz << ";" << endl;
  if (Get(data, sz)) 
    os << "CNetPacket::m_data::m_szPacket = " << data << ";" << endl;
  if (Get(sz)) 
    os << "CNetPacket::m_data::m_nSeqN = " << sz << ";" << endl;
  
  os << "Dump for the member variables:" << endl << endl;
  os << "CNetText::m_data::m_nSize = " << m_data.m_nSize << ";" << endl;
  os << "CNetText::m_data::m_szPacket = " << m_data.m_szPacket << ";" << endl;
  os << "CNetText::m_data::m_nSeqN = " << m_data.m_nSeqN << ";" << endl;
  
  os << endl << "End Dump for CNetPacket:" << endl << endl;
}

/////////////////////////// CNetStatus /////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetStatus::CNetStatus()
{
  m_data.m_nTextSz = 0;
  m_data.m_nCode = 0;
}

CNetStatus::CNetStatus(ulong host, ulong remote, CNetStatus::MSG msg_data)
{
  SetHost(host);
  SetRemote(remote);
// message data
  m_data.m_nCode     = msg_data.m_nCode;
  m_data.m_strText   = msg_data.m_strText;
  m_data.m_nTextSz   = m_data.m_strText.length();
}

CNetStatus::~CNetStatus()
{
}

CNetObject& CNetStatus::operator=(const CNetObject& status) 
{
// copies the header data
  CNetMsg::operator=(status);
  const CNetStatus& obj = dynamic_cast<const CNetStatus&>(status);
// copies the MSG structure
  m_data.m_nCode = obj.m_data.m_nCode;
  m_data.m_strText   = obj.m_data.m_strText;
  m_data.m_nTextSz   = m_data.m_strText.length();

  return *this;
}

bool CNetStatus::operator==(const CNetObject& status)
{
  CNetMsg::operator==(status);
  const CNetStatus& obj = dynamic_cast<const CNetStatus&>(status);
  if (!m_data.m_strText.compare(obj.m_data.m_strText) &&
       m_data.m_nCode == obj.m_data.m_nCode)
    return true;
  return false;
}

void CNetStatus::ReadBody()
{
  if 
  (
// read the size of the string
        !Get(m_data.m_nTextSz)                   ||
// read the string itself
        !Get(m_data.m_strText, m_data.m_nTextSz) ||
// read the exit code
        !Get(m_data.m_nCode)                                 
  )
// if something went wrong
        throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetStatus::ReadBody()");
}

void CNetStatus::ReadBody(const CNetMsg& status)
{
  const CNetStatus& obj = dynamic_cast<const CNetStatus&>(status);
  m_data.m_nCode     = obj.m_data.m_nCode;
  m_data.m_strText   = obj.m_data.m_strText;
  m_data.m_nTextSz   = m_data.m_strText.length();
}

void CNetStatus::WriteBody()
{
  if 
  (
// write the size of the string
        !Put(m_data.m_nTextSz)                                   ||
// write the string itself
        !Put(m_data.m_strText, m_data.m_nTextSz) ||
// write the exit code
        !Put(m_data.m_nCode)                                 
  )
// if something went wrong
        throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetStatus::ReadBody()");
}

ulong CNetStatus::GetSize_Write() const
{
// the total size = sizeof(ulong) + sizeof(strlen(m_strText)) + sizeof(ulong)
  return GetSize_ulong() + m_data.m_nTextSz + GetSize_ulong();
}

void CNetStatus::Dump(ostream& os)
{
  string data = 0;
  ulong sz = 0;
  os << endl << "Start Dump for CNetStatus:" << endl << endl;

  os << "Dump for the buffer:" << endl << endl;
  if (Get(sz)) 
        os << "CCNetStatus::m_data::m_nTextSz = " << sz << ";" << endl;
  if (Get(data, sz)) 
        os << "CNetStatus::m_data::m_strText = " << data.c_str() << ";" << endl;
  if (Get(sz)) 
        os << "CNetStatus::m_data::m_nCode = " << sz << ";" << endl;
  
  os << "Dump for the member variables:" << endl << endl;
  os << "CNetStatus::m_data::m_nTextSz = " << m_data.m_nTextSz << ";" << endl;
  os << "CNetStatus::m_data::m_strText = " << m_data.m_strText.c_str() << ";" << endl;
  os << "CNetStatus::m_data::m_nCode = " << m_data.m_nCode << ";" << endl;
  
  os << endl << "End Dump for CNetStatus." << endl << endl;
}

void CNetStatus::SetText(const char* name)
{
  m_data.m_nTextSz = strlen(name);
  m_data.m_strText = name;
}

void CNetStatus::SetCode(ulong nCode)
{
  m_data.m_nCode= nCode;
}
