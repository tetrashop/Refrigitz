// SenderThread.cpp: implementation of the CSenderThread class.
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
#include "NetFileHdr.h"
#include "NetFile.h"
#include "NetAddress.h"
#include "NetConnection.h"
#include "NetNotif.h"
#include "SenderThread.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////
/*
CSenderThread::CSenderThread()
{
  m_pProvider = new CFlatStream;
  m_pConsumer = new CNetStream;
}
*/

CSenderThread::CSenderThread(const CNetFileHdr& hdr):
  CSendReceiveThread(hdr)
{
  m_pProvider = new CFlatStream;
  m_pConsumer = new CNetStream;
}

CSenderThread::~CSenderThread()
{

}

CNotifFile* CSenderThread::CreateEventProgressFile() const
{
  return new CNotifProgressFile_Send;
}

CNotifFile* CSenderThread::CreateEventDoneFile() const
{
  return new CNotifDoneFile_Send;
}

CNotifFile* CSenderThread::CreateEventErrorFile() const
{
  return new CNotifErrorFile_Send;
}

CNotifFile* CSenderThread::CreateEventAbortFile() const
{
  return new CNotifAbortFile_Send;
}

// connect to the consumer are three step phase:
// 1. Establish phisical connection with the remote peer
// 2. Send the file header
// 3. Receieve the confirmation/rejection

bool CSenderThread::ConnectConsumer()
{
 // establish a connection with the remote peer (its address must be known)
  CSendReceiveThread::ConnectConsumer();

// send the header data
  SendHeader();

// if no data yet and the thread allowed to work, continue looping
  while (CanContinue() && !m_pConsumer->IsThereData())
	Sleep(0);
// if thread was requested to abort, return false
  if (!CanContinue()) 
    return false;

// got the data, read the status back
  return IsConfirmed();
}

void CSenderThread::InstallProgressCallBack()
{
  if (!m_pFile) return;
// install progress callback for the consumer stream
  m_pFile->InstallProgressCallBack(this, m_pConsumer, CSendReceiveThread::OnTransmitProgress);
}

void CSenderThread::SendHeader()
{
// create the netfile
  m_pFile = new CNetFile(m_pProvider, m_pConsumer, &m_fHdr);
// send header
  m_pFile->SendHeader();
}

bool CSenderThread::IsConfirmed()
{
// read the message
  CNetMsg* pMsg = 0;
  *m_pConsumer >> pMsg;
  CNetStatus* pStatus = dynamic_cast<CNetStatus*>(pMsg);
// if the file is rejected, quit the thread
  if (pStatus->GetCode() == nsReject) {
// notify the subscribers about the rejection
  	CNotifRejectFile* pEvent = new CNotifRejectFile(new CNetFileTransmitInfo(*m_pFile));
	Notify(pEvent);
    return false;
  }
// file is wanted
  return true;
}
