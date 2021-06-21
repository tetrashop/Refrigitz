// MsgConverter.h: interface for the CMsgConverter class.
//
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

#if !defined(AFX_MSGCONVERTER_H__32A147B8_9A7D_4474_8E78_C3E7BA8105AF__INCLUDED_)
#define AFX_MSGCONVERTER_H__32A147B8_9A7D_4474_8E78_C3E7BA8105AF__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CNetMsg;
class CSimpleChatDlg;

class CMsgConverter  
{
public:
						CMsgConverter();
	virtual				~CMsgConverter();

	virtual		bool	Process() = 0;

				void	SetMsg(CNetMsg* pMsg) {m_pMsg = pMsg;}
protected:
	CNetMsg* m_pMsg;
};

class CMsgChatConverter : public CMsgConverter
{
public:
						CMsgChatConverter() {}
	virtual				~CMsgChatConverter() {}

	virtual	bool		Process();

			void		Attach(CSimpleChatDlg* pDlg) {m_pDlg = pDlg;}
protected:
			void		DoText();
			void		DoLogin();
			void		DoLogout();
			void		DoUserActivityStatus();
			void        DoAcceptAddr();
			void		DoList();

protected:
	CSimpleChatDlg* m_pDlg;
};

#endif // !defined(AFX_MSGCONVERTER_H__32A147B8_9A7D_4474_8E78_C3E7BA8105AF__INCLUDED_)
