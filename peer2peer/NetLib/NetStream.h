// NetStream.h: interface for the CNetStream class.
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

#if !defined(AFX_NETSTREAM_H__41C753E3_5328_4076_9A84_FBFBFC2463C2__INCLUDED_)
#define AFX_NETSTREAM_H__41C753E3_5328_4076_9A84_FBFBFC2463C2__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

//#include "NetMutex.h"

class CNetConnection;
class CNetMsg;
class CMsgFactory;

class CNetStream  
{
friend class CNetMsg;
public:
								CNetStream();
        virtual                 ~CNetStream();

// lets the stream to redirect its data flow through a differnet connector
// the return value is the previously used connector 
   CNetConnection*              Attach(CNetConnection*);
// lets the stream to handle different type of messages
// the return value is the previsouly used message factory 
// there is no Deatch(CMsgFactory*) method, based on assumtion
// that factories are created statically
   CMsgFactory*					Attach(CMsgFactory*);
// makes the stream unusable, however the connector is preserved and may
// be reassociated with a different stream
   CNetConnection*              Detach();
// returns true if an active connector is present for this stream
                bool            IsOpen() const;
				bool            IsThereData();
// reads data from the CNetConnector and constructs CNetMessage out of it
   virtual CNetStream&           operator>>(CNetMsg*&);
// writes data from CNetMessage into the CNetConnector
   virtual CNetStream&           operator<<(CNetMsg&);

           CNetConnection*       GetConnection() {return m_pConnector;}

//		       ulong             GetLastPacketSz() const;

//               void              InstallProgressCallBack(void*, PROGRESS_CB);
protected:
                   void         Write(CNetMsg&);
				CNetMsg*		CreateMessage(long nClassId);
protected:
        CNetConnection*         m_pConnector;
		CMsgFactory*			m_pFactory;
};

class CFlatStream : public CNetStream
{
friend class CNetMsg;
public:
                                CFlatStream() {}
	virtual                    ~CFlatStream() {}

// reads data from the CNetConnector and constructs CNetMessage out of it
          CNetStream&           operator>>(CNetMsg*&);
// writes data from CNetMessage into the CNetConnector
          CNetStream&           operator<<(CNetMsg&);
};


#endif // !defined(AFX_NETSTREAM_H__41C753E3_5328_4076_9A84_FBFBFC2463C2__INCLUDED_)

// static        CNetStream*    CreateReceivingStream(nppProtocol, int nPort, bool bThreaded);
// static        CNetStream*    CreateSendingStream(nppProtocol, const char* strHostName, int nPort);
//                              void    ConnectThreaded(nppProtocol, int);
//                              void    ConnectThreaded(nppProtocol,const char*, int);
