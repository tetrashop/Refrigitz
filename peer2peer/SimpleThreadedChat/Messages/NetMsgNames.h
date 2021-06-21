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

#ifndef __NETMSGNAMES_INCLUDE__
#define __NETMSGNAMES_INCLUDE__

// defines net message class ids
enum classIds{
  ciNetText             =  classCommonIdsNext,
  ciNetLogin            =  classCommonIdsNext + 2,
  ciNetMsgStatus        =  classCommonIdsNext + 3
};

// defines net message statuses
enum netStatus {
  nsLogin        = nsCommonIdsNext,
  nsLogout       = nsCommonIdsNext + 1,
  nsNickChange   = nsCommonIdsNext + 2,
  nsAcceptAddr   = nsCommonIdsNext + 3,
  nsUserTypesMsg = nsCommonIdsNext + 4,
  nsUserDoesNtng = nsCommonIdsNext + 5,
  nsUserIsHere   = nsCommonIdsNext + 6
};

#endif // #define __NETMSGNAMES_INCLUDE__
