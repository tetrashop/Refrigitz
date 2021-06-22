#include "stdafx.h"
#include "RTFStream.h"

#ifdef _DEBUG
#undef THIS_FILE
static char THIS_FILE[]=__FILE__;
#define new DEBUG_NEW
#endif

using std::streambuf;

namespace rtf
{
	//	To avoid issues with multiple quoting and conversion of RTF characters
	//	we use \001 as a prefix/escape character
	//	There is a very tight link between the entries here and the font & color tables 
	//	initialised in the prefix.

	// Attributes
	const CRtfString bold(		_T( "\001\\b " ) );
	const CRtfString nobold(	_T( "\001\\b0 " ) );
	const CRtfString underline(_T( "\001\\ul " ) );
	const CRtfString nounderline(_T( "\001\\ul0 " ) );
	const CRtfString italic(	_T( "\001\\i " ) );
	const CRtfString noitalic(	_T( "\001\\i0 " ) );

	// Fonts
	const CRtfString roman(	_T( "\001\\plain\001\\f2\001\\fs20 " ) );
	const CRtfString fixed(	_T( "\001\\plain\001\\f5\001\\fs20 " ) );

	// Colours
	const CRtfString black(	_T( "\001\\cf0 " ) );
	const CRtfString maroon(	_T( "\001\\cf1 " ) );
	const CRtfString green(	_T( "\001\\cf2 " ) );
	const CRtfString olive(	_T( "\001\\cf3 " ) );
	const CRtfString navy(		_T( "\001\\cf7 " ) );
	const CRtfString purple(	_T( "\001\\cf4 " ) );
	const CRtfString teal(		_T( "\001\\cf5 " ) );
	const CRtfString grey(		_T( "\001\\cf6 " ) );
	const CRtfString silver(	_T( "\001\\cf11 " ) );
	const CRtfString red(		_T( "\001\\cf8 " ) );
	const CRtfString lime(		_T( "\001\\cf9 " ) );
	const CRtfString yellow(	_T( "\001\\cf10 " ) );
	const CRtfString blue(		_T( "\001\\cf15 " ) );
	const CRtfString fuchsia(	_T( "\001\\cf12 " ) );
	const CRtfString aqua(		_T( "\001\\cf13 " ) );
	const CRtfString white(	_T( "\001\\cf14 " ) );

	// Other
	const CRtfString bullet(	_T( "\001{\001\\pntext\001\\f1\001\\'b7\001\\tab\001}" ) );

	CRtfString fontsize( int ptSize )
	{
		CString str;
		str.Format( _T( "\001\\fs%d" ), ptSize * 2 );
		return str;
	}

	// not generally accessible outside this package
	extern const CString Prefix =
		_T( "{\\rtf1\\ansi\\deff0\\deftab720\n" )
		_T( "{\\fonttbl" )
		_T( "{\\f0\\fswiss MS Sans Serif;}" )
		_T( "{\\f1\\froman\\fcharset2 Symbol;}" )
		_T( "{\\f2\\froman Times New Roman;}" )
		_T( "{\\f3\\froman Times New Roman;}" )
		_T( "{\\f4\\fswiss\\fprq2 MS Sans Serif;}" )
		_T( "{\\f5\\fmodern\\fprq1 Courier New;}" )
		_T( "{\\f6\\froman\\fprq2 Times New Roman;}}" )
		_T( "\n" )
		_T( "{\\colortbl" )
		_T( "\\red0\\green0\\blue0;" )
		_T( "\\red128\\green0\\blue0;" )
		_T( "\\red0\\green128\\blue0;" )
		_T( "\\red128\\green128\\blue0;" )
		_T( "\\red128\\green0\\blue128;" )
		_T( "\\red0\\green128\\blue128;" )
		_T( "\\red128\\green128\\blue128;" )
		_T( "\\red0\\green0\\blue128;" )
		_T( "\\red255\\green0\\blue0;" )
		_T( "\\red0\\green255\\blue0;" )
		_T( "\\red255\\green255\\blue0;" )
		_T( "\\red192\\green192\\blue192;" )
		_T( "\\red255\\green0\\blue255;" )
		_T( "\\red0\\green255\\blue255;" )
		_T( "\\red255\\green255\\blue255;" )
		_T( "\\red0\\green0\\blue255;}\n" )
		_T( "\\pard\\plain\\f2\\fs20" );

	extern const CString Postfix = _T( "\n\\par }" );
};

////////////////////////////////////////////////////////////////////////////////////////////////////////////

CRTFStreambuf::CRTFStreambuf( HWND hWnd /* = 0 */, unsigned int BufferSize /* = 1024 */ )
	: m_BufferSize( BufferSize ),
	m_hWnd( hWnd )
{
	m_pBuffer = new TCHAR[m_BufferSize + 1];
	setp( m_pBuffer, m_pBuffer + m_BufferSize );
}

CRTFStreambuf::~CRTFStreambuf()
{
	delete [] m_pBuffer;
}

int CRTFStreambuf::overflow( int ch )
{
	Flush();
	if (ch != EOF)
		if (pbase() == epptr())
		{
			CString msg;
			msg.Format("%d", ch);
			SetText(msg);
		}

		else        
			sputc(ch); 
	return streambuf::overflow( ch );
}

int CRTFStreambuf::sync()
{
	ASSERT( this );
	Flush();
	return streambuf::sync();
}

void CRTFStreambuf::Flush()
{
	ASSERT( this );
	*pptr() = '\0';         // NULL terminate
	SetText((CString) m_pBuffer );
	setp( m_pBuffer, m_pBuffer + m_BufferSize );
	setg( 0, 0, 0);
}

static DWORD CALLBACK EditStreamCallBack(DWORD dwCookie, LPBYTE pbBuff, LONG cb, LONG *pcb)
{
	// NOTE: pstr is NOT const
	CString *pstr = (CString *)dwCookie;

	if( pstr->GetLength() < cb )
	{
		*pcb = pstr->GetLength();
		memcpy(pbBuff, (LPCSTR)*pstr, *pcb );
		pstr->Empty();
	}
	else
	{
		*pcb = cb;
		memcpy(pbBuff, (LPCSTR)*pstr, *pcb );
		*pstr = pstr->Right( pstr->GetLength() - cb );
	}
	return 0;
}

CString CRTFStreambuf::RTFprepare( const CString & str )
{
	CString ret;
	int size = str.GetLength();
	for ( int i = 0; i < size; i++ )
	{
		switch ( str[i] )
		{
			case '\001':
				if ( i < size -1 )
				{
					i++;
					ret += str[i];
				}
				break;
			case '\n':
				ret += "\n\\par ";
				break;
			case '\r':
				break;
			case '\t':
				ret += "\\tab ";
				break;
			case '\\':
				ret += "\\\\";
				break;
			case '{':
				ret += "\\{";
				break;
			case '}':
				ret += "\\}";
				break;
			default:
				ret += str[i];
				break;
		}
	}
	return ret;
}

void CRTFStreambuf::SetText(  CString & str )
{
	if ( m_hWnd )
	{
		if ( ::IsWindow( m_hWnd ) )
		{
			CString rtfString = rtf::Prefix + RTFprepare(m_default + str) + rtf::Postfix;
			EDITSTREAM es = {(DWORD)&rtfString, 0, EditStreamCallBack};
			::SendMessage( m_hWnd, EM_SETSEL, -1, -1 );
			::SendMessage( m_hWnd, EM_STREAMIN, SF_RTF | SFF_SELECTION, (LPARAM)&es);
		}
	}
}

