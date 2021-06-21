// NetFileAddress.cpp: implementation of the CNetFileAddress class.
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

CNetFileAddress::CNetFileAddress()
{

}

CNetFileAddress::~CNetFileAddress()
{

}

const char* CNetFileAddress::GetFileLocation() const
{
  return m_parser.GetValue("file");
}

void CNetFileAddress::SetConnectString(const char* szAddr)
{
  CNetAddressScanner proto;
  m_parser.SetScannerType(proto);
  m_parser.SetString(szAddr);
}

void CNetFileAddress::ValidateFlags() const
{
  ulong pos = 0;
  const char* file = m_parser.GetValue("file");
  if (strlen(file) > 0) {
    const char* flag = m_parser.GetValue("openopt");
	if (strcmp(flag, "trunc") && strcmp(flag, "app") && strcmp(flag, "read"))
	  throw CNetFileException(ERR_NET_FILE_INVALID_FLAG, "bool CNetFileAddress::CanOverwrite()");
  }
  else
    throw CNetFileException(ERR_NET_FILE_UNKNOWN, "bool CNetFileAddress::CanOverwrite()");
}

bool CNetFileAddress::CanOverwrite() const
{
  ValidateFlags();
  const char* flag = m_parser.GetValue("openopt");
  if (!strcmp(flag, "trunc"))
    return true;
  return false;
}

bool CNetFileAddress::IsReadOnly() const
{
  ValidateFlags();
  const char* flag = m_parser.GetValue("openopt");
  if (!strcmp(flag, "read"))
    return true;
  return false;
}

bool CNetFileAddress::IsEmpty() const
{
  if (!GetFileLocation())
    return true;
  return false;
}

void CNetFileAddress::HandleException(int nNetErr, const char* strSrc) const
{
  throw CNetFileException(nNetErr, strSrc);
}
