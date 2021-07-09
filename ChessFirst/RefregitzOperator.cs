/**************************************************************************************************************
 * Ramin Edjlal Copyright 1397/04/20 **************************************************************************
 * 1397/04/26:Problem in Seirlization Recurisvely of linked list for refrigitz.********************************
 * ************************************************************************************************************
 */using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
namespace ChessFirst
{
    [Serializable]
    public class RefregitzOperator//:RefregizMemmory
    {
        public static int AllDrawKind = 0;//0,1,2,3,4,5,6
        public static string AllDrawKindString = "";


        public bool MovementsAStarGreedyHeuristicFoundT = false;
        public bool IgnoreSelfObjectsT = false;
        public bool UsePenaltyRegardMechnisamT = false;
        public bool BestMovmentsT = false;
        public bool PredictHeuristicT = true;
        public bool OnlySelfT = false;
        public bool AStarGreedyHeuristicT = false;
        public bool ArrangmentsT = false;
        public static string Root = System.IO.Path.GetDirectoryName(Environment.GetCommandLineArgs()[0]);
        private readonly string SAllDraw = "";

        //static RefregizMemmory Node;
        //AllDraw Current = null;
        //RefregizMemmory Next = null;
        //int Kind = -1;
        private static void Log(Exception ex)
        {

            object a = new object();
            lock (a)
            {
                string stackTrace = ex.ToString();
                File.AppendAllText(Root + "\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString()); // path of file where stack trace will be stored.
            }

        }

        private void SetAllDrawKindString()
        {
            object O = new object();
            lock (O)
            {
                if (AllDrawKind == 4)
                {
                    AllDrawKindString = "F_AllDrawBT.asd";
                }
                else
                if (AllDrawKind == 3)
                {
                    AllDrawKindString = "F_AllDrawFFST.asd";
                }
                else
                if (AllDrawKind == 2)
                {
                    AllDrawKindString = "F_AllDrawFTSF.asd";
                }
                else
                if (AllDrawKind == 1)
                {
                    AllDrawKindString = "F_AllDrawFFSF.asd";
                }
            }
        }
        public RefregitzOperator(int Order, bool MovementsAStarGreedyHeuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments//) : base(MovementsAStarGreedyHeuristicTFou, IgnoreSelfObject, UsePenaltyRegardMechnisa, BestMovment, PredictHurist, OnlySel, AStarGreedyHuris, Arrangments
            )
        {
            if (UsePenaltyRegardMechnisamT && AStarGreedyHeuristicT)
            {
                AllDrawKind = 4;
            }
            else
                                            if ((!UsePenaltyRegardMechnisamT) && AStarGreedyHeuristicT)
            {
                AllDrawKind = 3;
            }

            if (UsePenaltyRegardMechnisamT && (!AStarGreedyHeuristicT))
            {
                AllDrawKind = 2;
            }

            if ((!UsePenaltyRegardMechnisamT) && (!AStarGreedyHeuristicT))
            {
                AllDrawKind = 1;
            }
            //Set Configuration To True for some unknown reason!.
            //UpdateConfigurationTableVal = true;                             
            SetAllDrawKindString();
            SAllDraw = AllDrawKindString;
            object o = new object();
            lock (o)
            {
                MovementsAStarGreedyHeuristicFoundT = MovementsAStarGreedyHeuristicTFou;
                IgnoreSelfObjectsT = IgnoreSelfObject;
                UsePenaltyRegardMechnisamT = UsePenaltyRegardMechnisa;
                BestMovmentsT = BestMovment;
                PredictHeuristicT = PredictHurist;
                OnlySelfT = OnlySel;
                AStarGreedyHeuristicT = AStarGreedyHuris;
                ArrangmentsT = Arrangments;
                AllDraw Current = new AllDraw(Order, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsT);
            }
        }


        public AllDraw GetRefregiz(int No)
        {
            object o = new object();
            lock (o)
            {

                FileStream DummyFileStream = null;
                DummyFileStream = new FileStream(SAllDraw, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Read);
                int p = 0;
                AllDraw Dummy = null;
                BinaryFormatter Formatters = new BinaryFormatter();
                DummyFileStream.Seek(0, SeekOrigin.Begin);

                while (p <= No)
                {
                    if (DummyFileStream.Length >= DummyFileStream.Position)
                    {
                        Dummy = (AllDraw)Formatters.Deserialize(DummyFileStream);
                    }
                    else
                    {
                        Dummy = null;
                    }

                    p++;
                }
                DummyFileStream.Flush(); DummyFileStream.Close();

                return Dummy;
            }
        }

    }
}
