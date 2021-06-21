// MDC.cpp: implementation of the CMDC class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "MDC.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CMDC::CMDC():
m_pSDC(0),
m_pOldCanvas(0)
{
  m_crt.SetRectEmpty();
}

// CDC* - the source DC, which this DC will be compatible with
// rect - is the rectangular area of the source DC in which this DC can draw
CMDC::CMDC(CDC* pSDC, CRect& rect):
m_pSDC(0),
m_pOldCanvas(0)
{
  Create(pSDC, rect);
}

void CMDC::Create(CDC* pSDC, CRect& rect)
{
// cleanup
  DeleteObject();
// initialize
  m_pSDC = pSDC;
  m_crt = rect;
// create compatible DC
  CreateCompatibleDC(pSDC);
// and bitmap
  m_canvas.CreateCompatibleBitmap(pSDC, rect.Width(), rect.Height());
// select the created bitmap into 'this' DC and save the old one
  m_pOldCanvas = SelectObject(&m_canvas);
// now reposition origin of this DC, so when one will draw in it the coordinates
// can be relative to the origin of the source DC
  SetWindowOrg(rect.left, rect.top);
// copy the clipping rect
  CRect rtClip;
  pSDC->GetClipBox(rtClip);
  IntersectClipRect(rtClip);
}

// destroyes the canvas, and DC
void CMDC::DeleteObject()
{
  if (!m_pSDC) return;

  SelectObject(m_pOldCanvas);
  m_canvas.DeleteObject();
  m_crt.SetRectEmpty();
  m_pOldCanvas = 0;
  m_pSDC = 0;
  CDC::DeleteDC();
}

CMDC::~CMDC()
{
// if m_pSDC is 0 then caller used empty constructor
// and never called Create(...), or prior do destroying
// this instance called DestroyObject()
  if (!m_pSDC) return;

  CopyToBase();
  SelectObject(m_pOldCanvas);
}

void CMDC::CopyFromBase()
{
// now copy the image from the source buffer into 'this' buffer 
  BitBlt(m_crt.left, m_crt.top, m_crt.Width(), m_crt.Height(), 
         m_pSDC, m_crt.left, m_crt.top, SRCCOPY);
}

void CMDC::CopyToBase()
{
// now copy the image from the 'this' memory buffer into the source 
  m_pSDC->BitBlt(m_crt.left, m_crt.top, m_crt.Width(), m_crt.Height(), 
	             this, m_crt.left, m_crt.top, SRCCOPY);
}

// copies the context of 'this' 
// at position pt with size of [dx,dy] 
// into bmTarget
void CMDC::CopyTo(CBitmap& bmTarget, CPoint& pt, int dx, int dy)
{
// precaution
  ASSERT(m_pSDC);

// if the bitmap has to be created for the first time
  if (!bmTarget.m_hObject) {
// recreate
    bmTarget.DeleteObject();
    bmTarget.CreateCompatibleBitmap(m_pSDC, dx, dy);
  } else {
	// the bitmap has been created but will not fit the requested [dx, dy]
    BITMAP bm;
    bmTarget.GetBitmap(&bm);
    if (dx > bm.bmWidth || dy > bm.bmHeight) {
    // recreate
      bmTarget.DeleteObject();
   	  bmTarget.CreateCompatibleBitmap(m_pSDC, dx, dy);
	}
  }

// create compatible with m_pSDC dc and bitmap
  CDC dc;
  dc.CreateCompatibleDC(m_pSDC);
// copy the context
  CBitmap* pOldBitmap = dc.SelectObject(&bmTarget);
  dc.BitBlt(0, 0, dx, dy, 
	        this, pt.x, pt.y, SRCCOPY);
// restore the old bitmap
  dc.SelectObject(pOldBitmap);
}

// copies the context of bmSource into m_pSDC
// at position pt with size of [dx,dy] 
void CMDC::CopyFrom(CBitmap& bmSource, CPoint& ptSrc, CPoint& pt, 
					int dx /*= -1*/, int dy /*= -1*/, DWORD nOp/* = SRCCOPY*/)
{
// precaution
  ASSERT(m_pSDC);

  BITMAP bm;
  bmSource.GetBitmap(&bm);
  if (dx == -1 && dy == -1) {
	dx = bm.bmWidth;
	dy = bm.bmHeight;
  } else
	  if (ptSrc.x + dx > bm.bmWidth || ptSrc.y + dy > bm.bmHeight) {
	    dx = bm.bmWidth  - ptSrc.x;
		dy = bm.bmHeight - ptSrc.y;
	  }

  int infx = (pt.x + dx) - m_crt.right;
  int infy = (pt.y + dy) - m_crt.bottom;
// if the m_canvas will not be able to fit bmSource - error out
  if (infx > 0 || infy > 0 )
	ASSERT(FALSE);

// create compatible with m_pSDC dc and bitmap
  CDC dc;
  dc.CreateCompatibleDC(m_pSDC);
// copy the context of the source bitmap
  CBitmap* pOldBitmap = dc.SelectObject(&bmSource);
  BitBlt(pt.x, pt.y, dx, dy, 
		 &dc, ptSrc.x, ptSrc.y, nOp);
// restore the old bitmap
  dc.SelectObject(pOldBitmap);
}

