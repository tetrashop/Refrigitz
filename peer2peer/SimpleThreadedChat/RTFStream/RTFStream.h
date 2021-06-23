#if ! defined( __RTFSTEAM_H__ )
#define __RTFSTEAM_H__

#if _MSC_VER >= 1000
	#pragma once
#endif // _MSC_VER >= 1000

//	The idea here is to be able to create an ostream connected to an RTF control, then dump out
//	formatted text in a simple typesafe manner.  This implementation is tied to an MDI child window
//	and the MFC framework.  That part of it shouldn't be too hard to remove though.
//
//	It can be used like this:
//
//		#include "RTFStream.h"
//
//		....
//
//		CRTFStream os;
//
//		os << rtf::bold << rtf::fontsize( 16 ) << "CRTFStream" << rtf::fontsize( 10 ) << rtf::nobold
//			<< " is a neat class for using iostream output features ";
//		os << "with a " << rtf::underline << "Windows NT/95"
//			<< rtf::nounderline << " RTF common comtrol " << endl;
//
//	We also provide a setformat function to simplify setting the stream formatting attributes
//	whilst still acting in a type safe manner.
//
//		os << rtf::roman << setformat( "%+06d" ) << 3.14159265 << endl << endl;
//
//	As far as CRTFStream is concerned endl not only outputs a new line and flushes the buffer to the window,
//	but it also acts as a reset to any of the formatting attributes that are currently in use in the control.
//
//	Guy Gascoigne - Piggford Monday, May 11, 1998

#include "StreamStuff.h"

//////////////////////////////////////////////////////////////////////

// implementation class

class CRTFStreambuf : public std::streambuf
{
public:
	CRTFStreambuf( HWND hWnd = 0, unsigned int BufferSize = 1024 );

	~CRTFStreambuf();

	virtual int overflow( int ch );		// Called when the buffer is full and the stream wants to add more stuff
	virtual int sync();					// Called whenever the stream wants to be flushed

	void DefaultFormat( const CString & s ) { m_default = s; }

	void Attach( HWND hWnd ) { pubsync(); m_hWnd = hWnd; }
protected:
	void Flush();

	TCHAR * m_pBuffer;
	unsigned int m_BufferSize;
	CString m_default;

	HWND m_hWnd;
	void SetText(  CString & str );
	CString RTFprepare( const CString & str );
};

//////////////////////////////////////////////////////////////////////
// public interface

class CRTFStream : public std::basic_ostream<TCHAR>
{
public:
	CRTFStream( HWND hWnd = 0, unsigned int BufferSize = 1024 )
		: m_buf( hWnd, BufferSize ),
		std::basic_ostream<TCHAR>( &m_buf ) { }

	~CRTFStream() { m_buf.pubsync(); }
	void DefaultFormat( const CString & s ) { m_buf.DefaultFormat( s ); }

	void Attach( HWND hWnd ) { m_buf.Attach( hWnd ); }
protected:
	CRTFStreambuf m_buf;
};

//////////////////////////////////////////////////////////////////////
// stream modification 'manipulators'

// This class exists so that I can use RFT formatting commands to any ostream,
// but becuase they all go through the operator<< I can check whether I really want
// them output to the specific ostream type.
// There is probably a better way of doing this, but so far I havn't found it.

class CRtfString : public CString
{
public:
	CRtfString( CString & s ): CString( s )
	{}
	CRtfString( LPCSTR s ) : CString( s )
	{}
};

inline std::ostream & operator<<( std::ostream & os, CRtfString s )
{
	return os << (dynamic_cast<CRTFStream*>(&os) ? (LPCSTR)s : "" );
}

namespace rtf
{
	extern CRtfString fontsize( int ptSize );
	extern const CRtfString roman, fixed;
	extern const CRtfString bold, nobold, underline, nounderline, italic, noitalic;
	extern const CRtfString black, maroon, green, olive, navy, purple, teal, grey, silver, red, lime, yellow, blue, fuchsia, aqua, white;
	extern const CRtfString bullet;
};

#endif //__RTFSTEAM_H__
