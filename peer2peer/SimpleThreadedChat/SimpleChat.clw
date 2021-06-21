; CLW file contains information for the MFC ClassWizard

[General Info]
Version=1
LastClass=CSimpleChatApp
LastTemplate=CRichEditCtrl
NewFileInclude1=#include "stdafx.h"
NewFileInclude2=#include "simplechat.h"
LastPage=0

ClassCount=7
Class1=CSystemTray
Class2=CSimpleChatApp
Class3=CAboutDlg
Class4=CSimpleChatDlg

ResourceCount=3
Resource1=IDD_SIMPLECHAT_DIALOG
Resource2=IDM_TRAY
Class5=CMyRichEditCtrl
Class6=CChatButton
Class7=CMyRichEdit
Resource3=IDD_ABOUTBOX

[CLS:CSystemTray]
Type=0
BaseClass=CWnd
HeaderFile=Shell\SystemTray.h
ImplementationFile=Shell\SystemTray.cpp

[CLS:CSimpleChatApp]
Type=0
BaseClass=CWinApp
HeaderFile=SimpleChat.h
ImplementationFile=SimpleChat.cpp
LastObject=CSimpleChatApp
Filter=N
VirtualFilter=AC

[CLS:CAboutDlg]
Type=0
BaseClass=CDialog
HeaderFile=SimpleChatDlg.cpp
ImplementationFile=SimpleChatDlg.cpp

[CLS:CSimpleChatDlg]
Type=0
BaseClass=CDialog
HeaderFile=SimpleChatDlg.h
ImplementationFile=SimpleChatDlg.cpp
Filter=D
VirtualFilter=dWC
LastObject=CSimpleChatDlg

[DLG:IDD_ABOUTBOX]
Type=1
Class=CAboutDlg
ControlCount=5
Control1=IDC_STATIC,static,1342179331
Control2=IDC_STATIC,static,1342308480
Control3=IDOK,button,1342373889
Control4=IDC_STATIC,static,1342308352
Control5=IDC_STATIC,static,1342308352

[DLG:IDD_SIMPLECHAT_DIALOG]
Type=1
Class=CSimpleChatDlg
ControlCount=40
Control1=IDED_ADDRESS,static,1342308352
Control2=IDC_IPADDRESS,SysIPAddress32,1342242816
Control3=IDC_STATIC,static,1342308352
Control4=IDED_PORT,edit,1350631552
Control5=IDED_TEXT,edit,1352728644
Control6=IDBT_SEND,button,1342242817
Control7=IDBT_GO,button,1342242816
Control8=IDBT_CLEAR,button,1342242816
Control9=IDBT_DISCONNECT,button,1342242816
Control10=IDST_WELCOME,static,1342308352
Control11=IDCB_WELCOME_MESSAGE,combobox,1344340226
Control12=IDST_GOODBYE,static,1342308352
Control13=IDCB_GOODBYE_MESSAGE,combobox,1344340226
Control14=IDST_NICK_NAME,static,1342308352
Control15=IDCB_NICK_NAME,combobox,1344340226
Control16=IDC_PROGRESS_RECEIVED,msctls_progress32,1350565888
Control17=IDST_RECEIVED,static,1342308352
Control18=IDC_PROGRESS_SENT,msctls_progress32,1350565888
Control19=IDST_SENT,static,1342308352
Control20=IDBT_CLEAR_CHAT,button,1342242816
Control21=IDBT_SEND_FILE,button,1342242816
Control22=IDC_QUEUE_SIZE,static,1073872896
Control23=IDBT_SEND_CANCEL,button,1342242816
Control24=IDBT_RECEIVE_CANCEL,button,1342242816
Control25=IDST_STATUS,static,1342312448
Control26=IDST_USE_NAMES,button,1342178055
Control27=IDCH_USE_THIS,button,1342242819
Control28=IDC_STATIC,button,1342177287
Control29=IDRB_CONNECT,button,1342177289
Control30=IDRB_LISTEN,button,1342177289
Control31=IDST_CONNECT_STATUS,static,1342312460
Control32=IDBT_RECEIVE_ACCEPT,button,1476460544
Control33=IDBT_RECEIVE_REJECT,button,1476460544
Control34=IDST_RECEIVE,button,1342177287
Control35=IDST_SEND,button,1342177287
Control36=IDST_PERCENTAGE_RECEIVED,static,1073872896
Control37=IDST_PERCENTAGE_SENT,static,1073872896
Control38=IDST_PIN_POINT,static,1073872896
Control39=IDRE_MESSAGES,RICHEDIT,1352730692
Control40=IDCH_SHOW_BK,button,1342242819

[MNU:IDM_TRAY]
Type=1
Class=?
Command1=IDM_RESTORE
Command2=IDM_EXIT
CommandCount=2

[CLS:CMyRichEditCtrl]
Type=0
HeaderFile=MyRichEditCtrl.h
ImplementationFile=MyRichEditCtrl.cpp
BaseClass=CRichEditCtrl
Filter=W
VirtualFilter=WC
LastObject=CMyRichEditCtrl

[CLS:CChatButton]
Type=0
HeaderFile=ChatButton.h
ImplementationFile=ChatButton.cpp
BaseClass=CButton
Filter=W
VirtualFilter=BWC
LastObject=IDM_EXIT

[CLS:CMyRichEdit]
Type=0
HeaderFile=MyRichEdit.h
ImplementationFile=MyRichEdit.cpp
BaseClass=CRichEditCtrl
Filter=W
VirtualFilter=WC
LastObject=CMyRichEdit

