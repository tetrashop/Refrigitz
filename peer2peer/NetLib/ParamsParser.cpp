// Params.cpp: implementation of the CParamsParser class.
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
#include "ParamsParser.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CParamsParser::CParamsParser():
m_pScanner(0)
{

}

CParamsParser::~CParamsParser()
{
  delete m_pScanner;
}

void CParamsParser::SetScannerType(CParamsScanner& proto)
{
  delete m_pScanner;
  m_pScanner = proto.Clone(); 
}

void CParamsParser::SetString(const char* s)
{
  if (!m_pScanner) return;

  m_Input = s;
  if (Parse())
	cout << "Parsed successfully";
  else
	cout << "Parse error. Last known token => " << m_strToken;
  cout << endl;
}

string CParamsParser::GetString() const
{
  map<string, string>::const_iterator it = m_mapValues.begin();
  string s = "";
  while (it != m_mapValues.end()) {
	s += it->first;
	s += "='";
	s += it->second;
	s += "';";
	it++;
  }
  return s;
}

const char* CParamsParser::GetValue(const char* szID) const
{
  string key = szID;
  map<string, string>::const_iterator it = m_mapValues.find(key);
  if (it == m_mapValues.end()) 
	return 0;
  return it->second.c_str();
}

bool CParamsParser::Update(const char* id, const char* value)
{
  map<string, string>::iterator it = m_mapValues.find(id);
  if (it == m_mapValues.end()) return false;
  it->second = value;
  return true;
}

bool CParamsParser::Parse()
{
  m_tkType = m_pScanner->GetToken(m_Input, m_strToken);
  while (m_tkType != CParamsScanner::tkEnd) {
    if (!DoPair()) return false;
	if (!DoList()) return false;
  }
  return true;
}

bool CParamsParser::DoPair()
{
 // save the ID token in a temp
  string id = m_strToken;

  if (!Match(CParamsScanner::tkID)) return false;

// must be followed by '=' sign
  if (!Match(CParamsScanner::tkEqual)) return false;

//  if (m_tkType != tkValue) return false;

  // add to the map
  m_mapValues[id] = m_strToken;

  if (!Match(CParamsScanner::tkValue)) return false;

  return true;
}

bool CParamsParser::DoList()
{
  if (m_tkType == CParamsScanner::tkEnd)
    return true;
  if (m_tkType == CParamsScanner::tkSemiColon)
	if (!Match(CParamsScanner::tkSemiColon)) 
	  return false;
	else 
	  return true;
  return false; 
}

bool CParamsParser::Match(CParamsScanner::Tokens tkType)
{
  if (m_tkType != tkType)
    return false;
  if ((m_tkType = m_pScanner->GetToken(m_Input, m_strToken)) == CParamsScanner::tkError) 
	return false;
  return true;
}

