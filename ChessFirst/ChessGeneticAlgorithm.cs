
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
namespace ChessFirst
{
    [Serializable]
    public class ChessFirstGeneticAlgorithm
    {
        //
        public bool Hit = false;
        private const int PlusOne = 1;
        private const int MinusOne = -1;
        private const int ConversionDistantRowBelow = 6;
        private const int ConversionDistantRowUp = 1;
        private const int DistantColumnSmall = 6;
        private const int DistantColumnBig = 3;
        private const int DistantRowBelow = 7;
        private const int DistantRowUp = 0;
        private const int SmallCastlingColumnBefore = 4;
        private const int SmallCastlingColumnAfter = 6;
        private const int SmallCastleCastleColumnBefore = 7;
        private const int SmallCastleCastleColumnAfter = 5;
        private const int BigCastlingColumnBefore = 4;
        private const int BigCastlingColumnAfter = 2;
        private const int BigCastleCastleColumnBefore = 0;
        private const int BigCastleCastleColumnAfter = 3;
        private const int TowObjectDistanceInBigCastleBefor = 4;
        private const int TowObjectDistanceInBigCastleAfter = 1;
        private const int TowObjectDistanceInSamllCastleBefor = 2;
        private const int TowObjectDistanceInSmallCastleAfter = 1;
        private const int CastleGray = 4;
        private const int CastleBrown = -4;
        private const int KingGray = 6;
        private const int KingBrown = -6;
        //Initiate Global Variables.
        public bool MovementsAStarGreedyHeuristicFoundT = false;
        public bool IgnoreSelfObjectsT = false;
        public bool UsePenaltyRegardMechnisamT = false;
        public bool BestMovmentsT = false;
        public bool PredictHeuristicT = true;
        public bool OnlySelfT = false;
        public bool AStarGreedyHeuristicT = false;
        private readonly bool ArrangmentsChanged = true;
        public bool CastlesKing = false;
        public static bool NoGameFounf = false;
        private readonly List<int[]> RowColumn = new List<int[]>();
        private int Ki = 0;
        public int CromosomRow = -1, CromosomColumn = -1, CromosomRowHit = -1, CromosomColumnHit = -1;
        public int CromosomRowFirst = -1, CromosomColumnFirst = -1;
        private int Gen1 = 0, Gen2 = 0;
        private int[,] GeneticTable = new int[8, 8];

