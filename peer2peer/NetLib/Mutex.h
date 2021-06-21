// Mutex.h: interface for the CMutex class.
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

#if !defined(AFX_MUTEX_H__24ED6AB8_469A_433C_9377_4275F98CA347__INCLUDED_)
#define AFX_MUTEX_H__24ED6AB8_469A_433C_9377_4275F98CA347__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CMutex  
{
public:
					CMutex();
	virtual			~CMutex();
			bool	Lock(unsigned long nTimeout = INFINITE);
			bool	Unlock();
			int		GetLastError() const;
protected:
			void	HandleError();
protected:
	HANDLE			m_hMutex;
	int				m_nLastError;
};

#endif // !defined(AFX_MUTEX_H__24ED6AB8_469A_433C_9377_4275F98CA347__INCLUDED_)
