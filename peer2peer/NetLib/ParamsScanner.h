// ParamScaner.h: interface for the CParamScaner class.
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

#if !defined(AFX_PARAMSCANER_H__1DA02D18_D0E2_4686_A64F_93A329978245__INCLUDED_)
#define AFX_PARAMSCANER_H__1DA02D18_D0E2_4686_A64F_93A329978245__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CParamsScanner  
{
public:
	enum Tokens
	{
	  tkUnknown		= -1,
	  tkID			= 0,
	  tkValue		= 1,
	  tkEqual		= 2,
	  tkSemiColon	= 3,
	  tkQuote		= 4,
	  tkEnd			= 5,
	  tkError		= 6
	};

public:
							 CParamsScanner();
	virtual					~CParamsScanner();

    virtual		    Tokens   GetToken(const char*, string&);
    virtual CParamsScanner*   Clone() const {return new CParamsScanner;}
protected:
  unsigned int		m_nAhead;

};

#endif // !defined(AFX_PARAMSCANER_H__1DA02D18_D0E2_4686_A64F_93A329978245__INCLUDED_)
