// NetMsg.h: interface for the CNetMsg class.
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

#if !defined(AFX_NETMsg_H__C4F3191C_99C0_454A_BFAB_CE57E3CAC7A9__INCLUDED_)
#define AFX_NETMsg_H__C4F3191C_99C0_454A_BFAB_CE57E3CAC7A9__INCLUDED_

#if _MSC_VER > 1000
#pragma once
#endif // _MSC_VER > 1000

// base root class
class CRootObject
{
public:
                                      CRootObject() {}
        virtual                       ~CRootObject() {}
};

// base class that can be sent across a network
class CNetObject : public CRootObject
{
public:
// resets member variables 
                                       CNetObject();
// resets member variables and sets m_pData
//                                       CNetObject(void*);

// resets member variables (m_pData is not destoryed)
        virtual                                 ~CNetObject();
// returns the offset of the last read from m_pData
                        long           GetSeekReadPos() const {return m_nSeekRead;}
// returns the offset of the last write into m_pData
                        long           GetSeekWritePos() const {return m_nSeekWrite;}
// returns true if m_pData is nil
                        bool           IsBufferEmpty() const;

// returns a value of type 
// - ulong (unsigned long)
// - ushort (unsigned short) 
// - char*
// read from m_pData, starting from m_nSeekRead postition 
                        bool           Get(ulong&);
                        bool           Get(ushort&);
						bool           Get(bool&);
                        bool           Get(string&, ulong);
						bool           Get(char*, ulong);

// writes a value of type 
// - ulong (unsigned long)
// - ushort (unsigned short) 
// - char*
// into m_pData, starting from m_nSeekWrite postition (m_pData is preallocated)
                        bool           Put(const ulong);
                        bool           Put(const ushort);
						bool           Put(const bool);
                        bool           Put(const string&, ulong);
						bool           Put(const char*, ulong);

// returns the size of the (char*)m_pData
        virtual         ulong          GetSize_Write() const {return 0;}
        virtual         ulong          GetSize_Read() const {return 0;}


// copies all member variables from obj into 'this'
  virtual        CNetObject&            operator=(const CNetObject& obj);
  virtual               bool            operator==(const CNetObject& obj);
// resets member variables (destroys m_pData)
 virtual                void           EmptyBuffer();
protected:
// allocates a chunk of memory the size of GetSize() 
// if m_pData already held a data buffer, it is destroyed
                       void*           AllocateData(const ulong nSz = 0);
                 const void*           GetData() {return m_pData;}
// returns the size in bytes for each supported data type
// this may differ from platform to platform
   virtual           ushort            GetSize_ulong()  const  {return 4;}
   virtual           ushort            GetSize_int()    const  {return 4;}

   virtual           ushort            GetSize_ushort() const  {return 2;}
   virtual           ushort            GetSize_bool()   const  {return 1;}
   virtual           ushort            GetSize_char()   const  {return 1;}

// before a Get(...) operation is called this method checks if there
// is data ahead for the length the Get(...) opeartion is about to read
                       bool            CheckReadBound(ulong) const;

// before a Put(...) operation is called this method checks if m_pData
// can hold more data of the length the Put(...) opeartion is about to write
                       bool            CheckWriteBound(ulong) const;

// dumps the data into the ostream
        virtual        void            Dump(ostream&) = 0;
protected:
        void*   m_pData;
// seek points to the byte + 1 in m_pData after the last Get(...) call
        ulong   m_nSeekRead;
// seek points to the byte + 1 in m_pData after the last Put(...) call
        ulong   m_nSeekWrite;
};

class CNetStream;
class CNetMsg;

// the header of the message class
class CNetMsgHdr : public CNetObject
{
friend class CNetMsg;
friend class CNetStream;
public:
        struct MSG {
          string        m_strSignature;
          ulong         m_nHost;
          ulong         m_nRemote;
          ulong         m_nClassId;
          ulong         m_nMsgSize;
        };
protected:
                                void    SetHost(const char*);
                                void    SetRemote(const char*);
                                void    SetHost(ulong);
                                void    SetRemote(ulong);
                                void    SetClassId(ulong id) {m_data.m_nClassId = id;}
                                void    SetMsgSize(ulong sz) {m_data.m_nMsgSize = sz;}
public:
                        const string    GetHost() const;
                        const string    GetRemote() const;
public:
                                        CNetMsgHdr();
										CNetMsgHdr(const CNetMsgHdr&);
        virtual                        ~CNetMsgHdr();

