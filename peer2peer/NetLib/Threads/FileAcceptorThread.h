// FileAcceptorThread.h: interface for the CFileAcceptorThread class.
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

#if !defined(AFX_FILEACCEPTORTHREAD_H__867E047B_023B_42E0_BEA9_3CA449D91DAC__INCLUDED_)
#define AFX_FILEACCEPTORTHREAD_H__867E047B_023B_42E0_BEA9_3CA449D91DAC__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "AcceptorThread.h"
#include "NetPortScanner.h"

class CFileAcceptorThread : public CAcceptorThread  
{
public:
	                   CFileAcceptorThread();
	virtual           ~CFileAcceptorThread();

	            bool   SetAddress(ulong, ulong);
protected:
	virtual	  void	   Process();
protected:
  CNetPortScanner      m_portScanner;
};

#endif // !defined(AFX_FILEACCEPTORTHREAD_H__867E047B_023B_42E0_BEA9_3CA449D91DAC__INCLUDED_)
