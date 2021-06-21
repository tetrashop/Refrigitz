// NotifFile.cpp: implementation of the CNotifSaveAsFile class.
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
#include "NetNotif.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////



CNotifEvent* CNotifListening::clone() const 
{
  return new CNotifListening;
}

CNotifEvent* CNotifAccept::clone() const 
{
  return new CNotifAccept;
}

CNotifEvent* CNotifAcceptForFile::clone() const 
{
  return new CNotifAcceptForFile;
}

CNotifEvent* CNotifErrorAccept::clone() const 
{
  return new CNotifErrorAccept;
}

CNotifEvent* CNotifFile::clone() const 
{
  return new CNotifFile;
}

CNotifEvent* CNotifSaveAsFile::clone() const 
{
  return new CNotifSaveAsFile;
}

CNotifEvent* CNotifReceivedHeaderFile::clone() const 
{
  return new CNotifReceivedHeaderFile ;
}

CNotifEvent* CNotifProgressFile_Send::clone() const 
{
  return new CNotifProgressFile_Send;
}

CNotifEvent* CNotifProgressFile_Receive::clone() const 
{
  return new CNotifProgressFile_Receive;
}

CNotifEvent* CNotifDoneFile_Send::clone() const 
{
  return new CNotifDoneFile_Send;
}

CNotifEvent* CNotifDoneFile_Receive::clone() const 
{
  return new CNotifDoneFile_Receive;
}

CNotifEvent* CNotifRejectFile::clone() const 
{
  return new CNotifRejectFile;
}

CNotifEvent* CNotifRejectReceiveFile::clone() const 
{
  return new CNotifRejectReceiveFile;
}

CNotifEvent* CNotifErrorFile_Send::clone() const 
{
  return new CNotifErrorFile_Send;
}

CNotifEvent* CNotifErrorFile_Receive::clone() const 
{
  return new CNotifErrorFile_Receive;
}

CNotifEvent* CNotifAbortFile_Send::clone() const 
{
  return new CNotifAbortFile_Send;
}

CNotifEvent* CNotifAbortFile_Receive::clone() const 
{
  return new CNotifAbortFile_Receive;
}

CNotifEvent* CNotifPortAvailable::clone() const 
{
  return new CNotifPortAvailable;
}

CNotifEvent* CNotifPortUnavailable::clone() const 
{
  return new CNotifPortUnavailable;
}

CNotifEvent* CNotifNoPortAvailable::clone() const 
{
  return new CNotifNoPortAvailable;
}
