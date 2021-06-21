// Notification.h: interface for the CNotification class.
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

#if !defined(AFX_NOTIFICATION_H__5DC42029_3A68_437D_8939_199391C1376D__INCLUDED_)
#define AFX_NOTIFICATION_H__5DC42029_3A68_437D_8939_199391C1376D__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "ThreadStorage.h"

class CNotifEvent
{
public:
	                      CNotifEvent():
							m_pX(0),
							m_threadId(0),
							m_nRefCount(1) {}
	
    virtual               ~CNotifEvent() 
							{
							  _DELETE(m_pX);
							}

	virtual unsigned int  event_id() const = 0;

	virtual CNotifEvent*  clone() const = 0;

	virtual CNotifEvent&  operator=(const CNotifEvent& e)
							{
								m_threadId = e.m_threadId;
								_DELETE(m_pX);
								if (e.m_pX) {
								  m_pX = e.m_pX->clone();
								  *m_pX = *(e.m_pX);
								}
								return *this;
							}

	                void  setThreadId(unsigned long tid) 
							{m_threadId = tid;}

					void  setException(CNetException* pX) 
							{m_pX = pX;}

		   unsigned long  getThreadId() const
							{return m_threadId;}

	 const CNetException& getException() 
							{return *m_pX;}
protected:
	unsigned long  m_threadId;
	long		   m_nRefCount;
	CMutex         m_mutex;
	CNetException* m_pX;
};

class CNotifSubscriber  
{
public:
					  CNotifSubscriber():m_bHandleNotify(false) {}

    virtual          ~CNotifSubscriber() {}
// each subscriber (running in a separate thread perhaps)
// gets its own copy of CNotifEvent instance. That way
// I don't have to worry synchronizing access to the data within the
// event objects and event destruction
// if the thread supports its own queue (say a CWinThread)
// then have it handle the event its own way (say PostMessage(...) or PostThreadMessage(...)
// that will ultimatelly add this event into their own queue.
// Otherwise add this event into the this class' supplied queue
	virtual    void   OnEvent(CNotifEvent* pEvent)
					{  
					   CNotifEvent* pClone = pEvent->clone();
					   *pClone = *pEvent;
					   if (m_bHandleNotify)
						 HandleNotification(pClone);
					   else
				   // the notification handler has to delete pEvent
					     m_queue.Add(pClone);

					   delete pEvent;
					}
	           bool   IsThereEvent() 
					{
		              return m_queue.IsThereData();
					}

	    CNotifEvent*  GetEvent()
					{
					   if (!m_queue.IsThereData())
			             return 0;
		               return m_queue.Retrieve();
					}
		void          EmptyQueue() 
					{
					   m_queue.EmptyQueue();
					}
 unsigned int         GetQueueSize()
					{
					   return m_queue.Size();
					}

protected:
 virtual void		HandleNotification(CNotifEvent*) {}

protected:
	CQueue<CNotifEvent*> m_queue;
	bool				 m_bHandleNotify;
};


#endif // !defined(AFX_NOTIFICATION_H__5DC42029_3A68_437D_8939_199391C1376D__INCLUDED_)
