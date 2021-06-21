// NetConnector.h: interface for the CNetConnector class.
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

#if !defined(AFX_NETCONNECTION_H__44325C25_DE14_430E_8BCE_73CCB62A213E__INCLUDED_)
#define AFX_NETCONNECTION_H__44325C25_DE14_430E_8BCE_73CCB62A213E__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetAddress.h"

class CNetConnection  
{
public:
                                                        CNetConnection();
        virtual                                        ~CNetConnection();

        virtual         CNetConnection*         		Connect(const CNetAddress&) = 0;
        virtual         CNetConnection*         		Accept(const CNetAddress&) = 0;
        virtual         void                            Disconnect() = 0;

        virtual         void                            Read(void*, unsigned long) = 0;
        virtual         void                            Write(const void*, unsigned long) = 0;

						void							InstallProgressCallBack(void*, PROGRESS_CB);

        virtual         bool                            IsConnected() const = 0;
        virtual         bool                            IsConnected(const CNetAddress&) const = 0;

        virtual         bool                            CanRead() = 0;
        virtual         bool                            CanWrite() = 0;

		                ulong                           GetLastPacketSz() const {return m_nLastPacketSz;}

        virtual         ulong                           GetRemoteHandle() const = 0;
        virtual         ulong                           GetLocalHandle() const = 0;

		                void                            SetTimeout(ulong t) {m_nTimeout = t;}

		virtual         void                            SetBlocking(bool) = 0;
protected:
        virtual         void                            HandleException(int, const char*) const = 0;

		                void                            InvokeCallBack();

protected:
        ulong         m_nTimeout;
		PROGRESS_CB   m_pfnPtr;
		void*         m_pSubscriber;
		ulong         m_nLastPacketSz;
};

#endif // !defined(AFX_NETCONNECTION_H__44325C25_DE14_430E_8BCE_73CCB62A213E__INCLUDED_)
