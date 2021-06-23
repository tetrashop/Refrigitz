// SimpleChatDlg.cpp : implementation file

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
#include "SimpleChat.h"
#include "SimpleChatDlg.h"

#include "NetIPAddress.h"
#include "NetFileAddress.h"
#include "ChatMessages.h"
#include "NetSockConnector.h"
#include "NetFileConnector.h"
#include "NetFileInfo.h"
#include "NetFileHdr.h"
#include "NetNotif.h"
#include "SenderThread.h"
#include "ReceiverThread.h"
#include "ThreadStorage.h"
#include "NetSockAcceptor.h"
#include "MDC.h"
#include "SystemTray.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CAboutDlg dialog used for App About

class CAboutDlg : public CDialog
{
public:
	CAboutDlg();

// Dialog Data
	//{{AFX_DATA(CAboutDlg)
	enum { IDD = IDD_ABOUTBOX };
	//}}AFX_DATA

	// ClassWizard generated virtual function overrides
	//{{AFX_VIRTUAL(CAboutDlg)
	protected:
	virtual void DoDataExchange(CDataExchange* pDX);    // DDX/DDV support
	//}}AFX_VIRTUAL

// Implementation
protected:
	//{{AFX_MSG(CAboutDlg)
	//}}AFX_MSG
	DECLARE_MESSAGE_MAP()
};

CAboutDlg::CAboutDlg() : CDialog(CAboutDlg::IDD)
{
	//{{AFX_DATA_INIT(CAboutDlg)
	//}}AFX_DATA_INIT
}

void CAboutDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CAboutDlg)
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CAboutDlg, CDialog)
	//{{AFX_MSG_MAP(CAboutDlg)
		// No message handlers
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

const char* lpszTip = "SimpleChat is waiting for a peer to connect...";
HHOOK g_hook = 0;
CSimpleChatDlg* g_pDlg = 0;

/////////////////////////////////////////////////////////////////////////////
// CSimpleChatDlg dialog

CSimpleChatDlg::CSimpleChatDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CSimpleChatDlg::IDD, pParent),
	  m_pTrayIcon(0),
	  m_bMinimized(false),
	  m_nmsgTray(RegisterWindowMessage(lpszTip)),
	  m_nCheckForData(0),
	  m_nThreadId(0)
{
	//{{AFX_DATA_INIT(CSimpleChatDlg)
	m_strText = _T("");
	m_strPort = _T("");
	m_strWelMsg = _T("");
	m_strByeMsg = _T("");
	m_strYou = _T("");
	m_bUseMsg = FALSE;
	m_strStatus = _T("");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_bHandleNotify = true;
}

void CSimpleChatDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CSimpleChatDlg)
	DDX_Control(pDX, IDC_PROGRESS_SENT, m_ctrlProgressSent);
	DDX_Control(pDX, IDC_PROGRESS_RECEIVED, m_ctrlProgressReceived);
	DDX_Control(pDX, IDRE_MESSAGES, m_reChat);
	DDX_Control(pDX, IDC_IPADDRESS, m_ctrlAddress);
	DDX_Text(pDX, IDED_TEXT, m_strText);
	DDV_MaxChars(pDX, m_strText, 255);
	DDX_Text(pDX, IDED_PORT, m_strPort);
	DDV_MaxChars(pDX, m_strPort, 4);
	DDX_CBString(pDX, IDCB_WELCOME_MESSAGE, m_strWelMsg);
	DDV_MaxChars(pDX, m_strWelMsg, 100);
	DDX_CBString(pDX, IDCB_GOODBYE_MESSAGE, m_strByeMsg);
	DDV_MaxChars(pDX, m_strByeMsg, 100);
	DDX_CBString(pDX, IDCB_NICK_NAME, m_strYou);
	DDV_MaxChars(pDX, m_strYou, 100);
	DDX_Check(pDX, IDCH_USE_THIS, m_bUseMsg);
	DDX_Text(pDX, IDST_CONNECT_STATUS, m_strStatus);
	DDV_MaxChars(pDX, m_strStatus, 100);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CSimpleChatDlg, CDialog)
	//{{AFX_MSG_MAP(CSimpleChatDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_BN_CLICKED(IDBT_GO, OnGo)
	ON_BN_CLICKED(IDBT_DISCONNECT, OnDisconnect)
	ON_WM_DESTROY()
	ON_BN_CLICKED(IDBT_SEND, OnSend)
	ON_BN_CLICKED(IDBT_CLEAR, OnClear)
	ON_WM_TIMER()
	ON_BN_CLICKED(IDCH_USE_THIS, OnUseThis)
	ON_MESSAGE(UWM_ON_ACCEPT, OnAccept)
	ON_MESSAGE(UWM_ON_ACCEPT_FOR_FILE, OnAcceptForFile)
	ON_MESSAGE(UWM_ON_LISTENING, OnListening)
	ON_MESSAGE(UWM_ON_ACCEPT_ERROR, OnAcceptError)
	ON_MESSAGE(UWM_ON_FILE, OnFile)
	ON_MESSAGE(UWM_ON_FILE_SAVE_AS, OnFileSaveAs)
	ON_MESSAGE(UWM_ON_FILE_RECEIVED_HEADER_FILE, OnFileHeaderReceived)
	ON_MESSAGE(UWM_ON_FILE_PROGRESS_SEND, OnFileProgressSend)
	ON_MESSAGE(UWM_ON_FILE_PROGRESS_RECEIVE, OnFileProgressReceive)
	ON_MESSAGE(UWM_ON_FILE_DONE_SEND, OnFileDoneSend)
	ON_MESSAGE(UWM_ON_FILE_DONE_RECEIVE, OnFileDoneReceive)
	ON_MESSAGE(UWM_ON_FILE_REJECT, OnFileReject)
	ON_MESSAGE(UWM_ON_FILE_RECEIVE_REJECT, OnFileReceiveReject)
	ON_MESSAGE(UWM_ON_FILE_ERROR_SEND, OnFileErrorSend)
	ON_MESSAGE(UWM_ON_FILE_ERROR_RECEIVE, OnFileErrorReceive)
	ON_MESSAGE(UWM_ON_FILE_ABORT_SEND, OnFileAbortSend)
	ON_MESSAGE(UWM_ON_FILE_ABORT_RECEIVE, OnFileAbortReceive)
	ON_CBN_KILLFOCUS(IDCB_NICK_NAME, OnKillfocusNickName)
	ON_BN_CLICKED(IDBT_CLEAR_CHAT, OnClearChat)
	ON_BN_CLICKED(IDBT_SEND_FILE, OnSendFile)
	ON_BN_CLICKED(IDBT_SEND_CANCEL, OnSendCancel)
	ON_BN_CLICKED(IDBT_RECEIVE_CANCEL, OnReceiveCancel)
	ON_BN_CLICKED(IDRB_CONNECT, OnConnect)
	ON_BN_CLICKED(IDRB_LISTEN, OnListen)
	ON_BN_CLICKED(IDBT_RECEIVE_ACCEPT, OnReceiveAccept)
	ON_BN_CLICKED(IDBT_RECEIVE_REJECT, OnReceiveReject)
	ON_BN_CLICKED(IDCH_SHOW_BK, OnShowBk)
	ON_WM_ERASEBKGND()
	//}}AFX_MSG_MAP
	ON_COMMAND(IDM_RESTORE, OnRestore)
	ON_COMMAND(IDM_EXIT, OnExit)
