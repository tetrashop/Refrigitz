// NetException.cpp: implementation of the CNetException class.
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

#ifdef _WIN32
  #ifdef _DEBUG
  #undef THIS_FILE
  static char THIS_FILE[]=__FILE__;
  #define new DEBUG_NEW
  #endif
#endif
//////////////////////////////////////////////////////////////////////
long GetLastSocketError()
{
#ifdef _WIN32
 return WSAGetLastError();
#else
 return errno;
#endif
}

const char* GetSocketErrorDescription(long nError)
{
 return strerror(nError);
}
//////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetException::CNetException(const CNetException& x)
{
  m_nNetErr = x.m_nNetErr;
  m_strFuncName = x.m_strFuncName;
  m_strDescr = x.m_strDescr;
}

CNetException::~CNetException()
{

}

CNetMsgException::CNetMsgException(int nNetErr, const char* strFuncName):
CNetException(nNetErr, strFuncName)
{
}

CNetMsgException::~CNetMsgException()
{
}

CNetSockException::CNetSockException(const CNetSockException& x):
  CNetException(x)
{
  m_nSockErr = x.m_nSockErr;
}