        virtual           CNetObject&   operator=(const CNetObject& obj);
        virtual                 bool    operator==(const CNetObject& obj);

                        const    MSG&   GetMSG() const {return m_data;}


                               ulong    GetSize_Read() const { return GetSize(); }
                               ulong    GetSize_Write() const { return GetSize(); }

// returns the size in bytes of members of this message that can be sent accross the network
         					   ulong    GetSize() const { return GetSize_ulong() * 4 + SIGNATURE_SIZE; }
                               ulong    GetMsgSize() const {return m_data.m_nMsgSize;}

                                void    Dump(ostream&);
protected:
                                void    Read();
                                void    Write();
protected:
  MSG m_data;
};

// the message class
class CNetMsg : public CNetObject
{
friend class CNetStream;
friend class CNetMsgHdr;
public:
                                void    SetHost(const char* addr) {m_header.SetHost(addr);}
                                void    SetRemote(const char* addr) {m_header.SetRemote(addr);}
                                void    SetHost(ulong addr) {m_header.SetHost(addr);}
                                void    SetRemote(ulong addr) {m_header.SetRemote(addr);}
public:
                                        CNetMsg();

                                        CNetMsg(const CNetMsg&);

        virtual              CNetMsg*   Clone() const {throw; return 0;}

// resets member variables (destroys m_pData)
        virtual                        ~CNetMsg();

        virtual           CNetObject&   operator=(const CNetObject& obj);
        virtual                  bool   operator==(const CNetObject& obj);

        virtual            CNetMsgHdr*  GetMsgHdr() {return &m_header;}

// if this is a complex class (aggregates other CNetMsg derivables) return true
		virtual                  bool   HasChildren() const = 0;

// returns the size in bytes of members of this message that can be sent accross the network
                                ulong   GetSize_Write() const;
                                ulong   GetSize_Read() const;

// returns the class id as defined in the "NetMsgNames.h" file
        virtual                 ulong   GetClassId() const {throw; return 0;}
                                ulong   GetHostId() const {return m_header.GetMSG().m_nHost;}
                                ulong   GetRemoteId() const {return m_header.GetMSG().m_nRemote;}
        
// resets CNetObject's member variables (destroys m_pData of the message body and the header)
        virtual                  void   EmptyBuffer();

protected:
// reads the message members from m_pData
        virtual                  void   ReadBody();
// reads m_header from CNetStream*, then creates a message
// and reads data of the message from the stream
		static                   void   Read(CNetStream*, CNetMsg*&);
        virtual                  void   ReadChildren(CNetStream*);
// reads the message members from the prototype
        virtual                  void   ReadBody(const CNetMsg&);
// write the message members into m_pData
        virtual                  void   WriteBody();
// writes its member variables into the preallocated m_pData.
// The size of the data buffer is known by calling GetMsgSize()
// this calls m_header.Write()
        virtual                  void   Write(CNetStream*);
		virtual                  void   WriteChildren(CNetStream*);
protected:
  CNetMsgHdr m_header;
};

// a list of arbitrary CNetMsg derivables
class CNetList : public CNetMsg
{
public:
  struct MSG {
     ulong  m_nElemN;
  };
protected:
// define the type of callback function
  typedef bool (*DataCallBack)(void*, CNetList*);
public:
                                        CNetList();
                                        CNetList(const CNetList&);
// resets member variables (destroys m_pData)
		virtual                        ~CNetList();

        virtual           CNetObject&   operator=(const CNetObject& obj);
        virtual                  bool   operator==(const CNetObject& obj);
        virtual              CNetMsg*   Clone() const { return new CNetList;}

// if this is a complex class (aggregates other CNetMsg derivables) return true
		virtual                  bool   HasChildren() const;

		                         ulong  GetElemN() const
								 { return m_vecElem.size(); }

							  CNetMsg*  GetElem(ulong n)
							  { if (m_vecElem.size() > 0 && m_vecElem.size() > n)
							      return m_vecElem[n]; 
							    return 0;
							  }

                                 void   SetMaxElem(ulong nElemN)
								 { m_data.m_nElemN = nElemN; }
                                 ulong  GetMaxElemN() const
								 { return m_data.m_nElemN; }

