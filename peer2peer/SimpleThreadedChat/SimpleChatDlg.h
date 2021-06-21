// SimpleChatDlg.h : header file

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

#if !defined(AFX_SIMPLECHATDLG_H__71A9B4C5_915C_4C8F_A586_1107D4EEF432__INCLUDED_)
#define AFX_SIMPLECHATDLG_H__71A9B4C5_915C_4C8F_A586_1107D4EEF432__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetStream.h"
#include "NetMessage.h"
#include "RTFStream.h"
#include "MsgConverter.h"
#include "AcceptorThread.h"
#include "Notification.h"
#include "FileAcceptorThread.h"
#include "ChatButton.h"
#include "TrayIcon.h"
#include "MyRichEdit.h"
#include "MImage.h"

class CNetList;
class CSystemTray;
class CNotifEvent;

extern HHOOK g_hook;
extern CSimpleChatDlg* g_pDlg;

LRESULT CALLBACK CallWndProc(
  int nCode,      // hook code
  WPARAM wParam,  // current-process flag
  LPARAM lParam   // message data
);

/////////////////////////////////////////////////////////////////////////////
// CSimpleChatDlg dialog

class CSimpleChatDlg : public CDialog, public CNotifSubscriber
{
// Construction
public:
				CSimpleChatDlg(CWnd* pParent = NULL);	// standard constructor

    void		DisconnectAll();
	void		DisconnectFileTransfers();

    void        SetRemoteFileAcceptorAddr(const char* szAddr)
					{ m_strRemoteAddrFileAccept = szAddr; }

	void		WriteYourName();
	void	    WritePeerName();
	void	    WriteMsg(const char*);
	void	    WriteSystem(const char*);

// Dialog Data
	//{{AFX_DATA(CSimpleChatDlg)
	enum { IDD = IDD_SIMPLECHAT_DIALOG };
	CProgressCtrl	m_ctrlProgressSent;
	CProgressCtrl	m_ctrlProgressReceived;
	CMyRichEdit   	m_reChat;
	CIPAddressCtrl	m_ctrlAddress;
	CString			m_strText;
	CChatButton     m_ctrlText;
	CNetStream		m_NetStream;
	CString			m_strPort;
	CRTFStream		m_rtfStream;
	UINT			m_nCheckForData;
	CString			m_strWelMsg;
	CString			m_strByeMsg;
	CString			m_strPeer;
	CString			m_strYou;
	BOOL			m_bUseMsg;
	CString			m_strStatus;
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CSimpleChatDlg)
	public:
	virtual BOOL OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo);
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);	// DDX/DDV support
	//}}AFX_VIRTUAL
protected:
	void		Connect(string&);

	void		Listen(string&);
    void        ListenForFiles();
	
	bool		SendText();
	bool		SendLogin(netStatus);
	bool		SendLogout();
    bool        SendLocalFileAcceptAddr();
	bool		SendFile();
	bool		SendLocalUserActivityStatus(CNetStatus&);

	void		Receive();
	
	void				ScrollToEnd();
	ushort				GetVisibleHeight();
	void				EnableControls();
	void				DisableControls();

	virtual void		HandleNotification(CNotifEvent*);
//	static bool			DataRequest(void*, CNetList*);

protected:
	CMsgChatConverter	 m_msgConverter;
	CAcceptorThread		 m_acceptorThread;
	CFileAcceptorThread	 m_acceptorFileThread;
	string				 m_strRemoteAddrFileAccept;
	ulong				 m_nThreadId;

	CSystemTray*		 m_pTrayIcon;
	CMImage				 m_imgSmile;
	CMImage				 m_imgBack;
	UINT				 m_nmsgTray;
	bool				 m_bMinimized;
//	CFile				 m_file;
//	DWORD				 m_nFLen;
	CTime				 m_time;
	
// Implementation
protected:
	HICON m_hIcon;

	// Generated message map functions
	//{{AFX_MSG(CSimpleChatDlg)
	virtual BOOL OnInitDialog();
	afx_msg void OnPaint();
	afx_msg HCURSOR OnQueryDragIcon();
	afx_msg void OnGo();
	afx_msg void OnDisconnect();
	afx_msg void OnDestroy();
	afx_msg void OnSend();
	afx_msg void OnClear();
	afx_msg void OnTimer(UINT nIDEvent);
	afx_msg void OnUseThis();
	afx_msg LRESULT OnAccept(WPARAM, LPARAM);
	afx_msg LRESULT OnAcceptForFile(WPARAM, LPARAM);
	afx_msg LRESULT OnListening(WPARAM, LPARAM);
	afx_msg LRESULT OnAcceptError(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFile(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileSaveAs(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileHeaderReceived(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileProgressSend(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileProgressReceive(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileDoneSend(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileDoneReceive(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileReject(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileReceiveReject(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileErrorSend(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileErrorReceive(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileAbortSend(WPARAM wParam, LPARAM);
	afx_msg LRESULT OnFileAbortReceive(WPARAM wParam, LPARAM);
	afx_msg void OnKillfocusNickName();
	afx_msg void OnClearChat();
	afx_msg void OnSendFile();
	afx_msg void OnSendCancel();
	afx_msg void OnReceiveCancel();
	afx_msg void OnConnect();
	afx_msg void OnListen();
	afx_msg void OnReceiveAccept();
	afx_msg void OnReceiveReject();
	afx_msg void OnShowBk();
	afx_msg BOOL OnEraseBkgnd(CDC* pDC);
	//}}AFX_MSG
	void OnSysCommand(UINT nID, LPARAM lParam);
	afx_msg void OnRestore();
	afx_msg void OnExit();
	DECLARE_MESSAGE_MAP()
//	afx_msg LRESULT	OnTestMessage(WPARAM wParam, LPARAM);
protected:
	CNetAddress* GetRemoteFileAcceptorAddr();
	void				SetupTrayIcon(bool);
};

//{{AFX_INSERT_LOCATION}}
// Microsoft Visual C++ will insert additional declarations immediately before the previous line.

#endif // !defined(AFX_SIMPLECHATDLG_H__71A9B4C5_915C_4C8F_A586_1107D4EEF432__INCLUDED_)
