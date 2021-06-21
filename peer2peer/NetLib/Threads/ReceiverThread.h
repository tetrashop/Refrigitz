// ReceiverThread.h: interface for the CReceiverThread class.
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

#if !defined(AFX_RECEIVERTHREAD_H__7732809F_59FC_4F21_9BF7_0D5178C40613__INCLUDED_)
#define AFX_RECEIVERTHREAD_H__7732809F_59FC_4F21_9BF7_0D5178C40613__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "SendReceiveThread.h"

class CReceiverThread : public CSendReceiveThread
{
public:
	                       CReceiverThread();
	                       CReceiverThread(const CNetFileHdr& hdr);
	virtual               ~CReceiverThread();

protected:
                    bool   ConnectConsumer();
	                void   ReceiveHeader();
    virtual   CNotifFile*  CreateEventProgressFile() const;
    virtual   CNotifFile*  CreateEventDoneFile() const;
    virtual   CNotifFile*  CreateEventErrorFile() const;
    virtual   CNotifFile*  CreateEventAbortFile() const;

	virtual			void   InstallProgressCallBack();
};

#endif // !defined(AFX_RECEIVERTHREAD_H__7732809F_59FC_4F21_9BF7_0D5178C40613__INCLUDED_)
