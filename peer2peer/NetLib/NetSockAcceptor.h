// NetAcceptor.h: interface for the CNetAcceptor class.
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

#if !defined(AFX_NETSOCKACCEPTOR_H__9A06289C_10DA_442F_B420_141D1ED8F349__INCLUDED_)
#define AFX_NETSOCKACCEPTOR_H__9A06289C_10DA_442F_B420_141D1ED8F349__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetSockConnection.h"

class CNetSockAcceptor : public CNetSockConnection  
{
public:
                                                                        CNetSockAcceptor();
        virtual                                                 ~CNetSockAcceptor();

        virtual         bool                            CanRead();

        virtual         CNetConnection*         Connect(const CNetAddress&);
        virtual         CNetConnection*         Accept(const CNetAddress&);

        virtual         bool                            IsConnected(const CNetAddress&) const;
protected:
};

#endif // !defined(AFX_NETSOCKACCEPTOR_H__9A06289C_10DA_442F_B420_141D1ED8F349__INCLUDED_)