// copy and blend the first param bitmap into 'this' dc bitmap 
// at the position indicated by the second param, 
// and with the Width and the Height as the third  and fourth the  params specify
// with the alpha as the fifth param tells
// with the "transparent" color as the six parameter
void CMDC::CopyBlendFrom(CBitmap& bmSource, CPoint& ptSrc, CPoint& pt, int dx /* = -1*/, int dy /* = -1*/, int nA /* = 127*/)
{
// the source image is being blended out of the range of the destination DC
  if ((pt.x + dx) - m_crt.right > 0 ||
	  (pt.y + dy) - m_crt.bottom > 0)
	ASSERT(FALSE);

  BYTE r, g, b;
  BYTE rS, gS, bS, rD, gD, bD;
  COLORREF crS;
  COLORREF crD;
  CRect rect;
// the rect to be copied and blended
  rect.SetRect(0, 0, dx, dy);
// create compatible with m_pSDC dc and bitmap
  CDC dc;
  dc.CreateCompatibleDC(m_pSDC);
// copy the context of the source bitmap
  CBitmap* pOldBitmap = dc.SelectObject(&bmSource);

  for (int x=0; x<rect.Width() - ptSrc.x; x++)
	 for (int y=0; y<rect.Height() - ptSrc.y; y++) {
// start from x = ptSrc.x + x; y = ptSrc.y + y
	   crS = dc.GetPixel(ptSrc.x + x, ptSrc.y + y);
// start from pt.x + x; pt.x + y
	   crD = GetPixel(pt.x + x, pt.y + y);

	   rS = GetRValue(crS);
	   gS = GetGValue(crS);
	   bS = GetBValue(crS);

	   rD = GetRValue(crD);
	   gD = GetGValue(crD);
	   bD = GetBValue(crD);

       r =  (BYTE)((((rS - rD) * nA) + (rD << 8)) >> 8);
	   g =  (BYTE)((((gS - gD) * nA) + (gD << 8)) >> 8);
	   b =  (BYTE)((((bS - bD) * nA) + (bD << 8)) >> 8);
       
	   SetPixel(pt.x + x, pt.y + y, RGB(r,g,b));
	 }

/*
	if ( m_Size!=Dib->m_Size )
		return;

	BYTE *src=(BYTE*)Dib->m_Bits;
	BYTE *dst=(BYTE*)m_Bits;
	int Size=m_Size.cx*m_Size.cy;
		
	while ( Size-- )
	{
		dst[0]=(BYTE)(((src[0]-dst[0])*A+(dst[0]<<8))>>8);
		dst[1]=(BYTE)(((src[1]-dst[1])*A+(dst[1]<<8))>>8);
		dst[2]=(BYTE)(((src[2]-dst[2])*A+(dst[2]<<8))>>8);	
		dst+=4;
		src+=4;
	}
*/
 }

// param 1 - copy and blend the source bitmap into 'this' dc bitmap 
// param 2 - the origin point for 'this' dc bitmap
// param 3 - dx of the source bitmap
// param 4 - dy of the source bitmap
// param 5 - blend strength (alpha value)
// param 6 - "transparent" color (the source's bitmap color that is excluded and only destination's bitmap color is preserved)

void CMDC::CopyBlendMaskedFrom(CBitmap& bmSource, CPoint& ptSrc, CPoint& pt, COLORREF crTrans, int dx /*= -1*/, int dy /*= -1*/, int nA /* = 127*/)
{
// !! test !!
//  float nA = 0.5;

// the source image is being blended out of the range of the destination DC
  if ((pt.x + dx) - m_crt.right > 0 ||
	  (pt.y + dy) - m_crt.bottom > 0)
	ASSERT(FALSE);

  BYTE r, g, b;
  BYTE rS, gS, bS, rD, gD, bD;
  COLORREF crS;
  COLORREF crD;
  CRect rect;
// the rect to be copied and blended
  rect.SetRect(0, 0, dx, dy);
// create compatible with m_pSDC dc and bitmap
  CDC dc;
  dc.CreateCompatibleDC(m_pSDC);
// copy the context of the source bitmap
  CBitmap* pOldBitmap = dc.SelectObject(&bmSource);

  for (int x=0; x<rect.Width() - ptSrc.x; x++)
	 for (int y=0; y<rect.Height() - ptSrc.y; y++) {
// start from x = ptSrc.x + x; y = ptSrc.y + y
	   crS = dc.GetPixel(ptSrc.x + x, ptSrc.y + y);
// start from pt.x + x; pt.x + y
	   crD = GetPixel(pt.x + x, pt.y + y);

// if this is the "transparent" color, ignore it 
	   if (crS == crTrans)
		 continue;

	   rS = GetRValue(crS);
	   gS = GetGValue(crS);
	   bS = GetBValue(crS);

	   rD = GetRValue(crD);
	   gD = GetGValue(crD);
	   bD = GetBValue(crD);
/*
	   r =  (BYTE)(rS * nA + (1 - nA) * rD);
	   g =  (BYTE)(gS * nA + (1 - nA) * gD);
	   b =  (BYTE)(bS * nA + (1 - nA) * bD);
*/
       r =  (BYTE)((((rS - rD) * nA) + (rD << 8)) >> 8);
	   g =  (BYTE)((((gS - gD) * nA) + (gD << 8)) >> 8);
	   b =  (BYTE)((((bS - bD) * nA) + (bD << 8)) >> 8);
       
	   SetPixel(pt.x + x, pt.y + y, RGB(r,g,b));
	 }
}

