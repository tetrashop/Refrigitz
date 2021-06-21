// NetTrans.h: interface for the CNetTrans class.
//
//////////////////////////////////////////////////////////////////////

#if !defined(AFX_NETTRANS_H__79992E87_55E3_4469_B2F7_A47A95624AD8__INCLUDED_)
#define AFX_NETTRANS_H__79992E87_55E3_4469_B2F7_A47A95624AD8__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

#include "NetMessage.h"

class CNetTrans : public CNetMsg  
{
public:
                                                        
	                                    CNetTrans() {}
        virtual                        ~CNetTrans() {}

        virtual              CNetMsg*   Clone() const = 0;

		virtual                  bool   HasChildren() const {return false;}

                                ulong   GetClassId() const = 0;
// dumps the data into the ostream
        virtual                  void   Dump(ostream&) = 0;
};

class CNetStartTran : public CNetTrans  
{
public:
	                                    CNetStartTran() {}
        virtual                        ~CNetStartTran() {}

        virtual              CNetMsg*   Clone() const { return new CNetStartTran; }

		virtual                  bool   HasChildren() const {return false;}

		                        ulong   GetClassId() const { return ciNetStartTran; }
// dumps the data into the ostream
        virtual                  void    Dump(ostream&);
};

class CNetCommitTran : public CNetTrans  
{
public:
	                                    CNetCommitTran() {}
        virtual                        ~CNetCommitTran() {}

        virtual              CNetMsg*   Clone() const { return new CNetCommitTran; }

		virtual                  bool   HasChildren() const {return false;}

		                        ulong   GetClassId() const { return ciNetCommitTran; }
// dumps the data into the ostream
        virtual                  void    Dump(ostream&);
};

class CNetAbortTran : public CNetTrans  
{
public:
	                                    CNetAbortTran() {}
        virtual                        ~CNetAbortTran() {}

        virtual              CNetMsg*   Clone() const { return new CNetAbortTran; }

		virtual                  bool   HasChildren() const {return false;}

		                        ulong   GetClassId() const { return ciNetAbortTran; }
// dumps the data into the ostream
        virtual                  void    Dump(ostream&);
};

#endif // !defined(AFX_NETTRANS_H__79992E87_55E3_4469_B2F7_A47A95624AD8__INCLUDED_)
