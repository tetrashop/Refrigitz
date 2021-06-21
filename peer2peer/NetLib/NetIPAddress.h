// NetIPAddress.h: interface for the CNetIPAddress class.
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

#if !defined(AFX_NETIPADDRESS_H__B4C555D7_EBC4_42FB_98D2_34A3D895BF42__INCLUDED_)
#define AFX_NETIPADDRESS_H__B4C555D7_EBC4_42FB_98D2_34A3D895BF42__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetAddress.h"
#include "NetSockConnector.h"

class CNetIPAddress : public CNetAddress  
{
public:
                                         CNetIPAddress();
        virtual                          ~CNetIPAddress();

        virtual       CNetConnection*    CreateConnector() const 
											{ return new CNetSockConnector; }

        virtual 	     CNetAddress* 	 Clone() const; 

                                void     SetConnectString(sockaddr* sockAddr);

        virtual                 void     SetConnectString(const char* szAddr);

					static	  string	 MakeLocalHostAddr(const char*);

                                void     GetLocalHostAddr(sockaddr_in&) const;
							  string     GetLocalHostName() const;

                          const char*    GetHostName() const;
                                 int     GetPortNumber() const;
 
                                void     GetRemoteHostAddr(sockaddr_in&) const;

                                bool     IsEmpty() const;
protected:
                                void     HandleException(int nNetErr, const char* strSrc) const;
};


#endif // !defined(AFX_NETIPADDRESS_H__B4C555D7_EBC4_42FB_98D2_34A3D895BF42__INCLUDED_)
