// MImage.cpp: implementation of the CMImage class.
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

#include "stdafx.h"
#include <atlconv.h>

#include "MImage.h"
#include "MDC.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

int _convert = 0; UINT _acp = GetACP(); LPCWSTR _lpw = NULL; LPCSTR _lpa = NULL;

/*#define A2W(lpa) (\
	((_lpa = lpa) == NULL) ? NULL : (\
		_convert = (lstrlenA(_lpa)+1),\
		ATLA2WHELPER((LPWSTR) alloca(_convert*2), _lpa, _convert)))*/

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

CMImage::CMImage():
m_pbmSource(0),
m_pbmMask(0),
m_pIPic(0),
m_hWnd(0),
m_bVisible(true)
{
  Cleanup();

// create the font
  VERIFY(m_font.CreateFont( 12,                        // nHeight
							0,                         // nWidth
							0,                         // nEscapement
							0,                         // nOrientation
							FW_NORMAL,                 // nWeight
							FALSE,                     // bItalic
							FALSE,                     // bUnderline
							0,                         // cStrikeOut
							ANSI_CHARSET,              // nCharSet
							OUT_DEFAULT_PRECIS,        // nOutPrecision
							CLIP_DEFAULT_PRECIS,       // nClipPrecision
							DEFAULT_QUALITY,           // nQuality
							DEFAULT_PITCH | FF_SWISS,  // nPitchAndFamily
							"Arial"));                 // lpszFacename// create it invisible

}

CMImage::~CMImage()
{
  Cleanup();
}

void CMImage::Cleanup(bool bDelBm/* = true*/)
{
// cleanup
  m_ptNew.x  = 0;
  m_ptNew.y  = 0;
  m_ptOld.x  = 0;
  m_ptOld.y  = 0;

  memset(&m_bmHdr, 0, sizeof(m_bmHdr));

// the mask bitmap gets deleted anytime 
// new bitmap is being Loaded/Attached to 'this'
  if (m_pbmMask)
	delete m_pbmMask;
  m_pbmMask = 0;

// delete palette
  m_palette.DeleteObject();
// empty visible rect
  m_rtVisible.SetRectEmpty();

  if (!bDelBm) return;

  if (m_pbmSource) 
    delete m_pbmSource;
  m_pbmSource = 0;

  if (m_pIPic)
    m_pIPic->Release();
}

void CMImage::CreateLogicalPalette()
{
  ASSERT(m_pbmSource);

// precaution
  m_palette.DeleteObject();

// create a logical palette for the bitmap
  DIBSECTION ds;
  ZeroMemory(&ds, sizeof(ds));
  BITMAPINFOHEADER &bmInfo = ds.dsBmih;
  m_pbmSource->GetObject(sizeof(ds), &ds);

  int nColors = bmInfo.biClrUsed ? bmInfo.biClrUsed : 1 << bmInfo.biBitCount;

// create a halftone palette if colors > 256. 
  CClientDC dc(NULL);			// Desktop DC

  if( nColors > 256 )
	m_palette.CreateHalftonePalette(&dc);
  else {
// Create the palette
	RGBQUAD *pRGB = new RGBQUAD[nColors];
	CDC mdc;
	mdc.CreateCompatibleDC(&dc);
	mdc.SelectObject(m_pbmSource);

	::GetDIBColorTable(mdc, 0, nColors, pRGB );

	UINT nSize = sizeof(LOGPALETTE) + (sizeof(PALETTEENTRY) * nColors);
	LOGPALETTE *pLP = (LOGPALETTE *) new BYTE[nSize];

	pLP->palVersion = 0x300;
	pLP->palNumEntries = nColors;

	for( int i=0; i < nColors; i++)	{
	  pLP->palPalEntry[i].peRed = pRGB[i].rgbRed;
	  pLP->palPalEntry[i].peGreen = pRGB[i].rgbGreen;
	  pLP->palPalEntry[i].peBlue = pRGB[i].rgbBlue;
	  pLP->palPalEntry[i].peFlags = 0;
	}

	m_palette.CreatePalette(pLP);

	delete[] pLP;
	delete[] pRGB;
  }
}

