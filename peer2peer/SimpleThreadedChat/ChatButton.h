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

#if !defined(AFX_CHATBUTTON_H__C2DB0EA7_015C_43D1_BEBD_4FCE781D5B79__INCLUDED_)
#define AFX_CHATBUTTON_H__C2DB0EA7_015C_43D1_BEBD_4FCE781D5B79__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000
// ChatButton.h : header file
//

#include "NetMessage.h"

/////////////////////////////////////////////////////////////////////////////
// CChatButton window

class CChatButton : public CButton
{
// Construction
public:
	CChatButton();

// Attributes
public:
    CNetStatus m_status;
// Operations
public:

// Overrides
	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CChatButton)
	//}}AFX_VIRTUAL

// Implementation
public:
	virtual ~CChatButton();

	// Generated message map functions
protected:
	//{{AFX_MSG(CChatButton)
	afx_msg void OnChar(UINT nChar, UINT nRepCnt, UINT nFlags);
	afx_msg void OnKillFocus(CWnd* pNewWnd);
	afx_msg void OnSetFocus(CWnd* pOldWnd);
	afx_msg void OnTimer(UINT nIDEvent);
	afx_msg int OnCreate(LPCREATESTRUCT lpCreateStruct);
	afx_msg void OnDestroy();
	//}}AFX_MSG

	DECLARE_MESSAGE_MAP()
};

/////////////////////////////////////////////////////////////////////////////

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_CHATBUTTON_H__C2DB0EA7_015C_43D1_BEBD_4FCE781D5B79__INCLUDED_)
