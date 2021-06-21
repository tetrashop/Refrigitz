// NetSockConnector.h: interface for the CNetSockConnector class.
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

#if !defined(AFX_NETSOCKCONNECTOR_H__46B93FDF_AA35_4EBC_898A_9F1C89B8C401__INCLUDED_)
#define AFX_NETSOCKCONNECTOR_H__46B93FDF_AA35_4EBC_898A_9F1C89B8C401__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetSockConnection.h"

class CNetSockConnector : public CNetSockConnection
{
public:
                                                                        CNetSockConnector();
                                                                        CNetSockConnector(SOCKET hSocket):
                                                                          CNetSockConnection(hSocket) {}

        virtual                                                 ~CNetSockConnector();
        virtual         CNetConnection*         Connect(const CNetAddress&);
        virtual         CNetConnection*         Accept(const CNetAddress& addr) {return Connect(addr);}
        virtual         bool                            IsConnected(const CNetAddress&) const;
};

#endif // !defined(AFX_NETSOCKCONNECTOR_H__46B93FDF_AA35_4EBC_898A_9F1C89B8C401__INCLUDED_)
