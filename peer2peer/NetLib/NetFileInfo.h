// NetFileInfo.h: interface for the CNetFileInfo class.
//
// Written by Marat Bedretdinov (maratb@hotmail.com)
// Copyright (c) 2000.
//
// This code may be used in compiled form in any way you desire. This
// file may be redistributed unmodified by any means PROVIDING it is 
// not sold for profit without the authors written consent, and 
// providing that this notice and the authors name is included. 
//
// This file is provided "as is" with no expressed or implied warranty.
// The author accepts no liability if it causes any damage whatsoever.
// It's free - so you get what you pay for.//

#if !defined(AFX_NetFileInfo_H__9E2BE869_1B09_4DC5_B839_5A4C97A9D183__INCLUDED_)
#define AFX_NetFileInfo_H__9E2BE869_1B09_4DC5_B839_5A4C97A9D183__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

class CNetFileInfo
{
public:
							CNetFileInfo(const char* path, const char* filter);
	virtual					~CNetFileInfo();

					void	Cleanup();

					void	FindChildren();
			CNetFileInfo*	FindChild(const char*);

					void	SetDigDeep(bool b)
								{ m_bDigDeep = b; }

					bool	IsDigged() const
								{ return m_bDigged; }

			const char*		GetName() const 
								{ return m_fdata.cFileName; }
			const char*		GetPath() const
								{ return m_path.c_str(); }

		   unsigned long	GetChildrenN() const
								{ return m_vecFiles.size(); }

	         CNetFileInfo*	    GetChild(unsigned long n)
								{ if (m_vecFiles.size() > 0 && n > m_vecFiles.size()-1)
								    return 0; 
								  return m_vecFiles[n]; }

		   unsigned long	GetDepth() const
								{ return m_nDepth; }

					bool	IsDirectory() const
								{ return m_fdata.dwFileAttributes == FILE_ATTRIBUTE_DIRECTORY; }

					bool	HasChildren() const 
								{ return m_bHasChildren; }
protected:								
							CNetFileInfo(const char* path, const char* filter, const WIN32_FIND_DATA&, CNetFileInfo*, unsigned long nDepth);
protected:
	vector<CNetFileInfo*>	m_vecFiles;
	WIN32_FIND_DATA			m_fdata;
	string					m_path;
	string					m_filter;
	bool					m_bDigDeep;
	bool					m_bDigged;
	bool					m_bHasChildren;
	unsigned long			m_nDepth;
};

#endif // !defined(AFX_NetFileInfo_H__9E2BE869_1B09_4DC5_B839_5A4C97A9D183__INCLUDED_)
