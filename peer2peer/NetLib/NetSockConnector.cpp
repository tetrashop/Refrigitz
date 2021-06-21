// NetSockConnector.cpp: implementation of the CNetSockConnector class.
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

CNetSockConnector::CNetSockConnector()
{
}

CNetSockConnector::~CNetSockConnector()
{
}

CNetConnection* CNetSockConnector::Connect(const CNetAddress& obj)
{
  const CNetIPAddress& resolver = dynamic_cast<const CNetIPAddress&>(obj);
  sockaddr_in addr;
  resolver.GetRemoteHostAddr(addr);

  if (connect(m_hSocket, reinterpret_cast<sockaddr*>(&addr), sizeof(addr)) == SOCKET_ERROR) {
    Disconnect();
    HandleException(ERR_NET_FAILED_CONNECT, "void CNetSockConnector::Connect()");
  }
  return this;
}

bool CNetSockConnector::IsConnected(const CNetAddress& addr) const
{
  CNetIPAddress remoteAddr;
  GetRemoteAddr(remoteAddr);
  return (remoteAddr == addr);
}
