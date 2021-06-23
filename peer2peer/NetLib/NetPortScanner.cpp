// NetPortScanner.cpp: implementation of the CNetPortScanner class.
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
#include "NetNotif.h"
#include "NetThread.h"
#include "NetSockAcceptor.h"
#include "NetSockConnector.h"
#include "NetIPAddress.h"
#include "NetPortScanner.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetPortScanner::CNetPortScanner():
m_bPortFound(false)
{
  
}

CNetPortScanner::~CNetPortScanner()
{
}

// will be notified when a port is found to be available
bool CNetPortScanner::Subscribe(CNotifSubscriber* p)
{
  return m_thread.Subscribe(p);
}

bool CNetPortScanner::Unsubscribe(CNotifSubscriber* p)
{
  return m_thread.Unsubscribe(p);
}

// sets the address on which ports are to be checked
void CNetPortScanner::SetAddress(const string& addr)
{
  m_thread.SetAddress(addr);
}

void CNetPortScanner::SetPortRange(unsigned int b, unsigned int e)
{
  m_thread.SetPortRange(b, e);
}

// scans the ports in a separate thread
void CNetPortScanner::StartScan()
{
  m_thread.Start();
}

// interrupts the scanning process
void CNetPortScanner::StopScan()
{
  m_thread.Stop();
}

bool CNetPortScanner::IsStoped() const
{
  return m_thread.IsStoped();
}

bool CNetPortScanner::ScanNow()
{
// have the notification come to 'this'
  m_thread.Subscribe(this);
// initiate scanning
  m_thread.Process();
// by now if m_bPortFound == true then there is an available port
// if m_bPortFound == false then I ran out of ports  
  return m_bPortFound;
}

void CNetPortScanner::OnEvent(CNotifEvent* pEvent)
{
  const CNotifPortAvailable& event = dynamic_cast<const CNotifPortAvailable&>(*pEvent);
  event.GetAddress(m_strAddr);

  if (event.event_id() == UWM_ON_ADDR_PORT_AVAILABLE)
	m_bPortFound = true;
  else
	if (event.event_id() == UWM_ON_ADDR_PORT_UNAVAILABLE) {
	  m_bPortFound = false;
	  // do a single scan again, util found one or no more ports to check
      m_thread.Process();
	}
	else
	  if (event.event_id() == UWM_ON_NO_ADDR_PORT_AVAILABLE)
	    m_bPortFound = false;

  delete pEvent;
}

bool CNetScanThread::GetNextAddress(string& addr)
{
  if (m_nNow > m_nEnd)
	return false;

  addr = m_strAddr;
// "port='" + up to 10 digits in 'int' type + "';"
  char port[19];
// make up a string out of the port number and increase for the next query
  sprintf_s(port, "port='%d';", m_nNow++);
  addr += port;
  return true;
}

bool CNetScanThread::IsLocalAddress() const
{
  if (CNetIPAddress::MakeLocalHostAddr(NULL) == m_strAddr)
	return true;
  return false;
}

void CNetScanThread::CreateConnector()
{
  if (IsLocalAddress())
	m_pConnector = new CNetSockAcceptor;
  else
	m_pConnector = new CNetSockConnector;
}

void CNetScanThread::Process()
{
// close previous listening connection
  Cleanup();
  // resolver the address
  CNetIPAddress address;
  string strAddr; 
  try {
	if (!GetNextAddress(strAddr)) {
	  PostStop();
 // let the subscribers know that all ports are unavailable
      Notify(new CNotifNoPortAvailable(strAddr));
	  return;
	}
	address.SetConnectString(strAddr.c_str());
    CreateConnector();
 // connect to the new address
    m_pConnector->Connect(address);
  } catch (CNetException) {
// if this was a local address and I got an exception then this address is occupied
	if (IsLocalAddress()) {
	  Cleanup();
// let the subscribers know that this port is unavailable
      Notify(new CNotifPortUnavailable(strAddr));
	  return;
	}
  }
  Cleanup();
// let the subscribers know that this port on this local address is available
  Notify(new CNotifPortAvailable(strAddr));
}

void CNetScanThread::Cleanup()
{
  _DELETE(m_pConnector);
}


