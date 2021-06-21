// CNetConnection.cpp: implementation of the CNetConnector class.
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
#include "NetConnection.h"

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

CNetConnection::CNetConnection():
m_nTimeout(50),
m_nLastPacketSz(0),
m_pfnPtr(0),
m_pSubscriber(0)
{
}

CNetConnection::~CNetConnection()
{

}

void CNetConnection::InstallProgressCallBack(void* pSubscriber, PROGRESS_CB pfn)
{
  m_pfnPtr = pfn;
  m_pSubscriber = pSubscriber;
}

void CNetConnection::InvokeCallBack()
{
  if (m_pfnPtr) 
    m_pfnPtr(this, m_pSubscriber);
}