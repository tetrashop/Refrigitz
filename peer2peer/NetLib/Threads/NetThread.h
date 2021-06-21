// NetThread.h: interface for the CNetThread class.
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

#if !defined(AFX_NETTHREAD_H__150644D9_A351_4A47_9132_4A128D10213A__INCLUDED_)
#define AFX_NETTHREAD_H__150644D9_A351_4A47_9132_4A128D10213A__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "SafeVector.h"

class CNotifSubscriber;
class CNotifEvent;

// Use:
// 1. Derive your own C{XXX}Thread class.
// 2. Override CNetThread::Process() method
// 3. If you thread is going to allocated resouces on the heap
//    override CNetThread::Cleanup() method
// 4. Implement CNetThread::Process(). This is you main entry point,
//    just like main() function of any C program. The difference
//    is that this function will be called in cycle. If you will 
//    be doing lenghty operations in CNetThread::Process() you should
//    check CanContinue() within reasonable short periods of time (computer time).
//    If CanContinue() returns false, that means that another controlling
//    thread has called CNetThread::Stop() and is blocked, until your thread
//    is terminated. At least that's the recommended scenario. So say your
//    application (the main thread) is about to terminate, and say 
//    it plays the role of the controlling thread. In order for
//    your process to terminate in a clean manner, without leaving any
//    unfreed resources all threads must be terminated prior the termination
//    of the main thread. To do that, for each CNetThread instance
//    the main thread controlls, it will call CNetThread::Stop(). Now
//    if your thread is busy doing something lenghty you process is blocked
//    until you realise that you were told to quit (read the CNetThread::CanContinue())
//    and do quit. 
// 5. Create the instance of you C{XXX}Thread in your code. 
// 6. Call CNetThread::Start() - the execution thread is alive and kicking
// 7. Call CNetThread::Stop() from a contolling thread (see remarks) -
//    the execution thread is terminated. The instance of CNetThread is intact
// 8. Repeat step 6 and 7 as many times as you need to.
// 9. If you need to terminate the execution thread and release
//    the instance of CNetThread associated with that thread
//    in you overridden CNetThread::Cleanup() you last line
//    has to state "delete this"

// this class works under Windows and POSIX Threads compatible platform
class CNetThread
{
public:
						CNetThread();
	virtual				~CNetThread();

			void		Start();
// call it from another thread which will block until 'this' thread is terminated
			void		Stop();

// call it from within the thread if want to terminated itself
			void		PostStop() {m_bCanContinue = false;}

#ifdef _WIN32
   unsigned long        GetThreadId() const {return m_threadContext;}
#else
	   pthread_t        GetThreadId() const {return m_threadContext;}
#endif
			bool		CanContinue() const;
			bool		IsStoped() const;

// Subscribe whoever wants to know what is going on in the thread processing.
// A derivable of this class will have to call Notify(CNotifEvent*) at whatever 
// moment it desires from within the Process() method. A poiter to a specific event
// is passed in. The notified objects can then behave accordingly
			bool		Subscribe(CNotifSubscriber*);
			bool        Unsubscribe(CNotifSubscriber*);

protected:
#ifdef _WIN32
	unsigned long	    m_threadContext;
#else
	pthread_t           m_threadContext;
#endif

#ifdef _WIN32
	static	void		Run(void*);
#else
	static	void		Run(void*);
#endif
	virtual void		Process() = 0;
	virtual void		Cleanup() = 0;
	        void        Notify(CNotifEvent*);

protected:
	volatile bool					m_bCanContinue;
	volatile bool					m_bStoped;

    safe_vector<CNotifSubscriber*> m_vecNotifees;
};

#endif // !defined(AFX_NETTHREAD_H__150644D9_A351_4A47_9132_4A128D10213A__INCLUDED_)
