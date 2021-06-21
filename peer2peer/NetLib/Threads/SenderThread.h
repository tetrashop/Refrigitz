// SenderThread.h: interface for the CSenderThread class.
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

#if !defined(AFX_SENDERTHREAD_H__FB7F8D72_640C_4222_80FC_630113F15BD0__INCLUDED_)
#define AFX_SENDERTHREAD_H__FB7F8D72_640C_4222_80FC_630113F15BD0__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "SendReceiveThread.h"

class CNetAddress;

class CSenderThread : public CSendReceiveThread  
{
public:
//	                        CSenderThread();
                            CSenderThread(const CNetFileHdr& hdr);
							  

	virtual                ~CSenderThread();

protected:
                    bool    ConnectConsumer();

					void    SendHeader();
					bool    IsConfirmed();

    virtual   CNotifFile*   CreateEventProgressFile() const;
    virtual   CNotifFile*   CreateEventDoneFile() const;
    virtual   CNotifFile*   CreateEventErrorFile() const;
    virtual   CNotifFile*   CreateEventAbortFile() const;

	virtual			void    InstallProgressCallBack();
};

#endif // !defined(AFX_SENDERTHREAD_H__FB7F8D72_640C_4222_80FC_630113F15BD0__INCLUDED_)
