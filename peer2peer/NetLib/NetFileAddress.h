// NetFileAddress.h: interface for the CNetFileAddress class.
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

#if !defined(AFX_NETFILEADDRESS_H__9B70D465_4ABC_4CD4_B099_B9EE0D8960CD__INCLUDED_)
#define AFX_NETFILEADDRESS_H__9B70D465_4ABC_4CD4_B099_B9EE0D8960CD__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetIPAddress.h"
#include "NetFileConnector.h"

class CNetFileAddress : public CNetIPAddress  
{
public:
									 	 CNetFileAddress();
        virtual                          ~CNetFileAddress();

        virtual       CNetConnection*    CreateConnector() const 
											{ return new CNetBinFileConnector; }

        virtual                 void     SetConnectString(const char* szAddr);

                          const char*    GetFileLocation() const;
                                bool     CanOverwrite() const;
                                bool     IsEmpty() const;
								bool     IsReadOnly() const;

        virtual CNetAddress*    Clone() const {return new CNetFileAddress;}
protected:
                                  void          HandleException(int nNetErr, const char* strSrc) const;
								  void			ValidateFlags() const;
};

#endif // !defined(AFX_NETFILEADDRESS_H__9B70D465_4ABC_4CD4_B099_B9EE0D8960CD__INCLUDED_)
