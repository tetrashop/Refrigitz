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

// in:  an IP address in format like "127.0.0.1"
// out: a ulong containing this address 
ulong  g_addr_stu(const char* strAddr)
{
  ulong ip[4], one;
// parse out the digits
  sscanf_s(strAddr,"%d.%d.%d.%d",&ip[0],&ip[1],&ip[2],&ip[3]);
// save the digits in one ulong
  one = (((((ip[0] << 8) + ip[1]) << 8) + ip[2]) << 8) + ip[3];
  return one;
}

// in:  a ulong containing an IP address 
// (0..7 bit [value 1], 8..15 bit [value 0], 16..23 bit [value 0], 24..31 bit [value 127])
// out: a string in format like "127.0.0.1"
string  g_addr_uts(const ulong one)
{
  string strIP;
  char buff[20]; // should be enough
// extract the digits from 'one' ulong into a string buffer
  //sprintf_s(buff, "%u.%u.%u.%u", one >> 24, one >> 16, one >> 8); // ?? & 0xFF
  sprintf_s(buff, "%u.%u.%u.%u",one>>31, one >> 24, one >> 16, one >> 8); // ?? & 0xFF
  strIP = buff;
  return strIP;
}
