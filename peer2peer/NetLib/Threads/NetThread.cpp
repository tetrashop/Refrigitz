// NetThread.cpp: implementation of the CNetThread class.
//
// Written by Marat Bedretdinov (maratb@hotmail.com)
// Copyright (c) 2000.
//
// This code may be used in compiled form in any way you desire. This
// file may be redistributed unmodified by any means PROVIDING it is 
// not sold for profit without the authors written consent, and 
// providing that this notice and the authors name is included. 
//
// If the source code in  this file is used in any commercial application 
// then acknowledgement must be made to the author of this file 
// and permissions to use this file are requested from the author
//
// (in whatever form you wish).// This file is provided "as is" with no expressed or implied warranty.
// The author accepts no liability if it causes any damage whatsoever.
// It's free - so you get what you pay for.//

#include "stdafx.h"
#include "Notification.h"
#include "NetThread.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

#ifdef _PTHREADS_DRAFT4
  #define THREAD_DEFATTR pthread_attr_default
#else
  #define THREAD_DEFATTR 0
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CNetThread::CNetThread():
m_bCanContinue(true),
m_bStoped(true)
{
 m_threadContext = 0;
}

CNetThread::~CNetThread()
{
}

// Warning! This method must be called from a contolling thread
void CNetThread::Start()
{
// if this thread had already been Started return
  if (!IsStoped()) {
	Stop();
    return;
  }
  m_bCanContinue = true;
#ifdef _WIN32
    m_threadContext = _beginthread(Run, 0, this);
// if failed creation _beginthread returns -1, then quit (m_bStoped = true)
  m_bStoped = m_threadContext == (unsigned long)(-1L);
  m_bCanContinue = !m_bStoped;
#else
  int result = pthread_create(m_threadContext, THREAD_DEFATTR, Run, this);
  m_bStoped = result != 0;
  m_bCanContinue = !m_bStoped;
#endif
}


// Warning! This method must be called from a contolling thread
// if not, then 'Wait' function will deadlock (thread will wait on itself)
// Stops the execution thread
void CNetThread::Stop()
{
// let the thread know that the next time it comes around its main loop
// it has to quit
  m_bCanContinue = false;

// if the execution thread had already been stoped just return
//  if (IsStoped())
//	return;

#ifdef _WIN32
// make sure this is a different thread
  HANDLE ctctx = GetCurrentThread();
  ASSERT((HANDLE)m_threadContext != ctctx);
// wait until the thread reads out the m_bCanContinue 
// and exists the Run() method
  WaitForSingleObject((HANDLE)m_threadContext, INFINITE);
#else
  void* pResult;
  pthread_join(m_threadContext, &pResult);
#endif
// okay we waited out and the thread has terminated by now
  m_threadContext = 0;
// inform the caller that the thread has exited
  m_bStoped = true;
}

bool CNetThread::CanContinue() const
{
  return m_bCanContinue;
}

bool CNetThread::IsStoped() const
{
  return m_bStoped;
}

bool CNetThread::Subscribe(CNotifSubscriber* pSubscriber)
{
  if (m_vecNotifees.find(pSubscriber) != -1)
	return false;
  m_vecNotifees.push_back(pSubscriber);
  return true;
}

bool CNetThread::Unsubscribe(CNotifSubscriber* pSubscriber)
{
  int i;
  if ((i = m_vecNotifees.find(pSubscriber)) != -1)
    m_vecNotifees.pop_at(i);
  return i != -1;
}

void CNetThread::Notify(CNotifEvent* pEvent)
{
  for (int i=0; i<(int)m_vecNotifees.size(); i++) {
    m_vecNotifees[i]->OnEvent(pEvent);
  }
}

#ifdef _WIN32
void CNetThread::Run(void* pPtr)
#else
void CNetThread::Run(void* pPtr)
#endif
{
  CNetThread* pThis = (CNetThread*)pPtr;
#ifdef _WIN32
  if (!pThis) return;
#else
#endif

// run the main thread loop
  while (pThis->CanContinue()) {
	try {
// do some specific processing if not suspended
      pThis->Process();
// let go for a bit ?
	  Sleep(0);
	} catch(CNetException e) {
// stop the thread
	  break;
	}
  }
// considered stoped
//  pThis->m_bStoped = true;

// allow to do some special cleanup for derived classes
  pThis->Cleanup();
}