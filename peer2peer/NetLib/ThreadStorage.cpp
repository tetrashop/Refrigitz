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
#include "ThreadStorage.h"


CThreadStorage& g_getThreadStorage()
{
  static CThreadStorage theThreadStorage;
  return theThreadStorage;
}

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CThreadStorage::CThreadStorage()
{

}

CThreadStorage::~CThreadStorage()
{

}

void CThreadStorage::Add(CNetThread* pThread)
{
  m_vecThreads.push_back(pThread);
}

void CThreadStorage::Remove(CNetThread* pThread)
{
  unsigned int i = 0;
  try {
    while (m_vecThreads[i] != pThread)
	  i++;
  } catch (out_of_range x) {
	  return;
	}
  m_vecThreads.pop_at(i);
}

CNetThread* CThreadStorage::GetAt(unsigned int n)
{
  return m_vecThreads[n];
}

CNetThread* CThreadStorage::GetByThreadId(unsigned long tid)
{
  unsigned int i = 0;
  try {
    while (m_vecThreads[i]->GetThreadId() != tid)
	  i++;
  } catch (out_of_range x) {
	  return 0;
	}
  return m_vecThreads[i];
}


