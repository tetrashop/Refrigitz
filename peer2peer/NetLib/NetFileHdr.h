// NetFileHdr.h: interface for the CNetFileHdr class.
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

#if !defined(AFX_NETFILEHDR_H__331D7632_6108_4ECA_BC12_3F7C25C81F39__INCLUDED_)
#define AFX_NETFILEHDR_H__331D7632_6108_4ECA_BC12_3F7C25C81F39__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetMessage.h"

// Sample: c:\windows\command\start.exe - file
//         c:\windows\command\InBetween - directory
//         c:\windows\command\InBetween\end.exe - file

// If the "command" directory was selected to be sent, then

// for start.exe:
// m_strProviderPath = "command\"
// m_strName = "start.exe"
// m_bDir = false;

// for InBetween:
// m_strProviderPath = "command\"
// m_strName = "InBetween";
// m_bDir = true;

// for end.exe:
// m_strProviderPath = "command\InBetween\"
// m_strName = "end.exe";
// m_bDir = false;

// the consumer path will comprise out of 
// path = _User_Local_Selected_Path_ + m_strProviderPath
// If it is a directory then mkdir(m_strName) is called
// If it is a file then CNetFileAddress is created and 
// the this connect string is passed "file = 'path'; openopt = 'trunc'"

#include "NetFileInfo.h"

class CNetFileHdr : public CNetMsg  
{
public:
  struct MSG {
// # of CNetPackets upcomming for the CNetFile to expect
     ulong  m_nElemN;
// size of the file in bytes
	 ulong  m_nFileSize;
	 ulong  m_nProviderPathSz;
// a relative path of the file (must end with '\'). 
	 string m_strProviderPath;
	 ulong  m_nProviderNameSz;
// the file/directory name
	 string m_strProviderName;

	 ulong  m_nConsumerPathSz;
// a relative path of the file (must end with '\'). 
	 string m_strConsumerPath;
	 ulong  m_nConsumerNameSz;
// the file/directory name
	 string m_strConsumerName;
// indicates whether this is a directory (if it is there is no CNetPacket(s) to follow)
	 bool   m_bDir;  
  };
										CNetFileHdr() {}
	                                     //CNetFileHdr(const CNetFileInfo&);
										 CNetFileHdr(const CNetFileHdr&);
	virtual                             ~CNetFileHdr();

	            CNetFileHdr&             operator=(const CNetFileHdr&);

					   bool              IsDirectory() const 
											{return m_data.m_bDir;}

			     const char*             GetProviderName() const
											{return m_data.m_strProviderName.c_str();}

			     const char*             GetProviderPath() const
											{return m_data.m_strProviderPath.c_str();}

			     const char*             GetConsumerName() const
											{return m_data.m_strProviderName.c_str();}

			     const char*             GetConsumerPath() const
											{return m_data.m_strProviderPath.c_str();}

			           ulong             GetTotalPacketN() const
											{return m_data.m_nElemN;}

					   ulong             GetFileSize() const
											{return m_data.m_nFileSize;}
public:
    virtual         CNetMsg*             Clone() const 
											{return new CNetFileHdr;}

	virtual            bool              HasChildren() const 
											{return false;}

                  const MSG&             GetMSG() const 
											{return m_data;}

// returns the class id as defined in the "NetMsgNames.h" file
                      ulong              GetClassId() const {return ciNetFileHdr;}
// returns the size in bytes of members of this message that can be sent accross the network
                      ulong              GetSize_Write() const;
// dumps the data into the ostream
                       void              Dump(ostream&);

					   void              SetFileSize(ulong n);
	                   void              SetDirectory(bool b);
					   void              SetProviderName(const char* name);
					   void              SetProviderPath(const char* path);
					   void              SetConsumerName(const char* name);
					   void              SetConsumerPath(const char* path);

protected:
	                   void              ComputePacketN();

protected:
// reads the message members from m_pData
                       void              ReadBody();
// reads the message members from the prototype
                       void              ReadBody(const CNetMsg&);
// write the message members into m_pData
                      void               WriteBody();
protected:
	MSG		m_data;
};


#endif // !defined(AFX_NETFILEHDR_H__331D7632_6108_4ECA_BC12_3F7C25C81F39__INCLUDED_)
