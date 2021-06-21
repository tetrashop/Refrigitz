// NetAcceptor.cpp: implementation of the CNetAcceptor class.
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
#include "NetSockConnector.h"
#include "NetSockAcceptor.h"

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

CNetSockAcceptor::CNetSockAcceptor()
{

}

CNetSockAcceptor::~CNetSockAcceptor()
{

}

CNetConnection* CNetSockAcceptor::Connect(const CNetAddress& obj)
{
  const CNetIPAddress& resolver = dynamic_cast<const CNetIPAddress&>(obj);

  sockaddr addr;
  resolver.GetLocalHostAddr(*((sockaddr_in*)&addr));

  if (bind(m_hSocket, &addr, sizeof(addr)) == SOCKET_ERROR) {
    Disconnect();
    HandleException(ERR_NET_FAILED_BIND, "CNetConnection* CNetSockAcceptor::Connect()");
  }

  if (listen(m_hSocket, 4) == SOCKET_ERROR) {
    Disconnect();    
    HandleException(ERR_NET_FAILED_LISTEN, "CNetConnection* CNetSockAcceptor::Connect()");
  }
  return this;
}

CNetConnection* CNetSockAcceptor::Accept(const CNetAddress& RemoteAddr)
{
  CNetAddress* pRemoteAddr = const_cast<CNetAddress*>(&RemoteAddr);
  if (!pRemoteAddr) return 0;

  sockaddr addr;
  int sz = sizeof(addr);
  SOCKET hSocket = accept(m_hSocket, &addr, &sz);

  if (m_hSocket == INVALID_SOCKET) {
        Disconnect();
        HandleException(ERR_NET_FAILED_ACCEPT_SOCKET, "CNetConnection* CNetSockAcceptor::Connect()");
  }
// create new connector for the accpeted socket
  CNetSockConnector* pConnector = new CNetSockConnector(hSocket);
// resolve remote peer's address
  pConnector->GetRemoteAddr(*pRemoteAddr);
  return pConnector;
}

bool CNetSockAcceptor::IsConnected(const CNetAddress& addr) const
{
  CNetIPAddress locAddr;
  GetLocalAddr(locAddr);
  return (locAddr == addr);
}

bool CNetSockAcceptor::CanRead()
{
  fd_set readFDs;             // File descriptors to be checked 
  struct timeval CallTimeout; // for 'select' call
 
// Setup timeout for 'select' call as 0
  CallTimeout.tv_sec  = m_nTimeout / 1000;
  CallTimeout.tv_usec = m_nTimeout % 1000;

// Setup array for 'select' call
  FD_ZERO(&readFDs);
  FD_SET(m_hSocket,&readFDs);

// Test which Handles is Ready for Accept via 'select' call
// select tests file descriptors in range 0 - firstParam-1
  if (select(m_hSocket+1, &readFDs, 0, 0, &CallTimeout) < 0)
    HandleException(NET_ERR_CANNOT_CHECK_FOR_REQUEST, "bool CNetSockAcceptor::CanRead()");

// Test 'm_nSocketHandle' (ready or not) & return result
  return FD_ISSET(m_hSocket, &readFDs) != 0;
}
