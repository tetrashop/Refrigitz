// NetSockConnection.h: interface for the CNetSockConnection class.
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

#if !defined(AFX_NETSOCKCONNECTION_H__BDDE9B8B_9A92_452A_AEDE_BB8A6FE074C9__INCLUDED_)
#define AFX_NETSOCKCONNECTION_H__BDDE9B8B_9A92_452A_AEDE_BB8A6FE074C9__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetConnection.h"

class CNetSockConnection : public CNetConnection  
{
public:

#ifdef _WIN32
                static  void            InitWinSock();
                static   bool           m_bWinSockLoaded;
#endif
                                        CNetSockConnection();
                                        CNetSockConnection(SOCKET);
        virtual                         ~CNetSockConnection();

        virtual         void            Disconnect();

        virtual         void            Read(void*, unsigned long);
        virtual         void            Write(const void*, unsigned long);


        virtual         bool            CanRead();
        virtual         bool            CanWrite();

        virtual         bool            IsConnected() const;
        
                        void            GetRemoteAddr(CNetAddress&) const;
                        void            GetLocalAddr(CNetAddress&) const;

                       ulong            GetRemoteHandle() const;

                       ulong            GetLocalHandle() const;

                        void            SetBlocking(bool);
protected:
        virtual         void            HandleException(int, const char*) const;
protected:
        SOCKET          m_hSocket;
};

#endif // !defined(AFX_NETSOCKCONNECTION_H__BDDE9B8B_9A92_452A_AEDE_BB8A6FE074C9__INCLUDED_)