                                 bool   AddElem(CNetMsg*);
                                 void   DeleteAllElem();

// allows to a data provider with postponeded data supply
                                 bool   SetDataRequestCallback(void*, DataCallBack);
                                 bool   SetDataArrivedCallback(void*, DataCallBack);

// returns the size in bytes of members of this message that can be sent accross the network
		                        ulong   GetSize_Write() const {return GetSize_ulong();}
                                ulong   GetSize_Read() const  {return GetSize_ulong();}

// returns the class id as defined in the "NetMsgNames.h" file
        virtual                 ulong   GetClassId() const {return ciNetList;}
                                void    Dump(ostream&);
protected:
// reads the message members from m_pData
        virtual                  void   ReadBody();
		virtual                  void   ReadChildren(CNetStream*);
// write the message members into m_pData
        virtual                  void   WriteBody();
		virtual                  void   WriteChildren(CNetStream*);
protected:
               MSG m_data;
  vector<CNetMsg*> m_vecElem;
      DataCallBack m_pfnOutOfData;
			 void* m_pCaller;
};

// a packet of raw data up to MAX_NET_PACKET in size
class CNetPacket : public CNetMsg
{
public:
        struct MSG {
		  ulong m_nSize;
          char  m_szPacket[MAX_NET_PACKET];
		  ulong m_nSeqN;
        };

public:
                                                CNetPacket();
                                                CNetPacket(ulong, ulong, MSG);
                                                CNetPacket(const char*, ulong, ulong);

        virtual                                 ~CNetPacket() {}

        virtual                      CNetMsg*   Clone() const {return new CNetPacket;}

        virtual                   CNetObject&   operator=(const CNetObject& obj);
        virtual                          bool   operator==(const CNetObject& obj);

		virtual                          bool   HasChildren() const {return false;}

                                   const MSG&   GetMSG() const {return m_data;}

// returns the class id as defined in the "NetMsgNames.h" file
                                ulong           GetClassId() const {return ciNetPacket;}

// returns the size in bytes of members of this message that can be sent accross the network
                                ulong           GetSize_Write() const;
					static		ulong           GetMaxSize_Packet() {return MAX_NET_PACKET;}

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
                               void             SetData(const char*, ulong, ulong);
                        const char*             GetData() const {return m_data.m_szPacket;}
						      ulong             GetSize() const {return m_data.m_nSize;}
							  ulong             GetSeqN() const {return m_data.m_nSeqN;}

protected:
        MSG     m_data;
};

class CNetStatus : public CNetMsg
{
public:
        struct MSG {
          ulong                 m_nTextSz;
          string                m_strText;
          ulong                 m_nCode;
        };

public:
                                        CNetStatus();
                                        CNetStatus(ulong, ulong, MSG);
        virtual                        ~CNetStatus();

        virtual              CNetMsg*   Clone() const {return new CNetStatus;}


        virtual            CNetObject&  operator=(const CNetObject& obj);
        virtual                  bool   operator==(const CNetObject& obj);

		virtual                  bool   HasChildren() const {return false;}

                            const MSG&  GetMSG() const {return m_data;}

// returns the class id as defined in the "NetMsgNames.h" file
                                ulong   GetClassId() const {return ciNetStatus;}
// returns the size in bytes of members of this message that can be sent accross the network
        virtual                 ulong   GetSize_Write() const;

// dumps the data into the ostream
                                 void   Dump(ostream&);
protected:
// reads the message members from m_pData
                                 void   ReadBody();
// reads the message members from the prototype
                                 void   ReadBody(const CNetMsg&);
// write the message members into m_pData
                                 void   WriteBody();

public:
                                 void   SetCode(ulong);
                                 void   SetText(const char*);
                                ulong   GetCode() const {return m_data.m_nCode;}
                           const char*  GetText() const {return m_data.m_strText.c_str();}
protected:
        MSG                             m_data;
};

class CMsgFactory
{
public:
                         CMsgFactory() {}
  virtual               ~CMsgFactory() {}

  virtual   CNetMsg*     CreateMessage(long) = 0;
};

extern CMsgFactory*  g_getDefaultMsgFactory();

#endif // !defined(AFX_NETMsg_H__C4F3191C_99C0_454A_BFAB_CE57E3CAC7A9__INCLUDED_)
