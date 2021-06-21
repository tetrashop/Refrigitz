// TrayIcon.cpp: implementation of the CTrayIcon class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "SimpleChat.h"
#include "TrayIcon.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CTrayIcon::CTrayIcon():
m_nMsg(0),
m_pWnd(0),
m_nMenu(0)
{

}

CTrayIcon::~CTrayIcon()
{

}

void CTrayIcon::SetData(UINT mMsg, CWnd* pOwner, UINT nIcon, UINT nMenu, const char* strTip)
{
  m_nMsg	= mMsg;
  m_pWnd	= pOwner;
  m_nIcon   = nIcon;
  m_nMenu   = nMenu;
  m_strTip  = strTip;
}

bool CTrayIcon::Show()
{
  if (!m_pWnd || !m_pWnd->GetSafeHwnd() || !m_nIcon) return false;

  HICON hIcon = (HICON)LoadImage(AfxGetApp()->m_hInstance, MAKEINTRESOURCE(m_nIcon), IMAGE_ICON, 16, 16, 0);
  if (!hIcon) return false;

  NOTIFYICONDATA tnd;

  tnd.cbSize  = sizeof(NOTIFYICONDATA);
  tnd.hWnd	  = m_pWnd->GetSafeHwnd();
  tnd.uID	  = m_nMenu;

  tnd.uFlags			= NIF_MESSAGE|NIF_ICON|NIF_TIP;
  tnd.uCallbackMessage	= m_nMsg;
  tnd.hIcon				= hIcon;
  if (!m_strTip.IsEmpty())
	lstrcpyn(tnd.szTip, m_strTip, sizeof(tnd.szTip));
  else
	tnd.szTip[0] = '\0';

  BOOL b = Shell_NotifyIcon(NIM_ADD, &tnd);

  DestroyIcon(hIcon);
  return (b)?true:false;
}

bool CTrayIcon::Hide()
{
  if (!m_pWnd || !m_pWnd->GetSafeHwnd() || !m_nIcon) return false;
  
  NOTIFYICONDATA tnid;      
  tnid.cbSize = sizeof(NOTIFYICONDATA); 
  tnid.hWnd = m_pWnd->GetSafeHwnd();     
  tnid.uID = m_nMenu;          
  BOOL b = Shell_NotifyIcon(NIM_DELETE, &tnid);     
  return (b)?true:false;
}