// Mutex.cpp: implementation of the CMutex class.
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
#include "Mutex.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CMutex::CMutex()
{
  m_hMutex = CreateMutex(NULL, FALSE, NULL);
  if (m_hMutex == NULL) HandleError();
}

CMutex::~CMutex()
{
  m_nLastError = 0;
  if (!CloseHandle(m_hMutex)) 
	HandleError();
}

bool CMutex::Lock(unsigned long nTimeout/* = INFINITE*/)
{
// attempt to gain ownership on the m_hMutex
  DWORD dwRet = WaitForSingleObject(m_hMutex, nTimeout);
  switch (dwRet) {
	case WAIT_ABANDONED :
	case WAIT_OBJECT_0	: return true;
	case WAIT_FAILED	: HandleError(); return false;
    case WAIT_TIMEOUT	: return false;
  }
  return false;
}

bool CMutex::Unlock()
{
  if (ReleaseMutex(m_hMutex) == TRUE) return true;
  HandleError(); 
  return false;
}

void CMutex::HandleError()
{
  m_nLastError = GetLastError();
}

int CMutex::GetLastError() const
{
  return m_nLastError;
}
