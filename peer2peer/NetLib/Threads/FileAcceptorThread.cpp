// FileAcceptorThread.cpp: implementation of the CFileAcceptorThread class.
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
#include "ReceiverThread.h"
#include "ThreadStorage.h"
#include "NetNotif.h"
#include "FileAcceptorThread.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CFileAcceptorThread::CFileAcceptorThread()
{

}

CFileAcceptorThread::~CFileAcceptorThread()
{

}

bool CFileAcceptorThread::SetAddress(ulong nStart, ulong nEnd)
{
// by now previously created connection must be removed by the UI thread
  m_pConnection = 0;
  bool bConnected = false;
// set up the query
  m_portScanner.SetAddress(CNetIPAddress::MakeLocalHostAddr(NULL));
  m_portScanner.SetPortRange(nStart, nEnd);

  while (!bConnected) {
  // close previous listening connection
    delete m_pSockAcceptor;
    m_pSockAcceptor = 0;
    try {
   // if returns false then I ran out of ports to scan
      if (!m_portScanner.ScanNow())
		return false;
	  m_portScanner.GetAddress(m_AcceptAddr);
  // resolver the address
      CNetIPAddress address;
  	  address.SetConnectString(m_AcceptAddr.c_str());
  // bind to the new address
      m_pSockAcceptor = new CNetSockAcceptor;
      m_pSockAcceptor->Connect(address);
      m_pSockAcceptor->SetTimeout(1000);
	} catch (CNetException) {
	}
// if no exceptions then accepter is all set
	bConnected = true;
  }
  return true;
}

void CFileAcceptorThread::Process()
{
  try {
// if there is no request return
   if (!m_pSockAcceptor->CanRead()) return;

// got the request, accept it!
   CNetIPAddress address;
// returns new instance
   CNetConnection* pConnector = m_pSockAcceptor->Accept(address);

   Notify(new CNotifAcceptForFile(pConnector));

  } catch (CNetException e) {
	_DELETE(m_pConnection);
// stop the execution thread, so thread doesn't try to accept any more calls
	PostStop();
  }
}
