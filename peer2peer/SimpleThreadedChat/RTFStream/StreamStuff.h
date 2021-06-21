#if ! defined( __STREAMSTUFF_H__ )
#define __STREAMSTUFF_H__

#if _MSC_VER >= 1000
	#pragma once
#endif // _MSC_VER >= 1000

#include <strstream>
#include <iomanip>
#include <ostream>
#include <fstream>

inline std::ostream & operator<<(std::ostream & os, const CString& s ) { return os << (LPCTSTR)s; }

// I can't believe that there isn't a 'standard' implementaion of this class.

template<class T> class omanip
{  
public:
	omanip(std::ostream& (*a)(std::ostream&,T), T v)
		: _action(a), _value(v) {}
	friend std::ostream& operator<<(std::ostream&, const omanip<T>&);
private:
	std::ostream& (*_action)(std::ostream&,T);
	T _value;
};

template<class T> std::ostream& operator<<(std::ostream& s, const omanip<T>& m)
{
	return (*m._action)(s, m._value);
}

// Now actually use it to export the setformat manipulator.
omanip<const char*> setformat( const char* fmt );

CString DottedString( int length );

inline LPCSTR safeStr( LPCSTR s ) { return s ? s : ""; }

inline std::ostream & operator<<(std::ostream & os, SIZE size)
{
	return os << "(" << size.cx << " x " << size.cy << ")";
}

inline std::ostream & operator<<(std::ostream & os, POINT point)
{
	return os << "(" << point.x << ", " << point.y << ")";
}

inline std::ostream & operator<<(std::ostream & os, const RECT& rect)
{
	return os << "(L " << rect.left << ", T " << rect.top << ", R " <<
		rect.right << ", B " << rect.bottom << ")";
}

#endif //__STREAMSTUFF_H__
