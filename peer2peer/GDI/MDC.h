// MDC.h: interface for the CMDC class.
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

#if !defined(AFX_MDC_H__58FA4C9D_71C5_4C7A_83B8_ED6E63267F6B__INCLUDED_)
#define AFX_MDC_H__58FA4C9D_71C5_4C7A_83B8_ED6E63267F6B__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class AFX_EXT_CLASS CMDC : public CDC  
{
public:
						CMDC();
						CMDC(CDC*, CRect&);

				void	Create(CDC*, CRect&);

				bool	IsInit() { return (m_pSDC && !m_crt.IsRectNull()) ? true : false; }

			   CRect&	GetRect() {return m_crt;}

// copy (SRCCOPY) from CDC* bitmap that was passed  in to the constructor 
// or Create(...) method into 'this' dc bitmap (equalize the base and 'this' dc)

				void	CopyFromBase();

// copy (SRCCOPY) from 'this' dc bitmap into CDC* bitmap that was passed 
// in to the constructor or Create(...) method (flush the drawings)

				void	CopyToBase();

// param 1 - copy (SRCCOPY) 'this' dc bitmap into the destination bitmap
//           if the destination bitmap is not init-ed or is too small it will be recreated
// param 2 - the origin point for 'this' dc bitmap
// param 3 - dx of the source bitmap
// param 4 - dy of the source bitmap

				void	CopyTo(CBitmap&, CPoint&, int, int);

// param 1 - blit (SRCCOPY) the source bitmap into 'this' dc bitmap 
// param 2 - the origin point for the source bitmap, relative to the base dc coordinates
// param 3 - the origin point for 'this' dc bitmap relative to the base dc coordinates
// param 4 - dx of the source bitmap
// param 5 - dy of the source bitmap
// param 6 - raster operation (SRCCOPY, SRCAND, ...)

				void	CopyFrom(CBitmap&, CPoint&, CPoint&, 
					             int dx = -1, int dy = -1, DWORD = SRCCOPY);

// param 1 - copy and blend the source bitmap into 'this' dc bitmap 
// param 2 - the origin point for the source bitmap
// param 3 - the origin point for 'this' dc bitmap
// param 4 - dx of the source bitmap
// param 5 - dy of the source bitmap
// param 6 - blend strength (alpha value)

				void    CopyBlendFrom(CBitmap&, CPoint&, CPoint&, int = -1, int = -1, int nA = 127);

// param 1 - copy and blend the source bitmap into 'this' dc bitmap 
// param 2 - the origin point for the source bitmap
// param 3 - the origin point for 'this' dc bitmap
// param 4 - dx of the source bitmap
// param 5 - dy of the source bitmap
// param 6 - blend strength (alpha value)
// param 7 - "transparent" color (the source's bitmap color that is excluded and only destination's bitmap color is preserved)

				void    CopyBlendMaskedFrom(CBitmap&, CPoint&, CPoint&, COLORREF crTrans, int = -1, int = -1, int nA = 127);

				void	DeleteObject();

	virtual				~CMDC();

	CBitmap  m_canvas;
protected:
				void GetColorRef(BYTE&, BYTE&, BYTE&, BITMAP&, BYTE*, unsigned int x, unsigned int y);
				bool SetColorRef(BYTE, BYTE, BYTE, BITMAP&, BYTE*, unsigned int x, unsigned int y);
protected:
	CBitmap* m_pOldCanvas;
	BYTE*	 m_pCanvasBits;
	CDC*	 m_pSDC;
	CRect	 m_crt;
};

#endif // !defined(AFX_MDC_H__58FA4C9D_71C5_4C7A_83B8_ED6E63267F6B__INCLUDED_)
