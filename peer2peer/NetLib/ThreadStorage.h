// ThreadStorage.h: interface for the CThreadStorage class.
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

#if !defined(AFX_THREADSTORAGE_H__CB0FDBE4_3223_48F8_A59B_716CCC364FEC__INCLUDED_)
#define AFX_THREADSTORAGE_H__CB0FDBE4_3223_48F8_A59B_716CCC364FEC__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetThread.h"
#include "SafeVector.h"

class CThreadStorage  
{
public:
                                  CThreadStorage();
	virtual                      ~CThreadStorage();

	                   void       Add(CNetThread*);
					   void       Remove(CNetThread*);
			     CNetThread*      GetAt(unsigned int);
			     CNetThread*      GetByThreadId(unsigned long);
				      ulong       Size() {return m_vecThreads.size();}
protected:
	safe_vector<CNetThread*> m_vecThreads;
};

template <class T>
class CQueue  
{
public:
	                              CQueue() {}
    virtual                      ~CQueue();

	                   void       Add(T);
					      T       Retrieve();
					   bool       IsThereData() {return (m_vecItems.size() > 0) ? true : false;}
		       unsigned int       Size() {return m_vecItems.size();} 

					   void       EmptyQueue() { m_vecItems.wipe(); }
protected:
	safe_list<T> m_vecItems;
};

/////////////////////////////////////////////////////////////////////////


template <class T> CQueue<T>::~CQueue()
{
  EmptyQueue();
}

template <class T> void CQueue<T>::Add(T item)
{
  m_vecItems.push_back(item);
}

template <class T> T CQueue<T>::Retrieve()
{
  if (!m_vecItems.size())
	throw CNetException(ERR_NET_CANNOT_RETRIEVE_FROM_EMPTY_QUEUE, "template <class T> T CQueue<T>::Retrieve()");
  return m_vecItems.retrieve_front();
}

#endif // !defined(AFX_THREADSTORAGE_H__CB0FDBE4_3223_48F8_A59B_716CCC364FEC__INCLUDED_)
