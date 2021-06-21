// Params.h: interface for the CParams class.
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

#if !defined(AFX_PARAMS_H__0551014C_1814_4297_AB17_6258E087D61B__INCLUDED_)
#define AFX_PARAMS_H__0551014C_1814_4297_AB17_6258E087D61B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

// the class contains a string of parameters in the following
// format: id=value[;id=value...]
// the BNF looks like this:
//
// start ::= pair list | e
// pair  ::= id "='" value "'"
// list  ::= ";" start | e

#include "ParamsScanner.h"

class CParamsParser  
{
public:
						CParamsParser();
	virtual				~CParamsParser();

				void	SetScannerType(CParamsScanner& proto);

				void	SetString(const char*);
		      string	GetString() const;
		  const char*	GetValue(const char*) const;

			    bool	IsEmpty() const 
					{ return m_mapValues.size() == 0; }

				bool	Update(const char*, const char*);
protected:
				bool	Parse();
				bool	DoPair();
				bool	DoList();
				bool	Match(CParamsScanner::Tokens);
protected:
	map<string, string>		m_mapValues;
	const char*				m_Input;
	string					m_strToken;
    CParamsScanner::Tokens	m_tkType;
	CParamsScanner*			m_pScanner;
};

#endif // !defined(AFX_PARAMS_H__0551014C_1814_4297_AB17_6258E087D61B__INCLUDED_)
