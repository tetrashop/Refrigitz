// ReceiverThread.cpp: implementation of the CReceiverThread class.
//
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
#include "ReceiverThread.h"
#include "NetFile.h"
#include "NetNotif.h"
#include "ThreadStorage.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CReceiverThread::CReceiverThread()
{
  m_pProvider = new CNetStream;
  m_pConsumer = new CFlatStream;
}

CReceiverThread::CReceiverThread(const CNetFileHdr& hdr):
  CSendReceiveThread(hdr)
{
  m_pProvider = new CNetStream;
  m_pConsumer = new CFlatStream;
}

CReceiverThread::~CReceiverThread()
{

}

CNotifFile* CReceiverThread::CreateEventProgressFile() const
{
  return new CNotifProgressFile_Receive;
}

CNotifFile* CReceiverThread::CreateEventDoneFile() const
{
  return new CNotifDoneFile_Receive;
}

CNotifFile* CReceiverThread::CreateEventErrorFile() const
{
  return new CNotifErrorFile_Receive;
}

CNotifFile* CReceiverThread::CreateEventAbortFile() const
{
  return new CNotifAbortFile_Receive;
}

// connect to the consumer is a three step phase:
// 1. Receieve the file header
// 2. Establish physical connection to the local file
// 3. Send a status back with the confirmation/rejection for this file

bool CReceiverThread::ConnectConsumer()
{
// if no data yet and the thread allowed to work, continue looping
  while (CanContinue() && !m_pProvider->IsThereData());
// if thread was requested to abort, return false
  if (!CanContinue()) 
    return false;

// got data, read the header
  ReceiveHeader();

// now block until the GUI thread decides whether this file is wanted
//  while (CanContinue() && m_nsStatus == nsPending)
//	Sleep(0);

// send the accept status to the provider
  CNetStatus status;
  status.SetCode(m_nsStatus);
  *m_pProvider << status;

  if (m_nsStatus == nsReject) 
	return false;

 // establish a connection with the local file (throws if can't connect)
  CSendReceiveThread::ConnectConsumer();
  return true;
}

void CReceiverThread::InstallProgressCallBack()
{
  if (!m_pFile) return;
  if (!m_pFile) return;
// install progress callback for the provider stream
  m_pFile->InstallProgressCallBack(this, m_pProvider, CSendReceiveThread::OnTransmitProgress);
}

void CReceiverThread::ReceiveHeader()
{
// got data, receive the header info
  m_pFile = new CNetFile(m_pProvider, m_pConsumer);
  m_pFile->ReceiveHeader();

// let the subscribers know about the file 
// (at this point the subscriber(s) decide if they want this file - press <Accept...> or <Reject>)
  CNotifReceivedHeaderFile* pEvent1 = new CNotifReceivedHeaderFile(new CNetFileTransmitInfo(*m_pFile));
// set this thread id, so the subscriber knows at which thread 
// to update the status of the file
  pEvent1->setThreadId(GetThreadId());
  Notify(pEvent1);

  int nSec = 0;
// now I'll wait until timed out or action from the user
  while ((CanContinue() && m_nsStatus == nsPending) && nSec < 30) {
	Sleep(1000);
	nSec++;
  }

// if either timed out or nsReject just bail out
  if (m_nsStatus != nsAccept) {
// if timed out make sure it's set to nsReject
	m_nsStatus = nsReject;
// notify the subscribers about the rejection
  	CNotifRejectReceiveFile* pEvent2 = new CNotifRejectReceiveFile(new CNetFileTransmitInfo(*m_pFile));
// set this thread id, so the subscriber knows at which thread 
// to update the status of the file
    pEvent2->setThreadId(GetThreadId());
  	Notify(pEvent2);
	return;
  }

// okay user pressed <Accept...>, 
// let the UI choose the destination directory and the file name
  CNotifSaveAsFile* pEvent3 = new CNotifSaveAsFile(new CNetFileTransmitInfo(*m_pFile));
// set this thread id, so the subscriber knows at which thread 
// to update the status of the file
  pEvent3->setThreadId(GetThreadId());
  Notify(pEvent3);

// now I'll wait until the user either chooses the file or cancels out of the dialog
  while (CanContinue() && m_nsStatus == nsAccept)
	Sleep(0);
// if nsReject returned then the caller will bail out
}