BOOL CMImage::Load(UINT nBm)
{
  Cleanup();

  LPCTSTR lpszBm = (LPCTSTR)nBm;
// still need to understand LR_CREATEDIBSECTION
  HBITMAP hBm = (HBITMAP)::LoadImage(AfxGetInstanceHandle(), lpszBm, 
								     IMAGE_BITMAP, 0, 0, LR_DEFAULTCOLOR/*LR_CREATEDIBSECTION*/);
  if(!hBm) return FALSE;

  m_pbmSource = new CBitmap;
  m_pbmSource->Attach(hBm);
  m_pbmSource->GetBitmap(&m_bmHdr);

  CreateLogicalPalette();

  return TRUE;
}

BOOL CMImage::Load(const char* path)
{
  Cleanup();

  LPOLESTR lposPath = A2W(path);
  if (OleLoadPicturePath(lposPath, 0, 0, 0, IID_IPicture, (void**)&m_pIPic) != S_OK)
    return FALSE;

  OLE_HANDLE hBm = 0;
  m_pIPic->get_Handle(&hBm);
  if (!hBm) {
	Cleanup();
	return FALSE;
  }
  m_pbmSource = new CBitmap;
  m_pbmSource->Attach((HBITMAP)hBm);
  m_pbmSource->GetBitmap(&m_bmHdr);

  CreateLogicalPalette();
  return TRUE;
}

BOOL CMImage::Save(const char* path)
{
  if (!m_pIPic) return FALSE;

  LPOLESTR lposPath = A2W(path);

  IPictureDisp* pDispatch = 0;
  if (m_pIPic->QueryInterface(IID_IPictureDisp, (void**)&pDispatch) != S_OK)
	return FALSE;

  if (OleSavePictureFile(pDispatch, lposPath) != S_OK) {
	pDispatch->Release();
    return FALSE;
  }

  pDispatch->Release();
  return TRUE;
}

CBitmap* CMImage::Attach(CBitmap* pBm)
{
// save in a temp pointer the source bitmap
  CBitmap* pSource = m_pbmSource;
// pass 'false' to preserve the source bitmap
  Cleanup(false);

  m_pbmSource = pBm;
  m_pbmSource->GetBitmap(&m_bmHdr);

  CreateLogicalPalette();

  return pSource;
}

CBitmap* CMImage::Detach()
{
// save in a temp pointer the source bitmap
  CBitmap* pSource = m_pbmSource;
// pass false to preserve the source bitmap
  Cleanup(false);
// members points to nil
  m_pbmSource = 0;

  return pSource;
}

void CMImage::DrawAt(HWND hWnd, CDC& dc, const CPoint& pt)
{
  if (!m_bVisible) return;

  ASSERT(hWnd);
// save the CWnd pointer
  m_hWnd = hWnd;
// save new position
  m_ptNew = pt;

// get the union of old and new rect
  CRect rtUnion;
  GetRectUnionVisible(rtUnion);
// invalidate that rect in the window this image is drawn
// but leave the background alone
  InvalidateRect(m_hWnd, rtUnion, TRUE);

// draw it
  Draw(&dc);

// validate that rect in the window back in
  ValidateRect(m_hWnd, rtUnion);
}

void CMImage::Draw(CDC* pDC)
{
// simply draw the whole image without nay blending
  DrawBlended(pDC, 0xFF);
}

void CMImage::DrawBlended(CDC* pDC, BYTE nAlpha)
{
  DrawBlendedRect(pDC, CRect(0, 0, m_bmHdr.bmWidth, m_bmHdr.bmHeight), nAlpha);
}

// draw a part of image defined by rect with nAlpha blend factor
void CMImage::DrawBlendedRect(CDC* pDC, CRect& rect, BYTE nAlpha)
{
  if (!m_bVisible) return;
// precaution
  ASSERT(pDC);
  ASSERT(m_hWnd);
  ASSERT(m_pbmSource);

// get new rect
  CRect rtNew;
  GetRectNew(rtNew);  

  CRect rtClip;
  pDC->GetClipBox(rtClip);
// draw only if any part of the image is visible
  if (!pDC->RectVisible(rtNew) || !rtClip.IntersectRect(rtNew, rtClip))
	return;

  CPalette* pOldPalette = 0;
// realize pallete if supported and I built it for this image successfully
  BOOL bPalette = pDC->GetDeviceCaps(RASTERCAPS) & RC_PALETTE;
  if(bPalette && m_palette.m_hObject) {
    pOldPalette = pDC->SelectPalette(&m_palette, FALSE);
    pDC->RealizePalette();
  }

// this DC will hold the restored background and the new image
  CMDC dcAll(pDC, rtNew);

// now copy current source pDC image
// so the invisible areas of this image will show correct background
  dcAll.CopyFromBase();

// blend it if asked
  if (nAlpha < 0xFF)
	dcAll.CopyBlendFrom(*m_pbmSource, CPoint(rect.left, rect.top), m_ptNew, rtNew.Width(), rtNew.Height(), nAlpha);
  else
// draw the bitmap itself, only visible part of it
    dcAll.CopyFrom(*m_pbmSource, CPoint(rect.left, rect.top), m_ptNew, rtNew.Width(), rtNew.Height());

// now when drawing is over new point is old
  m_ptOld = m_ptNew;

// unrealize my pallete and realize privious one
  if (pOldPalette) {
	pDC->SelectPalette(pOldPalette, FALSE);
    pDC->RealizePalette();
  }
}


