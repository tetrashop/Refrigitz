// NetFile.h: interface for the CNetFile class.
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

#if !defined(AFX_NETFILE_H__00BF63FC_09C4_4C58_8626_7E4EA4D3B61C__INCLUDED_)
#define AFX_NETFILE_H__00BF63FC_09C4_4C58_8626_7E4EA4D3B61C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetFileHdr.h"
#include "NetStream.h"

class CNetFileHdr;

// provider and consumer can hide behind any kind of medium depending
// on the CNetConnection it is attached to (local hard disk storage, network)
// CNetFile ultimately is a bridge between two incompatible mediums.

class CNetFile  
{
public:
                                       CNetFile(CNetStream* pProvider, CNetStream* pConsumer, CNetFileHdr* pHeader = 0);

	virtual                           ~CNetFile();

					 void              SendHeader();
	                 void              ReceiveHeader();

                     void			   InstallProgressCallBack(void*, CNetStream*, PROGRESS_CB);

// receives a single CNetPacket from provider and sends it to the consumer
					 bool              SendReceivePacket();
					 void              SetBlockingTransmit(bool);

				    ulong              GetCurrentPacketN() const;
					ulong              GetTotalPacketN() const;
        const CNetFileHdr&             GetNetFileHdr() const {return m_hdr;}

					ulong              GetCurrentBytesN() const;
					ulong              GetTotalBytesN() const;
protected:
	   static        void              OnTransmitProgress(void*, void*);
protected:
	CNetFileHdr  m_hdr;
	CNetPacket*  m_pPacket;
	CNetStream*  m_pProvider;
	CNetStream*  m_pConsumer;
	ulong        m_nPacketN;
    ulong        m_nLastPacketSz;
	ulong		 m_nTransmitedSz;
    PROGRESS_CB  m_pfnPtr;
    void*        m_pSubscriber;
};

// this is a helper class which carries the info about the file 
// being transmitted from the CSendReceiveThread to 
// a GUI subscribed thread (CDialog)

class CNetFileTransmitInfo
{
public:
  struct MSG {
// the size of the file in bytes
     ulong  m_nTotalSz;
// the size of the file in bytes that has been sent/received so far
	 ulong  m_nCurrentSz;
// a relative path of the file (must end with '\'). 
	 string m_strProviderPath;
// the file/directory name
	 string m_strProviderName;
// a relative path of the file (must end with '\'). 
	 string m_strConsumerPath;
// the file/directory name
	 string m_strConsumerName;
// indicates whether this is a directory (if it is there is no CNetPacket(s) to follow)
	 bool   m_bDir;  
  };

public:
	                               CNetFileTransmitInfo() {}
                                   CNetFileTransmitInfo(const CNetFile&);
  virtual                         ~CNetFileTransmitInfo() {}

           CNetFileTransmitInfo*   clone() const {return new CNetFileTransmitInfo;}
           CNetFileTransmitInfo&   operator=(const CNetFileTransmitInfo&);
		              const MSG&   GetData() const {return m_data;}
					      ushort   GetCompletePercentage() const;
protected:
protected:
  MSG m_data;
};

#endif // !defined(AFX_NETFILE_H__00BF63FC_09C4_4C58_8626_7E4EA4D3B61C__INCLUDED_)
