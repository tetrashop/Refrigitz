// NetPortScanner.h: interface for the CNetPortScanner class.
//
// Written by Marat Bedretdinov (maratb@hotmail.com)
// Copyright (c) 2000.
//
// This code may be used in compiled form in any way you desire. This
// file may be redistributed unmodified by any means PROVIDING it is 
// not sold for profit without the authors written consent, and 
// providing that this notice and the authors name is included. 
//
// This file is provided "as is" with no expressed or implied warranty.
// The author accepts no liability if it causes any damage whatsoever.
// It's free - so you get what you pay for.//

#if !defined(AFX_NETPORTSCANNER_H__25C3E5BC_F7C0_4430_B565_3E798F42E43E__INCLUDED_)
#define AFX_NETPORTSCANNER_H__25C3E5BC_F7C0_4430_B565_3E798F42E43E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "Notification.h"

class CNotifSubscriber;
class CNetThread;
class CNetConnection;

class CNetScanThread : public CNetThread
{
friend class CNetPortScanner;
public:
	                         CNetScanThread():
                               m_nBegin(0), 
							   m_nEnd(0), 
							   m_nNow(0),
							   m_pConnector(0) {}

  virtual					~CNetScanThread() {}
// this address does not include the port numer, just the address
                     void    SetAddress(const string& strAddr) 
								{ m_strAddr = strAddr; }

					 void    SetPortRange(unsigned int b, unsigned int e)
					 { m_nNow = m_nBegin = b; m_nEnd = e;}
protected:
	                 void    Process();
					 void    Cleanup();

	                 bool    GetNextAddress(string&);
					 void    CreateConnector();
					 bool    IsLocalAddress() const;
protected:
// this holds just the ip part of the address, NO port number
	string          m_strAddr;
	unsigned int    m_nBegin;
	unsigned int    m_nEnd;
	unsigned int    m_nNow;
	CNetConnection*	m_pConnector;
};

class CNetPortScanner  : public CNotifSubscriber
{
public:
	                         CNetPortScanner();
	virtual                 ~CNetPortScanner();
// will be notified when a port is found to be available
	                bool     Subscribe(CNotifSubscriber*);
					bool     Unsubscribe(CNotifSubscriber*);
// sets the address on which ports are to be checked
					void     SetAddress(const string&);
					void     SetPortRange(unsigned int b, unsigned int e);
// scans the ports in a separate thread
					void	 StartScan();
// scans the ports in the current thread
					bool	 ScanNow();
// interrupts the scanning process
					void     StopScan();
					bool     IsStoped() const;

	                void     OnEvent(CNotifEvent*);
					void	 GetAddress(string& addr) { addr = m_strAddr; }
protected:
  CNetScanThread  m_thread;
// this address an available address + port number
  string          m_strAddr;
  bool            m_bPortFound;
};



#endif // !defined(AFX_NETPORTSCANNER_H__25C3E5BC_F7C0_4430_B565_3E798F42E43E__INCLUDED_)
