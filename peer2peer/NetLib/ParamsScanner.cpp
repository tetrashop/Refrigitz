// ParamScaner.cpp: implementation of the CParamScaner class.
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
#include "ParamsScanner.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CParamsScanner::CParamsScanner():
m_nAhead(0)
{
}

CParamsScanner::~CParamsScanner()
{
}

CParamsScanner::Tokens CParamsScanner::GetToken(const char* szInput, string& strToken)
{
  strToken = "";

  while (m_nAhead < strlen(szInput)) {
	char c = szInput[m_nAhead];
	m_nAhead++;
	if (c == ' ')
	  continue;
	else
	  if (c == '=') {
	    strToken = c;
		return tkEqual;
	  }
	  else
		if (c == ';') {
		  strToken = c;
		  return tkSemiColon;
		}
		else
		  if (c == '\'') {
		    strToken = "";
		    while ((c = szInput[m_nAhead++]) != '\'') {
    		  strToken += c;
			  if (m_nAhead > strlen(szInput)) 
				return tkError;
			}
		    return tkValue;
		  }
		  else
	        if (isalpha(c)) {
		      strToken = c;
		      while (isalnum(c = szInput[m_nAhead++])) {
    		    strToken += c;
				if (m_nAhead > strlen(szInput)) 
				  return tkError;
			  }
		      return tkID;
			}
			else
			  if (isdigit(c))
				return tkError;
  }
  return tkEnd;
}