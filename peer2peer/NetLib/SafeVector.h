// SafeVector.h: interface for the CSafeVector class.
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

#if !defined(AFX_SAFEVECTOR_H__5557C876_2C29_4E52_B1C1_422B168362C8__INCLUDED_)
#define AFX_SAFEVECTOR_H__5557C876_2C29_4E52_B1C1_422B168362C8__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "Mutex.h"

template <class T> class safe_vector  
{
public:
	                         safe_vector() {}
    virtual                 ~safe_vector() {}

			         void    push_back(const T&);
					 void    pop_back();
					 void    pop_at(unsigned int);
                      int    find(const T&);
	                    T    operator[](unsigned int);
	         unsigned int    size();

protected:
	vector<T> m_vecItems;
	CMutex    m_mutex;
};

template <class T> class safe_list  
{
public:

	                         safe_list() {}
    virtual                 ~safe_list() {}

	   list<T>&    front();
			         void    push_front(const T&);
					 void    pop_front();
                        T    retrieve_front();

       list<T>&    back();
			         void    push_back(const T&);
					 void    pop_back();

					 void    wipe();
	         unsigned int    size();

protected:
	list<T>   m_vecItems;
	CMutex    m_mutex;
};

#include "SafeVector.cpp"
#endif // !defined(AFX_SAFEVECTOR_H__5557C876_2C29_4E52_B1C1_422B168362C8__INCLUDED_)