        private static void Log(Exception ex)
        {
            try
            {
                object a = new object();
                lock (a)
                {
                    string stackTrace = ex.ToString();
                    Helper.WaitOnUsed(AllDraw.Root + "\\ErrorProgramRun.txt"); File.AppendAllText(AllDraw.Root + "\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString());
                }
            }

            catch (Exception) { }

        }
        //Constructor.
        public ChessFirstGeneticAlgorithm(bool MovementsAStarGreedyHeuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments)
        {
            MovementsAStarGreedyHeuristicFoundT = MovementsAStarGreedyHeuristicTFou;
            IgnoreSelfObjectsT = IgnoreSelfObject;
            UsePenaltyRegardMechnisamT = UsePenaltyRegardMechnisa;
            BestMovmentsT = BestMovment;
            PredictHeuristicT = PredictHurist;
            OnlySelfT = OnlySel;
            AStarGreedyHeuristicT = AStarGreedyHuris;
            ArrangmentsChanged = Arrangments;
            //Initiate Global Variables.
            RowColumn.Clear();
        }
        public bool FindHitToModified(int[,] Cromosom1, int[,] Cromosom2, List<int[,]> List, int Index, int Order, bool and)
        {
            bool Find = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (Order == 1 && Cromosom1[i, j] <= 0)
                    {
                        continue;
                    }

                    if (Order == -1 && Cromosom1[i, j] >= 0)
                    {
                        continue;
                    }

                    if (Order == 1)
                    {
                        if (Cromosom1[i, j] != Cromosom2[i, j])
                        {
                            if (Order == 1)
                            {
                                if (Cromosom1[i, j] > 0 && Cromosom2[i, j] < 0)
                                {
                                    CromosomRowHit = i;
                                    CromosomColumnHit = j;
                                    Find = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (Cromosom1[i, j] < 0 && Cromosom2[i, j] > 0)
                                {
                                    CromosomRowHit = i;
                                    CromosomColumnHit = j;
                                    Find = true;
                                    break;
                                }

                            }
                        }
                    }

                }
                if (Find)
                {
                    break;
                }
            }
            return Find;
        }
        public bool FindGenToModified(int[,] Cromosom1, int[,] Cromosom2, List<int[,]> List, int Index, int Order, bool and)
        {
            ChessRules.SmallKingCastleBrown = false;
            ChessRules.SmallKingCastleGray = false;
            ChessRules.BigKingCastleBrown = false;
            ChessRules.BigKingCastleGray = false;
            //Injtjate Local Varjables.
            bool Find = false;
            int FindNumber = 0;
            bool Brj = false;

            //For All Table Home
            for (int j = 0; j < 8; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    //Gray Order.
                    if (Cromosom1[j, i] == 0 && Cromosom2[j, i] == 0)
                    {
                        continue;
                    }

                    //Gray Order.

                    if (!ArrangmentsChanged)
                    {
                        {
                            if (Order == 1 && i == ConversionDistantRowBelow && j > 0 && j < 7)
                            {
                                if (((Cromosom2[j, i + PlusOne] > 0) || (Cromosom2[j + PlusOne, i + PlusOne] > 0 && Cromosom1[j + PlusOne, i + PlusOne] < 0) || (Cromosom2[j + MinusOne, i + PlusOne] > 0 && Cromosom1[j + MinusOne, i + PlusOne] < 0)) && Cromosom1[j, i] == 1)
                                {
                                    CromosomRowFirst = j;
                                    CromosomColumnFirst = i;
                                    if (Cromosom2[j, i + PlusOne] > 0)
                                    {
                                        CromosomRow = j;
                                        CromosomColumn = i + PlusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                    else if (Cromosom2[j + PlusOne, i + PlusOne] > 0 && Cromosom1[j + PlusOne, i + PlusOne] < 0)
                                    {
                                        CromosomRow = j + PlusOne;
                                        CromosomColumn = i + PlusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                    else if (Cromosom2[j + MinusOne, i + PlusOne] > 0 && Cromosom1[j + MinusOne, i + PlusOne] < 0)
                                    {
                                        CromosomRow = j + MinusOne;
                                        CromosomColumn = i + PlusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }

                                }

                            }
                            else
                                if (Order == -1 && i == ConversionDistantRowUp && j > 0 && j < 7)
                            {
                                if (((Cromosom2[j, i + MinusOne] < 0) || (Cromosom2[j + PlusOne, i + MinusOne] < 0 && Cromosom1[j + PlusOne, i + MinusOne] > 0) || (Cromosom2[j + MinusOne, i + MinusOne] < 0 && Cromosom1[j + MinusOne, i + MinusOne] < 0)) && Cromosom1[j, i] == -1)
                                {
                                    CromosomRowFirst = j;
                                    CromosomColumnFirst = i;
                                    if (Cromosom2[j, i + MinusOne] > 0)
                                    {
                                        CromosomRow = j;
                                        CromosomColumn = i + MinusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                    else if (Cromosom2[j + PlusOne, i + MinusOne] > 0 && Cromosom1[j + PlusOne, i + MinusOne] < 0)
                                    {
                                        CromosomRow = j + PlusOne;
                                        CromosomColumn = i + MinusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                    else if (Cromosom2[j + MinusOne, i + MinusOne] > 0 && Cromosom1[j + MinusOne, i + MinusOne] < 0)
                                    {
                                        CromosomRow = j + MinusOne;
                                        CromosomColumn = i + MinusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                }
                            }

                            //Castles King Valjdjty Condjtjon.
                            if (Order == 1 && i == DistantRowUp)
                            {
                                //Small Gray Castles King.
                                if (j == DistantColumnSmall && Cromosom2[SmallCastleCastleColumnAfter, DistantRowUp] == KingGray && Cromosom2[SmallCastleCastleColumnAfter, DistantRowUp] == CastleGray && Cromosom1[SmallCastlingColumnBefore, DistantRowUp] == KingGray && Cromosom1[SmallCastleCastleColumnBefore, DistantRowUp] == CastleGray)
                                {
                                    //CromosomRowFirst = SmallCastlingColumnBefore;
                                    //CromosomColumnFirst = i;
                                    CromosomRow = SmallCastleCastleColumnAfter;
                                    CromosomColumn = i;
                                    Find = true;
                                    FindNumber++;
                                    ChessRules.SmallKingCastleGray = true;
                                    Brj = true;
                                }
                                else //Big Brjges King Gray.
                                    if (j == DistantColumnBig && Cromosom2[BigCastleCastleColumnAfter, DistantRowUp] == CastleGray && Cromosom2[BigCastlingColumnAfter, DistantRowUp] == KingGray && Cromosom1[BigCastleCastleColumnBefore, DistantRowUp] == CastleGray && Cromosom1[BigCastlingColumnBefore, DistantRowUp] == KingGray)
                                {
                                    //CromosomRowFirst = DistantRowUp;
                                    //CromosomColumnFirst = i;
                                    CromosomRow = SmallCastleCastleColumnBefore;
                                    CromosomColumn = i;
                                    Find = true;
                                    FindNumber++;
                                    ChessRules.BigKingCastleGray = true;
                                    Brj = true;
                                }

                            }
                            else if (i == DistantRowBelow)
                            {
                                //Small Castles King Brown.
                                if (j == DistantColumnSmall && Cromosom2[BigCastlingColumnAfter, DistantRowBelow] == KingBrown && Cromosom2[BigCastleCastleColumnAfter, DistantRowBelow] == CastleBrown && Cromosom1[BigCastlingColumnBefore, DistantRowBelow] == KingBrown && Cromosom1[BigCastleCastleColumnBefore, DistantRowBelow] == CastleBrown)
                                {
                                    object O = new object();
                                    lock (O)
                                    {
                                        //CromosomRowFirst = DistantRowBelow;
                                        //CromosomColumnFirst = i;
                                        CromosomRow = BigCastlingColumnAfter;
                                        CromosomColumn = i;
                                        Find = true;
                                        FindNumber++;
                                        ChessRules.SmallKingCastleBrown = true;
                                        Brj = true;
                                    }
                                }
                                else//Big Castles King Brown.
                                    if (j == DistantColumnBig && Cromosom2[BigCastleCastleColumnAfter, DistantRowBelow] == CastleBrown && Cromosom2[BigCastlingColumnBefore, DistantRowBelow] == KingBrown && Cromosom1[BigCastleCastleColumnBefore, DistantRowBelow] == CastleBrown && Cromosom1[BigCastlingColumnBefore, DistantRowBelow] == KingBrown)
                                {
                                    object O = new object();
                                    lock (O)
                                    {
                                        CromosomRowFirst = BigCastlingColumnBefore;
                                        CromosomColumnFirst = i;
                                        //CromosomRow = DistantRowBelow;
                                        //CromosomColumn = i;
                                        Find = true;
                                        FindNumber++;
                                        ChessRules.BigKingCastleBrown = true;
                                        Brj = true;
                                    }
                                }

                            }

                        }
                    }
                    else
                    {
                        {
                            if (Order == 1 && i == ConversionDistantRowUp && j > 0 && j < 7)
                            {
                                if (((Cromosom2[j, i + MinusOne] > 0) || (Cromosom2[j + PlusOne, i + MinusOne] > 0 && Cromosom1[j + PlusOne, i + MinusOne] < 0) || (Cromosom2[j + MinusOne, i + MinusOne] > 0 && Cromosom1[j + MinusOne, i + MinusOne] < 0)) && Cromosom1[j, i] == 1)
                                {
                                    CromosomRowFirst = j;
                                    CromosomColumnFirst = i;
                                    if (Cromosom2[j, i + MinusOne] > 0)
                                    {
                                        CromosomRow = j;
                                        CromosomColumn = i + MinusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                    else if (Cromosom2[j + PlusOne, i + MinusOne] > 0 && Cromosom1[j + PlusOne, i + MinusOne] < 0)
                                    {
                                        CromosomRow = j + PlusOne;
                                        CromosomColumn = i + MinusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                    else if (Cromosom2[j + MinusOne, i + MinusOne] > 0 && Cromosom1[j + MinusOne, i + MinusOne] < 0)
                                    {
                                        CromosomRow = j + MinusOne;
                                        CromosomColumn = i + MinusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }

                                }

                            }
                            else
                                if (Order == -1 && i == ConversionDistantRowBelow && j > 0 && j < 7)
                            {
                                if (((Cromosom2[j, i + PlusOne] < 0) || (Cromosom2[j + PlusOne, i + PlusOne] < 0 && Cromosom1[j + PlusOne, i + PlusOne] > 0) || (Cromosom2[j + MinusOne, i + PlusOne] < 0 && Cromosom1[j + MinusOne, i + PlusOne] < 0)) && Cromosom1[j, i] == -1)
                                {
                                    CromosomRowFirst = j;
                                    CromosomColumnFirst = i;
                                    if (Cromosom2[j, i + PlusOne] > 0)
                                    {
                                        CromosomRow = j;
                                        CromosomColumn = i + PlusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                    else if (Cromosom2[j + PlusOne, i + PlusOne] > 0 && Cromosom1[j + PlusOne, i + PlusOne] < 0)
                                    {
                                        CromosomRow = j + PlusOne;
                                        CromosomColumn = i + PlusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                    else if (Cromosom2[j + MinusOne, i + PlusOne] > 0 && Cromosom1[j + MinusOne, i + PlusOne] < 0)
                                    {
                                        CromosomRow = j + MinusOne;
                                        CromosomColumn = i + PlusOne;
                                        Find = true;
                                        FindNumber++;
                                        AllDraw.SodierConversionOcuured = true;
                                    }
                                }
                            }

                            //Castles King Valjdjty Condjtjon.
                            if (Order == 1 && i == DistantRowBelow)
                            {
                                //Small Gray Castles King.
                                if (j == DistantColumnSmall && Cromosom2[SmallCastlingColumnAfter, DistantRowBelow] == KingGray && Cromosom2[SmallCastleCastleColumnAfter, DistantRowBelow] == CastleGray && Cromosom1[SmallCastlingColumnBefore, DistantRowBelow] == KingGray && Cromosom1[SmallCastleCastleColumnBefore, DistantRowBelow] == CastleGray)
                                {
                                    //CromosomRowFirst = DistantRowBelow;
                                    //CromosomColumnFirst = i;
                                    CromosomRow = SmallCastleCastleColumnAfter;
                                    CromosomColumn = i;
                                    Find = true;
                                    FindNumber++;
                                    ChessRules.SmallKingCastleGray = true;
                                    Brj = true;
                                }
                                else //Big Brjges King Gray.
                                    if (j == DistantColumnBig && Cromosom2[BigCastleCastleColumnAfter, DistantRowBelow] == CastleGray && Cromosom2[BigCastlingColumnAfter, DistantRowBelow] == KingGray && Cromosom1[BigCastleCastleColumnBefore, DistantRowBelow] == CastleGray && Cromosom1[BigCastlingColumnBefore, DistantRowBelow] == KingGray)
                                {

                                    CromosomRowFirst = SmallCastleCastleColumnBefore;
                                    CromosomColumnFirst = i;
                                    //CromosomRow = SmallCastleCastleColumnAfter;
                                    //CromosomColumn = i;
                                    Find = true;
                                    FindNumber++;
                                    ChessRules.BigKingCastleGray = true;
                                    Brj = true;
                                }

                            }
                            else if (i == DistantRowUp)
                            {
                                //Small Castles King Brown.
                                if (j == DistantColumnSmall && Cromosom2[SmallCastlingColumnAfter, DistantRowUp] == KingBrown && Cromosom2[SmallCastleCastleColumnAfter, DistantRowUp] == CastleBrown && Cromosom1[SmallCastlingColumnBefore, DistantRowUp] == KingBrown && Cromosom1[SmallCastleCastleColumnBefore, DistantRowUp] == CastleBrown)
                                {
                                    object O = new object();
                                    lock (O)
                                    {
                                        //CromosomRowFirst = DistantRowUp;
                                        //CromosomColumnFirst = i;
                                        CromosomRow = BigCastlingColumnAfter;
                                        CromosomColumn = i;
                                        Find = true;
                                        FindNumber++;
                                        ChessRules.SmallKingCastleBrown = true;
                                        Brj = true;
                                    }
                                }
                                else//Big Castles King Brown.
                                    if (j == DistantColumnBig && Cromosom2[BigCastleCastleColumnAfter, DistantRowUp] == CastleBrown && Cromosom2[BigCastlingColumnAfter, DistantRowUp] == KingBrown && Cromosom1[BigCastleCastleColumnBefore, DistantRowUp] == CastleBrown && Cromosom1[BigCastlingColumnBefore, DistantRowUp] == KingBrown)
                                {
                                    object O = new object();
                                    lock (O)
                                    {
                                        CromosomRowFirst = BigCastlingColumnBefore;
                                        CromosomColumnFirst = i;
                                        //CromosomRow = DistantRowUp;
                                        //CromosomColumn = i;
                                        Find = true;
                                        FindNumber++;
                                        ChessRules.BigKingCastleBrown = true;
                                        Brj = true;
                                    }
                                }

                            }

                        }
                    }


                    //When To Same Locatjon Tbles are Different jn Gen.
                    if (Cromosom1[j, i] != Cromosom2[j, i])
                    {
                        if (CromosomRowFirst == -1 && CromosomColumnFirst == -1)
                        {
                            FindNumber++;
                            CromosomRowFirst = AllDraw.LastRow;
                            CromosomColumnFirst = AllDraw.LastColumn;

                        }
                        else
                        {
                            if (CromosomRow == -1 && CromosomColumn == -1)
                            {
                                CromosomRow = AllDraw.NextRow;
                                CromosomColumn = AllDraw.NextColumn;
                                Find = true;
                                FindNumber++;
                                Ki = Cromosom1[CromosomColumnFirst, CromosomRowFirst];
                            }


                        }
                    }

                    //Store Locatjon of Gen and Calculate Gen Numbers.

                }


            }

            Hit = HitSet(Order, Cromosom1, Cromosom2);

            //If Gen Foundatjon js Valjd. 
            if (((FindNumber >= 1) && Find) || Brj || AllDraw.SodierConversionOcuured)
            {
                return Find;
            }
            //Gen Not Found.
            return false;
        }

        private bool HitSet(int Order, int[,] Cromosom1, int[,] Cromosom2)
        {
            bool Hit = false;
            try
            {
                if (CromosomRowFirst != -1 && CromosomColumnFirst != -1 && CromosomRow != -1 && CromosomColumn != -1)
                {
                    if (Order == 1)
                    {
                        if (Cromosom1[CromosomRowFirst, CromosomColumnFirst] > 0 && Cromosom1[CromosomRow, CromosomColumn] < 0)
                        {
                            Hit = true;
                        }
                    }
                    else
                    {
                        if (Cromosom1[CromosomRowFirst, CromosomColumnFirst] < 0 && Cromosom1[CromosomRow, CromosomColumn] > 0)
                        {
                            Hit = true;
                        }
                    }
                }
            }
            catch (Exception t) { Log(t); }
            return Hit;
        }

        private int[,] CloneATable(int[,] Tab)
        {

            object O = new object();
            lock (O)
            {
                //Create and new an Object.
                int[,] Table = new int[8, 8];
                //Assigne Parameter To New Objects.
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Table[i, j] = Tab[i, j];
                    }
                }
                //Return New Object.

                return Table;
            }

        }

        private bool[,] CloneATable(bool[,] Tab)
        {

            object O = new object();
            lock (O)
            {
                //Create and new an Object.
                bool[,] Table = new bool[8, 8];
                //Assigne Parameter To New Objects.
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        Table[i, j] = Tab[i, j];
                    }
                }
                //Return New Object.

                return Table;
            }

        }
        //Table Foundation of Genetic Alogorithm Method.
        public int[,] GenerateTable(List<int[,]> List, int Index, int Order)
        {
            //Initiate Local Variables.
            Begine5:
            RowColumn.Clear();
            int Store = Index;
            int[,] Cromosom1 = null;
            int[,] Cromosom2 = null;
            try
            {
                Cromosom1 = List[List.Count - 2];
                Cromosom2 = List[List.Count + MinusOne];
            }
            catch (IndexOutOfRangeException t)
            {
                Log(t);
                return null;
            }

            Index = Store;
            //Found of Gen.
            if (!FindGenToModified(Cromosom1, Cromosom2, List, Index, Order, false))
            {
                goto EndFindAThing;
            }





            //Initiate Global Variables.
            BeginFind:
            Color color = Color.Gray;
            if (Order == -1)
            {
                color = Color.Brown;
            }

            try
            {
                //If Cromosom Location is Not Founded.
                if (CromosomRow == -1 && CromosomColumn == -1)
                {
                    //Initiayte Local Variables.
                    List.RemoveAt(List.Count + MinusOne);
                    Index--;
                    goto Begine5;
                }
                //Found Kind Of Gen.
                Ki = List[List.Count + MinusOne][CromosomRow, CromosomColumn];
                //Initiate Local Variables.
                GeneticTable = new int[8, 8];
                //If Gen Kind Not Found Retrun Not Valididity.
                if (List[List.Count + MinusOne][CromosomRow, CromosomColumn] == 0)
                {
                    return null;
                }
                else
                {
                    //Clone a Copy.
                    for (int ii = 0; ii < 8; ii++)
                    {
                        for (int jj = 0; jj < 8; jj++)
                        {
                            GeneticTable[ii, jj] = List[List.Count + MinusOne][ii, jj];
                        }
                    }
                }
                //Initiate Global and Local Variables.
                color = Color.Gray;
                if (Order == -1)
                {
                    color = Color.Brown;
                }
                //For All Gens.
                for (Gen1 = 0; Gen1 < 8; Gen1++)
                {
                    for (Gen2 = 0; Gen2 < 8; Gen2++)
                    {
                        //If Gen is Current Gen Location Continue Traversal Back.
                        if (Gen1 == CromosomRow && Gen2 == CromosomColumn)
                        {
                            continue;
                        }
                        //Rulement of Gen Movments.
                        if ((new ChessRules(0, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, GeneticTable[CromosomRow, CromosomColumn], CloneATable(GeneticTable), Order, CromosomRow, CromosomColumn)).Rules(CromosomRow, CromosomColumn, Gen1,
                        Gen2, color, GeneticTable[CromosomRow, CromosomColumn]))
                        {
                            //Initiate Global Variables and Syntax.
                            int[] A = new int[2];
                            A[0] = CromosomRow;
                            A[1] = CromosomColumn;
                            RowColumn.Add(A);



                            GeneticTable[Gen1, Gen2] = GeneticTable[CromosomRow, CromosomColumn];
                            GeneticTable[CromosomRow, CromosomColumn] = 0;
                            //Table Repeatative Consideration.
                            if (ThinkingChessFirst.ExistTableInList(CloneATable(GeneticTable), List, 0))
                            {
                                GeneticTable[CromosomRow, CromosomColumn] = GeneticTable[Gen1, Gen2];
                                GeneticTable[Gen1, Gen2] = 0;
                                continue;

                            }
                            else
                            {
                                //Check Consideration.
                                if ((new ChessRules(0, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, GeneticTable[CromosomRow, CromosomRow], CloneATable(GeneticTable), Order, CromosomRow, CromosomColumn)).Check(CloneATable(GeneticTable), Order))
                                {
                                    GeneticTable[CromosomRow, CromosomColumn] = GeneticTable[Gen1, Gen2];
                                    GeneticTable[Gen1, Gen2] = 0;
                                    continue;
                                }

                                else
                                {

                                    //Return Genetic Table.
                                    return GeneticTable;
                                }

                            }
                        }


                    }
                }
                //Initiate Try Catch.
                GeneticTable = null;
                int a = GeneticTable[0, 0];
            }

            catch (NullReferenceException t)
            {
                //Try Catch Expetion Handling of Not Successful Foundation of Gen.
                Log(t);
                if (Order == 1)
                {
                    Ki = (new Random()).Next(1, 7);
                }
                else
                {
                    Ki = (new Random()).Next(1, 7) * -1;
                }

                if (Order == 1)
                {
                    int Count = 0;
                    do
                    {
                        if (Ki < 6)
                        {
                            Ki++;
                        }
                        else
                        {
                            Ki = 1;
                        }

                        Count++;
                    } while (Count < 6 && !(new ChessRules(0, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Ki, List[List.Count + MinusOne], Order, CromosomRow, CromosomColumn)).FindAThing(List[List.Count + MinusOne], ref CromosomRow, ref CromosomColumn, Ki, true, RowColumn));
                    if (Count >= 6)
                    {
                        NoGameFounf = true;
                        return null;
                    }


                }
                else
                {
                    int Count = 0;
                    do
                    {
                        if (Ki > -6)
                        {
                            Ki--;
                        }
                        else
                        {
                            Ki = -1;
                        }

                        Count++;
                    } while (Count < 6 && !(new ChessRules(0, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Ki, List[List.Count + MinusOne], Order, CromosomRow, CromosomColumn)).FindAThing(List[List.Count + MinusOne], ref CromosomRow, ref CromosomColumn, Ki, true, RowColumn));
                    if (Count >= 6)
                    {
                        NoGameFounf = true;
                        return null;
                    }






                }

                goto BeginFind;
            }

            EndFindAThing:
            //Foudn of Some Samness Gen.
            if (Order == 1)
            {
                Ki = (new Random()).Next(1, 7);
            }
            else
            {
                Ki = (new Random()).Next(1, 7) * -1;
            }

            if (Order == 1)
            {
                int Count = 0;
                do
                {
                    if (Ki < 6)
                    {
                        Ki++;
                    }
                    else
                    {
                        Ki = 1;
                    }

                    Count++;
                } while (Count < 6 && !(new ChessRules(0, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Ki, List[List.Count + MinusOne], Order, CromosomRow, CromosomColumn)).FindAThing(List[List.Count + MinusOne], ref CromosomRow, ref CromosomColumn, Ki, true, RowColumn));
                if (Count >= 6)
                {
                    return null;
                }
            }
            else
            {
                int Count = 0;
                do
                {
                    if (Ki > -6)
                    {
                        Ki--;
                    }
                    else
                    {
                        Ki = -1;
                    }

                    Count++;
                } while (Count < 6 && !(new ChessRules(0, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Ki, List[List.Count + MinusOne], Order, CromosomRow, CromosomColumn)).FindAThing(List[List.Count + MinusOne], ref CromosomRow, ref CromosomColumn, Ki, true, RowColumn));
                if (Count >= 6)
                {
                    return null;
                }
            }

            goto BeginFind;


        }
    }

}
//End of Documentation.
