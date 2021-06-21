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
#include "ChatMessages.h"
#include "NetStream.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////// CNetLogin /////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetLogin::CNetLogin()
{
  m_data.m_nNameSz = 0;
}

CNetLogin::CNetLogin(ulong host, ulong remote, CNetStatus::MSG& status_data, CNetLogin::MSG& msg_data):
CNetStatus(host, remote, status_data)
{
// message data
  m_data.m_strName   = msg_data.m_strName;
  m_data.m_nNameSz   = m_data.m_strName.length();
}

CNetLogin::~CNetLogin()
{
}

CNetObject& CNetLogin::operator=(const CNetObject& login) 
{
// copies the header data
  CNetMsg::operator=(login);
  CNetStatus::operator=(login);
  const CNetLogin& obj = dynamic_cast<const CNetLogin&>(login);
// copies the MSG structure
  m_data.m_strName   = obj.m_data.m_strName;
  m_data.m_nNameSz   = m_data.m_strName.length();

  return *this;
}

bool CNetLogin::operator==(const CNetObject& login)
{
  bool b = CNetStatus::operator==(login);
  const CNetLogin& obj = dynamic_cast<const CNetLogin&>(login);
  if (!m_data.m_strName.compare(obj.m_data.m_strName) && b)
    return true;
  return false;
}

void CNetLogin::ReadBody()
{
  CNetStatus::ReadBody();
// read the size of the string
  if (!Get(m_data.m_nNameSz) ||
// read the string itself
      !Get(m_data.m_strName, m_data.m_nNameSz))
// if something went wrong
    throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetLogin::ReadBody()");
}

void CNetLogin::ReadBody(const CNetMsg& login)
{
  CNetStatus::ReadBody(login);
  const CNetLogin& obj = dynamic_cast<const CNetLogin&>(login);
  m_data.m_strName   = obj.m_data.m_strName;
  m_data.m_nNameSz   = m_data.m_strName.length();
}

void CNetLogin::WriteBody()
{
  CNetStatus::WriteBody();
// write the size of the string
  if(!Put(m_data.m_nNameSz)  ||
// write the string itself
     !Put(m_data.m_strName, m_data.m_nNameSz))
// if something went wrong
    throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetLogin::ReadBody()");
}

ulong CNetLogin::GetSize_Write() const
{
//                                     sizeof(ulong) + strlen(m_strName)
  return CNetStatus::GetSize_Write() + GetSize_ulong() + m_data.m_nNameSz;
}

void CNetLogin::Dump(ostream& os)
{
  CNetStatus::Dump(os);

  string data = 0;
  ulong sz = 0;
  os << endl << "Start Dump for CNetLogin:" << endl << endl;

  os << "Dump for the buffer:" << endl << endl;

  if (Get(sz)) 
        os << "CNetLogin::m_data::m_nNameSz = " << sz << ";" << endl;
  if (Get(data, sz)) 
        os << "CNetLogin::m_data::m_strName = " << data.c_str() << ";" << endl;
  
  os << "Dump for the member variables:" << endl << endl;
  os << "CNetLogin::m_data::m_nNameSz = " << m_data.m_nNameSz << ";" << endl;
  os << "CNetLogin::m_data::m_strName = " << m_data.m_strName.c_str() << ";" << endl;
  
  os << endl << "End Dump for CNetLogin." << endl << endl;
}

void CNetLogin::SetName(const char* name)
{
  m_data.m_nNameSz = strlen(name);
  m_data.m_strName = name;
}


/////////////////////////// CNetText /////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetText::CNetText()
{
  m_data.m_nSize = 0;
}

CNetText::CNetText(ulong hostId, ulong remoteId, CNetText::MSG msg_data)
{
  SetHost(hostId);
  SetRemote(remoteId);
// message data
  m_data.m_strText = msg_data.m_strText;
  m_data.m_nSize   = m_data.m_strText.length();
}

CNetText::CNetText(const char* strText)
{
  m_data.m_strText =  strText;
  m_data.m_nSize = m_data.m_strText.length();
}

void CNetText::ReadBody()
{
  if 
  (
// read the size of the string
        !Get(m_data.m_nSize) ||
// read the string itself
        !Get(m_data.m_strText, m_data.m_nSize)
  )
// if something went wrong
        throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetText::ReadBody()");
}

void CNetText::ReadBody(const CNetMsg& obj)
{
  const CNetText& text = dynamic_cast<const CNetText&>(obj);
  m_data.m_nSize = text.m_data.m_nSize;
  m_data.m_strText = text.m_data.m_strText;
}

void CNetText::WriteBody()
{
  if
  (
// write the size of the string
        !Put(m_data.m_nSize) ||
// write the string itself
        !Put(m_data.m_strText, m_data.m_nSize)
  )
// if something went wrong
        throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetText::WriteBody()");
}

ulong CNetText::GetSize_Write() const
{
// the total size = sizeof(ulong) + strlen(m_strText)
  return GetSize_ulong() + m_data.m_nSize;
}

void CNetText::SetText(const char* strText)
{
  m_data.m_strText = strText;
  m_data.m_nSize = m_data.m_strText.length();
}

CNetObject& CNetText::operator=(const CNetObject& obj) 
{
// copies the header data
  CNetMsg::operator=(obj);
  const CNetText& text = dynamic_cast<const CNetText&>(obj);
// copies the MSG structure
  m_data.m_strText = text.m_data.m_strText;
  m_data.m_nSize = m_data.m_strText.length();
  return *this;
}

bool CNetText::operator==(const CNetObject& obj)
{
  CNetMsg::operator==(obj);
  const CNetText& peer = dynamic_cast<const CNetText&>(obj);
  if (!m_data.m_strText.compare(peer.m_data.m_strText))
    return true;
  return false;
}

void CNetText::Dump(ostream& os)
{
  string data = 0;
  ulong  sz = 0;
  os << endl << "Start Dump for CNetText:" << endl << endl;

  os << "Dump for the buffer:" << endl << endl;
  if (Get(sz)) 
        os << "CNetText::m_data::m_mSize = " << sz << ";" << endl;
  if (Get(data, sz)) 
        os << "CNetText::m_data::m_strText = " << data.c_str() << ";" << endl;

  os << "Dump for the member variables:" << endl << endl;
  os << "CNetText::m_data::m_nSize = " << m_data.m_nSize << ";" << endl;
  os << "CNetText::m_data::m_strText = " << m_data.m_strText.c_str() << ";" << endl;

  os << endl << "End Dump for CNetText:" << endl << endl;
}

