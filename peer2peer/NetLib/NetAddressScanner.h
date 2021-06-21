// NetAddressScanner.h: interface for the CNetAddressScanner class.
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

#if !defined(AFX_NETADDRESSSCANNER_H__F29453FC_8A98_45B4_A383_3CD027AFE7CF__INCLUDED_)
#define AFX_NETADDRESSSCANNER_H__F29453FC_8A98_45B4_A383_3CD027AFE7CF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "ParamsScanner.h"

class CNetAddressScanner : public CParamsScanner  
{
public:
						     CNetAddressScanner();
	virtual				    ~CNetAddressScanner();
    virtual		    Tokens   GetToken(const char*, string&);
    virtual CParamsScanner*  Clone() const {return new CNetAddressScanner;}
};

#endif // !defined(AFX_NETADDRESSSCANNER_H__F29453FC_8A98_45B4_A383_3CD027AFE7CF__INCLUDED_)
