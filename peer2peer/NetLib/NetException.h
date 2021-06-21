// NetException.h: interface for the CNetException class.
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

#if !defined(AFX_NETEXCEPTION_H__C8305B92_33D1_4983_B276_501A47CFDF21__INCLUDED_)
#define AFX_NETEXCEPTION_H__C8305B92_33D1_4983_B276_501A47CFDF21__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#define ERR_NET_UNKNOWN_ERROR                                                                   -1
#define ERR_NET_FAILED_SEND                                                                             100
#define ERR_NET_FAILED_READ                                                                             101
#define ERR_NET_NOT_CONNECTED                                                                   102
#define ERR_NET_FAILED_CREATE_SOCKET                                                    103
#define ERR_NET_FAILED_LISTEN                                                                   104
#define ERR_NET_FAILED_BIND                                                                             105
#define ERR_NET_FAILED_CONNECT                                                                  106
#define ERR_NET_FAILED_INIT_DLL                                                                 107
#define ERR_NET_UNKNOWN_PROTOCOL_TYPE                                                   110
#define ERR_NET_FAILED_RESOLVE_ADDRESS                                                  112
#define ERR_NET_FAILED_SELECT                                                                   113
#define ERR_NET_MSG_DATA_CORRUPTED                                                              115
#define ERR_NET_WRONG_SIGNATURE                                                                 116
#define ERR_NET_CONNECTION_MANAGER_NOT_INITIALIZED                              117
#define ERR_NET_STORAGE_MANAGER_NOT_INITIALIZED                                 118
#define ERR_NET_BAD_CONNECTION_HANDLER                                                  119
#define ERR_NET_FILE_OPEN_FAILED                                                                120
#define ERR_NET_MSG_UKNOWN_TYPE                                                                 121
#define ERR_NET_FILE_INVALID_FLAG                                                               122
#define ERR_NET_FILE_HOST_UNKNOWN                                                               123
#define ERR_NET_FILE_UNKNOWN                                                                    123
#define ERR_NET_FAILED_ACCEPT_SOCKET                                                    124
#define ERR_NET_CANNOT_RESOLVE_PEER_ADDRESS                                             125
#define ERR_NET_CANNOT_RESOLVE_LOCAL_ADDRESS                                    126
#define ERR_NET_NOT_YET_IMPL                                                                    127
#define ERR_NET_MUST_BE_OVERRIDDEN                                                              128
#define NET_ERR_CANNOT_CHECK_FOR_REQUEST                                                129
#define ERR_NET_CANNOT_CREATE_THREADS                                                   130
#define ERR_NET_NO_MSG_FACTORY															131
#define ERR_NET_NO_CALLBACK_REGISTERED													132
#define ERR_NET_BAD_CAST													            133
#define ERR_NET_NO_CONSUMER_ADDRESS_PROVIDED										    134
#define ERR_NET_FILE_TRANSFER_ABORTED													135
#define ERR_NET_CANNOT_RETRIEVE_FROM_EMPTY_QUEUE										136
#define ERR_NET_FAILED_SOCKOPT															137

extern long GetLastSocketError();
extern const char* GetSocketErrorDescription(long);

class CNetException  
{
public:
			     CNetException():m_nNetErr(0) {}
                 CNetException(int nNetErr, const char* strFuncName, const char* strDescr = 0):
                   m_nNetErr(nNetErr)
				 { 
				   if (strFuncName) m_strFuncName = strFuncName;
                   if (strDescr) m_strDescr = strDescr;
				 }

				CNetException(const CNetException& x);
										
		virtual	CNetException& operator=(const CNetException& x)
				{
				  m_nNetErr = x.m_nNetErr;
				  m_strFuncName = x.m_strFuncName;
				  m_strDescr = x.m_strDescr;
				  return *this;
				}

		virtual CNetException* clone() const { return new CNetException; }

        virtual  ~CNetException();
            int  GetErrCode() {return m_nNetErr;}
	  const char* GetErrDescr() {return m_strDescr.c_str();}
      const char* GetErrFunc() {return m_strFuncName.c_str();}

protected:
        int    m_nNetErr;
        string m_strFuncName;
        string m_strDescr;
};

class CNetSockException : public CNetException
{
public:
        CNetSockException(int nNetErr, int nSockErr, const char* strFuncName):
          CNetException(nNetErr, strFuncName),
          m_nSockErr(nSockErr) {}

		CNetSockException(const CNetSockException&);

		virtual CNetException& operator=(const CNetException& x)
		{
		  CNetException::operator=(x);
		  const CNetSockException& sx = dynamic_cast<const CNetSockException&>(x);
		  m_nSockErr = sx.m_nSockErr;
          return *this;
		}

		virtual CNetException* clone() const { return new CNetSockException; }

        virtual ~CNetSockException() {}

        int             GetSockErrCode() {return m_nSockErr;}
		const char*		GetSockErrDescr() {return GetSocketErrorDescription(m_nSockErr);}
protected:
						CNetSockException() {}
protected:
        int     m_nSockErr;
};

class CNetObject;

class CNetMsgException : public CNetException
{
public:
                                CNetMsgException(int nNetErr, const char* strFuncName);
        virtual                 ~CNetMsgException();

		virtual CNetException* clone() const { return new CNetMsgException; }

                        void    Dump();
protected:
								CNetMsgException() {}
};

class CNetFileException : public CNetException
{
public:
        CNetFileException(int nNetErr, const char* strFuncName):
          CNetException(nNetErr, strFuncName) {}
        virtual ~CNetFileException() {}

		virtual  CNetException* clone() const { return new CNetFileException; }
protected:
	    CNetFileException() {}
};

#endif // !defined(AFX_NETEXCEPTION_H__C8305B92_33D1_4983_B276_501A47CFDF21__INCLUDED_)
