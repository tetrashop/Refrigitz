// HoverButton.cpp : implementation file
//

#include "stdafx.h"
#include "HoverButton.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CHoverButton

CHoverButton::CHoverButton()
{
	m_bHover = FALSE;
	m_bTracking = FALSE;

}

CHoverButton::~CHoverButton()
{
}
IMPLEMENT_DYNAMIC(CHoverButton, CBitmapButton)

BEGIN_MESSAGE_MAP(CHoverButton, CBitmapButton)
	//{{AFX_MSG_MAP(CHoverButton)
	ON_WM_MOUSEMOVE()
ON_MESSAGE(WM_MOUSELEAVE, OnMouseLeave)
ON_MESSAGE(WM_MOUSEHOVER, OnMouseHover)
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////
 //	CHoverButton message handlers
		
void CHoverButton::OnMouseMove(UINT nFlags, CPoint point) 
{
	//	TODO: Add your message handler code here and/or call default

	if (!m_bTracking)
	{
		TRACKMOUSEEVENT tme;
		tme.cbSize = sizeof(tme);
		tme.hwndTrack = m_hWnd;
		tme.dwFlags = TME_LEAVE|TME_HOVER;
		tme.dwHoverTime = 1;
		m_bTracking = _TrackMouseEvent(&tme);
	}
	CBitmapButton::OnMouseMove(nFlags, point);
}

BOOL CHoverButton::PreTranslateMessage(MSG* pMsg) 
{
	// TODO: Add your specialized code here and/or call the base class
	InitToolTip();
	m_ToolTip.RelayEvent(pMsg);		
	return CButton::PreTranslateMessage(pMsg);
}

// Set the tooltip with a string resource
void CHoverButton::SetToolTipText(int nId, BOOL bActivate)
{
	CString sText;

	// load string resource
	sText.LoadString(nId);
	// If string resource is not empty
	if (sText.IsEmpty() == FALSE) SetToolTipText(&sText, bActivate);

}

// Set the tooltip with a CString
void CHoverButton::SetToolTipText(CString *spText, BOOL bActivate)
{
	// We cannot accept NULL pointer
	if (spText == NULL) return;

	// Initialize ToolTip
	InitToolTip();

	// If there is no tooltip defined then add it
	if (m_ToolTip.GetToolCount() == 0)
	{
		CRect rectBtn; 
		GetClientRect(rectBtn);
		m_ToolTip.AddTool(this, (LPCTSTR)*spText, rectBtn, 1);
	}

	// Set text for tooltip
	m_ToolTip.UpdateTipText((LPCTSTR)*spText, this, 1);
	m_ToolTip.Activate(bActivate);
}

void CHoverButton::InitToolTip()
{
	if (m_ToolTip.m_hWnd == NULL)
	{
		// Create ToolTip control
		m_ToolTip.Create(this);
		// Create inactive
		m_ToolTip.Activate(FALSE);
	}
} // End of InitToolTip

// Activate the tooltip
void CHoverButton::ActivateTooltip(BOOL bActivate)
{
	// If there is no tooltip then do nothing
	if (m_ToolTip.GetToolCount() == 0) return;

	// Activate tooltip
	m_ToolTip.Activate(bActivate);
} // End of EnableTooltip






void CHoverButton::DrawItem(LPDRAWITEMSTRUCT lpDrawItemStruct) 
{
	// TODO: Add your code to draw the specified item

	CDC* pDC = CDC::FromHandle(lpDrawItemStruct->hDC);
		
// the button state images in the image: normal | selected | hoover

// this is the second state (selected)
	if(lpDrawItemStruct->itemState & ODS_SELECTED)
	  m_imgFace.DrawMaskedRect(pDC, 
		                      CRect(m_ButtonSize.cx, 0, m_ButtonSize.cx * 2, m_ButtonSize.cy),
							  m_crTrans);
	else {		
// this is the third state (hoover)
	  if (m_bHover)
		m_imgFace.DrawMaskedRect(pDC, 
	                            CRect(m_ButtonSize.cx * 2, 0, m_ButtonSize.cx * 3, m_ButtonSize.cy),
							    m_crTrans);
	  else
// this is the first state (normal - unselected)
		m_imgFace.DrawMaskedRect(pDC, 
	                            CRect(0, 0, m_ButtonSize.cx, m_ButtonSize.cy),
							    m_crTrans);

	}
}

// Load a bitmap from the resources in the button, the bitmap has to have 3 buttonsstates next to each other: Up/Down/Hover
BOOL CHoverButton::LoadBitmap(UINT bitmapid, COLORREF crTrans)
{
/*
	mybitmap.Attach(::LoadImage(::AfxGetInstanceHandle(),MAKEINTRESOURCE(bitmapid), IMAGE_BITMAP,0,0,LR_LOADMAP3DCOLORS));
	BITMAP	bitmapbits;
	mybitmap.GetBitmap(&bitmapbits);
*/
	m_imgFace.Load(bitmapid);
	m_crTrans = crTrans;
	m_ButtonSize.cy = m_imgFace.GetSize().Height();
	m_ButtonSize.cx = m_imgFace.GetSize().Width() / 3;

	SetWindowPos( NULL, 0,0, m_ButtonSize.cx,m_ButtonSize.cy,SWP_NOMOVE   |SWP_NOOWNERZORDER   );

	m_imgFace.SetParent(GetSafeHwnd());
	m_imgFace.Move(CPoint(0,0));

	return TRUE;
}



LRESULT CHoverButton::OnMouseHover(WPARAM wparam, LPARAM lparam)
{
	// TODO: Add your message handler code here and/or call default
	m_bHover=TRUE;
	Invalidate();
	return 0;
}


LRESULT CHoverButton::OnMouseLeave(WPARAM wparam, LPARAM lparam)
{
	m_bTracking = FALSE;
	m_bHover=FALSE;
	Invalidate();
	return 0;
}
