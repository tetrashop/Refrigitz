// NetSockConnection.cpp: implementation of the CNetSockConnection class.
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

#include "NetIPAddress.h"
#include "NetSockConnection.h"

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
#ifdef _WIN32
  bool CNetSockConnection::m_bWinSockLoaded = false;
#else
#endif

#define PR_TCP 6 // TCP protocol number for 'socket' call

CNetSockConnection::CNetSockConnection():
m_hSocket(0)
{
#ifdef _WIN32
  InitWinSock();
#endif
// create the socket
  m_hSocket = socket(AF_INET, SOCK_STREAM, PR_TCP);
  if (m_hSocket == INVALID_SOCKET) {
    Disconnect();
    HandleException(ERR_NET_FAILED_CREATE_SOCKET, "CNetSockConnection::CNetSockConnection()");
  }
}

CNetSockConnection::CNetSockConnection(SOCKET hSocket):
m_hSocket(hSocket)
{
}

CNetSockConnection::~CNetSockConnection()
{
  Disconnect();
}

void CNetSockConnection::Disconnect()
{
// graceful shutdown
#ifdef _WIN32
//  shutdown(m_hSocket, SD_BOTH);
  int n = closesocket(m_hSocket);
#else
  close((int)m_hSocket);
#endif
}

void CNetSockConnection::Read(void* pBuff, unsigned long nSize)
{
  if (!IsConnected()) 
      HandleException(ERR_NET_NOT_CONNECTED, "void CNetSockConnection::Read(void*, unsigned long)");

// receive the buffer of the size nSize
// the size of the last received packet
  m_nLastPacketSz = 0;
// the total size of receieved data
  int nTotalRead = 0;
// while there is still data to read
  while (nSize > 0) {
// read as much as was received (up to nSize)
    m_nLastPacketSz = recv(m_hSocket, (char*)pBuff + nTotalRead, nSize, 0);
    if (m_nLastPacketSz == SOCKET_ERROR || m_nLastPacketSz <= 0) 
      HandleException(ERR_NET_FAILED_READ, "void CNetSockConnection::Read(void*, unsigned long)");

// let the subcriber know than m_nLastPacketSz bytes have been received
    InvokeCallBack();
// increment nTotalRead by m_nLastPacketSz
	nTotalRead += m_nLastPacketSz;
// decrement nSize by m_nLastPacketSz
    nSize -= m_nLastPacketSz;
  }
}

void CNetSockConnection::Write(const void* pBuff, unsigned long nSize)
{
  if (!IsConnected()) 
     HandleException(ERR_NET_NOT_CONNECTED, "void CNetSockConnection::Write(void*, unsigned long)");

// send the buffer in packets, each is maximum nSize long
// the size of the last sent packet
  m_nLastPacketSz = 0;
// the total bytes sent
  int nTotalSent = 0;
// while still have data to sent
  while (nSize > 0) {
// send as much as TCP layer can take
    m_nLastPacketSz = send(m_hSocket, (const char*)pBuff + nTotalSent, nSize, 0);
    if (m_nLastPacketSz == SOCKET_ERROR || m_nLastPacketSz <= 0) 
      HandleException(ERR_NET_FAILED_SEND, "void CNetSockConnection::Write(void*, unsigned long)");
// let the subcriber know than m_nLastPacketSz bytes have been sent
    InvokeCallBack();
// increment nTotalSent by m_nLastPacketSz
	nTotalSent += m_nLastPacketSz;
// decrement nSize by m_nLastPacketSz
    nSize -= m_nLastPacketSz;
  }
}

// changes the socket from/to blocking/nonblocking
void CNetSockConnection::SetBlocking(bool bOn)
{
  if (!IsConnected()) 
    HandleException(ERR_NET_NOT_CONNECTED, "void CNetSockConnection::Write(void*, unsigned long)");

  ulong opt = (ulong)!bOn;
  int nCode = ioctlsocket(m_hSocket, FIONBIO, &opt);

  if (nCode == SOCKET_ERROR) 
    HandleException(ERR_NET_FAILED_SOCKOPT, "void CNetSockConnection::SetBlocking(bool)");
}

bool CNetSockConnection::CanRead()
{
  if (!IsConnected()) return false;

  fd_set readHandles;
  struct timeval CallTimeout;
// Setup timeout for 'select' call

  CallTimeout.tv_sec = m_nTimeout/1000;
  CallTimeout.tv_usec = m_nTimeout % 1000;

// Setup array for 'select' call
  FD_ZERO(&readHandles);
  FD_SET(m_hSocket, &readHandles);
// Test which Handles is Ready for Read via 'select' call
  int nFds = select(m_hSocket + 1, &readHandles, 0, 0, &CallTimeout);
  if (nFds == SOCKET_ERROR || nFds < 0)
    HandleException(ERR_NET_FAILED_SELECT, "void CNetSockConnection::CanRead()");
// timed out
  if (nFds == 0)
    return false;
// Test 'm_hSocket' (ready or not) & return result
  return FD_ISSET(m_hSocket, &readHandles) != 0;
}

