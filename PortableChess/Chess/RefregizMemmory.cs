/**************************************************************************************************************
 * Ramin Edjlal Copyright 1397/04/20 **************************************************************************
 * 1397/04/26:Problem in Seirlization Recurisvely of linked list for refrigitz.********************************
 * ************************************************************************************************************
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
namespace GalleryStudio
{

    [Serializable]

    public class RefregizMemmory //:AllDraw
    {
        public static int AllDrawKind = 0;//0,1,2,3,4,5,6
        public static String AllDrawKindString = "";

        public int iii = 0, jjj = 0;
        public bool MovementsAStarGreedyHeuristicFoundT = false;
        public bool IgnoreSelfObjectsT = false;
        public bool UsePenaltyRegardMechnisamT = true;
        public bool BestMovmentsT = false;
        public bool PredictHeuristicT = true;
        public bool OnlySelfT = false;
        public bool AStarGreedyHeuristicT = false;
        public bool ArrangmentsT = false;
        string SAllDraw = "";
        public int Kind = 0;
        //static GalleryStudio.RefregizMemmory Node;
        public RefrigtzChessPortable.AllDraw Current = null;
         //bool NewListOfNextBegins = true;


        
        void SetAllDrawKindString()
        {
            if (AllDrawKind == 4)
                AllDrawKindString = "AllDrawBT.asd";//Both True
            else
                if (AllDrawKind == 3)
                AllDrawKindString = "AllDrawFFST.asd";//First false second true
            else
                if (AllDrawKind == 2)
                AllDrawKindString = "AllDrawFTSF.asd";//First true second false
            else
                if (AllDrawKind == 1)
                AllDrawKindString = "AllDrawFFSF.asd";//Fist false second false


        }
        public RefregizMemmory(bool MovementsAStarGreedyHeuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments//) : base(MovementsAStarGreedyHeuristicTFou, IgnoreSelfObject, UsePenaltyRegardMechnisa, BestMovment, PredictHurist, OnlySel, AStarGreedyHuris, Arrangments
            )
        {
            if (UsePenaltyRegardMechnisa && AStarGreedyHuris)
                AllDrawKind = 4;
            else
                                           if ((!UsePenaltyRegardMechnisa) && AStarGreedyHuris)
                AllDrawKind = 3;
            if (UsePenaltyRegardMechnisa && (!AStarGreedyHuris))
                AllDrawKind = 2;
            if ((!UsePenaltyRegardMechnisa) && (!AStarGreedyHuris))
                AllDrawKind = 1;
            //Set Configuration To True for some unknown reason!.
            //UpdateConfigurationTableVal = true;                             
            SetAllDrawKindString();
            SAllDraw = AllDrawKindString;

  
            Object o = new Object();
            lock (o)
            {
                MovementsAStarGreedyHeuristicFoundT = MovementsAStarGreedyHeuristicTFou;
                IgnoreSelfObjectsT = IgnoreSelfObject;
                BestMovmentsT = BestMovment;
                PredictHeuristicT = PredictHurist;
                OnlySelfT = OnlySel;
                ArrangmentsT = Arrangments;
            }

        }
        public RefrigtzChessPortable.AllDraw Load(bool Quantum, int Order)
        {
            Object o = new Object();
            lock (o)
            {
                if (File.Exists(SAllDraw))
                {
                    FileInfo A = new FileInfo(SAllDraw);
                    if (A.Length == 0)
                        return null;

                    RefrigtzChessPortable.AllDraw tt = new RefrigtzChessPortable.AllDraw(Order, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsT);
                    FileStream DummyFileStream = new FileStream(SAllDraw, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite);
                    BinaryFormatter Formatters = new BinaryFormatter();
                    DummyFileStream.Seek(0, SeekOrigin.Begin);

                    Console.WriteLine("Loading...");
                    tt = (RefrigtzChessPortable.AllDraw)Formatters.Deserialize(DummyFileStream);
                    if (tt == null)
                        return tt;
                    tt = (RefrigtzChessPortable.AllDraw)tt.LoaderEC(Quantum, Order, DummyFileStream, Formatters);

                    DummyFileStream.Flush();
                    DummyFileStream.Close();

                    return tt;
                }

                return null;

                //return Node.al;
            }
        }
        public void RewriteAllDraw(int Order)
        {
            Object oo = new Object();
            lock (oo)
            {

                //Current = new RefrigtzChessPortable.AllDraw(MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsT);
                FileStream DummyFileStream = null;



                //RefregizMemmory t = p;

                FileInfo DummyFileInfo = new FileInfo(SAllDraw);
                //DummyFileInfo.Delete();
                DummyFileStream = new FileStream(SAllDraw, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write);
                BinaryFormatter Formatters = new BinaryFormatter();
                DummyFileStream.Seek(0, SeekOrigin.Begin);

                Formatters.Serialize(DummyFileStream, Current);
                Current.RewriteAllDrawRec(Formatters, DummyFileStream, Order);


                DummyFileStream.Flush(); DummyFileStream.Close();

            }
        }
 
        public RefrigtzChessPortable.AllDraw AllDrawCurrentAccess
        {
            get
            { return Current; }
            set
            { Current = value; }
        }
        
    }
}
