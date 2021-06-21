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

#include "NetMessage.h"

class CNetLogin : public CNetStatus
{
public:
        struct MSG {
          ulong                 m_nNameSz;
          string                m_strName;
        };

public:
                                        CNetLogin();
                                        CNetLogin(ulong, ulong, CNetStatus::MSG&,  MSG&);
        virtual                         ~CNetLogin();

        virtual              CNetMsg*   Clone() const {return new CNetLogin;}

        virtual            CNetObject&  operator=(const CNetObject& obj);
        virtual                  bool   operator==(const CNetObject& obj);


// returns the class id as defined in the "NetMsgNames.h" file
                                ulong   GetClassId() const {return ciNetLogin;}
// returns the size in bytes of members of this message that can be sent accross the network
        virtual                 ulong   GetSize_Write() const;

// dumps the data into the ostream
                                void    Dump(ostream&);
protected:
// reads the message members from m_pData
                                void    ReadBody();
// reads the message members from the prototype
                                void    ReadBody(const CNetMsg&);
// write the message members into m_pData
                                void    WriteBody();

public:
                                void    SetName(const char* name);

                          const char*   GetName() const {return m_data.m_strName.c_str();}
protected:
  MSG   m_data;
};

class CNetText : public CNetMsg
{
public:
        struct MSG {
          ulong  m_nSize;
          string m_strText;
        };

public:
                                                        CNetText();
                                                        CNetText(ulong, ulong, MSG);
                                                        CNetText(const char*);

        virtual                                 ~CNetText() {}

        virtual              CNetMsg*   Clone() const {return new CNetText;}

        virtual            CNetObject&  operator=(const CNetObject& obj);
        virtual                  bool   operator==(const CNetObject& obj);

		virtual                  bool   HasChildren() const {return false;}

                        const MSG&              GetMSG() const {return m_data;}

// returns the class id as defined in the "NetMsgNames.h" file
                                ulong           GetClassId() const {return ciNetText;}
// returns the size in bytes of members of this message that can be sent accross the network
                                ulong           GetSize_Write() const;
// dumps the data into the ostream
                                void            Dump(ostream&);
protected:
// reads the message members from m_pData
                                void            ReadBody();
// reads the message members from the prototype
                                void            ReadBody(const CNetMsg&);
// write the message members into m_pData
                        void                    WriteBody();
public:
                        void                    SetText(const char*);
                        const char*             GetText() {return m_data.m_strText.c_str();}

protected:
        MSG     m_data;
};