bool CNetSockConnection::CanWrite()
{
  if (!IsConnected()) return false;

  fd_set writeHandles;
  struct timeval  CallTimeout;
 
// Setup timeout for 'select' call as 50 ms - by default
  CallTimeout.tv_sec = m_nTimeout/1000;
  CallTimeout.tv_usec = m_nTimeout % 1000;
// Setup array for 'select' call
  FD_ZERO(&writeHandles);
  FD_SET(m_hSocket, &writeHandles);
// Test which Handles is Ready for Write via 'select' call
  int nFds = select(m_hSocket + 1, 0, &writeHandles, 0, &CallTimeout);
  if (nFds == SOCKET_ERROR || nFds < 0)
    HandleException(ERR_NET_FAILED_SELECT, "void CNetSockConnection::CanWrite()");
  if (nFds == 0)
    return false;
// Test 'm_ioHandle' (ready or not) & return result
  return FD_ISSET(m_hSocket, &writeHandles) != 0;
}

bool CNetSockConnection::IsConnected() const
{
  return m_hSocket > 0;
}

void CNetSockConnection::GetRemoteAddr(CNetAddress& obj) const
{
  if (!IsConnected())
    HandleException(ERR_NET_NOT_CONNECTED, "void CNetSockConnection::GetRemoteAddr(CNetAddress& addr)");

  CNetIPAddress& addr = dynamic_cast<CNetIPAddress&>(obj);

  struct sockaddr SocketAddr;
  int SocketAddrSize = sizeof(SocketAddr);
// get remote socket address as 'struct sockaddr'
  if (getpeername(m_hSocket, &SocketAddr, &SocketAddrSize) == SOCKET_ERROR)
    HandleException(ERR_NET_CANNOT_RESOLVE_PEER_ADDRESS, "void CNetSockConnection::GetRemoteAddr(CNetAddress& addr)");
// if OK -> convert 'struct sockaddr' to a connect string format
  addr.SetConnectString(&SocketAddr);
}

void CNetSockConnection::GetLocalAddr(CNetAddress& obj) const
{
  if (!IsConnected())
    HandleException(ERR_NET_NOT_CONNECTED, "void CNetSockConnection::GetRemoteAddr(CNetAddress& addr)");

  CNetIPAddress& addr = dynamic_cast<CNetIPAddress&>(obj);

  struct sockaddr SocketAddr;
  int nSz = sizeof(SocketAddr);
// get local socket address as 'struct sockaddr'
  if (getsockname(m_hSocket, (sockaddr*)&SocketAddr, (int*)&nSz) == SOCKET_ERROR)
    HandleException(ERR_NET_CANNOT_RESOLVE_LOCAL_ADDRESS, "void CNetSockConnection::GetLocalAddr(CNetAddress& addr)");
// if OK -> convert 'struct sockaddr' to a connect string format

  addr.SetConnectString(&SocketAddr);
}

ulong CNetSockConnection::GetRemoteHandle() const
{
  CNetIPAddress addr;
  GetRemoteAddr(addr);
  return g_addr_stu(addr.GetHostName());
}

ulong CNetSockConnection::GetLocalHandle() const
{
  CNetIPAddress addr;
  GetLocalAddr(addr);
  return g_addr_stu(addr.GetHostName());
}

void CNetSockConnection::HandleException(int nNetErr, const char* strSrc) const
{
  throw CNetSockException(nNetErr, GetLastSocketError(), strSrc);
}

#ifdef _WIN32
void CNetSockConnection::InitWinSock()
{
  if (m_bWinSockLoaded) return;

  WORD wVerReq;
  WSADATA wsaData;
  int nErr; 
// try to get version 2 of WinSock interface
  wVerReq = MAKEWORD(2, 0); 
  nErr = WSAStartup(wVerReq, &wsaData );
  // could not find a usable WinSock DLL.                                  
  if (nErr) 
        throw CNetSockException(ERR_NET_FAILED_INIT_DLL, GetLastSocketError(), "bool CNetSockConnection::InitWinSock() const");
  if (LOBYTE(wsaData.wVersion) != 2 || HIBYTE(wsaData.wVersion) != 0) {
    WSACleanup();
        throw CNetSockException(ERR_NET_FAILED_INIT_DLL, GetLastSocketError(), "bool CNetSockConnection::InitWinSock() const");
  }
  m_bWinSockLoaded = true;
}
#endif