END_MESSAGE_MAP()
/*	ON_MESSAGE(UWM_TEST_MESSAGE, OnTestMessage)

LRESULT	CSimpleChatDlg::OnTestMessage(WPARAM wParam, LPARAM)
{
  return 0;
}

*/
void CSimpleChatDlg::OnGo() 
{
  CButton* pbtnConnect = (CButton*)GetDlgItem(IDRB_CONNECT);
  CButton* pbtnListen  = (CButton*)GetDlgItem(IDRB_LISTEN);

  UpdateData();

// get the ip adress from the UI control for the local host machine
  BYTE d0, d1, d2, d3;
  m_ctrlAddress.GetAddress(d0, d1, d2, d3);
  char buff[20];
  sprintf_s(buff, "%d.%d.%d.%d", d0, d1, d2, d3);
  if (atoi(m_strPort) <= 0) return;
  
  string addr = "addr='";
  addr += buff;
  addr += "';port='";
  addr += m_strPort;
  addr += "';";

// this call is blocking, so if return from Connect() I must start the timer  
  if (pbtnConnect->GetCheck() == 1)
	Connect(addr);

// this call is threaded, so the timer starts when there is a connection
  if (pbtnListen->GetCheck() == 1) 
	Listen(addr);
}

void CSimpleChatDlg::Connect(string& addr)
{
// cleanup
  DisconnectAll();

  try {
    CNetIPAddress address;
	address.SetConnectString(addr.c_str());
    CNetSockConnector* pConnector = new CNetSockConnector;
// attach it to the connector
    m_NetStream.Attach(pConnector);
// connect to the address of the remote host
    m_NetStream.GetConnection()->Connect(address);
  } catch (CNetException e) {
	delete m_NetStream.Detach();
	MessageBox("The peer appears to be offline. Please try again later.", "Simple Chat", MB_ICONINFORMATION|MB_OK);
	DisconnectAll();
	return;
  }

// send first message - login
  if (!SendLogin(nsLogin)) return;
// set up the acceptor thread for incomming connections for file transfer
  ListenForFiles();

  DisableControls();
}


void CSimpleChatDlg::ListenForFiles()
{
// prepare the thread for accepting, 
// let it find an available port in the range from 4000 to 5000

  if (!m_acceptorFileThread.SetAddress(4000, 5000)) {
   	MessageBox("Failed to listen on the given address and port. Please correct your parameters.", "Simple Chat", MB_ICONINFORMATION|MB_OK);
	return;
  }
  m_acceptorFileThread.Subscribe(this);
// start accepting for incoming files transfers
// (none blocking, so the thread can successfuly terminate 
// if application exists while thread is accepting) 
  m_acceptorFileThread.Start();

// now let the remote peer now that I have the file acceptor on such port
  SendLocalFileAcceptAddr();
}

LRESULT CSimpleChatDlg::OnAcceptForFile(WPARAM wParam, LPARAM)
{
  CNotifAcceptForFile* pEvent = (CNotifAcceptForFile*)wParam;
  CNetConnection* pConnection = pEvent->getConnection();

// a file is going to be received through this connection
// so create a receiver thread
  CReceiverThread* pThread = new CReceiverThread;
// pass the provider connection 
// Note: pConnection will be deleted by the receiver thread
  pThread->SetProviderConnector(pConnection);
  pThread->Subscribe(this);
// add the thread into the thread storage
  g_getThreadStorage().Add(pThread);
// start receiving!
  pThread->Start();

// save the thread's id these buttons control
  CWnd* pReceive = GetDlgItem(IDST_RECEIVE);
  pReceive->SetWindowContextHelpId(pThread->GetThreadId());

  delete pEvent;

  return 0;
}

