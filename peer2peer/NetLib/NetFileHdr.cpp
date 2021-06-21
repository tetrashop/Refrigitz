// NetFileHdr.cpp: implementation of the CNetFileHdr class.
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
#include "NetFileHdr.h"

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
/*
CNetFileHdr::CNetFileHdr(const CNetFileInfo& fi)
{
  SetDirectory(fi.IsDirectory());
  SetProviderName(fi.GetName());
  SetProviderPath(fi.GetPath());
  SetConsumerName("");
  SetConsumerPath("");
  ComputePacketN();
}
*/
CNetFileHdr::CNetFileHdr(const CNetFileHdr& fh)
{
  *this = fh;
}

CNetFileHdr& CNetFileHdr::operator=(const CNetFileHdr& fh)
{
  SetDirectory(fh.IsDirectory());
  SetFileSize(fh.GetFileSize());
  SetProviderName(fh.GetProviderName());
  SetProviderPath(fh.GetProviderPath());
  SetConsumerName(fh.GetConsumerName());
  SetConsumerPath(fh.GetConsumerPath());
  ComputePacketN();
  return *this;
}

CNetFileHdr::~CNetFileHdr()
{

}

void CNetFileHdr::SetDirectory(bool b)
{
  m_data.m_bDir = b;
}

void CNetFileHdr::SetFileSize(ulong n)
{
  m_data.m_nFileSize = n;
}

void CNetFileHdr::SetProviderName(const char* name)
{
  m_data.m_strProviderName = name;
  m_data.m_nProviderNameSz = (!name) ? 0 : strlen(name);
}

void CNetFileHdr::SetProviderPath(const char* path)
{
  m_data.m_strProviderPath = path;
  m_data.m_nProviderPathSz = (!path) ? 0 : strlen(path);
}
  

void CNetFileHdr::SetConsumerName(const char* name)
{
  m_data.m_strConsumerName = name;
  m_data.m_nConsumerNameSz = (!name) ? 0 : strlen(name);
}


void CNetFileHdr::SetConsumerPath(const char* path)
{
  m_data.m_strConsumerPath = path;
  m_data.m_nConsumerPathSz = (!path) ? 0 : strlen(path);
}

void CNetFileHdr::ComputePacketN()
{
// set the max number of packets
  m_data.m_nElemN = (m_data.m_nFileSize + MAX_NET_PACKET - 1) / MAX_NET_PACKET;
}

ulong CNetFileHdr::GetSize_Write() const
{
  //     ulong  m_nElemN;  ulong m_nSize; ulong  m_nProviderPathSz;	string m_strProviderPath; ulong  m_nNameSz;	 string m_strName;	 bool   m_bDir;  
  return GetSize_ulong() + GetSize_ulong() 
	   + GetSize_ulong() + m_data.m_nProviderPathSz 
	   + GetSize_ulong() + m_data.m_nProviderNameSz 
	   + GetSize_ulong() + m_data.m_nConsumerPathSz 
	   + GetSize_ulong() + m_data.m_nConsumerNameSz 
	   + GetSize_bool();  
}

void CNetFileHdr::ReadBody(const CNetMsg& obj)
{
  const CNetFileHdr& fhdr = dynamic_cast<const CNetFileHdr&>(obj);
  m_data.m_nElemN = fhdr.m_data.m_nElemN;
  m_data.m_nFileSize = fhdr.m_data.m_nFileSize;

  m_data.m_nProviderPathSz = fhdr.m_data.m_nProviderPathSz;
  m_data.m_strProviderPath = fhdr.m_data.m_strProviderPath;

  m_data.m_nProviderNameSz = fhdr.m_data.m_nProviderNameSz;
  m_data.m_strProviderName = fhdr.m_data.m_strProviderName;

  m_data.m_nConsumerPathSz = fhdr.m_data.m_nConsumerPathSz;
  m_data.m_strConsumerPath = fhdr.m_data.m_strConsumerPath;

  m_data.m_nConsumerNameSz = fhdr.m_data.m_nConsumerNameSz;
  m_data.m_strConsumerName = fhdr.m_data.m_strConsumerName;

  m_data.m_bDir = fhdr.m_data.m_bDir;
}

void CNetFileHdr::ReadBody()
{
  if 
  (
        !Get(m_data.m_nElemN) ||
        !Get(m_data.m_nFileSize) ||
// read the size of the string
		!Get(m_data.m_nProviderPathSz) ||
// read the string itself
        !Get(m_data.m_strProviderPath, m_data.m_nProviderPathSz) ||
// read the size of the string
		!Get(m_data.m_nProviderNameSz) ||
// read the string itself
		!Get(m_data.m_strProviderName, m_data.m_nProviderNameSz) ||
// read the size of the string
		!Get(m_data.m_nConsumerPathSz) ||
// read the string itself
        !Get(m_data.m_strConsumerPath, m_data.m_nConsumerPathSz) ||
// read the size of the string
		!Get(m_data.m_nConsumerNameSz) ||
// read the string itself
		!Get(m_data.m_strConsumerName, m_data.m_nConsumerNameSz) ||
		!Get(m_data.m_bDir)
  )
// if something went wrong
        throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetFileHdr::ReadBody()");
}

void CNetFileHdr::WriteBody()
{
  if
  (
// write the size of the string
        !Put(m_data.m_nElemN) ||
		!Put(m_data.m_nFileSize) ||

		!Put(m_data.m_nProviderPathSz) ||
        !Put(m_data.m_strProviderPath, m_data.m_nProviderPathSz) ||

		!Put(m_data.m_nProviderNameSz) ||
		!Put(m_data.m_strProviderName, m_data.m_nProviderNameSz) ||

        !Put(m_data.m_nConsumerPathSz) ||
        !Put(m_data.m_strConsumerPath, m_data.m_nConsumerPathSz) ||

		!Put(m_data.m_nConsumerNameSz) ||
		!Put(m_data.m_strConsumerName, m_data.m_nConsumerNameSz) ||

		!Put(m_data.m_bDir)
  )
// if something went wrong
        throw CNetMsgException(ERR_NET_MSG_DATA_CORRUPTED, "void CNetFileHdr::WriteBody()");
}

void CNetFileHdr::Dump(ostream& os)
{
  os << endl << "Start Dump for CNetFileHdr:" << endl << endl;
  os << "TO DO!";
  os << endl << "End Dump for CNetFileHdr" << endl << endl;
}

