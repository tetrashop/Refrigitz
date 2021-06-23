// NetFileInfo.cpp: implementation of the CNetFileInfo class.
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
#include "NetFileInfo.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetFileInfo::CNetFileInfo(const char* path, const char* filter):
m_path(path),
m_filter(filter),
m_bDigDeep(true),
m_bDigged(false),
m_bHasChildren(false),
m_nDepth(0)
{
  memset(&m_fdata, 0, sizeof(m_fdata));

  strcpy_s(m_fdata.cFileName, filter);

  string s = m_path;
  s += '\\';
  s += m_filter;

  m_fdata.dwFileAttributes = GetFileAttributes(s.c_str());

  // only works with Windows 98 :-(
/*
  WIN32_FILE_ATTRIBUTE_DATA data;
  if (!GetFileAttributesEx(m_path.c_str(), GetFileExInfoStandard, &data)) return;

  m_fdata.dwFileAttributes = data.dwFileAttributes;
  m_fdata.ftCreationTime   = data.ftCreationTime;
  m_fdata.ftLastAccessTime = data.ftLastAccessTime;
  m_fdata.ftLastWriteTime  = data.ftLastWriteTime;
  m_fdata.nFileSizeHigh	   = data.nFileSizeHigh;
  m_fdata.nFileSizeLow	   = data.nFileSizeLow;
*/
}

CNetFileInfo::CNetFileInfo(const char* path, const char* filter, const WIN32_FIND_DATA& fdata, CNetFileInfo* pParent, unsigned long nDepth):
m_path(path),
m_filter(filter),
m_bDigDeep(true),
m_bDigged(false),
m_bHasChildren(false),
m_nDepth(nDepth)
{
  memcpy(&m_fdata, &fdata, sizeof(m_fdata));
}

CNetFileInfo::~CNetFileInfo()
{
  Cleanup();
}

void CNetFileInfo::Cleanup()
{
  for (int i=0; i<(int)m_vecFiles.size(); i++)
    delete m_vecFiles[i];
  m_vecFiles.clear();
}

void CNetFileInfo::FindChildren()
{
// cleanup any previous finds
  Cleanup();
// indicate that the children were searched for
  m_bDigged = true;
// form full filter
  string s = m_path;
  s += '\\';
  s += m_filter;

  WIN32_FIND_DATA	info;
  HANDLE			hHandle;
// find the first file/directory in this directory
  BOOL bFound = ((hHandle = FindFirstFile(s.c_str(), &info)) != INVALID_HANDLE_VALUE);

  while (bFound) {
	if (!strcmp(info.cFileName, ".") || !strcmp(info.cFileName, "..")) {
	  bFound = FindNextFile(hHandle, &info);
	  continue;
	}
// indicate that this file has children
	m_bHasChildren = true;
	string nf = m_path;
	if (info.dwFileAttributes == FILE_ATTRIBUTE_DIRECTORY) {
	  nf += '\\';
	  nf += info.cFileName;
	}
	CNetFileInfo* pNetFileInfo = new CNetFileInfo(nf.c_str(), m_filter.c_str(), info, this, GetDepth() + 1);
	m_vecFiles.push_back(pNetFileInfo);
// find all children for the new found directory
    if (info.dwFileAttributes == FILE_ATTRIBUTE_DIRECTORY && m_bDigDeep)
      pNetFileInfo->FindChildren();

    bFound = FindNextFile(hHandle, &info);
  }
  FindClose(hHandle);
}

CNetFileInfo* CNetFileInfo::FindChild(const char* path)
{
  if (!strcmp(GetPath(), path)) 
	return this;
  for (int i=0; i< (int)m_vecFiles.size(); i++) {
	CNetFileInfo* pFound = m_vecFiles[i]->FindChild(path);
	if (pFound) return pFound;
  }
  return 0;
}
