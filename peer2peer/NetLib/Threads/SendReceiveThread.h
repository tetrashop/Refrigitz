// SendReceiveThread.h: interface for the CSendReceiveThread class.
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

#if !defined(AFX_SENDRECEIVETHREAD_H__9EEF1B75_06F6_4297_A479_13C60901D7A5__INCLUDED_)
#define AFX_SENDRECEIVETHREAD_H__9EEF1B75_06F6_4297_A479_13C60901D7A5__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetThread.h"
#include "NetStream.h"
#include "NetFileHdr.h"

class CNotifFile;
class CNetAddress;
class CNetConnection;
class CNetFileInfo;
class CNetFile;

class CSendReceiveThread : public CNetThread  
{
public:

	                               CSendReceiveThread():
								     m_nsStatus(nsPending),
									 m_pFile(0),
									 m_pConAddr(0),
									 m_pProvider(0),
									 m_pConsumer(0) {}

	                               CSendReceiveThread(const CNetFileHdr& hdr):
									 m_nsStatus(nsPending),
	                                 m_fHdr(hdr),
									 m_pFile(0),
									 m_pConAddr(0),
									 m_pProvider(0),
									 m_pConsumer(0) {}

	virtual                       ~CSendReceiveThread();

	                void		   SetProviderConnector(CNetConnection* pConnection)
									{ m_pProvider->Attach(pConnection); }

	                void           SetConsumerAddress(CNetAddress* pAddr)
									{ m_pConAddr = pAddr; }

					void           SetFileStatus(netCommonStatus nsStatus)
									{ m_nsStatus = nsStatus; }
protected:
	                void           Process();
	                void	       Cleanup();

    virtual			bool           ConnectConsumer();

                    void           SendReceiveFile();

                    bool           IsConnected();

    virtual   CNotifFile*          CreateEventProgressFile() const = 0;
    virtual   CNotifFile*          CreateEventDoneFile() const = 0;
    virtual   CNotifFile*          CreateEventErrorFile() const = 0;
	virtual   CNotifFile*          CreateEventAbortFile() const = 0;

	virtual			void		   InstallProgressCallBack() = 0;

    static	        void           OnTransmitProgress(void*, void*);

protected:
    CNetFile*                   m_pFile;
	CNetAddress*                m_pConAddr;
	CNetFileHdr                 m_fHdr;
	CNetStream*                 m_pProvider;
	CNetStream*                 m_pConsumer;
	netCommonStatus				m_nsStatus;
};

#endif // !defined(AFX_SENDRECEIVETHREAD_H__9EEF1B75_06F6_4297_A479_13C60901D7A5__INCLUDED_)