void CMImage::DrawMasked(CDC* pDC, COLORREF crMask)
{
  DrawMaskedRect(pDC, CRect(0, 0, m_bmHdr.bmWidth, m_bmHdr.bmHeight), crMask);
}

void CMImage::DrawMaskedRect(CDC* pDC, CRect& rect, COLORREF crMask)
{
  if (!m_bVisible) return;

// precaution
  ASSERT(pDC);
  ASSERT(m_hWnd);
  ASSERT(m_pbmSource);

// get new rect
  CRect rtNew;
  GetRectNew(rtNew);  

  CRect rtClip;
  pDC->GetClipBox(rtClip);

// draw only if any part of the image is visible
  if (!pDC->RectVisible(rtNew) || !rtClip.IntersectRect(rtNew, rtClip))
	return;

  CPalette* pOldPalette = 0;
// realize pallete if supported and I built it for this image successfully
  BOOL bPalette = pDC->GetDeviceCaps(RASTERCAPS) & RC_PALETTE;
  if(bPalette && m_palette.m_hObject) {
    pOldPalette = pDC->SelectPalette(&m_palette, FALSE);
    pDC->RealizePalette();
  }

  if (!m_pbmMask)
	InitMask(crMask);

// this DC will hold the restored background and the new image
  CMDC dcAll(pDC, rtNew);
// now copy current source pDC image
// so the invisible areas of this image will show correct background
  dcAll.CopyFromBase();

// draw the mask of the source bitmap, only visible part of it
  dcAll.CopyFrom(*m_pbmMask, CPoint(rect.left, rect.top), m_ptNew, rect.Width(), rect.Height(), SRCAND);
// now draw the source bitmap itself, only visible part of it
  dcAll.CopyFrom(*m_pbmSource, CPoint(rect.left, rect.top), m_ptNew, rect.Width(), rect.Height(), SRCPAINT);

// now when drawing is over new point is old
  m_ptOld = m_ptNew;

// unrealize my pallete and realize privious one
  if (pOldPalette) {
	pDC->SelectPalette(pOldPalette, FALSE);
    pDC->RealizePalette();
  }
}

void CMImage::DrawMaskedBlended(CDC* pDC, COLORREF crMask, BYTE nAlpha)
{
  DrawMaskedBlendedRect(pDC, CRect(0, 0, m_bmHdr.bmWidth, m_bmHdr.bmHeight), crMask, nAlpha);
}

void CMImage::DrawMaskedBlendedRect(CDC* pDC, CRect& rect, COLORREF crMask, BYTE nAlpha)
{
  if (!m_bVisible) return;

// precaution
  ASSERT(pDC);
  ASSERT(m_hWnd);
  ASSERT(m_pbmSource);

// get new rect
  CRect rtNew;
  GetRectNew(rtNew);  

  CRect rtClip;
  pDC->GetClipBox(rtClip);

// draw only if any part of the image is visible
  if (!pDC->RectVisible(rtNew) || !rtClip.IntersectRect(rtNew, rtClip))
	return;

  CPalette* pOldPalette = 0;
// realize pallete if supported and I built it for this image successfully
  BOOL bPalette = pDC->GetDeviceCaps(RASTERCAPS) & RC_PALETTE;
  if(bPalette && m_palette.m_hObject) {
    pOldPalette = pDC->SelectPalette(&m_palette, FALSE);
    pDC->RealizePalette();
  }

// this DC will hold the restored background and the new image
  CMDC dcAll(pDC, rtNew);
// now copy current source pDC image
// so the invisible areas of this image will show correct background
  dcAll.CopyFromBase();

// blend it
  dcAll.CopyBlendMaskedFrom(*m_pbmSource, CPoint(rect.left, rect.top), m_ptNew, crMask, rect.Width(), rect.Height(), nAlpha);

// now when drawing is over new point is old
  m_ptOld = m_ptNew;

// unrealize my pallete and realize privious one
  if (pOldPalette) {
	pDC->SelectPalette(pOldPalette, FALSE);
    pDC->RealizePalette();
  }
}

