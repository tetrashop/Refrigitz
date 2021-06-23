// SafeVector.cpp: implementation of the CSafeVector class.
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

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
#include "SafeVector.h"

template <class T> void safe_vector<T>::push_back(const T& x)
{
  m_mutex.Lock();
  m_vecItems.push_back(x);
  m_mutex.Unlock();
}

template <class T> void safe_vector<T>::pop_back()
{
// don't event attempt to pop it if it's empty
  if (!size()) return;
  m_mutex.Lock();
  m_vecItems.pop_back();
  m_mutex.Unlock();
}

template <class T> int safe_vector<T>::find(const T& proto)
{
  m_mutex.Lock();
  for (int i=0; i<(int)m_vecItems.size(); i++)
    if (m_vecItems.at(i) == proto) {
	  m_mutex.Unlock();
	  return i;
	}
	
  m_mutex.Unlock();
  return -1;
}

template <class T> T safe_vector<T>::operator[](unsigned int i)
{
  T item;
  try {
    m_mutex.Lock();
    item = m_vecItems.at(i);
    m_mutex.Unlock();
  } catch (out_of_range x) {
	m_mutex.Unlock();
 	throw x;
  }
  return item;
}

template <class T> unsigned int safe_vector<T>::size()
{
  m_mutex.Lock();
  unsigned int sz = m_vecItems.size();
  m_mutex.Unlock();
  return sz;
}

template <class T> void safe_vector<T>::pop_at(unsigned int i)
{
  if (i < 0 || i > size())
	return;

  m_mutex.Lock();
  vector<T> temp; 
  vector<T>::iterator it = m_vecItems.begin();
  for (; it != m_vecItems.end(); it++) {
	if (*it != m_vecItems.at(i))
      temp.push_back(*it);
  }
  m_vecItems.clear();
  m_vecItems.insert(m_vecItems.end(), temp.begin(), temp.end());
  m_mutex.Unlock();
}


/////////////////////////////////////////////////////////////////

template <class T> list<T>& safe_list<T>::back()
{
  m_mutex.Lock();
  list<T>& r = m_vecItems.back();
  m_mutex.Unlock();
  return r;
}

template <class T> void safe_list<T>::push_back(const T& x)
{
  m_mutex.Lock();
  m_vecItems.push_back(x);
  m_mutex.Unlock();
}

template <class T> void safe_list<T>::pop_back()
{
// don't even attempt to pop it if it's empty
  if (!size()) return;
  m_mutex.Lock();
  m_vecItems.pop_back();
  m_mutex.Unlock();
}

template <class T> list<T>& safe_list<T>::front()
{
  m_mutex.Lock();
  list<T>& r = m_vecItems.front();
  m_mutex.Unlock();
  return r;
}

template <class T> void safe_list<T>::push_front(const T& x)
{
  m_mutex.Lock();
  m_vecItems.push_front(x);
  m_mutex.Unlock();
}

template <class T> void safe_list<T>::pop_front()
{
// don't even attempt to pop it if it's empty
  if (!size()) return;
  m_mutex.Lock();
  m_vecItems.pop_front();
  m_mutex.Unlock();
}

template <class T> T safe_list<T>::retrieve_front()
{
  m_mutex.Lock();
  T item = m_vecItems.front();
  m_vecItems.pop_front();
  m_mutex.Unlock();
  return item;
}

template <class T> void safe_list<T>::wipe()
{
  m_mutex.Lock();
  while (m_vecItems.size() > 0) {
	T item = m_vecItems.front();
	delete item;
	m_vecItems.pop_front();
  }
  m_mutex.Unlock();
}

template <class T> unsigned int safe_list<T>::size()
{
  m_mutex.Lock();
  unsigned int sz = m_vecItems.size();
  m_mutex.Unlock();
  return sz;
}