LRESULT CSimpleChatDlg::OnFile(WPARAM wParam, LPARAM)
{
  delete (CNotifEvent*)wParam;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileHeaderReceived(WPARAM wParam, LPARAM)
{
  const CNotifSaveAsFile* pEvent = (const CNotifSaveAsFile*)wParam;
  const CNetFileTransmitInfo info = pEvent->getInfo();

// save the thread id
  m_nThreadId = pEvent->getThreadId();

  CString strMsg;
  if (!m_strPeer.IsEmpty()) {
    strMsg = "The peer (" + m_strPeer + ") sends you file '";
	strMsg += info.GetData().m_strProviderName.c_str();
	strMsg += "'";
  }
  else {
	strMsg = "The peer sends you file '";
	strMsg += info.GetData().m_strProviderName.c_str();
	strMsg += "'";
  }
// notify user that there is a file awaits to be accepted
  WriteSystem("");
  WriteSystem(strMsg);
  WriteSystem("Press <Accept...> button to download this file, or press <Reject> button to cancel.");
  WriteSystem("");

  GetDlgItem(IDBT_RECEIVE_ACCEPT)->EnableWindow(TRUE);
  GetDlgItem(IDBT_RECEIVE_REJECT)->EnableWindow(TRUE);

  delete pEvent;
  return 0;
}

void CSimpleChatDlg::OnReceiveAccept() 
{
// get the thread
  CNetThread* p = g_getThreadStorage().GetByThreadId(m_nThreadId);
  CReceiverThread* pThread = dynamic_cast<CReceiverThread*>(p);
  if (!pThread) return;

// reset it
  m_nThreadId = 0;

  GetDlgItem(IDBT_RECEIVE_ACCEPT)->EnableWindow(FALSE);
  GetDlgItem(IDBT_RECEIVE_REJECT)->EnableWindow(FALSE);

// set file's status as accepted
  pThread->SetFileStatus(nsAccept);
// next notification will be OnFileSaveAs

}

void CSimpleChatDlg::OnReceiveReject() 
{
// get the thread
  CNetThread* p = g_getThreadStorage().GetByThreadId(m_nThreadId);
  CReceiverThread* pThread = dynamic_cast<CReceiverThread*>(p);
  if (!pThread) return;

// reset it
  m_nThreadId = 0;

  GetDlgItem(IDBT_RECEIVE_ACCEPT)->EnableWindow(FALSE);
  GetDlgItem(IDBT_RECEIVE_REJECT)->EnableWindow(FALSE);

// set file's status as accepted
  pThread->SetFileStatus(nsReject);
// next notification will be OnFileReceiveReject	
}

LRESULT CSimpleChatDlg::OnFileSaveAs(WPARAM wParam, LPARAM)
{
  const CNotifSaveAsFile* pEvent = (const CNotifSaveAsFile*)wParam;
  const CNetFileTransmitInfo info = pEvent->getInfo();

  // get the thread
  CNetThread* p = g_getThreadStorage().GetByThreadId(pEvent->getThreadId());
  CReceiverThread* pThread = dynamic_cast<CReceiverThread*>(p);
  if (!pThread) {
	delete pEvent;
	return 0;
  }

// prompt user to choose the name of the file and the destination directory
  CFileDialog dlg(FALSE, NULL, info.GetData().m_strProviderName.c_str(), OFN_HIDEREADONLY|OFN_OVERWRITEPROMPT|OFN_PATHMUSTEXIST, "All Files (*.*)|*.*||", this);

// if user canceled out, then set the status of the file as 'reject'
  if (dlg.DoModal() == IDCANCEL) {
// reject the file
	pThread->SetFileStatus(nsReject);
	delete pEvent;
	return 0;
  }
// ok it seems that the file is wanted
// enable cancel button
  GetDlgItem(IDBT_RECEIVE_CANCEL)->EnableWindow(TRUE);
  GetDlgItem(IDST_PERCENTAGE_RECEIVED)->ShowWindow(SW_SHOW);
  GetDlgItem(IDST_PERCENTAGE_RECEIVED)->UpdateWindow();


// form the connect string  
  CString strPath = dlg.GetPathName();
// form the connect string for a brand new file
  CString strConnect = "file = '";
  strConnect += strPath;
  strConnect += "'; openopt = 'trunc'";

// create the provider address. this will be deleted by the thread
  CNetFileAddress* pConAddr = new CNetFileAddress;
  pConAddr->SetConnectString(strConnect);
// now set the address to the thread
  pThread->SetConsumerAddress(pConAddr);
  pThread->SetFileStatus(nsPathSet);

  delete pEvent;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileProgressReceive(WPARAM wParam, LPARAM)
{
  const CNotifProgressFile_Receive* pEvent = (const CNotifProgressFile_Receive*)wParam;
  const CNetFileTransmitInfo& info = pEvent->getInfo();
  char szBuff[200];
  sprintf_s(szBuff, "%d%% completed", info.GetCompletePercentage());
  SetDlgItemText(IDST_PERCENTAGE_RECEIVED, szBuff);
  m_ctrlProgressReceived.SetPos(info.GetCompletePercentage());
  sprintf_s(szBuff, "%.2d of %.2d Kbytes", info.GetData().m_nCurrentSz / 1024, info.GetData().m_nTotalSz / 1024);
  SetDlgItemText(IDST_RECEIVED, szBuff);
  delete pEvent;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileProgressSend(WPARAM wParam, LPARAM)
{
  const CNotifProgressFile_Send* pEvent = (const CNotifProgressFile_Send*)wParam;
  const CNetFileTransmitInfo& info = pEvent->getInfo();
  char szBuff[100];
  sprintf_s(szBuff, "%d%% completed", info.GetCompletePercentage());
  SetDlgItemText(IDST_PERCENTAGE_SENT, szBuff);
  m_ctrlProgressSent.SetPos(info.GetCompletePercentage());
  sprintf_s(szBuff, "%.2d of %.2d Kbytes", info.GetData().m_nCurrentSz / 1024, info.GetData().m_nTotalSz / 1024);
  SetDlgItemText(IDST_SENT, szBuff);
  delete pEvent;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileDoneReceive(WPARAM wParam, LPARAM)
{
  const CNotifDoneFile_Receive* pEvent = (const CNotifDoneFile_Receive*)wParam;
  const CNetFileTransmitInfo info = pEvent->getInfo();
  m_ctrlProgressReceived.SetPos(0);
  SetDlgItemText(IDST_PERCENTAGE_RECEIVED, "");
  SetDlgItemText(IDST_RECEIVED, "Done.");
// disable cancel button
  GetDlgItem(IDBT_RECEIVE_CANCEL)->EnableWindow(FALSE);
  GetDlgItem(IDST_PERCENTAGE_RECEIVED)->ShowWindow(SW_HIDE);
  delete pEvent;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileDoneSend(WPARAM wParam, LPARAM)
{
  const CNotifDoneFile_Send* pEvent = (const CNotifDoneFile_Send*)wParam;
  const CNetFileTransmitInfo info = pEvent->getInfo();
  m_ctrlProgressSent.SetPos(0);
  SetDlgItemText(IDST_PERCENTAGE_SENT, "");
  SetDlgItemText(IDST_SENT, "Done.");
// enable the send button
  GetDlgItem(IDBT_SEND_FILE)->EnableWindow(TRUE);
// disable cancel button
  GetDlgItem(IDBT_SEND_CANCEL)->EnableWindow(FALSE);
  GetDlgItem(IDST_PERCENTAGE_SENT)->ShowWindow(SW_HIDE);
  delete pEvent;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileReceiveReject(WPARAM wParam, LPARAM)
{
  SetDlgItemText(IDST_RECEIVED, "You have rejected the download.");
// disable cancel button
  GetDlgItem(IDBT_RECEIVE_CANCEL)->EnableWindow(FALSE);

  GetDlgItem(IDBT_RECEIVE_ACCEPT)->EnableWindow(FALSE);
  GetDlgItem(IDBT_RECEIVE_REJECT)->EnableWindow(FALSE);
  delete (CNotifEvent*)wParam;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileReject(WPARAM wParam, LPARAM)
{
  SetDlgItemText(IDST_SENT, "File is rejected by remote peer.");
// enable the send button
  GetDlgItem(IDBT_SEND_FILE)->EnableWindow(TRUE);
// disable cancel button
  GetDlgItem(IDBT_SEND_CANCEL)->EnableWindow(FALSE);
  delete (CNotifEvent*)wParam;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileErrorReceive(WPARAM wParam, LPARAM)
{
  const CNotifErrorFile_Receive* pEvent = (const CNotifErrorFile_Receive*)wParam;
  const CNetFileTransmitInfo info = pEvent->getInfo();
  m_ctrlProgressReceived.SetPos(0);
  SetDlgItemText(IDST_PERCENTAGE_RECEIVED, "");
  SetDlgItemText(IDST_RECEIVED, "Failed.");
// disable cancel button
  GetDlgItem(IDBT_RECEIVE_CANCEL)->EnableWindow(FALSE);
  GetDlgItem(IDST_PERCENTAGE_RECEIVED)->ShowWindow(SW_HIDE);
  delete pEvent;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileErrorSend(WPARAM wParam, LPARAM)
{
  const CNotifErrorFile_Send* pEvent = (const CNotifErrorFile_Send*)wParam;
  const CNetFileTransmitInfo info = pEvent->getInfo();
  m_ctrlProgressSent.SetPos(0);
  SetDlgItemText(IDST_PERCENTAGE_SENT, "");
  SetDlgItemText(IDST_SENT, "Failed.");
// enable the send button
  GetDlgItem(IDBT_SEND_FILE)->EnableWindow(TRUE);
// disable cancel button
  GetDlgItem(IDBT_SEND_CANCEL)->EnableWindow(FALSE);
  GetDlgItem(IDST_PERCENTAGE_SENT)->ShowWindow(SW_HIDE);
  delete pEvent;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileAbortReceive(WPARAM wParam, LPARAM)
{
  const CNotifAbortFile_Receive* pEvent = (const CNotifAbortFile_Receive*)wParam;
  const CNetFileTransmitInfo info = pEvent->getInfo();
  m_ctrlProgressReceived.SetPos(0);
  SetDlgItemText(IDST_PERCENTAGE_RECEIVED, "");
  SetDlgItemText(IDST_RECEIVED, "Cancelled.");
// disable the cancel button
  GetDlgItem(IDBT_RECEIVE_CANCEL)->EnableWindow(FALSE);
  GetDlgItem(IDST_PERCENTAGE_RECEIVED)->ShowWindow(SW_HIDE);
  delete pEvent;
  return 0;
}

LRESULT CSimpleChatDlg::OnFileAbortSend(WPARAM wParam, LPARAM)
{
  const CNotifAbortFile_Send* pEvent = (const CNotifAbortFile_Send*)wParam;
  const CNetFileTransmitInfo info = pEvent->getInfo();
  m_ctrlProgressSent.SetPos(0);
  SetDlgItemText(IDST_PERCENTAGE_SENT, "");
  SetDlgItemText(IDST_SENT, "Cancelled.");
// enable the send button
  GetDlgItem(IDBT_SEND_FILE)->EnableWindow(TRUE);
// disable cancel button
  GetDlgItem(IDBT_SEND_CANCEL)->EnableWindow(FALSE);
  GetDlgItem(IDST_PERCENTAGE_SENT)->ShowWindow(SW_HIDE);
  delete pEvent;
  return 0;
}
void CSimpleChatDlg::Listen(string& addr)
{
// cleanup
  DisconnectAll();
// prepare the thread for accepting
  if (!m_acceptorThread.SetAddress(addr)) {
   	MessageBox("Failed to listen on the given address and port. Please correct your parameters.", "Simple Chat", MB_ICONINFORMATION|MB_OK);
	return;
  }
// subscribe fro the notification from the thread
  m_acceptorThread.Subscribe(this);
// start accepting (none blocking, so the thread can successfuly terminate 
// if application exists while thread is accepting) 
  m_acceptorThread.Start();

  GetDlgItem(IDBT_GO)->EnableWindow(FALSE);
// enable the disconnect button
  GetDlgItem(IDBT_DISCONNECT)->EnableWindow(TRUE);

// minimize the window and create the tray
  SetupTrayIcon(true);
}

LRESULT CSimpleChatDlg::OnListening(WPARAM wParam, LPARAM)
{
  const CNotifListening* pEvent = (const CNotifListening*)wParam;
  delete pEvent;

  if (m_acceptorThread.IsStoped()) return 0;

  CTimeSpan span =  CTime::GetCurrentTime() - m_time;
  if (span.GetSeconds() < 1) return 0;
  m_time = CTime::GetCurrentTime();

  int nDots = m_strStatus.GetLength() - strlen("Listening ");
  m_strStatus = "Listening ";
  if (nDots > 30) nDots = 0;
  for (int i=0; i<nDots + 1; i++)
	m_strStatus += '>';

  GetDlgItem(IDST_CONNECT_STATUS)->SetWindowText(m_strStatus);
  return 0;
}

LRESULT CSimpleChatDlg::OnAcceptError(WPARAM wParam, LPARAM)
{
  delete (CNotifEvent*)wParam;
// restore the window
  ShowWindow(SW_RESTORE);

  MessageBox("A remote peer has arrived but failed to establish a connection on the given address and port. Please try to change the port number.", "Simple Chat", MB_ICONINFORMATION|MB_OK);
// stop the thread 
  m_acceptorThread.Stop();
// restore the window and remove the tray
  SetupTrayIcon(false);
  return 0;
}

LRESULT CSimpleChatDlg::OnAccept(WPARAM wParam, LPARAM)
{
  delete (CNotifEvent*)wParam;

// by now the acceptor execution thread is no longer running (accepting)
// however the object exists for the lifetime of this dialog

  CNetConnection* pConnector = m_acceptorThread.GetConnection();

  if (!pConnector) {
	WriteSystem("a Peer's attempt to connect has failed");
	return 0;
  }

// got the connection to the peer. Attach in to the stream
  m_NetStream.Attach(pConnector);

// stop the thread 
  m_acceptorThread.Stop();

// reset the progress status
  m_strStatus.Empty();
  GetDlgItem(IDST_CONNECT_STATUS)->SetWindowText(m_strStatus);

  DisableControls();

// restore the window and remove the tray
  SetupTrayIcon(false);

// start up the file listener thread
  ListenForFiles();

  return 0;
}

void CSimpleChatDlg::DisconnectAll() 
{
// if the acceptor thread is running, stop it
  if (!m_acceptorThread.IsStoped()) {
// restore the window and remove the tray
    SetupTrayIcon(false);
// stop thread
	m_acceptorThread.Stop();
// reset the progress status
	m_strStatus.Empty();
	UpdateData(FALSE);
  }
// if the file acceptor thread is running, stop it
  m_acceptorFileThread.Stop();

// stop listening for incoming data
//  KillTimer(m_nCheckForData);

  EnableControls();

// close the text message channel
  CNetConnection* pConnection = m_NetStream.Detach();
  if (!pConnection) return;
// break the pipe
  pConnection->Disconnect();
  delete pConnection;

  DisconnectFileTransfers();

}

void CSimpleChatDlg::DisconnectFileTransfers()
{
// abort any file transfers
  CThreadStorage& storage = g_getThreadStorage();
  while (storage.Size() > 0) {
	CNetThread* pThread = storage.GetAt(storage.Size() - 1);
// block until thread quits, selfremoves from the storage and selfdestructs
	if (pThread)
	  pThread->Stop();
  }
  
// empty the queue from the notifications
  while (IsThereEvent()) {
	CNotifEvent* pEvent = GetEvent();
	SendMessage(pEvent->event_id(), (WPARAM)pEvent);
  }
}

bool CSimpleChatDlg::SendText() 
{
  if (!m_NetStream.IsOpen()) {
	WriteSystem("The connection is broken.");
    return false;
  }

  UpdateData();

  CNetText* pNetText = 0;
  try {
// create new message with local host id, remote host id and the text message  
    CNetText::MSG msg;
    msg.m_strText = m_strText;
    pNetText = new CNetText(m_NetStream.GetConnection()->GetRemoteHandle(), m_NetStream.GetConnection()->GetLocalHandle(), msg) ;
// send the message
	m_NetStream << *pNetText;
	WriteYourName();
// display this message on the local screen
    WriteMsg(pNetText->GetText());
// clean up the edit box
    OnClear();
    GetDlgItem(IDED_TEXT)->SetFocus();
  } catch (CNetException e) {
// if cannot send notify the user and do not clean up the edit box
	WriteSystem("The message cannot be delivered.");
// the connection is broken, close the socket
	DisconnectAll();
	delete pNetText;
	return false;
  }
// clean up
  delete pNetText;
  return true;
}

bool CSimpleChatDlg::SendLocalFileAcceptAddr()
{
  if (!m_NetStream.IsOpen()) {
	WriteSystem("The connection is broken.");
    return false;
  }

  CNetStatus status;
  status.SetCode(nsAcceptAddr);
  status.SetText(m_acceptorFileThread.GetAddress());
  try {
// send the message
	m_NetStream << status;
  } catch (CNetException e) {
// if cannot send notify the user and do not clean up the edit box
	WriteSystem("The message cannot be delivered.");
// the connection is broken, close the socket
	DisconnectAll();
	return false;
  }
  return true;
}

bool CSimpleChatDlg::SendFile()
{
  if (!m_NetStream.IsOpen()) {
	WriteSystem("The connection is broken.");
    return false;
  }

  CFileDialog dlg(TRUE, NULL, NULL, OFN_HIDEREADONLY, "All Files (*.*)|*.*||", this);

  if (dlg.DoModal() == IDCANCEL)
	return true;
  
  CString strPath = dlg.GetPathName();
// form the connect string
  CString strConnect = "file = '";
  strConnect += strPath;
  strConnect += "'; openopt = 'read'";

// create the provider address
  CNetFileAddress addr;
  addr.SetConnectString(strConnect);
// create the provider's connector
  CNetBinFileConnector* pConnector = new CNetBinFileConnector;
// connect!
  pConnector->Connect(addr);

// create the file header
  strPath.Delete(strPath.Find(dlg.GetFileName()), dlg.GetFileName().GetLength());
  CNetFileHdr  hdr;
  hdr.SetDirectory(false);
  hdr.SetProviderPath(strPath);
  hdr.SetProviderName(dlg.GetFileName());
  hdr.SetFileSize(pConnector->GetSize());
// create the sending thread
  CSenderThread* pThread = new CSenderThread(hdr);
// set the provider's connector
  pThread->SetProviderConnector(pConnector);
// set the consumer's address
  pThread->SetConsumerAddress(GetRemoteFileAcceptorAddr());
// subscribe for notifications
  pThread->Subscribe(this);
// add the thread to the thread storage
  g_getThreadStorage().Add(pThread);
// run it!
  pThread->Start();

// save the thread's id these buttons control
  CWnd* pSend = GetDlgItem(IDST_SEND);
  pSend->SetWindowContextHelpId(pThread->GetThreadId());
// disable the button
  GetDlgItem(IDBT_SEND_FILE)->EnableWindow(FALSE);
// enable cancel button
  GetDlgItem(IDBT_SEND_CANCEL)->EnableWindow(TRUE);
  GetDlgItem(IDST_PERCENTAGE_SENT)->ShowWindow(SW_SHOW);
  GetDlgItem(IDST_PERCENTAGE_SENT)->UpdateWindow();
/*
  if (!m_file.Open(strPath, CFile::modeRead)) {
	MessageBox("Cannot open the file", "Error", MB_OK|MB_ICONERROR);
	return false;
  }
// set the file length;
  m_nFLen = m_file.GetLength();

// create the message
  CNetList list;
  list.SetDataRequestCallback(this, DataRequest);
    
// set the max number of packets
  DWORD nLen  = (m_nFLen > MAX_NET_PACKET) ? m_nFLen : MAX_NET_PACKET;
  short nTail = (nLen % MAX_NET_PACKET > 0) ? 1 : 0;
  list.SetMaxElem(nLen / MAX_NET_PACKET + nTail);
// send it!
  m_NetStream << list;
*/
  return true;
}
/*
bool CSimpleChatDlg::DataRequest(void* pCaller, CNetList* pList)
{
  CSimpleChatDlg* pThis = (CSimpleChatDlg*)pCaller;

  DWORD nPos = pThis->m_file.GetPosition();

// end of the file
  if (pThis->m_nFLen == nPos) {
	pThis->m_file.Close();
	return true;
  }

// figure out how many bytes to read
  DWORD nSz = (pThis->m_nFLen - nPos > MAX_NET_PACKET) ? MAX_NET_PACKET : pThis->m_nFLen - nPos;

  char szBuff[MAX_NET_PACKET];
  DWORD nRead = 0;
// if read less, that's a problem
  if ((nRead = pThis->m_file.Read(szBuff, nSz)) != nSz) {
	pThis->m_file.Close();
	return false;
  }

  CNetPacket* pMsg = new CNetPacket;
  pMsg->SetData(szBuff, nSz, pList->GetElemN() + 1);
// send the message
  pList->AddElem(pMsg);

  if (nPos + nRead == pThis->m_nFLen)
    pThis->m_file.Close();
  return true;
}
*/
bool CSimpleChatDlg::SendLogin(netStatus ns)
{
  if (!m_NetStream.IsOpen()) {
	WriteSystem("The connection is broken.");
    return false;
  }

  UpdateData();

// create new message with the local user name and the text welcome message  
  CNetLogin login;
  login.SetCode(ns);
  login.SetName(m_strYou);
  if (m_bUseMsg) 
    login.SetText(m_strWelMsg);
  try {
// send the message
	m_NetStream << login;
  } catch (CNetException e) {
// if cannot send notify the user and do not clean up the edit box
	WriteSystem("The message cannot be delivered.");
// the connection is broken, close the socket
	DisconnectAll();
	return false;
  }
  if (ns == nsLogin)
    WriteSystem("You have joined the chat room.");
  else 
    if (ns == nsNickChange)
  	  WriteSystem("You have changed your nick name.");
// don't show the welcome message if it's empty
  if (m_bUseMsg && !m_strWelMsg.IsEmpty()) {
    WriteYourName();
// display this message on the local screen
    WriteMsg(login.GetText());
  }
// clean up the edit box
  OnClear();
  GetDlgItem(IDED_TEXT)->SetFocus();
  return true;
}

bool CSimpleChatDlg::SendLogout()
{
  if (!m_NetStream.IsOpen()) {
	WriteSystem("The connection is broken.");
    return false;
  }

  UpdateData();

// create the logout message
  CNetStatus logout;
  logout.SetCode(nsLogout);
  if (m_bUseMsg) 
    logout.SetText(m_strByeMsg);

  try {
// send the message
	m_NetStream << logout;
  } catch (CNetException e) {
// if cannot send notify the user and do not clean up the edit box
	WriteSystem("The message cannot be delivered.");
// the connection is broken, close the socket
	DisconnectAll();
	return false;
  }
  WriteSystem("You have left the chat room.");
  if (m_bUseMsg && !m_strByeMsg.IsEmpty()) {
    WriteYourName();
// display this message on the local screen
    WriteMsg(logout.GetText());
  }
// clean up the edit box
  OnClear();
  GetDlgItem(IDED_TEXT)->SetFocus();
  return true;
}

bool CSimpleChatDlg::SendLocalUserActivityStatus(CNetStatus& status)
{
  if (!m_NetStream.IsOpen())
    return false;

// send the local user activity status to the remote peer
  try {
    m_NetStream << status;
  } catch (CNetException e) {
// the connection is broken, close the socket(s)
	DisconnectAll();
	return false;
  }
  return true;
}

void CSimpleChatDlg::Receive() 
{
  CNetMsg*			pMsg = 0;
// read it
  try {
    m_NetStream >> pMsg;
// deal with the message based on its type
	m_msgConverter.SetMsg(pMsg);
	m_msgConverter.Process();
  } catch (CNetException e) {
// if cannot extract notify the user
	WriteSystem("The message was received but cannot be read.");
// the connection is broken, close the socket
	DisconnectAll();
  }
// clean up
  delete pMsg;
}

void CSimpleChatDlg::OnSend() 
{
  SendText();
}

void CSimpleChatDlg::OnSendFile() 
{
  SendFile();
}
// here is two ways of handling the events being a subscriber
// since this thread is a CWinThread it has its won queue
// so I can just post this event into it
void CSimpleChatDlg::HandleNotification(CNotifEvent* pEvent)
{
  HWND hwnd = GetSafeHwnd();
  if (!hwnd) 
    return;

  ::PostMessage(hwnd, pEvent->event_id(), (WPARAM)pEvent, 0);
}

// here is an example of using subscriber's own queue.
// To turn this piece of code on set m_bHandleNotify = false in the constructor.
// The events are stored in subscribers CQueue and retrieved
// by polling the queue. For this thread wainting on an event
// would not work since it has to wait for Windows events
// on its own Windows queue. Therefore if I was to wait for
// an event from subscriber's queue, the UI would not update at all (hang)
// So better off just using the Microsoft's supplied queue
// and post messages there via PostMessage(...) aimed at a particualar
// window in a thread or PostThreadMessage(...) aimed at a thread in general
void CSimpleChatDlg::OnTimer(UINT nIDEvent) 
{
  CDialog::OnTimer(nIDEvent);

// if wrong event - return 
  if (m_nCheckForData != nIDEvent)
	return;

  while (IsThereEvent()) {
    unsigned int sz = GetQueueSize();
	char buff[20];
	sprintf_s(buff, "%d", sz);
	SetDlgItemText(IDC_QUEUE_SIZE, buff);
	CNotifEvent* pEvent = GetEvent();
	SendMessage(pEvent->event_id(), (WPARAM)pEvent);
	delete pEvent;
  }

// if no connection or no data has arrived - return 
  if (!m_NetStream.IsOpen())
	return;

  static netStatus status = nsUserDoesNtng;
  static int nTimes = 0;

  if (status != m_ctrlText.m_status.GetCode() || ++nTimes == 50) {
	nTimes = 0;
    SendLocalUserActivityStatus(m_ctrlText.m_status);
    status = (netStatus)m_ctrlText.m_status.GetCode();
  }

  if (!m_NetStream.GetConnection()->CanRead())
	return;

// data is here!
  Receive();
}

void CSimpleChatDlg::OnDisconnect() 
{
// send the logout message if not listening
  if (m_acceptorThread.IsStoped())
    SendLogout();
  DisconnectAll();
}

void CSimpleChatDlg::OnDestroy() 
{
  UnhookWindowsHookEx(g_hook);

  CDialog::OnDestroy();

// send the logout message
  SendLogout();
// stop all threads
  DisconnectAll();

// kill the polling the connection for data and for the events
  KillTimer(m_nCheckForData);

// get rid of systray icon
  SetupTrayIcon(false);
// get rid of the task bar
  ShowWindow(SW_HIDE);

}


void CSimpleChatDlg::OnRestore()
{
//  ShowWindow(SW_RESTORE);
  SetupTrayIcon(false);
}

void CSimpleChatDlg::OnExit()
{
  SendMessage(WM_CLOSE, 0, 0);
}

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////

void CSimpleChatDlg::WriteYourName()
{
  UpdateData();

  CString strNick = "You";
  if (!m_strYou.IsEmpty())
	strNick += " (" + m_strYou + ")";
  strNick += " say";

  m_rtfStream << rtf::italic << rtf::bold << rtf::blue << strNick << rtf::nobold << rtf::noitalic;
  ScrollToEnd();
}

void CSimpleChatDlg::WritePeerName()
{
  CString strNick = "Peer";
  if (!m_strPeer.IsEmpty())
    strNick += " (" + m_strPeer + ")";
  strNick += " says";

  m_rtfStream << rtf::italic << rtf::bold << rtf::blue << strNick << rtf::nobold << rtf::noitalic;
  ScrollToEnd();
}

void CSimpleChatDlg::WriteMsg(const char* strText)
{
  m_rtfStream << " : " << rtf::bold << rtf::black << strText << rtf::nobold << std::endl;
  ScrollToEnd();
}

void CSimpleChatDlg::WriteSystem(const char* strText)
{
  m_rtfStream << rtf::bold << rtf::red << strText << std::endl;
  ScrollToEnd();
}

void CSimpleChatDlg::ScrollToEnd()
{
  int maxy = m_reChat.GetLineCount();
  int miny = m_reChat.GetFirstVisibleLine() + GetVisibleHeight();
  if (maxy > miny)
    m_reChat.LineScroll(maxy - miny);
}

void CSimpleChatDlg::OnClearChat() 
{
  int maxy = m_reChat.GetLineCount();
  int nPos = m_reChat.LineIndex(maxy-1);
  int nLenght	 = m_reChat.LineLength(maxy);    
  m_reChat.SetSel(0, nPos + nLenght);
  m_reChat.ReplaceSel("");
}

ushort CSimpleChatDlg::GetVisibleHeight()
{
  return 12;
}

void CSimpleChatDlg::OnClear() 
{
  m_strText.Empty();
  UpdateData(FALSE);
}

void CSimpleChatDlg::DisableControls()
{
  GetDlgItem(IDC_IPADDRESS)->EnableWindow(FALSE);
  GetDlgItem(IDED_PORT)->EnableWindow(FALSE);
  GetDlgItem(IDRB_CONNECT)->EnableWindow(FALSE);
  GetDlgItem(IDRB_LISTEN)->EnableWindow(FALSE);
  GetDlgItem(IDBT_GO)->EnableWindow(FALSE);

  GetDlgItem(IDBT_SEND)->EnableWindow(TRUE);
  GetDlgItem(IDBT_SEND_FILE)->EnableWindow(TRUE);
  GetDlgItem(IDBT_DISCONNECT)->EnableWindow(TRUE);
}

void CSimpleChatDlg::EnableControls()
{
  GetDlgItem(IDC_IPADDRESS)->EnableWindow(TRUE);
  GetDlgItem(IDED_PORT)->EnableWindow(TRUE);
  GetDlgItem(IDRB_CONNECT)->EnableWindow(TRUE);
  GetDlgItem(IDRB_LISTEN)->EnableWindow(TRUE);
  GetDlgItem(IDBT_GO)->EnableWindow(TRUE);

  GetDlgItem(IDBT_SEND)->EnableWindow(FALSE);
  GetDlgItem(IDBT_SEND_FILE)->EnableWindow(FALSE);
  GetDlgItem(IDBT_SEND_CANCEL)->EnableWindow(FALSE);
  GetDlgItem(IDBT_RECEIVE_CANCEL)->EnableWindow(FALSE);
  GetDlgItem(IDST_PERCENTAGE_RECEIVED)->ShowWindow(SW_HIDE);
  GetDlgItem(IDBT_DISCONNECT)->EnableWindow(FALSE);
}

void CSimpleChatDlg::OnUseThis() 
{
  UpdateData();

  if (m_bUseMsg) {
	GetDlgItem(IDCB_NICK_NAME)->EnableWindow(TRUE);
    GetDlgItem(IDCB_WELCOME_MESSAGE)->EnableWindow(TRUE);
	GetDlgItem(IDCB_GOODBYE_MESSAGE)->EnableWindow(TRUE);
  } else {
	GetDlgItem(IDCB_NICK_NAME)->EnableWindow(FALSE);
    GetDlgItem(IDCB_WELCOME_MESSAGE)->EnableWindow(FALSE);
	GetDlgItem(IDCB_GOODBYE_MESSAGE)->EnableWindow(FALSE);
  } 
}

void CSimpleChatDlg::OnKillfocusNickName() 
{
  CString strOldYou = m_strYou;
  UpdateData();
// if nick name has not changed return
  if (m_strYou == strOldYou) return;
// send a message, informing remote peer that the user has changed its nick name
  SendLogin(nsNickChange);
}

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
// CSimpleChatDlg message handlers

BOOL CSimpleChatDlg::OnInitDialog()
{

   g_hook = SetWindowsHookEx(
		WH_CALLWNDPROC,			// hook type
		CallWndProc,			// hook procedure
		NULL,					// handle to application instance
		AfxGetThread()->m_nThreadID						// thread identifier
	);

   g_pDlg = this;

  m_reChat.SubclassDlgItem(IDRE_MESSAGES, this);
  CDialog::OnInitDialog();

	// IDM_ABOUTBOX must be in the system command range.
	ASSERT((IDM_ABOUTBOX & 0xFFF0) == IDM_ABOUTBOX);
	ASSERT(IDM_ABOUTBOX < 0xF000);
	CMenu* pSysMenu = GetSystemMenu(FALSE);
	if (pSysMenu != NULL) {
	  CString strAboutMenu;
	  strAboutMenu.LoadString(IDS_ABOUTBOX);
	  if (!strAboutMenu.IsEmpty()) {
	    pSysMenu->AppendMenu(MF_SEPARATOR);
		pSysMenu->AppendMenu(MF_STRING, IDM_ABOUTBOX, strAboutMenu);
	  }
	}
	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon

// subclass the button
	m_ctrlText.SubclassWindow(GetDlgItem(IDED_TEXT)->GetSafeHwnd());
// attach the stream
    m_rtfStream.Attach(m_reChat.GetSafeHwnd());
	m_msgConverter.Attach(this);

// find out the local address
	CNetIPAddress address;
	address.SetConnectString(CNetIPAddress::MakeLocalHostAddr(DEFAULT_PORT_N).c_str());
	m_ctrlAddress.SetAddress(g_addr_stu(address.GetHostName()));
	m_strPort = DEFAULT_PORT_N;
	UpdateData(FALSE);

	CButton*	pbtnListen  = (CButton*)GetDlgItem(IDRB_LISTEN);
	CComboBox*  pWBox		= (CComboBox*)GetDlgItem(IDCB_WELCOME_MESSAGE);
	CComboBox*  pGBox		= (CComboBox*)GetDlgItem(IDCB_GOODBYE_MESSAGE);
    CComboBox*  pNBox		= (CComboBox*)GetDlgItem(IDCB_NICK_NAME);

	pbtnListen->SetCheck(1);
	pWBox->SetCurSel(0);
	pGBox->SetCurSel(0);
	pNBox->SetCurSel(0);

	EnableControls();

	OnUseThis();

	m_time = CTime::GetCurrentTime();

// start checking the connection for data
   SetTimer(m_nCheckForData, 100, 0);

   CRect rtParent;
   CRect rt;
   GetWindowRect(rtParent);

   GetDlgItem(IDST_PIN_POINT)->GetWindowRect(rt);
   rt.OffsetRect(rtParent.TopLeft());

   m_imgSmile.Load(IDB_ABOUT_OLD);
   m_imgSmile.SetParent(GetSafeHwnd());
   m_imgSmile.Move(rt.TopLeft());

   m_imgBack.Load(IDB_BACK);
   m_imgBack.SetParent(GetSafeHwnd());
   m_imgBack.Visible(false);

// init the tray icon
//	m_trayIcon.SetData(UWM_ON_NOTIFY, this, IDI_TRAY_ON, IDM_TRAY, lpszTip);

	return TRUE;  // return TRUE  unless you set the focus to a control
}

void CSimpleChatDlg::SetupTrayIcon(bool bMin)
{
  m_bMinimized = bMin;
  if (m_bMinimized && m_pTrayIcon == 0) {
// create the tray
	  m_pTrayIcon = new CSystemTray;
	  m_pTrayIcon->Create(0, m_nmsgTray, lpszTip, AfxGetApp()->LoadIcon(IDI_ABOUT), IDM_TRAY);
// hide this window
	  ShowWindow(SW_HIDE);
	} 
	else {
// get rid of the tray
	  delete m_pTrayIcon;
	  m_pTrayIcon = 0;
// show this window
	  ShowWindow(SW_SHOW);
	}
}

void CSimpleChatDlg::OnSysCommand(UINT nID, LPARAM lParam)
{
  if ((nID & 0xFFF0) == IDM_ABOUTBOX) {
	CAboutDlg dlgAbout;
	dlgAbout.DoModal();
  } else
	CDialog::OnSysCommand(nID, lParam);
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CSimpleChatDlg::OnPaint() 
{
	CPaintDC dc(this); // device context for painting

	if (IsIconic())
	{
		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		CRect rect;
		GetClientRect(&rect);
		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
	  CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CSimpleChatDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////



BOOL CSimpleChatDlg::OnCmdMsg(UINT nID, int nCode, void* pExtra, AFX_CMDHANDLERINFO* pHandlerInfo) 
{
  if (nID == IDM_ABOUTBOX) {
	CAboutDlg dlgAbout;
	dlgAbout.DoModal();
	return TRUE;
  } else
	return CDialog::OnCmdMsg(nID, nCode, pExtra, pHandlerInfo);
}

CNetAddress* CSimpleChatDlg::GetRemoteFileAcceptorAddr()
{
  CNetIPAddress* pAddr = new CNetIPAddress;
  pAddr->SetConnectString(m_strRemoteAddrFileAccept.c_str());
  return pAddr;
}

void CSimpleChatDlg::OnSendCancel() 
{
// get the thread's id these buttons control
  CWnd* pSend = GetDlgItem(IDST_SEND);
  unsigned long nThreadId = pSend->GetWindowContextHelpId();

// find the right thread
  CThreadStorage& storage = g_getThreadStorage();
  while (storage.Size() > 0) {
	CNetThread* pThread = storage.GetAt(storage.Size() - 1);
// if this is the right thread, then abort transfer
// and wait for its self termination and self unregistration
	if (pThread)
	  if (pThread->GetThreadId() == nThreadId) {
		pThread->Stop();
		break;
	  }
  }
}

void CSimpleChatDlg::OnReceiveCancel() 
{
// restore the thread's id these buttons control
  CWnd* pReceive = GetDlgItem(IDST_RECEIVE);
  unsigned long nThreadId = pReceive->GetWindowContextHelpId();

// find the right thread
  CThreadStorage& storage = g_getThreadStorage();
  while (storage.Size() > 0) {
	CNetThread* pThread = storage.GetAt(storage.Size() - 1);
// if this is the right thread, then abort transfer
// and wait for its self termination and self unregistration
	if (pThread)
	  if (pThread->GetThreadId() == nThreadId) {
		pThread->Stop();
		break;
	  }
  }
}

LRESULT CALLBACK CallWndProc(
  int nCode,      // hook code
  WPARAM wParam,  // current-process flag
  LPARAM lParam   // message data
)
{
  if (nCode != HC_ACTION)
    return CallNextHookEx(g_hook, nCode, wParam, lParam);

  HWND hDlg = g_pDlg->GetSafeHwnd();
  HWND hActive = ::GetActiveWindow();

  if (hDlg != hActive) {
    g_pDlg->m_ctrlText.m_status.SetCode(nsUserDoesNtng);
    g_pDlg->m_ctrlText.m_status.SetText("The remote peer is away");
  } else {
	  if (g_pDlg->m_ctrlText.m_status.GetCode() == nsUserDoesNtng) {
	    g_pDlg->m_ctrlText.m_status.SetCode(nsUserIsHere);
	    g_pDlg->m_ctrlText.m_status.SetText("The remote peer is reading messages");
	  }
  }

  return CallNextHookEx(g_hook, nCode, wParam, lParam);
}

void CSimpleChatDlg::OnConnect() 
{
  SetDlgItemText(IDED_ADDRESS, "Peer's IP address:");
}

void CSimpleChatDlg::OnListen() 
{
  SetDlgItemText(IDED_ADDRESS, "Your IP address:");	
}


void CSimpleChatDlg::OnShowBk() 
{
  m_imgBack.Visible(!m_imgBack.IsVisible());	
  Invalidate();
}

BOOL CSimpleChatDlg::OnEraseBkgnd(CDC* pDC) 
{
  CRect rect;
  GetClientRect(&rect);

  CMDC mdc(pDC, rect);
  mdc.FillSolidRect(rect, ::GetSysColor(COLOR_3DFACE));
  m_imgBack.DrawMaskedBlended(&mdc, RGB(255, 255, 255), 40);
  m_imgSmile.DrawMasked(&mdc, RGB(255, 0, 0));

  return TRUE;
}
