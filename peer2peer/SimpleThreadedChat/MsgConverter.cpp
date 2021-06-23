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
#include "ChatMessages.h"
#include "MsgConverter.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CMsgConverter::CMsgConverter()
{

}

CMsgConverter::~CMsgConverter()
{

}

bool CMsgChatConverter::Process()
{
  if (!m_pMsg || !m_pDlg) return false;
  switch (m_pMsg->GetClassId()) {
	case ciNetText				: DoText();
								  break;
	case ciNetLogin				: DoLogin();
								  break;
	case ciNetStatus			: switch (((CNetStatus*)m_pMsg)->GetCode()) {
	                                 case nsLogout       : DoLogout();
														   break;
									 case nsAcceptAddr   : DoAcceptAddr();
														   break;
									 case nsUserDoesNtng :
									 case nsUserTypesMsg :
									 case nsUserIsHere   : DoUserActivityStatus();
														   break;

														   break;
									 default             : return false;
								  };
								  break;
	case ciNetList				: DoList();
								  break;
	default						: return false;
	}
  return true;
}

void CMsgChatConverter::DoLogin()
{
  CNetLogin* pLogin = dynamic_cast<CNetLogin*>(m_pMsg);
// store the Peer's name
  m_pDlg->m_strPeer = pLogin->GetName();

  if (pLogin->GetCode() == nsLogin)
    m_pDlg->WriteSystem("The peer has joined the chat room.");
  else 
	if (pLogin->GetCode() == nsNickChange)
    m_pDlg->WriteSystem("The peer has changed the nick name.");

  if (*pLogin->GetText() != 0) {
    m_pDlg->WritePeerName();
    m_pDlg->WriteMsg(pLogin->GetText());
  }
}

void CMsgChatConverter::DoLogout()
{
  CNetStatus* pLogout = dynamic_cast<CNetStatus*>(m_pMsg);
  if (*pLogout->GetText() != 0) {
    m_pDlg->WritePeerName();
    m_pDlg->WriteMsg(pLogout->GetText());
  }
  m_pDlg->WriteSystem("The peer has left the chat room.");
// this is the last message from the remote peer, so break the connection
  m_pDlg->DisconnectAll();
}

void CMsgChatConverter::DoAcceptAddr()
{
  CNetStatus* pStatus = dynamic_cast<CNetStatus*>(m_pMsg);
// save the address and port number of the remote peer's file acceptor
  m_pDlg->SetRemoteFileAcceptorAddr(pStatus->GetText());
}

void CMsgChatConverter::DoUserActivityStatus()
{
  CNetStatus* pStatus = dynamic_cast<CNetStatus*>(m_pMsg);
// save the address and port number of the remote peer's file acceptor
  m_pDlg->GetDlgItem(IDST_STATUS)->SetWindowText(pStatus->GetText());
}

void CMsgChatConverter::DoText()
{
  m_pDlg->WritePeerName();
  CNetText* pNetText = dynamic_cast<CNetText*>(m_pMsg);
// display this message on the local screen
  m_pDlg->WriteMsg(pNetText->GetText());
}

void CMsgChatConverter::DoList()
{
  CNetList* pNetList = dynamic_cast<CNetList*>(m_pMsg);
// display this message on the local screen
  m_pDlg->WriteSystem("File data transfer complete.");
  m_pDlg->WriteSystem("Saving into a file (Temp.out).");
  CFile file;
  if (!file.Open("Temp.out", CFile::modeCreate | CFile::modeWrite)) {
	m_pDlg->WriteSystem("Cannot create file! Bailing out.");
	return;
  }
  for (int i=0; i<(int)pNetList->GetElemN(); i++) {
	CNetPacket* pPacket = dynamic_cast<CNetPacket*>(pNetList->GetElem(i));
	file.Write(pPacket->GetData(), pPacket->GetSize());
  }
  file.Close();
  m_pDlg->WriteSystem("Done.");
}

