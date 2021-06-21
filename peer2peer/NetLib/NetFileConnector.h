// NetFileConnector.h: interface for the CNetFileConnector class.
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

#if !defined(AFX_NETFILECONNECTOR_H__F5F56045_4115_4370_8130_E510481D81EF__INCLUDED_)
#define AFX_NETFILECONNECTOR_H__F5F56045_4115_4370_8130_E510481D81EF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetConnection.h"
#include "NetSockConnector.h"

class CNetFileConnector : public CNetConnection
{
public:
								CNetFileConnector() {}
	virtual                    ~CNetFileConnector();

	virtual         void		Disconnect();

    virtual			bool        IsConnected() const;
    virtual			bool        IsConnected(const CNetAddress&) const;

// does nothing for the system files
	                void        SetBlocking(bool) {}

    virtual			bool        CanRead();
    virtual			bool        CanWrite();

					void        GetLocalAddr(CNetAddress&) const;
                   ulong        GetRemoteHandle() const;
                   ulong        GetLocalHandle() const;
				   ulong        GetSize();

// retunrs true if there are data for 'the current position + len' length ahead
                   ulong        CanRead(ulong len);
				   ulong        GetCurrentPos();

protected:
        virtual      void		HandleException(int, const char*) const;
protected:
        fstream m_ioStream;
};

class CNetBinFileConnector : public CNetFileConnector
{
public:
									CNetBinFileConnector();
	virtual                        ~CNetBinFileConnector();

    virtual   CNetConnection*       Connect(const CNetAddress&);
    virtual	  CNetConnection*       Accept(const CNetAddress& addr) {return Connect(addr);}
        
    virtual   void                  Read(void*, ulong);
    virtual   void                  Write(const void*, ulong);
};

class CNetTextFileConnector : public CNetFileConnector
{
public:
									CNetTextFileConnector();
	virtual						   ~CNetTextFileConnector();

	virtual   CNetConnection*       Connect(const CNetAddress&);
	virtual   CNetConnection*       Accept(const CNetAddress& addr) {return Connect(addr);}

	virtual   void                  Read(void*, unsigned long);
	virtual   void                  Write(const void*, unsigned long);
};

#endif // !defined(AFX_NETFILECONNECTOR_H__F5F56045_4115_4370_8130_E510481D81EF__INCLUDED_)
