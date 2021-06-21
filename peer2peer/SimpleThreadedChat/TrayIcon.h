// TrayIcon.h: interface for the CTrayIcon class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_TRAYICON_H__FBF5DE7D_5677_4C9D_B870_06FF2996AF57__INCLUDED_)
#define AFX_TRAYICON_H__FBF5DE7D_5677_4C9D_B870_06FF2996AF57__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CTrayIcon  
{
public:
						CTrayIcon();
	virtual				~CTrayIcon();

				void	SetData(UINT, CWnd* pOwner, UINT nIcon, UINT Menu, const char* strTip);
				UINT	GetMenuId() {return m_nMenu;}
				bool	Show();
				bool	Hide();

protected:
	UINT		m_nMsg;
	CWnd*		m_pWnd;
	UINT		m_nMenu;
	UINT		m_nIcon;
	CString		m_strTip;
};

#endif // !defined(AFX_TRAYICON_H__FBF5DE7D_5677_4C9D_B870_06FF2996AF57__INCLUDED_)

