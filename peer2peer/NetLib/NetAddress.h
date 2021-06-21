// NetAddress.h: interface for the CNetAddress class.
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

#if !defined(AFX_NETADDRESS_H__B42B393F_A4EB_48B3_8CD0_63DFF82B21FD__INCLUDED_)
#define AFX_NETADDRESS_H__B42B393F_A4EB_48B3_8CD0_63DFF82B21FD__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "ParamsParser.h"

class CNetConnection;

class CNetAddress  
{
public:
                                         CNetAddress();
        virtual                         ~CNetAddress();

		virtual       CNetConnection*    CreateConnector() const = 0;

        virtual                 void     SetConnectString(const char* szAddr) = 0;
						      string	 GetConnectString() const { return m_parser.GetString(); }

        virtual                 bool     IsEmpty() const {return m_parser.IsEmpty();}
        virtual          CNetAddress&    operator=(const CNetAddress&);
        virtual                 bool     operator==(const CNetAddress&);
        virtual          CNetAddress*    Clone() const = 0;
protected:
        virtual                 void     HandleException(int nNetErr, const char* strSrc) const = 0;

protected:
  CParamsParser  m_parser;
protected:
// parsing stuff
//                          string        GetLexValue(const char*, ulong&) const;
//                          string        GetValue(ulong&) const;
//                                void     RemoveSpaces();
};

// m_strAddr supports the following format:
// "[[[addr = '{IP address}']; [port = '{Port Number}']]; [file = '{File location}'; [openopt = 'app' | 'trunc';]]]"
// where :
// IP address    - is either a name resolvable by a DNS (for example: crusader)
//                 or an IP address (for example: 127.0.0.1). 
//                 This value is not required if establishing a listener on a local host
//
// Port Number   - the port number on which a listener will accept an incomming transmittion
// File location - a path + a file name for a persistant file (for example c:\command.com)
//
// Examples: 
//  "addr = 'crusader'; port = '5555';" - usually used by a connecting application (client)
//
//  "port = '5555';"                    - usually used by a listening application (server)
//                                        adding a local host name doen't hurt
//
//  "file = 'c:\command.com'; openopt = 'app'"      - if a file need be read from the local host
//  "file = 'c:\command.com'; openopt = 'trunc'"      - if a file need be written to the local host
//
//  addr = 'crusader'; port = '5555'; file = 'c:\command.com'; - if a file need be read from a remote machine
//
//  BNF grammar definition:
//
//  expr     :== pair list | e;
//  pair     :== addr | port | file | openopt;
//  list     :== ";" expr | e;
//
//  addr     :== id_addr "=" value_addr;
//  port     :== id_port "=" value_port;
//  file     :== id_file "=" value_file;
//  openopt  :== id_openopt "=" value_openopt;

//  id_addr    :== "addr";
//  id_port    :== "port";
//  id_file    :== "file";
//  id_openopt :== "openopt";
//  value_addr :== ip | hostname;
//  hostname   :== letter text;
//  text	   :== word morewords;
//  morewords  :== "." word morewords | e;
//  word       :== letter identifier;
//  identifier :== letter identifier | digit identifier | e;
//  ip         :== 

#endif // !defined(AFX_NETADDRESS_H__B42B393F_A4EB_48B3_8CD0_63DFF82B21FD__INCLUDED_)
