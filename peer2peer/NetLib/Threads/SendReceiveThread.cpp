// SendReceiveThread.cpp: implementation of the CSendReceiveThread class.
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
#include "ThreadStorage.h"
#include "NetAddress.h"
#include "NetConnection.h"
#include "NetFile.h"
#include "NetMessage.h"
#include "NetNotif.h"
#include "SendReceiveThread.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CSendReceiveThread::~CSendReceiveThread()
{

}

void CSendReceiveThread::Cleanup()
{
  _DELETE(m_pFile);
  _DELETE(m_pConAddr);
  _DELETE(m_pProvider);
  _DELETE(m_pConsumer);

// remove itself from the storage and release from the memory the stoped thread
  g_getThreadStorage().Remove(this);
  delete this;
}

bool CSendReceiveThread::IsConnected()
{
  return m_pProvider->IsOpen() && m_pConsumer->IsOpen();
}

bool CSendReceiveThread::ConnectConsumer()
{
  if (!m_pConAddr) 
	throw CNetException(ERR_NET_NO_CONSUMER_ADDRESS_PROVIDED, "CSendReceiveThread::ConnectConsumer()", "The consumer stream address is not provided");
// establish the consumer connection
  CNetConnection* pConnector = m_pConAddr->CreateConnector();
// connect to the address
  pConnector->Connect(*m_pConAddr);
// attach it to the connector
  m_pConsumer->Attach(pConnector);
  return true;
}

void CSendReceiveThread::Process()
{
  try {
// if the consumer isn't connected then connect
	if (!IsConnected())
// if failed to connect
      if (!ConnectConsumer()) {
// stop the thread
	    PostStop();
		return;
	  }

// by now the consumer is connected and the file is wanted
// so ship it out!
   SendReceiveFile();

  } catch (CNetException x) {
// notify the subscribers about the error
	CNotifFile* pEvent = 0;
	if (x.GetErrCode() == ERR_NET_FILE_TRANSFER_ABORTED)
	  pEvent = CreateEventAbortFile();
	else
	  pEvent = CreateEventErrorFile();

  	pEvent->setFileTransmittInfo(new CNetFileTransmitInfo(*m_pFile));
	pEvent->setException(new CNetException(x));
	pEvent->setThreadId(GetThreadId());
	Notify(pEvent);
// stop the thread
	PostStop();
  }
}

void CSendReceiveThread::SendReceiveFile()
{
// I want the transmitting layer call me back on the progress
  InstallProgressCallBack();
// I want the transmitting layer call me back as soon as part of the packet was sent/received
//  m_pFile->SetBlockingTransmit(false);

// file is wanted. send it
  while (m_pFile->GetCurrentPacketN() < m_pFile->GetTotalPacketN()) {
	if (!CanContinue())
	  throw CNetException(ERR_NET_FILE_TRANSFER_ABORTED, "void CSendReceiveThread::SendReceiveFile()", "The peer has aborted the file transfer.");

// send/receive the file packet
    m_pFile->SendReceivePacket();
  }

// let the subscribers know that the trasnfer is succeeded
  CNotifFile* pEvent = CreateEventDoneFile();
  pEvent->setFileTransmittInfo(new CNetFileTransmitInfo(*m_pFile));
  pEvent->setThreadId(GetThreadId());
  Notify(pEvent);
// stop the thread
  PostStop();
}

void CSendReceiveThread::OnTransmitProgress(void* pNetFile, void* pSubscriber)
{
  CSendReceiveThread* pThis = (CSendReceiveThread*)pSubscriber;
  if (!pThis) return;
// let the subscribers know about the progress 
  CNotifFile* pEvent = pThis->CreateEventProgressFile();
  pEvent->setFileTransmittInfo(new CNetFileTransmitInfo(*(pThis->m_pFile)));
  pEvent->setThreadId(pThis->GetThreadId());
  pThis->Notify(pEvent);
}