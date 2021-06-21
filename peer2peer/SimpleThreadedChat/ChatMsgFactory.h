// Written by Marat Bedretdinov (maratb@hotmail.com)
// Copyright (c) 2000.
//
// This code may be used in compiled form in any way you desire. This
// file may be redistributed unmodified by any means PROVIDING it is 
// not sold for profit without the authors written consent, and 
// providing that this notice and the authors name is included. 
//
// If the source code in  this file is used in any commercial application 
// then acknowledgement must be made to the author of this file 
// and permissions to use this file are requested from the author
//
// (in whatever form you wish).// This file is provided "as is" with no expressed or implied warranty.
// The author accepts no liability if it causes any damage whatsoever.
// It's free - so you get what you pay for.//

#if !defined(AFX_CHATMSGFACTORY_H__70156ED1_F434_4F4E_A19F_6AB508F7EE19__INCLUDED_)
#define AFX_CHATMSGFACTORY_H__70156ED1_F434_4F4E_A19F_6AB508F7EE19__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetMessage.h" 

class CChatMsgFactory : public CMsgFactory  
{
public:
						CChatMsgFactory();
	virtual				~CChatMsgFactory();

	virtual	CNetMsg*	CreateMessage(long);
};

#endif // !defined(AFX_CHATMSGFACTORY_H__70156ED1_F434_4F4E_A19F_6AB508F7EE19__INCLUDED_)
