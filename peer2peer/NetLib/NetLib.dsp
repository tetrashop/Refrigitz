# Microsoft Developer Studio Project File - Name="NetLib" - Package Owner=<4>
# Microsoft Developer Studio Generated Build File, Format Version 6.00
# ** DO NOT EDIT **

# TARGTYPE "Win32 (x86) Static Library" 0x0104

CFG=NetLib - Win32 Debug
!MESSAGE This is not a valid makefile. To build this project using NMAKE,
!MESSAGE use the Export Makefile command and run
!MESSAGE 
!MESSAGE NMAKE /f "NetLib.mak".
!MESSAGE 
!MESSAGE You can specify a configuration when running NMAKE
!MESSAGE by defining the macro CFG on the command line. For example:
!MESSAGE 
!MESSAGE NMAKE /f "NetLib.mak" CFG="NetLib - Win32 Debug"
!MESSAGE 
!MESSAGE Possible choices for configuration are:
!MESSAGE 
!MESSAGE "NetLib - Win32 Release" (based on "Win32 (x86) Static Library")
!MESSAGE "NetLib - Win32 Debug" (based on "Win32 (x86) Static Library")
!MESSAGE 

# Begin Project
# PROP AllowPerConfigDependencies 0
# PROP Scc_ProjName ""
# PROP Scc_LocalPath ""
CPP=cl.exe
RSC=rc.exe

!IF  "$(CFG)" == "NetLib - Win32 Release"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 0
# PROP BASE Output_Dir "Release"
# PROP BASE Intermediate_Dir "Release"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 0
# PROP Output_Dir "Release"
# PROP Intermediate_Dir "Release"
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /GX /O2 /D "WIN32" /D "NDEBUG" /D "_MBCS" /D "_LIB" /YX /FD /c
# ADD CPP /nologo /MD /W3 /GR /GX /O2 /I "Threads" /I "Notifications" /I "." /D "WIN32" /D "NDEBUG" /D "_MBCS" /D "_LIB" /Yu"stdafx.h" /FD /c
# ADD BASE RSC /l 0x409 /d "NDEBUG"
# ADD RSC /l 0x409 /d "NDEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LIB32=link.exe -lib
# ADD BASE LIB32 /nologo
# ADD LIB32 /nologo /out:"../Bin/Release\NetLib.lib"

!ELSEIF  "$(CFG)" == "NetLib - Win32 Debug"

# PROP BASE Use_MFC 0
# PROP BASE Use_Debug_Libraries 1
# PROP BASE Output_Dir "Debug"
# PROP BASE Intermediate_Dir "Debug"
# PROP BASE Target_Dir ""
# PROP Use_MFC 0
# PROP Use_Debug_Libraries 1
# PROP Output_Dir "Debug"
# PROP Intermediate_Dir "Debug"
# PROP Target_Dir ""
# ADD BASE CPP /nologo /W3 /Gm /GX /ZI /Od /D "WIN32" /D "_DEBUG" /D "_MBCS" /D "_LIB" /YX /FD /GZ /c
# ADD CPP /nologo /MDd /W3 /Gm /GR /GX /ZI /Od /I "Threads" /I "Notifications" /I "." /D "WIN32" /D "_DEBUG" /D "_MBCS" /D "_LIB" /Yu"stdafx.h" /FD /GZ /c
# ADD BASE RSC /l 0x409 /d "_DEBUG"
# ADD RSC /l 0x409 /d "_DEBUG"
BSC32=bscmake.exe
# ADD BASE BSC32 /nologo
# ADD BSC32 /nologo
LIB32=link.exe -lib
# ADD BASE LIB32 /nologo
# ADD LIB32 /nologo /out:"../Bin/Debug\NetLib.lib"

!ENDIF 

# Begin Target

# Name "NetLib - Win32 Release"
# Name "NetLib - Win32 Debug"
# Begin Group "Source Files"

# PROP Default_Filter "cpp;c;cxx;rc;def;r;odl;idl;hpj;bat"
# Begin Source File

SOURCE=.\Threads\AcceptorThread.cpp
# End Source File
# Begin Source File

SOURCE=.\Threads\FileAcceptorThread.cpp
# End Source File
# Begin Source File

SOURCE=.\Mutex.cpp
# End Source File
# Begin Source File

SOURCE=.\NetAddress.cpp
# End Source File
# Begin Source File

SOURCE=.\NetAddressScanner.cpp
# End Source File
# Begin Source File

SOURCE=.\NetConnection.cpp
# End Source File
# Begin Source File

SOURCE=.\NetException.cpp
# End Source File
# Begin Source File

SOURCE=.\NetFile.cpp
# End Source File
# Begin Source File

