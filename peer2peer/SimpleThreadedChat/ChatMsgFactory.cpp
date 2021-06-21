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

#include "stdafx.h"
#include "ChatMsgFactory.h"
#include "ChatMessages.h" 
#include "NetFileHdr.h"

CMsgFactory*  g_getDefaultMsgFactory()
{
  static CChatMsgFactory theFactory;
  return &theFactory;
}

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CChatMsgFactory::CChatMsgFactory()
{

}

CChatMsgFactory::~CChatMsgFactory()
{

}

CNetMsg* CChatMsgFactory::CreateMessage(long nClassId)
{
  switch (nClassId) {
    case ciNetStatus	: return new CNetStatus;
    case ciNetLogin		: return new CNetLogin; 
    case ciNetText		: return new CNetText;
	case ciNetList		: return new CNetList;
	case ciNetPacket	: return new CNetPacket;
	case ciNetFileHdr   : return new CNetFileHdr;
    default				: return 0;
  }
  return 0;
}