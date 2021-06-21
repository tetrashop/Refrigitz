// NetAddress.cpp: implementation of the CNetAddress class.
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
#include "NetAddress.h"

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

CNetAddress::CNetAddress()
{

}

CNetAddress::~CNetAddress()
{

}

CNetAddress& CNetAddress::operator=(const CNetAddress& addr)
{
  SetConnectString(addr.GetConnectString().c_str());
  return *this;
}

bool CNetAddress::operator==(const CNetAddress& addr)
{
  return !GetConnectString().compare(addr.GetConnectString());
}

/*
void CNetAddress::RemoveSpaces()
{
  string strTemp;
  for (int i=0; i<m_strAddr.size(); i++)
    if (m_strAddr[i] != ' ')
      strTemp += m_strAddr[i];
  m_strAddr = strTemp;
}


string CNetAddress::GetLexValue(const char* lex, ulong& nPos) const
{
  string strLex;
  while (nPos < m_strAddr.size()) {
        if (!strcmp(strLex.c_str(), lex) && m_strAddr[nPos++] == '=')
          return GetValue(nPos);
        strLex += m_strAddr[nPos++];
  }
  strLex = "";
  return strLex;

}

string CNetAddress::GetValue(ulong& nPos) const
{
  string strValue;
  while (nPos < m_strAddr.size()) {
        if (m_strAddr[nPos] == ';') {
          nPos++;
          return strValue;
        }
        strValue += m_strAddr[nPos++];
  }
  strValue = "";
  return strValue;
}
*/