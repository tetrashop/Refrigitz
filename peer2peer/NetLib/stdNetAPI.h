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

#ifdef _WIN32
  #pragma warning(disable:4786)
#endif

#include <vector>
#include <list>
#include <string>
#include <map>
#include <errno.h>

#ifdef _WIN32
 #include <iostream>
  #include <fstream> 
  #include <winsock2.h>
  #include <process.h>
  #include <typeinfo>
  using namespace std;
#else
  #include <iostream.h>
  #include <fstream.h>  
  
  #include <sys/types.h>
  #include <sys/socket.h>
  #include <netinet/in.h>
  #include <unistd.h>
  #include <netdb.h>
  #include <arpa/inet.h>
#endif

#define UWM_ON_ACCEPT					WM_APP + 100
#define UWM_ON_LISTENING				WM_APP + 101
#define UWM_ON_ACCEPT_ERROR				WM_APP + 102
#define UWM_ON_FILE						WM_APP + 103
#define UWM_ON_FILE_SAVE_AS				WM_APP + 104
#define UWM_ON_FILE_PROGRESS_SEND		WM_APP + 105
#define UWM_ON_FILE_DONE_SEND			WM_APP + 106
#define UWM_ON_FILE_REJECT				WM_APP + 107
#define UWM_ON_FILE_ERROR_SEND			WM_APP + 108
#define UWM_ON_FILE_ABORT_SEND			WM_APP + 109

#define UWM_ON_ADDR_PORT_AVAILABLE		WM_APP + 110
#define UWM_ON_ADDR_PORT_UNAVAILABLE	WM_APP + 111
#define UWM_ON_NO_ADDR_PORT_AVAILABLE	WM_APP + 112
#define UWM_ON_ACCEPT_FOR_FILE			WM_APP + 113

#define UWM_ON_FILE_DONE_RECEIVE		WM_APP + 114
#define UWM_ON_FILE_ERROR_RECEIVE		WM_APP + 115
#define UWM_ON_FILE_PROGRESS_RECEIVE	WM_APP + 116
#define UWM_ON_FILE_ABORT_RECEIVE		WM_APP + 117
#define UWM_ON_FILE_RECEIVED_HEADER_FILE WM_APP + 118
#define UWM_ON_FILE_RECEIVE_REJECT		WM_APP + 119
// testing
#define UWM_TEST_MESSAGE				WM_APP + 120

// defines the signature, which is put in the head of all net messages data
// to indicate that the message has come from an authentic source
#define SIGNATURE_SIZE 5
#define NET_SIGNATURE "NA|00" 

// network transmit progress call back function type
typedef void (*PROGRESS_CB)(void*, void*);

// NA is descriptor;
// | is a separator; 
// 0 is a version number

#define _DELETE(p) { \
  if (p) delete p; \
  p = 0; \
}

#ifndef _WIN32
  #define SOCKET_ERROR (-1)
  #define SOCKET           long
// this is used instead of -1, since the SOCKET type is unsigned.
  #define INVALID_SOCKET  (SOCKET)(~0)
#endif

typedef unsigned long ulong;
typedef unsigned short ushort;

class CThreadStorage;

extern          ulong      g_addr_stu(const char* strAddr);
extern         string      g_addr_uts(const ulong one);
extern CThreadStorage&     g_getThreadStorage();

enum classCommonIds{
  ciNetList             =  0,
  ciNetPacket           =  1,
  ciNetStatus           =  2,
  ciNetFileHdr          =  3,
  classCommonIdsNext    =  4
};

enum netCommonStatus {
  nsPending = 0,
  nsAccept = 1,
  nsReject = 2,
  nsPathSet = 3,
  nsCommonIdsNext = 4
};

#define MAX_NET_PACKET 1024

#include "NetException.h"