SOURCE=.\NetFileAddress.cpp
# End Source File
# Begin Source File

SOURCE=.\NetFileConnector.cpp
# End Source File
# Begin Source File

SOURCE=.\NetFileHdr.cpp
# End Source File
# Begin Source File

SOURCE=.\NetFileInfo.cpp
# End Source File
# Begin Source File

SOURCE=.\NetIPAddress.cpp
# End Source File
# Begin Source File

SOURCE=.\NetMessage.cpp
# End Source File
# Begin Source File

SOURCE=.\Notifications\NetNotif.cpp
# End Source File
# Begin Source File

SOURCE=.\NetPortScanner.cpp
# End Source File
# Begin Source File

SOURCE=.\NetSockAcceptor.cpp
# End Source File
# Begin Source File

SOURCE=.\NetSockConnection.cpp
# End Source File
# Begin Source File

SOURCE=.\NetSockConnector.cpp
# End Source File
# Begin Source File

SOURCE=.\NetStream.cpp
# End Source File
# Begin Source File

SOURCE=.\Threads\NetThread.cpp
# End Source File
# Begin Source File

SOURCE=.\Notifications\Notification.cpp
# End Source File
# Begin Source File

SOURCE=.\ParamsParser.cpp
# End Source File
# Begin Source File

SOURCE=.\ParamsScanner.cpp
# End Source File
# Begin Source File

SOURCE=.\Threads\ReceiverThread.cpp
# End Source File
# Begin Source File

SOURCE=.\SafeVector.cpp
# PROP Exclude_From_Build 1
# End Source File
# Begin Source File

SOURCE=.\Threads\SenderThread.cpp
# End Source File
# Begin Source File

SOURCE=.\Threads\SendReceiveThread.cpp
# End Source File
# Begin Source File

SOURCE=.\stdafx.cpp
# ADD CPP /Yc"stdafx.h"
# End Source File
# Begin Source File

SOURCE=.\stdNetAPI.cpp
# End Source File
# Begin Source File

SOURCE=.\ThreadStorage.cpp
# End Source File
# End Group
# Begin Group "Header Files"

# PROP Default_Filter "h;hpp;hxx;hm;inl"
# Begin Source File

SOURCE=.\Threads\AcceptorThread.h
# End Source File
# Begin Source File

SOURCE=.\Threads\FileAcceptorThread.h
# End Source File
# Begin Source File

SOURCE=.\Mutex.h
# End Source File
# Begin Source File

SOURCE=.\NetAddress.h
# End Source File
# Begin Source File

SOURCE=.\NetAddressScanner.h
# End Source File
# Begin Source File

SOURCE=.\NetConnection.h
# End Source File
# Begin Source File

SOURCE=.\NetException.h
# End Source File
# Begin Source File

SOURCE=.\NetFile.h
# End Source File
# Begin Source File

SOURCE=.\NetFileAddress.h
# End Source File
# Begin Source File

SOURCE=.\NetFileConnector.h
# End Source File
# Begin Source File

SOURCE=.\NetFileHdr.h
# End Source File
# Begin Source File

SOURCE=.\NetFileInfo.h
# End Source File
# Begin Source File

SOURCE=.\NetIPAddress.h
# End Source File
# Begin Source File

SOURCE=.\NetMessage.h
# End Source File
# Begin Source File

SOURCE=.\Notifications\NetNotif.h
# End Source File
# Begin Source File

SOURCE=.\NetPortScanner.h
# End Source File
# Begin Source File

SOURCE=.\NetSockAcceptor.h
# End Source File
# Begin Source File

SOURCE=.\NetSockConnection.h
# End Source File
# Begin Source File

SOURCE=.\NetSockConnector.h
# End Source File
# Begin Source File

SOURCE=.\NetStream.h
# End Source File
# Begin Source File

SOURCE=.\Threads\NetThread.h
# End Source File
# Begin Source File

SOURCE=.\Notifications\Notification.h
# End Source File
# Begin Source File

SOURCE=.\ParamsParser.h
# End Source File
# Begin Source File

SOURCE=.\ParamsScanner.h
# End Source File
# Begin Source File

SOURCE=.\Threads\ReceiverThread.h
# End Source File
# Begin Source File

SOURCE=.\SafeVector.h
# End Source File
# Begin Source File

SOURCE=.\Threads\SenderThread.h
# End Source File
# Begin Source File

SOURCE=.\Threads\SendReceiveThread.h
# End Source File
# Begin Source File

SOURCE=.\stdafx.h
# End Source File
# Begin Source File

SOURCE=.\stdNetAPI.h
# End Source File
# Begin Source File

SOURCE=.\ThreadStorage.h
# End Source File
# End Group
# End Target
# End Project