void CMImage::SetParent(HWND hWnd)
{
  ASSERT(hWnd);
// save the CWnd pointer
  m_hWnd = hWnd;
}

void CMImage::Move(const CPoint& pt)
{
  ASSERT(m_hWnd);
// save new position
  m_ptNew = pt;
// get the union of old and new rect
  CRect rtUnion;
  GetRectUnionVisible(rtUnion);
// invalidate that rect in the window this image is drawn
  InvalidateRect(m_hWnd, rtUnion, FALSE);
}

void CMImage::InitMask(COLORREF crTrans)
{
  if (m_pbmMask) {
	delete m_pbmMask;
	m_pbmMask = 0;
  }

  m_pbmMask = new CBitmap;

// create monochrome bitmap
  m_pbmMask->CreateBitmap(m_bmHdr.bmWidth, m_bmHdr.bmHeight, 1, 1, NULL);

// create two dc(s) 
  CDC dcS;
  dcS.CreateCompatibleDC(NULL);
  CDC dcD;
  dcD.CreateCompatibleDC(NULL);

// select bitmaps
  CBitmap* pbmOldS = dcS.SelectObject(m_pbmSource);
  CBitmap* pbmOldD = dcD.SelectObject(m_pbmMask);

// set backgound color as crTrans for the source dc
  COLORREF crOldBk = dcS.SetBkColor(crTrans);

// copy the source bitmap into the mask bitmap
  dcD.BitBlt(0, 0, m_bmHdr.bmWidth, m_bmHdr.bmHeight, &dcS, 0, 0, SRCCOPY);

  COLORREF crOldText = dcS.SetTextColor(RGB(255,255,255));
  dcS.SetBkColor(RGB(0,0,0));
  
  dcS.BitBlt(0, 0, m_bmHdr.bmWidth, m_bmHdr.bmHeight, &dcD, 0, 0, SRCAND);

  dcS.SetTextColor(crOldText);
  dcS.SetBkColor(crOldBk);
  dcS.SelectObject(pbmOldS);
  dcD.SelectObject(pbmOldD);
}

void CMImage::GetDiffRgn(CRgn& rgnDiff)
{
}

CRect CMImage::GetSize() const
{
  return CRect(0, 0, m_bmHdr.bmWidth, m_bmHdr.bmHeight);
}

void CMImage::GetRectNew(CRect& rt) const
{
  rt.SetRect(m_ptNew.x, m_ptNew.y, 
	         m_ptNew.x + m_bmHdr.bmWidth, m_ptNew.y + m_bmHdr.bmHeight); 
}

void CMImage::GetRectNewVisible(CRect& rtVisible) const
{
  ASSERT(m_hWnd);

  CRect rtWnd;
  GetClientRect(m_hWnd, &rtWnd);
  CRect rtNew;
  GetRectNew(rtNew);

  rtVisible.IntersectRect(rtWnd, rtNew);
}

void CMImage::GetRectOld(CRect& rt) const
{
  rt.SetRect(m_ptOld.x, m_ptOld.y, 
	         m_ptOld.x + m_bmHdr.bmWidth, m_ptOld.y + m_bmHdr.bmHeight); 
}

void CMImage::GetRectOldVisible(CRect& rtVisible) const
{
  ASSERT(m_hWnd);

  CRect rtWnd;
  GetClientRect(m_hWnd, &rtWnd);
  CRect rtOld;
  GetRectOld(rtOld);

  rtVisible.IntersectRect(rtWnd, rtOld);
}

void CMImage::GetRectUnion(CRect& rtUnion) const
{
// union old img rect with its new one
  CRect rtOld;
  GetRectNew(rtUnion);
  GetRectOld(rtOld);
  rtUnion.UnionRect(rtUnion, rtOld);
}

void CMImage::GetRectUnionVisible(CRect& rtVisible) const
{
  ASSERT(m_hWnd);

  CRect rtWnd;
  GetClientRect(m_hWnd, &rtWnd);
  CRect rtUnion;
  GetRectUnion(rtUnion);

  rtVisible.IntersectRect(rtWnd, rtUnion);
}