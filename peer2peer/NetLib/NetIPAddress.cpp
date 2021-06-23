// NetIPAddress.cpp: implementation of the CNetIPAddress class.
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
#include "NetAddressScanner.h"

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

CNetIPAddress::CNetIPAddress()
{
}

CNetIPAddress::~CNetIPAddress()
{
}

string CNetIPAddress::MakeLocalHostAddr(const char* strPort)
{
  string strAddr;
  char szBuff[1000]; // enough ?
  int  nSize = sizeof(szBuff);
// get the name from the local hosts name file
  if (gethostname(szBuff, nSize) == SOCKET_ERROR) {
	long i = GetLastSocketError();
    return strAddr;
  }
  hostent* phe = 0;
// get the hostent* struct back 
  phe = gethostbyname(szBuff);
 // in_addr addr;
// save the network formated addr and the port number into addr
  ulong ip = *(((ulong**)(phe->h_addr_list))[0]);
  sockaddr_in   srv_addr;
  srv_addr.sin_addr.s_addr = ip;
//  memcpy((char*)&(addr), phe->h_addr_list[0], phe->h_length);
  strAddr = "addr='";
  strAddr += inet_ntoa(srv_addr.sin_addr);
  strAddr += "';";
  if (strPort) {
    strAddr += "port='";
    strAddr += strPort;
    strAddr += "';";
  }
  return strAddr;
}

void CNetIPAddress::GetLocalHostAddr(sockaddr_in& addr) const
{
// zero out the structure
  memset(&addr, 0, sizeof(addr));
// dealing with Internet
  addr.sin_family = AF_INET;
// Set IP address
  ulong& sin_addr = *((ulong*)&(addr.sin_addr));
//  ulong  ip = g_addr_stu(GetHostName().c_str());
//  sin_addr = htonl(ip);
  sin_addr = inet_addr(GetHostName());
//
  memset(addr.sin_zero, 0, sizeof(addr.sin_zero));

// port on which this listener will sit
  int pn = GetPortNumber();
  addr.sin_port =  (pn != -1) ? htons(pn) : 0;
}

string  CNetIPAddress::GetLocalHostName() const
{
  char szBuff[500]; // enough ?
  int  nSize = sizeof(szBuff);
// try to get the name from the local hosts name file
  int nErr = gethostname(szBuff, nSize);
  if (nErr) HandleException(ERR_NET_FAILED_RESOLVE_ADDRESS, "void CNetIPAddress::GetLocalHostAddr()");
  return string(szBuff);
}

CNetAddress* CNetIPAddress::Clone() const 
{
  return new CNetIPAddress;
}

void CNetIPAddress::GetRemoteHostAddr(sockaddr_in& addr) const
{
  hostent* phe = 0;
// zero out the structure
  memset(&addr, 0, sizeof(addr));
// dealing with Internet
  addr.sin_family = AF_INET;
// get the hostent* struct back based on the remote host ip addr or its host name
  phe = gethostbyname(GetHostName());
  if (!phe) HandleException(ERR_NET_FAILED_RESOLVE_ADDRESS, "void CNetIPAddress::GetRemoteHostAddr()");
// save the network formated addr and the port number into addr
  memcpy((char*)&(addr.sin_addr), phe->h_addr_list[0], phe->h_length);
  int pn = GetPortNumber();
  addr.sin_port =  (pn != -1) ? htons(pn) : 0;
}

const char* CNetIPAddress::GetHostName() const
{
  return m_parser.GetValue("addr");
}

int CNetIPAddress::GetPortNumber() const
{
 const char* szValue = m_parser.GetValue("port");

  if (szValue)
    return atoi(szValue);

  return -1;
}

bool CNetIPAddress::IsEmpty() const
{
  if (!GetHostName() && GetPortNumber() == -1)
    return true;
  return false;
}

void CNetIPAddress::SetConnectString(const char* szAddr)
{
  CNetAddressScanner proto;
  m_parser.SetScannerType(proto);
  m_parser.SetString(szAddr);
}

void CNetIPAddress::SetConnectString(sockaddr* sockAddr)
{
 struct sockaddr_in* InetSockAddr;
 char*               addr;
 char                ip[50];
 char                port[10];

 InetSockAddr = reinterpret_cast<sockaddr_in*>(sockAddr);
 addr=(char*)(&InetSockAddr->sin_addr);

// Make a string 
 sprintf_s(ip,"%u.%u.%u.%u",0xFF&addr[0],0xFF&addr[1],0xFF&addr[2],0xFF&addr[3]);
// Get the port number
 sprintf_s(port, "%u", ntohs(InetSockAddr->sin_port));

// form the connect string
 string s = "addr='";
 s += ip;
 s += "';port='";
 s += port;
 s += "';";
 SetConnectString(s.c_str());
}

void CNetIPAddress::HandleException(int nNetErr, const char* strSrc) const
{
  throw CNetSockException(nNetErr, GetLastSocketError(), strSrc);
}

