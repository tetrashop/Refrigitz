// MImage.h: interface for the CMImage class.

// Written by Marat Bedretdinov (maratb@hotmail.com)
// Copyright (c) 2000.
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

#if !defined(AFX_MIMAGE_H__CD439035_CC81_4FF2_8016_FA5D0A19AF8C__INCLUDED_)
#define AFX_MIMAGE_H__CD439035_CC81_4FF2_8016_FA5D0A19AF8C__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class AFX_EXT_CLASS CMImage  
{
public:
						 CMImage();
	virtual				~CMImage();

				void	 SetParent(HWND pWnd);
				bool	 IsInit() const {return (m_pbmSource && m_hWnd);}

// loads image from a linked in resource
				BOOL	 Load(UINT);
// load image from a file system file or a URL
				BOOL	 Load(const char*);
// saves image to a file system file or a URL
				BOOL	 Save(const char*);

			CBitmap*	 Attach(CBitmap*);
			CBitmap*	 Detach();

			void		 Visible(bool b) {m_bVisible = b;}
			bool		 IsVisible() const {return m_bVisible;}
// this only invalidates the necessary rectangle, but does not actually draw
// the drawing will occur when CWnd gets its WM_PAINT message
// don't foget to place any of the desired calls to Draw() methods
// in the WM_PAINT handler
				void	 Move(const CPoint&);
// draws the image as it is at the position passed in Move(...) method
				void	 Draw(CDC*);
// draws the image as it is and/or blended at the position passed in Move(...) method
// pass in the Alpha level (0..255) as the second parameter
				void	 DrawBlended(CDC*, BYTE nAlpha);
// draws the entire image "transparently" at the position passed in Move(...)
// pass in the transparent color as the second parameter
				void	 DrawMasked(CDC*, COLORREF crMask);
// draws the entire image "blended" and "transparently" at the position passed in Move(...)
// pass in the transparent color as the second parameter and
// the Alpha level (0..255) as the third parameter
				void	 DrawMaskedBlended(CDC*, COLORREF crMask, BYTE nAlpha);

// draws a part of the image "transparently" at the position passed in Move(...)
// the second parameter is the rectangle of the image to be drawn
// the rectangle must be relative to 0,0
// pass in the transparent color as the third parameter
				void	 DrawMaskedRect(CDC*, CRect&, COLORREF crMask);

// draws a part of the image blended at the position passed in Move(...)
// the second parameter is the rectangle of the image to be drawn
// the rectangle must be relative to 0,0
// the Alpha level (0..255) as the third parameter
				void	 DrawBlendedRect(CDC*, CRect&, BYTE nAlpha);

// draws a part of the image "transparently" and/or blended at the position passed in Move(...)
// the second parameter is the rectangle of the image to be drawn
// the rectangle must be relative to 0,0
// pass in the transparent color as the third parameter
// the Alpha level (0..255) as the fourth parameter
				void	 DrawMaskedBlendedRect(CDC*, CRect&, COLORREF crMask, BYTE nAlpha);

// this will reposition and redraw your image instanteneously without
// invalidating the window
				void	 DrawAt(HWND, CDC&, const CPoint&);

		const CPoint&	 GetPosNew() const { return m_ptNew; }
		const CPoint&	 GetPosOld() const { return m_ptOld; }
			   CRect	 GetSize() const;
protected:
				void	 Cleanup(bool bDelBm = true);

				void	 InitMask(COLORREF);

				void	 GetDiffRgn(CRgn&);
				void	 GetRectNew(CRect&) const;
				void	 GetRectNewVisible(CRect&) const;
				void	 GetRectOld(CRect&) const;
				void	 GetRectOldVisible(CRect&) const;
				void	 GetRectUnion(CRect&) const;
				void	 GetRectUnionVisible(CRect&) const;

				void	 CreateLogicalPalette();
protected:
// holds source bitmap's header structure
	BITMAP		m_bmHdr;
// holds source bitmap
	CBitmap*	m_pbmSource;
// the monochrome mask bitmap
	CBitmap*	m_pbmMask;
// allows you to load/save an image from a local/remote location using IStream
	IPicture*	m_pIPic;
// the new position of the bitmap (left, top)
	CPoint		m_ptNew;
// the last known position of the bitmap (left, top)
	CPoint		m_ptOld;
// keeps the last known visible rect
// this tells me the rect of the last saved background
	CRect		m_rtVisible;
// the windows on which surface this image is drawn
	HWND		m_hWnd;
// indicates that the background needs be saved before
// the first drawing to occur (done whenever a new image is loaded or attached)
	bool		m_bInitBack;
	bool		m_bVisible;
// the palette thing
	CPalette	m_palette;
// for debug outputs
	CFont		m_font;
};

#endif // !defined(AFX_MIMAGE_H__CD439035_CC81_4FF2_8016_FA5D0A19AF8C__INCLUDED_)
