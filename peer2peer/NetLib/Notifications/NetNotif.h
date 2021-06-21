// NotifFile.h: interface for the CNotifFile class.
//
// Written by Marat Bedretdinov (maratb@hotmail.com)
// Copyright (c) 2000.
//
// This code may be used in compiled form in any way you desire. This
// file may be redistributed unmodified by any means PROVIDING it is 
// not sold for profit without the authors written consent, and 
// providing that this notice and the authors name is included. 
//
// This file is provided "as is" with no expressed or implied warranty.
// The author accepts no liability if it causes any damage whatsoever.
// It's free - so you get what you pay for.//

#if !defined(AFX_NOTIFFILE_H__EFF6643A_1982_48FB_B1E7_F3FF5023F9C6__INCLUDED_)
#define AFX_NOTIFFILE_H__EFF6643A_1982_48FB_B1E7_F3FF5023F9C6__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "Notification.h"
#include "NetFile.h"
#include "NetAddress.h"

class CNotifListening : public CNotifEvent  
{
public:
	                        CNotifListening() {}

    virtual  unsigned int   event_id() const {return UWM_ON_LISTENING;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifAccept : public CNotifEvent  
{
public:
	                        CNotifAccept() {}

    virtual  unsigned int   event_id() const {return UWM_ON_ACCEPT;}
	virtual  CNotifEvent*   clone() const;
};

// the CNetConnection object if CNotifAcceptForFile
// is copied is only copied by pointer, be ware 
// I don't think it's a good idea to make socket calls
// on the same socket from diff. threads
class CNotifAcceptForFile : public CNotifEvent  
{
public:
							CNotifAcceptForFile(): 
								m_pConnection(0) {}
							CNotifAcceptForFile(CNetConnection* p):
								m_pConnection(p) {}

    virtual  unsigned int   event_id() const {return UWM_ON_ACCEPT_FOR_FILE;}
	virtual  CNotifEvent*   clone() const;
	virtual  CNotifEvent&   operator=(const CNotifEvent& e) 
	{ 
	   CNotifEvent::operator=(e);
	   m_pConnection = ((CNotifAcceptForFile&)e).m_pConnection; 
	   return *this;
	}
		  CNetConnection*  getConnection() {return m_pConnection;}
protected:
	CNetConnection* m_pConnection;
};

class CNotifErrorAccept : public CNotifEvent  
{
public:
	                        CNotifErrorAccept() {}

    virtual  unsigned int   event_id() const {return UWM_ON_ACCEPT_ERROR;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifFile : public CNotifEvent  
{
public:
						   CNotifFile(): 
							 m_pInfo(0) {}
	                       CNotifFile(CNetFileTransmitInfo* pInfo):
	                         m_pInfo(pInfo) {}

				   void	   setFileTransmittInfo(CNetFileTransmitInfo* pInfo)
							{m_pInfo = pInfo;}

   virtual  unsigned int   event_id() const {return UWM_ON_FILE;}
   virtual  CNotifEvent*   clone() const;
   virtual  CNotifEvent&   operator=(const CNotifEvent& e) 
   { 
	   CNotifEvent::operator=(e);
	   _DELETE(m_pInfo);
	   if (((CNotifFile&)e).m_pInfo) {
	     m_pInfo = ((CNotifFile&)e).m_pInfo->clone();
	     *m_pInfo = *(((CNotifFile&)e).m_pInfo); 
	   }
	   return *this;
   }

   const CNetFileTransmitInfo& getInfo() const {return *m_pInfo;}

protected:
   virtual                ~CNotifFile()
							{ delete m_pInfo; }
   
protected:
  CNetFileTransmitInfo* m_pInfo;
};

class CNotifReceivedHeaderFile : public CNotifFile  
{
public:
							 CNotifReceivedHeaderFile() {}
						     CNotifReceivedHeaderFile(CNetFileTransmitInfo* pInfo):
						       CNotifFile(pInfo) {}

     virtual  unsigned int   event_id() const {return UWM_ON_FILE_RECEIVED_HEADER_FILE;}
	 virtual  CNotifEvent*   clone() const;
};

class CNotifSaveAsFile : public CNotifFile  
{
public:
							 CNotifSaveAsFile() {}
						     CNotifSaveAsFile(CNetFileTransmitInfo* pInfo):
						       CNotifFile(pInfo) {}

     virtual  unsigned int   event_id() const {return UWM_ON_FILE_SAVE_AS;}
	 virtual  CNotifEvent*   clone() const;
};

class CNotifProgressFile_Send : public CNotifFile  
{
public:
							CNotifProgressFile_Send() {}
						    CNotifProgressFile_Send(CNetFileTransmitInfo* pInfo):
						      CNotifFile(pInfo) {}

    virtual  unsigned int   event_id() const {return UWM_ON_FILE_PROGRESS_SEND;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifProgressFile_Receive : public CNotifFile  
{
public:
							CNotifProgressFile_Receive() {}
						    CNotifProgressFile_Receive(CNetFileTransmitInfo* pInfo):
						      CNotifFile(pInfo) {}

    virtual  unsigned int   event_id() const {return UWM_ON_FILE_PROGRESS_RECEIVE;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifDoneFile_Send : public CNotifFile  
{
public:
							CNotifDoneFile_Send() {}
						    CNotifDoneFile_Send(CNetFileTransmitInfo* pInfo):
						      CNotifFile(pInfo) {}

    virtual  unsigned int   event_id() const {return UWM_ON_FILE_DONE_SEND;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifDoneFile_Receive : public CNotifFile  
{
public:
							CNotifDoneFile_Receive() {}
						    CNotifDoneFile_Receive(CNetFileTransmitInfo* pInfo):
						      CNotifFile(pInfo) {}

    virtual  unsigned int   event_id() const {return UWM_ON_FILE_DONE_RECEIVE;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifRejectFile : public CNotifFile  
{
public:
							CNotifRejectFile() {}
						    CNotifRejectFile(CNetFileTransmitInfo* pInfo):
						      CNotifFile(pInfo) {}

    virtual  unsigned int   event_id() const {return UWM_ON_FILE_REJECT;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifRejectReceiveFile : public CNotifFile  
{
public:
							CNotifRejectReceiveFile() {}
						    CNotifRejectReceiveFile(CNetFileTransmitInfo* pInfo):
						      CNotifFile(pInfo) {}

    virtual  unsigned int   event_id() const {return UWM_ON_FILE_RECEIVE_REJECT;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifErrorFile_Send : public CNotifFile  
{
public:
							CNotifErrorFile_Send() {}
						    CNotifErrorFile_Send(CNetFileTransmitInfo* pInfo, CNetException* pX):
						      CNotifFile(pInfo)
							  { setException(pX); }

    virtual  unsigned int   event_id() const {return UWM_ON_FILE_ERROR_SEND;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifErrorFile_Receive : public CNotifFile  
{
public:
							CNotifErrorFile_Receive() {}
						    CNotifErrorFile_Receive(CNetFileTransmitInfo* pInfo, CNetException* pX):
						      CNotifFile(pInfo)
							  { setException(pX); }


    virtual  unsigned int   event_id() const {return UWM_ON_FILE_ERROR_RECEIVE;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifAbortFile_Send : public CNotifFile  
{
public:
							CNotifAbortFile_Send() {}
						    CNotifAbortFile_Send(CNetFileTransmitInfo* pInfo, CNetException* pX):
						      CNotifFile(pInfo)
							  { setException(pX); }


    virtual  unsigned int   event_id() const {return UWM_ON_FILE_ABORT_SEND;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifAbortFile_Receive : public CNotifFile  
{
public:
							CNotifAbortFile_Receive() {}
						    CNotifAbortFile_Receive(CNetFileTransmitInfo* pInfo, CNetException* pX):
						      CNotifFile(pInfo)
							  { setException(pX); }

    virtual  unsigned int   event_id() const {return UWM_ON_FILE_ABORT_RECEIVE;}
	virtual  CNotifEvent*   clone() const;
};

/*
class CNotifErrorConnect : public CNotifEvent  
{
public:
							CNotifErrorConnect() {}
						   CNotifErrorConnect(const CNetException& x):
						     m_x(x) {}

    virtual                 ~CNotifErrorConnect() {}

    virtual  unsigned int   event_id() const {return UWM_ON_CONNECT_ERROR;}
	virtual  CNotifEvent*   clone() const {return new CNotifErrorConnect;}
	virtual  CNotifEvent&   operator=(const CNotifEvent& e) { m_x = ((CNotifErrorConnect&)e).m_x; return *this;}
protected:
	CNetException m_x;
};
*/
class CNotifPortAvailable : public CNotifEvent  
{
public:
							 CNotifPortAvailable() {}
                             CNotifPortAvailable(string& addr)
								{ m_strAddr = addr; }

    virtual  unsigned int   event_id() const {return UWM_ON_ADDR_PORT_AVAILABLE;}
	virtual  CNotifEvent*   clone() const;
	virtual  CNotifEvent&   operator=(const CNotifEvent& e) 
	{ 
		CNotifEvent::operator=(e);
		m_strAddr = ((CNotifPortAvailable&)e).m_strAddr; 
		return *this;
	}
	                 void   GetAddress(string& addr) const { addr = m_strAddr; }
protected:
	string m_strAddr;
};

class CNotifPortUnavailable : public CNotifPortAvailable  
{
public:
							 CNotifPortUnavailable() {}
                             CNotifPortUnavailable(string& addr):
								CNotifPortAvailable(addr) {}

    virtual  unsigned int   event_id() const {return UWM_ON_ADDR_PORT_UNAVAILABLE;}
	virtual  CNotifEvent*   clone() const;
};

class CNotifNoPortAvailable : public CNotifPortAvailable  
{
public:
							 CNotifNoPortAvailable() {}
                             CNotifNoPortAvailable(string& addr):
								CNotifPortAvailable(addr) {}

    virtual  unsigned int   event_id() const {return UWM_ON_NO_ADDR_PORT_AVAILABLE;}
	virtual  CNotifEvent*   clone() const;
};

#endif // !defined(AFX_NOTIFFILE_H__EFF6643A_1982_48FB_B1E7_F3FF5023F9C6__INCLUDED_)
