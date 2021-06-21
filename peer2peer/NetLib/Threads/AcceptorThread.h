// AcceptorThread.h: interface for the CAcceptorThread class.
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

#if !defined(AFX_ACCEPTORTHREAD_H__732CF9C5_669E_4ABC_A80C_80B423E83F5A__INCLUDED_)
#define AFX_ACCEPTORTHREAD_H__732CF9C5_669E_4ABC_A80C_80B423E83F5A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetThread.h"
#include "NetSockAcceptor.h"

class CNetConnection;

class CAcceptorThread : public CNetThread  
{
public:
							CAcceptorThread();
	virtual					~CAcceptorThread();

					bool	SetAddress(const string& strAddr);
             const char*    GetAddress() const;

			CNetConnection*	GetConnection() {return m_pConnection;}
    
protected:
	virtual			void	Process();
	virtual			void	Cleanup();
protected:
	CNetSockAcceptor*	m_pSockAcceptor;
	CNetConnection*		m_pConnection;
	string              m_AcceptAddr;
};

#endif // !defined(AFX_ACCEPTORTHREAD_H__732CF9C5_669E_4ABC_A80C_80B423E83F5A__INCLUDED_)
