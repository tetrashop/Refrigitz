// AcceptorThread.cpp: implementation of the CAcceptorThread class.
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
#include "NetIPAddress.h"
#include "NetNotif.h"
#include "AcceptorThread.h"


#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CAcceptorThread::CAcceptorThread():
m_pSockAcceptor(0),
m_pConnection(0)
{
}

CAcceptorThread::~CAcceptorThread()
{
}

bool CAcceptorThread::SetAddress(const string& strAddr)
{
  m_AcceptAddr = strAddr;
// by now previously created connection must be removed by the UI thread
  m_pConnection = 0;
// close previous listening connection
  delete m_pSockAcceptor;
  m_pSockAcceptor = 0;
  try {
// resolver the address
    CNetIPAddress address;
	address.SetConnectString(strAddr.c_str());
// bind to the new address
    m_pSockAcceptor = new CNetSockAcceptor;
    m_pSockAcceptor->Connect(address);
	m_pSockAcceptor->SetTimeout(1000);
  } catch (CNetException) {
// remove the acceptor
	delete m_pSockAcceptor;
// reset it, so next call to SetAddress() would not die on delete m_pAcceptor
	m_pSockAcceptor = 0;
	return false;
  }
  return true;
}

const char* CAcceptorThread::GetAddress() const
{
  return m_AcceptAddr.c_str();
}

// gets called just before the quitting of the main thread function
void CAcceptorThread::Cleanup()
{
// close acceptor connection
  delete m_pSockAcceptor;
  m_pSockAcceptor = 0;
}

void CAcceptorThread::Process()
{
  AfxGetApp()->PostThreadMessage(UWM_TEST_MESSAGE, 0, 0);
  try {
   Notify(new CNotifListening);

// if there is no request return
   if (!m_pSockAcceptor->CanRead()) return;
// got the request, accept it!
   CNetIPAddress address;
   m_pConnection = m_pSockAcceptor->Accept(address);

// close acceptor connection
   delete m_pSockAcceptor;
   m_pSockAcceptor = 0;

   Notify(new CNotifAccept);
// stop the execution thread, so thread doesn't try to accept any more calls
   PostStop();
  } catch (CNetException e) {
	delete m_pConnection;
	m_pConnection = 0;
// close acceptor connection
    delete m_pSockAcceptor;
    m_pSockAcceptor = 0;
	Notify(new CNotifErrorAccept);
// stop the execution thread, so thread doesn't try to accept any more calls
	PostStop();
  }
}
