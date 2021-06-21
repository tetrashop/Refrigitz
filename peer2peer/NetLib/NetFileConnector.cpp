// NetFileConnector.cpp: implementation of the CNetFileConnector class.
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
#include "NetFileAddress.h"
#include "NetFileConnector.h"

#ifdef _WIN32
  #ifdef _DEBUG
  #undef THIS_FILE
  static char THIS_FILE[]=__FILE__;
  #define new DEBUG_NEW
  #endif
#else
  #define ios_base ios
#endif
 
CNetFileConnector::~CNetFileConnector()
{
  Disconnect();
}

ulong CNetFileConnector::CanRead(ulong len)
{
// save the current position
  ulong nCurPos = m_ioStream.rdbuf()->pubseekoff(0, ios_base::cur);
  ulong nSize = GetSize();
// if can move that far then return true else false
  return (nSize - nCurPos > len) ? len : nSize - nCurPos;
}

ulong CNetFileConnector::GetSize()
{
  ulong nCurPos = m_ioStream.rdbuf()->pubseekoff(0, ios_base::cur);
  ulong nSize = m_ioStream.rdbuf()->pubseekoff(0, ios_base::end);
  m_ioStream.rdbuf()->pubseekoff(nCurPos, ios_base::beg);
  return nSize;
}

ulong CNetFileConnector::GetCurrentPos()
{
  return m_ioStream.rdbuf()->pubseekoff(0, ios_base::cur);
}

void CNetFileConnector::Disconnect()
{
  if (IsConnected())
        m_ioStream.close();
}

bool CNetFileConnector::IsConnected() const
{
  return m_ioStream.rdbuf()->is_open();
}

bool CNetFileConnector::IsConnected(const CNetAddress& addr) const
{
  CNetFileAddress locAddr;
  GetLocalAddr(locAddr);
  return (locAddr == addr);
}

void CNetFileConnector::GetLocalAddr(CNetAddress& obj) const
{
  throw CNetFileException(ERR_NET_NOT_YET_IMPL, "void CNetFileConnector::GetLocalAddr(CNetAddress&) const");
}

ulong CNetFileConnector::GetRemoteHandle() const
{
  throw CNetFileException(ERR_NET_NOT_YET_IMPL, "ulong CNetFileConnector::GetRemoteHandle() const");
  return 0;
}

ulong CNetFileConnector::GetLocalHandle() const
{
  throw CNetFileException(ERR_NET_NOT_YET_IMPL, "ulong CNetFileConnector::GetLocalHandle() const");
  return 0;
}

bool CNetFileConnector::CanRead()
{
  int fail = m_ioStream.fail();
  int eof =  m_ioStream.eof();
  int bad =  m_ioStream.bad();

  if (fail && !bad)
          m_ioStream.clear();

  return IsConnected() && (!fail || !bad) && !bad && !eof;
}

bool CNetFileConnector::CanWrite()
{
  int fail = m_ioStream.fail();
  int eof =  m_ioStream.eof();
  int bad =  m_ioStream.bad();

  if (fail && !bad)
          m_ioStream.clear();

  return IsConnected() && (!fail || !bad) && !bad;
}

void CNetFileConnector::HandleException(int, const char*) const
{
}

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////


CNetBinFileConnector::CNetBinFileConnector()
{
}

CNetBinFileConnector::~CNetBinFileConnector()
{
}

CNetConnection* CNetBinFileConnector::Connect(const CNetAddress& obj)
{
  if (IsConnected())
    Disconnect();
// resolove the file location
  const CNetFileAddress& resolver = dynamic_cast<const CNetFileAddress&>(obj);
  const char* szLocation = resolver.GetFileLocation();
  if (resolver.CanOverwrite())
    m_ioStream.open(szLocation, ios_base::binary | ios_base::in | ios_base::out | ios_base::trunc);
  else
	if (resolver.IsReadOnly())
      m_ioStream.open(szLocation, ios_base::binary | ios_base::in);
	else
	  m_ioStream.open(szLocation, ios_base::binary | ios_base::in | ios_base::out | ios_base::app);

  return this;
}

void CNetBinFileConnector::Read(void* pBuff, ulong nSz)
{
  m_ioStream.read((char*)pBuff, nSz);
}

void CNetBinFileConnector::Write(const void* pBuff, ulong nSz)
{
  m_ioStream.write((const char*)pBuff, nSz);
  m_ioStream.flush();
}


//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////


CNetTextFileConnector::CNetTextFileConnector()
{
}

CNetTextFileConnector::~CNetTextFileConnector()
{
}

CNetConnection* CNetTextFileConnector::Connect(const CNetAddress& obj)
{
  if (IsConnected())
        Disconnect();

// reslove the file location
  const CNetFileAddress& resolver = dynamic_cast<const CNetFileAddress&>(obj);
  string strLocation = resolver.GetFileLocation();
  if (resolver.CanOverwrite())
    m_ioStream.open(strLocation.c_str(), ios_base::in | ios_base::out | ios_base::trunc);
  else
        m_ioStream.open(strLocation.c_str(), ios_base::in | ios_base::out | ios_base::app);
  return this;
}

void CNetTextFileConnector::Read(void* pBuff, unsigned long nSz)
{
  m_ioStream.getline((char*)pBuff, nSz);
}

void CNetTextFileConnector::Write(const void* pBuff, unsigned long nSz)
{
  m_ioStream << (const char*)pBuff;
  m_ioStream.flush();
}


/////////////////////////////////////////////////////////////////
/*
CNetSockFileConnector::CNetSockFileConnector()
{
}

CNetSockFileConnector::~CNetSockFileConnector()
{
}

CNetConnection* CNetSockFileConnector::Connect(CNetAddress& obj)
{
  CNetIPAddress&   resolver_ip = dynamic_cast<CNetIPAddress&>(obj);
  CNetFileAddress& resolver_file = dynamic_cast<CNetFileAddress&>(obj);
// if there are data for a remote host, establish a connection with it
  if (!resolver_ip.IsEmpty()) {
         m_sockConnector.Connect(obj);

  }
  else
// if there are data for a file, establish a connection with it
   if (!resolver_file.IsEmpty())
         CNetBinFileConnector::Connect(obj);

  return this;
}

void CNetSockFileConnector::Disconnect()
{
  if (CNetBinFileConnector::IsConnected())
        CNetBinFileConnector::Disconnect();
  else
    if (m_sockConnector.IsConnected())
          m_sockConnector.Disconnect();
}

void CNetSockFileConnector::Read(void* pBuff, unsigned long nSz)
{
  if (CNetBinFileConnector::CanRead())
     CNetBinFileConnector::Read(pBuff, nSz);
  else
        if (m_sockConnector.CanRead())
          m_sockConnector.Read(pBuff, nSz);
}

void CNetSockFileConnector::Write(void* pBuff, unsigned long nSz)
{
  if (CNetBinFileConnector::CanWrite())
     CNetBinFileConnector::Write(pBuff, nSz);
  else
        if (m_sockConnector.CanWrite())
          m_sockConnector.Write(pBuff, nSz);
}

bool CNetSockFileConnector::IsConnected() const
{
  return CNetBinFileConnector::IsConnected() || m_sockConnector.IsConnected();
}

bool CNetSockFileConnector::CanRead()
{
  return CNetBinFileConnector::CanRead() || m_sockConnector.CanRead();
}

bool CNetSockFileConnector::CanWrite()
{
  return CNetBinFileConnector::CanWrite() || m_sockConnector.CanWrite();
}

void CNetSockFileConnector::HandleException(int, const char*)
{
}
*/
