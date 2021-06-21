// NetFile.cpp: implementation of the CNetFile class.
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
#include "NetFile.h"

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

CNetFile::CNetFile(CNetStream* pProvider, CNetStream* pConsumer, CNetFileHdr* pHeader /* = 0*/):
m_pPacket(0),
m_nPacketN(0),
m_nTransmitedSz(0),
m_nLastPacketSz(0),
m_pfnPtr(0),
m_pSubscriber(0)
{
  m_pProvider = pProvider;
  m_pConsumer = pConsumer;

  if (pHeader)
    m_hdr     = *pHeader;
}

CNetFile::~CNetFile()
{
  _DELETE(m_pPacket);
}

void CNetFile::SendHeader()
{
// read the CNetFileHdr from the provider stream
  *m_pConsumer << m_hdr;
}

void CNetFile::ReceiveHeader()
{
  CNetMsg* pMsg = 0;
  try {
// read the CNetFileHdr from the provider stream
    *m_pProvider  >> pMsg;
    m_hdr = *(dynamic_cast<CNetFileHdr*>(pMsg));
	delete pMsg;
  } catch (CNetException x) {
	throw x;
  } catch (bad_cast x) {
	delete pMsg;
	throw x;
  }
}

// receives a single CNetPacket from provider 
bool CNetFile::SendReceivePacket()
{
// must have that
  if (!m_pProvider || !m_pProvider->GetConnection() || 
	  !m_pConsumer || !m_pConsumer->GetConnection()) 
	return false;
// release the previous packet
  _DELETE(m_pPacket);
  CNetMsg* pMsg = 0;
  try {
// receive
    *m_pProvider >> pMsg;

// cast it
	 if ((m_pPacket = dynamic_cast<CNetPacket*>(pMsg)) == 0) {
	   	_DELETE(pMsg);
	   throw CNetException(ERR_NET_MSG_UKNOWN_TYPE, "bool CNetFile::SendReceivePacket()");
	 }
// send it
	 *m_pConsumer << *m_pPacket;

// increase the current packet number sent_received 
// Note: it may differ from CNetPacket::GetSeqN() if CConnection is DATAGRAM
	 m_nPacketN++;
  } catch (CNetException x) {
    _DELETE(m_pPacket);
	throw x;
  }
  return true;
}

void  CNetFile::SetBlockingTransmit(bool bOn)
{
  if (m_pProvider->GetConnection())
	m_pProvider->GetConnection()->SetBlocking(bOn);
  if (m_pConsumer->GetConnection())
    m_pConsumer->GetConnection()->SetBlocking(bOn);
}

ulong CNetFile::GetCurrentPacketN() const
{
  return m_nPacketN;
}

ulong CNetFile::GetCurrentBytesN() const
{
/*
  if (!m_nPacketN)
	return m_nLastPacketSz;
  else
    return m_nPacketN * MAX_NET_PACKET - MAX_NET_PACKET + m_nLastPacketSz;
*/
  return m_nTransmitedSz;
}

void CNetFile::InstallProgressCallBack(void* pSubscriber, CNetStream* pStream, PROGRESS_CB pfn)
{
  if (!pStream || !pStream->GetConnection()) return;
  m_pfnPtr = pfn;
  m_pSubscriber = pSubscriber;
  m_nLastPacketSz = 0;
  m_nTransmitedSz = 0;

  pStream->GetConnection()->InstallProgressCallBack(this, CNetFile::OnTransmitProgress);
}

void CNetFile::OnTransmitProgress(void* pConn, void* pSubscriber)
{
  CNetFile* pThis = (CNetFile*)pSubscriber;
  CNetConnection* pConnecton = (CNetConnection*)pConn;

  CNetMsgHdr hdr;
  if (pConnecton->GetLastPacketSz() == hdr.GetSize()) return;

// combine previous sent bytes total with just sent ones
  pThis->m_nTransmitedSz += pConnecton->GetLastPacketSz();
  pThis->m_nLastPacketSz = pConnecton->GetLastPacketSz();

  if (pThis->m_pfnPtr)
	pThis->m_pfnPtr(pThis, pThis->m_pSubscriber);
}

ulong CNetFile::GetTotalPacketN() const
{
  return m_hdr.GetTotalPacketN();
}

ulong CNetFile::GetTotalBytesN() const
{
  return m_hdr.GetFileSize();
}

// this is a helper class which carries the info about the file 
// being transmitted from the CSendReceiveThread to 
// a GUI subscribed thread (CDialog)

CNetFileTransmitInfo::CNetFileTransmitInfo(const CNetFile& f)
{
  const CNetFileHdr& h = f.GetNetFileHdr();
  m_data.m_bDir = h.IsDirectory();
  m_data.m_nCurrentSz = f.GetCurrentBytesN();
  m_data.m_nTotalSz = h.GetFileSize();
  m_data.m_strConsumerName = h.GetConsumerName();
  m_data.m_strConsumerPath = h.GetConsumerPath();
  m_data.m_strProviderName = h.GetProviderName();
  m_data.m_strProviderPath = h.GetProviderPath();
}

CNetFileTransmitInfo&  CNetFileTransmitInfo::operator=(const CNetFileTransmitInfo& fi)
{
  m_data.m_bDir = fi.m_data.m_bDir;
  m_data.m_nCurrentSz = fi.m_data.m_nCurrentSz;
  m_data.m_nTotalSz =  fi.m_data.m_nTotalSz;
  m_data.m_strConsumerName = fi.m_data.m_strConsumerName;
  m_data.m_strConsumerPath = fi.m_data.m_strConsumerPath;
  m_data.m_strProviderName = fi.m_data.m_strProviderName;
  m_data.m_strProviderPath = fi.m_data.m_strProviderPath;
  return *this;
}

ushort CNetFileTransmitInfo::GetCompletePercentage() const
{
  return (100 * m_data.m_nCurrentSz) / m_data.m_nTotalSz;
}