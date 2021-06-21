// ChatButton.cpp : implementation file

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
#include "ChatButton.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CChatButton

CChatButton::CChatButton()
{
  m_status.SetCode(nsUserDoesNtng);
  m_status.SetText("The remote peer is away");
}

CChatButton::~CChatButton()
{
}


BEGIN_MESSAGE_MAP(CChatButton, CButton)
	//{{AFX_MSG_MAP(CChatButton)
	ON_WM_CHAR()
	ON_WM_KILLFOCUS()
	ON_WM_SETFOCUS()
	ON_WM_TIMER()
	ON_WM_CREATE()
	ON_WM_DESTROY()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CChatButton message handlers

void CChatButton::OnChar(UINT nChar, UINT nRepCnt, UINT nFlags) 
{
  CButton::OnChar(nChar, nRepCnt, nFlags);
  m_status.SetCode(nsUserTypesMsg);
  m_status.SetText("The remote peer is typing a message");
}

void CChatButton::OnKillFocus(CWnd* pNewWnd) 
{
  CButton::OnKillFocus(pNewWnd);
}

void CChatButton::OnSetFocus(CWnd* pOldWnd) 
{
  CButton::OnSetFocus(pOldWnd);	
  m_status.SetCode(nsUserTypesMsg);
  m_status.SetText("The remote peer is typing a message");
}

void CChatButton::OnTimer(UINT nIDEvent) 
{
  CButton::OnTimer(nIDEvent);

  if (nIDEvent != 1)
	return;

  m_status.SetCode(nsUserDoesNtng);
  m_status.SetText("The remote peer is away");
}

int CChatButton::OnCreate(LPCREATESTRUCT lpCreateStruct) 
{
  if (CButton::OnCreate(lpCreateStruct) == -1)
 	return -1;

  SetTimer(1, 3000, 0);
		
  return 0;
}

void CChatButton::OnDestroy() 
{
  CButton::OnDestroy();

  KillTimer(1);	
}

