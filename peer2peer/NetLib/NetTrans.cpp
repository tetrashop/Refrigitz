// NetTrans.cpp: implementation of the CNetTrans class.
//
//////////////////////////////////////////////////////////////////////

#include "stdafx.h"
#include "NetTrans.h"

//////////////////////////////////////////////////////////////////////
// Construction/Destruction
//////////////////////////////////////////////////////////////////////

void CNetStartTrans::Dump(ostream& os)
{
  os << endl << "Start Dump for CNetStartTran:" << endl << endl;
  os << endl << "End Dump for CNetStartTran:" << endl << endl;
}

void CNetCommitTrans::Dump(ostream& os)
{
  os << endl << "Start Dump for CNetCommitTrans:" << endl << endl;
  os << endl << "End Dump for CNetCommitTrans:" << endl << endl;
}

void CNetAbortTrans::Dump(ostream& os)
{
  os << endl << "Start Dump for CNetAbortTrans:" << endl << endl;
  os << endl << "End Dump for CNetAbortTrans:" << endl << endl;
}