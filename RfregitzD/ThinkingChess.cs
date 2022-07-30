/**************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
*************TETRASHOP.IR**************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
***************************************
**************************************/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;


namespace RefrigtzDLL
{
    [Serializable]
    public class ThinkingChess//: IDisposable
    {
        private bool ExcangePerformed = false;
        private double[] PerformedExchange = null;

        public static string OutP = "";
        private readonly List<List<List<int[]>>> MovableAllObjectsList = new List<List<List<int[]>>>();
        public int RemoveOfDisturbIndex = -1;
        private int HeuristicDoubleDefenceIndexInOnGameMidle = 0;
        private readonly List<List<int[]>> HeuristicDoubleDefenceIndexInOnGame = new List<List<int[]>>();
        private int HeuristicReducedAttackedIndexInOnGameMidle = 0;
        private readonly List<int> HeuristicReducedAttackedIndexInOnGame = new List<int>();
        private static bool GoldenFinished = false;
        public List<List<List<int[]>>> AchmazPure = new List<List<List<int[]>>>();
        public List<List<List<int[]>>> AchmazReduced = new List<List<List<int[]>>>();

        public List<int> WinChiled = new List<int>();
        public List<int> LoseChiled = new List<int>();
        private bool IKIsCentralPawnIsOk = false;
        private List<int[]> HeuristicAllSupport = new List<int[]>();
        private int HeuristicAllSupportMidel = 0;
        private List<int[]> HeuristicAllReducedSupport = new List<int[]>();
        private int HeuristicAllReducedSupportMidel = 0;
        private List<int[]> HeuristicAllAttacked = new List<int[]>();
        private int HeuristicAllAttackedMidel = 0;
        private List<int[]> HeuristicAllReducedAttacked = new List<int[]>();
        private int HeuristicAllReducedAttackedMidel = 0;
        private List<int[]> HeuristicAllMove = new List<int[]>();
        private int HeuristicAllMoveMidel = 0;
        private List<int[]> HeuristicAllReducedMove = new List<int[]>();
        private int HeuristicAllReducedMoveMidel = 0;
        public static int NoOfBoardMovedGray = 0;
        public static int NoOfBoardMovedBrown = 0;
        public static int NoOfMovableAllObjectMove = 1;
        public int DifOfNoOfSupporteAndReducedSupportGray = int.MinValue;
        public int DifOfNoOfSupporteAndReducedSupportBrown = int.MinValue;
        public static int ColleralationGray = int.MinValue;
        public static int ColleralationBrown = int.MinValue;
        public static int Colleralation = int.MaxValue;
        public static int DeColleralation = int.MaxValue;

        public static int[,] TableInitiation ={
             { -4, -1, 0, 0, 0, 0, 1, 4 },
            { -3, -1, 0, 0, 0, 0, 1, 3 },
            { -2, -1, 0, 0, 0, 0, 1, 2 },
            { -5, -1, 0, 0, 0, 0, 1, 5 },
            { -6, -1, 0, 0, 0, 0, 1, 6 },
            { -2, -1, 0, 0, 0, 0, 1, 2 },
            { -3, -1, 0, 0, 0, 0, 1, 3 },
            { -4, -1, 0, 0, 0, 0, 1, 4 }
            };

        public static int[,] TableInitiationPreventionOfMultipleMove ={
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0, 0, 0, 0 }
            };

        private int RationalRegard = 10;
        private int RationalPenalty = -10;

        public static bool FullGameAllow = false;
        private readonly int iIndex = -1;
        public static bool IsAtLeastOneKillerAtDraw = false;
        public List<bool> KishSelf = new List<bool>();
        public List<bool> KishEnemy = new List<bool>();

        public double HeuristicAttackValueSup = new double();

        public double HeuristicMovementValueSup = new double();
        public double HeuristicSelfSupportedValueSup = new double();
        public double HeuristicReducedMovementValueSup = new double();
        public double HeuristicReducedSupportSup = new double();
        public double HeuristicReducedAttackValueSup = new double();
        public double HeuristicDistributionValueSup = new double();
        public double HeuristicKingSafeSup = new double();
        public double HeuristicFromCenterSup = new double();
        public double HeuristicKingDangourSup = new double();
        public List<bool> IsSup = new List<bool>();
        public List<bool> IsSupHu = new List<bool>();

        //Initiate Global and Static Variables.
        public List<bool> IsThereMateOfEnemy = new List<bool>();

        public List<bool> IsThereMateOfSelf = new List<bool>();
        public List<bool> IsThereCheckOfEnemy = new List<bool>();
        public List<bool> IsThereCheckOfSelf = new List<bool>();
        public static LearningMachine.NetworkQuantumLearningKrinskyAtamata LearniningTable = new LearningMachine.NetworkQuantumLearningKrinskyAtamata(8, 8, 8);
        public static string ActionsString = "";
        public List<bool[]> LearningVarsObject = new List<bool[]>();
        public static bool LearningVarsCheckedMateOccured;
        public static bool LearningVarsCheckedMateOccuredOneCheckedMate;
        private bool IsGardHighPriority = false;

        public static double MaxHeuristicx = double.MinValue;
        public bool MovementsAStarGreedyHeuristicFoundT = false;
        public bool IgnoreSelfObjectsT = false;
        public bool UsePenaltyRegardMechnisamT = false;
        public bool BestMovmentsT = false;
        public bool PredictHeuristicT = true;
        public bool OnlySelfT = false;
        public bool AStarGreedyHeuristicT = false;
        private bool ArrangmentsChanged = true;
        public int NumberOfPenalties = 0;
        private static int NumbersOfCurrentBranchesPenalties = 0;
        public static int NumbersOfAllNode = 0;
        public int SodierMidle = 0;
        public int SodierHigh = 0;
        public int ElefantMidle = 0;
        public int ElefantHigh = 0;
        public int HourseMidle = 0;
        public int HourseHight = 0;
        public int CastleMidle = 0;
        public int CastleHigh = 0;
        public int MinisterMidle = 0;
        public int MinisterHigh = 0;
        public int KingMidle = 0;
        public int KingHigh = 0;
        public static bool KingMaovableGray = false;
        public static bool KingMaovableBrown = false;
        public static int FoundFirstMating;
        public static int FoundFirstSelfMating;
        public int SodierValue = 1 * 3;
        public int ElefantValue = 2 * 16;
        public int HourseValue = 3 * 8;
        public int CastleValue = 5 * 16;
        public int MinisterValue = 8 * 32;
        public int KingValue = 10 * 8;
        public static int BeginThread = 0;
        public static int EndThread = 0;
        private bool ExistingOfEnemyHiiting = false;
        private int IgnoreObjectDangour = -1;
        public int CheckMateAStarGreedy = 0;
        private bool CheckMateOcuured = false;
        private int CurrentRow = -1, CurrentColumn = -1;
        public bool IsCheck = false;
        public int Kind = 0;
        public List<int> HitNumber = new List<int>();
        public static bool NotSolvedKingDanger = false;
        public static bool ThinkingRun = false;
        public int ThingsNumber = 0;
        public int CurrentArray = 0;
        public bool ThinkingBegin = false;
        public bool ThinkingFinished = false;
        public int IndexSoldier = 0;
        public int IndexElefant = 0;
        public int IndexHourse = 0;
        public int IndexCastle = 0;
        public int IndexMinister = 0;
        public int IndexKing = 0;
        public int IndexCastling = 0;

        public List<int[]> RowColumnSoldier = null;
        public List<int[]> RowColumnElefant = null;
        public List<int[]> RowColumnHourse = null;
        public List<int[]> RowColumnCastle = null;
        public List<int[]> RowColumnMinister = null;
        public List<int[]> RowColumnKing = null;
        public List<int[]> RowColumnCastling = null;
        public int[,] TableT;
        public List<int> HitNumberSoldier = null;
        public List<int> HitNumberElefant = null;
        public List<int> HitNumberHourse = null;
        public List<int> HitNumberCastle = null;
        public List<int> HitNumberMinister = null;
        public List<int> HitNumberKing = null;
        public List<int> HitNumberCastling = null;
        public int[,] TableConst;
        public List<int[,]> TableListSolder = null;
        public List<int[,]> TableListElefant = null;
        public List<int[,]> TableListHourse = null;
        public List<int[,]> TableListCastle = null;
        public List<int[,]> TableListMinister = null;
        public List<int[,]> TableListKing = null;
        public List<int[,]> TableListCastling = null;

        public List<double[]> HeuristicListSolder = null;
        public List<double[]> HeuristicListElefant = null;
        public List<double[]> HeuristicListHourse = null;
        public List<double[]> HeuristicListCastle = null;
        public List<double[]> HeuristicListMinister = null;
        public List<double[]> HeuristicListKing = null;
        public List<double[]> HeuristicListCastling = null;
        public List<int> KillerAtThinking = null;
        public List<LearningMachine.QuantumAtamata> PenaltyRegardListSolder = null;
        public List<LearningMachine.QuantumAtamata> PenaltyRegardListElefant = null;
        public List<LearningMachine.QuantumAtamata> PenaltyRegardListHourse = null;
        public List<LearningMachine.QuantumAtamata> PenaltyRegardListCastle = null;
        public List<LearningMachine.QuantumAtamata> PenaltyRegardListMinister = null;
        public List<LearningMachine.QuantumAtamata> PenaltyRegardListKing = null;
        public List<LearningMachine.QuantumAtamata> PenaltyRegardListCastling = null;

        public int Max;
        public int Row, Column;
        public Color color;
        public int Order;

        //[NonSerialized()]
        public List<AllDraw> AStarGreedy = new List<AllDraw>();

        public List<bool> AStarGreedyMove = new List<bool>();
        public int CurrentAStarGredyMax = -1;

        ///Log of Errors.
        private static void Log(Exception ex)
        {
            try
            {
                object a = new object();
                lock (a)
                {
                    string stackTrace = ex.ToString();
                    //Write to File.
                    Helper.WaitOnUsed(AllDraw.Root + "\\ErrorProgramRun.txt"); File.AppendAllText(AllDraw.Root + "\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString());
                }
            }
            catch (Exception) { }
        }

        //distiguis object boundries
        public void SetObjectNumbers(int[,] TabS)
        {
            object a = new object();
            lock (a)
            {
                SodierMidle = 0;
                SodierHigh = 0;
                ElefantMidle = 0;
                ElefantHigh = 0;
                HourseMidle = 0;
                HourseHight = 0;
                CastleMidle = 0;
                CastleHigh = 0;
                MinisterMidle = 0;
                MinisterHigh = 0;
                KingMidle = 0;
                KingHigh = 0;
                for (int h = 0; h < 8; h++)
                {
                    for (int s = 0; s < 8; s++)
                    {
                        if (TabS[h, s] == 1)
                        {
                            SodierMidle++;
                            SodierHigh++;
                        }
                        else if (TabS[h, s] == 2)
                        {
                            ElefantMidle++;
                            ElefantHigh++;
                        }
                        else if (TabS[h, s] == 3)
                        {
                            HourseMidle++;
                            HourseHight++;
                        }
                        else if (TabS[h, s] == 4)
                        {
                            CastleMidle++;
                            CastleHigh++;
                        }
                        else if (TabS[h, s] == 5)
                        {
                            MinisterMidle++;
                            MinisterHigh++;
                        }
                        else if (TabS[h, s] == 6)
                        {
                            KingMidle++;
                            KingHigh++;
                        }
                        else
                            if (TabS[h, s] == -1)
                        {
                            SodierHigh++;
                        }
                        else if (TabS[h, s] == -2)
                        {
                            ElefantHigh++;
                        }
                        else if (TabS[h, s] == -3)
                        {
                            HourseHight++;
                        }
                        else if (TabS[h, s] == -4)
                        {
                            CastleHigh++;
                        }
                        else if (TabS[h, s] == -5)
                        {
                            MinisterHigh++;
                        }
                        else if (TabS[h, s] == -6)
                        {
                            KingHigh++;
                        }
                    }
                }
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
        }

        public string AsS(int i, int j, int ii, int jj)
        {
            object o = new object();
            lock (o)
            {
                OutP = Alphabet(i) + Number(j) + Alphabet(ii) + Number(jj);
                return OutP;
            }
        }

        public ThinkingChess()
        { }

        //Constructor
        public ThinkingChess(int iInde, int KindO, int CurrentAStarGredy, bool MovementsAStarGreedyHeuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments, int i, int j)
        {
            object O = new object();
            lock (O)
            {
                //Initiate Variables.

                iIndex = iInde;
                CurrentAStarGredyMax = CurrentAStarGredy;
                MovementsAStarGreedyHeuristicFoundT = MovementsAStarGreedyHeuristicTFou;
                IgnoreSelfObjectsT = IgnoreSelfObject;
                UsePenaltyRegardMechnisamT = UsePenaltyRegardMechnisa;
                BestMovmentsT = BestMovment;
                PredictHeuristicT = PredictHurist;
                OnlySelfT = OnlySel;
                AStarGreedyHeuristicT = AStarGreedyHuris;
                ArrangmentsChanged = Arrangments;
                Row = i;
                Column = j;
                //Clear Dearty Part.
                if (KindO == 1)
                {
                    TableListSolder = new List<int[,]>();
                    RowColumnSoldier = new List<int[]>();
                    HitNumberSoldier = new List<int>();
                    HeuristicListSolder = new List<double[]>();
                    PenaltyRegardListSolder = new List<LearningMachine.QuantumAtamata>();
                }
                else
                    if (KindO == 2)
                {
                    TableListElefant = new List<int[,]>();
                    RowColumnElefant = new List<int[]>();
                    HitNumberElefant = new List<int>();
                    HeuristicListElefant = new List<double[]>();
                    PenaltyRegardListElefant = new List<LearningMachine.QuantumAtamata>();
                }
                else
                    if (KindO == 3)
                {
                    TableListHourse = new List<int[,]>();
                    RowColumnHourse = new List<int[]>();
                    HitNumberHourse = new List<int>();
                    HeuristicListHourse = new List<double[]>();
                    PenaltyRegardListHourse = new List<LearningMachine.QuantumAtamata>();
                }
                else
                    if (KindO == 4)
                {
                    TableListCastle = new List<int[,]>();
                    RowColumnCastle = new List<int[]>();
                    HitNumberCastle = new List<int>();
                    HeuristicListCastle = new List<double[]>();
                    PenaltyRegardListCastle = new List<LearningMachine.QuantumAtamata>();
                }
                else
                    if (KindO == 5)
                {
                    TableListMinister = new List<int[,]>();
                    RowColumnMinister = new List<int[]>();
                    HitNumberMinister = new List<int>();
                    HeuristicListMinister = new List<double[]>();
                    PenaltyRegardListMinister = new List<LearningMachine.QuantumAtamata>();
                }
                else if (KindO == 6)
                {
                    TableListKing = new List<int[,]>();
                    RowColumnKing = new List<int[]>();
                    HitNumberKing = new List<int>();
                    HeuristicListKing = new List<double[]>();
                    PenaltyRegardListKing = new List<LearningMachine.QuantumAtamata>();
                }
                else if (KindO == 7 || KindO == -7)
                {
                    TableListCastling = new List<int[,]>();
                    RowColumnCastling = new List<int[]>();
                    HitNumberCastling = new List<int>();
                    HeuristicListCastling = new List<double[]>();
                    PenaltyRegardListCastling = new List<LearningMachine.QuantumAtamata>();
                }
                KillerAtThinking = new List<int>();
                //if (AStarGreedy == null)
                AStarGreedy = new List<AllDraw>();
                MovableAllObjectsList.Clear();
                HeuristicDoubleDefenceIndexInOnGame.Clear();
                HeuristicReducedAttackedIndexInOnGame.Clear();
                AchmazPure.Clear();
                AchmazReduced.Clear();
                WinChiled.Clear();
                LoseChiled.Clear();
                HeuristicAllSupport.Clear();
                HeuristicAllReducedSupport.Clear();
                HeuristicAllAttacked.Clear();
                HeuristicAllReducedAttacked.Clear();
                HeuristicAllMove.Clear();
                HeuristicAllReducedMove.Clear();
                KishSelf.Clear();
                KishEnemy.Clear();
                IsThereMateOfEnemy.Clear();
                IsThereMateOfSelf.Clear();
                IsThereCheckOfEnemy.Clear();
                IsThereCheckOfSelf.Clear();
                LearningVarsObject.Clear();

                //Network  LearningMachine.QuantumAtamata Book Initiate For Every Clone.
            }
        }

        //Constructor
        public ThinkingChess(int iInde, int KindO, int CurrentAStarGredy, bool MovementsAStarGreedyHeuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments, int i, int j, Color a, int[,] Tab, int Ma, int Ord, bool ThinkingBeg, int CurA, int ThingN, int Kin)
        {
            object O = new object();
            lock (O)
            {
                iIndex = iInde;
                CurrentAStarGredyMax = CurrentAStarGredy;
                MovementsAStarGreedyHeuristicFoundT = MovementsAStarGreedyHeuristicTFou;
                IgnoreSelfObjectsT = IgnoreSelfObject;
                UsePenaltyRegardMechnisamT = UsePenaltyRegardMechnisa;
                BestMovmentsT = BestMovment;
                PredictHeuristicT = PredictHurist;
                OnlySelfT = OnlySel;
                AStarGreedyHeuristicT = AStarGreedyHuris;
                //Initiate Variables.
                ArrangmentsChanged = Arrangments;
                Kind = Kin;
                SetObjectNumbers(Tab);
                AStarGreedy = new List<AllDraw>();
                ThingsNumber = ThingN;
                CurrentArray = CurA;
                if (KindO == 1)
                {
                    TableListSolder = new List<int[,]>();
                    RowColumnSoldier = new List<int[]>();
                    HitNumberSoldier = new List<int>();
                    HeuristicListSolder = new List<double[]>();
                    PenaltyRegardListSolder = new List<LearningMachine.QuantumAtamata>();
                }
                else
                       if (KindO == 2)
                {
                    TableListElefant = new List<int[,]>();
                    RowColumnElefant = new List<int[]>();
                    HitNumberElefant = new List<int>();
                    HeuristicListElefant = new List<double[]>();
                    PenaltyRegardListElefant = new List<LearningMachine.QuantumAtamata>();
                }
                else
                       if (KindO == 3)
                {
                    TableListHourse = new List<int[,]>();
                    RowColumnHourse = new List<int[]>();
                    HitNumberHourse = new List<int>();
                    HeuristicListHourse = new List<double[]>();
                    PenaltyRegardListHourse = new List<LearningMachine.QuantumAtamata>();
                }
                else
                       if (KindO == 4)
                {
                    TableListCastle = new List<int[,]>();
                    RowColumnCastle = new List<int[]>();
                    HitNumberCastle = new List<int>();
                    HeuristicListCastle = new List<double[]>();
                    PenaltyRegardListCastle = new List<LearningMachine.QuantumAtamata>();
                }
                else
                       if (KindO == 5)
                {
                    TableListMinister = new List<int[,]>();
                    RowColumnMinister = new List<int[]>();
                    HitNumberMinister = new List<int>();
                    HeuristicListMinister = new List<double[]>();
                    PenaltyRegardListMinister = new List<LearningMachine.QuantumAtamata>();
                }
                else if (KindO == 6)
                {
                    TableListKing = new List<int[,]>();
                    RowColumnKing = new List<int[]>();
                    HitNumberKing = new List<int>();
                    HeuristicListKing = new List<double[]>();
                    PenaltyRegardListKing = new List<LearningMachine.QuantumAtamata>();
                }
                else if (KindO == 7 || KindO == -7)
                {
                    TableListCastling = new List<int[,]>();
                    RowColumnCastling = new List<int[]>();
                    HitNumberCastling = new List<int>();
                    HeuristicListCastling = new List<double[]>();
                    PenaltyRegardListCastling = new List<LearningMachine.QuantumAtamata>();
                }
                KillerAtThinking = new List<int>();
                //if (AStarGreedy == null)
                AStarGreedy = new List<AllDraw>();

                Row = i;
                Column = j;
                color = a;
                Max = Ma;
                TableT = Tab;
                IndexSoldier = 0;
                IndexElefant = 0;
                IndexHourse = 0;
                IndexCastle = 0;
                IndexMinister = 0;
                IndexKing = 0;
                IndexCastling = 0;
                TableConst = CloneATable(Tab);
                Order = Ord;
                ThinkingBegin = ThinkingBeg;
                MovableAllObjectsList.Clear();
                HeuristicDoubleDefenceIndexInOnGame.Clear();
                HeuristicReducedAttackedIndexInOnGame.Clear();
                AchmazPure.Clear();
                AchmazReduced.Clear();
                WinChiled.Clear();
                LoseChiled.Clear();
                HeuristicAllSupport.Clear();
                HeuristicAllReducedSupport.Clear();
                HeuristicAllAttacked.Clear();
                HeuristicAllReducedAttacked.Clear();
                HeuristicAllMove.Clear();
                HeuristicAllReducedMove.Clear();
                KishSelf.Clear();
                KishEnemy.Clear();
                IsThereMateOfEnemy.Clear();
                IsThereMateOfSelf.Clear();
                IsThereCheckOfEnemy.Clear();
                IsThereCheckOfSelf.Clear();
                LearningVarsObject.Clear();
            }
        }

        //Clone A Table
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

        //Clone A List.
        private int[] CloneAList(int[] Tab, int Count)
        {
            object O = new object();
            lock (O)
            {
                //Initiate new Objects.
                int[] Table = new int[Count];
                //Asigne to new Objects.
                for (int i = 0; i < Count; i++)
                {
                    Table[i] = Tab[i];
                }
                //Retrun new Object.
                return Table;
            }
        }

        private double[] CloneAList(double[] Tab, int Count)
        {
            object O = new object();
            lock (O)
            {
                //Initiate new Objects.
                double[] Table = new double[Count];
                //Asigne to new Objects.
                for (int i = 0; i < Count; i++)
                {
                    Table[i] = Tab[i];
                }
                //Retrun new Object.
                return Table;
            }
        }

        ///Clone a Copy.
        public void Clone(ref ThinkingChess AA)
        {
            object O = new object();
            lock (O)
            {
                //Assignment Content to New Content Object.
                //Initaite New Object.
                if (AA == null)
                {
                    AA = new ThinkingChess(iIndex, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Row, Column//, Kind
                        );
                }

                AA.ArrangmentsChanged = ArrangmentsChanged;
                //When Depth Object is not NULL.
                if (AStarGreedy.Count != 0)
                {
                    AA.AStarGreedy = new System.Collections.Generic.List<AllDraw>();
                    //For All Depth(s).
                    for (int i = 0; i < AStarGreedy.Count; i++)
                    {
                        //Clone a Copy From Depth Objects.
                        AStarGreedy[i].Clone(AA.AStarGreedy[i]);
                    }
                }
                //For All Moves Indexx Solders List Count.
                for (int j = 0; j < RowColumnSoldier.Count; j++)
                {
                    //Add a Clone To New Solder indexx Object.
                    AA.RowColumnSoldier.Add(CloneAList(RowColumnSoldier[j], 2));
                }
                //For All Castle List Count.
                for (int j = 0; j < RowColumnCastle.Count; j++)
                {
                    //Add a Clone to New Castle index Objects List.
                    AA.RowColumnCastle.Add(CloneAList(RowColumnCastle[j], 2));
                }
                //For All Elephant index List Count.
                for (int j = 0; j < RowColumnElefant.Count; j++)
                {
                    //Add a Clone to New Elephant Object List.
                    AA.RowColumnElefant.Add(CloneAList(RowColumnElefant[j], 2));
                }
                //For All Hourse index List Count.
                for (int j = 0; j < RowColumnHourse.Count; j++)
                {
                    //Add a Clone to New Hourse index List.
                    AA.RowColumnHourse.Add(CloneAList(RowColumnHourse[j], 2));
                }
                //For All King index List Count.
                for (int j = 0; j < RowColumnKing.Count; j++)
                {
                    //Add a Clone To New King Object List.
                    AA.RowColumnKing.Add(CloneAList(RowColumnKing[j], 2));
                }
                //For All Minister index Count.
                for (int j = 0; j < RowColumnMinister.Count; j++)
                {
                    //Add a Clone To Minister New index List.
                    AA.RowColumnMinister.Add(CloneAList(RowColumnMinister[j], 2));
                }

                for (int j = 0; j < RowColumnCastling.Count; j++)
                {
                    //Add a Clone To New King Object List.
                    AA.RowColumnCastling.Add(CloneAList(RowColumnCastling[j], 2));
                }
                //Assgine thread.
                //Create and Initiate new Table Object.
                AA.TableT = new int[8, 8];
                //Create and Initaite New Table Object.
                AA.TableConst = new int[8, 8];
                //if Table is not NULL>
                if (TableT != null)
                {
                    //For All Items in Table Object.
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            //Assgine Table items in New Table Object.
                            AA.TableT[i, j] = TableT[i, j];
                        }
                    }
                }
                //If Table is Not Null.
                if (TableConst != null)
                {
                    //For All Items in Table Object.
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            //Assignm Items in New Table Object.
                            AA.TableConst[i, j] = TableConst[i, j];
                        }
                    }
                }
                //For All Table State Movements in Castles Objects.
                for (int i = 0; i < TableListCastle.Count; i++)
                {
                    //Add aclon of a Table in New Briges Table List.
                    AA.TableListCastle.Add(CloneATable(TableListCastle[i]));
                }
                //For All Table List Movements in  Elephant Objects
                for (int i = 0; i < TableListElefant.Count; i++)
                {
                    //Add a Clone of Tables in Elephant Mevments Obejcts List To New One.
                    AA.TableListElefant.Add(CloneATable(TableListElefant[i]));
                }
                //For All Hourse Table Movemnts items.
                for (int i = 0; i < TableListHourse.Count; i++)
                {
                    //Add a Clone of Hourse Table Movement in New List.
                    AA.TableListHourse.Add(CloneATable(TableListHourse[i]));
                }
                //For All King Tables Movment Count.
                for (int i = 0; i < TableListKing.Count; i++)
                {
                    //Add a Clone To New King Table List.
                    AA.TableListKing.Add(CloneATable(TableListKing[i]));
                }
                //For All Minister Table Movment Items.
                for (int i = 0; i < TableListMinister.Count; i++)
                {
                    //Add a clone To New Minister Table Movment List.
                    AA.TableListMinister.Add(CloneATable(TableListMinister[i]));
                }
                //For All Solder Table Movment Count.
                for (int i = 0; i < TableListSolder.Count; i++)
                {
                    //Add a Clone of Table item to New Table List Movments.
                    AA.TableListSolder.Add(CloneATable(TableListSolder[i]));
                }

                for (int i = 0; i < TableListCastling.Count; i++)
                {
                    //Add a Clone To New King Table List.
                    AA.TableListCastling.Add(CloneATable(TableListCastling[i]));
                }
                //For All Solder Husrist List Count.
                for (int i = 0; i < HeuristicListSolder.Count; i++)
                {
                    //Ad a Clone of Hueristic Solders To New List.
                    AA.HeuristicListSolder.Add(CloneAList(HeuristicListSolder[i], 4));
                }
                //For All Elephant Heuristic List Count.
                for (int i = 0; i < HeuristicListElefant.Count; i++)
                {
                    //Add A Clone of Copy to New Elephant Heuristic List.
                    AA.HeuristicListElefant.Add(CloneAList(HeuristicListElefant[i], 4));
                }
                //For All Hours Heuristic Hourse Count.
                for (int i = 0; i < HeuristicListHourse.Count; i++)
                {
                    //Add a Clone of Copy To New Housre Heuristic List.
                    AA.HeuristicListHourse.Add(CloneAList(HeuristicListHourse[i], 4));
                }
                //For All Castles Heuristic List Count.
                for (int i = 0; i < HeuristicListCastle.Count; i++)
                {
                    //Add a Clone of Copy to New Castles Heuristic List.
                    AA.HeuristicListCastle.Add(CloneAList(HeuristicListCastle[i], 4));
                }
                //For All Minister Heuristic List Count.
                for (int i = 0; i < HeuristicListMinister.Count; i++)
                {
                    //Add a Clone of Copy to New Minister List.
                    AA.HeuristicListMinister.Add(CloneAList(HeuristicListMinister[i], 4));
                }
                //For All King Husrict List Items.
                for (int i = 0; i < HeuristicListKing.Count; i++)
                {
                    //Add a Clone of Copy to New King Hursitic List.
                    AA.HeuristicListKing.Add(CloneAList(HeuristicListKing[i], 4));
                }

                for (int i = 0; i < HeuristicListCastling.Count; i++)
                {
                    //Add a Clone of Copy to New King Hursitic List.
                    AA.HeuristicListCastling.Add(CloneAList(HeuristicListCastling[i], 4));
                }
                //Initiate and create Penalty Solder List.
                AA.PenaltyRegardListSolder = new List<LearningMachine.QuantumAtamata>();
                //For All Solder Penalty List Count.
                if (Kind == 1)
                {
                    AA.PenaltyRegardListSolder = new List<LearningMachine.QuantumAtamata>();
                    for (int i = 0; i < PenaltyRegardListSolder.Count; i++)
                    {
                        //Initiate a new  LearningMachine.QuantumAtamata Object
                        //Add New Object Create to New Penalty Solder List.
                        AA.PenaltyRegardListSolder.Add(PenaltyRegardListSolder[i]);
                    }
                }
                else
                if (Kind == 2)
                {
                    //Initaite and Create Elephant Penalty List Object.
                    AA.PenaltyRegardListElefant = new List<LearningMachine.QuantumAtamata>();
                    //For All Elepahtn Penalty List Count.
                    for (int i = 0; i < PenaltyRegardListElefant.Count; i++)
                    {
                        //Initiate a new  LearningMachine.QuantumAtamata Object
                        //Clone a Copy Of Penalty Elephant.
                        AA.PenaltyRegardListElefant.Add(PenaltyRegardListElefant[i]);
                        //Add New Object Create to New Penalty Elephant List.
                    }
                }
                else
            if (Kind == 3)
                {
                    //Initaite and Create Hourse Penalty List Object.
                    AA.PenaltyRegardListHourse = new List<LearningMachine.QuantumAtamata>();
                    //For All Solder Hourse List Count.
                    for (int i = 0; i < PenaltyRegardListHourse.Count; i++)
                    {
                        //Initiate a new  LearningMachine.QuantumAtamata Object
                        LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                        //Clone a Copy Of Penalty Hourse.
                        //Add New Object Create to New Penalty Hourse List.
                        AA.PenaltyRegardListHourse.Add(PenaltyRegardListHourse[i]);
                    }
                }
                else
                if (Kind == 4)
                {
                    //Initaite and Create Castles Penalty List Object.
                    AA.PenaltyRegardListCastle = new List<LearningMachine.QuantumAtamata>();
                    //For All Solder Castle List Count.
                    for (int i = 0; i < PenaltyRegardListCastle.Count; i++)
                    {
                        //Initiate a new  LearningMachine.QuantumAtamata Object
                        //Clone a Copy Of Penalty Castles.
                        //Add New Object Create to New Penalty Castles List.
                        AA.PenaltyRegardListCastle.Add(PenaltyRegardListCastle[i]);
                    }
                }
                else
                if (Kind == 5)
                {
                    //Initaite and Create Minister Penalty List Object.
                    AA.PenaltyRegardListMinister = new List<LearningMachine.QuantumAtamata>();
                    //For All Solder Minster List Count.
                    for (int i = 0; i < PenaltyRegardListMinister.Count; i++)
                    {
                        //Initiate a new  LearningMachine.QuantumAtamata Object
                        //Clone a Copy Of Penalty Minsiter.
                        //Add New Object Create to New Penalty Minsietr List.
                        AA.PenaltyRegardListMinister.Add(PenaltyRegardListMinister[i]);
                    }
                }
                else
                if (Kind == 7 || Kind == -7)
                {
                    //Initaite and Create King Penalty List Object.
                    AA.PenaltyRegardListCastling = new List<LearningMachine.QuantumAtamata>();
                    //For All Solder King List Count.
                    for (int i = 0; i < PenaltyRegardListCastling.Count; i++)
                    {
                        //Initiate a new  LearningMachine.QuantumAtamata Object
                        //Clone a Copy Of Penalty King.
                        //Add New Object Create to New Penalty King List.
                        AA.PenaltyRegardListCastling.Add(PenaltyRegardListCastling[i]);
                    }
                }
                else
                if (Kind == 6)
                {
                    //Initaite and Create King Penalty List Object.
                    AA.PenaltyRegardListKing = new List<LearningMachine.QuantumAtamata>();
                    //For All Solder King List Count.
                    for (int i = 0; i < PenaltyRegardListKing.Count; i++)
                    {
                        //Initiate a new  LearningMachine.QuantumAtamata Object
                        //Clone a Copy Of Penalty King.
                        //Add New Object Create to New Penalty King List.
                        AA.PenaltyRegardListKing.Add(PenaltyRegardListKing[i]);
                    }
                }
                //Iniktiate Same Obejcts to New Same Obejcts.
                AA.AStarGreedy = AStarGreedy;
                AA.CastleValue = CastleValue;
                AA.color = color;
                AA.Column = Column;
                AA.CurrentArray = CurrentArray;
                AA.CurrentColumn = CurrentColumn;
                AA.CurrentRow = CurrentRow;
                AA.ElefantValue = ElefantValue;
                AA.ExistingOfEnemyHiiting = ExistingOfEnemyHiiting;
                AA.HourseValue = HourseValue;
                AA.IgnoreObjectDangour = IgnoreObjectDangour;
                AA.IndexCastle = IndexCastle;
                AA.IndexElefant = IndexElefant;
                AA.IndexHourse = IndexHourse;
                AA.IndexKing = IndexKing;
                AA.IndexCastling = IndexCastling;
                AA.IndexMinister = IndexMinister;
                AA.IndexSoldier = IndexSoldier;
                AA.IsCheck = IsCheck;
                AA.Kind = Kind;
                AA.KingValue = KingValue;
                AA.CheckMateAStarGreedy = CheckMateAStarGreedy;
                AA.CheckMateOcuured = CheckMateOcuured;
                AA.Max = Max;
                AA.MinisterValue = MinisterValue;
                AA.Order = Order;
                AA.Row = Row;
                AA.SodierValue = SodierValue;
                AA.ThingsNumber = ThingsNumber;
                AA.ThinkingBegin = ThinkingBegin;
                AA.ThinkingFinished = ThinkingFinished;
            }
        }

        private bool IsDistributedObjectAttackNonDistributedEnemyObject(int[,] Table, int Ord, int RowS, int ColS, int RowD, int ColD)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                if ((Table[RowS, ColS] != TableInitiation[RowS, ColS]) && (Table[RowD, ColD] == TableInitiation[RowD, ColD]))
                {
                    Is = true;
                }

                return Is;
            }
        }

        ///Heuristic of Attacker.
        private double HeuristicAttack(ref double HA, int[,] Table, int Ord, Color aa, int RowS, int ColS, int RowD, int ColD)
        {
            object O = new object();
            lock (O)
            {
                int HeuristicAttackValue = 0;
                ////double HA =1;
                int DumOrder = Order;
                int DummyOrder = Order;
                int DummyCurrentOrder = ChessRules.CurrentOrder;
                ///When AStarGreedy Heuristic is Not Assigned.
                //When Heuristic is not Greedy.
                if (!AStarGreedyHeuristicT)
                {
                    Order = new int();
                    Color a = new Color();
                    a = aa;
                    if (RowS == RowD && ColS == ColD)
                    {
                        return HeuristicAttackValue;
                    }

                    int Sign = new int();
                    Order = DummyOrder;
                    ///When Attack is true. means [RowD,ColD] is in Attacked  [RowS,ColS].
                    ///What is Attack!
                    ///Ans:When [RowD,ColD] is Attacked [RowS,ColS] continue true when enemy is located in [RowD,ColD].
                    if (Table[RowD, ColD] > 0 && DummyOrder == -1 && Table[RowS, ColS] < 0)
                    {
                        Order = -1;
                        Sign = AllDraw.SignAttack;
                        ChessRules.CurrentOrder = -1;
                        a = Color.Brown;
                    }
                    else if (Table[RowD, ColD] < 0 && DummyOrder == 1 && Table[RowS, ColS] > 0)
                    {
                        Order = 1;
                        Sign = AllDraw.SignAttack;
                        ChessRules.CurrentOrder = -1;
                        a = Color.Gray;
                    }
                    else
                    {
                        return HeuristicAttackValue;
                    }
                    //For Attack Movments.- GetObjectValueHeuristic
                    object O1 = new object();
                    lock (O1)
                    {
                        //if (Before)
                        {
                            bool ab = false;
                            Task<bool> th = Task.Factory.StartNew(() => ab = IsDistributedObjectAttackNonDistributedEnemyObject(CloneATable(Table), Ord, RowS, ColS, RowD, ColD));
                            th.Wait();
                            th.Dispose();
                            if (ab)
                            {
                                HA += Rational(HA, RationalPenalty) * RationalPenalty; ;
                            }
                            else
                            {
                                Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), RowS, ColS, RowD, ColD, a, Order));
                                th1.Wait();
                                th1.Dispose();

                                if (ab)
                                {
                                    HA += Rational(HA, RationalRegard) * RationalRegard;
                                    //When there is supporter of attacked Objects take Heuristic negative else take muliply sign and muliply Heuristic.
                                    int Supported = new int();
                                    int SupportedS = new int();
                                    Supported = 0;
                                    SupportedS = 0;
                                    //For All Enemy Obejcts.
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, g =>
                                    for (int g = 0; g < 8; g++)
                                    {
                                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, h =>
                                        for (int h = 0; h < 8; h++)
                                        {
                                            //Ignore Of Self Objects.
                                            if (Order == 1 && Table[g, h] >= 0)
                                            {
                                                continue;
                                            }

                                            if (Order == -1 && Table[g, h] <= 0)
                                            {
                                                continue;
                                            }

                                            Color aaa = new Color();
                                            //Assgin Enemy ints.
                                            aaa = Color.Gray;
                                            if (Order * -1 == -1)
                                            {
                                                aaa = Color.Brown;
                                            }
                                            else
                                            {
                                                aaa = Color.Gray;
                                            }
                                            //When Enemy is Supported.
                                            bool A = new bool();
                                            bool B = new bool();
                                            object O2 = new object();
                                            lock (O2)
                                            {
                                                Task<bool> th2 = Task.Factory.StartNew(() => A = Support(CloneATable(Table), g, h, RowD, ColD, aaa, Order * -1));
                                                th2.Wait();
                                                th2.Dispose();
                                                Task<bool> th3 = Task.Factory.StartNew(() => B = Support(CloneATable(Table), g, h, RowS, ColS, a, Order));
                                                th3.Wait();
                                                th3.Dispose();
                                            }
                                            //When Enemy is Supported.
                                            if (B)
                                            {
                                                //Assgine variable.
                                                SupportedS += System.Math.Abs(Table[g, h]);
                                            }
                                            if (A)
                                            {
                                                //Assgine variable.
                                                Supported += System.Math.Abs(Table[g, h]);
                                                continue;
                                            }
                                        }
                                    }

                                    if (SupportedS > Supported && (System.Math.Abs(Table[RowS, ColS]) < System.Math.Abs(Table[RowD, ColD])))
                                    {
                                        HA += Rational(HA, (int)System.Math.Pow(2, SupportedS)) * (int)System.Math.Pow(2, SupportedS);
                                    }
                                    else
                                    if (SupportedS < Supported)
                                    {
                                        HA += Rational(HA, (int)(-1 * System.Math.Pow(2, Supported))) * (int)(-1 * System.Math.Pow(2, Supported));
                                    }
                                }
                            }
                        }
                    }
                }
                //For All Table Homes find Attack Heuristic.
                else
                {
                    Order = new int();
                    Color a = new Color();
                    a = aa;
                    //Ignore of Current.
                    if (RowS == RowD && ColS == ColD)
                    {
                        return HeuristicAttackValue;
                    }

                    Order = DummyOrder;
                    int Sign = 1;
                    ///When Attack is true. means [RowD,ColD] is in Attacked  [RowS,ColS].
                    ///What is Attack!
                    ///Ans:When [RowD,ColD] is Attacked [RowS,ColS] continue true when enemy is located in [RowD,ColD].
                    if (Table[RowD, ColD] > 0 && DummyOrder == -1 && Table[RowS, ColS] < 0)
                    {
                        Order = -1;
                        Sign = AllDraw.SignAttack;
                        ChessRules.CurrentOrder = -1;
                        a = Color.Brown;
                    }
                    else if (Table[RowD, ColD] < 0 && DummyOrder == 1 && Table[RowS, ColS] > 0)
                    {
                        Order = 1;
                        Sign = AllDraw.SignAttack;
                        ChessRules.CurrentOrder = -1;
                        a = Color.Gray;
                    }
                    else
                    {
                        return HeuristicAttackValue;
                    }

                    //For Attack Movments.
                    object O2 = new object();
                    lock (O2)
                    {
                        //if (Before)
                        {
                            bool ab = false;
                            Task<bool> th = Task.Factory.StartNew(() => ab = IsDistributedObjectAttackNonDistributedEnemyObject(CloneATable(Table), Ord, RowS, ColS, RowD, ColD));
                            th.Wait();
                            th.Dispose();
                            if (ab)
                            {
                                HA += Rational(HA, RationalPenalty) * RationalPenalty; ;
                            }

                            Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), RowS, ColS, RowD, ColD, a, Order));
                            th1.Wait();
                            th1.Dispose();

                            if (ab)
                            {
                                HA += Rational(HA, RationalRegard) * RationalRegard;

                                //When there is supporter of attacked Objects take Heuristic negative else take muliply sign and muliply Heuristic.
                                //For All Enemy Obejcts.
                                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, g =>
                                int Supported = new int();
                                int SupportedS = new int();
                                Supported = 0;
                                SupportedS = 0;
                                //For All Enemy Obejcts.
                                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, g =>
                                for (int g = 0; g < 8; g++)
                                {
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, h =>
                                    for (int h = 0; h < 8; h++)
                                    {
                                        //Ignore Of Self Objects.
                                        if (Order == 1 && Table[g, h] >= 0)
                                        {
                                            continue;
                                        }

                                        if (Order == -1 && Table[g, h] <= 0)
                                        {
                                            continue;
                                        }

                                        Color aaa = new Color();
                                        //Assgin Enemy ints.
                                        aaa = Color.Gray;
                                        if (Order * -1 == -1)
                                        {
                                            aaa = Color.Brown;
                                        }
                                        else
                                        {
                                            aaa = Color.Gray;
                                        }
                                        //When Enemy is Supported.
                                        bool A = new bool();
                                        bool B = new bool();
                                        object O12 = new object();
                                        lock (O12)
                                        {
                                            Task<bool> th2 = Task.Factory.StartNew(() => A = Support(CloneATable(Table), g, h, RowD, ColD, aaa, Order * -1));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<bool> th3 = Task.Factory.StartNew(() => B = Support(CloneATable(Table), g, h, RowS, ColS, a, Order));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                        if (B)
                                        {
                                            //Assgine variable.
                                            SupportedS += System.Math.Abs(Table[g, h]);
                                        }
                                        if (A)
                                        {
                                            //Assgine variable.
                                            Supported += System.Math.Abs(Table[g, h]);
                                            continue;
                                        }
                                    }
                                }

                                if (SupportedS > Supported && (System.Math.Abs(Table[RowS, ColS]) < System.Math.Abs(Table[RowD, ColD])))
                                {
                                    HA += Rational(HA, (int)System.Math.Pow(2, SupportedS)) * (int)System.Math.Pow(2, SupportedS);
                                }
                                else
                                if (SupportedS < Supported)
                                {
                                    HA += Rational(HA, (int)(-1 * System.Math.Pow(2, Supported))) * (int)(-1 * System.Math.Pow(2, Supported));
                                }
                            }
                        }
                    }
                }
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                Order = DumOrder;
                //Initiate to Begin Call Orders.
                return 1 * HA;
            }
        }

        //Attacks Of Enemy that is not Supported.QC_OK
        private bool InAttackEnemyThatIsNotSupported(int Kilded, int[,] Table, int Order, Color a, int i, int j, int ii, int jj)
        {
            object O = new object();
            lock (O)
            {
                //Initiate Global Variables.
                int Ord = Order;
                bool S = true;
                bool EnemyNotSupported = true;
                if (Kilded != 0)
                {
                    EnemyNotSupported = true;
                    //Enemy
                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, RowS =>
                    for (int RowS = 0; RowS < 8; RowS++)
                    {
                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, ColS =>
                        for (int ColS = 0; ColS < 8; ColS++)
                        {
                            if (!EnemyNotSupported)
                            {
                                continue;
                            }

                            int Order1 = new int();
                            Order1 = Ord;
                            int[,] Tab = new int[8, 8];
                            ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, ik =>
                            for (int ik = 0; ik < 8; ik++)
                            {
                                if (!EnemyNotSupported)
                                {
                                    continue;
                                }

                                for (int jk = 0; jk < 8; jk++)
                                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, jk =>
                                {
                                    object O3 = new object();
                                    lock (O3)
                                    {
                                        Tab[ik, jk] = Table[ik, jk];
                                    }
                                }
                            }
                            object O2 = new object();
                            lock (O2)
                            {
                                Tab[i, j] = Tab[ii, jj];
                                Tab[ii, jj] = Kilded;
                            }
                            //Ignore of Current
                            if (Order1 == 1 && Tab[RowS, ColS] >= 0)
                            {
                                continue;
                            }
                            else
                                    if (Order1 == -1 && Tab[RowS, ColS] <= 0)
                            {
                                continue;
                            }

                            a = Color.Gray;
                            if (Order1 * -1 == -1)
                            {
                                a = Color.Brown;
                            }
                            //When Enemy is Supported.
                            object O1 = new object();
                            lock (O1)
                            {
                                bool ab = false;
                                Task<bool> th = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), RowS, ColS, ii, jj, a, Order1 * -1)
                                        && ObjectValueCalculator(CloneATable(Tab), i, j) >= ObjectValueCalculator(CloneATable(Tab), ii, jj));

                                th.Wait();
                                th.Dispose();
                                if (ab)
                                //Wehn [i,j] (Current) is less or equal than [ii,jj] (Enemy)
                                //EnemyNotSupported method Should continue [valid]
                                //By this situation continue not valid
                                {
                                    EnemyNotSupported = false;
                                    continue;
                                }
                            }
                        }
                        if (!EnemyNotSupported)
                        {
                            continue;
                        }
                    }
                    if (EnemyNotSupported)
                    {
                        S = false;
                    }
                }
                //When S is not valid there is one node in [EnemyNotSupported]
                if (!S)
                {
                    Order = Ord;
                    return true;
                }
                Order = Ord;
                return false;
            }
        }

        //When at least one Attacked Self Object return true.
        private bool InAttackEnemyThatIsNotSupportedAll(bool EnemyIsValuable, int[,] Table, int Order, Color a, ref List<int[]> ValuableEnemyNotSupported)
        {
            object O = new object();
            lock (O)
            {
                //Initiate Global Variables.
                int Ord = Order;
                object O4 = new object();
                lock (O4)
                {
                    int[,] Tab = new int[8, 8];
                    for (int ik = 0; ik < 8; ik++)
                    {
                        for (int jk = 0; jk < 8; jk++)
                        {
                            Tab[ik, jk] = Table[ik, jk];
                        }
                    }

                    bool S = true;
                    bool EnemyNotSupported = true;
                    bool InAttackedNotEnemySupported = false;
                    //For Current
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            //Ignore of Enemy
                            if (Order == 1 && Tab[i, j] <= 0)
                            {
                                continue;
                            }
                            else
                                if (Order == -1 && Tab[i, j] >= 0)
                            {
                                continue;
                            }
                            //For Enemies.
                            for (int ii = 0; ii < 8; ii++)
                            {
                                for (int jj = 0; jj < 8; jj++)
                                {
                                    //Ignore of Curent
                                    if (Order == 1 && Tab[ii, jj] >= 0)
                                    {
                                        continue;
                                    }
                                    else
                                        if (Order == -1 && Tab[ii, jj] <= 0)
                                    {
                                        continue;
                                    }

                                    object O1 = new object();
                                    lock (O1)
                                    {
                                        bool ab = false;
                                        List<int[]> ValuableEnemyNotSupportedA = ValuableEnemyNotSupported;
                                        Task<bool> th = Task.Factory.StartNew(() => ab = EnemyIsValuable && (!IsObjectValaubleObjectEnemy(Tab[ii, jj], ref ValuableEnemyNotSupportedA)));
                                        th.Wait();
                                        th.Dispose();

                                        ValuableEnemyNotSupported = ValuableEnemyNotSupportedA;

                                        if (ab)
                                        {
                                            continue;
                                        }

                                        EnemyNotSupported = true;
                                        InAttackedNotEnemySupported = false;
                                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Tab), i, j, ii, jj, a, Order));
                                        th1.Wait();
                                        th1.Dispose();

                                        if (ab)
                                        {
                                            InAttackedNotEnemySupported = true;
                                            //Enemy
                                            for (int RowS = 0; RowS < 8; RowS++)
                                            {
                                                for (int ColS = 0; ColS < 8; ColS++)
                                                {
                                                    //Ignore of Current
                                                    if (Order == 1 && Tab[RowS, ColS] >= 0)
                                                    {
                                                        continue;
                                                    }
                                                    else
                                                        if (Order == -1 && Tab[RowS, ColS] <= 0)
                                                    {
                                                        continue;
                                                    }

                                                    a = Color.Gray;
                                                    if (Order * -1 == -1)
                                                    {
                                                        a = Color.Brown;
                                                    }
                                                    //
                                                    Task<bool> th2 = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), RowS, ColS, ii, jj, a, Order * -1));
                                                    th2.Wait();
                                                    th2.Dispose();

                                                    if (ab
                                                        //&& (ObjectValueCalculator(CloneATable(Tab),i,j) >= ObjectValueCalculator(CloneATable(Tab),ii,jj)
                                                        //Wehn [i,j] (Current) is less or equal than [ii,jj] (Enemy)
                                                        //EnemyNotSupported method Should return [valid]
                                                        //By this situation return not valid
                                                        )
                                                    {
                                                        EnemyNotSupported = false;
                                                    }
                                                }
                                                if (!EnemyNotSupported)
                                                {
                                                    break;
                                                }
                                            }
                                        }
                                        if (EnemyNotSupported && InAttackedNotEnemySupported)
                                        {
                                            S = false;
                                            break;
                                        }
                                    }
                                }
                                if (!S)
                                {
                                    break;
                                }
                            }
                            if (!S)
                            {
                                break;
                            }
                        }
                        if (!S)
                        {
                            break;
                        }
                    }
                    //When there is at leat tow enmy of attackment.
                    if (!S)
                    {
                        Order = Ord;
                        return true;
                    }
                    Order = Ord;
                }
                return false;
            }
        }

        //When  there is more than tow self object not supported on atacked by movement return true.
        private int IsNotSafeToMoveAenemeyToAttackMoreThanTowObject(int AttackCount, int[,] Table, int Order, int i, int j, int ii, int jj)
        {
            //For All Enemie
            object O1 = new object();
            lock (O1)
            {
                //Ignore of Self
                if (Order == 1 && Table[i, j] >= 0)
                {
                    return 0;
                }
                if (Order == -1 && Table[i, j] <= 0)
                {
                    return 0;
                }
                //For All Self and Empty.
                //Ignore of Enemy.
                if (Order == 1 && Table[ii, jj] < 0)
                {
                    return 0;
                }
                if (Order == -1 && Table[ii, jj] > 0)
                {
                    return 0;
                }
                ChessRules A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[i, j], CloneATable(Table), Order * -1);
                Color a = Color.Gray;
                if (Order * -1 == -1)
                {
                    a = Color.Brown;
                }

                int[,] Tab = new int[8, 8];
                object O = new object();
                lock (O)
                {
                    for (int ik = 0; ik < 8; ik++)
                    {
                        for (int jk = 0; jk < 8; jk++)
                        {
                            Tab[ik, jk] = Table[ik, jk];
                        }
                    }
                }
                //When there is attack to some self node.
                object OO = new object();
                lock (OO)
                {
                    if (A.Rules(i, j, ii, jj, Tab[i, j]))
                    {
                        //take Move
                        Tab[ii, jj] = Tab[i, j];
                        Tab[i, j] = 0;
                        AttackCount = 0;
                        //For All Self
                        for (int RowS = 0; RowS < 8; RowS++)
                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, RowS =>
                        {
                            //if (AttackCount > 1)
                            for (int ColS = 0; ColS < 8; ColS++)
                            ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, ColS =>
                            {
                                if (AttackCount > 1)
                                {
                                    continue;
                                }
                                //Ignore of Enemy.
                                if (Order == 1 && Tab[RowS, ColS] <= 0)
                                {
                                    continue;
                                }

                                if (Order == -1 && Tab[RowS, ColS] >= 0)
                                {
                                    continue;
                                }

                                a = Color.Gray;
                                if (Order * -1 == -1)
                                {
                                    a = Color.Brown;
                                }

                                bool ab = false;
                                Task<bool> th = Task.Factory.StartNew(() => ab = Attack(CloneATable(Tab), ii, jj, RowS, ColS, a, Order * -1));
                                th.Wait();
                                th.Dispose();
                                //when there is attack to some self node.
                                if (ab)
                                {
                                    bool Supporte = false;
                                    //For All Self
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, RowD =>
                                    for (int RowD = 0; RowD < 8; RowD++)
                                    {
                                        if (AttackCount > 1)
                                        {
                                            continue;
                                        }
                                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, ColD =>
                                        for (int ColD = 0; ColD < 8; ColD++)
                                        {
                                            if (AttackCount > 1)
                                            {
                                                continue;
                                            }
                                            //Ignore of Enemy.
                                            if (Order == 1 && Tab[RowD, ColD] <= 0)
                                            {
                                                continue;
                                            }

                                            if (Order == -1 && Tab[RowD, ColD] >= 0)
                                            {
                                                continue;
                                            }

                                            a = Color.Gray;
                                            if (Order == -1)
                                            {
                                                a = Color.Brown;
                                            }
                                            //when there is attack of self node to that enemy node.
                                            Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), RowD, ColD, RowS, ColS, a, Order) || Attack(CloneATable(Tab), RowD, ColD, ii, jj, a, Order));
                                            th1.Wait();
                                            th1.Dispose();
                                            if (ab)
                                            {
                                                Supporte = true;
                                                continue;
                                            }
                                        }
                                    }
                                    if (!Supporte)
                                    {
                                        AttackCount++;
                                    }
                                }
                                else
                                {
                                    continue;
                                }

                                if (AttackCount > 1)
                                {
                                    continue;
                                }
                            }
                            if (AttackCount > 1)
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }

                return AttackCount;
            }
        }

        //Supported of Self that is Not Attacks.QC_BAD
        private bool InAttackSelfThatNotSupported(int[,] TableS, int Order, Color a, int ij, int ji, int ii, int jj)
        {
            object O = new object();
            lock (O)
            {
                //Initiate Variables.
                int[,] Tab = new int[8, 8];
                object O1 = new object();
                lock (O1)
                {
                    for (int ik = 0; ik < 8; ik++)
                    {
                        for (int jk = 0; jk < 8; jk++)
                        {
                            Tab[ik, jk] = TableS[ik, jk];
                        }
                    }

                    int Ord = Order;
                    bool SelfSupported = false;
                    bool InAttackedNotSelfSupported = false;
                    bool IsObjDangerest = false;
                    bool S = true;
                    int i = ii, j = jj;
                    //Ignore of Current
                    //For Enemy.
                    for (int RowS = 0; RowS < 8; RowS++)
                    {
                        for (int ColS = 0; ColS < 8; ColS++)
                        {
                            //Ignore of Current
                            if (Order == 1 && Tab[RowS, ColS] >= 0)
                            {
                                continue;
                            }
                            else
                            if (Order == -1 && Tab[RowS, ColS] <= 0)
                            {
                                continue;
                            }
                            //Enemy
                            a = Color.Gray;
                            if (Order * -1 == -1)
                            {
                                a = Color.Brown;
                            }

                            for (int ik = 0; ik < 8; ik++)
                            {
                                for (int jk = 0; jk < 8; jk++)
                                {
                                    Tab[ik, jk] = TableS[ik, jk];
                                }
                            }

                            InAttackedNotSelfSupported = false;
                            SelfSupported = false;
                            object OO = new object();
                            lock (OO)
                            {
                                bool ab = false;
                                Task<bool> th = Task.Factory.StartNew(() => ab = Attack(CloneATable(Tab), RowS, ColS, i, j, a, Order * -1));
                                th.Wait();
                                th.Dispose();
                                if (ab)
                                {
                                    InAttackedNotSelfSupported = true;
                                    a = Color.Gray;
                                    if (Order == -1)
                                    {
                                        a = Color.Brown;
                                    }
                                    //For Self.
                                    for (int RowD = 0; RowD < 8; RowD++)
                                    {
                                        for (int ColD = 0; ColD < 8; ColD++)
                                        {
                                            //Ignore of Enemies
                                            if (Order == 1 && Tab[RowD, ColD] <= 0)
                                            {
                                                continue;
                                            }
                                            else
                                                if (Order == -1 && Tab[RowD, ColD] >= 0)
                                            {
                                                continue;
                                            }

                                            a = Color.Gray;
                                            if (Order == -1)
                                            {
                                                a = Color.Brown;
                                            }

                                            for (int ik = 0; ik < 8; ik++)
                                            {
                                                for (int jk = 0; jk < 8; jk++)
                                                {
                                                    Tab[ik, jk] = TableS[ik, jk];
                                                }
                                            }
                                            //When there is support and cuurent is less than enemy.
                                            //method return true when is not supporte and the enemy is less than cuurent in to be hitten.
                                            Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), RowD, ColD, i, j, a, Order));
                                            th1.Wait();
                                            th1.Dispose();
                                            if (ab)
                                            {
                                                SelfSupported = true;
                                                S = S && true;
                                                break;
                                            }
                                        }
                                        if (SelfSupported)
                                        {
                                            break;
                                        }
                                    }
                                    //When a source enemy object attack a destination source object
                                    //a source object is greater than another source object. Is = -1 Is another object valuable.
                                    //a source object is less than or equal  than another source object.Is = 1 Is not another object valuable.
                                }
                            }
                            if ((!SelfSupported && InAttackedNotSelfSupported) //|| IsObjDangerest
                            )
                            {
                                S = false;
                                break;
                            }
                        }
                        if ((!SelfSupported && InAttackedNotSelfSupported) || IsObjDangerest
                            )
                        {
                            S = false;
                            break;
                        }
                    }
                    if (!SelfSupported
                         && InAttackedNotSelfSupported
                         )
                    {
                        S = false;
                    }
                    if (!SelfSupported && InAttackedNotSelfSupported)
                    {
                        S = false;
                    }

                    Order = Ord;
                    if (S)
                    {
                        return false;
                    }

                    return true;
                }
            }
        }

        //When there is at least on self object that is not safty.
        private bool InAttackSelfThatNotSupportedAll(int[,] TableS, int Order, Color a, int i, int j, int RowS, int ColS, int ikk, int jkk, int iik, int jjk)
        {
            object O = new object();
            lock (O)
            {
                bool S = true;
                int Ord = Order;
                List<int[]> ValuableSelfSupported = new List<int[]>();
                bool IsTowValuableObject = false;
                object O1 = new object();
                lock (O1)
                {
                    //list of existence self and valuable dangrouse attack
                    Task<bool> th = Task.Factory.StartNew(() => IsTowValuableObject = InAttackSelfThatNotSupportedCalculateValuableAll(CloneATable(TableS), Order, color, ikk, jkk, ref ValuableSelfSupported));
                    th.Wait();
                    th.Dispose();

                    //Initiate Variables.
                    int[,] Tab = new int[8, 8];
                    for (int ik = 0; ik < 8; ik++)
                    {
                        for (int jk = 0; jk < 8; jk++)
                        {
                            Tab[ik, jk] = TableS[ik, jk];
                        }
                    }

                    bool SelfSupported = false;
                    bool InAttackedNotSelfSupported = false;
                    S = true;
                    Order = Ord;
                    //Ignore of Enemies
                    if (Order == 1 && Tab[i, j] <= 0)
                    {
                        return false;
                    }
                    else
                        if (Order == -1 && Tab[i, j] >= 0)
                    {
                        return false;
                    }
                    //when there is another object valuable in List continue.(when movement is dangrouse costly)
                    bool ab = false;
                    Task<bool> th1 = Task.Factory.StartNew(() => ab = IsTowValuableObject && (IsObjectValaubleObjectSelf(Tab[i, j], ref ValuableSelfSupported)));
                    th1.Wait();
                    th1.Dispose();
                    if (ab)
                    {
                        return true;
                    }

                    Order = Ord;
                    //Ignore of Current
                    if (Order == 1 && Tab[RowS, ColS] >= 0)
                    {
                        return false;
                    }
                    else
                        if (Order == -1 && Tab[RowS, ColS] <= 0)
                    {
                        return false;
                    }

                    if (i == RowS && j == ColS)
                    {
                        return false;
                    }
                    //Enemy
                    a = Color.Gray;
                    Order = Ord;
                    if (Order * -1 == -1)
                    {
                        a = Color.Brown;
                    }

                    for (int ik = 0; ik < 8; ik++)
                    {
                        for (int jk = 0; jk < 8; jk++)
                        {
                            Tab[ik, jk] = TableS[ik, jk];
                        }
                    }

                    InAttackedNotSelfSupported = false;
                    SelfSupported = false;
                    for (int ik = 0; ik < 8; ik++)
                    {
                        for (int jk = 0; jk < 8; jk++)
                        {
                            Tab[ik, jk] = TableS[ik, jk];
                        }
                    }

                    object O2 = new object();
                    lock (O2)
                    {
                        Task<bool> th2 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Tab), RowS, ColS, i, j, a, Order * -1));
                        th2.Wait();
                        th2.Dispose();
                        if (ab)
                        {
                            InAttackedNotSelfSupported = true;
                            a = Color.Gray;
                            if (Order == -1)
                            {
                                a = Color.Brown;
                            }
                            //For Self.
                            for (int RowD = 0; RowD < 8; RowD++)
                            {
                                for (int ColD = 0; ColD < 8; ColD++)
                                {
                                    //Ignore of Enemies
                                    if (Order == 1 && Tab[RowD, ColD] <= 0)
                                    {
                                        continue;
                                    }
                                    else
                                        if (Order == -1 && Tab[RowD, ColD] >= 0)
                                    {
                                        continue;
                                    }

                                    if (i == RowD && j == ColD)
                                    {
                                        continue;
                                    }

                                    a = Color.Gray;
                                    if (Order == -1)
                                    {
                                        a = Color.Brown;
                                    }

                                    for (int ik = 0; ik < 8; ik++)
                                    {
                                        for (int jk = 0; jk < 8; jk++)
                                        {
                                            Tab[ik, jk] = TableS[ik, jk];
                                        }
                                    }
                                    //When there is supporte and cuurent is less than enemy.
                                    //method return true when is not supporte and the enemy is less than cuurent in to be hitten.
                                    Task<bool> th3 = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), RowD, ColD, i, j, a, Order) && (ObjectValueCalculator(CloneATable(Tab), i, j) <= ObjectValueCalculator(CloneATable(Tab), RowS, ColS)));
                                    th3.Wait();
                                    th3.Dispose();
                                    if (ab)
                                    {
                                        SelfSupported = true;
                                        S = S && true;
                                        break;
                                    }
                                }
                                //When a source enemy object attack a destination source object
                                //a source object is greater than another source object. Is = -1 Is another object valuable.
                                //a source object is less than or equal  than another source object.Is = 1 Is not another object valuable.
                                if (SelfSupported)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    if ((!SelfSupported && InAttackedNotSelfSupported))
                    {
                        S = false;
                    }
                }
                Order = Ord;

                if (S)
                {
                    return false;
                }

                return true;
            }
        }

        //Creation A Complete List of Attacked Self Object(s).
        private bool InAttackSelfThatNotSupportedCalculateValuableAll(int[,] TableS, int Order, Color a, int ij, int ji, ref List<int[]> ValuableSelfSupported)
        {
            object O = new object();
            lock (O)
            {
                //Initiate Variables.
                int[,] Tab = new int[8, 8];
                for (int ik = 0; ik < 8; ik++)
                {
                    for (int jk = 0; jk < 8; jk++)
                    {
                        Tab[ik, jk] = TableS[ik, jk];
                    }
                }

                int Ord = Order;
                bool SelfSupported = false;
                bool InAttackedNotSelfSupported = false;
                bool S = true;
                //For Self
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        S = true;
                        //Ignore of Enemy
                        if (Order == 1 && Tab[i, j] <= 0)
                        {
                            continue;
                        }
                        else
                            if (Order == -1 && Tab[i, j] >= 0)
                        {
                            continue;
                        }
                        //For Enemy.
                        for (int RowS = 0; RowS < 8; RowS++)
                        {
                            for (int ColS = 0; ColS < 8; ColS++)
                            {
                                //Ignore of Current
                                if (Order == 1 && Tab[RowS, ColS] >= 0)
                                {
                                    continue;
                                }
                                else
                                    if (Order == -1 && Tab[RowS, ColS] <= 0)
                                {
                                    continue;
                                }
                                //Enemy
                                a = Color.Gray;
                                if (Order * -1 == -1)
                                {
                                    a = Color.Brown;
                                }

                                for (int ik = 0; ik < 8; ik++)
                                {
                                    for (int jk = 0; jk < 8; jk++)
                                    {
                                        Tab[ik, jk] = TableS[ik, jk];
                                    }
                                }

                                InAttackedNotSelfSupported = false;
                                SelfSupported = false;
                                S = true;
                                //Wehn an Object of Enemy Attack Self Object
                                object O1 = new object();
                                lock (O1)
                                {
                                    bool ab = false;
                                    Task<bool> th = Task.Factory.StartNew(() => ab = Attack(CloneATable(Tab), RowS, ColS, i, j, a, Order * -1));
                                    th.Wait();
                                    th.Dispose();
                                    if (ab)
                                    {
                                        InAttackedNotSelfSupported = true;
                                        a = Color.Gray;
                                        if (Order == -1)
                                        {
                                            a = Color.Brown;
                                        }
                                        //For Self.
                                        for (int RowD = 0; RowD < 8; RowD++)
                                        {
                                            for (int ColD = 0; ColD < 8; ColD++)
                                            {
                                                //Ignore of Enemies
                                                if (Order == 1 && Tab[RowD, ColD] <= 0)
                                                {
                                                    continue;
                                                }
                                                else
                                                    if (Order == -1 && Tab[RowD, ColD] >= 0)
                                                {
                                                    continue;
                                                }

                                                a = Color.Gray;
                                                if (Order == -1)
                                                {
                                                    a = Color.Brown;
                                                }

                                                for (int ik = 0; ik < 8; ik++)
                                                {
                                                    for (int jk = 0; jk < 8; jk++)
                                                    {
                                                        Tab[ik, jk] = TableS[ik, jk];
                                                    }
                                                }
                                                //When There is Supporter For Attacked Self Object and Is Greater than Attacking Object.
                                                Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), RowD, ColD, i, j, a, Order) && (ObjectValueCalculator(CloneATable(Tab), i, j) <= ObjectValueCalculator(CloneATable(Tab), RowS, ColS)));
                                                th1.Wait();
                                                th1.Dispose();
                                                if (ab)
                                                {
                                                    SelfSupported = true;
                                                    S = S && true;
                                                    break;
                                                }
                                            }
                                            if (SelfSupported)
                                            {
                                                break;
                                            }
                                        }
                                        //When a source enemy object attack a destination source object
                                        //a source object is greater than another source object. Is = -1 Is another object valuable.
                                        //a source object is less than or equal  than another source object.Is = 1 Is not another object valuable.
                                    }
                                }
                                //When Attacked Current Object is not supported and there is another object valuable
                                object O2 = new object();
                                lock (O2)
                                {
                                    if ((!SelfSupported && InAttackedNotSelfSupported))
                                    {
                                        S = false;
                                        if (!S)
                                        {
                                            int[] Valuable = new int[3];
                                            Valuable[0] = TableS[i, j];
                                            Valuable[1] = i;
                                            Valuable[2] = j;
                                            if (!ExistValuble(Valuable, ref ValuableSelfSupported))
                                            {
                                                ValuableSelfSupported.Add(Valuable);
                                            }

                                            S = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                Order = Ord;
                //When There is at Last tow SelfNotSupporeted Object.
                if (ValuableSelfSupported.Count > 1)
                {
                    return true;
                }

                return false;
            }
        }

        private bool ExistValuble(int[] Table, ref List<int[]> ValuableSelfSupported)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                for (int i = 0; i < ValuableSelfSupported.Count; i++)
                {
                    if (ValuableSelfSupported[i][0] == Table[0] && ValuableSelfSupported[i][1] == Table[1] && ValuableSelfSupported[i][2] == Table[2])
                    {
                        return true;
                    }
                }
                return Is;
            }
        }

        ///Heuristic of King safty.
        private double HeuristicKingSafety(ref double HA, int[,] Tab, int Order, int RowS, int ColS
          )
        {
            object ol = new object();
            lock (ol)
            {
                ////double HA =1;
                const int CastleGray = 4, CastleBrown = -4, KingGray = 6, KingBrown = -6;
                if (Order == 1)
                {
                    int RowK = -1, ColK = -1;
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Tab), Order);
                    G.FindGrayKing(CloneATable(Tab), ref RowK, ref ColK);
                    if (Kind == 7)
                    {
                        HA += RationalRegard;
                    }

                    if (Tab[RowK, ColK] == KingGray && Tab[RowK, ColK] == TableInitiation[RowK, ColK] && ChessRules.CastlingAllowedGray)
                    {
                        HA += Rational(HA, RationalPenalty) * RationalPenalty;
                    }

                    if ((Tab[RowK, ColK] == KingGray) && (Tab[RowK, 7] == CastleGray || Tab[RowK, 0] == CastleGray) && (TableInitiation[RowK, ColK] == 6) && ChessRules.CastlingAllowedGray)
                    {
                        if (RowS == RowK && ColS == 5)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }

                        if (RowS == RowK && ColS == 6)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        //if (RowS == RowK - 1 && ColS == 5)
                        ///if (RowS == RowK - 1 && ColS == 6)

                        if (RowS == RowK && ColS == 3)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }

                        if (RowS == RowK && ColS == 2)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }

                        if (RowS == RowK && ColS == 1)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        // if (RowS == RowK - 1 && ColS == 3)
                        //if (RowS == RowK - 1 && ColS == 2)
                        //if (RowS == Row - 1 && ColS == 1)
                    }
                }
                else
                {
                    int RowK = -1, ColK = -1;
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Tab), Order);
                    G.FindBrownKing(CloneATable(Tab), ref RowK, ref ColK);
                    if (Kind == -7)
                    {
                        HA += RationalRegard;
                    }

                    if (Tab[RowK, ColK] == KingBrown && Tab[RowK, ColK] == TableInitiation[RowK, ColK] && ChessRules.CastlingAllowedBrown)
                    {
                        HA += Rational(HA, RationalPenalty) * RationalPenalty;
                    }

                    if ((Tab[RowK, ColK] == KingBrown) && (Tab[RowK, 7] == CastleBrown || Tab[RowK, 0] == CastleBrown) && (TableInitiation[RowK, ColK] == -6) && ChessRules.CastlingAllowedBrown)
                    {
                        if (RowS == RowK && ColS == 5)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }

                        if (RowS == RowK && ColS == 6)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        //if (RowS == RowK + 1 && ColS == 5)
                        // if (RowS == RowK + 1 && ColS == 6)

                        if (RowS == RowK && ColS == 3)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }

                        if (RowS == RowK && ColS == 2)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }

                        if (RowS == RowK && ColS == 1)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        //if (RowS == RowK + 1 && ColS == 3)
                        // if (RowS == RowK + 1 && ColS == 2)
                        //if (RowS == RowK + 1 && ColS == 1)
                    }
                }
                return HA;
            }
        }

        private double HeuristicKingPreventionOfCheckedAtBegin(ref double HA, int[,] Tab, int Order, int CurrentAStarGredy, int RowS, int ColS, int RowD, int ColD
            )
        {
            object O3 = new object();
            lock (O3)
            {
                ////double HA =1;
                int[,] Tabl = CloneATable(Tab);
                if (Tabl[RowS, ColS] != 0)
                {
                    Tabl[RowD, ColD] = Tabl[RowS, ColS];
                    Tabl[RowS, ColS] = 0;
                    ChessRules A = new ChessRules(CurrentAStarGredy, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Tab[RowD, ColD], CloneATable(Tab), Order);
                    Task<bool> th = Task.Factory.StartNew(() => A.CheckMate(Tabl, Order));
                    th.Wait();
                    th.Dispose();

                    if (!(A.CheckMateGray || A.CheckMateBrown))
                    {
                        if (A.CheckGray || A.CheckBrown)
                        {
                            HA += Rational(HA, RationalPenalty) * RationalPenalty;
                        }
                    }
                    if (Order == 1)
                    {
                        if (A.CheckMateGray)
                        {
                            HA += Rational(HA, RationalPenalty) * RationalPenalty;
                        }
                        else
                        if (A.CheckMateBrown)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                    }
                    else
                    {
                        if (A.CheckMateGray)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        else
             if (A.CheckMateBrown)
                        {
                            HA += Rational(HA, RationalPenalty) * RationalPenalty;
                        }
                    }
                }
                else
                {
                    ChessRules A = new ChessRules(CurrentAStarGredy, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Tab[RowD, ColD], CloneATable(Tab), Order);
                    Task<bool> th = Task.Factory.StartNew(() => A.CheckMate(Tabl, Order));
                    th.Wait();
                    th.Dispose();
                    if (A.CheckGray || A.CheckBrown)
                    {
                        HA += Rational(HA, RationalRegard) * RationalRegard;
                    }
                    if (Order == 1)
                    {
                        if (A.CheckMateGray)
                        {
                            HA += Rational(HA, RationalPenalty) * RationalPenalty;
                        }
                        else
                        if (A.CheckMateBrown)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                    }
                    else
                    {
                        if (A.CheckMateGray)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        else
             if (A.CheckMateBrown)
                        {
                            HA += Rational(HA, RationalPenalty) * RationalPenalty;
                        }
                    }
                }
                return HA;
            }
        }

        //Heuristic of Supportation.

        private double HeuristicEnemySupported(ref double HA, int[,] Tab, int Ord, Color aa, int RowD, int ColD, int RowS, int ColS
          )
        {
            object O = new object();
            lock (O)
            {
                int HeuristicSelfSupportedValue = 0;
                //Initiate Local Vrariables.
                ////double HA =1;
                int DumOrder = Order;
                int DummyOrder = Order;
                int DummyCurrentOrder = ChessRules.CurrentOrder;
                //If There is Not AStarGreedy Heuristic Boolean Value.
                if (!AStarGreedyHeuristicT)
                {
                    //For All Self
                    {
                        {
                            //For Current Object Lcation.
                            Order = new int();
                            Order = DumOrder;
                            Color a = new Color();
                            a = aa;
                            //Ignore Current Unnessery Home.
                            if (RowS == RowD && ColS == ColD)
                            {
                                return 0;
                            }
                            //Default Is Gray One.
                            int Sign = 1;
                            Order = DummyOrder;
                            ///When Supporte is true. means [RowD,ColD] Supportes [RowS,ColS].
                            ///What is Supporte!
                            ///Ans:When [RowS,ColS] is Supporte [RowD,ColD] return true when Self is located in [RowD,ColD].
                            //if (Order == 1 && Tab[RowD, ColD] <= 0)
                            //if (Order == -1 && Tab[RowD, ColD] >= 0)
                            //if (!Scop(RowS, ColS, RowD, ColD, System.Math.Abs(Tab[RowS, ColS])))
                            if (Tab[RowD, ColD] < 0 && DummyOrder == -1 && Tab[RowS, ColS] <= 0)
                            {
                                Order = -1;
                                object O1 = new object();
                                lock (O1)
                                {
                                    Sign = 1 * AllDraw.SignSupport;
                                    ChessRules.CurrentOrder = -1;
                                }
                                a = Color.Brown;
                            }
                            else if (Tab[RowD, ColD] > 0 && DummyOrder == 1 && Tab[RowS, ColS] > 0)
                            {
                                Order = 1;
                                object O1 = new object();
                                lock (O1)
                                {
                                    Sign = 1 * AllDraw.SignSupport;
                                    ChessRules.CurrentOrder = 1;
                                }
                                a = Color.Gray;
                            }
                            else
                            {
                                return HeuristicSelfSupportedValue;
                            }
                            //For Support Movments.
                            bool ab = false;
                            Task<bool> th = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), RowS, ColS, RowD, ColD, a, Order));
                            th.Wait();
                            th.Dispose();
                            if (ab)
                            {
                                //Calculate Local Support Heuristic.
                                HA += Rational(HA, RationalPenalty) * RationalPenalty; ;
                                int Supported = new int();
                                int SupportedE = new int();
                                Supported = 0;
                                SupportedE = 0;
                                //For All Self Obejcts.
                                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, g =>
                                for (int g = 0; g < 8; g++)
                                {
                                    //if (Supported)
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, h =>
                                    for (int h = 0; h < 8; h++)
                                    {
                                        object O2 = new object();
                                        lock (O2)
                                        {
                                            //if (Supported)
                                            //Ignore Of Enemy Objects.
                                            if (Order == 1 && Tab[g, h] == 0)
                                            {
                                                continue;
                                            }

                                            if (Order == -1 && Tab[g, h] == 0)
                                            {
                                                continue;
                                            }

                                            if (!Scop(g, h, RowS, ColS, System.Math.Abs(Tab[g, h])))
                                            {
                                                continue;
                                            }

                                            Color aaa = new Color();
                                            //Assgin Enemy ints.
                                            aaa = Color.Gray;
                                            aa = Color.Gray;
                                            if (Order == -1)
                                            {
                                                aaa = Color.Brown;
                                            }
                                            else
                                            {
                                                aaa = Color.Gray;
                                            }

                                            if (Order * -1 == -1)
                                            {
                                                aa = Color.Brown;
                                            }
                                            else
                                            {
                                                aa = Color.Gray;
                                            }
                                            //When Enemy is Supported.
                                            bool A = new bool();
                                            bool B = new bool();
                                            Task<bool> th1 = Task.Factory.StartNew(() => A = Support(CloneATable(Tab), g, h, RowS, ColS, aaa, Order));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task<bool> th2 = Task.Factory.StartNew(() => B = Attack(CloneATable(Tab), g, h, RowS, ColS, aa, Order * -1));
                                            th2.Wait();
                                            th2.Dispose();
                                            //When Enemy is Supported.
                                            if (A)
                                            {
                                                //Assgine variable.
                                                Supported++;
                                            }
                                            if (B)
                                            {
                                                //Assgine variable.
                                                SupportedE++;
                                            }
                                        }
                                    }
                                    // if (Supported)
                                }
                                object O1 = new object();
                                lock (O1)
                                {
                                    if (SupportedE > 0 && Supported == 0)
                                    {
                                        //When is Not Supported multyply 100.
                                        HA += Rational(HA, (int)System.Math.Pow(2, SupportedE));
                                    }
                                    else
                                       if (Supported > 0)
                                    {
                                        //When is Supported Multyply -100.
                                        HA += Rational(HA, (int)(-1 * System.Math.Pow(2, Supported))) * (int)(-1 * System.Math.Pow(2, Supported));
                                    }
                                }
                            }
                        }
                    }
                }
                //For All Homes Table.
                else
                {
                    {
                        {
                            {
                                {
                                    Order = new int();
                                    Color a = new Color();
                                    a = aa;
                                    {
                                        //Ignore Current Home.
                                        if (RowS == RowD && ColS == ColD)
                                        {
                                            return 0;
                                        }
                                        //Initiate Local Variables.
                                        int Sign = 1;
                                        Order = DummyOrder;
                                        ///When Supporte is true. means [RowD,ColD] is in SelfSupported.by [RowS,ColS].
                                        ///What is Supporte!
                                        ///Ans:When [RowS,ColS] is Supporte [RowD,ColD] return true when Self is located in [RowD,ColD].
                                        //if (!Scop(RowS, ColS, RowD, ColD, System.Math.Abs(Tab[RowS, ColS])))
                                        if (Tab[RowD, ColD] < 0 && DummyOrder == -1 && Tab[RowS, ColS] <= 0)
                                        {
                                            Order = -1;
                                            object O2 = new object();
                                            lock (O2)
                                            {
                                                Sign = 1 * AllDraw.SignSupport;
                                                ChessRules.CurrentOrder = -1;
                                                a = Color.Brown;
                                            }
                                        }
                                        else if (Tab[RowD, ColD] > 0 && DummyOrder == 1 && Tab[RowS, ColS] > 0)
                                        {
                                            Order = 1;
                                            object O2 = new object();
                                            lock (O2)
                                            {
                                                Sign = 1 * AllDraw.SignSupport;
                                                ChessRules.CurrentOrder = 1;
                                                a = Color.Gray;
                                            }
                                        }
                                        else
                                        {
                                            return HeuristicSelfSupportedValue;
                                        }
                                        //For Support Movments.
                                        bool ab = false;
                                        Task<bool> th = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), RowS, ColS, RowD, ColD, a, Order));
                                        th.Wait();
                                        th.Dispose();
                                        if (ab)
                                        {
                                            //Calculate Local Support Heuristic.
                                            HA += Rational(HA, RationalPenalty) * RationalPenalty; ;
                                            int Supported = new int();
                                            int SupportedE = new int();
                                            Supported = 0;
                                            SupportedE = 0;
                                            //For All Self Obejcts.
                                            ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, g =>
                                            for (int g = 0; g < 8; g++)
                                            {
                                                //if (Supported)
                                                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, h =>
                                                for (int h = 0; h < 8; h++)
                                                {
                                                    object O2 = new object();
                                                    lock (O2)
                                                    {
                                                        //if (Supported)
                                                        //Ignore Of Enemy Objects.
                                                        if (Order == 1 && Tab[g, h] == 0)
                                                        {
                                                            continue;
                                                        }

                                                        if (Order == -1 && Tab[g, h] == 0)
                                                        {
                                                            continue;
                                                        }

                                                        if (!Scop(g, h, RowS, ColS, System.Math.Abs(Tab[g, h])))
                                                        {
                                                            continue;
                                                        }

                                                        Color aaa = new Color();
                                                        //Assgin Enemy ints.
                                                        aaa = Color.Gray;
                                                        aa = Color.Gray;
                                                        if (Order == -1)
                                                        {
                                                            aaa = Color.Brown;
                                                        }
                                                        else
                                                        {
                                                            aaa = Color.Gray;
                                                        }

                                                        if (Order * -1 == -1)
                                                        {
                                                            aa = Color.Brown;
                                                        }
                                                        else
                                                        {
                                                            aa = Color.Gray;
                                                        }
                                                        //When Enemy is Supported.
                                                        bool A = new bool();
                                                        bool B = new bool();
                                                        Task<bool> th1 = Task.Factory.StartNew(() => A = Support(CloneATable(Tab), g, h, RowS, ColS, aaa, Order));
                                                        th1.Wait();
                                                        th1.Dispose();
                                                        Task<bool> th2 = Task.Factory.StartNew(() => B = Attack(CloneATable(Tab), g, h, RowS, ColS, aa, Order * -1));
                                                        th2.Wait();
                                                        th2.Dispose();
                                                        //When Enemy is Supported.
                                                        if (A)
                                                        {
                                                            //Assgine variable.
                                                            Supported++;
                                                        }
                                                        if (B)
                                                        {
                                                            //Assgine variable.
                                                            SupportedE++;
                                                        }
                                                    }
                                                }
                                                // if (Supported)
                                            }
                                            object O1 = new object();
                                            lock (O1)
                                            {
                                                if (SupportedE > 0 && Supported == 0)
                                                {
                                                    //When is Not Supported multyply 100.
                                                    HA += Rational(HA, (int)System.Math.Pow(2, SupportedE));
                                                }
                                                else
                                                      if (Supported > 0)
                                                {
                                                    //When is Supported Multyply -100.
                                                    HA += Rational(HA, (int)(-1 * System.Math.Pow(2, Supported))) * (int)(-1 * System.Math.Pow(2, Supported));
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //Reassignments of Global Orders with Local Begining One.
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                Order = DumOrder;
                return HA * 1;
            }
        }        ///Identification of Equality

        public static bool TableEqual(int[,] Tab1, int[,] Tab2)
        {
            object O = new object();
            lock (O)
            {
                //For All Home
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        //When there is different values in same location of tow Table return non equality.
                        if (Tab1[i, j] != Tab2[i, j])
                        {
                            return false;
                        }
                    }
                }
                //Else return equlity.
                return true;
            }
        }

        //If tow int Objects is equal.
        public static bool TableEqual(int Tab1, int Tab2)
        {
            object O = new object();
            lock (O)
            {
                //When there is different values in same location of tow Table return non equality.
                if (Tab1 != Tab2)
                {
                    return false;
                }
                //Else return equlity.
                return true;
            }
        }

        //Deterimination of Existance of Table in List..
        public static bool ExistTableInList(int[,] Tab, List<int[,]> List, int Index)
        {
            object O = new object();
            lock (O)
            {
                //Initiate Local Variables.
                bool Exist = false;
                //For All Tables of Table List.
                for (int i = Index; i < List.Count; i++)
                {
                    //Strore Equality Value.
                    bool Eq = TableEqual(Tab, List[i]);
                    //When is Equality is Occurred.
                    if (Eq)
                    {
                        //Store Equality Local Value in a Global static value.
                        AllDraw.LoopHeuristicIndex = i;
                        return Eq;
                    }
                    Exist |= Eq;
                }
                //return Equality Local value of all lists.
                return Exist;
            }
        }

        ///Move Determination.
        public bool Movable(int[,] Tab, int i, int j, int ii, int jj, Color a, int Order)
        {
            object O = new object();
            lock (O)
            {
                if (Tab[i, j] == 0)
                {
                    return false;
                }

                if (Order == 1 && Tab[i, j] < 0)
                {
                    return false;
                }

                if (Order == -1 && Tab[i, j] > 0)
                {
                    return false;
                }

                int[,] Table = new int[8, 8];
                for (int p = 0; p < 8; p++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        Table[p, k] = Tab[p, k];
                    }
                }
                //Initiate Local Variables.
                int Store = Table[ii, jj];
                ChessRules A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[i, j], CloneATable(Table), Order);
                //Menen Parameter is Moveble to Second Parameters Location returm Movable.
                if (Order == 1 && Table[ii, jj] < 0)
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = A.Rules(i, j, ii, jj, Order));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        return true;
                    }
                }
                else
                 if (Order == -1 && Table[ii, jj] > 0)
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = A.Rules(i, j, ii, jj, Order));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        return true;
                    }
                }
                if (Order == 1 && Table[ii, jj] == 0)
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = A.Rules(i, j, ii, jj, Order));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        return true;
                    }
                }
                else
                if (Order == -1 && Table[ii, jj] == 0)
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = A.Rules(i, j, ii, jj, Order));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        //
        //When Oredrs of OrderPalte and Calculation Order is not equal return negative one and else return one.
        private int SignOrderToPlate(int Order)
        {
            object O = new object();
            lock (O)
            {
                int Sign = 1;
                //When Current Order Sign Positive.
                if (Order == AllDraw.OrderPlateDraw)
                {
                    Sign = 1;
                }
                else
                    //When Order is Opposite Sign Negative.
                    if (Order != AllDraw.OrderPlateDraw)
                {
                    Sign = -1;
                }

                return Sign;
            }
        }

        //Remove Penalties of Unnesserily Nodes.
        public bool RemovePenalty(int[,] Tab, int Order, int i, int j)
        {
            object O = new object();
            lock (O)
            {
                bool Remove = false;
                //Create Objects.
                ChessRules AA = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Tab[i, j], CloneATable(Tab), Order);
                //When is Check.
                if (AA.Check(CloneATable(Tab), Order))
                {
                    //When there is Current Checked or Objects Danger return false.
                    if (Order == 1 && (AA.CheckGray || AA.CheckGrayObjectDangour))
                    {
                        return Remove;
                    }
                    if (Order == -1 && (AA.CheckBrown || AA.CheckBrownObjectDangour))
                    {
                        return Remove;
                    }
                }

                //For Enemy.
                for (int ii = 0; ii < 8; ii++)
                {
                    for (int jj = 0; jj < 8; jj++)
                    {
                        if (Order == 1 && Tab[ii, jj] >= 0)
                        {
                            continue;
                        }

                        if (Order == -1 && Tab[ii, jj] <= 0)
                        {
                            continue;
                        }
                        //Clone a Copy.
                        int[,] Table = new int[8, 8];
                        //Clone a Table.
                        for (int RowS = 0; RowS < 8; RowS++)
                        {
                            for (int ColS = 0; ColS < 8; ColS++)
                            {
                                Table[RowS, ColS] = Tab[RowS, ColS];
                            }
                        }

                        ChessRules A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[ii, jj], CloneATable(Table), Order * -1);
                        Color a = Color.Gray;
                        if (Order * -1 == -1)
                        {
                            a = Color.Brown;
                        }
                        //When there is movment to current OPbject.
                        bool ab = false;
                        Task<bool> th = Task.Factory.StartNew(() => ab = A.Rules(ii, jj, i, j, Table[ii, jj]));
                        th.Wait();
                        th.Dispose();
                        if (ab)
                        {
                            //Number of Attacks and take Move.
                            int Count = 0;
                            Task<int> th1 = Task.Factory.StartNew(() => Count = AttackerCount(CloneATable(Table), Order * -1, a, ii, jj));
                            th1.Wait();
                            th1.Dispose();
                            //When there is Object Danger.
                            //Clone a Copy.
                            for (int RowS = 0; RowS < 8; RowS++)
                            {
                                for (int ColS = 0; ColS < 8; ColS++)
                                {
                                    Table[RowS, ColS] = Tab[RowS, ColS];
                                }
                            }
                            //Create new ThinkingChess Rule Object.
                            A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[ii, jj], CloneATable(Table), Order);
                            //Detect int.
                            a = Color.Gray;
                            if (Order == -1)
                            {
                                a = Color.Brown;
                            }
                            //When Current Movments Attacks Enemy.
                            Task<bool> th2 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), i, j, ii, jj, a, Order));
                            th2.Wait();
                            th2.Dispose();
                            if (ab)
                            {
                                //For Current Home.
                                for (int RowS = 0; RowS < 8; RowS++)
                                {
                                    for (int ColS = 0; ColS < 8; ColS++)
                                    {
                                        //Ignore of Enemy.
                                        if (Order == 1 && Tab[RowS, ColS] <= 0)
                                        {
                                            continue;
                                        }

                                        if (Order == -1 && Tab[RowS, ColS] >= 0)
                                        {
                                            continue;
                                        }
                                        //Whn Value Of Current is Less That Enemy.
                                        Task<bool> th3 = Task.Factory.StartNew(() => ab = ObjectValueCalculator(CloneATable(Table), i, j) < ObjectValueCalculator(CloneATable(Table), ii, jj));
                                        th3.Wait();
                                        th3.Dispose();
                                        if (ab)
                                        {
                                            //Take Move.
                                            Table[ii, jj] = Table[i, j];
                                            Table[i, j] = 0;
                                            a = Color.Gray;
                                            if (Order * -1 == -1)
                                            {
                                                a = Color.Brown;
                                            }
                                            //When Enemy Attacks Current Moved.
                                            Task<bool> th4 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), RowS, ColS, ii, jj, a, Order * -1));
                                            th4.Wait();
                                            th4.Dispose();
                                            if (ab)
                                            {
                                                //For Current Order.
                                                for (int RowD = 0; RowD < 8; RowD++)
                                                {
                                                    for (int ColD = 0; ColD < 8; ColD++)
                                                    {
                                                        //Ignore of Enemy.
                                                        if (Order == 1 && Tab[RowD, ColD] <= 0)
                                                        {
                                                            continue;
                                                        }

                                                        if (Order == -1 && Tab[RowD, ColD] >= 0)
                                                        {
                                                            continue;
                                                        }

                                                        a = Color.Gray;
                                                        if (Order == -1)
                                                        {
                                                            a = Color.Brown;
                                                        }
                                                        //When Self Supported Current
                                                        Task<bool> th5 = Task.Factory.StartNew(() => ab = Support(CloneATable(Table), RowD, ColD, i, j, a, Order));
                                                        th5.Wait();
                                                        th5.Dispose();
                                                        if (ab)
                                                        {
                                                            //If V alue of Enemy is Greater Than Current and Value of Enemy is Greater than Supporter.
                                                            Task<bool> th6 = Task.Factory.StartNew(() => ab = ObjectValueCalculator(CloneATable(Table), RowS, ColS) < ObjectValueCalculator(CloneATable(Table), ii, jj) && ObjectValueCalculator(CloneATable(Table), RowS, ColS) > ObjectValueCalculator(CloneATable(Table), Row, ColS));
                                                            th6.Wait();
                                                            th6.Dispose();
                                                            if (ab)
                                                            {
                                                                Remove = true;
                                                                return Remove;
                                                            }
                                                            else
                                                            {
                                                                return Remove;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            return Remove;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                return Remove;
                                            }
                                        }
                                        else
                                        {
                                            return Remove;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return Remove;
            }
        }

        //Dangouring of current movment fo current Order.
        private bool IsCurrentStateIsDangreousForCurrentOrder(int[,] Tabl, int Order, int ii, int jj)
        {
            object O = new object();
            lock (O)
            {
                //Initiate Object.
                ChessRules A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, 1, CloneATable(Tabl), 1);
                //Gray Order.
                if (Order == 1)
                {
                    //Find location of Gray King.
                    int RowG = -1, ColumnG = -1;
                    A.FindGrayKing(Tabl, ref RowG, ref ColumnG);
                    //When found.
                    if (RowG != -1 && ColumnG != -1)
                    {
                        //For Brown
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                //Ignore of Gray and Empty
                                if (Tabl[i, j] >= 0)
                                {
                                    continue;
                                }

                                if (i != ii && j != jj)
                                {
                                    //Create new Objects of Table
                                    int[,] TablCon = new int[8, 8];
                                    for (int RowS = 0; RowS < 8; RowS++)
                                    {
                                        for (int ColS = 0; ColS < 8; ColS++)
                                        {
                                            TablCon[RowS, ColS] = Tabl[RowS, ColS];
                                        }
                                    }
                                    //For Enemy Order.
                                    if (TablCon[i, j] < 0)
                                    {
                                        //For Gray and Empty Objects.
                                        if (TablCon[ii, jj] >= 0)
                                        {
                                            //Setting Enemy Order.
                                            int DummyOrder = Order;
                                            int DummyCurrentOrder = ChessRules.CurrentOrder;
                                            A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TablCon[i, j], TablCon, -1);
                                            //When Enemy is Attacked Gray Objects.
                                            bool ab = false;
                                            Task<bool> th = Task.Factory.StartNew(() => ab = A.Rules(i, j, ii, jj, TablCon[i, j]));
                                            th.Wait();
                                            th.Dispose();
                                            if (ab)
                                            {
                                                //Take Movments.
                                                TablCon[ii, jj] = TablCon[i, j];
                                                TablCon[i, j] = 0;
                                                //Settting Current Order.
                                                ChessRules.CurrentOrder = 1;
                                                //Settting Object.
                                                A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TablCon[ii, jj], TablCon, 1);
                                                //When Occured Check.
                                                Task<bool> th1 = Task.Factory.StartNew(() => ab = A.Check(TablCon, 1));
                                                th1.Wait();
                                                th1.Dispose();
                                                if (ab)
                                                {
                                                    //When Gray is Check.
                                                    if (A.CheckGray)
                                                    {
                                                        //For Enemy Order Objects.
                                                        for (int RowD = 0; RowD < 8; RowD++)
                                                        {
                                                            for (int ColD = 0; ColD < 8; ColD++)
                                                            {
                                                                //When is not Conflict.
                                                                if (RowD != i && ColD != j && RowD != ii && ColD != jj)
                                                                {
                                                                    //Setting Enemy.
                                                                    ChessRules.CurrentOrder = -1;
                                                                    //When Enemy is Supported
                                                                    Task<bool> th2 = Task.Factory.StartNew(() => ab = Support(TablCon, RowD, ColD, i, j, Color.Brown, -1));
                                                                    th2.Wait();
                                                                    th2.Dispose();
                                                                    if (ab)
                                                                    {
                                                                        //restore and return true.
                                                                        Order = DummyOrder;
                                                                        ChessRules.CurrentOrder = DummyCurrentOrder;
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    Order = DummyOrder;
                                                    ChessRules.CurrentOrder = DummyCurrentOrder;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //For Brown Order.
                else if (Order == -1)
                {
                    //Found of Brown King.
                    int RowB = -1, ColumnB = -1;
                    A.FindBrownKing(Tabl, ref RowB, ref ColumnB);
                    //When found.
                    if (RowB != -1 && ColumnB != -1)
                    {
                        //For Gray.
                        for (int i = 0; i < 8; i++)
                        {
                            for (int j = 0; j < 8; j++)
                            {
                                if (Tabl[i, j] <= 0)
                                {
                                    continue;
                                }

                                if (i != ii && j != jj)
                                {
                                    //Create new Objects of Table
                                    int[,] TablCon = new int[8, 8];
                                    for (int RowS = 0; RowS < 8; RowS++)
                                    {
                                        for (int ColS = 0; ColS < 8; ColS++)
                                        {
                                            TablCon[RowS, ColS] = Tabl[RowS, ColS];
                                        }
                                    }
                                    //For Enemy Objects.
                                    if (TablCon[i, j] > 0)
                                    {
                                        //For Self Objects and Empty.
                                        if (TablCon[ii, jj] <= 0)
                                        {
                                            //Store and Enemy Order.
                                            int DummyOrder = Order;
                                            int DummyCurrentOrder = ChessRules.CurrentOrder;
                                            A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TablCon[i, j], TablCon, 1);
                                            ChessRules.CurrentOrder = 1;
                                            //When Enemy Attacked Self Objects.
                                            bool ab = false;
                                            Task<bool> th = Task.Factory.StartNew(() => ab = A.Rules(i, j, ii, jj, TablCon[i, j]));
                                            th.Wait();
                                            th.Dispose();
                                            if (ab)
                                            {
                                                //Take movemnts.
                                                TablCon[ii, jj] = TablCon[i, j];
                                                TablCon[i, j] = 0;
                                                //Setting current Order.
                                                ChessRules.CurrentOrder = -1;
                                                A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TablCon[ii, jj], TablCon, -1);
                                                //When Check Occured.
                                                Task<bool> th1 = Task.Factory.StartNew(() => ab = A.Check(TablCon, -1));
                                                th1.Wait();
                                                th1.Dispose();
                                                if (ab)
                                                {
                                                    //When Current is Check.
                                                    if (A.CheckBrown)
                                                    {
                                                        //For Enemy Objecvts.
                                                        for (int RowD = 0; RowD < 8; RowD++)
                                                        {
                                                            for (int ColD = 0; ColD < 8; ColD++)
                                                            {
                                                                //Ignore of Conflit.
                                                                if (RowD != i && ColD != j && RowD != ii && ColD != jj)
                                                                {
                                                                    //Setting Enemy Order
                                                                    ChessRules.CurrentOrder = 1;
                                                                    //When Enemy is Supported.
                                                                    Task<bool> th2 = Task.Factory.StartNew(() => ab = Support(TablCon, RowD, ColD, i, j, Color.Gray, 1));
                                                                    th2.Wait();
                                                                    th2.Dispose();
                                                                    if (ab)
                                                                    {
                                                                        //restore and return true.
                                                                        Order = DummyOrder;
                                                                        ChessRules.CurrentOrder = DummyCurrentOrder;
                                                                        return true;
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    //restore.
                                                    Order = DummyOrder;
                                                    ChessRules.CurrentOrder = DummyCurrentOrder;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //return false.
                return false;
            }
        }

        //When Next Movements is Checked.QC_OK.
        private int[] IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(int Order, int[,] Tabl, int ik, int jk, int iki, int jki, int OrderPalte, int OrderPalteMulMinuse, int Depth, bool KindCheckedSelf)
        {
            object O = new object();
            lock (O)
            {
                int[] Is = new int[4];
                object O3 = new object();
                lock (O3)
                {
                    Is[0] = 0;
                    Is[1] = 0;
                    Is[2] = 0;
                    Is[3] = 0;
                    int[,] Tab2 = CloneATable(Tabl);
                    ChessRules A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Tab2[ik, jk], Tab2, Order * -1);
                    if (Order * -1 == 1)
                    {
                        color = Color.Gray;
                    }
                    else
                    {
                        color = Color.Brown;
                    }
                    //When Enemy Attack Currnet.
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = A.Rules(ik, jk, iki, jki, Tab2[ik, jk]));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Tab2[iki, jki] = Tab2[ik, jk];
                        Tab2[ik, jk] = 0;
                        A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Tab2[iki, jki], Tab2, Order * -1);
                        //When Current Always is in CheckedMate.
                        Task<bool> th1 = Task.Factory.StartNew(() => ab = A.CheckMate(Tab2, Order * -1));
                        th1.Wait();
                        th1.Dispose();
                        if (ab)
                        {
                            //When Order is Gray.
                            if (OrderPalte == 1)
                            {
                                if (A.CheckMateGray)
                                {
                                    Is[0] = 1;
                                    if (KindCheckedSelf)
                                    {
                                        Is[1] = Depth;
                                    }
                                }
                                else
                                {
                                    //if (A.CheckMateBrown)
                                }
                            }
                            //When Order is Brown.
                            else
                               if (OrderPalte == -1)
                            {
                                if (A.CheckMateBrown)
                                {
                                    Is[0] = 1;
                                    Is[1] = Depth;
                                }
                                else
                                {
                                    //if (A.CheckMateGray)
                                }
                            }

                            //When Order * -1 is Gray
                            if (OrderPalteMulMinuse == 1)
                            {
                                if (A.CheckMateGray)
                                {
                                    Is[2] = 1;
                                    Is[3] = Depth;
                                }
                                else
                                {
                                    //if (A.CheckMateBrown)
                                }
                            }
                            //When Order * -1 is Brown
                            else
                               if (OrderPalteMulMinuse == -1)
                            {
                                if (A.CheckMateBrown)
                                {
                                    Is[2] = 1;
                                    Is[3] = Depth;
                                }
                                else
                                {
                                    //if (A.CheckMateGray)
                                }
                            }
                        }
                        if (Order * -1 == 1)
                        {
                            color = Color.Gray;
                        }
                        else
                        {
                            color = Color.Brown;
                        }
                        //if (Tab2[iki, jki] == 0)
                        //For Movements.
                        int Ord = Order * -1;
                        int[,] Tab = CloneATable(Tab2);
                        Color a = color;
                        if (Ord == 1)
                        {
                            a = Color.Gray;
                        }
                        else
                        {
                            a = Color.Brown;
                        }

                        int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMulMinuse, Depth1 = Depth + 1;
                        bool KindCheckedSelf1 = KindCheckedSelf;
                        object O1 = new object();
                        int[] IS = null;
                        lock (O1)
                        {
                            Task<int[]> th2 = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovment(CloneATable(Tab), Ord, Depth1, OrderP, OrderM, KindCheckedSelf1));
                            th2.Wait();
                            th2.Dispose();
                        }
                        if (IS[0] == 1)
                        {
                            Is[0] = 1;
                        }

                        if (IS[2] == 1)
                        {
                            Is[2] = 1;
                        }

                        Is[1] = IS[1];
                        Is[3] = IS[3];
                    }
                }
                return Is;
            }
        }

        private int[] IsNextMovmentIsCheckOrCheckMateForCurrentMovment(int[,] Tabl, int Order, int Depth, int OrderPalte, int OrderPalteMinusPluse, bool KindCheckedSelf)
        {
            object O = new object();
            lock (O)
            {
                int[] Is = new int[4];
                object O3 = new object();
                lock (O3)
                {
                    Is[0] = 0;
                    Is[1] = 0;
                    Is[2] = 0;
                    Is[3] = 0;
                    int DummyOrder = Order;
                    int DummyCurrentOrder = ChessRules.CurrentOrder;
                    if (Depth >= AllDraw.MaxAStarGreedy)
                    {
                        return Is;
                    }
                    //For All Enemies.
                    for (int ik = 0; ik < 8; ik++)
                    {
                        for (int jk = 0; jk < 8; jk++)
                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, ik =>
                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, jk =>
                        {
                            //Ignore of Current
                            if (Order == 1 && Tabl[ik, jk] >= 0)
                            {
                                continue;
                            }

                            if (Order == -1 && Tabl[ik, jk] <= 0)
                            {
                                continue;
                            }

                            switch (System.Math.Abs(Tabl[ik, jk]))
                            {
                                case 1:
                                    //For Current Home
                                    for (int iki = ik - 2; iki < ik + 3; iki++)
                                    {
                                        for (int jki = jk - 2; jki < jk + 3; jki++)
                                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(ik - 2, ik + 3, iki =>
                                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(jk - 2, jk + 3, jki =>
                                        // init subtotal
                                        {
                                            if (!Scop(ik, jk, iki, jki, 1))
                                            {
                                                continue;
                                            }
                                            //Ignore of Enemy
                                            if (Order == 1 && Tabl[iki, jki] < 0)
                                            {
                                                continue;
                                            }

                                            if (Order == -1 && Tabl[iki, jki] > 0)
                                            {
                                                continue;
                                            }

                                            if (Is[0] == 1)
                                            {
                                                continue;
                                            }

                                            int Ord = Order;
                                            int[,] Tab = CloneATable(Tabl);
                                            int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMinusPluse, Depth1 = Depth + 1;
                                            bool KindCheckedSelf1 = KindCheckedSelf;
                                            int[] IS = null;
                                            object O1 = new object();
                                            lock (O1)
                                            {
                                                Task<int[]> th = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(Ord, CloneATable(Tab), ik1, jk1, iki1, jki1, OrderP, OrderM, Depth1, KindCheckedSelf1));
                                                th.Wait();
                                                th.Dispose();
                                                if (Is[0] == 1)
                                                {
                                                    Is[0] = 1;
                                                }

                                                if (IS[2] == 1)
                                                {
                                                    Is[2] = 1;
                                                }

                                                Is[1] = IS[1]; Is[3] = IS[3];
                                            }
                                        }
                                    }

                                    break;

                                case 2:

                                    //For Current Home
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, iki =>
                                    for (int iki = 0; iki < 8; iki++)
                                    {
                                        int jki = iki + jk - ik;
                                        if (!Scop(ik, jk, iki, jki, 2))
                                        {
                                            continue;
                                        }
                                        //Ignore of Enemy
                                        if (Order == 1 && Tabl[iki, jki] < 0)
                                        {
                                            continue;
                                        }

                                        if (Order == -1 && Tabl[iki, jki] > 0)
                                        {
                                            continue;
                                        }

                                        if (Is[0] == 1)
                                        {
                                            continue;
                                        }

                                        int Ord = Order;
                                        int[,] Tab = CloneATable(Tabl);
                                        int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMinusPluse, Depth1 = Depth + 1;
                                        bool KindCheckedSelf1 = KindCheckedSelf;
                                        int[] IS = null;
                                        object O1 = new object();
                                        lock (O1)
                                        {
                                            Task<int[]> th = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(Ord, CloneATable(Tab), ik1, jk1, iki1, jki1, OrderP, OrderM, Depth1, KindCheckedSelf1));
                                            th.Wait();
                                            th.Dispose();
                                            if (Is[0] == 1)
                                            {
                                                Is[0] = 1;
                                            }

                                            if (IS[2] == 1)
                                            {
                                                Is[2] = 1;
                                            }

                                            Is[1] = IS[1]; Is[3] = IS[3];
                                        }
                                    }
                                    //For Current Home
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, iki =>
                                    for (int iki = 0; iki < 8; iki++)
                                    {
                                        int jki = iki * -1 + jk + ik;
                                        if (!Scop(ik, jk, iki, jki, 2))
                                        {
                                            continue;
                                        }
                                        //Ignore of Enemy
                                        if (Order == 1 && Tabl[iki, jki] < 0)
                                        {
                                            continue;
                                        }

                                        if (Order == -1 && Tabl[iki, jki] > 0)
                                        {
                                            continue;
                                        }

                                        if (Is[0] == 1)
                                        {
                                            continue;
                                        }

                                        int Ord = Order;
                                        int[,] Tab = CloneATable(Tabl);
                                        int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMinusPluse, Depth1 = Depth + 1;
                                        bool KindCheckedSelf1 = KindCheckedSelf;
                                        int[] IS = null;
                                        object O1 = new object();
                                        lock (O1)
                                        {
                                            Task<int[]> th = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(Ord, CloneATable(Tab), ik1, jk1, iki1, jki1, OrderP, OrderM, Depth1, KindCheckedSelf1));
                                            th.Wait();
                                            th.Dispose();
                                            if (Is[0] == 1)
                                            {
                                                Is[0] = 1;
                                            }

                                            if (IS[2] == 1)
                                            {
                                                Is[2] = 1;
                                            }

                                            Is[1] = IS[1]; Is[3] = IS[3];
                                        }
                                    }
                                    break;

                                case 3:
                                    //For Current Home
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(ik - 2, ik + 3, iki =>
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(jk - 2, jk + 3, jki =>
                                    for (int iki = ik - 2; iki < ik + 3; iki++)
                                    {
                                        for (int jki = jk - 2; jki < jk + 3; jki++)
                                        {
                                            if (!Scop(ik, jk, iki, jki, 3))
                                            {
                                                continue;
                                            }
                                            //Ignore of Enemy
                                            if (Order == 1 && Tabl[iki, jki] < 0)
                                            {
                                                continue;
                                            }

                                            if (Order == -1 && Tabl[iki, jki] > 0)
                                            {
                                                continue;
                                            }

                                            int Ord = Order;
                                            int[,] Tab = CloneATable(Tabl);
                                            int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMinusPluse, Depth1 = Depth + 1;
                                            bool KindCheckedSelf1 = KindCheckedSelf;
                                            int[] IS = null;
                                            object O1 = new object();
                                            lock (O1)
                                            {
                                                Task<int[]> th = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(Ord, CloneATable(Tab), ik1, jk1, iki1, jki1, OrderP, OrderM, Depth1, KindCheckedSelf1));
                                                th.Wait();
                                                th.Dispose();

                                                if (Is[0] == 1)
                                                {
                                                    Is[0] = 1;
                                                }

                                                if (IS[2] == 1)
                                                {
                                                    Is[2] = 1;
                                                }

                                                Is[1] = IS[1]; Is[3] = IS[3];
                                            }
                                        }
                                    }

                                    break;

                                case 4:
                                    //For Current Home
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, iki =>
                                    for (int iki = 0; iki < 8; iki++)
                                    {
                                        int jki = jk;
                                        if (!Scop(ik, jk, iki, jki, 4))
                                        {
                                            continue;
                                        }
                                        //Ignore of Enemy
                                        if (Order == 1 && Tabl[iki, jki] < 0)
                                        {
                                            continue;
                                        }

                                        if (Order == -1 && Tabl[iki, jki] > 0)
                                        {
                                            continue;
                                        }

                                        if (Is[0] == 1)
                                        {
                                            continue;
                                        }

                                        int Ord = Order;
                                        int[,] Tab = CloneATable(Tabl);
                                        int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMinusPluse, Depth1 = Depth + 1;
                                        bool KindCheckedSelf1 = KindCheckedSelf;
                                        int[] IS = null;
                                        object O1 = new object();
                                        lock (O1)
                                        {
                                            Task<int[]> th = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(Ord, CloneATable(Tab), ik1, jk1, iki1, jki1, OrderP, OrderM, Depth1, KindCheckedSelf1));
                                            th.Wait();
                                            th.Dispose();
                                            if (Is[0] == 1)
                                            {
                                                Is[0] = 1;
                                            }

                                            if (IS[2] == 1)
                                            {
                                                Is[2] = 1;
                                            }

                                            Is[1] = IS[1]; Is[3] = IS[3];
                                        }
                                    }
                                    //For Current Home
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, jki =>
                                    for (int jki = 0; jki < 8; jki++)
                                    {
                                        int iki = ik;
                                        if (!Scop(ik, jk, iki, jki, 4))
                                        {
                                            continue;
                                        }
                                        //Ignore of Enemy
                                        if (Order == 1 && Tabl[iki, jki] < 0)
                                        {
                                            continue;
                                        }

                                        if (Order == -1 && Tabl[iki, jki] > 0)
                                        {
                                            continue;
                                        }

                                        if (Is[0] == 1)
                                        {
                                            continue;
                                        }

                                        int Ord = Order;
                                        int[,] Tab = CloneATable(Tabl);
                                        int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMinusPluse, Depth1 = Depth + 1;
                                        bool KindCheckedSelf1 = KindCheckedSelf;
                                        int[] IS = null;
                                        object O1 = new object();
                                        lock (O1)
                                        {
                                            Task<int[]> th = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(Ord, CloneATable(Tab), ik1, jk1, iki1, jki1, OrderP, OrderM, Depth1, KindCheckedSelf1));
                                            th.Wait();
                                            th.Dispose();

                                            if (Is[0] == 1)
                                            {
                                                Is[0] = 1;
                                            }

                                            if (IS[2] == 1)
                                            {
                                                Is[2] = 1;
                                            }

                                            Is[1] = IS[1]; Is[3] = IS[3];
                                        }
                                    }
                                    break;

                                case 5:

                                    //For Current Home
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, iki =>
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, jki =>
                                    for (int iki = 0; iki < 8; iki++)
                                    {
                                        for (int jki = 0; jki < 8; jki++)
                                        {
                                            //Ignore of Enemy
                                            if (Order == 1 && Tabl[iki, jki] < 0)
                                            {
                                                continue;
                                            }

                                            if (Order == -1 && Tabl[iki, jki] > 0)
                                            {
                                                continue;
                                            }

                                            if (!Scop(ik, jk, iki, jki, 5))
                                            {
                                                continue;
                                            }

                                            if (Is[0] == 1)
                                            {
                                                continue;
                                            }

                                            int Ord = Order;
                                            int[,] Tab = CloneATable(Tabl);
                                            int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMinusPluse, Depth1 = Depth + 1;
                                            bool KindCheckedSelf1 = KindCheckedSelf;
                                            int[] IS = null;
                                            object O1 = new object();
                                            lock (O1)
                                            {
                                                Task<int[]> th = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(Ord, CloneATable(Tab), ik1, jk1, iki1, jki1, OrderP, OrderM, Depth1, KindCheckedSelf1));
                                                th.Wait();
                                                th.Dispose();

                                                if (Is[0] == 1)
                                                {
                                                    Is[0] = 1;
                                                }

                                                if (IS[2] == 1)
                                                {
                                                    Is[2] = 1;
                                                }

                                                Is[1] = IS[1]; Is[3] = IS[3];
                                            }
                                        }
                                    }

                                    break;

                                case 6:
                                    //For Current Home
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(ik - 1, ik + 2, iki =>
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(jk - 1, jk + 2, jki =>
                                    for (int iki = ik - 1; iki < ik + 2; iki++)
                                    {
                                        for (int jki = jk - 1; jki < jk + 2; jki++)
                                        {
                                            if (!Scop(ik, jk, iki, jki, 6))
                                            {
                                                continue;
                                            }
                                            //Ignore of Enemy
                                            if (Order == 1 && Tabl[iki, jki] < 0)
                                            {
                                                continue;
                                            }

                                            if (Order == -1 && Tabl[iki, jki] > 0)
                                            {
                                                continue;
                                            }

                                            if (Is[0] == 1)
                                            {
                                                continue;
                                            }

                                            int Ord = Order;
                                            int[,] Tab = CloneATable(Tabl);
                                            int ik1 = ik, jk1 = jk, iki1 = iki, jki1 = jki, OrderP = OrderPalte, OrderM = OrderPalteMinusPluse, Depth1 = Depth + 1;
                                            bool KindCheckedSelf1 = KindCheckedSelf;
                                            int[] IS = null;
                                            object O1 = new object();
                                            lock (O1)
                                            {
                                                Task<int[]> th = Task.Factory.StartNew(() => IS = IsNextMovmentIsCheckOrCheckMateForCurrentMovmentbaseKernel(Ord, CloneATable(Tab), ik1, jk1, iki1, jki1, OrderP, OrderM, Depth1, KindCheckedSelf1));
                                                th.Wait();
                                                th.Dispose();
                                                if (Is[0] == 1)
                                                {
                                                    Is[0] = 1;
                                                }

                                                if (IS[2] == 1)
                                                {
                                                    Is[2] = 1;
                                                }

                                                Is[1] = IS[1]; Is[3] = IS[3];
                                            }
                                        }
                                    }

                                    break;
                            }
                        }
                    }

                    Order = DummyOrder;
                    ChessRules.CurrentOrder = DummyCurrentOrder;
                }
                return Is;
            }
        }

        //When Current Movements is in dangrous and is not movable.
        private bool IsGardForCurrentMovmentsAndIsNotMovable(int[,] Tab, int Order, Color a, int ii, int jj, int RowS, int ColS)
        {
            object O = new object();
            lock (O)
            {
                //Setting false.
                bool Attacked = true;
                int NumberOfCurrentEnemyAttackSuchObject = 0;
                int DummyOrder = Order;
                int DummyCurrentOrder = ChessRules.CurrentOrder;
                //For Enemy Order.
                object O1 = new object();
                lock (O1)
                {
                    //Ignore of Self Objects.
                    if (Order == 1 && Tab[ii, jj] >= 0)
                    {
                        return false;
                    }
                    else
                        if (Order == -1 && Tab[ii, jj] <= 0)
                    {
                        return false;
                    }
                    //Restore
                    Order = DummyOrder;
                    ChessRules.CurrentOrder = DummyCurrentOrder;
                    NumberOfCurrentEnemyAttackSuchObject = 0;
                    //For Self Objects and Empty.
                    //Ignore of Enemy Objects.
                    if (Order == 1 && Tab[RowS, ColS] < 0)
                    {
                        return false;
                    }
                    else
                        if (Order == -1 && Tab[RowS, ColS] > 0)
                    {
                        return false;
                    }         //For Enemy Order.
                    ChessRules.CurrentOrder = Order * -1;
                    //Initiate for not exiting from abnormal loop.
                    Attacked = false;
                    Color aa = Color.Gray;
                    if (Order * -1 == -1)
                    {
                        aa = Color.Brown;
                    }

                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = Attack(CloneATable(Tab), ii, jj, RowS, ColS, aa, Order * -1) && (ObjectValueCalculator(CloneATable(Tab), ii, jj) < ObjectValueCalculator(CloneATable(Tab), RowS, ColS)));
                    th.Wait();
                    th.Dispose();

                    //When Enemy Attacked Current Movements.
                    if (ab)
                    {
                        NumberOfCurrentEnemyAttackSuchObject++;
                        //Clone a Table.
                        int[,] TabS = new int[8, 8];
                        for (int p = 0; p < 8; p++)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                TabS[p, m] = Tab[p, m];
                            }
                        }

                        TabS[RowS, ColS] = TabS[ii, jj];
                        TabS[ii, jj] = 0;
                        //For Self Objects.
                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, RowD =>
                        for (int RowD = 0; RowD < 8; RowD++)
                        {
                            if (!Attacked || NumberOfCurrentEnemyAttackSuchObject > 1)
                            {
                                continue;
                            }
                            ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, ColD =>
                            for (int ColD = 0; ColD < 8; ColD++)
                            {
                                if (!Attacked || NumberOfCurrentEnemyAttackSuchObject > 1)
                                {
                                    if (Order == 1 && Tab[RowD, ColD] <= 0)
                                    {
                                        continue;
                                    }
                                    else
                                            if (Order == -1 && Tab[RowD, ColD] >= 0)
                                    {
                                        continue;
                                    }
                                }
                                //Show the Attacked.
                                Attacked = true;
                                //For Self Objects and Empty.
                                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, iiiii =>
                                for (int iiiii = 0; iiiii < 8; iiiii++)
                                {
                                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, jjjjj =>
                                    for (int jjjjj = 0; jjjjj < 8; jjjjj++)
                                    {
                                        //Ignore of Enemy Objects.
                                        if (Order == 1 && Tab[iiiii, jjjjj] < 0)
                                        {
                                            continue;
                                        }
                                        else
                                               if (Order == -1 && Tab[iiiii, jjjjj] > 0)
                                        {
                                            continue;
                                        }
                                        //When Current Objects Movable not need to consideration mor going to next Current object.
                                        object O2 = new object();
                                        lock (O2)
                                        {
                                            Task<bool> th1 = Task.Factory.StartNew(() => ab = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TabS[RowD, ColD], TabS, Order)).Rules(RowD, ColD, iiiii, jjjjj, TabS[RowD, ColD]));
                                            th1.Wait();
                                            th1.Dispose();
                                            if (ab)
                                            {
                                                Attacked = Attacked && false;
                                                continue;
                                            }
                                        }
                                    }
                                    if (!Attacked || NumberOfCurrentEnemyAttackSuchObject > 1)
                                    {
                                        continue;
                                    }
                                }
                                if (Attacked || NumberOfCurrentEnemyAttackSuchObject > 1)
                                {
                                    continue;
                                }
                            }
                            if (Attacked || NumberOfCurrentEnemyAttackSuchObject > 1)
                            {
                                continue;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                //Restore.
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;

                //continue Variable when true show an object is not movable or one enemy object attack more than one current Object.
                return Attacked || NumberOfCurrentEnemyAttackSuchObject > 1;
            }
        }

        ///when current movments gards enemy with higer priority at movment.QC_OK
        private bool IsCurrentCanGardHighPriorityEnemy(int Depth, int[,] Table, int Order, Color a, int ij, int ji, int iij, int jji, int OrderPlate)
        {
            object O = new object();
            lock (O)
            {
                if (Depth >= CurrentAStarGredyMax)
                {
                    return false;
                }
                object O4 = new object();
                lock (O4)
                {
                    Depth++;
                    IsGardHighPriority = false;
                    int[,] Tabl1 = new int[8, 8];
                    for (int ik = 0; ik < 8; ik++)
                    {
                        for (int jk = 0; jk < 8; jk++)
                        {
                            Tabl1[ik, jk] = Table[ik, jk];
                        }
                    }
                    //For Current.
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            //Ignore of Enemy.QC_OK.
                            if (Order == 1 && Tabl1[i, j] <= 0)
                            {
                                continue;
                            }
                            else
                                if (Order == -1 && Tabl1[i, j] >= 0)
                            {
                                continue;
                            }
                            //For Enemy.
                            for (int ii = 0; ii < 8; ii++)
                            {
                                for (int jj = 0; jj < 8; jj++)
                                {
                                    //Ignore of Current.QC_OK.
                                    if (Order == 1 && Tabl1[ii, jj] >= 0)
                                    {
                                        continue;
                                    }
                                    else
                                        if (Order == -1 && Tabl1[ii, jj] >= 0)
                                    {
                                        continue;
                                    }

                                    for (int ik = 0; ik < 8; ik++)
                                    {
                                        for (int jk = 0; jk < 8; jk++)
                                        {
                                            Tabl1[ik, jk] = Table[ik, jk];
                                        }
                                    }
                                    //Take Movement.
                                    bool ab = false;
                                    Task<bool> th = Task.Factory.StartNew(() => ab = Attack(Tabl1, i, j, ii, jj, a, Order * -1));
                                    th.Wait();
                                    th.Dispose();
                                    if (ab)
                                    {
                                        Task<bool> th1 = Task.Factory.StartNew(() => ab = ObjectValueCalculator(Tabl1, i, j) <= ObjectValueCalculator(Tabl1, ii, jj));
                                        th1.Wait();
                                        th1.Dispose();
                                        if (ab)
                                        {//When Current Movments is
                                            if (Order == OrderPlate)
                                            {
                                                IsGardHighPriority = true;
                                            }
                                        }
                                        else
                                        {
                                            Tabl1[ii, jj] = Tabl1[i, j];
                                            Tabl1[i, j] = 0;
                                            if (Order * -1 == 1)
                                            {
                                                a = Color.Gray;
                                            }
                                            else
                                            {
                                                a = Color.Brown;
                                            }

                                            Task<bool> th2 = Task.Factory.StartNew(() => IsGardHighPriority = IsGardHighPriority || IsCurrentCanGardHighPriorityEnemy(Depth, CloneATable(Table), Order * -1, a, ii, jj, i, j, OrderPlate));
                                            th2.Wait();
                                            th2.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                return IsGardHighPriority;
            }
        }

        private bool CurrentIsTowCastleOrMinisterBecomeCheckedMateAtCloseRanAway(int RowK, int ColK, int[,] Table)
        {
            object O = new object();
            lock (O)
            {
                if (!IsNumberOfObjecttIsLessThanThreashold(CloneATable(Table)))
                {
                    return false;
                }

                bool Is = false;
                int IsN = 0;
                if (Table[RowK, ColK] != 0)
                {
                    int Sign = (System.Math.Abs(Table[RowK, ColK]) / Table[RowK, ColK]) * -1;
                    int Obj1 = Sign * 4;
                    int Obj2 = Sign * 5;
                    for (int k = 0; k < 8; k++)
                    {
                        if (RowK == k)
                        {
                            continue;
                        }

                        if (Table[k, ColK] == Obj1 || Table[k, ColK] == Obj2)
                        {
                            IsN++;
                        }
                        else
                        if (Table[k, ColK] != 0)
                        {
                            IsN = 0;
                        }

                        for (int p = 0; p < 8; p++)
                        {
                            if (p == ColK)
                            {
                                continue;
                            }

                            if (Table[k, p] == Obj1 || Table[k, p] == Obj2)
                            {
                                IsN++;
                            }
                            else
                        if (Table[k, p] != 0)
                            {
                                IsN = 0;
                            }
                        }
                    }

                    if (IsN >= 2)
                    {
                        return true;
                    }

                    IsN = 0;
                    for (int k = 0; k < 8; k++)
                    {
                        if (ColK == k)
                        {
                            continue;
                        }

                        if (Table[RowK, k] == Obj1 || Table[RowK, k] == Obj2)
                        {
                            IsN++;
                        }
                        else
                        if (Table[RowK, 0] != 0)
                        {
                            IsN = 0;
                        }

                        for (int p = 0; p < 8; p++)
                        {
                            if (p == RowK)
                            {
                                continue;
                            }

                            if (Table[p, k] == Obj1 || Table[p, k] == Obj2)
                            {
                                IsN++;
                            }
                            else
                        if (Table[p, k] != 0)
                            {
                                IsN = 0;
                            }
                        }
                    }
                    if (IsN >= 2)
                    {
                        Is = true;
                    }
                }
                return Is;
            }
        }

        private bool SameSign(int Obj1, int Obj2)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                if (Obj1 != 0 && Obj2 != 0)
                {
                    if ((System.Math.Abs(Obj1) / Obj1) == (System.Math.Abs(Obj2) / Obj2))
                    {
                        Is = true;
                    }
                }
                return Is;
            }
        }

        private bool ThereIsOneSideToRanAwayByEnemyKing(int RowK, int ColK, int[,] Table)
        {
            object O = new object();
            lock (O)
            {
                if (!IsNumberOfObjecttIsLessThanThreashold(CloneATable(Table)))
                {
                    return false;
                }

                bool Is = false;
                if ((ColK == 7) && (ColK - 1 >= 0) && (RowK - 1 >= 0) && (RowK + 1 < 8))
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = SameSign(Table[RowK, ColK], Table[RowK - 1, ColK - 1]) && SameSign(Table[RowK, ColK], Table[RowK + 1, ColK - 1]) && SameSign(Table[RowK, ColK], Table[RowK, ColK - 1]));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Is = true;
                    }
                }
                if ((ColK == 0) && (ColK + 1 < 8) && (RowK - 1 >= 0) && (RowK + 1 < 8))
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = SameSign(Table[RowK, ColK], Table[RowK - 1, ColK + 1]) && SameSign(Table[RowK, ColK], Table[RowK + 1, ColK + 1]) && SameSign(Table[RowK, ColK], Table[RowK, ColK + 1]));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Is = true;
                    }
                }
                return Is;
            }
        }

        private bool CurrentCanBecomeClosedRanAwayByOneCastleOrMinister(int RowK, int ColK, int[,] Table)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = ThereIsOneSideToRanAwayByEnemyKing(RowK, ColK, CloneATable(Table)));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        if (k == ColK)
                        {
                            continue;
                        }

                        for (int p = 0; p < 8; p++)
                        {
                            Task<bool> th1 = Task.Factory.StartNew(() => ab = SameSign(Table[RowK, ColK], Table[p, k]));
                            th1.Wait();
                            th1.Dispose();
                            if (!ab)
                            {
                                if (Table[p, k] != 0)
                                {
                                    int Obj = System.Math.Abs(Table[p, k]) / Table[p, k];
                                    int Obj1 = Obj * 4;
                                    int Obj2 = Obj * 5;
                                    if (Table[p, k] == Obj1)
                                    {
                                        return true;
                                    }

                                    if (Table[p, k] == Obj2)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    for (int k = 0; k < 8; k++)
                    {
                        if (k == RowK)
                        {
                            continue;
                        }

                        for (int p = 0; p < 8; p++)
                        {
                            Task<bool> th1 = Task.Factory.StartNew(() => ab = SameSign(Table[RowK, ColK], Table[k, p]));
                            th1.Wait();
                            th1.Dispose();
                            if (!ab)
                            {
                                if (Table[k, p] != 0)
                                {
                                    int Obj = System.Math.Abs(Table[k, p]) / Table[k, p];
                                    int Obj1 = Obj * 4;
                                    int Obj2 = Obj * 5;
                                    if (Table[k, p] == Obj1)
                                    {
                                        return true;
                                    }

                                    if (Table[k, p] == Obj2)
                                    {
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }
                return Is;
            }
        }

        private bool IsObjectrSelfAttackEnemyKing(int Rowk, int ColK, int[,] Table, int Order)
        {
            object O = new object();
            lock (O)
            {
                if (!IsNumberOfObjecttIsLessThanThreashold(CloneATable(Table)))
                {
                    return false;
                }

                bool Is = false;
                const int MinisteGrayObj = 5, CastleGrayObj = 4, ElepahantGrayObj = 2, PawnGrayObj = 1;
                const int MinisteBrownObj = -5, CastleBrownObj = -4//, ElephantBrownObj = -2
                , PawnBrownObj = -1;
                if (Order == 1)
                {
                    if (ColK == 0)
                    {
                        if (Table[Rowk, ColK + 1] == ElepahantGrayObj)
                        {
                            if (Table[Rowk, ColK + 2] == CastleGrayObj)
                            {
                                if ((Table[Rowk - 1, ColK + 1] == PawnGrayObj) || (Table[Rowk + 1, ColK + 1] == PawnGrayObj))
                                {
                                    Is = true;
                                }
                            }
                            if (Table[Rowk + 1, ColK + 2] == CastleGrayObj)
                            {
                                if ((Table[Rowk - 1, ColK + 1] == MinisteGrayObj) || (Table[Rowk + 1, ColK + 1] == MinisteGrayObj))
                                {
                                    Is = true;
                                }
                            }
                        }
                        if (Table[Rowk, ColK + 1] == PawnGrayObj)
                        {
                            if (Table[Rowk + 1, ColK + 2] == MinisteGrayObj)
                            {
                                if (Table[Rowk - 1, ColK + 2] == PawnGrayObj)
                                {
                                    Is = true;
                                }
                            }
                            if (Table[Rowk + 1, ColK + 2] == MinisteGrayObj)
                            {
                                if (Table[Rowk - 2, ColK + 2] == PawnGrayObj)
                                {
                                    Is = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (ColK == 7)
                    {
                        if (Table[Rowk, ColK - 1] == ElepahantGrayObj)
                        {
                            if (Table[Rowk, ColK - 2] == CastleBrownObj)
                            {
                                if ((Table[Rowk - 1, ColK - 1] == PawnBrownObj) || (Table[Rowk + 1, ColK - 1] == PawnBrownObj))
                                {
                                    Is = true;
                                }
                            }
                            if (Table[Rowk - 1, ColK - 2] == CastleBrownObj)
                            {
                                if ((Table[Rowk - 1, ColK - 1] == MinisteBrownObj) || (Table[Rowk + 1, ColK - 1] == MinisteBrownObj))
                                {
                                    Is = true;
                                }
                            }
                        }
                        if (Table[Rowk, ColK - 1] == PawnBrownObj)
                        {
                            if (Table[Rowk - 1, ColK - 2] == MinisteBrownObj)
                            {
                                if (Table[Rowk + 1, ColK - 2] == PawnBrownObj)
                                {
                                    Is = true;
                                }
                            }
                            if (Table[Rowk - 1, ColK - 2] == MinisteBrownObj)
                            {
                                if (Table[Rowk + 1, ColK - 2] == PawnBrownObj)
                                {
                                    Is = true;
                                }
                            }
                        }
                    }
                }
                return Is;
            }
        }

        public double SimpleMate_Zero(ref double HA, int[,] Table)
        {
            object O = new object();
            lock (O)
            {
                ////double HA =1;
                if (Order == 1)
                {
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Table), Order);
                    int[,] Tab = CloneATable(Table);
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = G.CheckMate(CloneATable(Tab), Order));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        if (Order == 1 && G.CheckMateBrown)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        else
                     if (Order == 1 && G.CheckMateGray)
                        {
                            HA += Rational(HA, RationalPenalty) * RationalPenalty;
                        }
                    }
                }
                else
                {
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Table), Order);
                    int[,] Tab = CloneATable(Table);
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = G.CheckMate(CloneATable(Tab), Order));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        if (Order == -1 && G.CheckMateGray)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        else
                        if (Order == -1 && G.CheckMateBrown)
                        {
                            HA += Rational(HA, RationalPenalty) * RationalPenalty;
                        }
                    }
                }
                return HA;
            }
        }

        public double SimpleMate_One(ref double HA, int[,] Table)
        {
            object O = new object();
            lock (O)
            {
                ////double HA =1;
                if (Order == 1)
                {
                    int RowK = -1, ColK = -1;
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Table), Order);
                    G.FindBrownKing(CloneATable(Table), ref RowK, ref ColK);
                    bool S1 = false;
                    Task<bool> H1 = Task.Factory.StartNew(() => S1 = CurrentIsTowCastleOrMinisterBecomeCheckedMateAtCloseRanAway(RowK, ColK, CloneATable(Table)));
                    H1.Wait();
                    H1.Dispose();
                    if (S1)
                    {
                        HA += Rational(HA, RationalRegard) * RationalRegard;
                    }
                    else
                    {
                        bool S2 = false;
                        Task<bool> H2 = Task.Factory.StartNew(() => S2 = CurrentCanBecomeClosedRanAwayByOneCastleOrMinister(RowK, ColK, CloneATable(Table)));
                        H2.Wait();
                        H2.Dispose();
                        if (S2)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        else
                        {
                            bool S3 = false;
                            Task<bool> H3 = Task.Factory.StartNew(() => S3 = IsObjectrSelfAttackEnemyKing(RowK, ColK, CloneATable(Table), Order));
                            H3.Wait();
                            H3.Dispose();
                            if (S3)
                            {
                                HA += Rational(HA, RationalRegard) * RationalRegard;
                            }
                        }
                    }
                }
                else
                {
                    int RowK = -1, ColK = -1;
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Table), Order);
                    G.FindGrayKing(CloneATable(Table), ref RowK, ref ColK);
                    bool S1 = false;
                    Task<bool> H1 = Task.Factory.StartNew(() => S1 = CurrentIsTowCastleOrMinisterBecomeCheckedMateAtCloseRanAway(RowK, ColK, CloneATable(Table)));
                    H1.Wait();
                    H1.Dispose();
                    if (S1)
                    {
                        HA += Rational(HA, RationalRegard) * RationalRegard;
                    }
                    else
                    {
                        bool S2 = false;
                        Task<bool> H2 = Task.Factory.StartNew(() => S2 = CurrentCanBecomeClosedRanAwayByOneCastleOrMinister(RowK, ColK, CloneATable(Table)));
                        H2.Wait();
                        H2.Dispose();
                        if (S2)
                        {
                            HA += Rational(HA, RationalRegard) * RationalRegard;
                        }
                        else
                        {
                            bool S3 = false;
                            Task<bool> H3 = Task.Factory.StartNew(() => S2 = IsObjectrSelfAttackEnemyKing(RowK, ColK, CloneATable(Table), Order));
                            H3.Wait();
                            H3.Dispose();
                            if (S3)
                            {
                                HA += Rational(HA, RationalRegard) * RationalRegard;
                            }
                        }
                    }
                }
                return HA;
            }
        }

        public double SimpleMate_Tow(ref double HA, int[,] Table)
        {
            object O = new object();
            lock (O)
            {
                ////double HA =1;
                if (Order == 1)
                {
                    int RowK = -1, ColK = -1;
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Table), Order);
                    G.FindBrownKing(CloneATable(Table), ref RowK, ref ColK);
                    if (EnemyKingCanMateByCloseHome(RowK, ColK, CloneATable(Table), Order))
                    {
                        HA += Rational(HA, RationalRegard) * RationalRegard;
                    }
                }
                else
                {
                    int RowK = -1, ColK = -1;
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Table), Order);
                    G.FindGrayKing(CloneATable(Table), ref RowK, ref ColK);
                    if (EnemyKingCanMateByCloseHome(RowK, ColK, CloneATable(Table), Order))
                    {
                        HA += Rational(HA, RationalRegard) * RationalRegard;
                    }
                }
                return HA;
            }
        }

        public double SimpleMate_Three_And_Four(ref double HA, int[,] Table)
        {
            object O = new object();
            lock (O)
            {
                ////double HA =1;
                if (Order == 1)
                {
                    int RowK = -1, ColK = -1;
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Table), Order);
                    G.FindBrownKing(CloneATable(Table), ref RowK, ref ColK);
                    if (EnemyKingHaveAtMostOneEmptyItemInAttack(RowK, ColK, CloneATable(Table), Order))
                    {
                        HA += Rational(HA, RationalRegard) * RationalRegard;
                    }
                }
                else
                {
                    int RowK = -1, ColK = -1;
                    ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Table), Order);
                    G.FindGrayKing(CloneATable(Table), ref RowK, ref ColK);
                    if (EnemyKingHaveAtMostOneEmptyItemInAttack(RowK, ColK, CloneATable(Table), Order))
                    {
                        HA += Rational(HA, RationalRegard) * RationalRegard;
                    }
                }
                return HA;
            }
        }

        public int EnemyKingHaveAtMostOneEmptyItem(int Rowk, int ColK, int[,] Table, ref List<int> EmptyR, ref List<int> EmptyC)
        {
            object O = new object();
            lock (O)
            {
                int NIs = 0;
                if (Rowk >= 0 && Rowk < 8 && ColK >= 0 && ColK < 8)
                {
                    if ((ColK - 1 >= 0))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk, ColK - 1]))
                        {
                            EmptyR.Add(Rowk);
                            EmptyC.Add(ColK - 1);
                            NIs++;
                        }
                    }
                    if ((ColK + 1 < 8))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk, ColK + 1]))
                        {
                            EmptyR.Add(Rowk);
                            EmptyC.Add(ColK + 1);
                            NIs++;
                        }
                    }
                    if ((Rowk - 1 >= 0))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk - 1, ColK]))
                        {
                            EmptyR.Add(Rowk - 1);
                            EmptyC.Add(ColK);
                            NIs++;
                        }
                    }
                    if ((Rowk + 1 < 8))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk + 1, ColK]))
                        {
                            EmptyR.Add(Rowk + 1);
                            EmptyC.Add(ColK);
                            NIs++;
                        }
                    }
                    if ((ColK - 1 >= 0) && (Rowk - 1 >= 0))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk - 1, ColK - 1]))
                        {
                            EmptyR.Add(Rowk - 1);
                            EmptyC.Add(ColK - 1);
                            NIs++;
                        }
                    }
                    if ((ColK - 1 >= 0) && (Rowk + 1 < 8))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk + 1, ColK - 1]))
                        {
                            EmptyR.Add(Rowk + 1);
                            EmptyC.Add(ColK - 1);
                            NIs++;
                        }
                    }
                    if ((ColK + 1 < 8) && (Rowk + 1 < 8))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk + 1, ColK + 1]))
                        {
                            EmptyR.Add(Rowk + 1);
                            EmptyC.Add(ColK + 1);
                            NIs++;
                        }
                    }
                    if ((ColK + 1 < 8) && (Rowk - 1 >= 0))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk - 1, ColK + 1]))
                        {
                            EmptyR.Add(Rowk - 1);
                            EmptyC.Add(ColK + 1);
                            NIs++;
                        }
                    }
                    if ((ColK + 1 < 8) && (Rowk - 1 >= 0))
                    {
                        if (!SameSign(Table[Rowk, ColK], Table[Rowk - 1, ColK + 1]))
                        {
                            EmptyR.Add(Rowk - 1);
                            EmptyC.Add(ColK + 1);
                            NIs++;
                        }
                    }
                }
                return NIs;
            }
        }

        public bool EnemyKingHaveAtMostOneEmptyItemInAttack(int Rowk, int ColK, int[,] Table, int Order)
        {
            bool ab = false;
            Task<bool> th = Task.Factory.StartNew(() => ab = IsNumberOfObjecttIsLessThanThreashold(CloneATable(Table)));
            th.Wait();
            th.Dispose();
            if (!ab)
            {
                return false;
            }

            object O = new object();
            lock (O)
            {
                //#pragma warning disable CS0219 // The variable 'NIs' is assigned but its value is never used
#pragma warning disable CS0219 // The variable 'NIs' is assigned but its value is never used
                int NIs = 0;
#pragma warning restore CS0219 // The variable 'NIs' is assigned but its value is never used
                //#pragma warning restore CS0219 // The variable 'NIs' is assigned but its value is never used
                for (int k = 0; k < 8; k++)
                {
                    for (int p = 0; p < 8; p++)
                    {
                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), k, p, Rowk, ColK, color, Order));
                        th1.Wait();
                        th1.Dispose();
                        if (ab)
                        {
                            for (int kk = 0; kk < 8; kk++)
                            {
                                for (int pp = 0; pp < 8; pp++)
                                {
                                    for (int kkk = 0; kkk < 8; kkk++)
                                    {
                                        for (int ppp = 0; ppp < 8; ppp++)
                                        {
                                            Task<bool> th2 = Task.Factory.StartNew(() => ab = Movable(CloneATable(Table), kk, pp, kkk, ppp, color, Order));
                                            th2.Wait();
                                            th2.Dispose();
                                            if (ab)
                                            {
                                                int[,] Ta = CloneATable(Table);
                                                Ta[kkk, ppp] = Ta[kk, pp];
                                                Ta[kk, pp] = 0;
                                                ChessRules G = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Order, CloneATable(Ta), Order);
                                                Task<bool> th3 = Task.Factory.StartNew(() => ab = G.CheckMate(CloneATable(Ta), Order));
                                                th3.Wait();
                                                th3.Dispose();
                                                if (ab)
                                                {
                                                    return true;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return false;
            }
        }

        private bool IsNumberOfObjecttIsLessThanThreashold(int[,] Tab, int Threashold = 30)
        {
            object O = new object();
            lock (O)
            {
                int ObjN = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Tab[i, j] != 0)
                        {
                            ObjN++;
                        }
                    }
                }
                if (ObjN <= Threashold)
                {
                    return true;
                }

                return false;
            }
        }

        public bool EnemyKingCanMateByCloseHome(int RowK, int ColK, int[,] Table, int Order)
        {
            object O = new object();
            lock (O)
            {
                if (!IsNumberOfObjecttIsLessThanThreashold(CloneATable(Table)))
                {
                    return false;
                }

                bool Is = false;
                List<int> EmptyR = new List<int>(), EmptyC = new List<int>();
                int NIs = 0;
                Task<int> th = Task.Factory.StartNew(() => NIs = EnemyKingHaveAtMostOneEmptyItem(RowK, ColK, CloneATable(Table), ref EmptyR, ref EmptyC));
                th.Wait();
                th.Dispose();

                //King Have One HomeAtlist movment
                if (NIs <= 2)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        for (int p = 0; p < 8; p++)
                        {
                            if (Order == 1 & Table[k, p] <= 0)
                            {
                                continue;
                            }

                            if (Order == -1 & Table[k, p] >= 0)
                            {
                                continue;
                            }

                            int[,] Tab = CloneATable(Table);
                            for (int kk = 0; kk < 8; kk++)
                            {
                                for (int pp = 0; pp < 8; pp++)
                                {
                                    if (Order == 1 & Table[kk, pp] <= 0)
                                    {
                                        continue;
                                    }

                                    if (Order == -1 & Table[kk, pp] >= 0)
                                    {
                                        continue;
                                    }
                                    //Self Have Support
                                    bool ab = false;
                                    Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(Tab), kk, pp, k, p, color, Order));
                                    th1.Wait();
                                    th1.Dispose();
                                    if (ab)
                                    {
                                        for (int kkk = 0; kkk < 8; kkk++)
                                        {
                                            for (int ppp = 0; ppp < 8; ppp++)
                                            {
                                                if (Order == 1 & Table[kkk, ppp] > 0)
                                                {
                                                    continue;
                                                }

                                                if (Order == -1 & Table[kkk, ppp] < 0)
                                                {
                                                    continue;
                                                }
                                                //Enemy King Attack
                                                Task<bool> th2 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Tab), k, p, kkk, ppp, color, Order));
                                                th2.Wait();
                                                th2.Dispose();
                                                if (ab)
                                                {
                                                    int[,] Ta = CloneATable(Tab);
                                                    Ta[kkk, ppp] = Ta[k, p];
                                                    Ta[k, p] = 0;
                                                    ChessRules A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Ta[kkk, ppp], CloneATable(Tab), Order);
                                                    if (A.CheckMate(CloneATable(Ta), Order * 1))
                                                    {
                                                        return true;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return Is;
            }
        }

        private bool IsMinisterOrElephantBecomeActive(int RowS, int ColS, int[,] Table)
        {
            bool Is = false;
            const int ElephantGray = 2, ElephantBrown = -2;
            const int MinisterGray = 2, MinisterBrown = -2;
            if (Order == 1)
            {
                if (Table[7, 2] == ElephantGray)
                {
                    if (ColS == 6 && RowS == 1)
                    {
                        Is = true;
                    }

                    if (ColS == 6 && RowS == 3)
                    {
                        Is = true;
                    }
                }
                if (Table[7, 5] == ElephantGray)
                {
                    if (ColS == 6 && RowS == 4)
                    {
                        Is = true;
                    }

                    if (ColS == 6 && RowS == 6)
                    {
                        Is = true;
                    }
                }
                if (Table[7, 3] == MinisterGray)
                {
                    if (ColS == 6 && RowS == 2)
                    {
                        Is = true;
                    }

                    if (ColS == 6 && RowS == 3)
                    {
                        Is = true;
                    }

                    if (ColS == 6 && RowS == 4)
                    {
                        Is = true;
                    }
                }
            }
            else
            {
                if (Table[0, 2] == ElephantBrown)
                {
                    if (ColS == 1 && RowS == 1)
                    {
                        Is = true;
                    }

                    if (ColS == 1 && RowS == 3)
                    {
                        Is = true;
                    }
                }
                if (Table[0, 5] == ElephantBrown)
                {
                    if (ColS == 1 && RowS == 4)
                    {
                        Is = true;
                    }

                    if (ColS == 1 && RowS == 6)
                    {
                        Is = true;
                    }
                }
                if (Table[0, 3] == MinisterBrown)
                {
                    if (ColS == 1 && RowS == 2)
                    {
                        Is = true;
                    }

                    if (ColS == 1 && RowS == 3)
                    {
                        Is = true;
                    }

                    if (ColS == 1 && RowS == 4)
                    {
                        Is = true;
                    }
                }
            }
            return Is;
        }

        private bool IsContorlCenter(int RowS, int ColS, int RowD, int ColD, int[,] Table, Color a)
        {
            bool Is = false;
            const int ControlF = 3, ControlS = 4;
            if ((RowD == ControlF || RowD == ControlS || ColD == ControlF || ColD == ControlS))
            {
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = Support(CloneATable(Table), RowS, ColS, RowD, ColD, a, Order));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    Is = true;
                }

                Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), RowS, ColS, RowD, ColD, a, Order));
                th1.Wait();
                th1.Dispose();
                if (ab)
                {
                    Is = true;
                }
            }
            return Is;
        }

        ///Heuristic of Check and CheckMate.
        public double HeuristicCheckAndCheckMate(ref double HS, int RowS, int ColS, int RowD, int ColD, int[,] Table, Color a)
        {
            object O = new object();
            lock (O)
            {
                double HA = HS;
                Task<double> H1 = Task.Factory.StartNew(() => HA += Rational(HA, SimpleMate_Zero(ref HA, CloneATable(Table))) * SimpleMate_Zero(ref HA, CloneATable(Table)));
                H1.Wait();
                H1.Dispose();

                if (HA == 1)
                {
                    Task<double> H2 = Task.Factory.StartNew(() => HA += Rational(HA, SimpleMate_One(ref HA, CloneATable(Table))) * SimpleMate_One(ref HA, CloneATable(Table)));
                    H2.Wait();
                    H2.Dispose();
                }
                if (HA == 1)
                {
                    Task<double> H3 = Task.Factory.StartNew(() => HA += Rational(HA, SimpleMate_Tow(ref HA, CloneATable(Table))) * SimpleMate_Tow(ref HA, CloneATable(Table)));
                    H3.Wait();
                    H3.Dispose();
                }
                if (HA == 1)
                {
                    Task<double> H4 = Task.Factory.StartNew(() => HA += Rational(HA, SimpleMate_Three_And_Four(ref HA, CloneATable(Table))) * SimpleMate_Three_And_Four(ref HA, CloneATable(Table)));
                    H4.Wait();
                    H4.Dispose();
                }
                bool S1 = false;

                Task<bool> H5 = Task.Factory.StartNew(() => S1 = IsContorlCenter(RowS, ColS, RowD, ColD, CloneATable(Table), a));
                H5.Wait();
                H5.Dispose();
                if (S1)
                {
                    HA += Rational(HA, RationalRegard) * RationalRegard;
                }
                bool S2 = false;

                Task<bool> H6 = Task.Factory.StartNew(() => S2 = IsMinisterOrElephantBecomeActive(RowS, ColS, CloneATable(Table)));
                H6.Wait();
                H6.Dispose();
                if (S2)
                {
                    HA += Rational(HA, RationalRegard) * RationalRegard;
                }
                HA = HS;
                return HA;
            }
        }

        //Attacks on Enemies.
        private int AttackerCount(int[,] Table, int Order, Color a, int i, int j)
        {
            object O = new object();
            lock (O)
            {
                int Count = 0;
                int DummyOrder = Order;
                int DummyCurrentOrder = ChessRules.CurrentOrder;
                int[,] Tab = new int[8, 8];
                for (int h = 0; h < 8; h++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        Tab[h, k] = Table[h, k];
                    }
                }
                //For Slef Objects..
                for (int ii = 0; ii < 8; ii++)
                {
                    for (int jj = 0; jj < 8; jj++)
                    {
                        //Ignore Of Self Objects
                        if (Order == 1 && Tab[ii, jj] >= 0)
                        {
                            continue;
                        }
                        else
                            if (Order == -1 && Tab[ii, jj] <= 0)
                        {
                            continue;
                        }
                        //If Current Attacks Enemy.
                        bool ab = false;
                        Task<bool> th = Task.Factory.StartNew(() => ab = Attack(CloneATable(Tab), i, j, ii, jj, a, Order));
                        th.Wait();
                        th.Dispose();
                        if (ab)
                        {
                            Count++;
                        }
                    }
                }

                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                return Count;
            }
        }

        //clear TableInitiationPreventionOfMultipleMoveWhenAll
        private void MakeEmptyTableInitiationPreventionOfMultipleMoveWhenAllIsFull()
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (TableInitiationPreventionOfMultipleMove[i, j] == 0)
                        {
                            Is = true;
                        }
                    }
                }
                if (!Is)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            TableInitiationPreventionOfMultipleMove[i, j] = 0;
                        }
                    }
                }
            }
        }

        //determine when specified ro column of TableInitiationPreventionOfMultipleMoveWhenAll is zero and empty
        private bool IsTableRowColIsZero(int Row, int Col)
        {
            object O = new object();
            lock (O)
            {
                Task th = Task.Factory.StartNew(() => MakeEmptyTableInitiationPreventionOfMultipleMoveWhenAllIsFull());
                th.Wait();
                th.Dispose();

                bool Is = false;
                if (TableInitiationPreventionOfMultipleMove[Row, Col] == 0)
                {
                    return true;
                }
                return Is;
            }
        }

        //when situation of cerntralized location pawn object is ok
        public bool IsCentralPawnIsOk(int[,] Tab, int Order)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                int NoOfPawn = 0;
                int NoOfSupport = 0;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Order == 1)
                        {
                            if (Tab[i, j] == 1)
                            {
                                NoOfPawn++;
                            }

                            if (i - 1 >= 0 && j + 1 < 8)
                            {
                                if (Tab[i - 1, j + 1] == 1)
                                {
                                    NoOfSupport++;
                                }
                            }
                            if (i - 1 >= 0 && j - 1 >= 0)
                            {
                                if (Tab[i - 1, j - 1] == 1)
                                {
                                    NoOfSupport++;
                                }
                            }
                        }
                        else
                        {
                            if (Tab[i, j] == -1)
                            {
                                NoOfPawn++;
                            }

                            if (i + 1 < 8 && j + 1 < 8)
                            {
                                if (Tab[i + 1, j + 1] == -1)
                                {
                                    NoOfSupport++;
                                }
                            }
                            if (i + 1 < 8 && j - 1 >= 0)
                            {
                                if (Tab[i + 1, j - 1] == -1)
                                {
                                    NoOfSupport++;
                                }
                            }
                        }
                    }
                }
                if (NoOfSupport >= (NoOfPawn / 2))
                {
                    Is = true;
                }

                return Is;
            }
        }

        //when center is controled by traversal objects.
        public bool CenrtrallnControlByTraversal(int[,] Tab, Color a, int Order, int RowS, int ColS, int RowD, int ColD)
        {
            object O = new object();
            lock (O)
            {
                Color aa = Color.Gray;
                if (Order * -1 == -1)
                {
                    aa = Color.Brown;
                }

                bool Is = false;
                if (Tab[RowS, ColS] == 1 || Tab[RowS, ColS] == -1)
                {
                    return Is;
                }

                if (Tab[RowS, ColS] != 0)
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = (Tab[3, 4] == 0) && Movable(CloneATable(Tab), RowS, ColS, 3, 4, a, Order));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Is = true;
                    }

                    Task<bool> th1 = Task.Factory.StartNew(() => ab = (Tab[4, 3] == 0) && Movable(CloneATable(Tab), RowS, ColS, 4, 3, a, Order));
                    th1.Wait();
                    th1.Dispose();
                    if (ab)
                    {
                        Is = true;
                    }

                    Task<bool> th2 = Task.Factory.StartNew(() => ab = (Tab[3, 3] == 0) && Movable(CloneATable(Tab), RowS, ColS, 3, 3, a, Order));
                    th2.Wait();
                    th2.Dispose();
                    if (ab)
                    {
                        Is = true;
                    }

                    Task<bool> th3 = Task.Factory.StartNew(() => ab = (Tab[4, 4] == 0) && Movable(CloneATable(Tab), RowS, ColS, 4, 4, a, Order));
                    th3.Wait();
                    th3.Dispose();
                    if (ab)
                    {
                        Is = true;
                    }

                    if (!Is)
                    {
                        if (Order == 1)
                        {
                            Task<bool> th4 = Task.Factory.StartNew(() => ab = (Tab[3, 4] < 0) && Attack(CloneATable(Tab), RowS, ColS, 3, 4, aa, Order * -1));
                            th4.Wait();
                            th4.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th5 = Task.Factory.StartNew(() => ab = (Tab[4, 3] < 0) && Attack(CloneATable(Tab), RowS, ColS, 4, 3, aa, Order * -1));
                            th5.Wait();
                            th5.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th6 = Task.Factory.StartNew(() => ab = (Tab[3, 3] < 0) && Attack(CloneATable(Tab), RowS, ColS, 3, 3, aa, Order * -1));
                            th6.Wait();
                            th6.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th7 = Task.Factory.StartNew(() => ab = (Tab[4, 4] < 0) && Attack(CloneATable(Tab), RowS, ColS, 4, 4, aa, Order * -1));
                            th7.Wait();
                            th7.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th8 = Task.Factory.StartNew(() => ab = (Tab[3, 4] > 0) && Support(CloneATable(Tab), RowS, ColS, 3, 4, a, Order));
                            th8.Wait();
                            th8.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th9 = Task.Factory.StartNew(() => ab = (Tab[4, 3] > 0) && Support(CloneATable(Tab), RowS, ColS, 4, 3, a, Order));
                            th9.Wait();
                            th9.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th10 = Task.Factory.StartNew(() => ab = (Tab[3, 3] > 0) && Support(CloneATable(Tab), RowS, ColS, 3, 3, a, Order));
                            th10.Wait();
                            th10.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th11 = Task.Factory.StartNew(() => ab = (Tab[4, 4] > 0) && Support(CloneATable(Tab), RowS, ColS, 4, 4, a, Order));
                            th11.Wait();
                            th11.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }
                        }
                        else
                        {
                            Task<bool> th4 = Task.Factory.StartNew(() => ab = (Tab[3, 4] > 0) && Attack(CloneATable(Tab), RowS, ColS, 3, 4, aa, Order * -1));
                            th4.Wait();
                            th4.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th5 = Task.Factory.StartNew(() => ab = (Tab[4, 3] > 0) && Attack(CloneATable(Tab), RowS, ColS, 4, 3, aa, Order * -1));
                            th5.Wait();
                            th5.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th6 = Task.Factory.StartNew(() => ab = (Tab[3, 3] > 0) && Attack(CloneATable(Tab), RowS, ColS, 3, 3, aa, Order * -1));
                            th6.Wait();
                            th6.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th7 = Task.Factory.StartNew(() => ab = (Tab[4, 4] > 0) && Attack(CloneATable(Tab), RowS, ColS, 4, 4, aa, Order * -1));
                            th7.Wait();
                            th7.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th8 = Task.Factory.StartNew(() => ab = (Tab[3, 4] < 0) && Support(CloneATable(Tab), RowS, ColS, 3, 4, a, Order));
                            th8.Wait();
                            th8.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th9 = Task.Factory.StartNew(() => ab = (Tab[4, 3] < 0) && Support(CloneATable(Tab), RowS, ColS, 4, 3, a, Order));
                            th9.Wait();
                            th9.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th10 = Task.Factory.StartNew(() => ab = (Tab[3, 3] < 0) && Support(CloneATable(Tab), RowS, ColS, 3, 3, a, Order));
                            th10.Wait();
                            th10.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th11 = Task.Factory.StartNew(() => ab = (Tab[4, 4] < 0) && Support(CloneATable(Tab), RowS, ColS, 4, 4, a, Order));
                            th11.Wait();
                            th11.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }
                        }
                        if (!Is)
                        {
                            int[,] Ta = CloneATable(Tab);
                            Ta[RowD, ColD] = Tab[RowS, ColS];
                            Tab[RowS, ColS] = 0;

                            Task<bool> th12 = Task.Factory.StartNew(() => ab = (Tab[3, 4] == 0) && Movable(CloneATable(Tab), RowD, ColD, 3, 4, a, Order));
                            th12.Wait();
                            th12.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th13 = Task.Factory.StartNew(() => ab = (Tab[4, 3] == 0) && Movable(CloneATable(Tab), RowD, ColD, 4, 3, a, Order));
                            th13.Wait();
                            th13.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th14 = Task.Factory.StartNew(() => ab = (Tab[3, 3] == 0) && Movable(CloneATable(Tab), RowD, ColD, 3, 3, a, Order));
                            th14.Wait();
                            th14.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            Task<bool> th15 = Task.Factory.StartNew(() => ab = (Tab[4, 4] == 0) && Movable(CloneATable(Tab), RowD, ColD, 4, 4, a, Order));
                            th15.Wait();
                            th15.Dispose();
                            if (ab)
                            {
                                Is = true;
                            }

                            if (!Is)
                            {
                                if (Order == 1)
                                {
                                    Task<bool> th4 = Task.Factory.StartNew(() => ab = (Tab[3, 4] < 0) && Attack(CloneATable(Tab), RowD, ColD, 3, 4, aa, Order * -1));
                                    th4.Wait();
                                    th4.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th5 = Task.Factory.StartNew(() => ab = (Tab[4, 3] < 0) && Attack(CloneATable(Tab), RowD, ColD, 4, 3, aa, Order * -1));
                                    th5.Wait();
                                    th5.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th6 = Task.Factory.StartNew(() => ab = (Tab[3, 3] < 0) && Attack(CloneATable(Tab), RowD, ColD, 3, 3, aa, Order * -1));
                                    th6.Wait();
                                    th6.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th7 = Task.Factory.StartNew(() => ab = (Tab[4, 4] < 0) && Attack(CloneATable(Tab), RowD, ColD, 4, 4, aa, Order * -1));
                                    th7.Wait();
                                    th7.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th8 = Task.Factory.StartNew(() => ab = (Tab[3, 4] > 0) && Support(CloneATable(Tab), RowD, ColD, 3, 4, a, Order));
                                    th8.Wait();
                                    th8.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th9 = Task.Factory.StartNew(() => ab = (Tab[4, 3] > 0) && Support(CloneATable(Tab), RowD, ColD, 4, 3, a, Order));
                                    th9.Wait();
                                    th9.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th10 = Task.Factory.StartNew(() => ab = (Tab[3, 3] > 0) && Support(CloneATable(Tab), RowD, ColD, 3, 3, a, Order));
                                    th10.Wait();
                                    th10.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th11 = Task.Factory.StartNew(() => ab = (Tab[4, 4] > 0) && Support(CloneATable(Tab), RowD, ColD, 4, 4, a, Order));
                                    th11.Wait();
                                    th11.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }
                                }
                                else
                                {
                                    Task<bool> th4 = Task.Factory.StartNew(() => ab = (Tab[3, 4] > 0) && Attack(CloneATable(Tab), RowD, ColD, 3, 4, aa, Order * -1));
                                    th4.Wait();
                                    th4.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th5 = Task.Factory.StartNew(() => ab = (Tab[4, 3] > 0) && Attack(CloneATable(Tab), RowD, ColD, 4, 3, aa, Order * -1));
                                    th5.Wait();
                                    th5.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th6 = Task.Factory.StartNew(() => ab = (Tab[3, 3] > 0) && Attack(CloneATable(Tab), RowD, ColD, 3, 3, aa, Order * -1));
                                    th6.Wait();
                                    th6.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th7 = Task.Factory.StartNew(() => ab = (Tab[4, 4] > 0) && Attack(CloneATable(Tab), RowD, ColD, 4, 4, aa, Order * -1));
                                    th7.Wait();
                                    th7.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th8 = Task.Factory.StartNew(() => ab = (Tab[3, 4] < 0) && Support(CloneATable(Tab), RowD, ColD, 3, 4, a, Order));
                                    th8.Wait();
                                    th8.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th9 = Task.Factory.StartNew(() => ab = (Tab[4, 3] < 0) && Support(CloneATable(Tab), RowD, ColD, 4, 3, a, Order));
                                    th9.Wait();
                                    th9.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th10 = Task.Factory.StartNew(() => ab = (Tab[3, 3] < 0) && Support(CloneATable(Tab), RowD, ColD, 3, 3, a, Order));
                                    th10.Wait();
                                    th10.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }

                                    Task<bool> th11 = Task.Factory.StartNew(() => ab = (Tab[4, 4] < 0) && Support(CloneATable(Tab), RowD, ColD, 4, 4, a, Order));
                                    th11.Wait();
                                    th11.Dispose();
                                    if (ab)
                                    {
                                        Is = true;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Tab[RowD, ColD] == 1 || Tab[RowD, ColD] == -1)
                        {
                            return Is;
                        }

                        Task<bool> th12 = Task.Factory.StartNew(() => ab = (Tab[3, 4] == 0) && Movable(CloneATable(Tab), RowD, ColD, 3, 4, a, Order));
                        th12.Wait();
                        th12.Dispose();
                        if (ab)
                        {
                            Is = true;
                        }

                        Task<bool> th13 = Task.Factory.StartNew(() => ab = (Tab[4, 3] == 0) && Movable(CloneATable(Tab), RowD, ColD, 4, 3, a, Order));
                        th13.Wait();
                        th13.Dispose();
                        if (ab)
                        {
                            Is = true;
                        }

                        Task<bool> th14 = Task.Factory.StartNew(() => ab = (Tab[3, 3] == 0) && Movable(CloneATable(Tab), RowD, ColD, 3, 3, a, Order));
                        th14.Wait();
                        th14.Dispose();
                        if (ab)
                        {
                            Is = true;
                        }

                        Task<bool> th15 = Task.Factory.StartNew(() => ab = (Tab[4, 4] == 0) && Movable(CloneATable(Tab), RowD, ColD, 4, 4, a, Order));
                        th15.Wait();
                        th15.Dispose();
                        if (ab)
                        {
                            Is = true;
                        }

                        if (!Is)
                        {
                            if (Order == 1)
                            {
                                Task<bool> th4 = Task.Factory.StartNew(() => ab = (Tab[3, 4] < 0) && Attack(CloneATable(Tab), RowD, ColD, 3, 4, aa, Order * -1));
                                th4.Wait();
                                th4.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th5 = Task.Factory.StartNew(() => ab = (Tab[4, 3] < 0) && Attack(CloneATable(Tab), RowD, ColD, 4, 3, aa, Order * -1));
                                th5.Wait();
                                th5.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th6 = Task.Factory.StartNew(() => ab = (Tab[3, 3] < 0) && Attack(CloneATable(Tab), RowD, ColD, 3, 3, aa, Order * -1));
                                th6.Wait();
                                th6.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th7 = Task.Factory.StartNew(() => ab = (Tab[4, 4] < 0) && Attack(CloneATable(Tab), RowD, ColD, 4, 4, aa, Order * -1));
                                th7.Wait();
                                th7.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th8 = Task.Factory.StartNew(() => ab = (Tab[3, 4] > 0) && Support(CloneATable(Tab), RowD, ColD, 3, 4, a, Order));
                                th8.Wait();
                                th8.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th9 = Task.Factory.StartNew(() => ab = (Tab[4, 3] > 0) && Support(CloneATable(Tab), RowD, ColD, 4, 3, a, Order));
                                th9.Wait();
                                th9.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th10 = Task.Factory.StartNew(() => ab = (Tab[3, 3] > 0) && Support(CloneATable(Tab), RowD, ColD, 3, 3, a, Order));
                                th10.Wait();
                                th10.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th11 = Task.Factory.StartNew(() => ab = (Tab[4, 4] > 0) && Support(CloneATable(Tab), RowD, ColD, 4, 4, a, Order));
                                th11.Wait();
                                th11.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }
                            }
                            else
                            {
                                Task<bool> th4 = Task.Factory.StartNew(() => ab = (Tab[3, 4] > 0) && Attack(CloneATable(Tab), RowD, ColD, 3, 4, aa, Order * -1));
                                th4.Wait();
                                th4.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th5 = Task.Factory.StartNew(() => ab = (Tab[4, 3] > 0) && Attack(CloneATable(Tab), RowD, ColD, 4, 3, aa, Order * -1));
                                th5.Wait();
                                th5.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th6 = Task.Factory.StartNew(() => ab = (Tab[3, 3] > 0) && Attack(CloneATable(Tab), RowD, ColD, 3, 3, aa, Order * -1));
                                th6.Wait();
                                th6.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th7 = Task.Factory.StartNew(() => ab = (Tab[4, 4] > 0) && Attack(CloneATable(Tab), RowD, ColD, 4, 4, aa, Order * -1));
                                th7.Wait();
                                th7.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th8 = Task.Factory.StartNew(() => ab = (Tab[3, 4] < 0) && Support(CloneATable(Tab), RowD, ColD, 3, 4, a, Order));
                                th8.Wait();
                                th8.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th9 = Task.Factory.StartNew(() => ab = (Tab[4, 3] < 0) && Support(CloneATable(Tab), RowD, ColD, 4, 3, a, Order));
                                th9.Wait();
                                th9.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th10 = Task.Factory.StartNew(() => ab = (Tab[3, 3] < 0) && Support(CloneATable(Tab), RowD, ColD, 3, 3, a, Order));
                                th10.Wait();
                                th10.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }

                                Task<bool> th11 = Task.Factory.StartNew(() => ab = (Tab[4, 4] < 0) && Support(CloneATable(Tab), RowD, ColD, 4, 4, a, Order));
                                th11.Wait();
                                th11.Dispose();
                                if (ab)
                                {
                                    Is = true;
                                }
                            }
                        }
                    }
                }
                return Is;
            }
        }

        //when tow self castle control tow beside row or column
        private bool ExistCastleInDouble(int Order, int[,] Table, int RowS, int ColS, int RowD, int ColD)
        {
            object O = new object();
            lock (O)
            {
                bool Ex = false;
                int[,] Tab = CloneATable(Table);
                if (Order == 1)
                {
                    if (Tab[RowD, ColD] == 4)
                    {
                        if (ColD == 7)
                        {
                            for (int Row = 0; Row < 8; Row++)
                            {
                                if (Tab[Row, 6] == 4)
                                {
                                    Ex = true;
                                }
                            }
                        }
                        else
                        if (ColD == 6)
                        {
                            for (int Row = 0; Row < 8; Row++)
                            {
                                if (Tab[Row, 7] == 4)
                                {
                                    Ex = true;
                                }
                            }
                        }
                    }
                    if (!Ex)
                    {
                        if (Tab[RowS, ColS] == 4 && Tab[RowD, ColD] <= 0)
                        {
                            Tab[RowD, ColD] = Tab[RowS, ColS];
                            Tab[RowS, ColS] = 0;
                            if (Tab[RowD, ColD] == 4)
                            {
                                if (ColD == 7)
                                {
                                    for (int Row = 0; Row < 8; Row++)
                                    {
                                        if (Tab[Row, 6] == 4)
                                        {
                                            Ex = true;
                                        }
                                    }
                                }
                                else
                                if (ColD == 6)
                                {
                                    for (int Row = 0; Row < 8; Row++)
                                    {
                                        if (Tab[Row, 7] == 4)
                                        {
                                            Ex = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (Tab[RowD, ColD] == -4)
                    {
                        if (ColD == 0)
                        {
                            for (int Row = 0; Row < 8; Row++)
                            {
                                if (Tab[Row, 1] == -4)
                                {
                                    Ex = true;
                                }
                            }
                        }
                        else
                        if (ColD == 1)
                        {
                            for (int Row = 0; Row < 8; Row++)
                            {
                                if (Tab[Row, 0] == -4)
                                {
                                    Ex = true;
                                }
                            }
                        }
                    }
                    if (!Ex)
                    {
                        if (Tab[RowS, ColS] == -4 && Tab[RowD, ColD] <= 0)
                        {
                            Tab[RowD, ColD] = Tab[RowS, ColS];
                            Tab[RowS, ColS] = 0;
                            if (Tab[RowD, ColD] == -4)
                            {
                                if (ColD == 0)
                                {
                                    for (int Row = 0; Row < 8; Row++)
                                    {
                                        if (Tab[Row, 1] == -4)
                                        {
                                            Ex = true;
                                        }
                                    }
                                }
                                else
                                if (ColD == 1)
                                {
                                    for (int Row = 0; Row < 8; Row++)
                                    {
                                        if (Tab[Row, 0] == -4)
                                        {
                                            Ex = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return Ex;
            }
        }

        //Distribution of Objects
        public double HeuristicDistribution(bool Before, int[,] Tab, int Order, Color a, int RowS, int ColS, int RowD, int ColD)
        {
            object O = new object();
            lock (O)
            {
                double Dis = 1;
                const int ObjectGray = 0, ObjectBrown = 0;
                //opperation decision making  on pawn movment
                bool ab = false;
                Task<bool> th1 = Task.Factory.StartNew(() => ab = IsTableRowColIsZero(RowS, ColS) && HeuristicAllReducedAttacked.Count == 0);
                th1.Wait();
                th1.Dispose();
                if (ab)
                {
                    Dis = RationalRegard;
                }
                else
                {
                    Dis = RationalPenalty;
                }

                Task<bool> H1 = Task.Factory.StartNew(() => IKIsCentralPawnIsOk = IsCentralPawnIsOk(CloneATable(Tab), Order));
                H1.Wait();
                H1.Dispose();

                if (IKIsCentralPawnIsOk && HeuristicAllReducedAttacked.Count == 0)
                {
                    Dis = Rational(Dis, RationalRegard) * RationalRegard;
                }
                else
                {
                    Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                }

                Task<bool> th2 = Task.Factory.StartNew(() => ab = ExistCastleInDouble(Order, CloneATable(Tab), RowS, ColS, RowD, ColD));
                th2.Wait();
                th2.Dispose();
                if (ab)
                {
                    Dis = Rational(Dis, RationalRegard) * RationalRegard;
                }

                if (Order == 1)
                {
                    //castle in col 7 8
                    if (ColD == 6 || ColD == 7)
                    {
                        if (Tab[RowS, ColS] == 4 || Tab[RowD, ColD] == 4)
                        {
                            Dis = Rational(Dis, RationalRegard) * RationalRegard;
                        }
                    }
                    if ((Tab[3, 4] > ObjectGray && Tab[4, 3] > ObjectGray && Tab[3, 3] > ObjectGray && Tab[4, 4] > ObjectGray) || (IsNumberOfObjecttIsLessThanThreashold(CloneATable(Tab), 25)))
                    {
                        Task<bool> th3 = Task.Factory.StartNew(() => ab = (Tab[RowS, ColS] == 3) && (NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) > 0));
                        th3.Wait();
                        th3.Dispose();
                        if (ab)
                        {
                            Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                        }
                        else
                        {
                            Task<bool> th4 = Task.Factory.StartNew(() => ab = (Tab[RowD, ColD] == 3) && (NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) > 0));
                            th4.Wait();
                            th4.Dispose();
                            if (ab)
                            {
                                Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                            }
                            else

                            {
                                Task<bool> th5 = Task.Factory.StartNew(() => ab = (Tab[RowS, ColS] == 3) && (NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) == 0));
                                th5.Wait();
                                th5.Dispose();
                                if (ab)
                                {
                                    Dis = Rational(Dis, RationalRegard) * RationalRegard;
                                }
                                else
                                {
                                    Task<bool> th6 = Task.Factory.StartNew(() => ab = (Tab[RowD, ColD] == 3) && (NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) == 0));
                                    th6.Wait();
                                    th6.Dispose();
                                    if (ab)
                                    {
                                        Dis = Rational(Dis, RationalRegard) * RationalRegard;
                                    }
                                }
                            }
                        }
                    }
                    if (IsNumberOfObjecttIsLessThanThreashold(CloneATable(Tab), 32))
                    {
                        int Cor = 0;
                        Task<int> H2 = Task.Factory.StartNew(() => Cor = RefrigtzDLL.Colleralation.GetCorrelationScore(TableInitiation, CloneATable(Tab), 8, Order));
                        H2.Wait();
                        H2.Dispose();

                        if (Cor < Colleralation)
                        {
                            Colleralation = Cor;
                            Dis = Rational(Dis, RationalRegard) * RationalRegard;
                        }
                        if (Cor > ColleralationGray && Tab[RowS, ColS] > 0 && (Cor >= 0))
                        {
                            ColleralationGray = Cor;
                        }
                    }

                    Task<bool> th7 = Task.Factory.StartNew(() => ab = (Tab[RowS, ColS] > 0) && (NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) > 0));
                    th7.Wait();
                    th7.Dispose();
                    if (ab)
                    {
                        Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                    }
                    else
                    {
                        Task<bool> th8 = Task.Factory.StartNew(() => ab = (Tab[RowD, ColD] > 0) && (NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) > 0));
                        th8.Wait();
                        th8.Dispose();
                        if (ab)
                        {
                            Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                        }
                        else
                        {
                            Task<bool> th9 = Task.Factory.StartNew(() => ab = (Tab[RowS, ColS] > 0) && (NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) == 0));
                            th9.Wait();
                            th9.Dispose();
                            if (ab)
                            {
                                Dis = Rational(Dis, RationalRegard) * RationalRegard;
                            }
                            else
                            {
                                Task<bool> th10 = Task.Factory.StartNew(() => ab = (Tab[RowD, ColD] > 0) && (NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) == 0));
                                th10.Wait();
                                th10.Dispose();
                                if (ab)
                                {
                                    Dis = Rational(Dis, RationalRegard) * RationalRegard;
                                }
                            }
                        }
                    }

                    Task<bool> th11 = Task.Factory.StartNew(() => ab = ((Tab[3, 4] > ObjectGray && Tab[4, 3] > ObjectGray && Tab[3, 3] > ObjectGray && Tab[4, 4] > ObjectGray)) && (!IsNumberOfObjecttIsLessThanThreashold(CloneATable(Tab), 25)));
                    th11.Wait();
                    th11.Dispose();
                    if (!ab)
                    {
                        Task<bool> th12 = Task.Factory.StartNew(() => ab = IsNumberOfObjecttIsLessThanThreashold(CloneATable(Tab), 32));
                        th12.Wait();
                        th12.Dispose();
                        if (!ab)
                        {
                            int Cor = 0;
                            Task<int> H3 = Task.Factory.StartNew(() => Cor = RefrigtzDLL.Colleralation.GetCorrelationScore(TableInitiation, CloneATable(Tab), 8, Order));
                            H3.Wait();
                            H3.Dispose();

                            if (Cor < DeColleralation)
                            {
                                DeColleralation = Cor;
                                Dis = Rational(Dis, RationalRegard) * RationalRegard;
                            }
                        }
                    }
                }
                else
                {
                    //castle in col 7 8
                    if (ColD == 1 || ColD == 0)
                    {
                        if (Tab[RowS, ColS] == -4 || Tab[RowD, ColD] == -4)
                        {
                            Dis = Rational(Dis, RationalRegard) * RationalRegard;
                        }
                    }
                    if ((Tab[3, 4] < ObjectBrown && Tab[4, 3] < ObjectBrown && Tab[3, 3] < ObjectBrown && Tab[4, 4] < ObjectBrown) || (IsNumberOfObjecttIsLessThanThreashold(CloneATable(Tab), 25)))
                    {
                        Task<bool> th13 = Task.Factory.StartNew(() => ab = (Tab[RowS, ColS] == -3) && (NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) > 0));
                        th13.Wait();
                        th13.Dispose();
                        if (ab)
                        {
                            Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                        }
                        else
                        {
                            Task<bool> th14 = Task.Factory.StartNew(() => ab = (Tab[RowD, ColD] == -3) && (NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) > 0));
                            th14.Wait();
                            th14.Dispose();
                            if (ab)
                            {
                                Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                            }
                            else
                            {
                                Task<bool> th15 = Task.Factory.StartNew(() => ab = (Tab[RowS, ColS] == -3) && (NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) == 0));
                                th15.Wait();
                                th15.Dispose();
                                if (ab)
                                {
                                    Dis = Rational(Dis, RationalRegard) * RationalRegard;
                                }
                                else
                                {
                                    Task<bool> th16 = Task.Factory.StartNew(() => ab = (Tab[RowD, ColD] == -3) && (NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) == 0));
                                    th16.Wait();
                                    th16.Dispose();
                                    if (ab)
                                    {
                                        Dis = Rational(Dis, RationalRegard) * RationalRegard;
                                    }
                                }
                            }
                        }
                    }
                    Task<bool> th17 = Task.Factory.StartNew(() => ab = IsNumberOfObjecttIsLessThanThreashold(CloneATable(Tab), 32));
                    th17.Wait();
                    th17.Dispose();
                    if (ab)
                    {
                        int Cor = 0;
                        Task<int> H4 = Task.Factory.StartNew(() => Cor = RefrigtzDLL.Colleralation.GetCorrelationScore(TableInitiation, CloneATable(Tab), 8, Order));
                        H4.Wait();
                        H4.Dispose();
                        if (Cor < Colleralation)
                        {
                            Colleralation = Cor;
                            Dis = Rational(Dis, RationalRegard) * RationalRegard;
                        }
                        if (Cor > ColleralationBrown && Tab[RowS, ColS] < 0 && (Cor >= 0))
                        {
                            ColleralationBrown = Cor;
                        }
                    }
                    Task<bool> th18 = Task.Factory.StartNew(() => ab = (Tab[RowS, ColS] < 0) && (NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) > 0));
                    th18.Wait();
                    th18.Dispose();
                    if (ab)
                    {
                        Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                    }
                    else
                    {
                        Task<bool> th19 = Task.Factory.StartNew(() => ab = (Tab[RowD, ColD] < 0) && (NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) > 0));
                        th19.Wait();
                        th19.Dispose();
                        if (ab)
                        {
                            Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                        }
                        else
                        {
                            Task<bool> th20 = Task.Factory.StartNew(() => ab = (Tab[RowS, ColS] < 0) && (NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) == 0));
                            th20.Wait();
                            th20.Dispose();
                            if (ab)
                            {
                                Dis = Rational(Dis, RationalRegard) * RationalRegard;
                            }
                            else
                            {
                                Task<bool> th21 = Task.Factory.StartNew(() => ab = (Tab[RowD, ColD] < 0) && (NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) == 0));
                                th21.Wait();
                                th21.Dispose();
                                if (ab)
                                {
                                    Dis = Rational(Dis, RationalRegard) * RationalRegard;
                                }
                            }
                        }
                    }
                    if (!((Tab[3, 4] < ObjectBrown && Tab[4, 3] < ObjectBrown && Tab[3, 3] < ObjectBrown && Tab[4, 4] < ObjectBrown)) && (!IsNumberOfObjecttIsLessThanThreashold(CloneATable(Tab), 25)))
                    {
                        if (!IsNumberOfObjecttIsLessThanThreashold(CloneATable(Tab), 32))
                        {
                            int Cor = 0;
                            Task<int> H5 = Task.Factory.StartNew(() => Cor = RefrigtzDLL.Colleralation.GetCorrelationScore(TableInitiation, CloneATable(Tab), 8, Order));
                            H5.Wait();
                            H5.Dispose();

                            if (Cor < DeColleralation)
                            {
                                DeColleralation = Cor;
                                Dis = Rational(Dis, RationalRegard) * RationalRegard;
                            }
                        }
                    }
                }
                if (CenrtrallnControlByTraversal(CloneATable(Tab), a, Order, RowS, ColS, RowD, ColD))
                {
                    Dis = Rational(Dis, RationalRegard) * RationalRegard;
                }
                else
                {
                    Dis = Rational(Dis, RationalPenalty) * RationalPenalty;
                }

                return Dis;
            }
        }

        //when pawn is doubled or isolated at Move before return true and rationalpenalty occured
        private bool IsPawnIsolatedOrDoubleBackAwayOrHung(int RowS, int ColS, int RowD, int ColD, int[,] Table, int Order)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                if (Order == 1)
                {
                    if (ColS < 5)
                    {
                        if (!Is)
                        {
                            bool A = true;
                            bool B = true;
                            if (RowD >= 1 && ColD >= 1)
                            {
                                A = (Table[RowD - 1, ColD - 1] == 1);
                            }

                            if (RowD + 1 < 8 && ColD >= 1)
                            {
                                B = (Table[RowD + 1, ColD - 1] == 1);
                            }

                            if (!(A || B))
                            {
                                Is = true;
                            }
                        }
                    }
                }
                else
                {
                    if (ColS > 2)
                    {
                        if (!Is)
                        {
                            bool A = true;
                            bool B = true;
                            if (RowS >= 1 && ColS + 1 < 8)
                            {
                                A = (Table[RowS - 1, ColS + 1] == -1);
                            }

                            if (RowS + 1 < 8 && ColS + 1 > 8)
                            {
                                B = (Table[RowS + 1, ColS + 1] == -1);
                            }

                            if (!(A || B))
                            {
                                Is = true;
                            }
                        }
                    }
                }
                if (!Is)
                {
                    if (Order == 1)
                    {
                        if (ColS + 1 < 8)
                        {
                            if ((Table[RowS, ColS + 1] == 1 && Table[RowS, ColS] == 1))
                            {
                                Is = false;
                            }
                        }
                        else
                        if (ColS - 1 >= 0)
                        {
                            if ((Table[RowS, ColS - 1] == 1 && Table[RowS, ColS] == 1))
                            {
                                Is = false;
                            }
                        }
                    }
                    else
                    {
                        if (ColS + 1 < 8)
                        {
                            if ((Table[RowS, ColS + 1] == -1 && Table[RowS, ColS] == -1))
                            {
                                Is = false;
                            }
                        }
                        else
                      if (ColS - 1 >= 0)
                        {
                            if ((Table[RowS, ColS - 1] == -1 && Table[RowS, ColS] == -1))
                            {
                                Is = false;
                            }
                        }
                    }
                }
                if (!Is)
                {
                    bool IsSuported = false;
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (Order == 1 && Table[i, j] <= 0)
                            {
                                continue;
                            }

                            if (Order == -1 && Table[i, j] >= 0)
                            {
                                continue;
                            }

                            if (Math.Abs(Table[RowS, ColS]) == 1 && SameSign(Table[RowS, ColS], Table[i, j]))
                            {
                                bool ab = false;
                                Task<bool> th = Task.Factory.StartNew(() => ab = Support(CloneATable(Table), i, j, RowS, ColS, color, Order));
                                th.Wait();
                                th.Dispose();
                                if (ab)
                                {
                                    IsSuported = true;
                                    break;
                                }
                            }
                            else
                            if (Math.Abs(Table[RowD, ColD]) == 1 && SameSign(Table[RowD, ColD], Table[i, j]))
                            {
                                bool ab = false;
                                Task<bool> th = Task.Factory.StartNew(() => ab = Support(CloneATable(Table), i, j, RowD, ColD, color, Order));
                                th.Wait();
                                th.Dispose();
                                if (ab)
                                {
                                    IsSuported = true;
                                    break;
                                }
                            }
                        }
                        if (IsSuported)
                        {
                            break;
                        }
                    }
                    Is = (!IsSuported);
                }
                return Is;
            }
        }

        //when pawn Move to center by no reducedattack rational regard and heuristic of attacked and "IsPawnIsolatedOrDoubleBackAwayOrHung" rational penalty
        public double HeuristicObjectAtCenterAndPawnAttackTraversalObjectsAndDangourForEnemy(ref double HA, int[,] Table, Color aa, int Ord, int ii, int jj, int i, int j)
        {
            object O = new object();
            lock (O)
            {
                ////double HA =1;
                object O1 = new object();
                lock (O1)
                {
                    if ((i == 3 || i == 4) && (j == 3 || j == 4) && HeuristicAllReducedAttacked.Count == 0)
                    {
                        HA += RationalRegard;
                    }
                    else
                    if ((i == 3 || i == 4) && (j == 3 || j == 4) && HeuristicAllReducedAttacked.Count != 0)
                    {
                        HA += RationalPenalty;
                    }

                    if (HA == 1)
                    {
                        int[,] Ta = CloneATable(Table);
                        if (Order == 1)
                        {
                            if (Ta[ii, jj] != 0)
                            {
                                Ta[i, j] = Ta[ii, jj];
                                Ta[ii, jj] = 0;
                            }
                            if (Ta[i, j] == 1)
                            {
                                HA += HeuristicAttack(ref HA, CloneATable(Ta), Ord, aa, ii, jj, i, j);
                            }
                        }
                        else
                        {
                            if (Ta[ii, jj] != 0)
                            {
                                Ta[i, j] = Ta[ii, jj];
                                Ta[ii, jj] = 0;
                            }
                            if (Ta[i, j] == -1)
                            {
                                HA += HeuristicAttack(ref HA, CloneATable(Ta), Ord, aa, ii, jj, i, j);
                            }
                        }
                    }
                    if (IsPawnIsolatedOrDoubleBackAwayOrHung(ii, jj, i, j, CloneATable(Table), Order))
                    {
                        HA += Rational(HA, RationalPenalty) * RationalPenalty;
                    }
                }
                return HA;
            }
        }

        //color by order specified
        private Color OrderColor(int Ord)
        {
            object O = new object();
            lock (O)
            {
                Color a = Color.Gray;
                if (Ord == -1)
                {
                    a = Color.Brown;
                }

                return a;
            }
        }

        //permit mehod suit for heuristicexchange
        private bool Permit(int Order, int TabS, int TabD, bool Self = true, bool Move = false)
        {
            object O = new object();
            lock (O)
            {
                bool Per = false;
                if (Self)
                {
                    if (Move)
                    {
                        if (Order == 1 && TabS > 0 && TabD == 0)
                        {
                            Per = true;
                        }

                        if (Order == -1 && TabS < 0 && TabD == 0)
                        {
                            Per = true;
                        }
                    }
                    else
                    {
                        if (Order == 1 && TabS > 0 && TabD > 0)
                        {
                            Per = true;
                        }

                        if (Order == -1 && TabS < 0 && TabD < 0)
                        {
                            Per = true;
                        }
                    }
                }
                else
                {
                    if (Move)
                    {
                        if (Order == 1 && TabS > 0 && TabD <= 0)
                        {
                            Per = true;
                        }

                        if (Order == -1 && TabS < 0 && TabD >= 0)
                        {
                            Per = true;
                        }
                    }
                    else
                    {
                        if (Order == 1 && TabS > 0 && TabD < 0)
                        {
                            Per = true;
                        }

                        if (Order == -1 && TabS < 0 && TabD > 0)
                        {
                            Per = true;
                        }
                    }
                }
                return Per;
            }
        }

        //all heuristics  of attacked and reduced attacked ans supported and reduced attacked
        public double[] HeuristicAll(int Killed, int[,] Table, int Ord)
        {
            object O = new object();
            lock (O)
            {
                double[] HeuristicA = new double[6];
                double[] HeuristicB = new double[6];
                double HA = 1;
                int DumOrder = Order;
                int DummyOrder = Order;
                int DummyCurrentOrder = ChessRules.CurrentOrder;
                ///When AStarGreedy Heuristic is Not Assigned.
                object O1 = new object();
                lock (O1)
                {
                    Task output = Task.Factory.StartNew(() =>
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, 8, RowS =>
                        {
                            ParallelOptions poo = new ParallelOptions
                            {
                                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                            }; Parallel.For(0, 8, ColS =>
                            {
                                ParallelOptions pooo = new ParallelOptions
                                {
                                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                                }; Parallel.For(0, 8, RowD =>
                                {
                                    ParallelOptions poooo = new ParallelOptions
                                    {
                                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                                    }; Parallel.For(0, 8, ColD =>
                                    {
                                        if (IsDistributedObjectAttackNonDistributedEnemyObject(CloneATable(Table), Ord, RowS, ColS, RowD, ColD))
                                        {
                                            HA += Rational(HA, RationalPenalty) * RationalPenalty;
                                            return;
                                        }

                                        ParallelOptions pooooo = new ParallelOptions
                                        {
                                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                                        }; Parallel.Invoke(() =>
                                        {
                                            object OO = new object();
                                            lock (OO)
                                            {
                                                if (HeuristicA[0] == 0)
                                                {
                                                    bool ab = false;
                                                    Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Order * -1, Table[RowD, ColD], Table[RowS, ColS], false, false));
                                                    th.Wait();
                                                    th.Dispose();
                                                    if (ab)
                                                    {
                                                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), RowD, ColD, RowS, ColS, OrderColor(Ord * -1), Ord * -1));
                                                        th1.Wait();
                                                        th1.Dispose();
                                                        if (ab)
                                                        {
                                                            if (HeuristicA[0] == 0)
                                                            {
                                                                HeuristicA[0] = RationalPenalty;
                                                            }

                                                            HeuristicB[0] += RationalPenalty;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        , () =>
                                        {
                                            if (HeuristicA[2] == 0)
                                            {
                                                bool ab = false;
                                                Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Order * -1, Table[RowD, ColD], Table[RowS, ColS], true, false));
                                                th.Wait();
                                                th.Dispose();
                                                if (ab)
                                                {
                                                    Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(Table), RowD, ColD, RowS, ColS, OrderColor(Ord * -1), Ord * -1));
                                                    th1.Wait();
                                                    th1.Dispose();
                                                    if (ab)
                                                    {
                                                        if (HeuristicA[2] == 0)
                                                        {
                                                            HeuristicA[2] = RationalPenalty;
                                                        }

                                                        HeuristicB[2] += RationalPenalty;
                                                    }
                                                }
                                            }
                                        }
                                        , () =>
                                        {
                                            if (HeuristicA[1] == 0)
                                            {
                                                bool ab = false;
                                                Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Order, Table[RowS, ColS], Table[RowD, ColD], false, false));
                                                th.Wait();
                                                th.Dispose();
                                                if (ab)
                                                {
                                                    Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), RowS, ColS, RowD, ColD, OrderColor(Ord), Ord));
                                                    th1.Wait();
                                                    th1.Dispose();
                                                    if (ab)
                                                    {
                                                        if (HeuristicA[1] == 0)
                                                        {
                                                            HeuristicA[1] = RationalRegard;
                                                        }

                                                        HeuristicB[1] += RationalRegard;
                                                    }
                                                }
                                            }
                                        }
                                         , () =>
                                         {
                                             if (HeuristicA[3] == 0)
                                             {
                                                 bool ab = false;
                                                 Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Order, Table[RowS, ColS], Table[RowD, ColD], true, false));
                                                 th.Wait();
                                                 th.Dispose();
                                                 if (ab)
                                                 {
                                                     Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(Table), RowS, ColS, RowD, ColD, OrderColor(Ord), Ord));
                                                     th1.Wait();
                                                     th1.Dispose();
                                                     if (ab)
                                                     {
                                                         if (HeuristicA[3] == 0)
                                                         {
                                                             HeuristicA[3] = RationalRegard;
                                                         }

                                                         HeuristicB[3] += RationalRegard;
                                                     }
                                                 }
                                             }
                                         });
                                    });
                                });
                            });
                        });
                    });

                    output.Wait(); output.Dispose();
                }
                return HeuristicA;
            }
        }

        //number of exists of Move situation in Heuristic lists
        private int NoOfExistInMoveList(bool Before, int Rows, int Cols, int Rowd, int Cold)
        {
            object O = new object();
            lock (O)
            {
                int Is = 0;
                if (Before)
                {
                    for (int i = 0; i < HeuristicAllMove.Count; i++)
                    {
                        if (HeuristicAllMove[i][0] == Rows && HeuristicAllMove[i][1] == Cols && HeuristicAllMove[i][2] == Rowd && HeuristicAllMove[i][3] == Cold)
                        {
                            Is++;
                        }
                    }
                }
                else
                {
                    if (HeuristicAllMoveMidel > 0 && HeuristicAllMoveMidel < HeuristicAllMove.Count)
                    {
                        for (int i = HeuristicAllMoveMidel; i < HeuristicAllMove.Count; i++)
                        {
                            if (HeuristicAllMove[i][0] == Rows && HeuristicAllMove[i][1] == Cols && HeuristicAllMove[i][2] == Rowd && HeuristicAllMove[i][3] == Cold)
                            {
                                Is++;
                            }
                        }
                    }
                }
                return Is;
            }
        }

        //number of exists of Move situation in Heuristic lists
        private int NoOfExistInReducedMoveList(bool Before, int Rows, int Cols, int Rowd, int Cold)
        {
            object O = new object();
            lock (O)
            {
                int Is = 0;
                if (Before)
                {
                    for (int i = 0; i < HeuristicAllReducedMove.Count; i++)
                    {
                        if (HeuristicAllReducedMove[i][2] == Rows && HeuristicAllReducedMove[i][3] == Cols && HeuristicAllReducedMove[i][0] == Rowd && HeuristicAllReducedMove[i][1] == Cold)
                        {
                            Is++;
                        }
                    }
                }
                else
                {
                    if (HeuristicAllReducedMoveMidel > 0 && HeuristicAllReducedMoveMidel < HeuristicAllReducedMove.Count)
                    {
                        for (int i = HeuristicAllReducedMoveMidel; i < HeuristicAllReducedMove.Count; i++)
                        {
                            if (HeuristicAllReducedMove[i][2] == Rows && HeuristicAllReducedMove[i][3] == Cols && HeuristicAllReducedMove[i][0] == Rowd && HeuristicAllReducedMove[i][1] == Cold)
                            {
                                Is++;
                            }
                        }
                    }
                }
                return Is;
            }
        }

        //number of exists of Move situation in Heuristic lists
        private int NoOfExistInAttackList(bool Before, int Rows, int Cols, int Rowd, int Cold)
        {
            object O = new object();
            lock (O)
            {
                int Is = 0;
                if (Before)
                {
                    for (int i = 0; i < HeuristicAllAttacked.Count; i++)
                    {
                        if (HeuristicAllAttacked[i][0] == Rows && HeuristicAllAttacked[i][1] == Cols && HeuristicAllAttacked[i][2] == Rowd && HeuristicAllAttacked[i][3] == Cold)
                        {
                            Is++;
                        }
                    }
                }
                else
                {
                    if (HeuristicAllAttackedMidel > 0 && HeuristicAllAttackedMidel < HeuristicAllAttacked.Count)
                    {
                        for (int i = HeuristicAllAttackedMidel; i < HeuristicAllAttacked.Count; i++)
                        {
                            if (HeuristicAllAttacked[i][0] == Rows && HeuristicAllAttacked[i][1] == Cols && HeuristicAllAttacked[i][2] == Rowd && HeuristicAllAttacked[i][3] == Cold)
                            {
                                Is++;
                            }
                        }
                    }
                }
                return Is;
            }
        }

        //number of exists of Move situation in Heuristic lists
        private int NoOfExistInReducedAttackList(bool Before, int Rows, int Cols, int Rowd, int Cold)
        {
            object O = new object();
            lock (O)
            {
                int Is = 0;
                if (Before)
                {
                    for (int i = 0; i < HeuristicAllReducedAttacked.Count; i++)
                    {
                        if (HeuristicAllReducedAttacked[i][2] == Rows && HeuristicAllReducedAttacked[i][3] == Cols && HeuristicAllReducedAttacked[i][0] == Rowd && HeuristicAllReducedAttacked[i][1] == Cold)
                        {
                            Is++;
                        }
                    }
                }
                else
                {
                    if (HeuristicAllReducedAttackedMidel > 0 && HeuristicAllReducedAttackedMidel < HeuristicAllReducedAttacked.Count)
                    {
                        for (int i = HeuristicAllReducedAttackedMidel; i < HeuristicAllReducedAttacked.Count; i++)
                        {
                            if (HeuristicAllReducedAttacked[i][2] == Rows && HeuristicAllReducedAttacked[i][3] == Cols && HeuristicAllReducedAttacked[i][0] == Rowd && HeuristicAllReducedAttacked[i][1] == Cold)
                            {
                                Is++;
                            }
                        }
                    }
                }
                return Is;
            }
        }

        private List<int[]> ListOfExistInReducedAttackList(bool Before, int Rows, int Cols, int Rowd, int Cold)
        {
            object O = new object();
            lock (O)
            {
                List<int[]> Is = new List<int[]>();
                if (Before)
                {
                    for (int i = 0; i < HeuristicAllReducedAttacked.Count; i++)
                    {
                        if (HeuristicAllReducedAttacked[i][2] == Rows && HeuristicAllReducedAttacked[i][3] == Cols && HeuristicAllReducedAttacked[i][0] == Rowd && HeuristicAllReducedAttacked[i][1] == Cold)
                        {
                            int[] I = new int[5];
                            I[0] = HeuristicAllReducedAttacked[i][0];
                            I[1] = HeuristicAllReducedAttacked[i][1];
                            I[2] = HeuristicAllReducedAttacked[i][2];
                            I[3] = HeuristicAllReducedAttacked[i][3];
                            I[4] = SignBeforNext(I[0], I[1], I[2], I[3]);
                            Is.Add(I);
                        }
                    }
                }
                else
                {
                    if (HeuristicAllReducedAttackedMidel > 0 && HeuristicAllReducedAttackedMidel < HeuristicAllReducedAttacked.Count)
                    {
                        for (int i = HeuristicAllReducedAttackedMidel; i < HeuristicAllReducedAttacked.Count; i++)
                        {
                            if (HeuristicAllReducedAttacked[i][2] == Rows && HeuristicAllReducedAttacked[i][3] == Cols && HeuristicAllReducedAttacked[i][0] == Rowd && HeuristicAllReducedAttacked[i][1] == Cold)
                            {
                                int[] I = new int[5];
                                I[0] = HeuristicAllReducedAttacked[i][0];
                                I[1] = HeuristicAllReducedAttacked[i][1];
                                I[2] = HeuristicAllReducedAttacked[i][2];
                                I[3] = HeuristicAllReducedAttacked[i][3];
                                I[4] = SignBeforNext(I[0], I[1], I[2], I[3]);
                                Is.Add(I);
                            }
                        }
                    }
                }
                return Is;
            }
        }

        private List<int[]> ListOfExistInAttackList(bool Before, int RowS, int ColS, int RowD, int ColD)
        {
            object O = new object();
            lock (O)
            {
                List<int[]> Is = new List<int[]>();
                if (Before)
                {
                    for (int i = 0; i < HeuristicAllAttacked.Count; i++)
                    {
                        if (HeuristicAllAttacked[i][2] == RowD && HeuristicAllAttacked[i][3] == ColD && HeuristicAllAttacked[i][0] == RowS && HeuristicAllAttacked[i][1] == ColS)
                        {
                            int[] I = new int[5];
                            I[0] = HeuristicAllAttacked[i][0];
                            I[1] = HeuristicAllAttacked[i][1];
                            I[2] = HeuristicAllAttacked[i][2];
                            I[3] = HeuristicAllAttacked[i][3];
                            I[4] = SignBeforNext(I[0], I[1], I[2], I[3]);
                            Is.Add(I);
                        }
                    }
                }
                else
                {
                    if (HeuristicAllAttackedMidel > 0 && HeuristicAllAttackedMidel < HeuristicAllAttacked.Count)
                    {
                        for (int i = HeuristicAllAttackedMidel; i < HeuristicAllAttacked.Count; i++)
                        {
                            if (HeuristicAllAttacked[i][2] == RowD && HeuristicAllAttacked[i][3] == ColD && HeuristicAllAttacked[i][0] == RowS && HeuristicAllAttacked[i][1] == ColS)
                            {
                                int[] I = new int[5];
                                I[0] = HeuristicAllAttacked[i][0];
                                I[1] = HeuristicAllAttacked[i][1];
                                I[2] = HeuristicAllAttacked[i][2];
                                I[3] = HeuristicAllAttacked[i][3];
                                I[4] = SignBeforNext(I[0], I[1], I[2], I[3]);
                                Is.Add(I);
                            }
                        }
                    }
                }
                return Is;
            }
        }

        //number of exists of Move situation in Heuristic lists
        private int NoOfExistInSupportList(bool Before, int Rows, int Cols, int Rowd, int Cold)
        {
            object O = new object();
            lock (O)
            {
                int Is = 0;
                if (Before)
                {
                    for (int i = 0; i < HeuristicAllSupport.Count; i++)
                    {
                        if (HeuristicAllSupport[i][0] == Rows && HeuristicAllSupport[i][1] == Cols && HeuristicAllSupport[i][2] == Rowd && HeuristicAllSupport[i][3] == Cold)
                        {
                            Is++;
                        }
                    }
                }
                else
                {
                    if (HeuristicAllSupportMidel > 0 && HeuristicAllSupportMidel < HeuristicAllSupport.Count)
                    {
                        for (int i = HeuristicAllSupportMidel; i < HeuristicAllSupport.Count; i++)
                        {
                            if (HeuristicAllSupport[i][0] == Rows && HeuristicAllSupport[i][1] == Cols && HeuristicAllSupport[i][2] == Rowd && HeuristicAllSupport[i][3] == Cold)
                            {
                                Is++;
                            }
                        }
                    }
                }
                return Is;
            }
        }

        //number of exists of Move situation in Heuristic lists
        private int NoOfExistInReducedSupportList(bool Before, int Rows, int Cols, int Rowd, int Cold)
        {
            object O = new object();
            lock (O)
            {
                int Is = 0;
                if (Before)
                {
                    for (int i = 0; i < HeuristicAllReducedSupport.Count; i++)
                    {
                        if (HeuristicAllReducedSupport[i][2] == Rows && HeuristicAllReducedSupport[i][3] == Cols && HeuristicAllReducedSupport[i][0] == Rowd && HeuristicAllReducedSupport[i][1] == Cold)
                        {
                            Is++;
                        }
                    }
                }
                else
                {
                    if (HeuristicAllReducedSupportMidel > 0 && HeuristicAllReducedSupportMidel < HeuristicAllReducedSupport.Count)
                    {
                        for (int i = HeuristicAllReducedSupportMidel; i < HeuristicAllReducedSupport.Count; i++)
                        {
                            if (HeuristicAllReducedSupport[i][2] == Rows && HeuristicAllReducedSupport[i][3] == Cols && HeuristicAllReducedSupport[i][0] == Rowd && HeuristicAllReducedSupport[i][1] == Cold)
                            {
                                Is++;
                            }
                        }
                    }
                }
                return Is;
            }
        }

        //promotion heuristic on pawn
        private int HeuristicPromotion(bool Before, int[,] Tab, int Order, int Ros, int Cos, int Rod, int Cod)
        {
            object O = new object();
            lock (O)
            {
                int HP = 0;
                if (Order == 1)
                {
                    if (Cod != 0)
                    {
                        return HP;
                    }

                    if (TableConst[Ros, Cos] == 1 && Tab[Rod, Cod] > 0)
                    {
                        HP = ((RationalRegard) * (NoOfExistInAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInSupportList(Before, Ros, Cos, Rod, Cod)) + ((RationalPenalty) * (NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInReducedSupportList(Before, Ros, Cos, Rod, Cod))));
                    }
                }
                else
                {
                    if (Cod != 7)
                    {
                        return HP;
                    }

                    if (TableConst[Ros, Cos] == -1 && Tab[Rod, Cod] < 0)
                    {
                        HP = ((RationalRegard) * (NoOfExistInAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInSupportList(Before, Ros, Cos, Rod, Cod)) + ((RationalPenalty) * (NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInReducedSupportList(Before, Ros, Cos, Rod, Cod))));
                    }
                }
                return HP;
            }
        }

        //heuristic on trying open elephant of self to Move
        private double HeuristicElephantOpen(bool Before, int[,] Tab, int Order, int Ros, int Cos, int Rod, int Cod)
        {
            object O = new object();
            lock (O)
            {
                double HE = 0;
                if (Order == 1)
                {
                    if (TableConst[Ros, Cos] == 2 && Tab[Rod, Cod] <= 0)
                    {
                        HE = ((RationalRegard) * (NoOfExistInAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInSupportList(Before, Ros, Cos, Rod, Cod)) + ((RationalPenalty) * (NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInReducedSupportList(Before, Ros, Cos, Rod, Cod))));
                        if (NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) == 0)
                        {
                            HE = Rational(HE, NoOfExistInMoveList(Before, Ros, Cos, Rod, Cod)) * NoOfExistInMoveList(Before, Ros, Cos, Rod, Cod);
                        }
                    }
                }
                else
                {
                    if (TableConst[Ros, Cos] == -2 && Tab[Rod, Cod] >= 0)
                    {
                        HE = ((RationalRegard) * (NoOfExistInAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInSupportList(Before, Ros, Cos, Rod, Cod)) + ((RationalPenalty) * (NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInReducedSupportList(Before, Ros, Cos, Rod, Cod))));
                        if (NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) == 0)
                        {
                            HE = Rational(HE, NoOfExistInMoveList(Before, Ros, Cos, Rod, Cod)) * NoOfExistInMoveList(Before, Ros, Cos, Rod, Cod);
                        }
                    }
                }
                return HE;
            }
        }

        //safety of self hourse by value.
        private int HeuristicHourseCloseBaseOfWeakHourseIsWhereIsHomeStrong(bool Before, int[,] Tab, int Order, int Ros, int Cos, int Rod, int Cod)
        {
            object O = new object();
            lock (O)
            {
                int HH = 0;
                if (Order == 1)
                {
                    if (TableConst[Ros, Cos] == 3 && Tab[Rod, Cod] <= 0)
                    {
                        //Base of weak hourse is where is Home strong.
                        HH = ((RationalRegard) * (NoOfExistInAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInSupportList(Before, Ros, Cos, Rod, Cod)) + ((RationalPenalty) * (128 - NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInReducedSupportList(Before, Ros, Cos, Rod, Cod))));
                        //Hourse close
                        if (NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) == 0)
                        {
                            HH *= (64 - NoOfExistInMoveList(Before, Ros, Cos, Rod, Cod));
                        }
                    }
                }
                else
                {
                    if (TableConst[Ros, Cos] == -3 && Tab[Rod, Cod] >= 0)
                    {
                        //Base of weak hourse is where is Home strong.
                        HH = ((RationalRegard) * (NoOfExistInAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInSupportList(Before, Ros, Cos, Rod, Cod)) + ((RationalPenalty) * (128 - NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInReducedSupportList(Before, Ros, Cos, Rod, Cod))));
                        //Hourse close
                        if (NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) == 0)
                        {
                            HH *= (64 - NoOfExistInMoveList(Before, Ros, Cos, Rod, Cod));
                        }
                    }
                }
                return HH;
            }
        }

        private bool AssignAAndList(int ros, int cos, int rod, int cod, ref List<int[]> T)
        {
            object o = new object();
            lock (o)
            {
                try
                {
                    int[] A = new int[5];
                    A[0] = ros;
                    A[1] = cos;
                    A[2] = rod;
                    A[3] = cod;
                    A[4] = SignBeforNext(ros, cos, rod, cod);
                    T.Add(A);
                }
                catch (Exception g)
                {
                    Log(g);
                    return false;
                }
            }
            return true;
        }

        private bool HeuristicExchangHeuristicAllReducedAttacked(int Ord, int RowS, int ColS, int RowD, int ColD, int[,] Table)
        {
            object OO = new object();
            lock (OO)
            {
                try
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Ord * -1, Table[RowD, ColD], Table[RowS, ColS], false, false));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), RowD, ColD, RowS, ColS, OrderColor(Ord * -1), Ord * -1));
                        th1.Wait();
                        th1.Dispose();
                        if (ab)
                        {
                            object OOO = new object();
                            lock (OOO)
                            {
                                AssignAAndList(RowD, ColD, RowS, ColS, ref HeuristicAllReducedAttacked);
                                return true;
                            }
                        }
                    }
                }
                catch (Exception t)
                {
                    Log(t);
                    return false;
                }
            }
            return false;
        }

        private bool HeuristicExchangeHeuristicAllReducedSupport(int Ord, int RowS, int ColS, int RowD, int ColD, int[,] Table)
        {
            object OO = new object();
            lock (OO)
            {
                try
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Ord * -1, Table[RowD, ColD], Table[RowS, ColS], true, false));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(Table), RowD, ColD, RowS, ColS, OrderColor(Ord * -1), Ord * -1));
                        th1.Wait();
                        th1.Dispose();
                        if (ab)
                        {
                            object OOO = new object();
                            lock (OOO)
                            {
                                AssignAAndList(RowD, ColD, RowS, ColS, ref HeuristicAllReducedSupport);
                                return true;
                            }
                        }
                    }
                }
                catch (Exception t)
                {
                    Log(t);
                    return false;
                }
            }
            return false;
        }

        private bool HeuristicExchangeHeuristicAllReducedMove(int Ord, int RowS, int ColS, int RowD, int ColD, int[,] Table)
        {
            object OO = new object();
            lock (OO)
            {
                try
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Ord * -1, Table[RowD, ColD], Table[RowS, ColS], true, true));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Movable(CloneATable(Table), RowD, ColD, RowS, ColS, OrderColor(Ord * -1), Ord * -1));
                        th1.Wait();
                        th1.Dispose();
                        if (ab)
                        {
                            object OOO = new object();
                            lock (OOO)
                            {
                                AssignAAndList(RowD, ColD, RowS, ColS, ref HeuristicAllReducedMove);
                                return true;
                            }
                        }
                    }
                }
                catch (Exception t)
                {
                    Log(t);
                    return false;
                }
            }
            return false;
        }

        private bool HeuristicExchangeHeuristicAllAttacked(int Ord, int RowS, int ColS, int RowD, int ColD, int[,] Table)
        {
            object OO = new object();
            lock (OO)
            {
                try
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Ord, Table[RowS, ColS], Table[RowD, ColD], false, false));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Attack(CloneATable(Table), RowS, ColS, RowD, ColD, OrderColor(Ord), Ord));
                        th1.Wait();
                        th1.Dispose();
                        if (ab)
                        {
                            object OOO = new object();
                            lock (OOO)
                            {
                                AssignAAndList(RowS, ColS, RowD, ColD, ref HeuristicAllAttacked);
                                return true;
                            }
                        }
                    }
                }
                catch (Exception t)
                {
                    Log(t);
                    return false;
                }
            }
            return false;
        }

        private bool HeuristicExchangeHeuristicAllSupport(int Ord, int RowS, int ColS, int RowD, int ColD, int[,] Table)
        {
            object OO = new object();
            lock (OO)
            {
                try
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Ord, Table[RowS, ColS], Table[RowD, ColD], true, false));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(Table), RowS, ColS, RowD, ColD, OrderColor(Ord), Ord));
                        th1.Wait();
                        th1.Dispose();
                        if (ab)
                        {
                            object OOO = new object();
                            lock (OOO)
                            {
                                AssignAAndList(RowS, ColS, RowD, ColD, ref HeuristicAllSupport);
                                return true;
                            }
                        }
                    }
                }
                catch (Exception t)
                {
                    Log(t);
                    return false;
                }
            }
            return false;
        }

        private bool HeuristicExchangeHeuristicAllMove(int Ord, int RowS, int ColS, int RowD, int ColD, int[,] Table)
        {
            object OO = new object();
            lock (OO)
            {
                try
                {
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = Permit(Ord, Table[RowS, ColS], Table[RowD, ColD], true, true));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        Task<bool> th1 = Task.Factory.StartNew(() => ab = Movable(CloneATable(Table), RowS, ColS, RowD, ColD, OrderColor(Ord), Ord));
                        th1.Wait();
                        th1.Dispose();
                        if (ab)
                        {
                            object OOO = new object();
                            lock (OOO)
                            {
                                AssignAAndList(RowS, ColS, RowD, ColD, ref HeuristicAllMove);
                                return true;
                            }
                        }
                    }
                }
                catch (Exception t)
                {
                    Log(t);
                    return false;
                }
            }
            return false;
        }

        //create heuristic and lists of Move and reduced Move and attack and reduced attack and support and reduced support
        public double[] HeuristicExchange(bool Before, int Killed, int[,] Table, Color aa, int Ord, int Ros, int Cos, int Rod, int Cod)
        {
            object O = new object();
            lock (O)
            {
                int[,] RemobeActiveDenfesiveObjectsOfEnemy = new int[8, 8];
                const int ToSupport = 3, ReducedAttacked = 0, ReducedSupport = 2, ReducedMove = 5, ToAttacked = 1, ToMoved = 4;
                double[] Exchange = new double[6];
                double[] ExchangeSeed = new double[3];
                int DumOrd = Ord;
                int DummyOrd = Ord;
                int DummyCurrentOrd = ChessRules.CurrentOrder;

                ///When AStarGreedy Exchange is Not Assigned.
                object O1 = new object();
                lock (O1)
                {
                    ParallelOptions poop = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, 8, RowS =>
                    {
                        ParallelOptions poo = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, 8, ColS =>
                        {
                            ParallelOptions pooo = new ParallelOptions
                            {
                                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                            }; Parallel.For(0, 8, RowD =>
                            {
                                ParallelOptions poooo = new ParallelOptions
                                {
                                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                                }; Parallel.For(0, 8, ColD =>
                                {
                                    object o = new object();
                                    lock (o)
                                    {
                                        if (Table[RowS, ColS] == 0 && Table[RowD, ColD] == 0)
                                        {
                                            return;
                                        }

                                        Task output = Task.Factory.StartNew(() =>
                                        {
                                            Task H70 = Task.Factory.StartNew(() => ExchangeE(Before, Ord, ref Exchange, ToSupport, ReducedSupport, ReducedAttacked, ToAttacked, ReducedMove, ToMoved, Table, RowS, ColS, RowD, ColD));
                                            H70.Wait();
                                            H70.Dispose();
                                        });
                                        output.Wait(); output.Dispose();
                                    }
                                });
                            });
                        });
                    });
                }

                //When situation is closed
                Task H71 = Task.Factory.StartNew(() => ExchangeA(Ord, ref ExchangeSeed, Exchange, ToSupport, ReducedSupport, ReducedAttacked));
                H71.Wait();
                H71.Dispose();
                //when situation is closed and restriction
                Task H72 = Task.Factory.StartNew(() => ExchangeB(Ord, ref ExchangeSeed, Exchange, ReducedAttacked, ToAttacked));
                H72.Wait();
                H72.Dispose();
                //Closed space remove
                double A1 = (Exchange[ToAttacked] + Exchange[ToSupport] + Exchange[ToMoved]);
                //penalties
                double A2 = A1 + (Exchange[ReducedAttacked] + Exchange[ReducedSupport] + Exchange[ReducedMove]);
                ExchangeSeed[2] = (int)(RationalPenalty * ((A2 / 64.0)));

                //When victorian of self on enemy to consideration of weaker self traversal object at active enemy strong traversal
                Task H73 = Task.Factory.StartNew(() => ExchangeC(Ord, aa, ref ExchangeSeed, Exchange, ToSupport, ReducedSupport, ReducedAttacked, ToAttacked, Table, Ros, Cos, Rod, Cod));
                H73.Wait();
                H73.Dispose();
                //Simplification of mathematic method when we have victories
                double ExchangedOfGameSimplification = Exchange[ToSupport] - Exchange[ReducedSupport] + Exchange[ToAttacked] - Exchange[ReducedSupport];
                double MAX = 64.0;
                ExchangeSeed[2] += (int)(RationalRegard * (ExchangedOfGameSimplification / MAX));
                //Remove of most impressive defensive enemy Objects
                Task H74 = Task.Factory.StartNew(() => ExchangeD(Before, Ord, MAX, ref ExchangeSeed, RemobeActiveDenfesiveObjectsOfEnemy, ReducedSupport, ReducedAttacked, Table, Ros, Cos, Rod, Cod));
                H74.Wait();
                H74.Dispose();
                //Safty before Attack
                Task<double> H7 = Task.Factory.StartNew(() => ExchangeSeed[2] += (RationalPenalty * (NoOfExistInReducedMoveList(Before, Ros, Cos, Rod, Cod) + NoOfExistInReducedAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInReducedSupportList(Before, Ros, Cos, Rod, Cod))) + (RationalRegard * (NoOfExistInMoveList(Before, Ros, Cos, Rod, Cod) + NoOfExistInAttackList(Before, Ros, Cos, Rod, Cod) + NoOfExistInSupportList(Before, Ros, Cos, Rod, Cod))));
                H7.Wait();
                H7.Dispose();
                Ord = DummyOrd;
                ChessRules.CurrentOrder = DummyCurrentOrd;
                Ord = DumOrd;
                //Initiate to Begin Call Ords.
                return ExchangeSeed;
            }
        }

        private void ExchangeA(int Ord, ref double[] ExchangeSeed, double[] Exchange, int ToSupport, int ReducedSupport, int ReducedAttacked)
        {
            object o = new object();
            lock (o)
            {
                double A1 = 0;
                Task<double> H1 = Task.Factory.StartNew(() => A1 = IsSupportLessThanReducedSupport(Exchange[ToSupport], Exchange[ReducedSupport]));
                H1.Wait();
                H1.Dispose();
                if (A1 > 0)
                {
                    ExchangeSeed[0] = RationalPenalty;
                }
                else
                if (A1 < 0 && Exchange[ReducedSupport] == 0)
                {
                    ExchangeSeed[0] = RationalRegard;
                }
                else//When reinforcment arrangments is Ok
                {
                    if (Ord != AllDraw.OrderPlate)
                    {
                        if (IKIsCentralPawnIsOk && Exchange[ReducedAttacked] == 0)
                        {
                            ExchangeSeed[0] += RationalRegard;
                        }
                        else
                        {
                            if (IKIsCentralPawnIsOk && Exchange[ReducedAttacked] != 0)
                            {
                                ExchangeSeed[0] += RationalPenalty;
                            }
                        }
                    }
                }
            }
        }

        private void ExchangeB(int Ord, ref double[] ExchangeSeed, double[] Exchange, int ReducedAttacked, int ToAttacked)
        {
            object o = new object();
            lock (o)
            {
                double A1 = 0;
                Task<double> H2 = Task.Factory.StartNew(() => A1 = IsAttackLessThanReducedAttack(Exchange[ToAttacked], Exchange[ReducedAttacked]));
                H2.Wait();
                H2.Dispose();
                if (A1 > 0)
                {
                    ExchangeSeed[1] = RationalPenalty;
                }
                else
                if (A1 < 0 && Exchange[ReducedAttacked] == 0)
                {
                    ExchangeSeed[1] = RationalRegard;
                }
            }
        }

        private void ExchangeC(int Ord, Color aa, ref double[] ExchangeSeed, double[] Exchange, int ToSupport, int ReducedSupport, int ReducedAttacked, int ToAttacked, int[,] Table, int Ros, int Cos, int Rod, int Cod)
        {
            object o = new object();
            lock (o)
            {
                double HA = 1;
                if (ExchangeSeed[0] + ExchangeSeed[1] + ExchangeSeed[2] >= 0)
                {
                    if (Exchange[ToSupport] - Exchange[ReducedSupport] + Exchange[ToAttacked] - Exchange[ReducedAttacked] > 0)
                    {
                        double HAA6 = 0;
                        object O11 = new object();
                        lock (O11)
                        {
                            int i6 = Ros, j6 = Cos, iiii6 = Rod, jjjj6 = Cod;
                            int[,] Table6 = CloneATable(Table);
                            int Ord6 = Ord;
                            Color aa6 = aa;
                            Task<double> H3 = Task.Factory.StartNew(() => HAA6 = HeuristicEnemySupported(ref HA, Table6, Ord6, aa6, i6, j6, iiii6, jjjj6));
                            H3.Wait();
                            H3.Dispose();
                        }
                        if (HAA6 != 0)
                        {
                            if (System.Math.Abs(Table[Rod, Cod]) - System.Math.Abs(Table[Ros, Cos]) >= 2)
                            {
                                ExchangeSeed[0] += RationalRegard;
                            }
                        }
                        else
                        if (HAA6 == 0)
                        {
                            ExchangeSeed[0] += RationalRegard;
                        }
                    }
                }
            }
        }

        private void ExchangeD(bool Before, int Ord, double MAX, ref double[] ExchangeSeedA, int[,] RemobeActiveDenfesiveObjectsOfEnemy, int ReducedSupport, int ReducedAttacked, int[,] Table, int Ros, int Cos, int Rod, int Cod)
        {
            object o = new object();
            lock (o)
            {
                double[] ExchangeSeed = ExchangeSeedA;
                double Defen = RemobeActiveDenfesiveObjectsOfEnemy[Ros, Cos] - RemobeActiveDenfesiveObjectsOfEnemy[Rod, Cod];
                ExchangeSeed[2] += (int)(RationalRegard * (Defen / MAX) * 4);
                Task<double> H4 = Task.Factory.StartNew(() => ExchangeSeed[2] += HeuristicPromotion(Before, CloneATable(Table), Ord, Ros, Cos, Rod, Cod));
                H4.Wait();
                H4.Dispose();
                Task<double> H5 = Task.Factory.StartNew(() => ExchangeSeed[2] += HeuristicElephantOpen(Before, CloneATable(Table), Ord, Ros, Cos, Rod, Cod));
                H5.Wait();
                H5.Dispose();
                Task<double> H6 = Task.Factory.StartNew(() => ExchangeSeed[2] += HeuristicHourseCloseBaseOfWeakHourseIsWhereIsHomeStrong(Before, CloneATable(Table), Ord, Ros, Cos, Rod, Cod));
                H6.Wait();
                H6.Dispose();
                ExchangeSeedA = ExchangeSeed;
            }
        }

        private void ExchangeE(bool Before, int Ord, ref double[] ExchangeA, int ToSupport, int ReducedSupport, int ReducedAttacked, int ToAttacked, int ReducedMove, int ToMoved, int[,] Table, int RowS, int ColS, int RowD, int ColD)
        {
            object o = new object();
            lock (o)
            {
                if (!Before || !ExcangePerformed)
                {
                    double[] Exchange = ExchangeA;

                    ParallelOptions pooooo = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.Invoke(() =>
                    {
                        object O11 = new object();
                        lock (O11)
                        {
                            if (HeuristicExchangHeuristicAllReducedAttacked(Ord, RowS, ColS, RowD, ColD, Table))
                            {
                                Exchange[ReducedAttacked]++;
                            }
                        }
                    }
                                             , () =>
                                             {
                                                 object O11 = new object();
                                                 lock (O11)
                                                 {
                                                     if (HeuristicExchangeHeuristicAllReducedSupport(Ord, RowS, ColS, RowD, ColD, Table))
                                                     {
                                                         Exchange[ReducedSupport]++;
                                                     }
                                                 }
                                             }
                                           , () =>

                                           {
                                               object O11 = new object();
                                               lock (O11)
                                               {
                                                   if (HeuristicExchangeHeuristicAllReducedMove(Ord, RowS, ColS, RowD, ColD, Table))
                                                   {
                                                       Exchange[ReducedMove]++;
                                                   }
                                               }
                                           }
                                                , () =>

                                                {
                                                    object O11 = new object();
                                                    lock (O11)
                                                    {
                                                        if (HeuristicExchangeHeuristicAllAttacked(Ord, RowS, ColS, RowD, ColD, Table))
                                                        {
                                                            Exchange[ToAttacked]++;
                                                        }
                                                    }
                                                }
                                                , () =>
                                                {
                                                    object O11 = new object();
                                                    lock (O11)
                                                    {
                                                        if (HeuristicExchangeHeuristicAllSupport(Ord, RowS, ColS, RowD, ColD, Table))
                                                        {
                                                            Exchange[ToSupport]++;
                                                        }
                                                    }
                                                }
                                                  , () =>

                                                  {
                                                      object O11 = new object();
                                                      lock (O11)
                                                      {
                                                          if (HeuristicExchangeHeuristicAllMove(Ord, RowS, ColS, RowD, ColD, Table))
                                                          {
                                                              Exchange[ToMoved]++;
                                                          }
                                                      }
                                                  });
                    ExchangeA = Exchange;
                    PerformedExchange = ExchangeA;
                    ExcangePerformed = true;
                }
                else
                {
                    ExchangeA = PerformedExchange;
                }
            }
        }

        //when support less than reduced support
        private double IsSupportLessThanReducedSupport(double Support, double ReducedSupport)
        {
            object O = new object();
            lock (O)
            {
                if (Support == 0)
                {
                    return 0;
                }

                if (Support < ReducedSupport)
                {
                    return 1;
                }
                else
                    if (Support > ReducedSupport)
                {
                    return -1;
                }

                return 0;
            }
        }

        //when attack less than reduced attack
        private double IsAttackLessThanReducedAttack(double Attack, double ReducedAttack)
        {
            object O = new object();
            lock (O)
            {
                if (Attack == 0)
                {
                    return 0;
                }

                if (Attack < ReducedAttack)
                {
                    return 1;
                }
                else
                     if (Attack > ReducedAttack)
                {
                    return -1;
                }

                return 0;
            }
        }

        ///Attack Determination.QC_Ok
        public bool Attack(int[,] Tab, int i, int j, int ii, int jj, Color a, int Order)
        {
            object O = new object();
            lock (O)
            {
                if (Tab[i, j] == 0)
                {
                    return false;
                }

                if (Tab[ii, jj] == 0)
                {
                    return false;
                }

                if (Tab[i, j] > 0 && Tab[ii, jj] > 0)
                {
                    return false;
                }

                if (Tab[i, j] > 0 && Tab[ii, jj] == 0)
                {
                    return false;
                }

                if (Tab[i, j] < 0 && Tab[ii, jj] < 0)
                {
                    return false;
                }

                if (Tab[i, j] < 0 && Tab[ii, jj] == 0)
                {
                    return false;
                }

                int CCurentOrder = ChessRules.CurrentOrder;
                //Initiate Global static  Variable.
                ChessRules.CurrentOrder = Order;
                int[,] Table = CloneATable(Tab);
                //when there is a Movment from Parameter One to Second Parameter return Attacke..
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[i, j], CloneATable(Table), Order)).Rules(i, j, ii, jj, Order));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    ChessRules.CurrentOrder = CCurentOrder;
                    return true;
                }
                ChessRules.CurrentOrder = CCurentOrder;
                return false;
            }
        }

        //Object Danger Determination.
        public bool ObjectDanger(int[,] Tab, int i, int j, int ii, int jj, Color a, int Order)
        {
            object O = new object();
            lock (O)
            {
                int CCurrentOrder = ChessRules.CurrentOrder;
                //Initiate Local Varibales.
                int[,] Table = new int[8, 8];
                for (int RowS = 0; RowS < 8; RowS++)
                {
                    for (int ColS = 0; ColS < 8; ColS++)
                    {
                        Table[RowS, ColS] = Tab[RowS, ColS];
                    }
                }

                ChessRules.CurrentOrder = Order;
                ///When [i,j] is Attacked [ii,jj] retrun true when enemy is located in [ii,jj].
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[i, j], CloneATable(Table), Order)).Rules(i, j, ii, jj, Order));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    //Initiate Local Variables.
                    for (int RowS = 0; RowS < 8; RowS++)
                    {
                        for (int ColS = 0; ColS < 8; ColS++)
                        {
                            Table[RowS, ColS] = Tab[RowS, ColS];
                        }
                    }
                    //Take Movments.
                    Table[ii, jj] = Table[i, j];
                    Table[i, j] = 0;
                    //Consider Check.
                    ChessRules AA = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[ii, jj], CloneATable(Table), Order);

                    Task<bool> th1 = Task.Factory.StartNew(() => ab = AA.ObjectDangourKingMove(Order, CloneATable(Table), false));
                    th1.Wait();
                    th1.Dispose();
                    if (ab)
                    {
                        ChessRules.CurrentOrder = CCurrentOrder;
                        //Return ObjectDanger.
                        if ((AA.CheckGrayObjectDangour) && Order == 1)
                        {
                            return true;
                        }
                        else
                            if ((AA.CheckBrownObjectDangour) && Order == -1)
                        {
                            return true;
                        }
                    }
                    if (AA.CheckMate(CloneATable(Table), Order))
                    {
                        ChessRules.CurrentOrder = CCurrentOrder;
                        //Return ObjectDanger.
                        if ((AA.CheckGray || AA.CheckMateGray) && Order == 1)
                        {
                            return true;
                        }
                        else
                            if ((AA.CheckBrown || AA.CheckMateBrown) && Order == -1)
                        {
                            return true;
                        }
                    }
                }

                ChessRules.CurrentOrder = CCurrentOrder;

                //return Non ObjectDanger.
                return false;
            }
        }

        ///Supportation Determination.QC_OK
        public bool Support(int[,] Tab, int i, int j, int ii, int jj, Color a, int Order)
        {
            object O = new object();
            lock (O)
            {
                if (Tab[i, j] == 0)
                {
                    return false;
                }
                //Initiate Local Variables.
                int[,] Table = new int[8, 8];
                for (int RowS = 0; RowS < 8; RowS++)
                {
                    for (int ColS = 0; ColS < 8; ColS++)
                    {
                        Table[RowS, ColS] = Tab[RowS, ColS];
                    }
                }

                ///When All Tables is Gray.
                if (Order == 1 && Table[i, j] > 0)
                {
                    ///When [i,j] Supporte [ii,jj].
                    bool ab = false;
                    Task<bool> th = Task.Factory.StartNew(() => ab = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[i, j], CloneATable(Table), Order)).Rules(i, j, ii, jj, Table[i, j], false) && SameSign(Table[i, j], Table[ii, jj]));
                    th.Wait();
                    th.Dispose();
                    if (ab)
                    {
                        return true;
                    }
                }
                else
                {
                    if (Order == -1 && Table[i, j] < 0)
                    {  ///When [i,j] Supporte [ii,jj].
                        bool ab = false;
                        Task<bool> th = Task.Factory.StartNew(() => ab = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Table[i, j], CloneATable(Table), Order)).Rules(i, j, ii, jj, Table[i, j], false) && SameSign(Table[i, j], Table[ii, jj]));
                        th.Wait();
                        th.Dispose();
                        if (ab)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
        }

        //Return Msx Huiristic of Child Level.
        public bool MaxHeuristic(ref int j, int Kin, ref double Less, int Order)
        {
            object O = new object();
            lock (O)
            {
                bool Found = false;
                //When Solders.
                if (Kin == 1)
                {
                    for (int i = 0; i < PenaltyRegardListSolder.Count; i++)
                    {
                        if (PenaltyRegardListSolder[i].IsPenaltyAction() != 0)
                        {
                            if (Order == AllDraw.OrderPlateDraw)
                            {
                                if (Less > HeuristicListSolder[i][0] +
                                    HeuristicListSolder[i][1] +
                                    HeuristicListSolder[i][2] +
                                    HeuristicListSolder[i][3] +
                                    HeuristicListSolder[i][4] +
                                    HeuristicListSolder[i][5] +
                                    HeuristicListSolder[i][6] +
                                    HeuristicListSolder[i][7] +
                                    HeuristicListSolder[i][8] +
                                    HeuristicListSolder[i][9])
                                {
                                    Less = HeuristicListSolder[i][0] +
                                HeuristicListSolder[i][1] +
                                HeuristicListSolder[i][2] +
                                HeuristicListSolder[i][3] +
                                HeuristicListSolder[i][4] +
                                HeuristicListSolder[i][5] +
                                HeuristicListSolder[i][6] +
                                HeuristicListSolder[i][7] +
                                    HeuristicListSolder[i][8] +
                                    HeuristicListSolder[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                            else
                            {
                                if (Less < HeuristicListSolder[i][0] +
                             HeuristicListSolder[i][1] +
                             HeuristicListSolder[i][2] +
                             HeuristicListSolder[i][3] +
                             HeuristicListSolder[i][4] +
                             HeuristicListSolder[i][5] +
                             HeuristicListSolder[i][6] +
                             HeuristicListSolder[i][7] +
                             HeuristicListSolder[i][8] +
                             HeuristicListSolder[i][9])
                                {
                                    Less = HeuristicListSolder[i][0] +
                                HeuristicListSolder[i][1] +
                                HeuristicListSolder[i][2] +
                                HeuristicListSolder[i][3] +
                                HeuristicListSolder[i][4] +
                                HeuristicListSolder[i][5] +
                                HeuristicListSolder[i][6] +
                                HeuristicListSolder[i][7] +
                                    HeuristicListSolder[i][8] +
                                    HeuristicListSolder[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                        }
                    }
                }
                else//When Elephant.
                    if (Kin == 2)
                {
                    for (int i = 0; i < PenaltyRegardListElefant.Count; i++)
                    {
                        if (PenaltyRegardListElefant[i].IsPenaltyAction() != 0)
                        {
                            if (Order == AllDraw.OrderPlateDraw)
                            {
                                if (Less > HeuristicListElefant[i][0] +
                                    HeuristicListElefant[i][1] +
                                    HeuristicListElefant[i][2] +
                                    HeuristicListElefant[i][3] +
                                    HeuristicListElefant[i][4] +
                                    HeuristicListElefant[i][5] +
                                    HeuristicListElefant[i][6] +
                                    HeuristicListElefant[i][7] +
                                    HeuristicListElefant[i][8] +
                                    HeuristicListElefant[i][9])
                                {
                                    Less = HeuristicListElefant[i][0] +
                                HeuristicListElefant[i][1] +
                                HeuristicListElefant[i][2] +
                                HeuristicListElefant[i][3] +
                                HeuristicListElefant[i][4] +
                                HeuristicListElefant[i][5] +
                                HeuristicListElefant[i][6] +
                                HeuristicListElefant[i][7] +
                                    HeuristicListElefant[i][8] +
                                    HeuristicListElefant[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                            else
                            {
                                if (Less < HeuristicListElefant[i][0] +
                             HeuristicListElefant[i][1] +
                             HeuristicListElefant[i][2] +
                             HeuristicListElefant[i][3] +
                             HeuristicListElefant[i][4] +
                             HeuristicListElefant[i][5] +
                             HeuristicListElefant[i][6] +
                             HeuristicListElefant[i][7] +
                             HeuristicListElefant[i][8] +
                             HeuristicListElefant[i][9])
                                {
                                    Less = HeuristicListElefant[i][0] +
                                HeuristicListElefant[i][1] +
                                HeuristicListElefant[i][2] +
                                HeuristicListElefant[i][3] +
                                HeuristicListElefant[i][4] +
                                HeuristicListElefant[i][5] +
                                HeuristicListElefant[i][6] +
                                HeuristicListElefant[i][7] +
                                    HeuristicListElefant[i][8] +
                                    HeuristicListElefant[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                        }
                    }
                }
                else//When Hourse.
                        if (Kin == 3)
                {
                    for (int i = 0; i < PenaltyRegardListHourse.Count; i++)
                    {
                        if (PenaltyRegardListHourse[i].IsPenaltyAction() != 0)
                        {
                            if (Order == AllDraw.OrderPlateDraw)
                            {
                                if (Less > HeuristicListHourse[i][0] +
                                    HeuristicListHourse[i][1] +
                                    HeuristicListHourse[i][2] +
                                    HeuristicListHourse[i][3] +
                                    HeuristicListHourse[i][4] +
                                    HeuristicListHourse[i][5] +
                                    HeuristicListHourse[i][6] +
                                    HeuristicListHourse[i][7] +
                                    HeuristicListHourse[i][8] +
                                    HeuristicListHourse[i][9])
                                {
                                    Less = HeuristicListHourse[i][0] +
                                HeuristicListHourse[i][1] +
                                HeuristicListHourse[i][2] +
                                HeuristicListHourse[i][3] +
                                HeuristicListHourse[i][4] +
                                HeuristicListHourse[i][5] +
                                HeuristicListHourse[i][6] +
                                HeuristicListHourse[i][7] +
                                    HeuristicListHourse[i][8] +
                                    HeuristicListHourse[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                            else
                            {
                                if (Less < HeuristicListHourse[i][0] +
                             HeuristicListHourse[i][1] +
                             HeuristicListHourse[i][2] +
                             HeuristicListHourse[i][3] +
                             HeuristicListHourse[i][4] +
                             HeuristicListHourse[i][5] +
                             HeuristicListHourse[i][6] +
                             HeuristicListHourse[i][7] +
                             HeuristicListHourse[i][8] +
                             HeuristicListHourse[i][9])
                                {
                                    Less = HeuristicListHourse[i][0] +
                                HeuristicListHourse[i][1] +
                                HeuristicListHourse[i][2] +
                                HeuristicListHourse[i][3] +
                                HeuristicListHourse[i][4] +
                                HeuristicListHourse[i][5] +
                                HeuristicListHourse[i][6] +
                                HeuristicListHourse[i][7] +
                                    HeuristicListHourse[i][8] +
                                    HeuristicListHourse[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                        }
                    }
                }
                else//When Castles.
                            if (Kin == 4)
                {
                    for (int i = 0; i < PenaltyRegardListCastle.Count; i++)
                    {
                        if (PenaltyRegardListCastle[i].IsPenaltyAction() != 0)
                        {
                            if (Order == AllDraw.OrderPlateDraw)
                            {
                                if (Less > HeuristicListCastle[i][0] +
                                    HeuristicListCastle[i][1] +
                                    HeuristicListCastle[i][2] +
                                    HeuristicListCastle[i][3] +
                                    HeuristicListCastle[i][4] +
                                    HeuristicListCastle[i][5] +
                                    HeuristicListCastle[i][6] +
                                    HeuristicListCastle[i][7] +
                                    HeuristicListCastle[i][8] +
                                    HeuristicListCastle[i][9])
                                {
                                    Less = HeuristicListCastle[i][0] +
                                HeuristicListCastle[i][1] +
                                HeuristicListCastle[i][2] +
                                HeuristicListCastle[i][3] +
                                HeuristicListCastle[i][4] +
                                HeuristicListCastle[i][5] +
                                HeuristicListCastle[i][6] +
                                HeuristicListCastle[i][7] +
                                    HeuristicListCastle[i][8] +
                                    HeuristicListCastle[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                            else
                            {
                                if (Less < HeuristicListCastle[i][0] +
                             HeuristicListCastle[i][1] +
                             HeuristicListCastle[i][2] +
                             HeuristicListCastle[i][3] +
                             HeuristicListCastle[i][4] +
                             HeuristicListCastle[i][5] +
                             HeuristicListCastle[i][6] +
                             HeuristicListCastle[i][7] +
                             HeuristicListCastle[i][8] +
                             HeuristicListCastle[i][9])
                                {
                                    Less = HeuristicListCastle[i][0] +
                                HeuristicListCastle[i][1] +
                                HeuristicListCastle[i][2] +
                                HeuristicListCastle[i][3] +
                                HeuristicListCastle[i][4] +
                                HeuristicListCastle[i][5] +
                                HeuristicListCastle[i][6] +
                                HeuristicListCastle[i][7] +
                                    HeuristicListCastle[i][8] +
                                    HeuristicListCastle[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                        }
                    }
                }
                else//When Minister.
                                if (Kin == 5)
                {
                    for (int i = 0; i < PenaltyRegardListMinister.Count; i++)
                    {
                        if (PenaltyRegardListMinister[i].IsPenaltyAction() != 0)
                        {
                            if (Order == AllDraw.OrderPlateDraw)
                            {
                                if (Less > HeuristicListMinister[i][0] +
                                    HeuristicListMinister[i][1] +
                                    HeuristicListMinister[i][2] +
                                    HeuristicListMinister[i][3] +
                                    HeuristicListMinister[i][4] +
                                    HeuristicListMinister[i][5] +
                                    HeuristicListMinister[i][6] +
                                    HeuristicListMinister[i][7] +
                                    HeuristicListMinister[i][8] +
                                    HeuristicListMinister[i][9]
                                    )
                                {
                                    Less = HeuristicListMinister[i][0] +
                                HeuristicListMinister[i][1] +
                                HeuristicListMinister[i][2] +
                                HeuristicListMinister[i][3] +
                                HeuristicListMinister[i][4] +
                                HeuristicListMinister[i][5] +
                                HeuristicListMinister[i][6] +
                                HeuristicListMinister[i][7] +
                                    HeuristicListMinister[i][8] +
                                    HeuristicListMinister[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                            else
                            {
                                if (Less < HeuristicListMinister[i][0] +
                             HeuristicListMinister[i][1] +
                             HeuristicListMinister[i][2] +
                             HeuristicListMinister[i][3] +
                             HeuristicListMinister[i][4] +
                             HeuristicListMinister[i][5] +
                             HeuristicListMinister[i][6] +
                             HeuristicListMinister[i][7] +
                             HeuristicListMinister[i][8] +
                             HeuristicListMinister[i][9]
                             )
                                {
                                    Less = HeuristicListMinister[i][0] +
                                HeuristicListMinister[i][1] +
                                HeuristicListMinister[i][2] +
                                HeuristicListMinister[i][3] +
                                HeuristicListMinister[i][4] +
                                HeuristicListMinister[i][5] +
                                HeuristicListMinister[i][6] +
                                HeuristicListMinister[i][7] +
                                    HeuristicListMinister[i][8] +
                                    HeuristicListMinister[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                        }
                    }
                }
                else//When King.
                                    if (Kin == 6)
                {
                    for (int i = 0; i < PenaltyRegardListKing.Count; i++)
                    {
                        if (PenaltyRegardListKing[i].IsPenaltyAction() != 0)
                        {
                            if (Order == AllDraw.OrderPlateDraw)
                            {
                                if (Less > HeuristicListKing[i][0] +
                                    HeuristicListKing[i][1] +
                                    HeuristicListKing[i][2] +
                                    HeuristicListKing[i][3] +
                                    HeuristicListKing[i][4] +
                                    HeuristicListKing[i][5] +
                                    HeuristicListKing[i][6] +
                                    HeuristicListKing[i][7] +
                                    HeuristicListKing[i][8] +
                                    HeuristicListKing[i][9])
                                {
                                    Less = HeuristicListKing[i][0] +
                                HeuristicListKing[i][1] +
                                HeuristicListKing[i][2] +
                                HeuristicListKing[i][3] +
                                HeuristicListKing[i][4] +
                                HeuristicListKing[i][5] +
                                HeuristicListKing[i][6] +
                                HeuristicListKing[i][7] +
                                    HeuristicListKing[i][8] +
                                    HeuristicListKing[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                            else
                            {
                                if (Less < HeuristicListKing[i][0] +
                             HeuristicListKing[i][1] +
                             HeuristicListKing[i][2] +
                             HeuristicListKing[i][3] +
                             HeuristicListKing[i][4] +
                             HeuristicListKing[i][5] +
                             HeuristicListKing[i][6] +
                             HeuristicListKing[i][7] +
                             HeuristicListKing[i][8] +
                             HeuristicListKing[i][9])
                                {
                                    Less = HeuristicListKing[i][0] +
                                HeuristicListKing[i][1] +
                                HeuristicListKing[i][2] +
                                HeuristicListKing[i][3] +
                                HeuristicListKing[i][4] +
                                HeuristicListKing[i][5] +
                                HeuristicListKing[i][6] +
                                HeuristicListKing[i][7] +
                                    HeuristicListKing[i][8] +
                                    HeuristicListKing[i][9];
                                    j = i;
                                    Found = true;
                                }
                            }
                        }
                    }
                }
                return Found;
            }
        }

        //Return Heuristic.
        public double ReturnHeuristic(int ii, int j, int Order, bool AA, ref int HaveKilled)
        {
            object O = new object();
            lock (O)
            {
                AllDraw.OutPut = new System.Text.StringBuilder("");

                //calculation of Heuristic methos and storing value retured.
                double Hur = new double();
                object O1 = new object();
                lock (O1)
                {
                    if (!AA)
                    {
                        if (ii >= 0 && UsePenaltyRegardMechnisamT)
                        {
                            int Hav = HaveKilled;
                            Task<double> th = Task.Factory.StartNew(() => Hur = (ReturnHeuristicCalculartor(0, ii, j, Order, ref Hav) * LearniningTable.LearingValue(Row, Column)));
                            th.Wait();
                            th.Dispose();
                            HaveKilled = Hav;
                        }
                        else
                        {
                            int Hav = HaveKilled;
                            Task<double> th = Task.Factory.StartNew(() => Hur = ReturnHeuristicCalculartor(0, ii, j, Order, ref Hav));
                            th.Wait();
                            th.Dispose();
                            HaveKilled = Hav;
                        }
                    }
                    else
                    {
                        int Hav = HaveKilled;
                        Task<double> th = Task.Factory.StartNew(() => Hur = ReturnHeuristicCalculartor(0, ii, j, Order, ref Hav) + 1000);
                        th.Wait();
                        th.Dispose();
                        HaveKilled = Hav;
                    }
                    //Optimization depend of numbers of unpealties nodes quefficient.
                    if (UsePenaltyRegardMechnisamT)
                    {
                        Task<double> th = Task.Factory.StartNew(() => Hur *= ((NumbersOfAllNode - NumbersOfCurrentBranchesPenalties) / NumbersOfAllNode));
                        th.Wait();
                        th.Dispose();
                        return Hur;
                    }

                    return Hur;
                }
            }
        }

        public double RationalArray(double Her, double[] a)
        {
            object o = new object();
            lock (o)
            {
                double HA = 0;

                if (Her >= 0)
                {
                    for (int i = 0; i < 10; i++)
                        HA += RationalArrayFor(i, Her, a);
                }
                else
                {
                    HA += RationalArrayIF(Her, a);
                }
                return HA;
            }
        }

        public double RationalArrayFor(int i,double Her, double[] a)
        {
            object o = new object();
            lock (o)
            {
                double HA = 0;


                if (a[i] >= 0 && HA >= 0)
                {
                    HA += a[i];
                }
                else
                {
                    HA = (System.Math.Abs(Math.Abs(HA) + Math.Abs(a[i])) * -1);
                }

                return HA;
            }
        }
        public double RationalArrayIF(double Her, double[] a)
        {
            object o = new object();
            lock (o)
            {
                double HA = 0;


                if (Her >= 0 && HA >= 0)
                {
                    HA += Her;
                }
                else
                {
                    HA = ((Math.Abs(HA) + Math.Abs(Her)) * -1);
                }
                return HA;
            }
        }
        //statstical html
        private string Alphabet(int RowRealesed)
        {
            object O = new object();
            lock (O)
            {
                string A = "";
                if (RowRealesed == 0)
                {
                    A = "a";
                }
                else
                    if (RowRealesed == 1)
                {
                    A = "b";
                }
                else
                        if (RowRealesed == 2)
                {
                    A = "c";
                }
                else
                            if (RowRealesed == 3)
                {
                    A = "d";
                }
                else
                                if (RowRealesed == 4)
                {
                    A = "e";
                }
                else
                                    if (RowRealesed == 5)
                {
                    A = "f";
                }
                else
                                        if (RowRealesed == 6)
                {
                    A = "g";
                }
                else
                                            if (RowRealesed == 7)
                {
                    A = "h";
                }

                return A;
            }
        }

        //statstical html
        private string Number(int ColumnRealeased)
        {
            object O = new object();
            lock (O)
            {
                string A = "";
                if (ColumnRealeased == 7)
                {
                    A = "0";
                }
                else
                    if (ColumnRealeased == 6)
                {
                    A = "1";
                }
                else
                        if (ColumnRealeased == 5)
                {
                    A = "2";
                }
                else
                            if (ColumnRealeased == 4)
                {
                    A = "3";
                }
                else
                                if (ColumnRealeased == 3)
                {
                    A = "4";
                }
                else
                                    if (ColumnRealeased == 2)
                {
                    A = "5";
                }
                else
                                        if (ColumnRealeased == 1)
                {
                    A = "6";
                }
                else
                                            if (ColumnRealeased == 0)
                {
                    A = "7";
                }

                return A;
            }
        }

        //Heuristic help to kiling of enemy or gave point witout only lraearning autamata exclusive but act on.
        public double ReturnHeuristicCalculartorKiller(int iAstarGready, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;
            //when killer list satisfied
            if (KillerAtThinking.Count > j)
            {
                //when killer list exist and for victory
                if (KillerAtThinking[j] > 0)
                {
                    IsAtLeastOneKillerAtDraw = true;
                    HaveKilled = iAstarGready;
                }
                else//when kiler is for lose
                if (KillerAtThinking[j] < 0)
                {
                    IsAtLeastOneKillerAtDraw = true;
                    HaveKilled = (iAstarGready * -1);
                }
            }
            //when there is not served layer
            bool A = !IsSupHuTrue(j); if (A && j >= 0)
            {
                //when there is computations
                for (j = 0; HeuristicListSolder != null && j < HeuristicListSolder.Count; j++)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListSolder[j]);
                    /*Heuristic += HeuristicListSolder[j][0] +
                        HeuristicListSolder[j][1] +
                        HeuristicListSolder[j][2] +
                        HeuristicListSolder[j][3] +
                        HeuristicListSolder[j][4] +
                        HeuristicListSolder[j][5] +
                        HeuristicListSolder[j][6] +
                    HeuristicListSolder[j][7] +
                    HeuristicListSolder[j][8] +
                    HeuristicListSolder[j][9];*/
                    object O1 = new object();
                    lock (O1)
                    {
                        if (AllDraw.NumberOfLeafComputation == -1 && AllDraw.FirstTraversalTree)
                        {
                            //if (Order == 1)
                            //else
                        }
                    }
                }
                //When Elephant Kind.
                for (j = 0; HeuristicListElefant != null && j < HeuristicListElefant.Count; j++)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListElefant[j]);
                    /* Heuristic += HeuristicListElefant[j][0] +
                     HeuristicListElefant[j][1] +
                     HeuristicListElefant[j][2] +
                     HeuristicListElefant[j][3] +
                     HeuristicListElefant[j][4] +
                     HeuristicListElefant[j][5] +
                     HeuristicListElefant[j][6] +
                     HeuristicListElefant[j][7] +
                     HeuristicListElefant[j][8] +
                     HeuristicListElefant[j][9];*/
                    object O1 = new object();
                    lock (O1)
                    {
                        if (AllDraw.NumberOfLeafComputation == -1 && AllDraw.FirstTraversalTree)
                        {
                            //if (Order == 1)
                            //else
                        }
                    }
                }
                //when is hourse
                for (j = 0; HeuristicListHourse != null && j < HeuristicListHourse.Count; j++)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListHourse[j]);
                    /*Heuristic += HeuristicListHourse[j][0] +
                HeuristicListHourse[j][1] +
                HeuristicListHourse[j][2] +
                HeuristicListHourse[j][3] +
                HeuristicListHourse[j][4] +
                HeuristicListHourse[j][5] +
                HeuristicListHourse[j][6] +
                HeuristicListHourse[j][7] +
                HeuristicListHourse[j][8] +
                HeuristicListHourse[j][9];*/
                    object O1 = new object();
                    lock (O1)
                    {
                        if (AllDraw.NumberOfLeafComputation == -1 && AllDraw.FirstTraversalTree)
                        {
                            //if (Order == 1)
                            //else
                        }
                    }
                }
                //when is Castle
                for (j = 0; HeuristicListCastle != null && j < HeuristicListCastle.Count; j++)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListCastle[j]);
                    /* Heuristic += HeuristicListCastle[j][0] +
             HeuristicListCastle[j][1] +
             HeuristicListCastle[j][2] +
             HeuristicListCastle[j][3] +
             HeuristicListCastle[j][4] +
             HeuristicListCastle[j][5] +
             HeuristicListCastle[j][6] +
             HeuristicListCastle[j][7] +
             HeuristicListCastle[j][8] +
             HeuristicListCastle[j][9];*/
                    object O1 = new object();
                    lock (O1)
                    {
                        if (AllDraw.NumberOfLeafComputation == -1 && AllDraw.FirstTraversalTree)
                        {
                            //if (Order == 1)
                            //else
                        }
                    }
                }
                //when is minister
                for (j = 0; HeuristicListMinister != null && j < HeuristicListMinister.Count; j++)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListMinister[j]);
                    /*Heuristic += HeuristicListMinister[j][0] +
        HeuristicListMinister[j][1] +
        HeuristicListMinister[j][2] +
        HeuristicListMinister[j][3] +
        HeuristicListMinister[j][4] +
        HeuristicListMinister[j][5] +
        HeuristicListMinister[j][6] +
        HeuristicListMinister[j][7] +
        HeuristicListMinister[j][8] +
        HeuristicListMinister[j][9];*/
                    object O1 = new object();
                    lock (O1)
                    {
                        if (AllDraw.NumberOfLeafComputation == -1 && AllDraw.FirstTraversalTree)
                        {
                            //if (Order == 1)
                            //else
                        }
                    }
                }
                //when is king
                for (j = 0; HeuristicListKing != null && j < HeuristicListKing.Count; j++)
                {
                    {
                        Heuristic = RationalArray(Heuristic, HeuristicListKing[j]);
                        /*Heuristic += HeuristicListKing[j][0] +
        HeuristicListKing[j][1] +
        HeuristicListKing[j][2] +
        HeuristicListKing[j][3] +
        HeuristicListKing[j][4] +
        HeuristicListKing[j][5] +
        HeuristicListKing[j][6] +
        HeuristicListKing[j][7] +
        HeuristicListKing[j][8] +
        HeuristicListKing[j][9];*/
                        object O1 = new object();
                        lock (O1)
                        {
                            if (AllDraw.NumberOfLeafComputation == -1 && AllDraw.FirstTraversalTree)
                            {
                                //if (Order == 1)
                                //else
                            }
                        }
                    }
                }//when is king
                for (j = 0; HeuristicListCastling != null && j < HeuristicListCastling.Count; j++)
                {
                    {
                        Heuristic = RationalArray(Heuristic, HeuristicListCastling[j]);
                        /*Heuristic += HeuristicListCastling[j][0] +
         HeuristicListCastling[j][1] +
         HeuristicListCastling[j][2] +
         HeuristicListCastling[j][3] +
         HeuristicListCastling[j][4] +
         HeuristicListCastling[j][5] +
         HeuristicListCastling[j][6] +
         HeuristicListCastling[j][7] +
         HeuristicListCastling[j][8] +
         HeuristicListCastling[j][9];*/
                        object O1 = new object();
                        lock (O1)
                        {
                            if (AllDraw.NumberOfLeafComputation == -1 && AllDraw.FirstTraversalTree)
                            {
                                //if (Order == 1)
                                //else
                            }
                        }
                    }
                }
            }

            return Heuristic;
        }

        //deeper section to deep inside Heuristic calculation
        public double ReturnHeuristicCalculartorDeeper(int ii, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;
            //when is deeper
            if (AStarGreedy != null)
            {
                //for all deeper
                for (int k = 0; k < AStarGreedy.Count; k++)
                {
                    //continue when deeper is null
                    if (AStarGreedy[k] == null)
                    {
                        continue;
                    }

                    object OOO = new object();
                    lock (OOO)
                    {
                        //Gray
                        if (Order == -1)
                        {
                            //Repeate for Solder.
                            for (int m = 0; m < AStarGreedy[k].SodierMidle; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperSolider(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();
                            }
                            //Repeate for Elephant.
                            for (int m = 0; m < AStarGreedy[k].ElefantMidle; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperElephant(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for Hourse.
                            for (int m = 0; m < AStarGreedy[k].HourseMidle; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperHourse(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for Castles.
                            for (int m = 0; m < AStarGreedy[k].CastleMidle; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperCastle(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for Minstre.
                            for (int m = 0; m < AStarGreedy[k].MinisterMidle; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperMinister(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for King.
                            for (int m = 0; m < AStarGreedy[k].KingMidle; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperKing(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            } //Repeate for King.
                            for (int m = 0; m < 1; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperCastling(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                        }
                        else
                        {
                            for (int m = AStarGreedy[k].SodierMidle; m < AStarGreedy[k].SodierHigh; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperSolider(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for Elephant.
                            for (int m = AStarGreedy[k].ElefantMidle; m < AStarGreedy[k].ElefantHigh; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperElephant(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for Hourse.
                            for (int m = AStarGreedy[k].HourseMidle; m < AStarGreedy[k].HourseHight; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperHourse(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for Castles.
                            for (int m = AStarGreedy[k].CastleMidle; m < AStarGreedy[k].CastleHigh; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperCastle(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for Minstre.
                            for (int m = AStarGreedy[k].MinisterMidle; m < AStarGreedy[k].MinisterHigh; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperMinister(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                            //Repeate for King.
                            for (int m = 0; m < 1; m++)
                            {
                                int hav = HaveKilled;
                                Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeperCastling(k, m, ii, j, Order, ref hav));
                                th.Wait();
                                th.Dispose();

                                HaveKilled = hav;
                            }
                        }
                    }
                }
            }
            return Heuristic;
        }

        //deeper for specific object
        public double ReturnHeuristicCalculartorDeeperCastling(int k, int m, int ii, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;
            try
            {
                if (AStarGreedy[k].CastlingOnTable == null || AStarGreedy[k].CastlingOnTable[m] == null || AStarGreedy[k].CastlingOnTable[m].CastlingThinking == null || AStarGreedy[k].CastlingOnTable[m].CastlingThinking[0] == null || AStarGreedy[k].CastlingOnTable[m].CastlingThinking[0].TableListCastling == null)
                {
                    return Heuristic;
                }

                for (int jj = 0; jj < AStarGreedy[k].CastlingOnTable[m].CastlingThinking[0].TableListCastling.Count; jj++)
                {
                    int hav = HaveKilled;
                    Task<double> th = Task.Factory.StartNew(() => Heuristic += AStarGreedy[k].CastlingOnTable[m].CastlingThinking[0].ReturnHeuristicCalculartor(0, ii, jj, Order * -1, ref hav));
                    th.Wait();
                    th.Dispose();
                    HaveKilled = hav;
                }
            }
            catch (Exception t) { Log(t); }
            return Heuristic;
        }

        //deeper for specific object
        public double ReturnHeuristicCalculartorDeeperKing(int k, int m, int ii, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;
            try
            {
                if (AStarGreedy[k].KingOnTable == null || AStarGreedy[k].KingOnTable[m] == null || AStarGreedy[k].KingOnTable[m].KingThinking == null || AStarGreedy[k].KingOnTable[m].KingThinking[0] == null || AStarGreedy[k].KingOnTable[m].KingThinking[0].TableListKing == null)
                {
                    return Heuristic;
                }

                for (int jj = 0; jj < AStarGreedy[k].KingOnTable[m].KingThinking[0].TableListKing.Count; jj++)
                {
                    int hav = HaveKilled;
                    Task<double> th = Task.Factory.StartNew(() => Heuristic += AStarGreedy[k].KingOnTable[m].KingThinking[0].ReturnHeuristicCalculartor(0, ii, jj, Order * -1, ref hav));
                    th.Wait();
                    th.Dispose();
                    HaveKilled = hav;
                }
            }
            catch (Exception t) { Log(t); }
            return Heuristic;
        }
        //deeper for specific object
        public double ReturnHeuristicCalculartorDeeperMinister(int k, int m, int ii, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;
            try
            {
                if (AStarGreedy[k].MinisterOnTable == null || AStarGreedy[k].MinisterOnTable[m] == null || AStarGreedy[k].MinisterOnTable[m].MinisterThinking == null || AStarGreedy[k].MinisterOnTable[m].MinisterThinking[0] == null || AStarGreedy[k].MinisterOnTable[m].MinisterThinking[0].TableListMinister == null)
                {
                    return Heuristic;
                }

                for (int jj = 0; jj < AStarGreedy[k].MinisterOnTable[m].MinisterThinking[0].TableListMinister.Count; jj++)
                {
                    int hav = HaveKilled;
                    Task<double> th = Task.Factory.StartNew(() => Heuristic += AStarGreedy[k].MinisterOnTable[m].MinisterThinking[0].ReturnHeuristicCalculartor(0, ii, jj, Order * -1, ref hav));
                    th.Wait();
                    th.Dispose();
                    HaveKilled = hav;
                }
            }
            catch (Exception t) { Log(t); }
            return Heuristic;
        }

        //deeper for specific object
        public double ReturnHeuristicCalculartorDeeperCastle(int k, int m, int ii, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;
            try
            {
                if (AStarGreedy[k].CastlesOnTable == null || AStarGreedy[k].CastlesOnTable[m] == null || AStarGreedy[k].CastlesOnTable[m].CastleThinking == null || AStarGreedy[k].CastlesOnTable[m].CastleThinking[0] == null || AStarGreedy[k].CastlesOnTable[m].CastleThinking[0].TableListCastle == null)
                {
                    return Heuristic;
                }

                for (int jj = 0; jj < AStarGreedy[k].CastlesOnTable[m].CastleThinking[0].TableListCastle.Count; jj++)
                {
                    int hav = HaveKilled;
                    Task<double> th = Task.Factory.StartNew(() => Heuristic += AStarGreedy[k].CastlesOnTable[m].CastleThinking[0].ReturnHeuristicCalculartor(0, ii, jj, Order * -1, ref hav));
                    th.Wait();
                    th.Dispose();
                    HaveKilled = hav;
                }
            }
            catch (Exception t) { Log(t); }
            return Heuristic;
        }

        //deeper for specific object
        public double ReturnHeuristicCalculartorDeeperHourse(int k, int m, int ii, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;
            try
            {
                if (AStarGreedy[k].HoursesOnTable == null || AStarGreedy[k].HoursesOnTable[m] == null || AStarGreedy[k].HoursesOnTable[m].HourseThinking == null || AStarGreedy[k].HoursesOnTable[m].HourseThinking[0] == null || AStarGreedy[k].HoursesOnTable[m].HourseThinking[0].TableListHourse == null)
                {
                    return Heuristic;
                }

                for (int jj = 0; jj < AStarGreedy[k].HoursesOnTable[m].HourseThinking[0].TableListHourse.Count; jj++)
                {
                    int hav = HaveKilled;
                    Task<double> th = Task.Factory.StartNew(() => Heuristic += AStarGreedy[k].HoursesOnTable[m].HourseThinking[0].ReturnHeuristicCalculartor(0, ii, jj, Order * -1, ref hav));
                    th.Wait();
                    th.Dispose();
                    HaveKilled = hav;
                }
            }
            catch (Exception t) { Log(t); }
            return Heuristic;
        }

        //deeper for specific object
        public double ReturnHeuristicCalculartorDeeperElephant(int k, int m, int ii, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;
            try
            {
                if (AStarGreedy[k].ElephantOnTable == null || AStarGreedy[k].ElephantOnTable[m] == null || AStarGreedy[k].ElephantOnTable[m].ElefantThinking == null || AStarGreedy[k].ElephantOnTable[m].ElefantThinking[0] == null || AStarGreedy[k].ElephantOnTable[m].ElefantThinking[0].TableListElefant == null)
                {
                    return Heuristic;
                }

                for (int jj = 0; jj < AStarGreedy[k].ElephantOnTable[m].ElefantThinking[0].TableListElefant.Count; jj++)
                {
                    int hav = HaveKilled;
                    Task<double> th = Task.Factory.StartNew(() => Heuristic += AStarGreedy[k].ElephantOnTable[m].ElefantThinking[0].ReturnHeuristicCalculartor(0, ii, jj, Order * -1, ref hav));
                    th.Wait();
                    th.Dispose();
                    HaveKilled = hav;
                }
            }
            catch (Exception t) { Log(t); }

            return Heuristic;
        }

        //deeper for specific object
        public double ReturnHeuristicCalculartorDeeperSolider(int k, int m, int ii, int j, int Order, ref int HaveKilled)
        {
            double Heuristic = 0;

            try
            {
                if (AStarGreedy[k].SolderesOnTable == null || AStarGreedy[k].SolderesOnTable[m] == null || AStarGreedy[k].SolderesOnTable[m].SoldierThinking == null || AStarGreedy[k].SolderesOnTable[m].SoldierThinking[0] == null || AStarGreedy[k].SolderesOnTable[m].SoldierThinking[0].TableListSolder == null)
                {
                    return Heuristic;
                }

                for (int jj = 0; jj < AStarGreedy[k].SolderesOnTable[m].SoldierThinking[0].TableListSolder.Count; jj++)
                {
                    int hav = HaveKilled;
                    Task<double> th = Task.Factory.StartNew(() => Heuristic += AStarGreedy[k].SolderesOnTable[m].SoldierThinking[0].ReturnHeuristicCalculartor(0, ii, jj, Order * -1, ref hav));
                    th.Wait();
                    th.Dispose();
                    HaveKilled = hav;
                }
            }
            catch (Exception t) { Log(t); }
            return Heuristic;
        }

        public double ReturnHeuristicCalculartorSurface(int j, int Order)
        {
            double Heuristic = 0;
            bool A = !IsSupHuTrue(j); if (A && j >= 0)
            {
                //When Solder Kind.
                if (System.Math.Abs(Kind) == 1 && HeuristicListSolder.Count > 0)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListSolder[j]);

                    /*Heuristic += HeuristicListSolder[j][0] +
                        HeuristicListSolder[j][1] +
                        HeuristicListSolder[j][2] +
                        HeuristicListSolder[j][3] +
                        HeuristicListSolder[j][4] +
                        HeuristicListSolder[j][5] +
                        HeuristicListSolder[j][6] +
                        HeuristicListSolder[j][7] +
                        HeuristicListSolder[j][8] +
                        HeuristicListSolder[j][9];*/
                }
                else
                //When Elephant Kind.
                if (System.Math.Abs(Kind) == 2 && HeuristicListElefant.Count > 0)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListElefant[j]);

                    /*Heuristic += HeuristicListElefant[j][0] +
                        HeuristicListElefant[j][1] +
                        HeuristicListElefant[j][2] +
                        HeuristicListElefant[j][3] +
                        HeuristicListElefant[j][4] +
                        HeuristicListElefant[j][5] +
                        HeuristicListElefant[j][6] +
                        HeuristicListElefant[j][7] +
                        HeuristicListElefant[j][8] +
                    HeuristicListElefant[j][9];*/
                }
                else
                //When Hourse Kind.
                if (System.Math.Abs(Kind) == 3 && HeuristicListHourse.Count > 0)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListHourse[j]);

                    /*Heuristic += HeuristicListHourse[j][0] +
                        HeuristicListHourse[j][1] +
                        HeuristicListHourse[j][2] +
                        HeuristicListHourse[j][3] +
                        HeuristicListHourse[j][4] +
                        HeuristicListHourse[j][5] +
                        HeuristicListHourse[j][6] +
                        HeuristicListHourse[j][7] +
                        HeuristicListHourse[j][8] +
                    HeuristicListHourse[j][9];*/
                }
                else
                //When Castles Kind.
                if (System.Math.Abs(Kind) == 4 && HeuristicListCastle.Count > 0)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListCastle[j]);
                    /*Heuristic += HeuristicListCastle[j][0] +
                         HeuristicListCastle[j][1] +
                         HeuristicListCastle[j][2] +
                         HeuristicListCastle[j][3] +
                         HeuristicListCastle[j][4] +
                         HeuristicListCastle[j][5] +
                         HeuristicListCastle[j][6] +
                         HeuristicListCastle[j][7] +
                     HeuristicListCastle[j][8] +
                         HeuristicListCastle[j][9];*/
                }
                else
                //When Minister Kind.
                if (System.Math.Abs(Kind) == 5 && HeuristicListMinister.Count > 0)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListMinister[j]);
                    /*Heuristic += HeuristicListMinister[j][0] +
                        HeuristicListMinister[j][1] +
                        HeuristicListMinister[j][2] +
                        HeuristicListMinister[j][3] +
                        HeuristicListMinister[j][4] +
                        HeuristicListMinister[j][5] +
                        HeuristicListMinister[j][6] +
                    HeuristicListMinister[j][7] +
                    HeuristicListMinister[j][8] +
                    HeuristicListMinister[j][9];*/
                }
                else
                //When King Kind.
                if (System.Math.Abs(Kind) == 6 && HeuristicListKing.Count > 0)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListKing[j]);
                    /*Heuristic += HeuristicListKing[j][0] +
                        HeuristicListKing[j][1] +
                        HeuristicListKing[j][2] +
                        HeuristicListKing[j][3] +
                        HeuristicListKing[j][4] +
                        HeuristicListKing[j][5] +
                        HeuristicListKing[j][6] +
                        HeuristicListKing[j][7] +
                        HeuristicListKing[j][8] +
                        HeuristicListKing[j][9];*/
                }
                else
                //When King Kind.
                if (System.Math.Abs(Kind) == 7 && HeuristicListCastling.Count > 0)
                {
                    Heuristic = RationalArray(Heuristic, HeuristicListCastling[j]);
                    /*Heuristic += HeuristicListCastling[j][0] +
                        HeuristicListCastling[j][1] +
                        HeuristicListCastling[j][2] +
                        HeuristicListCastling[j][3] +
                        HeuristicListCastling[j][4] +
                        HeuristicListCastling[j][5] +
                        HeuristicListCastling[j][6] +
                        HeuristicListCastling[j][7] +
                        HeuristicListCastling[j][8] +
                        HeuristicListCastling[j][9];*/
                }
            }

            return Heuristic;
        }

        //main insider method for manage Heuristic count
        public double ReturnHeuristicCalculartor(int iAstarGready, int ii, int j, int Order, ref int HaveKilled)
        {
            /*if (iAstarGready > PlatformHelper.ProcessorCount)
            {
                return 0;
            }*/

            iAstarGready++;
            object O = new object();
            lock (O)
            {
                double Heuristic = 0;
                //when deeper there is not or level exceed
                if (AStarGreedy == null && iAstarGready != 0)
                {
                    return 0;
                }
                NumbersOfCurrentBranchesPenalties += NumberOfPenalties;
                int DummyOrder = Order;
                //when there is deeper
                if (ii != -1)
                {
                    //kiiler Heuristic determination//main deeper Heuristic
                    int hav = HaveKilled;

                    Task<double> th = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorKiller(ii, j, Order, ref hav));
                    th.Wait();
                    th.Dispose();
                    HaveKilled = hav;

                    //main deeper Heuristic
                    hav = HaveKilled;

                    Task<double> th1 = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorDeeper(ii, j, Order, ref hav));
                    th1.Wait();
                    th1.Dispose();
                    HaveKilled = hav;
                }
                else
                {
                    //sufacive Heuristic
                    int hav = HaveKilled;

                    Task<double> th1 = Task.Factory.StartNew(() => Heuristic += ReturnHeuristicCalculartorSurface(j, Order));
                    th1.Wait();
                    th1.Dispose();
                    HaveKilled = hav;
                }
                Order = DummyOrder;
                /*if (BOUND < 0)
                    Heuristic = int.MinValue;
                else
                    if (BOUND > 0)
                    Heuristic = int.MaxValue;*/

                return Heuristic;
            }
        }

        //Returrn of Hurestic Tree.QC_Ok.
        //Scope of Every Objects Movments.
        private bool Scop(int i, int j, int ii, int jj, int Kind)
        {
            object O = new object();
            lock (O)
            {
                if (i == ii && j == jj)
                {
                    return false;
                }
                //Scope of index out of range.
                if (i < 0)
                {
                    return false;
                }
                if (j < 0)
                {
                    return false;
                }
                if (ii < 0)
                {
                    return false;
                }
                if (jj < 0)
                {
                    return false;
                }
                if (i > 7)
                {
                    return false;
                }
                if (j > 7)
                {
                    return false;
                }
                if (ii > 7)
                {
                    return false;
                }
                if (jj > 7)
                {
                    return false;
                }
                bool Validity = false;
                //Scope on estimation on rule movment.
                if (Kind == 1)//Sodier
                {
                    if (ArrangmentsChanged)
                    {
                        if (Order == 1)
                        {
                            if (j <= jj)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (j >= jj)
                            {
                                return false;
                            }
                        }
                    }
                    else if (!ArrangmentsChanged)
                    {
                        if (Order == -1)
                        {
                            if (j <= jj)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (j >= jj)
                            {
                                return false;
                            }
                        }
                    }
                    if (System.Math.Abs(i - ii) <= 2 && System.Math.Abs(j - jj) <= 2)
                    {
                        Validity = true;
                    }
                }
                else
                    if (Kind == 2)//Elephant
                {
                    if (System.Math.Abs(i - ii) == System.Math.Abs(j - jj))
                    {
                        Validity = true;
                    }
                }
                else
                        if (Kind == 3)//Hourse
                {
                    if (System.Math.Abs(i - ii) == 1 && System.Math.Abs(j - jj) == 2)
                    {
                        Validity = true;
                    }

                    if (System.Math.Abs(i - ii) == 2 && System.Math.Abs(j - jj) == 1)
                    {
                        Validity = true;
                    }
                }
                else
                            if (Kind == 4)//Castle
                {
                    if ((i == ii && j != jj) || (i != ii && j == jj))
                    {
                        Validity = true;
                    }
                }
                else
                                if (Kind == 5)//Minister
                {
                    if (((i == ii && j != jj) || (i != ii && j == jj)) || System.Math.Abs(i - ii) == System.Math.Abs(j - jj))
                    {
                        Validity = true;
                    }
                }
                else
              if (Kind == 6)//King
                {
                    if (System.Math.Abs(i - ii) <= 1 && System.Math.Abs(j - jj) <= 1)
                    {
                        Validity = true;
                    }
                }
                else
              if (Kind == 7 || Kind == -7)//Castling
                {
                    Validity = true;
                }
                if (Kind != 7 && Kind != -7)//Castling
                {
                    if (Order == 1)
                    {
                        if (TableConst[i, j] != Kind)
                        {
                            Validity = false;
                        }
                    }
                    else
                    {
                        if (TableConst[i, j] != (Kind * -1))
                        {
                            Validity = false;
                        }
                    }
                }
                return Validity;
            }
        }

        private bool Scop(int i, int j, int ii, int jj)
        {
            object O = new object();
            lock (O)
            {
                if (i == ii && j == jj)
                {
                    return false;
                }
                //Scope of index out of range.
                if (i < 0)
                {
                    return false;
                }
                if (j < 0)
                {
                    return false;
                }
                if (ii < 0)
                {
                    return false;
                }
                if (jj < 0)
                {
                    return false;
                }
                if (i > 7)
                {
                    return false;
                }
                if (j > 7)
                {
                    return false;
                }
                if (ii > 7)
                {
                    return false;
                }
                if (jj > 7)
                {
                    return false;
                }

                return true;
            }
        }

        public void HuMethod(ref double[] Hu, double HeuristicAttackValue, double HeuristicMovementValue, double HeuristicSelfSupportedValue, double HeuristicReducedMovementValue, double HeuristicReducedSupport, double HeuristicReducedAttackValue, double HeuristicDistributionValue, double HeuristicKingSafe, double HeuristicFromCenter, double HeuristicKingDangour, double HeuristicCheckedMate)
        {
            Hu[0] += HeuristicAttackValue;
            Hu[1] += HeuristicMovementValue;
            Hu[2] += HeuristicSelfSupportedValue;
            Hu[3] += HeuristicReducedMovementValue;
            Hu[4] += HeuristicReducedSupport;
            Hu[5] += HeuristicReducedAttackValue;
            Hu[6] += HeuristicDistributionValue;
            Hu[7] += HeuristicKingSafe;
            Hu[8] += HeuristicFromCenter;
            Hu[9] += HeuristicKingDangour + HeuristicCheckedMate;
            return;
        }

        private void HuMethodSup(double HeuristicAttackValue, double HeuristicMovementValue, double HeuristicSelfSupportedValue, double HeuristicReducedMovementValue, double HeuristicReducedSupport, double HeuristicReducedAttackValue, double HeuristicDistributionValue, double HeuristicKingSafe, double HeuristicFromCenter, double HeuristicKingDangour, double HeuristicCheckedMate)
        {
            HeuristicAttackValueSup += HeuristicAttackValue;
            HeuristicMovementValueSup += HeuristicMovementValue;
            HeuristicSelfSupportedValueSup += HeuristicSelfSupportedValue;
            HeuristicReducedMovementValueSup += HeuristicReducedMovementValue;
            HeuristicReducedSupportSup += HeuristicReducedSupport;
            HeuristicReducedAttackValueSup += HeuristicReducedAttackValue;
            HeuristicDistributionValueSup += HeuristicDistributionValue;
            HeuristicKingSafeSup += HeuristicKingSafe;
            HeuristicFromCenterSup += HeuristicFromCenter;
            HeuristicKingDangourSup += HeuristicKingDangour + HeuristicCheckedMate;
        }

        private void HuMethodSup(ref double[] Hu)
        {
            Hu[0] = HeuristicAttackValueSup;
            Hu[1] = HeuristicMovementValueSup;
            Hu[2] = HeuristicSelfSupportedValueSup;
            Hu[3] = HeuristicReducedMovementValueSup;
            Hu[4] = HeuristicReducedSupportSup;
            Hu[5] = HeuristicReducedAttackValueSup;
            Hu[6] = HeuristicDistributionValueSup;
            Hu[7] = HeuristicKingSafeSup;
            Hu[8] = HeuristicFromCenterSup;
            Hu[9] = HeuristicKingDangourSup;
            return;
        }

        //specific determination for thinking main method
        public void KingThinkingChess(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int DummyOrder, int DummyCurrentOrder, int[,] TableS, int RowSource, int ColumnSource, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, int RowDestination, int ColumnDestination, bool Castle)
        {
            object O = new object();
            lock (O)
            {
                TableS = CloneATable(TableConst);
                double HeuristicAttackValue = new double();
                double HeuristicMovementValue = new double();
                double HeuristicSelfSupportedValue = new double();
                double HeuristicReducedMovementValue = new double();
                double HeuristicReducedSupport = new double();
                double HeuristicReducedAttackValue = new double();
                double HeuristicDistributionValue = new double();
                double HeuristicKingSafe = new double();
                double HeuristicFromCenter = new double();
                double HeuristicKingDangour = new double(); double HeuristicCheckedMate = new double();
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                ///When There is Movments.
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = ThinkingChessRuleThinking(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                    int CheckedM = 0; bool PenaltyVCar = false;
                    bool Sup = false;
                    Task newTask1 = Task.Factory.StartNew(() => SupMethod(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, ref Sup));
                    newTask1.Wait(); newTask1.Dispose();

                    if (!Sup)
                    {
                        ///Add Table to List of Private.
                        HitNumberKing.Add(TableS[RowDestination, ColumnDestination]);
                        object OO = new object();
                        lock (OO)
                        {
                            ThinkingRun = true;
                        }
                    }
                    ///Predict Heuristic.
                    object A = new object();
                    lock (A)
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, true, Order, 0, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                    object A1 = new object();
                    lock (A1)
                    {
                        if (!Sup) { NumbersOfAllNode++; }
                    }
                    bool ac=false;
                    int Killed = 0;
                    newTask1 = Task.Factory.StartNew(() => ac=KilledMethod(ref Killed, Sup, RowSource, ColumnSource, RowDestination, ColumnDestination, ref TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!ac)
                    {
                        HitNumberKing.RemoveAt(HitNumberKing.Count - 1);
                        return;
                    }

                    // if (!Sup)
                    {
                        object A3 = new object();
                        lock (A3)
                        {
                            PenaltyVCar = false;
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                            newTask1 = Task.Factory.StartNew(() => PenaltyMechanisam(ref PenaltyVCar, ref TmpL, ref TmpW, ref CheckedM, Killed, Kind, CloneATable(TableS), RowSource, ColumnSource, ref Current, DoEnemySelf, PenRegStrore, RowDestination, ColumnDestination));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    ///Store of Indexes Changes and Table in specific List.
                    newTask1 = Task.Factory.StartNew(() => ObjectIndexes(Kind, Sup, RowDestination, ColumnDestination, TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    ///Wehn Predict of Operation Do operate a Predict of this movments.
                    object A5 = new object();
                    lock (A5)
                    {
                        //Caused this for Stachostic results.
                        if (!Sup)
                        {
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, false, Order, Killed, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    //Calculate Heuristic and Add to List and Cal Syntax.
                    if (!Sup)
                    {
                        string H = "";
                        object A6 = new object();
                        lock (A6)
                        {
                            AsS(RowSource, ColumnSource, RowDestination, ColumnDestination);
                            double[] Hu = new double[10];
                            //if (!(IsSup[j]))
                            {
                                //if (IgnoreFromCheckandMateHeuristic)

                                newTask1 = Task.Factory.StartNew(() => HuMethod(ref Hu, HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                                newTask1.Wait(); newTask1.Dispose();
                                H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                                HeuristicListKing.Add(Hu);
                            }
                        }
                        object O4 = new object();
                        lock (O4)
                        {
                        }
                    }
                    else
                    {
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        double[] Hu = new double[10];
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(ref Hu));
                        newTask1.Wait(); newTask1.Dispose();

                        string H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                        newTask1 = Task.Factory.StartNew(() => HeuristicInsertion(Kind, RowDestination, ColumnDestination, CloneATable(TableS), Hu));
                        newTask1.Wait(); newTask1.Dispose();
                    }
                }
                else
                {
                    MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, 1, -1);
                }
            }
        }

        //specific determination for thinking main method
        public void MinisterThinkingChess(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int DummyOrder, int DummyCurrentOrder, int[,] TableS, int RowSource, int ColumnSource, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, int RowDestination, int ColumnDestination, bool Castle)
        {
            object O11 = new object();
            lock (O11)
            {
                TableS = CloneATable(TableConst);
                double HeuristicAttackValue = new double();
                double HeuristicMovementValue = new double();
                double HeuristicSelfSupportedValue = new double();
                double HeuristicReducedMovementValue = new double();
                double HeuristicReducedSupport = new double();
                double HeuristicReducedAttackValue = new double();
                double HeuristicDistributionValue = new double();
                double HeuristicKingSafe = new double();
                double HeuristicFromCenter = new double();
                double HeuristicKingDangour = new double(); double HeuristicCheckedMate = new double();
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                ///When There is Movments.
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = ThinkingChessRuleThinking(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                    int CheckedM = 0; bool PenaltyVCar = false;
                    bool Sup = false;
                    Task newTask1 = Task.Factory.StartNew(() => SupMethod(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, ref Sup));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!Sup)
                    {
                        ///Add Table to List of Private.
                        HitNumberMinister.Add(TableS[RowDestination, ColumnDestination]);
                        object OO = new object();
                        lock (OO)
                        {
                            ThinkingRun = true;
                        }
                    }
                    ///Predict Heuristic.
                    object A = new object();
                    lock (A)
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, true, Order, 0, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                    object A1 = new object();
                    lock (A1)
                    {
                        if (!Sup) { NumbersOfAllNode++; }
                    }
                    bool ac=false;
                    int Killed = 0;
                    newTask1 = Task.Factory.StartNew(() => ac=KilledMethod(ref Killed, Sup, RowSource, ColumnSource, RowDestination, ColumnDestination, ref TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!ac)
                    {
                        HitNumberMinister.RemoveAt(HitNumberMinister.Count - 1);
                        return;
                    }

                    // if (!Sup)
                    {
                        object A3 = new object();
                        lock (A3)
                        {
                            PenaltyVCar = false;
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                            newTask1 = Task.Factory.StartNew(() => PenaltyMechanisam(ref PenaltyVCar, ref TmpL, ref TmpW, ref CheckedM, Killed, Kind, CloneATable(TableS), RowSource, ColumnSource, ref Current, DoEnemySelf, PenRegStrore, RowDestination, ColumnDestination));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    ///Store of Indexes Changes and Table in specific List.
                    newTask1 = Task.Factory.StartNew(() => ObjectIndexes(Kind, Sup, RowDestination, ColumnDestination, TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    ///Wehn Predict of Operation Do operate a Predict of this movments.
                    object A5 = new object();
                    lock (A5)
                    {
                        //Caused this for Stachostic results.
                        if (!Sup)
                        {
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, false, Order, Killed, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    //Calculate Heuristic and Add to List and Cal Syntax.
                    if (!Sup)
                    {
                        string H = "";
                        object A6 = new object();
                        lock (A6)
                        {
                            AsS(RowSource, ColumnSource, RowDestination, ColumnDestination);
                            double[] Hu = new double[10];
                            //if (!(IsSup[j]))
                            {
                                //if (IgnoreFromCheckandMateHeuristic)

                                newTask1 = Task.Factory.StartNew(() => HuMethod(ref Hu, HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                                newTask1.Wait(); newTask1.Dispose();
                                H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                                HeuristicListMinister.Add(Hu);
                            }
                        }
                        object O4 = new object();
                        lock (O4)
                        {
                        }
                    }
                    else
                    {
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        double[] Hu = new double[10];
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(ref Hu));
                        newTask1.Wait(); newTask1.Dispose();

                        string H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();

                        newTask1 = Task.Factory.StartNew(() => HeuristicInsertion(Kind, RowDestination, ColumnDestination, CloneATable(TableS), Hu));
                        newTask1.Wait(); newTask1.Dispose();
                    }
                }
                else
                {
                    MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, 1, -1);
                }
            }
        }

        //determination for kinmgs for stage of movment befor act
        private bool IsPrviousMovemntIsDangrousForCurrent(int[,] TableS, int Order)
        {
            object O = new object();
            lock (O)
            {
                bool Dang = false;
                int BREAK = 0;
                object O1 = new object();
                lock (O1)
                {
                    //.Current
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            BREAK = 0;
                            if (Order == 1 && TableS[i, j] <= 0)
                            {
                                return false;
                            }
                            else
                                if (Order == -1 && TableS[i, j] >= 0)
                            {
                                return false;
                            }
                            //Enemy
                            for (int ii = 0; ii < 8; ii++)
                            {
                                for (int jj = 0; jj < 8; jj++)
                                {
                                    BREAK = 0;
                                    if (Order == 1 && TableS[ii, jj] >= 0)
                                    {
                                        continue;
                                    }
                                    else
                                        if (Order == -1 && TableS[ii, jj] <= 0)
                                    {
                                        continue;
                                    }

                                    Color a = Color.Gray;
                                    if (Order * -1 == -1)
                                    {
                                        a = Color.Brown;
                                    }

                                    bool ab = false;
                                    Task<bool> th = Task.Factory.StartNew(() => ab = Attack(CloneATable(TableS), ii, jj, i, j, a, Order * -1));
                                    th.Wait();
                                    th.Dispose();
                                    if (ab)
                                    {
                                        BREAK = 1;
                                        //Current
                                        for (int RowS = 0; RowS < 8; RowS++)
                                        {
                                            for (int ColS = 0; ColS < 8; ColS++)
                                            {
                                                BREAK = 0;
                                                if (Order == 1 && TableS[RowS, ColS] <= 0)
                                                {
                                                    continue;
                                                }
                                                else
                                                    if (Order == -1 && TableS[RowS, ColS] >= 0)
                                                {
                                                    continue;
                                                }

                                                a = Color.Gray;
                                                if (Order == -1)
                                                {
                                                    a = Color.Brown;
                                                }

                                                Task<bool> th1 = Task.Factory.StartNew(() => ab = Support(CloneATable(TableS), RowS, ColS, i, j, a, Order));
                                                th1.Wait();
                                                th1.Dispose();
                                                if (ab)
                                                {
                                                    BREAK = 2;
                                                    break;
                                                }
                                            }
                                            if (BREAK == 2)
                                            {
                                                break;
                                            }
                                        }
                                    }
                                    if (BREAK == 2)
                                    {
                                        break;
                                    }
                                }
                                if (BREAK == 2)
                                {
                                    break;
                                }
                            }
                            if (BREAK == 2)
                            {
                                break;
                            }
                        }
                        if (BREAK == 2)
                        {
                            break;
                        }
                    }
                    if (BREAK == 1)
                    {
                        Dang = true;
                    }
                }
                return Dang;
            }
        }

        //When There is not valuable Object in List Greater than Target Self Object return true.
        private bool IsObjectValaubleObjectSelf(int Object, ref List<int[]> ValuableSelfSupported)
        {
            object O = new object();
            lock (O)
            {
                bool Is = true;
                for (int k = 0; k < ValuableSelfSupported.Count; k++)
                {
                    if (ValuableSelfSupported[k][0] > 0 && Object > 0)
                    {
                        if (System.Math.Abs(ValuableSelfSupported[k][0]) > System.Math.Abs(Object))
                        {
                            Is = false;
                        }
                    }
                    else
                       if (ValuableSelfSupported[k][0] < 0 && Object < 0)
                    {
                        if (System.Math.Abs(ValuableSelfSupported[k][0]) > System.Math.Abs(Object))
                        {
                            Is = false;
                        }
                    }
                    if (Is == false)
                    {
                        break;
                    }
                }
                return Is;
            }
        }

        //When There is not valuable Object in List Greater than Target enemy Object return true.
        private bool IsObjectValaubleObjectEnemy(int Object, ref List<int[]> ValuableEnemyNotSupported)
        {
            object O = new object();
            lock (O)
            {
                bool Is = true;
                for (int k = 0; k < ValuableEnemyNotSupported.Count; k++)
                {
                    if (System.Math.Abs(ValuableEnemyNotSupported[k][0]) < System.Math.Abs(Object))
                    {
                        Is = false;
                        break;
                    }
                }

                return Is;
            }
        }

        //a machine learning of learning autamata surface scan
        private bool[] SomeLearningVarsCalculator(int[,] TableS, int ik, int jk, int iik, int jjk)
        {
            object O22 = new object();
            lock (O22)
            {
                int AttackCount = 0;
                bool[] LearningV = new bool[3];
                object O = new object();
                lock (O)
                {
                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, i =>
                    for (int i = 0; i < 8; i++)
                    {
                        if ((LearningV[0] && LearningV[1] && LearningV[2]))
                        {
                            continue;
                        }
                        ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, j =>
                        for (int j = 0; j < 8; j++)
                        {
                            if ((LearningV[0] && LearningV[1] && LearningV[2]))
                            {
                                continue;
                            }
                            ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, RowS =>
                            for (int RowS = 0; RowS < 8; RowS++)
                            {
                                if ((LearningV[0] && LearningV[1] && LearningV[2]))
                                {
                                    continue;
                                }
                                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, ColS =>
                                for (int ColS = 0; ColS < 8; ColS++)
                                {
                                    if ((LearningV[0] && LearningV[1] && LearningV[2]))
                                    {
                                        continue;
                                    }
                                    //ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.Invoke(() =>
                                    {
                                        object O1 = new object();
                                        lock (O1)
                                        {
                                            if (!(LearningV[0] && LearningV[1] && LearningV[2]))
                                            {
                                                LearningV[0] = LearningV[0] || InAttackSelfThatNotSupportedAll(CloneATable(TableS), Order, color, i, j, RowS, ColS, ik, jk, iik, jjk);
                                            }
                                        }
                                    }//, () =>
                                    {
                                        object O1 = new object();
                                        lock (O1)
                                        {
                                            if (!(LearningV[0] && LearningV[1] && LearningV[2]))
                                            {
                                                continue;
                                            }

                                            if (AttackCount <= 1 && (!(LearningV[0] && LearningV[1] && LearningV[2])))
                                            {
                                                AttackCount += IsNotSafeToMoveAenemeyToAttackMoreThanTowObject(AttackCount, CloneATable(TableS), Order, i, j, RowS, ColS//, ii, jj, RowD, ColD
                                                    );
                                            }
                                            else
                                             if (!(LearningV[0] && LearningV[1] && LearningV[2]))
                                            {
                                                LearningV[1] = true;
                                            }
                                        }
                                    }//, () =>
                                    {
                                        object O1 = new object();
                                        lock (O1)
                                        {
                                            if (!(LearningV[0] && LearningV[1] && LearningV[2]))
                                            {
                                                LearningV[2] = LearningV[2] || IsGardForCurrentMovmentsAndIsNotMovable(CloneATable(TableS), Order, color, i, j, RowS, ColS//, ii, jj, RowD, ColD
                                                    );
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                return LearningV;
            }
        }

        //learning autamata main section
        private bool[] CalculateLearningVars(int Killed, int[,] TableS, int i, int j, int ii, int jj)
        {
            object O = new object();
            lock (O)
            {
                bool[] LearningV = new bool[14];
                bool IsCurrentCanGardHighPriorityEne = new bool();
                bool IsNextMovemntIsCheckOrCheckMateForCurrent = new bool();
                bool IsDangerous = new bool();
                bool CanKillerAnUnSupportedEnemy = new bool();
                bool InDangrousUnSupported = new bool();
                bool Support = new bool();
                bool IsNextMovemntIsCheckOrCheckMateForEnemy = new bool();
                bool IsPrviousMovemntIsDangrousForCurr = new bool();
                bool PDo = new bool();
                bool RDo = new bool();
                bool SelfNotSupported = new bool();
                bool EnemyNotSupported = new bool();
                bool IsGardForCurrentMovmentsAndIsNotMova = new bool();
                bool IsNotSafeToMoveAenemeyToAttackMoreThanTowObj = new bool();
                bool P = new bool();
                bool R = new bool();
                bool IsTowValuableObjectEnemy = false;
                List<int[]> ValuableEnemyNotSupported = new List<int[]>();
                List<int[]> ValuableSelfSupported = new List<int[]>();
                //When true must penalty
                object O11 = new object();
                lock (O11)
                {
                    //when true there is attack not supported.
                    Task<bool> newTask = Task.Factory.StartNew(() => IsPrviousMovemntIsDangrousForCurr = IsPrviousMovemntIsDangrousForCurrent(CloneATable(TableS), Order));
                    newTask.Wait();
                    newTask.Dispose();
                    //when true must penalty
                    if (!IsPrviousMovemntIsDangrousForCurr)
                    {
                        //when dangerouse Move that in it not supported true.
                        newTask = Task.Factory.StartNew(() => SelfNotSupported = InAttackSelfThatNotSupported(CloneATable(TableS), Order, color, i, j, ii, jj));
                        newTask.Wait();
                        newTask.Dispose();
                    } //when true must regard
                    Support = false;
                    int SelfChackedMateDepth = 0;
                    int EnemyCheckedMateDepth = 0;
                    IsDangerous = false;
                    //For All Current
                    bool[] LearningVars = new bool[3];
                    //every true must have cost.
                    Task.Factory.StartNew(() => LearningVars = SomeLearningVarsCalculator(CloneATable(TableS), ii, jj, i, j));
                    object O4 = new object();
                    lock (O4)
                    {
                        SelfNotSupported = LearningVars[0];
                        IsNotSafeToMoveAenemeyToAttackMoreThanTowObj = LearningVars[1];
                        IsGardForCurrentMovmentsAndIsNotMova = LearningVars[2];
                    }
                    //if ((!IsNextMovemntIsCheckOrCheckMateForCurrent) && (!SelfNotSupported) && (!IsPrviousMovemntIsDangrousForCurr) && (!IsGardForCurrentMovmentsAndIsNotMova) && (!IsNotSafeToMoveAenemeyToAttackMoreThanTowObj) && (!IsDangerous))
                    {
                        int[] Is = new int[4];
                        Is[0] = 0;
                        Is[1] = 0;
                        Is[2] = 0;
                        Is[3] = 0;
                        if (CurrentAStarGredyMax == 0)
                        {
                            int Depth = new int();
                            Depth = 0;
                            int[,] Tab = CloneATable(TableS);
                            int Ord = Order;
                            Color a = color;
                            int Ord1 = AllDraw.OrderPlate;
                            int Ord2 = AllDraw.OrderPlate * -1;
                            //when is true must penalty(Superposition)
                            Task.Factory.StartNew(() => Is = IsNextMovmentIsCheckOrCheckMateForCurrentMovment(CloneATable(Tab), Ord, Depth, Ord1, Ord2, true));
                            //A
                        }
                        object OO1 = new object();
                        lock (OO1)
                        {
                            if (Is[0] >= 1)
                            {
                                IsNextMovemntIsCheckOrCheckMateForCurrent = true;
                            }
                            else
                            {
                                IsNextMovemntIsCheckOrCheckMateForCurrent = false;
                            }

                            if (Is[2] >= 1)
                            {
                                IsNextMovemntIsCheckOrCheckMateForEnemy = true;
                            }
                            else
                            {
                                IsNextMovemntIsCheckOrCheckMateForEnemy = false;
                            }

                            SelfChackedMateDepth = Is[1];
                            EnemyCheckedMateDepth = Is[3];
                        }
                    }
                    //Order Depth Consideration Constraint.
                    if (IsNextMovemntIsCheckOrCheckMateForCurrent && IsNextMovemntIsCheckOrCheckMateForEnemy)
                    {
                        object OO2 = new object();
                        lock (OO2)
                        {
                            if (SelfChackedMateDepth < EnemyCheckedMateDepth)
                            {
                                IsNextMovemntIsCheckOrCheckMateForEnemy = false;
                            }
                            else
                            if (SelfChackedMateDepth > EnemyCheckedMateDepth)
                            {
                                IsNextMovemntIsCheckOrCheckMateForCurrent = false;
                            }
                        }
                    }
                    newTask = Task.Factory.StartNew(() => EnemyNotSupported = InAttackEnemyThatIsNotSupportedAll(IsTowValuableObjectEnemy, CloneATable(TableS), Order, color, ref ValuableEnemyNotSupported));
                    newTask.Wait();
                    newTask.Dispose();
                    if (!EnemyNotSupported)
                    {
                        newTask = Task.Factory.StartNew(() => EnemyNotSupported = InAttackEnemyThatIsNotSupportedAll(IsTowValuableObjectEnemy, CloneATable(TableS), Order, color, ref ValuableEnemyNotSupported));
                        newTask.Wait();
                        newTask.Dispose();
                    }
                    if (!EnemyNotSupported)
                    {
                        //when is true must regard.
                        newTask = Task.Factory.StartNew(() => IsCurrentCanGardHighPriorityEne = IsCurrentCanGardHighPriorityEnemy(0, CloneATable(TableS), Order, color, i, j, ii, jj, Order));
                        newTask.Wait();
                        newTask.Dispose();
                    }

                     if ((!IsNextMovemntIsCheckOrCheckMateForCurrent) && (!SelfNotSupported) && (!IsPrviousMovemntIsDangrousForCurr) && (!IsGardForCurrentMovmentsAndIsNotMova) && (!IsNotSafeToMoveAenemeyToAttackMoreThanTowObj) && (!IsDangerous))
                     {
                         newTask = Task.Factory.StartNew(() => EnemyNotSupported = InAttackEnemyThatIsNotSupportedAll(IsTowValuableObjectEnemy, CloneATable(TableS), Order, color, ref ValuableEnemyNotSupported));
                         newTask.Wait();
                         newTask.Dispose();
                     }
                     if ((!IsNextMovemntIsCheckOrCheckMateForCurrent) && (!SelfNotSupported) && (!IsPrviousMovemntIsDangrousForCurr) && (!IsGardForCurrentMovmentsAndIsNotMova) && (!IsNotSafeToMoveAenemeyToAttackMoreThanTowObj) && (!EnemyNotSupported) && (!IsDangerous))
                     {
                         newTask = Task.Factory.StartNew(() => EnemyNotSupported = InAttackEnemyThatIsNotSupported(Killed, CloneATable(TableS), Order, color, i, j, ii, jj));
                         newTask.Wait();
                         newTask.Dispose();
                     }
                     if ((!IsNextMovemntIsCheckOrCheckMateForCurrent) && (!SelfNotSupported) && (!IsPrviousMovemntIsDangrousForCurr) && (!IsGardForCurrentMovmentsAndIsNotMova) && (!IsNotSafeToMoveAenemeyToAttackMoreThanTowObj) && (!EnemyNotSupported) && (!IsDangerous))
                     {
                         newTask = Task.Factory.StartNew(() => EnemyNotSupported = InAttackEnemyThatIsNotSupportedAll(IsTowValuableObjectEnemy, CloneATable(TableS), Order, color, ref ValuableEnemyNotSupported));
                         newTask.Wait();
                         newTask.Dispose();
                     }
                     if (CurrentAStarGredyMax == 0 && (!IsNextMovemntIsCheckOrCheckMateForCurrent) && (!SelfNotSupported) && (!IsPrviousMovemntIsDangrousForCurr) && (!IsGardForCurrentMovmentsAndIsNotMova) && (!IsNotSafeToMoveAenemeyToAttackMoreThanTowObj) && (!EnemyNotSupported) && (!IsDangerous))
                     {
                         //when is true must regard.
                         newTask = Task.Factory.StartNew(() => IsCurrentCanGardHighPriorityEne = IsCurrentCanGardHighPriorityEnemy(0, CloneATable(TableS), Order, color, i, j, ii, jj, Order));
                         newTask.Wait();
                         newTask.Dispose();
                     }
                     if (SelfNotSupported || IsNextMovemntIsCheckOrCheckMateForCurrent || IsPrviousMovemntIsDangrousForCurr || IsGardForCurrentMovmentsAndIsNotMova && IsDangerous)
                     {
                         IsCurrentCanGardHighPriorityEne = false;
                         EnemyNotSupported = false;
                         IsNextMovemntIsCheckOrCheckMateForEnemy = false;
                     }
                    object OO = new object();
                    lock (OO)
                    {
                        //if (IsNextMovemntIsCheckOrCheckMateForCurrent)
                        CanKillerAnUnSupportedEnemy = Support || EnemyNotSupported || IsCurrentCanGardHighPriorityEne || IsNextMovemntIsCheckOrCheckMateForEnemy || IsNextMovemntIsCheckOrCheckMateForCurrent;
                        P = IsNotSafeToMoveAenemeyToAttackMoreThanTowObj || IsGardForCurrentMovmentsAndIsNotMova || IsPrviousMovemntIsDangrousForCurr || SelfNotSupported || IsDangerous || IsCurrentCanGardHighPriorityEne || IsNextMovemntIsCheckOrCheckMateForEnemy || IsNextMovemntIsCheckOrCheckMateForCurrent;
                        R = CanKillerAnUnSupportedEnemy;
                        InDangrousUnSupported = P && (!R);
                        PDo = P & (!R);
                        //B+C
                        RDo = R && (!P);

                        LearningV[0] = IsCurrentCanGardHighPriorityEne;
                        LearningV[1] = IsNextMovemntIsCheckOrCheckMateForCurrent;
                        LearningV[2] = IsDangerous;
                        LearningV[3] = CanKillerAnUnSupportedEnemy;
                        LearningV[4] = InDangrousUnSupported;
                        LearningV[5] = Support;
                        LearningV[6] = IsNextMovemntIsCheckOrCheckMateForEnemy;
                        LearningV[7] = IsPrviousMovemntIsDangrousForCurr;
                        LearningV[8] = PDo;
                        LearningV[9] = RDo;
                        LearningV[10] = SelfNotSupported;
                        LearningV[11] = EnemyNotSupported;
                        LearningV[12] = IsGardForCurrentMovmentsAndIsNotMova;
                        LearningV[13] = IsNotSafeToMoveAenemeyToAttackMoreThanTowObj;
                    }
                }
                return LearningV;
            }
        }

        public void CastlesThinkingChess(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int DummyOrder, int DummyCurrentOrder, int[,] TableS, int RowSource, int ColumnSource, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, int RowDestination, int ColumnDestination, bool Castle
        )
        {
            object O22 = new object();
            lock (O22)
            {
                TableS = CloneATable(TableConst);
                double HeuristicAttackValue = new double();
                double HeuristicMovementValue = new double();
                double HeuristicSelfSupportedValue = new double();
                double HeuristicReducedMovementValue = new double();
                double HeuristicReducedSupport = new double();
                double HeuristicReducedAttackValue = new double();
                double HeuristicDistributionValue = new double();
                double HeuristicKingSafe = new double();
                double HeuristicFromCenter = new double();
                double HeuristicKingDangour = new double(); double HeuristicCheckedMate = new double();
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                ///When There is Movments.
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = ThinkingChessRuleThinking(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                    int CheckedM = 0; bool PenaltyVCar = false;
                    bool Sup = false;
                    Task newTask1 = Task.Factory.StartNew(() => SupMethod(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, ref Sup));
                    newTask1.Wait(); newTask1.Dispose();

                    if (!Sup)
                    {
                        ///Add Table to List of Private.
                        HitNumberCastle.Add(TableS[RowDestination, ColumnDestination]);
                        object OO = new object();
                        lock (OO)
                        {
                            ThinkingRun = true;
                        }
                    }
                    ///Predict Heuristic.
                    object A = new object();
                    lock (A)
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, true, Order, 0, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                    object A1 = new object();
                    lock (A1)
                    {
                        if (!Sup) { NumbersOfAllNode++; }
                    }
                    bool ac=false;
                    int Killed = 0;
                    newTask1 = Task.Factory.StartNew(() => ac=KilledMethod(ref Killed, Sup, RowSource, ColumnSource, RowDestination, ColumnDestination, ref TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!ac)
                    {
                        HitNumberCastle.RemoveAt(HitNumberCastle.Count - 1);
                        return;
                    }

                    //if (!Sup)
                    {
                        object A3 = new object();
                        lock (A3)
                        {
                            PenaltyVCar = false;
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                            newTask1 = Task.Factory.StartNew(() => PenaltyMechanisam(ref PenaltyVCar, ref TmpL, ref TmpW, ref CheckedM, Killed, Kind, CloneATable(TableS), RowSource, ColumnSource, ref Current, DoEnemySelf, PenRegStrore, RowDestination, ColumnDestination));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    ///Store of Indexes Changes and Table in specific List.
                    newTask1 = Task.Factory.StartNew(() => ObjectIndexes(Kind, Sup, RowDestination, ColumnDestination, TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    ///Wehn Predict of Operation Do operate a Predict of this movments.
                    object A5 = new object();
                    lock (A5)
                    {
                        //Caused this for Stachostic results.
                        if (!Sup)
                        {
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, false, Order, Killed, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    //Calculate Heuristic and Add to List and Cal Syntax.
                    if (!Sup)
                    {
                        string H = "";
                        object A6 = new object();
                        lock (A6)
                        {
                            AsS(RowSource, ColumnSource, RowDestination, ColumnDestination);
                            double[] Hu = new double[10];
                            //if (!(IsSup[j]))
                            {
                                //if (IgnoreFromCheckandMateHeuristic)

                                newTask1 = Task.Factory.StartNew(() => HuMethod(ref Hu, HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                                newTask1.Wait(); newTask1.Dispose();
                                H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                                HeuristicListCastle.Add(Hu);
                            }
                            object O4 = new object();
                            lock (O4)
                            {
                            }
                        }
                    }
                    else
                    {
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        double[] Hu = new double[10];
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(ref Hu));
                        newTask1.Wait(); newTask1.Dispose();

                        string H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                        newTask1 = Task.Factory.StartNew(() => HeuristicInsertion(Kind, RowDestination, ColumnDestination, CloneATable(TableS), Hu));
                        newTask1.Wait(); newTask1.Dispose();
                    }
                }
                else
                {
                    MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, 1, -1);
                }
            }
        }

        //specific determination for thinking main method
        public void HourseThinkingChess(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int DummyOrder, int DummyCurrentOrder, int[,] TableS, int RowSource, int ColumnSource, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, int RowDestination, int ColumnDestination, bool Castle)
        {
            object OO = new object();
            lock (OO)
            {
                TableS = CloneATable(TableConst);
                double HeuristicAttackValue = new double();
                double HeuristicMovementValue = new double();
                double HeuristicSelfSupportedValue = new double();
                double HeuristicReducedMovementValue = new double();
                double HeuristicReducedSupport = new double();
                double HeuristicReducedAttackValue = new double();
                double HeuristicDistributionValue = new double();
                double HeuristicKingSafe = new double();
                double HeuristicFromCenter = new double();
                double HeuristicKingDangour = new double(); double HeuristicCheckedMate = new double();
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                ///When There is Movments.
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = ThinkingChessRuleThinking(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                    int CheckedM = 0; bool PenaltyVCar = false;
                    bool Sup = false;
                    Task newTask1 = Task.Factory.StartNew(() => SupMethod(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, ref Sup));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!Sup)
                    {
                        ///Add Table to List of Private.
                        HitNumberHourse.Add(TableS[RowDestination, ColumnDestination]);
                        object O = new object();
                        lock (O)
                        {
                            ThinkingRun = true;
                        }
                    }
                    ///Predict Heuristic.
                    object A = new object();
                    lock (A)
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, true, Order, 0, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                    object A1 = new object();
                    lock (A1)
                    {
                        if (!Sup) { NumbersOfAllNode++; }
                    }
                    bool ac=false;
                    int Killed = 0;
                    newTask1 = Task.Factory.StartNew(() => ac=KilledMethod(ref Killed, Sup, RowSource, ColumnSource, RowDestination, ColumnDestination, ref TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!ac)
                    {
                        HitNumberHourse.RemoveAt(HitNumberHourse.Count - 1);
                        return;
                    }

                    // if (!Sup)
                    {
                        object A3 = new object();
                        lock (A3)
                        {
                            PenaltyVCar = false;
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                            newTask1 = Task.Factory.StartNew(() => PenaltyMechanisam(ref PenaltyVCar, ref TmpL, ref TmpW, ref CheckedM, Killed, Kind, CloneATable(TableS), RowSource, ColumnSource, ref Current, DoEnemySelf, PenRegStrore, RowDestination, ColumnDestination));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    ///Store of Indexes Changes and Table in specific List.
                    newTask1 = Task.Factory.StartNew(() => ObjectIndexes(Kind, Sup, RowDestination, ColumnDestination, TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    ///Wehn Predict of Operation Do operate a Predict of this movments.
                    object A5 = new object();
                    lock (A5)
                    {
                        //Caused this for Stachostic results.
                        if (!Sup)
                        {
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, false, Order, Killed, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    //Calculate Heuristic and Add to List and Cal Syntax.
                    if (!Sup)
                    {
                        string H = "";
                        object A6 = new object();
                        lock (A6)
                        {
                            AsS(RowSource, ColumnSource, RowDestination, ColumnDestination);
                            double[] Hu = new double[10];
                            //if (!(IsSup[j]))
                            {
                                //if (IgnoreFromCheckandMateHeuristic)

                                newTask1 = Task.Factory.StartNew(() => HuMethod(ref Hu, HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                                newTask1.Wait(); newTask1.Dispose();
                                H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                                HeuristicListHourse.Add(Hu);
                            }
                            object O4 = new object();
                            lock (O4)
                            {
                            }
                        }
                    }
                    else
                    {
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        double[] Hu = new double[10];
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(ref Hu));
                        newTask1.Wait(); newTask1.Dispose();

                        string H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                        newTask1 = Task.Factory.StartNew(() => HeuristicInsertion(Kind, RowDestination, ColumnDestination, CloneATable(TableS), Hu));
                        newTask1.Wait(); newTask1.Dispose();
                    }
                }
                else
                {
                    MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, 1, -1);
                }
            }
        }

        //specific determination for thinking main method
        //specific determination for thinking main method
        public void ElefantThinkingChess(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int DummyOrder, int DummyCurrentOrder, int[,] TableS, int RowSource, int ColumnSource, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, int RowDestination, int ColumnDestination, bool Castle)
        {
            object OO = new object();
            lock (OO)
            {
                TableS = CloneATable(TableConst);
                double HeuristicAttackValue = new double();
                double HeuristicMovementValue = new double();
                double HeuristicSelfSupportedValue = new double();
                double HeuristicReducedMovementValue = new double();
                double HeuristicReducedSupport = new double();
                double HeuristicReducedAttackValue = new double();
                double HeuristicDistributionValue = new double();
                double HeuristicKingSafe = new double();
                double HeuristicFromCenter = new double();
                double HeuristicKingDangour = new double(); double HeuristicCheckedMate = new double();
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                ///When There is Movments.
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = ThinkingChessRuleThinking(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                    int CheckedM = 0; bool PenaltyVCar = false;
                    bool Sup = false;
                    Task newTask1 = Task.Factory.StartNew(() => SupMethod(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, ref Sup));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!Sup)
                    {
                        ///Add Table to List of Private.
                        HitNumberElefant.Add(TableS[RowDestination, ColumnDestination]);
                        object O = new object();
                        lock (O)
                        {
                            ThinkingRun = true;
                        }
                    }
                    ///Predict Heuristic.
                    object A = new object();
                    lock (A)
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, true, Order, 0, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                    object A1 = new object();
                    lock (A1)
                    {
                        if (!Sup) { NumbersOfAllNode++; }
                    }
                    bool ac=false;
                    int Killed = 0;
                    newTask1 = Task.Factory.StartNew(() => ac=KilledMethod(ref Killed, Sup, RowSource, ColumnSource, RowDestination, ColumnDestination, ref TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!ac)
                    {
                        HitNumberElefant.RemoveAt(HitNumberElefant.Count - 1);
                        return;
                    }

                    //if (!Sup)
                    {
                        object A3 = new object();
                        lock (A3)
                        {
                            PenaltyVCar = false;
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                            newTask1 = Task.Factory.StartNew(() => PenaltyMechanisam(ref PenaltyVCar, ref TmpL, ref TmpW, ref CheckedM, Killed, Kind, CloneATable(TableS), RowSource, ColumnSource, ref Current, DoEnemySelf, PenRegStrore, RowDestination, ColumnDestination));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    ///Store of Indexes Changes and Table in specific List.
                    newTask1 = Task.Factory.StartNew(() => ObjectIndexes(Kind, Sup, RowDestination, ColumnDestination, TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    ///Wehn Predict of Operation Do operate a Predict of this movments.
                    object A5 = new object();
                    lock (A5)
                    {
                        //Caused this for Stachostic results.
                        if (!Sup)
                        {
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, false, Order, Killed, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    //Calculate Heuristic and Add to List and Cal Syntax.
                    if (!Sup)
                    {
                        string H = "";
                        object A6 = new object();
                        lock (A6)
                        {
                            AsS(RowSource, ColumnSource, RowDestination, ColumnDestination);

                            double[] Hu = new double[10];
                            //if (!(IsSup[j]))
                            {
                                //if (IgnoreFromCheckandMateHeuristic)

                                newTask1 = Task.Factory.StartNew(() => HuMethod(ref Hu, HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                                newTask1.Wait(); newTask1.Dispose();
                                H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                                HeuristicListElefant.Add(Hu);
                            }
                            object O4 = new object();
                            lock (O4)
                            {
                            }
                        }
                    }
                    else
                    {
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        double[] Hu = new double[10];
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(ref Hu));
                        newTask1.Wait(); newTask1.Dispose();

                        string H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                        newTask1 = Task.Factory.StartNew(() => HeuristicInsertion(Kind, RowDestination, ColumnDestination, CloneATable(TableS), Hu));
                        newTask1.Wait(); newTask1.Dispose();
                    }
                }
                else
                {
                    MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, 1, -1);
                }
            }
        }

        //healthy of lists in learning auatama
        private bool EqualitOne(LearningMachine.QuantumAtamata Current, int kind)
        {
            object O = new object();
            lock (O)
            {
                bool Equality = false;
                if (kind == 1 && Current.IsPenaltyAction() != 0 && UsePenaltyRegardMechnisamT && PenaltyRegardListSolder != null && PenaltyRegardListSolder.Count == TableListSolder.Count)
                {
                    Equality = true;
                }
                else
                    if (kind == 2 && Current.IsPenaltyAction() != 0 && UsePenaltyRegardMechnisamT && PenaltyRegardListElefant != null && PenaltyRegardListElefant.Count == TableListElefant.Count)
                {
                    Equality = true;
                }
                else
                        if (kind == 3 && Current.IsPenaltyAction() != 0 && UsePenaltyRegardMechnisamT && PenaltyRegardListHourse != null && PenaltyRegardListHourse.Count == TableListHourse.Count)
                {
                    Equality = true;
                }
                else
                if (kind == 4 && Current.IsPenaltyAction() != 0 && UsePenaltyRegardMechnisamT && PenaltyRegardListCastle != null && PenaltyRegardListCastle.Count == TableListCastle.Count)
                {
                    Equality = true;
                }
                else
                            if (kind == 5 && Current.IsPenaltyAction() != 0 && UsePenaltyRegardMechnisamT && PenaltyRegardListMinister != null && PenaltyRegardListMinister.Count == TableListMinister.Count)
                {
                    Equality = true;
                }
                else
                                     if (kind == 6 && Current.IsPenaltyAction() != 0 && UsePenaltyRegardMechnisamT && PenaltyRegardListKing != null && PenaltyRegardListKing.Count == TableListKing.Count)
                {
                    Equality = true;
                }
                else
                                     if ((kind == 7 || kind == -7) && Current.IsPenaltyAction() != 0 && UsePenaltyRegardMechnisamT && PenaltyRegardListCastling != null && PenaltyRegardListCastling.Count == TableListCastling.Count)
                {
                    Equality = true;
                }

                return Equality;
            }
        }

        //add list
        private void AddAtList(int kind, LearningMachine.QuantumAtamata Current)
        {
            object O = new object();
            lock (O)
            {
                //Adding Autamata Object to Specified List.
                if (kind == 1)
                {
                    //Soldier
                    PenaltyRegardListSolder.Add(Current);
                }
                else
                if (kind == 2)
                {
                    //Elefant
                    PenaltyRegardListElefant.Add(Current);
                }
                else
                    if (kind == 3)
                {
                    //Hourse
                    PenaltyRegardListHourse.Add(Current);
                }
                else
                        if (kind == 4)
                {
                    //Castles.
                    PenaltyRegardListCastle.Add(Current);
                }
                else
                            if (kind == 5)
                {
                    //Minister.
                    PenaltyRegardListMinister.Add(Current);
                }
                else
                                if (kind == 6)
                {
                    //King.
                    PenaltyRegardListKing.Add(Current);
                }
                else
                                if (kind == 7 || kind == -7)
                {
                    //King.
                    PenaltyRegardListCastling.Add(Current);
                }
            }
        }

        //remove list
        private void RemoveAtList(int kind)
        {
            object O = new object();
            lock (O)
            {
                //Remove Last Atutamata Object.
                if (kind == 1)
                {
                    //Soldier
                    PenaltyRegardListSolder.RemoveAt(PenaltyRegardListSolder.Count - 1);
                }
                else
                if (kind == 2)
                {
                    //Elefant
                    PenaltyRegardListElefant.RemoveAt(PenaltyRegardListElefant.Count - 1);
                }
                else
                    if (kind == 3)
                {
                    //Hourse
                    PenaltyRegardListHourse.RemoveAt(PenaltyRegardListHourse.Count - 1);
                }
                else
                        if (kind == 4)
                {
                    //Castles
                    PenaltyRegardListCastle.RemoveAt(PenaltyRegardListCastle.Count - 1);
                }
                else
                            if (kind == 5)
                {
                    //Minister
                    PenaltyRegardListMinister.RemoveAt(PenaltyRegardListMinister.Count - 1);
                }
                else
                                if (kind == 6)
                {
                    //King.
                    PenaltyRegardListKing.RemoveAt(PenaltyRegardListKing.Count - 1);
                }
                else
                                if (kind == 7 || kind == -7)
                {
                    //King.
                    PenaltyRegardListCastling.RemoveAt(PenaltyRegardListCastling.Count - 1);
                }
            }
        }

        //learning autamata maib method
        private void PenaltyMechanisam(ref bool RETURN, ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, ref int CheckedM, int Killed, int kind, int[,] TableS, int ii, int jj, ref LearningMachine.QuantumAtamata Current, bool DoEnemySelf, bool PenRegStrore, int i, int j)
        {
            object OO = new object();
            lock (OO)
            {
                RETURN = false;
                object O3 = new object();
                ChessRules AA = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TableS[ii, jj], CloneATable(TableS), Order);
                object O = new object();
                lock (O)
                {
                    if (!UsePenaltyRegardMechnisamT || (GoldenFinished))
                    {
                        RETURN = true;
                        AddAtList(kind, Current);
                    }
                    //Consideration to go to Check.
                    //if (!UsePenaltyRegardMechnisamT)
                    AA.CheckMate(CloneATable(TableS), Order);
                    {
                        if (AllDraw.OrderPlateDraw == 1 && AA.CheckMateBrown)
                        {
                            object A = new object();
                            lock (A)
                            {
                                IsThereMateOfEnemy.Add(true);
                                IsThereCheckOfSelf.Add(false);
                                IsThereCheckOfEnemy.Add(false);
                                IsThereMateOfSelf.Add(false);
                                KishEnemy.Add(true);
                                KishSelf.Add(false);
                                FoundFirstMating++;
                                if (Order == AllDraw.OrderPlateDraw)
                                {
                                    WinChiled.Add(2);
                                    LoseChiled.Add(0);
                                    WinOcuuredatChiled = 2;
                                }
                                if (!(!UsePenaltyRegardMechnisamT || (GoldenFinished)))
                                {
                                    Current.LearningAlgorithmRegard();
                                    RemoveAtList(kind);
                                    AddAtList(kind, Current);
                                }
                                CheckedM = 3;
                                RETURN = true; return;
                            }
                        }
                        if (AllDraw.OrderPlateDraw == -1 && AA.CheckMateGray)
                        {
                            DoEnemySelf = false;
                            object A = new object();
                            lock (A)
                            {
                                IsThereMateOfEnemy.Add(true);
                                IsThereCheckOfSelf.Add(false);
                                IsThereCheckOfEnemy.Add(false);
                                IsThereMateOfSelf.Add(false);
                                KishEnemy.Add(true);
                                KishSelf.Add(false);
                                FoundFirstMating++;
                                if (Order == AllDraw.OrderPlateDraw)
                                {
                                    WinChiled.Add(2);
                                    LoseChiled.Add(0);
                                    WinOcuuredatChiled = 2;
                                }
                                if (!(!UsePenaltyRegardMechnisamT || (GoldenFinished)))
                                {
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmRegard();
                                    AddAtList(kind, Current);
                                }
                                CheckedM = 3;
                                RETURN = true; return;
                            }
                        }
                        if (//(AllDraw.OrderPlateDraw == -1 && AA.CheckBrown)||
                            (AllDraw.OrderPlateDraw == -1 && AA.CheckMateBrown))
                        {
                            object A = new object();
                            lock (A)
                            {
                                IsThereMateOfEnemy.Add(false);
                                IsThereMateOfSelf.Add(true);
                                IsThereCheckOfSelf.Add(false);
                                IsThereCheckOfEnemy.Add(false);
                                KishEnemy.Add(false);
                                KishSelf.Add(true);
                                FoundFirstSelfMating++;
                                if (Order == AllDraw.OrderPlateDraw)
                                {
                                    WinChiled.Add(0);
                                    LoseChiled.Add(-2);
                                    LoseOcuuredatChiled[0] = -2;
                                }
                                if (!(!UsePenaltyRegardMechnisamT || (GoldenFinished)))
                                {
                                    Current.LearningAlgorithmPenalty();
                                    RemoveAtList(kind);
                                    AddAtList(kind, Current);
                                }
                                CheckedM = 3;
                                RETURN = true; return;
                            }
                        }
                        if (//(AllDraw.OrderPlateDraw == 1 && AA.CheckGray) ||
                            (AllDraw.OrderPlateDraw == 1 && AA.CheckMateGray))
                        {
                            DoEnemySelf = false;
                            object A = new object();
                            lock (A)
                            {
                                IsThereMateOfEnemy.Add(false);
                                IsThereMateOfSelf.Add(true);
                                IsThereCheckOfSelf.Add(false);
                                IsThereCheckOfEnemy.Add(false);
                                KishEnemy.Add(false);
                                KishSelf.Add(true);
                                FoundFirstSelfMating++;
                                if (Order == AllDraw.OrderPlateDraw)
                                {
                                    WinChiled.Add(0);
                                    LoseChiled.Add(-2);
                                    LoseOcuuredatChiled[0] = -2;
                                }
                                if (!(!UsePenaltyRegardMechnisamT || (GoldenFinished)))
                                {
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                CheckedM = 3;
                                RETURN = true; return;
                            }
                        }
                        /*  if (Order == 1 && AA.CheckMateBrown)
                          {
                              IsThereCheckOfEnemy.Add(true);
                              IsThereMateOfEnemy.Add(false);
                              IsThereMateOfSelf.Add(false);
                              IsThereCheckOfSelf.Add(false);
                              KishEnemy.Add(true);
                              KishSelf.Add(false);
                              DoEnemySelf = false;
                              EnemyCheckMateActionsString = true;
                              CheckedM = -2;
                          }
                          if (Order == -1 && AA.CheckMateGray)
                          {
                              IsThereCheckOfEnemy.Add(true);
                              IsThereMateOfEnemy.Add(false);
                              IsThereMateOfSelf.Add(false);
                              IsThereCheckOfSelf.Add(false);
                              KishEnemy.Add(true);
                              KishSelf.Add(false);
                              DoEnemySelf = false;
                              EnemyCheckMateActionsString = true;
                              CheckedM = -2;
                          }
                          if (Order == 1 && AA.CheckMateGray)
                          {
                              IsThereCheckOfEnemy.Add(false);
                              IsThereMateOfEnemy.Add(false);
                              IsThereMateOfSelf.Add(true);
                              IsThereCheckOfSelf.Add(false);
                              KishEnemy.Add(false);
                              KishSelf.Add(true);
                              EnemyCheckMateActionsString = false;
                              CheckedM = -2;
                          }
                          if (Order == -1 && AA.CheckMateBrown)
                          {
                              IsThereCheckOfEnemy.Add(false);
                              IsThereMateOfEnemy.Add(false);
                              IsThereMateOfSelf.Add(true);
                              IsThereCheckOfSelf.Add(false);
                              KishEnemy.Add(false);
                              KishSelf.Add(true);
                              EnemyCheckMateActionsString = false;
                              CheckedM = -2;
                          }
                        */
                        if (AllDraw.OrderPlateDraw == 1 && AA.CheckGray)
                        {
                            IsThereCheckOfEnemy.Add(false);
                            IsThereMateOfEnemy.Add(false);
                            IsThereMateOfSelf.Add(false);
                            IsThereCheckOfSelf.Add(false);
                            KishSelf.Add(true);
                            KishEnemy.Add(false);

                            object A = new object();
                            lock (A)
                            {
                                NumberOfPenalties++;
                            }
                            CheckedM = -1;
                        }
                        else
                            if (AllDraw.OrderPlateDraw == -1 && AA.CheckBrown)
                        {
                            IsThereCheckOfEnemy.Add(false);
                            IsThereMateOfEnemy.Add(false);
                            IsThereMateOfSelf.Add(false);
                            IsThereCheckOfSelf.Add(false);
                            KishSelf.Add(true);
                            KishEnemy.Add(false);
                            object A = new object();
                            lock (A)
                            {
                                NumberOfPenalties++;
                            }
                            CheckedM = -1;
                        }
                        if (AllDraw.OrderPlateDraw == 1 && AA.CheckBrown)
                        {
                            IsThereCheckOfEnemy.Add(false);
                            IsThereMateOfEnemy.Add(false);
                            IsThereMateOfSelf.Add(false);
                            IsThereCheckOfSelf.Add(false);
                            KishEnemy.Add(true);
                            KishSelf.Add(false);
                            object A = new object();
                            lock (A)
                            {
                                NumberOfPenalties++;
                            }
                            CheckedM = -1;
                        }
                        if (AllDraw.OrderPlateDraw == -1 && AA.CheckGray)
                        {
                            IsThereCheckOfEnemy.Add(false);
                            IsThereMateOfEnemy.Add(false);
                            IsThereMateOfSelf.Add(false);
                            IsThereCheckOfSelf.Add(false);
                            KishEnemy.Add(true);
                            KishSelf.Add(false);
                            object A = new object();
                            lock (A)
                            {
                                NumberOfPenalties++;
                            }
                            CheckedM = -1;
                        }
                        //if (FoundFirstSelfMating > 0)
                        {
                        }
                    }
                    if (CheckedM != 3)
                    {
                        WinChiled.Add(0);
                        LoseChiled.Add(0);
                    }
                    if (CheckedM != -2 && CheckedM != -1)
                    {
                        IsThereCheckOfEnemy.Add(false);
                        IsThereMateOfEnemy.Add(false);
                        IsThereMateOfSelf.Add(false);
                        IsThereCheckOfSelf.Add(false);
                        KishEnemy.Add(false);
                        KishSelf.Add(false);
                    }
                    if (RETURN)
                    {
                        return;
                    }

                    if (AllDraw.OrderPlateDraw != Order)
                    {
                        return;
                    }
                }
                //Initiate Local Variables.
                bool IsCurrentCanGardHighPriorityEne = new bool();
                bool IsNextMovemntIsCheckOrCheckMateForCurrent = new bool();
                bool IsNextMovemntIsCheckOrCheckMateForEnemy = new bool();
                bool IsDangerous = new bool();
                bool CanKillerAnUnSupportedEnemy = new bool();
                bool InDangrousUnSupported = new bool();
                bool Support = new bool();
                bool IsPrviousMovemntIsDangrousForCurr = new bool();
                bool PDo = new bool(), RDo = new bool();
                bool SelfNotSupported = new bool();
                bool EnemyNotSupported = new bool();
                bool IsGardForCurrentMovmentsAndIsNotMova = new bool();
                bool IsNotSafeToMoveAenemeyToAttackMoreThanTowObj = new bool();
                bool[] LearningV = null;
                //Mechanisam of Regrad.
                object O1 = new object();
                lock (O1)
                {
                    if (kind == 1 && PenRegStrore && UsePenaltyRegardMechnisamT && PenaltyRegardListSolder != null && PenaltyRegardListSolder.Count == TableListSolder.Count)
                    {
                        Task<bool[]> newTask2 = Task.Factory.StartNew(() => LearningV = CalculateLearningVars(Killed, CloneATable(TableS), ii, jj, i, j));
                        newTask2.Wait();
                        newTask2.Dispose();
                    }
                    else
                     if (kind == 2 && PenRegStrore && UsePenaltyRegardMechnisamT && PenaltyRegardListElefant != null && PenaltyRegardListElefant.Count == TableListElefant.Count)
                    {
                        Task<bool[]> newTask2 = Task.Factory.StartNew(() => LearningV = CalculateLearningVars(Killed, CloneATable(TableS), ii, jj, i, j));
                        newTask2.Wait();
                        newTask2.Dispose();
                    }
                    else
                        if (kind == 3 && PenRegStrore && UsePenaltyRegardMechnisamT && PenaltyRegardListHourse != null && PenaltyRegardListHourse.Count == TableListHourse.Count)
                    {
                        Task<bool[]> newTask2 = Task.Factory.StartNew(() => LearningV = CalculateLearningVars(Killed, CloneATable(TableS), ii, jj, i, j));
                        newTask2.Wait();
                        newTask2.Dispose();
                    }
                    else
                        if (kind == 4 && PenRegStrore && UsePenaltyRegardMechnisamT && PenaltyRegardListCastle != null && PenaltyRegardListCastle.Count == TableListCastle.Count)
                    {
                        Task<bool[]> newTask2 = Task.Factory.StartNew(() => LearningV = CalculateLearningVars(Killed, CloneATable(TableS), ii, jj, i, j));
                        newTask2.Wait();
                        newTask2.Dispose();
                    }
                    else
                            if (kind == 5 && PenRegStrore && UsePenaltyRegardMechnisamT && PenaltyRegardListMinister != null && PenaltyRegardListMinister.Count == TableListMinister.Count)
                    {
                        Task<bool[]> newTask2 = Task.Factory.StartNew(() => LearningV = CalculateLearningVars(Killed, CloneATable(TableS), ii, jj, i, j));
                        newTask2.Wait();
                        newTask2.Dispose();
                    }
                    else
                                if (kind == 6 && PenRegStrore && UsePenaltyRegardMechnisamT && PenaltyRegardListKing != null && PenaltyRegardListKing.Count == TableListKing.Count)
                    {
                        Task<bool[]> newTask2 = Task.Factory.StartNew(() => LearningV = CalculateLearningVars(Killed, CloneATable(TableS), ii, jj, i, j));
                        newTask2.Wait();
                        newTask2.Dispose();
                    }
                    else
                                if ((kind == 7 || Kind == -7) && PenRegStrore && UsePenaltyRegardMechnisamT && PenaltyRegardListCastling != null && PenaltyRegardListCastling.Count == TableListCastling.Count)
                    {
                        Task<bool[]> newTask2 = Task.Factory.StartNew(() => LearningV = CalculateLearningVars(Killed, CloneATable(TableS), ii, jj, i, j));
                        newTask2.Wait();
                        newTask2.Dispose();
                    }
                }
                object O2 = new object();
                lock (O2)
                {
                    IsCurrentCanGardHighPriorityEne = LearningV[0];
                    IsNextMovemntIsCheckOrCheckMateForCurrent = LearningV[1];
                    IsDangerous = LearningV[2];
                    CanKillerAnUnSupportedEnemy = LearningV[3];
                    InDangrousUnSupported = LearningV[4];
                    Support = LearningV[5];
                    IsNextMovemntIsCheckOrCheckMateForEnemy = LearningV[6];
                    IsPrviousMovemntIsDangrousForCurr = LearningV[7];
                    PDo = LearningV[8];
                    RDo = LearningV[9];
                    SelfNotSupported = LearningV[10];
                    EnemyNotSupported = LearningV[11];
                    IsGardForCurrentMovmentsAndIsNotMova = LearningV[12];
                    IsNotSafeToMoveAenemeyToAttackMoreThanTowObj = LearningV[13];
                }
                //Consideration of Itterative Movments to ignore.
                //Operation of Penalty Regard Mechanisam on Check and mate speciffically.
                bool Equality = EqualitOne(Current, kind);
                object O4 = new object();
                lock (O4)
                {
                    if (Equality)
                    {
                        ChessRules A = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TableS[ii, jj], CloneATable(TableS), Order);
                        if (A.Check(CloneATable(TableS), Order))
                        {
                            if (Order == 1 && (A.CheckGray))
                            {
                                NumberOfPenalties++;
                                Current.LearningAlgorithmPenalty();
                            }
                            else
                                if (Order == -1 && (A.CheckBrown))
                            {
                                NumberOfPenalties++;
                                Current.LearningAlgorithmPenalty();
                            }
                            AddAtList(kind, Current);
                        }
                        else
                        {
                            if (IsCurrentStateIsDangreousForCurrentOrder(CloneATable(TableS), Order, i, j) && DoEnemySelf)
                            {
                                NumberOfPenalties++;
                                Current.LearningAlgorithmPenalty();
                                AddAtList(kind, Current);
                            }
                            else
                            {
                                AddAtList(kind, Current);
                            }
                        }
                        //When There is Penalty or Regard.To Side can not be equal.
                        if (PDo || RDo)
                        {
                            //Penalty.
                            if (PDo)
                            {
                                object OO1 = new object();
                                lock (OO1)
                                {
                                    for (int ik = 0; ik < System.Math.Abs(TableS[i, j]); ik++)
                                    {
                                        LearniningTable.LearningAlgorithmPenaltyNet(ii, jj);
                                    }
                                }
                                //When previous Move of Enemy goes to Dangoure Current Object.
                                if (IsPrviousMovemntIsDangrousForCurr && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                //For Not Suppored In Attacked.
                                if (SelfNotSupported && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                //When Current Move Dos,'t Supporte.
                                //For Ocuuring in Enemy CheckMate.
                                if (SelfNotSupported && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                if (IsGardForCurrentMovmentsAndIsNotMova && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                if (IsNotSafeToMoveAenemeyToAttackMoreThanTowObj && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                if (IsDangerous && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }

                                if (EnemyNotSupported && Current.IsPenaltyAction() != 0 && Current.IsRewardAction() != 1)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmRegard();
                                    AddAtList(kind, Current);
                                }
                            }
                            else if (RDo)
                            {
                                object OOO = new object();
                                lock (OOO)
                                {
                                    for (int ik = 0; ik < System.Math.Abs(TableS[i, j]); ik++)
                                    {
                                        LearniningTable.LearningAlgorithmRegardNet(ii, jj);
                                    }
                                }
                                if (SelfNotSupported && Current.IsPenaltyAction() != 0)
                                {
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                if (IsGardForCurrentMovmentsAndIsNotMova && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                if (IsNotSafeToMoveAenemeyToAttackMoreThanTowObj && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                if (IsDangerous && Current.IsPenaltyAction() != 0)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmPenalty();
                                    AddAtList(kind, Current);
                                }
                                if (EnemyNotSupported && Current.IsPenaltyAction() != 0 && Current.IsRewardAction() != 1)
                                {
                                    NumberOfPenalties++;
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmRegard();
                                    AddAtList(kind, Current);
                                }

                                if (IsCurrentCanGardHighPriorityEne && Current.IsPenaltyAction() != 0 && Current.IsRewardAction() != 1)
                                {
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmRegard();
                                    AddAtList(kind, Current);
                                }
                                //For Ocuuring Enemy Garding Objects.
                                if (Support && Current.IsPenaltyAction() != 0 && Current.IsRewardAction() != 1)
                                {
                                    RemoveAtList(kind);
                                    Current.LearningAlgorithmRegard();
                                    AddAtList(kind, Current);
                                }
                            }
                        }
                        else
                        {
                            //#pragma warning disable CS0219 // The variable 'Added' is assigned but its value is never used
#pragma warning disable CS0219 // The variable 'Added' is assigned but its value is never used
                            bool Added = false;
#pragma warning restore CS0219 // The variable 'Added' is assigned but its value is never used
                            //#pragma warning restore CS0219 // The variable 'Added' is assigned but its value is never used
                            object OO1 = new object();
                            lock (OO1)
                            {
                                for (int ik = 0; ik < System.Math.Abs(TableS[i, j]); ik++)
                                {
                                    LearniningTable.LearningAlgorithmRegardNet(ii, jj);
                                    LearniningTable.LearningAlgorithmPenaltyNet(ii, jj);
                                }
                            }
                            if (IsNextMovemntIsCheckOrCheckMateForCurrent && Current.IsPenaltyAction() != 0)
                            {
                                NumberOfPenalties++;
                                RemoveAtList(kind);
                                Current.LearningAlgorithmPenalty();
                                AddAtList(kind, Current);
                                Added = true;
                            }
                            if (SelfNotSupported && Current.IsPenaltyAction() != 0)
                            {
                                RemoveAtList(kind);
                                Current.LearningAlgorithmPenalty();
                                AddAtList(kind, Current);
                                Added = true;
                            }
                            if (IsGardForCurrentMovmentsAndIsNotMova && Current.IsPenaltyAction() != 0)
                            {
                                NumberOfPenalties++;
                                RemoveAtList(kind);
                                Current.LearningAlgorithmPenalty();
                                AddAtList(kind, Current);
                                Added = true;
                            }
                            if (IsNotSafeToMoveAenemeyToAttackMoreThanTowObj && Current.IsPenaltyAction() != 0)
                            {
                                NumberOfPenalties++;
                                RemoveAtList(kind);
                                Current.LearningAlgorithmPenalty();
                                AddAtList(kind, Current);
                                Added = true;
                            }
                            if (IsDangerous && Current.IsPenaltyAction() != 0)
                            {
                                NumberOfPenalties++;
                                RemoveAtList(kind);
                                Current.LearningAlgorithmPenalty();
                                AddAtList(kind, Current);
                                Added = true;
                            }
                            if (IsNextMovemntIsCheckOrCheckMateForEnemy && Current.IsPenaltyAction() != 0)
                            {
                                RemoveAtList(kind);
                                Current.LearningAlgorithmRegard();
                                AddAtList(kind, Current);
                                Added = true;
                            }
                            if (IsCurrentCanGardHighPriorityEne && Current.IsPenaltyAction() != 0)
                            {
                                RemoveAtList(kind);
                                Current.LearningAlgorithmRegard();
                                AddAtList(kind, Current);
                                Added = true;
                            }
                            if (EnemyNotSupported && Current.IsPenaltyAction() != 0 && Current.IsRewardAction() != 1)
                            {
                                NumberOfPenalties++;
                                RemoveAtList(kind);
                                Current.LearningAlgorithmRegard();
                                AddAtList(kind, Current);
                                Added = true;
                            }
                        }
                    }
                }
                return;
            }
        }

        private void SoldierConversion(ref ThingsConverter t, int RowSource, int ColumnSource, int RowDestination, int ColumnDestination, int[,] TableS)
        {
            object O = new object();
            lock (O)
            {
                t.ConvertOperation(RowSource, ColumnSource, CloneATable(TableS), Order);
                int[,] TableCon = new int[8, 8];
                if (t.Convert)
                {
                    TableS[RowSource, ColumnSource] = 0;
                    if (t.ConvertedToMinister)
                    {
                        TableS[RowDestination, ColumnDestination] = 5;
                    }
                    else if (t.ConvertedToCastle)
                    {
                        TableS[RowDestination, ColumnDestination] = 4;
                    }
                    else if (t.ConvertedToHourse)
                    {
                        TableS[RowDestination, ColumnDestination] = 3;
                    }
                    else if (t.ConvertedToElefant)
                    {
                        TableS[RowDestination, ColumnDestination] = 2;
                    }

                    if (Order == -1)
                    {
                        TableS[RowDestination, ColumnDestination] *= -1;
                    }
                }
            }
        }

        private int KilledBool(int row1, int col1, int row2, int col2, int[,] tab)
        {
            object O = new object();
            lock (O)
            {
                if (tab[row1, col1] != 0 && tab[row2, col2] != 0)
                {
                    if (tab[row2, col2] > 0)
                    {
                        return 1;
                    }

                    if (tab[row2, col2] < 0)
                    {
                        return -1;
                    }
                }
                return 0;
            }
        }

        //specific determination for thinking main method
        public void SupMethod(int[,] TableS, int RowSource, int ColumnSource, int RowDestination, int ColumnDestination, ref bool Sup)
        {
            object O = new object();
            lock (O)
            {
                if (TableS[RowDestination, ColumnDestination] > 0 && TableS[RowSource, ColumnSource] > 0)
                {
                    IsSup.Add(true);
                    IsSupHu.Add(true);
                    Sup = true;
                }
                else
                      if (TableS[RowDestination, ColumnDestination] < 0 && TableS[RowSource, ColumnSource] < 0)
                {
                    IsSup.Add(true);
                    IsSupHu.Add(true);
                    Sup = true;
                }
                else
                {
                    IsSup.Add(false);
                    IsSupHu.Add(false);
                    Sup = false;
                }
            }
        }

        private bool KilledMethod(ref int Killed, bool Sup, int RowSource, int ColumnSource, int RowDestination, int ColumnDestination, ref int[,] TableS, ThingsConverter t = null)
        {
            object O = new object();
            lock (O)
            {
                Killed = 0;
                if (!Sup)
                {
                    if (t != null)
                    {
                        if ((!t.Convert))
                        {
                            object A2 = new object();
                            lock (A2)
                            {
                                int[,] s = CloneATable(TableS);

                                MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, 1);
                                Killed = TableConst[RowDestination, ColumnDestination];
                                TableS[RowDestination, ColumnDestination] = TableS[RowSource, ColumnSource];
                                TableS[RowSource, ColumnSource] = 0;

                                ChessRules c = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TableS[RowSource, ColumnSource], CloneATable(TableS), Order));
                                if (c.Check(TableS, Order))
                                {
                                    if (Order == 1)
                                    {
                                        if (c.CheckGray)
                                        {
                                            TableS = s;
                                            return false;
                                        }

                                    }
                                    if (Order == -1)
                                    {
                                        if (c.CheckBrown)
                                        {
                                            TableS = s;
                                            return false;
                                        }

                                    }

                                }
                            }
                        }
                        else
                        {
                            int[,] s = CloneATable(TableS);

                            int con = 1;
                            if (t.ConvertedToMinister)
                            {
                                con = 5;
                            }
                            else
                            if (t.ConvertedToCastle)
                            {
                                con = 4;
                            }
                            else
                            if (t.ConvertedToHourse)
                            {
                                con = 3;
                            }
                            else
                            if (t.ConvertedToElefant)
                            {
                                con = 2;
                            }

                            MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, con);
                            Killed = TableConst[RowDestination, ColumnDestination];
                            TableS[RowDestination, ColumnDestination] = (Math.Abs(TableS[RowSource, ColumnSource]) / TableS[RowSource, ColumnSource]) * con;
                            TableS[RowSource, ColumnSource] = 0;

                            ChessRules c = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TableS[RowSource, ColumnSource], CloneATable(TableS), Order));
                            if (c.Check(TableS, Order))
                            {
                                if (Order == 1)
                                {
                                    if (c.CheckGray)
                                    {
                                        TableS = s;
                                        return false;
                                    }

                                }
                                if (Order == -1)
                                {
                                    if (c.CheckBrown)
                                    {
                                        TableS = s;
                                        return false;
                                    }

                                }

                            }
                        }
                    }
                    else
                    {
                        object A2 = new object();
                        lock (A2)
                        {
                            int[,] s = CloneATable(TableS);


                            MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, 1);
                            Killed = TableConst[RowDestination, ColumnDestination];
                            if (!(Kind == 7 || Kind == -7))
                            {
                                TableS[RowDestination, ColumnDestination] = TableS[RowSource, ColumnSource];
                                TableS[RowSource, ColumnSource] = 0;


                                ChessRules c = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TableS[RowSource, ColumnSource], CloneATable(TableS), Order));
                                if (c.Check(TableS, Order))
                                {
                                    if (Order == 1)
                                    {
                                        if (c.CheckGray)
                                        {
                                            TableS = s;
                                            return false;
                                        }

                                    }
                                    if (Order == -1)
                                    {
                                        if (c.CheckBrown)
                                        {
                                            TableS = s;
                                            return false;
                                        }

                                    }

                                }
                            }
                            else
                            {
                                
                                if (Kind == 7)
                                {
                                    if (RowDestination < RowSource)
                                    {
                                        TableS[RowSource - 1, ColumnDestination] = 4;
                                        TableS[RowSource - 2, ColumnDestination] = 6;
                                        TableS[RowSource - 4, ColumnDestination] = 0;
                                        TableS[RowSource, ColumnSource] = 0;
                                    }
                                    else
                                    {
                                        TableS[RowSource + 1, ColumnDestination] = 4;
                                        TableS[RowSource + 2, ColumnDestination] = 6;
                                        TableS[RowSource + 3, ColumnDestination] = 0;
                                        TableS[RowSource, ColumnSource] = 0;
                                    }
                                }
                                else
                                {
                                    if (RowDestination < RowSource)
                                    {
                                        TableS[RowSource - 1, ColumnDestination] = -4;
                                        TableS[RowSource - 2, ColumnDestination] = -6;
                                        TableS[RowSource - 4, ColumnDestination] = 0;
                                        TableS[RowSource, ColumnSource] = 0;
                                    }
                                    else
                                    {
                                        TableS[RowSource + 1, ColumnDestination] = -4;
                                        TableS[RowSource + 2, ColumnDestination] = -6;
                                        TableS[RowSource + 3, ColumnDestination] = 0;
                                        TableS[RowSource, ColumnSource] = 0;
                                    }
                                }
                                ChessRules c = (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TableS[RowSource, ColumnSource], CloneATable(TableS), Order));
                                if (c.Check(TableS, Order))
                                {
                                    if (Order == 1)
                                    {
                                        if (c.CheckGray)
                                        {
                                            TableS = s;
                                            return false;
                                        }

                                    }
                                    if (Order == -1)
                                    {
                                        if (c.CheckBrown)
                                        {
                                            TableS = s;
                                            return false;
                                        }

                                    }

                                }
                            }
                        }
                    }
                }
                KillerAtThinking.Add(KilledBool(RowSource, ColumnSource, RowDestination, ColumnDestination, TableS));
                return true;
            }
        }

        private void ObjectIndexes(int Kind, bool Sup, int RowDestination, int ColumnDestination, int[,] TableS)
        {
            object O = new object();
            lock (O)
            {
                if (!Sup)
                {
                    if (Kind == 1)
                    {
                        object A4 = new object();
                        lock (A4)
                        {
                            int[] AS = new int[2];
                            AS[0] = RowDestination;
                            AS[1] = ColumnDestination;
                            RowColumnSoldier.Add(AS);

                            TableListSolder.Add(CloneATable(TableS));
                            IndexSoldier++;
                        }
                    }
                    else
                    if (Kind == 2)
                    {
                        object A4 = new object();
                        lock (A4)
                        {
                            int[] AS = new int[2];
                            AS[0] = RowDestination;
                            AS[1] = ColumnDestination;
                            RowColumnElefant.Add(AS);

                            TableListElefant.Add(CloneATable(TableS));
                            IndexElefant++;
                        }
                    }
                    else
                    if (Kind == 3)
                    {
                        object A4 = new object();
                        lock (A4)
                        {
                            int[] AS = new int[2];
                            AS[0] = RowDestination;
                            AS[1] = ColumnDestination;
                            RowColumnHourse.Add(AS);

                            TableListHourse.Add(CloneATable(TableS));
                            IndexHourse++;
                        }
                    }
                    else
                    if (Kind == 4)
                    {
                        object A4 = new object();
                        lock (A4)
                        {
                            int[] AS = new int[2];
                            AS[0] = RowDestination;
                            AS[1] = ColumnDestination;
                            RowColumnCastle.Add(AS);

                            TableListCastle.Add(CloneATable(TableS));
                            IndexCastle++;
                        }
                    }
                    if (Kind == 5)
                    {
                        object A4 = new object();
                        lock (A4)
                        {
                            int[] AS = new int[2];
                            AS[0] = RowDestination;
                            AS[1] = ColumnDestination;
                            RowColumnMinister.Add(AS);

                            TableListMinister.Add(CloneATable(TableS));
                            IndexMinister++;
                        }
                    }
                    else
                    if (Kind == 6)
                    {
                        object A4 = new object();
                        lock (A4)
                        {
                            int[] AS = new int[2];
                            AS[0] = RowDestination;
                            AS[1] = ColumnDestination;
                            RowColumnKing.Add(AS);

                            TableListKing.Add(CloneATable(TableS));
                            IndexKing++;
                        }
                    }
                    else
                    if (Kind == 7 || Kind == -7)
                    {
                        object A4 = new object();
                        lock (A4)
                        {
                            int[] AS = new int[2];
                            AS[0] = RowDestination;
                            AS[1] = ColumnDestination;
                            RowColumnCastling.Add(AS);

                            TableListCastling.Add(CloneATable(TableS));
                            IndexKing++;
                        }
                    }
                }
            }
        }

        private void HeuristicInsertion(int Kind, int RowDestination, int ColumnDestination, int[,] TableS, double[] Hu)
        {
            object A4 = new object();
            lock (A4)
            {
                if (Kind == 1)
                {
                    int[] AS = new int[2];
                    AS[0] = RowDestination;
                    AS[1] = ColumnDestination;
                    RowColumnSoldier.Add(AS);

                    TableListSolder.Add(CloneATable(TableS));
                    IndexSoldier++;
                    HeuristicListSolder.Add(Hu);
                    HitNumberSoldier.Add(TableS[RowDestination, ColumnDestination]);
                }
                else
                if (Kind == 2)
                {
                    int[] AS = new int[2];
                    AS[0] = RowDestination;
                    AS[1] = ColumnDestination;
                    RowColumnElefant.Add(AS);

                    TableListElefant.Add(CloneATable(TableS));
                    IndexElefant++;
                    HeuristicListElefant.Add(Hu);
                    HitNumberElefant.Add(TableS[RowDestination, ColumnDestination]);
                }
                else
                if (Kind == 3)
                {
                    int[] AS = new int[2];
                    AS[0] = RowDestination;
                    AS[1] = ColumnDestination;
                    RowColumnHourse.Add(AS);

                    TableListHourse.Add(CloneATable(TableS));
                    IndexHourse++;
                    HeuristicListHourse.Add(Hu);
                    HitNumberHourse.Add(TableS[RowDestination, ColumnDestination]);
                }
                else
                if (Kind == 4)
                {
                    int[] AS = new int[2];
                    AS[0] = RowDestination;
                    AS[1] = ColumnDestination;
                    RowColumnCastle.Add(AS);

                    TableListCastle.Add(CloneATable(TableS));
                    IndexCastle++;
                    HeuristicListCastle.Add(Hu);
                    HitNumberCastle.Add(TableS[RowDestination, ColumnDestination]);
                }
                else
                if (Kind == 5)
                {
                    int[] AS = new int[2];
                    AS[0] = RowDestination;
                    AS[1] = ColumnDestination;
                    RowColumnMinister.Add(AS);

                    TableListMinister.Add(CloneATable(TableS));
                    IndexSoldier++;
                    HeuristicListMinister.Add(Hu);
                    HitNumberMinister.Add(TableS[RowDestination, ColumnDestination]);
                }
                else
                if (Kind == 6)
                {
                    int[] AS = new int[2];
                    AS[0] = RowDestination;
                    AS[1] = ColumnDestination;
                    RowColumnKing.Add(AS);

                    TableListKing.Add(CloneATable(TableS));
                    IndexKing++;
                    HeuristicListKing.Add(Hu);
                    HitNumberKing.Add(TableS[RowDestination, ColumnDestination]);
                }
                else
                if (Kind == 7 || Kind == -7)
                {
                    int[] AS = new int[2];
                    AS[0] = RowDestination;
                    AS[1] = ColumnDestination;
                    RowColumnCastling.Add(AS);

                    TableListCastling.Add(CloneATable(TableS));
                    IndexCastling++;
                    HeuristicListCastling.Add(Hu);
                    HitNumberCastling.Add(TableS[RowDestination, ColumnDestination]);
                }
            }
        }

        private bool ThinkingChessRuleThinking(int[,] TableS, int RowSource, int ColumnSource, int RowDestination, int ColumnDestination)
        {
            object O = new object();
            lock (O)
            {
                return (new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TableS[RowSource, ColumnSource], CloneATable(TableS), Order)).Rules(RowSource, ColumnSource, RowDestination, ColumnDestination, TableS[RowSource, ColumnSource], false);
            }
        }

        public void SolderThinkingChess(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int DummyOrder, int DummyCurrentOrder, int[,] TableS, int RowSource, int ColumnSource, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, int RowDestination, int ColumnDestination, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                TableS = CloneATable(TableConst);
                double HeuristicAttackValue = new double();
                double HeuristicMovementValue = new double();
                double HeuristicSelfSupportedValue = new double();
                double HeuristicReducedMovementValue = new double();
                double HeuristicReducedSupport = new double();
                double HeuristicReducedAttackValue = new double();
                double HeuristicDistributionValue = new double();
                double HeuristicKingSafe = new double();
                double HeuristicFromCenter = new double();
                double HeuristicKingDangour = new double(); double HeuristicCheckedMate = new double();
                Order = DummyOrder;
                ChessRules.CurrentOrder = DummyCurrentOrder;
                ///When There is Movments.
                bool ab = false;
                Task<bool> th = Task.Factory.StartNew(() => ab = ThinkingChessRuleThinking(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination));
                th.Wait();
                th.Dispose();
                if (ab)
                {
                    ThingsConverter t = new ThingsConverter(ArrangmentsChanged, RowSource, ColumnSource, Order);
                    LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                    int CheckedM = 0; bool PenaltyVCar = false;
                    bool Sup = false;
                    Task newTask1 = Task.Factory.StartNew(() => SupMethod(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, ref Sup));
                    newTask1.Wait(); newTask1.Dispose();

                    if (!Sup)
                    {
                        newTask1 = Task.Factory.StartNew(() => SoldierConversion(ref t, RowSource, ColumnSource, RowDestination, ColumnDestination, TableS));
                        newTask1.Wait(); newTask1.Dispose();
                        ///Add Table to List of Private.
                        HitNumberSoldier.Add(TableS[RowDestination, ColumnDestination]);
                        object O = new object();
                        lock (O)
                        {
                            ThinkingRun = true;
                        }
                    }
                    ///Predict Heuristic.
                    object A = new object();
                    lock (A)
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, true, Order, 0, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                    object A1 = new object();
                    lock (A1)
                    {
                        if (!Sup) { NumbersOfAllNode++; }
                    }
                    bool ac=false;
                    int Killed = 0;
                    newTask1 = Task.Factory.StartNew(() => ac=KilledMethod(ref Killed, Sup, RowSource, ColumnSource, RowDestination, ColumnDestination, ref TableS, t));
                    newTask1.Wait(); newTask1.Dispose();
                    if (!ac)
                    {
                        HitNumberSoldier.RemoveAt(HitNumberSoldier.Count - 1);
                        return;
                    }

                    //if (!Sup)
                    {
                        object A3 = new object();
                        lock (A3)
                        {
                            PenaltyVCar = false;
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                            newTask1 = Task.Factory.StartNew(() => PenaltyMechanisam(ref PenaltyVCar, ref TmpL, ref TmpW, ref CheckedM, Killed, Kind, CloneATable(TableS), RowSource, ColumnSource, ref Current, DoEnemySelf, PenRegStrore, RowDestination, ColumnDestination));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    ///Store of Indexes Changes and Table in specific List.
                    newTask1 = Task.Factory.StartNew(() => ObjectIndexes(Kind, Sup, RowDestination, ColumnDestination, TableS));
                    newTask1.Wait(); newTask1.Dispose();
                    ///Wehn Predict of Operation Do operate a Predict of this movments.
                    object A5 = new object();
                    lock (A5)
                    {
                        //Caused this for Stachostic results.
                        if (!Sup)
                        {
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, false, Order, Killed, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                            newTask1.Wait(); newTask1.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                    //Calculate Heuristic and Add to List and Cal Syntax.
                    if (!Sup)
                    {
                        string H = "";
                        object A6 = new object();
                        lock (A6)
                        {
                            AsS(RowSource, ColumnSource, RowDestination, ColumnDestination);
                            double[] Hu = new double[10];
                            //if (!(IsSup[j]))
                            {
                                //if (IgnoreFromCheckandMateHeuristic)

                                newTask1 = Task.Factory.StartNew(() => HuMethod(ref Hu, HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                                newTask1.Wait(); newTask1.Dispose();
                                H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                                HeuristicListSolder.Add(Hu);
                            }
                        }
                        object O4 = new object();
                        lock (O4)
                        {
                        }
                    }
                    else
                    {
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        double[] Hu = new double[10];
                        newTask1 = Task.Factory.StartNew(() => HuMethodSup(ref Hu));
                        newTask1.Wait(); newTask1.Dispose();

                        string H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                        newTask1 = Task.Factory.StartNew(() => HeuristicInsertion(Kind, RowDestination, ColumnDestination, CloneATable(TableS), Hu));
                        newTask1.Wait(); newTask1.Dispose();
                    }
                }
                else
                {
                    MovableAllObjectsListMethos(CloneATable(TableS), true, RowSource, ColumnSource, RowDestination, ColumnDestination, 1, -1);
                }
            }
        }

        //specific determination for thinking main method
        public void CastleThinkingBrown(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int[,] TableS, int RowSource, int ColumnSource, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, int RowDestination, int ColumnDestination, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                TableS = CloneATable(TableConst);
                double HeuristicAttackValue = new double();
                double HeuristicMovementValue = new double();
                double HeuristicSelfSupportedValue = new double();
                double HeuristicReducedMovementValue = new double();
                double HeuristicReducedSupport = new double();
                double HeuristicReducedAttackValue = new double();
                double HeuristicDistributionValue = new double();
                double HeuristicKingSafe = new double();
                double HeuristicFromCenter = new double();
                double HeuristicKingDangour = new double(); double HeuristicCheckedMate = new double();
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                int CheckedM = 0; bool PenaltyVCar = false;
                bool Sup = false;
                Task newTask1 = Task.Factory.StartNew(() => SupMethod(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, ref Sup));
                newTask1.Wait(); newTask1.Dispose();
                if (!Sup)
                {
                    ///Add Table to List of Private.
                    HitNumberCastling.Add(TableS[RowDestination, ColumnDestination]);
                    object OO = new object();
                    lock (OO)
                    {
                        ThinkingRun = true;
                    }
                }
                ///Predict Heuristic.
                object A = new object();
                lock (A)
                {
                    int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, true, Order, 0, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                    newTask1.Wait(); newTask1.Dispose();
                    LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                }
                object A1 = new object();
                lock (A1)
                {
                    if (!Sup) { NumbersOfAllNode++; }
                }
                bool ac=false;
                int Killed = 0;
                newTask1 = Task.Factory.StartNew(() => ac=KilledMethod(ref Killed, Sup, RowSource, ColumnSource, RowDestination, ColumnDestination, ref TableS));
                newTask1.Wait(); newTask1.Dispose();
                if (!ac)
                {
                    HitNumberCastling.RemoveAt(HitNumberCastling.Count - 1);
                    return;
                }

                // if (!Sup)
                {
                    object A3 = new object();
                    lock (A3)
                    {
                        PenaltyVCar = false;
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        newTask1 = Task.Factory.StartNew(() => PenaltyMechanisam(ref PenaltyVCar, ref TmpL, ref TmpW, ref CheckedM, Killed, Kind, CloneATable(TableS), RowSource, ColumnSource, ref Current, DoEnemySelf, PenRegStrore, RowDestination, ColumnDestination));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
                ///Store of Indexes Changes and Table in specific List.
                newTask1 = Task.Factory.StartNew(() => ObjectIndexes(Kind, Sup, RowDestination, ColumnDestination, TableS));
                newTask1.Wait(); newTask1.Dispose();
                ///Wehn Predict of Operation Do operate a Predict of this movments.
                object A5 = new object();
                lock (A5)
                {
                    //Caused this for Stachostic results.
                    if (!Sup)
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, false, Order, Killed, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
                //Calculate Heuristic and Add to List and Cal Syntax.
                if (!Sup)
                {
                    string H = "";
                    object A6 = new object();
                    lock (A6)
                    {
                        AsS(RowSource, ColumnSource, RowDestination, ColumnDestination);
                        double[] Hu = new double[10];
                        //if (!(IsSup[j]))
                        {
                            //if (IgnoreFromCheckandMateHeuristic)

                            newTask1 = Task.Factory.StartNew(() => HuMethod(ref Hu, HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                            newTask1.Wait(); newTask1.Dispose();
                            H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                            HeuristicListCastling.Add(Hu);
                        }
                    }
                    object O4 = new object();
                    lock (O4)
                    {
                    }
                }
                else
                {
                    newTask1 = Task.Factory.StartNew(() => HuMethodSup(HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                    newTask1.Wait(); newTask1.Dispose();
                    double[] Hu = new double[10];
                    newTask1 = Task.Factory.StartNew(() => HuMethodSup(ref Hu));
                    newTask1.Wait(); newTask1.Dispose();

                    string H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();

                    newTask1 = Task.Factory.StartNew(() => HeuristicInsertion(Kind, RowDestination, ColumnDestination, CloneATable(TableS), Hu));
                    newTask1.Wait(); newTask1.Dispose();
                }
            }
        }

        private double HeuristicBetterSpace(ref double HA, int[,] TableSS, Color colorS, Color colorE, int OrderS, int OrderE)
        {
            object OO = new object();
            lock (OO)
            {
                ////double HA =1;
                int SpaceSelf = 0, SpaceEnemy = 0;
                for (int RowS = 0; RowS < 8; RowS++)
                {
                    for (int ColS = 0; ColS < 8; ColS++)
                    {
                        for (int RowD = 0; RowD < 8; RowD++)
                        {
                            for (int ColD = 0; ColD < 8; ColD++)
                            {
                                if ((Order == 1 && TableSS[RowS, ColS] > 0) || (Order == -1 && TableSS[RowS, ColS] < 0))
                                {
                                    if (Attack(CloneATable(TableSS), RowS, ColS, RowD, ColD, colorS, OrderS))
                                    {
                                        SpaceSelf++;
                                    }
                                }
                                if ((Order == 1 && TableSS[RowD, ColD] < 0) || (Order == -1 && TableSS[RowD, ColD] > 0))
                                {
                                    if (Attack(CloneATable(TableSS), RowD, ColD, RowS, ColS, colorE, OrderE))
                                    {
                                        SpaceEnemy++;
                                    }
                                }
                            }
                        }
                    }
                }
                if (SpaceSelf > SpaceEnemy)
                {
                    HA += RationalRegard;
                }
                else
                    if (SpaceSelf < SpaceEnemy)
                {
                    HA += RationalPenalty;
                }

                return HA;
            }
        }

        public double[] CalculateHeuristicsParallel(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
      )
        {
            object OO = new object();
            lock (OO)
            {
                double HA = HS;
                double[] Heuristic = null;
                double[] Exchange = new double[3];
                double[] HeuristicRemain = new double[6];
                Task output = Task.Factory.StartNew(() =>
                {
                    //if (!feedCancellationTokenSource.IsCancellationRequested)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.Invoke(() =>
                        {
                            object O = new object();
                            lock (O)
                            {
                                Heuristic = CalculateHeuristicsParallelHeuristicAll(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color);
                            }
                        }, () =>
                        {
                            object O = new object();
                            lock (O)
                            {
                                Exchange = CalculateHeuristicsParallelExchange(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color);
                            }
                        });
                    }
                });

                output.Wait(); output.Dispose();
                Task output1 = Task.Factory.StartNew(() =>
                {
                    //if (!feedCancellationTokenSource.IsCancellationRequested)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.Invoke(() =>
                        {
                            object O = new object();
                            lock (O)
                            {
                                HeuristicRemain[0] = CalculateHeuristicsParallelHeuristicRemainZero(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color);
                            }
                        }, () =>
                        {
                            object O = new object();
                            lock (O)
                            {
                                HeuristicRemain[1] = CalculateHeuristicsParallelHeuristicRemainOne(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color);
                            }
                        }, () =>
                        {
                            object O = new object();
                            lock (O)
                            {
                                HeuristicRemain[2] = CalculateHeuristicsParallelRemainTow(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color);
                            }
                        }, () =>
                        {
                            object O = new object();
                            lock (O)
                            {
                                HeuristicRemain[3] = CalculateHeuristicsParallelHeuristicRemainThree(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color);
                            }
                        }, () =>
                        {
                            object O = new object();
                            lock (O)
                            {
                                HeuristicRemain[4] = CalculateHeuristicsParallelHeuristicRemainFour(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color);
                            }
                        }, () =>
                        {
                            object O = new object();
                            lock (O)
                            {
                                HeuristicRemain[5] = CalculateHeuristicsParallelHeuristicRemainFive(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color);
                            }
                        });
                    }
                });

                output1.Wait(); output1.Dispose();
                HA = HS;
                //Central control befor attack
                bool A = (Heuristic[1] > 0);
                bool B = (HeuristicRemain[4] > 0);
                if (A || (!B))
                {
                    Heuristic[1] = 0;
                }

                double[] hu = new double[15];
                for (int i = 0; i < 6; i++)
                {
                    hu[i] = Heuristic[i];
                }

                for (int i = 6; i < 12; i++)
                {
                    hu[i] = HeuristicRemain[i - 6];
                }

                for (int i = 12; i < 15; i++)
                {
                    hu[i] = Exchange[i - 12];
                }

                return hu;
            }
        }

        public double[] CalculateHeuristicsParallelHeuristicAll(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
    )
        {
            object OO = new object();
            lock (OO)
            {
                double HA = HS;
                double[] Heuristic = null;

                object O = new object();
                lock (O)
                {
                    if (!Scop(RowS, ColS, RowD, ColD, Kind))
                    {
                        return Heuristic;
                    }

                    int[,] TableSS = CloneATable(TableS);
                    int RoS = RowS, CoS = ColS, RoD = RowD, CoD = ColD;
                    Task<double[]> H = Task.Factory.StartNew(() => Heuristic = HeuristicAll(Killed, TableSS, Order));
                    H.Wait();
                    H.Dispose();
                }
                return Heuristic;
            }
        }

        public double[] CalculateHeuristicsParallelExchange(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
    )
        {
            object OO = new object();
            lock (OO)
            {
                double HA = HS;
                double[] Exchange = new double[3];

                object O = new object();
                lock (O)
                {
                    if (!Scop(RowS, ColS, RowD, ColD, Kind))
                    {
                        return Exchange;
                    }

                    int[,] TableSS = CloneATable(TableS);
                    int RoS = RowS, CoS = ColS, RoD = RowD, CoD = ColD;
                    Task<double[]> H = Task.Factory.StartNew(() => Exchange = HeuristicExchange(Before, Killed, TableSS, color, Order, RowS, ColS, RowD, ColD));
                    H.Wait();
                    H.Dispose();
                }


                return Exchange;
            }

        }

        public double CalculateHeuristicsParallelHeuristicRemainZero(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
    )
        {
            object OO = new object();
            lock (OO)
            {
                double HA = HS;
                double HeuristicRemain = 0;

                object O = new object();
                lock (O)
                {
                    //if (SubOfHeuristicAllIsPositive(Heuristic))
                    {
                        if (!Scop(RowS, ColS, RowD, ColD, Kind))
                        {
                            return 0;
                        }

                        int RoS = RowS, CoS = ColS, RoD = RowD, CoD = ColD;
                        int[,] TableSS = CloneATable(TableS);
                        Task<double> H = Task.Factory.StartNew(() => HeuristicRemain = HeuristicCheckAndCheckMate(ref HA, RoS, CoS, RoD, CoD, TableSS, color//, ref HeuristicReducedMovementValue
                        ));
                        H.Wait();
                        H.Dispose();
                    }
                }
                HS = HA;
                return HeuristicRemain;
            }
        }

        public double CalculateHeuristicsParallelHeuristicRemainOne(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
    )
        {
            object OO = new object();
            lock (OO)
            {
                double HA = HS;
                double HeuristicRemain = 0;

                object O = new object();
                lock (O)
                {
                    //if (SubOfHeuristicAllIsPositive(Heuristic))
                    {
                        if (!Scop(RowS, ColS, RowD, ColD, Kind))
                        {
                            return 0;
                        }

                        int RoS = RowS, CoS = ColS, RoD = RowD, CoD = ColD;
                        int[,] TableSS = CloneATable(TableS);
                        Task<double> H = Task.Factory.StartNew(() => HeuristicRemain = HeuristicDistribution(Before, TableSS, Order, color, RowS, ColS, RowD, ColD//, ref HeuristicDistributionValue
                             ));
                        H.Wait();
                        H.Dispose();
                    }
                }
                HS = HA;
                return HeuristicRemain;
            }
        }
        public double CalculateHeuristicsParallelRemainTow(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
  )
        {
            object OO = new object();
            lock (OO)
            {
                double HA = HS;
                double HeuristicRemain = 0;

                object O = new object();
                lock (O)
                {
                    if (!Scop(RowS, ColS, RowD, ColD, Kind))
                    {
                        return 0;
                    }

                    int RoS = RowS, CoS = ColS, RoD = RowD, CoD = ColD;
                    int[,] TableSS = CloneATable(TableS);
                    Task<double> H = Task.Factory.StartNew(() => HeuristicRemain = HeuristicKingSafety(ref HA, TableSS, Order, RoS, CoS//, ref HeuristicKingSafe
                         ));
                    H.Wait();
                    H.Dispose();
                }

                HS = HA;
                return HeuristicRemain;
            }
        }


        public double CalculateHeuristicsParallelHeuristicRemainThree(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
       )
        {
            object OO = new object();
            lock (OO)
            {
                double HA = HS;
                double HeuristicRemain = 0;

                object O = new object();
                lock (O)
                {
                    if (!Scop(RowS, ColS, RowD, ColD, Kind))
                    {
                        return 0;
                    }

                    int RoS = RowS, CoS = ColS, RoD = RowD, CoD = ColD;
                    int[,] TableSS = CloneATable(TableS);
                    Task<double> H = Task.Factory.StartNew(() => HeuristicRemain = HeuristicKingPreventionOfCheckedAtBegin(ref HA, TableSS, Order, CurrentAStarGredyMax, RoS, CoS, RoD, CoD//, ref HeuristicKingSafe
                    ));
                    H.Wait();
                    H.Dispose();
                }
                HS = HA;
                return HeuristicRemain;
            }
        }
        public double CalculateHeuristicsParallelHeuristicRemainFour(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
       )
        {
            object OO = new object();
            lock (OO)
            {
                double HA = HS;
                double HeuristicRemain = 0;

                object O = new object();
                lock (O)
                {
                    //if (SubOfHeuristicAllIsPositive(Heuristic))
                    {
                        if (!Scop(RowS, ColS, RowD, ColD, Kind))
                        {
                            return 0;
                        }

                        int RoS = RowS, CoS = ColS, RoD = RowD, CoD = ColD;
                        int[,] TableSS = CloneATable(TableS);
                        Task<double> H = Task.Factory.StartNew(() => HeuristicRemain = HeuristicObjectAtCenterAndPawnAttackTraversalObjectsAndDangourForEnemy(ref HA, TableSS, color, Order, RoS, CoS, RoD, CoD));
                        H.Wait();
                        H.Dispose();
                    }
                }
                HS = HA;
                return HeuristicRemain;
            }
        }
        public double CalculateHeuristicsParallelHeuristicRemainFive(ref double HS, bool Before, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
       )
        {
            object OO = new object();
            lock (OO)
            {

                double HA = HS;
                double HeuristicRemain = 0;

                object O = new object();
                lock (O)
                {
                    //if (SubOfHeuristicAllIsPositive(Heuristic))
                    {
                        if (!Scop(RowS, ColS, RowD, ColD, Kind))
                        {
                            return 0;
                        }

                        int RoS = RowS, CoS = ColS, RoD = RowD, CoD = ColD;
                        int[,] TableSS = CloneATable(TableS);
                        Color colorE = Color.Gray;
                        if (Order == -1)
                        {
                            colorE = Color.Gray;
                        }
                        else
                        {
                            colorE = Color.Brown;
                        }

                        Task<double> H = Task.Factory.StartNew(() => HeuristicRemain = HeuristicBetterSpace(ref HA, TableSS, color, colorE, Order, Order * -1));
                        H.Wait();
                        H.Dispose();
                    }
                }
                HS = HA;
                return HeuristicRemain;
            }
        }

        private void SetSupHuTrue()
        {
            object OO = new object();
            lock (OO)
            {
                IsSupHu[IsSupHu.Count - 1] = true;
            }
        }

        private void ClearSupHuTrue()
        {
            object OO = new object();
            lock (OO)
            {
                if (IsSup[IsSup.Count - 1] != true)
                {
                    IsSupHu[IsSupHu.Count - 1] = false;
                    IsSup[IsSup.Count - 1] = false;
                }
            }
        }

        private bool DisturbeOnHugeTraversalExchangePrevention(bool Before, int[,] TableS, int Order)
        {
            object OO = new object();
            lock (OO)
            {
                bool Is = false;
                if (!Before)
                {
                    if (HeuristicAllReducedAttackedMidel > 0 && HeuristicAllReducedAttackedMidel < HeuristicAllReducedAttacked.Count)
                    {
                        for (int i = HeuristicAllReducedAttackedMidel; i < HeuristicAllReducedAttacked.Count; i++)
                        {
                            if (Order == 1)
                            {
                                if (((System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]]) > System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]]))
                                //|| (System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]]) > 0 && NoOfExistInSupportList(Before, HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]) == 0)
                                ) && TableS[HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]] < 0)
                                {
                                    HeuristicReducedAttackedIndexInOnGame.Add(i);
                                    return true;
                                }
                            }
                            else
                            {
                                if (((System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]]) > System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]]))
                                //|| (System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]]) > 0 && NoOfExistInSupportList(Before, HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]) == 0)
                                ) && TableS[HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]] > 0)
                                {
                                    HeuristicReducedAttackedIndexInOnGame.Add(i);
                                    return true;
                                }
                            }
                        }
                    }
                }
                return Is;
            }
        }

        private bool DisturbeOnNonSupportedTraversalExchangePrevention(int Killded, bool Before, int[,] TableS, int Order)
        {
            object OO = new object();
            lock (OO)
            {
                bool Is = false;
                if (!Before)
                {
                    if (HeuristicAllReducedAttackedMidel > 0 && HeuristicAllReducedAttackedMidel < HeuristicAllReducedAttacked.Count)
                    {
                        for (int i = HeuristicAllReducedAttackedMidel; i < HeuristicAllReducedAttacked.Count; i++)
                        {
                            if (Order == 1)
                            {
                                List<int[]> Valuable = new List<int[]>();
                                bool DD = InAttackEnemyThatIsNotSupported(Killded, CloneATable(TableS), Order, OrderColor(Order), HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1], HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]);
                                if (DD || (System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]]) > System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]]) && TableS[HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]] < 0))
                                {
                                    HeuristicReducedAttackedIndexInOnGame.Add(i);
                                    return true;
                                }
                            }
                            else
                            {
                                List<int[]> Valuable = new List<int[]>();
                                bool DD = InAttackEnemyThatIsNotSupported(Killded, CloneATable(TableS), Order, OrderColor(Order), HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1], HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]);
                                if (DD || (System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][2], HeuristicAllReducedAttacked[i][3]]) > System.Math.Abs(TableS[HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]]) && TableS[HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]] > 0))
                                {
                                    HeuristicReducedAttackedIndexInOnGame.Add(i);
                                    return true;
                                }
                            }
                        }
                    }
                }
                return Is;
            }
        }

        //recursive of found achmaz detection to be tow objects at line of source attacked or reduced attack
        private int AchmazPuredBefore(bool Before, int[,] Table, int Level = 1)
        {
            object OO = new object();
            lock (OO)
            {
                if (!Before)
                {
                    return 0;
                }

                if (Level == 0)
                {
                    return 0;
                }

                int No = 0;
                if (Level == 1)
                {
                    if (Order == 1)
                    {
                        if (AchmazPure.Count > 0)
                        {
                            for (int i = 0; i < AchmazPure[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazPure[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazPure[0][i][j][0], AchmazPure[0][i][j][1]] > 0 && Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]] < 0))
                                    {
                                        ThinkingChess t = new ThinkingChess(0, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], OrderColor(Order), CloneATable(Table), 0, Order, false, 0, 0, Kind);
                                        if (Scop(AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]))
                                        {
                                            int Killed = Math.Abs(Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]]);
                                            Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]] = 0;
                                            Task<double[]> th1 = Task.Factory.StartNew(() => t.HeuristicExchange(Before, Killed, CloneATable(Tab), OrderColor(Order), Order, AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task th2 = Task.Factory.StartNew(() => t.Achmaz(CloneATable(Tab), Before, AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], AchmazPure[0][i][j][2], AchmazPure[0][i][j][3], Order));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<int> th3 = Task.Factory.StartNew(() => No += t.AchmazPuredBefore(Before, CloneATable(Tab), 2));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (AchmazPure.Count > 0)
                        {
                            for (int i = 0; i < AchmazPure[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazPure[0][i].Count; j++)
                                {
                                    if (AchmazPure[0][i].Count <= 1)
                                    {
                                        continue;
                                    }

                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazPure[0][i][j][0], AchmazPure[0][i][j][1]] < 0 && Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]] > 0))
                                    {
                                        ThinkingChess t = new ThinkingChess(0, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], OrderColor(Order), CloneATable(Table), 0, Order, false, 0, 0, Kind);
                                        if (Scop(AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]))
                                        {
                                            int Killed = Math.Abs(Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]]);
                                            Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]] = 0;
                                            Task<double[]> th1 = Task.Factory.StartNew(() => t.HeuristicExchange(Before, Killed, CloneATable(Tab), OrderColor(Order), Order, AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task th2 = Task.Factory.StartNew(() => t.Achmaz(CloneATable(Tab), Before, AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], AchmazPure[0][i][j][2], AchmazPure[0][i][j][3], Order));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<int> th3 = Task.Factory.StartNew(() => No += t.AchmazPuredBefore(Before, CloneATable(Tab), 2));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Level == 2)
                {
                    if (Order == 1)
                    {
                        if (AchmazPure.Count > 0)
                        {
                            for (int i = 0; i < AchmazPure[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazPure[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazPure[0][i][j][0], AchmazPure[0][i][j][1]] > 0 && Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]] < 0))
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (AchmazPure.Count > 0)
                        {
                            for (int i = 0; i < AchmazPure[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazPure[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazPure[0][i][j][0], AchmazPure[0][i][j][1]] < 0 && Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]] > 0))
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                }
                return No;
            }
        }

        //recursive of found achmaz detection to be tow objects at line of source attacked or reduced attack
        private int AchmazPuredAfter(bool Before, int[,] Table, int Level = 1)
        {
            object OO = new object();
            lock (OO)
            {
                if (Before)
                {
                    return 0;
                }

                if (Level == 0)
                {
                    return 0;
                }

                int No = 0;
                if (Level == 1)
                {
                    if (Order == 1)
                    {
                        if (AchmazPure.Count > 1)
                        {
                            for (int i = 0; i < AchmazPure[1].Count; i++)
                            {
                                for (int j = 0; j < AchmazPure[1][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazPure[1][i][j][0], AchmazPure[1][i][j][1]] > 0 && Tab[AchmazPure[1][i][j][2], AchmazPure[1][i][j][3]] < 0))
                                    {
                                        ThinkingChess t = new ThinkingChess(0, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, AchmazPure[1][i][j][0], AchmazPure[1][i][j][1], OrderColor(Order), CloneATable(Table), 0, Order, false, 0, 0, Kind);
                                        if (Scop(AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]))
                                        {
                                            int Killed = Math.Abs(Tab[AchmazPure[1][i][j][2], AchmazPure[1][i][j][3]]);
                                            Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]] = 0;
                                            Task<double[]> th1 = Task.Factory.StartNew(() => t.HeuristicExchange(Before, Killed, CloneATable(Tab), OrderColor(Order), Order, AchmazPure[1][i][j][0], AchmazPure[1][i][j][1], AchmazPure[1][i][j][2], AchmazPure[1][i][j][3]));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task th2 = Task.Factory.StartNew(() => t.Achmaz(CloneATable(Tab), Before, AchmazPure[1][i][j][0], AchmazPure[1][i][j][1], AchmazPure[1][i][j][2], AchmazPure[1][i][j][3], Order));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<int> th3 = Task.Factory.StartNew(() => No += t.AchmazPuredBefore(Before, CloneATable(Tab), 2));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (AchmazPure.Count > 1)
                        {
                            for (int i = 0; i < AchmazPure[1].Count; i++)
                            {
                                for (int j = 0; j < AchmazPure[1][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazPure[1][i][j][0], AchmazPure[1][i][j][1]] > 0 && Tab[AchmazPure[1][i][j][2], AchmazPure[1][i][j][3]] < 0))
                                    {
                                        ThinkingChess t = new ThinkingChess(0, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, AchmazPure[1][i][j][0], AchmazPure[1][i][j][1], OrderColor(Order), CloneATable(Table), 0, Order, false, 0, 0, Kind);
                                        if (Scop(AchmazPure[0][i][j][0], AchmazPure[0][i][j][1], AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]))
                                        {
                                            int Killed = Math.Abs(Tab[AchmazPure[1][i][j][2], AchmazPure[1][i][j][3]]);
                                            Tab[AchmazPure[0][i][j][2], AchmazPure[0][i][j][3]] = 0;
                                            Task<double[]> th1 = Task.Factory.StartNew(() => t.HeuristicExchange(Before, Killed, CloneATable(Tab), OrderColor(Order), Order, AchmazPure[1][i][j][0], AchmazPure[1][i][j][1], AchmazPure[1][i][j][2], AchmazPure[1][i][j][3]));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task th2 = Task.Factory.StartNew(() => t.Achmaz(CloneATable(Tab), Before, AchmazPure[1][i][j][0], AchmazPure[1][i][j][1], AchmazPure[1][i][j][2], AchmazPure[1][i][j][3], Order));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<int> th3 = Task.Factory.StartNew(() => No += t.AchmazPuredBefore(Before, CloneATable(Tab), 2));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Level == 2)
                {
                    if (Order == 1)
                    {
                        if (AchmazPure.Count > 0)
                        {
                            for (int i = 0; i < AchmazPure[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazPure[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazPure[1][i][j][0], AchmazPure[1][i][j][1]] > 0 && Tab[AchmazPure[1][i][j][2], AchmazPure[1][i][j][3]] < 0))
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (AchmazPure.Count > 0)
                        {
                            for (int i = 0; i < AchmazPure[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazPure[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazPure[1][i][j][0], AchmazPure[1][i][j][1]] < 0 && Tab[AchmazPure[1][i][j][2], AchmazPure[1][i][j][3]] > 0))
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                }
                return No;
            }
        }

        //recursive of found achmaz detection to be tow objects at line of source attacked or reduced attack
        private int AchmazReducedBefore(bool Before, int[,] Table, int Level = 1)
        {
            object OO = new object();
            lock (OO)
            {
                if (!Before)
                {
                    return 0;
                }

                if (Level == 0)
                {
                    return 0;
                }

                int No = 0;
                if (Level == 1)
                {
                    if (Order == 1)
                    {
                        if (AchmazReduced.Count > 0)
                        {
                            for (int i = 0; i < AchmazReduced[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazReduced[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1]] < 0 && Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]] > 0))
                                    {
                                        ThinkingChess t = new ThinkingChess(0, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1], OrderColor(Order), CloneATable(Table), 0, Order, false, 0, 0, Kind);
                                        if (Scop(AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3], AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1]))
                                        {
                                            int Killed = Math.Abs(Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]]);
                                            Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]] = 0;
                                            Task<double[]> th1 = Task.Factory.StartNew(() => t.HeuristicExchange(Before, Math.Abs(Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]]), CloneATable(Tab), OrderColor(Order * -1), Order * -1, AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1], AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task th2 = Task.Factory.StartNew(() => t.Achmaz(CloneATable(Tab), Before, AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1], AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3], Order));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<int> th3 = Task.Factory.StartNew(() => No += t.AchmazReducedBefore(Before, CloneATable(Tab), 2));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (AchmazReduced.Count > 0)
                        {
                            for (int i = 0; i < AchmazReduced[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazReduced[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1]] > 0 && Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]] < 0))
                                    {
                                        ThinkingChess t = new ThinkingChess(0, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1], OrderColor(Order), CloneATable(Table), 0, Order, false, 0, 0, Kind);
                                        if (Scop(AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3], AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1]))
                                        {
                                            int Killed = Math.Abs(Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]]);
                                            Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]] = 0;
                                            Task<double[]> th1 = Task.Factory.StartNew(() => t.HeuristicExchange(Before, Math.Abs(Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]]), CloneATable(Tab), OrderColor(Order * -1), Order * -1, AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1], AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task th2 = Task.Factory.StartNew(() => t.Achmaz(CloneATable(Tab), Before, AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1], AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3], Order));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<int> th3 = Task.Factory.StartNew(() => No += t.AchmazReducedBefore(Before, CloneATable(Tab), 2));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Level == 2)
                {
                    if (Order == 1)
                    {
                        if (AchmazReduced.Count > 0)
                        {
                            for (int i = 0; i < AchmazReduced[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazReduced[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]] < 0 && Tab[AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1]] > 0))
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (AchmazReduced.Count > 0)
                        {
                            for (int i = 0; i < AchmazReduced[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazReduced[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]] > 0 && Tab[AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1]] < 0))
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                }
                return No;
            }
        }

        //recursive of found achmaz detection to be tow objects at line of source attacked or reduced attack
        private int AchmazReducedAfter(bool Before, int[,] Table, int Level = 1)
        {
            object OO = new object();
            lock (OO)
            {
                if (Before)
                {
                    return 0;
                }

                if (Level == 0)
                {
                    return 0;
                }

                int No = 0;
                if (Level == 1)
                {
                    if (Order == 1)
                    {
                        if (AchmazReduced.Count > 1)
                        {
                            for (int i = 0; i < AchmazReduced[1].Count; i++)
                            {
                                for (int j = 0; j < AchmazReduced[1][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3]] < 0 && Tab[AchmazReduced[1][i][j][0], AchmazReduced[1][i][j][1]] > 0))
                                    {
                                        ThinkingChess t = new ThinkingChess(0, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3], OrderColor(Order), CloneATable(Table), 0, Order, false, 0, 0, Kind);
                                        if (Scop(AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3], AchmazReduced[1][i][j][0], AchmazReduced[1][i][j][1]))
                                        {
                                            int Killed = Math.Abs(Tab[AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3]]);
                                            Tab[AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3]] = 0;
                                            Task<double[]> th1 = Task.Factory.StartNew(() => t.HeuristicExchange(Before, Killed, CloneATable(Tab), OrderColor(Order * -1), Order * -1, AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1], AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task th2 = Task.Factory.StartNew(() => t.Achmaz(CloneATable(Tab), Before, AchmazReduced[1][i][j][0], AchmazReduced[1][i][j][1], AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3], Order * -1));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<int> th3 = Task.Factory.StartNew(() => No += t.AchmazReducedAfter(Before, CloneATable(Tab), 2));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (AchmazReduced.Count > 1)
                        {
                            for (int i = 0; i < AchmazReduced[1].Count; i++)
                            {
                                for (int j = 0; j < AchmazReduced[1][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3]] > 0 && Tab[AchmazReduced[1][i][j][0], AchmazReduced[1][i][j][1]] < 0))
                                    {
                                        ThinkingChess t = new ThinkingChess(0, Kind, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3], OrderColor(Order), CloneATable(Table), 0, Order, false, 0, 0, Kind);
                                        if (Scop(AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3], AchmazReduced[1][i][j][0], AchmazReduced[1][i][j][1]))
                                        {
                                            int Killed = Math.Abs(Tab[AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3]]);
                                            Tab[AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3]] = 0;
                                            Task<double[]> th1 = Task.Factory.StartNew(() => t.HeuristicExchange(Before, Killed, CloneATable(Tab), OrderColor(Order * -1), Order * -1, AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1], AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]));
                                            th1.Wait();
                                            th1.Dispose();
                                            Task th2 = Task.Factory.StartNew(() => t.Achmaz(CloneATable(Tab), Before, AchmazReduced[1][i][j][0], AchmazReduced[1][i][j][1], AchmazReduced[1][i][j][2], AchmazReduced[1][i][j][3], Order * -1));
                                            th2.Wait();
                                            th2.Dispose();
                                            Task<int> th3 = Task.Factory.StartNew(() => No += t.AchmazReducedAfter(Before, CloneATable(Tab), 2));
                                            th3.Wait();
                                            th3.Dispose();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Level == 2)
                {
                    if (Order == 1)
                    {
                        if (AchmazReduced.Count > 0)
                        {
                            for (int i = 0; i < AchmazReduced[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazReduced[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]] < 0 && Tab[AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1]] > 0))
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (AchmazReduced.Count > 0)
                        {
                            for (int i = 0; i < AchmazReduced[0].Count; i++)
                            {
                                for (int j = 0; j < AchmazReduced[0][i].Count; j++)
                                {
                                    int[,] Tab = CloneATable(Table);
                                    if ((Tab[AchmazReduced[0][i][j][2], AchmazReduced[0][i][j][3]] > 0 && Tab[AchmazReduced[0][i][j][0], AchmazReduced[0][i][j][1]] < 0))
                                    {
                                        return 1;
                                    }
                                }
                            }
                        }
                    }
                }
                return No;
            }
        }

        //method of list of reduced attack or attack by lists of method found lists by every specified objects on board.
        private List<List<int[]>> AchMazReducedElephasnt(int[,] Tabl, bool Before, int RowS, int ColS, int Order)
        {
            object OO = new object();
            lock (OO)
            {
                List<List<int[]>> Existence = new List<List<int[]>>();

                int ii = RowS, jj = ColS;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            if (Order == 1 && Tabl[i, j] != -2)
                            {
                                continue;
                            }

                            if (Order == -1 && Tabl[i, j] != 2)
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInReducedAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }
                //===============================

                return Existence;
            }
        }

        //method of list of reduced attack or attack by lists of method found lists by every specified objects on board.
        private List<List<int[]>> AchMazReducedCastle(int[,] Tabl, bool Before, int RowS, int ColS, int Order)
        {
            object OOk = new object();
            lock (OOk)
            {
                List<List<int[]>> Existence = new List<List<int[]>>();

                int ii = RowS, jj = ColS;

                object O1 = new object();
                lock (O1)
                {
                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, i =>
                    for (int i = 0; i < 8; i++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int j = jj;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            if (Order == 1 && Tabl[i, j] != -4)
                            {
                                continue;
                            }

                            if (Order == -1 && Tabl[i, j] != 4)
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInReducedAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }
                //===============================

                object OO = new object();
                lock (OO)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int i = ii;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            if (Order == 1 && Tabl[i, j] != -4)
                            {
                                continue;
                            }

                            if (Order == -1 && Tabl[i, j] != 4)
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInReducedAttackList(Before, i, j, RowS, ColS);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }

                return Existence;
            }
        }

        //method of list of reduced attack or attack by lists of method found lists by every specified objects on board.
        private List<List<int[]>> AchMazElephasnt(int[,] Tabl, bool Before, int RowS, int ColS, int Order)
        {
            object OO = new object();
            lock (OO)
            {
                List<List<int[]>> Existence = new List<List<int[]>>();

                int ii = RowS, jj = ColS;
                if (Order == 1 && Tabl[RowS, ColS] != 2)
                {
                    return Existence;
                }

                if (Order == -1 && Tabl[RowS, ColS] != -2)
                {
                    return Existence;
                }

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }
                //===============================
                return Existence;
            }
        }

        //method of list of reduced attack or attack by lists of method found lists by every specified objects on board.
        private List<List<int[]>> AchMazCastle(int[,] Tabl, bool Before, int RowS, int ColS, int Order)
        {
            object OOk = new object();
            lock (OOk)
            {
                List<List<int[]>> Existence = new List<List<int[]>>();

                int ii = RowS, jj = ColS;
                if (Order == 1 && Tabl[RowS, ColS] != 4)
                {
                    return Existence;
                }

                if (Order == -1 && Tabl[RowS, ColS] != -4)
                {
                    return Existence;
                }

                object O1 = new object();
                lock (O1)
                {
                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, i =>
                    for (int i = 0; i < 8; i++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int j = jj;

                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }
                //===============================
                object OO = new object();
                lock (OO)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int i = ii;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }

                return Existence;
            }
        }

        //method of list of reduced attack or attack by lists of method found lists by every specified objects on board.
        private List<List<int[]>> AchMazMinister(int[,] Tabl, bool Before, int RowS, int ColS, int Order)
        {
            object OO = new object();
            lock (OO)
            {
                List<List<int[]>> Existence = new List<List<int[]>>();

                int ii = RowS, jj = ColS;
                if (Order == 1 && Tabl[RowS, ColS] != 5)
                {
                    return Existence;
                }

                if (Order == -1 && Tabl[RowS, ColS] != -5)
                {
                    return Existence;
                }

                object O1 = new object();
                lock (O1)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            object O = new object();
                            lock (O)
                            {
                                if (!Scop(ii, jj, i, j))
                                {
                                    continue;
                                }

                                List<int[]> Exist = ListOfExistInAttackList(Before, RowS, ColS, i, j);
                                if (Exist.Count >= 1)
                                {
                                    Existence.Add(Exist);
                                }
                            }
                        }
                    }
                }

                return Existence;
            }
        }

        //method of list of reduced attack or attack by lists of method found lists by every specified objects on board.
        private List<List<int[]>> AchMazKing(int[,] Tabl, bool Before, int RowS, int ColS, int Order)
        {
            object OOk = new object();
            lock (OOk)
            {
                List<List<int[]>> Existence = new List<List<int[]>>();

                int ii = RowS, jj = ColS;
                if (Order == 1 && Tabl[RowS, ColS] != 6)
                {
                    return Existence;
                }

                if (Order == -1 && Tabl[RowS, ColS] != -6)
                {
                    return Existence;
                }

                object O1 = new object();
                lock (O1)
                {
                    for (int i = ii - 1; i < ii + 2; i++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int j = i + ii - jj;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                    //===============================
                    object OOOo1 = new object();
                    lock (OOOo1)
                    {
                        for (int i = ii - 1; i < ii + 2; i++)
                        {
                            object O = new object();
                            lock (O)
                            {
                                int j = i * -1 + ii - jj;
                                if (!Scop(ii, jj, i, j))
                                {
                                    continue;
                                }

                                List<int[]> Exist = ListOfExistInAttackList(Before, RowS, ColS, i, j);
                                if (Exist.Count >= 1)
                                {
                                    Existence.Add(Exist);
                                }
                            }
                        }
                    }
                    //=============================================
                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, i =>

                    for (int i = ii - 1; i < ii + 2; i++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int j = jj;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }
                //===============================
                object OO = new object();
                lock (OO)
                {
                    for (int j = ii - 1; j < ii + 2; j++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int i = ii;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }

                return Existence;
            }
        }

        //method of list of reduced attack or attack by lists of method found lists by every specified objects on board.
        private List<List<int[]>> AchMazReducedKing(int[,] Tabl, bool Before, int RowS, int ColS, int Order)
        {
            object OOk = new object();
            lock (OOk)
            {
                List<List<int[]>> Existence = new List<List<int[]>>();

                int ii = RowS, jj = ColS;

                object O1 = new object();
                lock (O1)
                {
                    for (int i = ii - 1; i < ii + 2; i++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int j = i + ii - jj;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            if (Order == 1 && Tabl[i, j] != 6)
                            {
                                continue;
                            }

                            if (Order == -1 && Tabl[i, j] != -6)
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInReducedAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                    //===============================
                    object OOOo1 = new object();
                    lock (OOOo1)
                    {
                        for (int i = ii - 1; i < ii + 2; i++)
                        {
                            object O = new object();
                            lock (O)
                            {
                                int j = i * -1 + ii - jj;
                                if (!Scop(ii, jj, i, j))
                                {
                                    continue;
                                }

                                if (Order == 1 && Tabl[i, j] != 6)
                                {
                                    continue;
                                }

                                if (Order == -1 && Tabl[i, j] != -6)
                                {
                                    continue;
                                }

                                List<int[]> Exist = ListOfExistInReducedAttackList(Before, RowS, ColS, i, j);
                                if (Exist.Count >= 1)
                                {
                                    Existence.Add(Exist);
                                }
                            }
                        }
                    }
                    //=============================================
                    ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, i =>

                    for (int i = ii - 1; i < ii + 2; i++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int j = jj;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            if (Order == 1 && Tabl[i, j] != 6)
                            {
                                continue;
                            }

                            if (Order == -1 && Tabl[i, j] != -6)
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInReducedAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }
                //===============================
                object OO = new object();
                lock (OO)
                {
                    for (int j = ii - 1; j < ii + 2; j++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            int i = ii;
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            if (Order == 1 && Tabl[i, j] != 6)
                            {
                                continue;
                            }

                            if (Order == -1 && Tabl[i, j] != -6)
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInReducedAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }

                return Existence;
            }
        }

        //method of list of reduced attack or attack by lists of method found lists by every specified objects on board.
        private List<List<int[]>> AchMazReducedMinister(int[,] Tabl, bool Before, int RowS, int ColS, int Order)
        {
            object OO = new object();
            lock (OO)
            {
                List<List<int[]>> Existence = new List<List<int[]>>();

                int ii = RowS, jj = ColS;

                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        object O = new object();
                        lock (O)
                        {
                            if (!Scop(ii, jj, i, j))
                            {
                                continue;
                            }

                            if (Order == 1 && Tabl[i, j] != -5)
                            {
                                continue;
                            }

                            if (Order == -1 && Tabl[i, j] != 5)
                            {
                                continue;
                            }

                            List<int[]> Exist = ListOfExistInReducedAttackList(Before, RowS, ColS, i, j);
                            if (Exist.Count >= 1)
                            {
                                Existence.Add(Exist);
                            }
                        }
                    }
                }

                return Existence;
            }
        }

        //calculation first level of achmaz by sub metods possible
        private void Achmaz(int[,] Table, bool Before, int RowS, int ColS, int RowD, int ColD, int Order)
        {
            object O1 = new object();
            lock (O1)
            {
                List<List<int[]>> EleRedAchmaz = null, EleAchmaz = null, HourAchmaz = null, HourRedAchmaz = null, CastRedAchmaz = null, CastAchmaz = null, MiniRedAchmaz = null, MiniAchmaz = null, KingRedAchmaz = null, KingAchmaz = null;
                //if (System.Math.Abs(Table[RowS, ColS]) == 2 || System.Math.Abs(Table[RowD, ColD]) == 2)
                {
                    Task tth = Task.Factory.StartNew(() =>
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.Invoke(() =>
                        {
                            Task<List<List<int[]>>> tth1 = Task.Factory.StartNew(() => EleRedAchmaz = AchMazReducedElephasnt(CloneATable(Table), Before, RowS, ColS, Order));
                            tth1.Wait();
                            tth1.Dispose();
                        }, () =>
                        {
                            Task<List<List<int[]>>> tth2 = Task.Factory.StartNew(() => EleAchmaz = AchMazElephasnt(CloneATable(Table), Before, RowS, ColS, Order));
                            tth2.Wait();
                            tth2.Dispose();
                        }, () =>
                        {
                            Task<List<List<int[]>>> tth1 = Task.Factory.StartNew(() => CastRedAchmaz = AchMazReducedCastle(CloneATable(Table), Before, RowS, ColS, Order));
                            tth1.Wait();
                            tth1.Dispose();
                        }, () =>
                        {
                            Task<List<List<int[]>>> tth2 = Task.Factory.StartNew(() => CastAchmaz = AchMazCastle(CloneATable(Table), Before, RowS, ColS, Order));
                            tth2.Wait();
                            tth2.Dispose();
                        }, () =>
                        {
                            Task<List<List<int[]>>> tth1 = Task.Factory.StartNew(() => MiniRedAchmaz = AchMazReducedMinister(CloneATable(Table), Before, RowS, ColS, Order));
                            tth1.Wait();
                            tth1.Dispose();
                        }, () =>
                        {
                            Task<List<List<int[]>>> tth2 = Task.Factory.StartNew(() => MiniAchmaz = AchMazMinister(CloneATable(Table), Before, RowS, ColS, Order));
                            tth2.Wait();
                            tth2.Dispose();
                        }, () =>
                        {
                            Task<List<List<int[]>>> tth1 = Task.Factory.StartNew(() => KingRedAchmaz = AchMazReducedKing(CloneATable(Table), Before, RowS, ColS, Order));
                            tth1.Wait();
                            tth1.Dispose();
                        }, () =>
                        {
                            Task<List<List<int[]>>> tth2 = Task.Factory.StartNew(() => KingAchmaz = AchMazKing(CloneATable(Table), Before, RowS, ColS, Order));
                            tth2.Wait();
                            tth2.Dispose();
                        });
                    });
                    tth.Wait();
                    tth.Dispose();
                }
                Task ttttth = Task.Factory.StartNew(() =>
                {
                    ParallelOptions po = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.Invoke(() =>
                    {
                        Task tth1 = Task.Factory.StartNew(() => AchmazPure.Add(CollectionSummation(EleAchmaz, HourAchmaz, CastAchmaz, MiniAchmaz)));
                        tth1.Wait();
                        tth1.Dispose();
                    }, () =>
                    {
                        Task tth2 = Task.Factory.StartNew(() => AchmazReduced.Add(CollectionSummation(EleRedAchmaz, HourRedAchmaz, CastRedAchmaz, MiniRedAchmaz)));
                        tth2.Wait();
                        tth2.Dispose();
                    });
                });
                ttttth.Wait();
                ttttth.Dispose();
            }
        }

        //creation of one region of collection of achmaz method
        private void CollectionSummation(List<List<int[]>> A, int Sum, ref List<int[]> Co)
        {
            object O1 = new object();
            lock (O1)
            {
                if (A == null)
                {
                    return;
                }

                for (int i = 0; i < A.Count; i++)
                {
                    for (int j = 0; j < A[i].Count; j++)
                    {
                        if (A[i][j][4] == Sum && (!Exist(Co, A[i][j])))
                        {
                            Co.Add(A[i][j]);
                        }
                    }
                }
            }
        }

        //collection of regionns redistributed from achmaz methods
        private List<List<int[]>> CollectionSummation(List<List<int[]>> A, List<List<int[]>> B, List<List<int[]>> C, List<List<int[]>> D)
        {
            object O1 = new object();
            lock (O1)
            {
                List<List<int[]>> Col = new List<List<int[]>>();

                List<int[]> Co1 = new List<int[]>();
                CollectionSummation(A, -4, ref Co1);
                CollectionSummation(B, -4, ref Co1);
                CollectionSummation(C, -4, ref Co1);
                CollectionSummation(D, -4, ref Co1);

                if (Co1.Count > 0)
                {
                    Col.Add(Co1);
                }

                List<int[]> Co2 = new List<int[]>();
                CollectionSummation(A, -3, ref Co2);
                CollectionSummation(B, -3, ref Co2);
                CollectionSummation(C, -3, ref Co2);
                CollectionSummation(D, -3, ref Co2);

                if (Co2.Count > 0)
                {
                    Col.Add(Co2);
                }

                List<int[]> Co3 = new List<int[]>();
                CollectionSummation(A, -2, ref Co3);
                CollectionSummation(B, -2, ref Co3);
                CollectionSummation(C, -2, ref Co3);
                CollectionSummation(D, -2, ref Co3);

                if (Co3.Count > 0)
                {
                    Col.Add(Co3);
                }

                List<int[]> Co4 = new List<int[]>();
                CollectionSummation(A, -1, ref Co4);
                CollectionSummation(B, -1, ref Co4);
                CollectionSummation(C, -1, ref Co4);
                CollectionSummation(D, -1, ref Co4);

                if (Co4.Count > 0)
                {
                    Col.Add(Co4);
                }

                List<int[]> Co5 = new List<int[]>();
                CollectionSummation(A, 1, ref Co5);
                CollectionSummation(B, 1, ref Co5);
                CollectionSummation(C, 1, ref Co5);
                CollectionSummation(D, 1, ref Co5);

                if (Co5.Count > 0)
                {
                    Col.Add(Co5);
                }

                List<int[]> Co6 = new List<int[]>();
                CollectionSummation(A, 2, ref Co6);
                CollectionSummation(B, 2, ref Co6);
                CollectionSummation(C, 2, ref Co6);
                CollectionSummation(D, 2, ref Co6);

                if (Co6.Count > 0)
                {
                    Col.Add(Co6);
                }

                List<int[]> Co7 = new List<int[]>();
                CollectionSummation(A, 3, ref Co7);
                CollectionSummation(B, 3, ref Co7);
                CollectionSummation(C, 3, ref Co7);
                CollectionSummation(D, 3, ref Co7);

                if (Co7.Count > 0)
                {
                    Col.Add(Co7);
                }

                List<int[]> Co8 = new List<int[]>();
                CollectionSummation(A, 4, ref Co8);
                CollectionSummation(B, 4, ref Co8);
                CollectionSummation(C, 4, ref Co8);
                CollectionSummation(D, 4, ref Co8);

                if (Co8.Count > 0)
                {
                    Col.Add(Co8);
                }

                return Col;
            }
        }

        //determine sign of 8th regions
        private int SignBeforNext(int Row, int Col, int i, int j)
        {
            object O1 = new object();
            lock (O1)
            {
                int Sign = 0;
                if (Row < i && Col > j)
                {
                    Sign = -4;
                }

                if (Row > i && Col > j)
                {
                    Sign = 4;
                }

                if (Row > i && Col < j)
                {
                    Sign = 3;
                }

                if (Row < i && Col > j)
                {
                    Sign = -3;
                }

                if (Row == i && Col < j)
                {
                    Sign = -2;
                }

                if (Row == i && Col > j)
                {
                    Sign = 2;
                }

                if (Row > i && Col == j)
                {
                    Sign = 1;
                }

                if (Row < i && Col == j)
                {
                    Sign = -1;
                }

                return Sign;
            }
        }

        //heuristic creation of double attacked
        private int DoubleAttack(int[,] Table, bool Before, int Order)
        {
            object O1 = new object();
            lock (O1)
            {
                int DD = 0;
                List<List<int[]>> DDL = new List<List<int[]>>();
                for (int RowSS = 0; RowSS < 8; RowSS++)
                {
                    for (int ColSS = 0; ColSS < 8; ColSS++)
                    {
                        if (Order == -1 && Table[RowSS, ColSS] >= 0)
                        {
                            continue;
                        }

                        if (Order == 1 && Table[RowSS, ColSS] <= 0)
                        {
                            continue;
                        }

                        for (int RowDD = 0; RowDD < 8; RowDD++)
                        {
                            for (int ColDD = 0; ColDD < 8; ColDD++)
                            {
                                List<int[]> DDA = ListOfExistInAttackList(Before, RowSS, ColSS, RowDD, ColDD);
                                if (DDA.Count > 0)
                                {
                                    DDL.Add(DDA);
                                }
                            }
                        }
                    }
                }
                List<int[]> DDE = new List<int[]>();
                for (int i = 0; i < DDL.Count; i++)
                {
                    for (int j = 0; j < DDL[i].Count; j++)
                    {
                        if (!ExistFull(DDE, DDL[i][j]))
                        {
                            DDE.Add(DDL[i][j]);
                        }
                    }
                }
                if (DDE.Count > 1)
                {
                    for (int RowSS = 0; RowSS < 8; RowSS++)
                    {
                        for (int ColSS = 0; ColSS < 8; ColSS++)
                        {
                            for (int RowDD = 0; RowDD < 8; RowDD++)
                            {
                                for (int ColDD = 0; ColDD < 8; ColDD++)
                                {
                                    for (int i = 0; i < DDE.Count; i++)
                                    {
                                        if (DDE[i][0] == RowSS && DDE[i][1] == ColSS && DDE[i][2] == RowDD && DDE[i][3] == ColDD)
                                        {
                                            DD++;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (DD <= 1)
                {
                    DD = 0;
                }

                DD = (RationalRegard) * (DD);
                return DD;
            }
        }

        //heuristic creation of double defence
        private int DoubleDefence(int[,] Table, bool Before, int Order)
        {
            object O1 = new object();
            lock (O1)
            {
                int DD = 0;
                List<List<int[]>> DDL = new List<List<int[]>>();
                for (int RowSS = 0; RowSS < 8; RowSS++)
                {
                    for (int ColSS = 0; ColSS < 8; ColSS++)
                    {
                        for (int RowDD = 0; RowDD < 8; RowDD++)
                        {
                            for (int ColDD = 0; ColDD < 8; ColDD++)
                            {
                                if (Order == 1 && Table[RowDD, ColDD] >= 0)
                                {
                                    continue;
                                }

                                if (Order == -1 && Table[RowDD, ColDD] <= 0)
                                {
                                    continue;
                                }

                                List<int[]> DDA = ListOfExistInReducedAttackList(Before, RowSS, ColSS, RowDD, ColDD);
                                if (DDA.Count > 0)
                                {
                                    DDL.Add(DDA);
                                }
                            }
                        }
                    }
                }
                List<int[]> DDE = new List<int[]>();
                for (int i = 0; i < DDL.Count; i++)
                {
                    for (int j = 0; j < DDL[i].Count; j++)
                    {
                        if (!ExistFull(DDE, DDL[i][j]))
                        {
                            DDE.Add(DDL[i][j]);
                        }
                    }
                }
                if (DDE.Count > 1)
                {
                    for (int RowDD = 0; RowDD < 8; RowDD++)
                    {
                        for (int ColDD = 0; ColDD < 8; ColDD++)
                        {
                            List<int[]> DDEE = new List<int[]>();
                            for (int RowSS = 0; RowSS < 8; RowSS++)
                            {
                                for (int ColSS = 0; ColSS < 8; ColSS++)
                                {
                                    for (int i = 0; i < DDE.Count; i++)
                                    {
                                        if (DDE[i][0] == RowDD && DDE[i][1] == ColDD && DDE[i][2] == RowSS && DDE[i][3] == ColSS)
                                        {
                                            int[] SS = new int[4];
                                            SS[0] = RowDD;
                                            SS[1] = ColDD;
                                            SS[2] = RowSS;
                                            SS[3] = ColSS;
                                            if (!ExistFull(DDEE, SS))
                                            {
                                                DDEE.Add(SS);
                                                DD++;
                                            }
                                        }
                                    }
                                }
                            }
                            if (DDEE.Count > 1)
                            {
                                if (!ExistFullDoubleList(HeuristicDoubleDefenceIndexInOnGame, DDEE))
                                {
                                    HeuristicDoubleDefenceIndexInOnGame.Add(DDEE);
                                }
                            }
                        }
                    }
                }
                if (HeuristicDoubleDefenceIndexInOnGame.Count == 0)
                {
                    DD = 0;
                }

                if (DD <= 1)
                {
                    DD = 0;
                }

                DD = (RationalPenalty) * (DD);
                return DD;
            }
        }

        //when after of Move
        private bool MidleIndex()
        {
            object OO = new object();
            lock (OO)
            {
                bool Is = true;
                if (HeuristicAllAttackedMidel != 0)
                {
                    return false;
                }

                if (HeuristicAllMoveMidel != 0)
                {
                    return false;
                }

                if (HeuristicAllReducedAttackedMidel != 0)
                {
                    return false;
                }

                if (HeuristicAllReducedMoveMidel != 0)
                {
                    return false;
                }

                if (HeuristicAllReducedSupportMidel != 0)
                {
                    return false;
                }

                if (HeuristicAllSupportMidel != 0)
                {
                    return false;
                }

                if (HeuristicReducedAttackedIndexInOnGameMidle != 0)
                {
                    return false;
                }

                if (HeuristicDoubleDefenceIndexInOnGameMidle != 0)
                {
                    return false;
                }

                return Is;
            }
        }

        private bool IsSupHuTrue()
        {
            object o = new object();
            lock (o)
            {
                bool Is = false;

                if (!AllDraw.AllowedSupTrue)
                {
                    Is = IsSupHu[IsSupHu.Count - 1];
                }
                else
                {
                    Is = IsSup[IsSup.Count - 1];
                }
                return Is;
            }
        }

        public void CalculateHeuristics(int[] LoseOcuuredatChiled, int WinOcuuredatChiled, bool Before, int Order, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, int color
           , ref double HeuristicAttackValue
               , ref double HeuristicMovementValue
               , ref double HeuristicSelfSupportedValue
               , ref double HeuristicReducedMovementValue
              , ref double HeuristicReducedSupport
               , ref double HeuristicReducedAttackValue
               , ref double HeuristicDistributionValue
           , ref double HeuristicKingSafe
           , ref double HeuristicFromCenter
           , ref double HeuristicKingDangour, ref double HeuristicCheckedMate)
        {
            if (color == 1)
                CalculateHeuristics(LoseOcuuredatChiled, WinOcuuredatChiled, Before, Order, Killed, TableS, RowS, ColS, RowD, ColD, Color.Gray
           , ref HeuristicAttackValue
               , ref HeuristicMovementValue
               , ref HeuristicSelfSupportedValue
               , ref HeuristicReducedMovementValue
              , ref HeuristicReducedSupport
               , ref HeuristicReducedAttackValue
               , ref HeuristicDistributionValue
           , ref HeuristicKingSafe
           , ref HeuristicFromCenter
           , ref HeuristicKingDangour, ref HeuristicCheckedMate);
            else
            {
                CalculateHeuristics(LoseOcuuredatChiled, WinOcuuredatChiled, Before, Order, Killed, TableS, RowS, ColS, RowD, ColD, Color.Brown
           , ref HeuristicAttackValue
               , ref HeuristicMovementValue
               , ref HeuristicSelfSupportedValue
               , ref HeuristicReducedMovementValue
              , ref HeuristicReducedSupport
               , ref HeuristicReducedAttackValue
               , ref HeuristicDistributionValue
           , ref HeuristicKingSafe
           , ref HeuristicFromCenter
           , ref HeuristicKingDangour, ref HeuristicCheckedMate);
            }
        }

        //heuristic main method
        /********************************************************************************
         complexity of achmaz is O((m^n)*(n^d))*********************************************
         m: avarage state complexity for on state (0<=m<=1026)***************************
         n: avarages every chiles node number of a parent********************************
         d: heighth of Tree**************************************************************
         ********************************************************************************/

        public void CalculateHeuristics(int[] LoseOcuuredatChiled, int WinOcuuredatChiled, bool Before, int Order, int Killed, int[,] TableS, int RowS, int ColS, int RowD, int ColD, Color color
            , ref double HeuristicAttackValue
                , ref double HeuristicMovementValue
                , ref double HeuristicSelfSupportedValue
                , ref double HeuristicReducedMovementValue
               , ref double HeuristicReducedSupport
                , ref double HeuristicReducedAttackValue
                , ref double HeuristicDistributionValue
            , ref double HeuristicKingSafe
            , ref double HeuristicFromCenter
            , ref double HeuristicKingDangour, ref double HeuristicCheckedMate)
        {
            object OO = new object();
            lock (OO)
            {
                double HA = 1;
                if (!Scop(RowS, ColS, RowD, ColD, Kind))
                {
                    return;
                }

                double[] Heuristic = new double[6];
                double HCheck = new double();
                double HDistance = new double();
                double HKingSafe = new double();
                double HKingDangour = new double();
                double HFromCenter = 0;
                double HExchangeInnovation = 0;
                double HExchangeSupport = 0;
                if (!Before && MidleIndex())
                {
                    HeuristicAllAttackedMidel = HeuristicAllAttacked.Count;
                    HeuristicAllMoveMidel = HeuristicAllMove.Count;
                    HeuristicAllReducedAttackedMidel = HeuristicAllReducedAttacked.Count;
                    HeuristicAllReducedMoveMidel = HeuristicAllReducedMove.Count;
                    HeuristicAllReducedSupportMidel = HeuristicAllReducedSupport.Count;
                    HeuristicAllSupportMidel = HeuristicAllSupport.Count;
                    HeuristicReducedAttackedIndexInOnGameMidle = HeuristicReducedAttackedIndexInOnGame.Count;
                    HeuristicDoubleDefenceIndexInOnGameMidle = HeuristicDoubleDefenceIndexInOnGame.Count;
                }
                if (Order != AllDraw.OrderPlateDraw)
                {
                    return;
                }

                double[] Hu = null;
                Task H1 = null, H2 = null, H3 = null;
                double HAchmaz = 0;
                double HDoubleAttack = 0, HDoubleDefense = 0;
                double HWin = 0, HLose = 0;
                bool IsS = false;
                Task<double[]> th = Task.Factory.StartNew(() => Hu = CalculateHeuristicsParallel(ref HA, Before, Killed, CloneATable(TableS), RowS, ColS, RowD, ColD, color));
                th.Wait();
                th.Dispose();

                //if (UsePenaltyRegardMechnisamT)

                Heuristic[0] = Hu[0];
                Heuristic[1] = Hu[1];
                Heuristic[2] = Hu[2];
                Heuristic[3] = Hu[3];
                Heuristic[4] = Hu[4];
                Heuristic[5] = Hu[5];
                HCheck = Hu[6];
                HDistance = Hu[7];
                HKingSafe = Hu[8];
                HKingDangour = Hu[9];
                HFromCenter = Hu[10];
                HExchangeInnovation = Hu[11] + Hu[12] + Hu[13];
                HExchangeSupport = Hu[14];

                object O1 = new object();
                lock (O1)
                {
                    if (!IsSupHuTrue())
                    {
                        if (Before)
                        {
                            if (Order == 1)
                            {
                                if ((System.Math.Abs(TableS[RowS, ColS]) > System.Math.Abs(TableS[RowD, ColD])) && TableS[RowD, ColD] > 0 && NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) > 0)
                                {
                                    //if (Before)
                                    SetSupHuTrue();
                                    IsS = true;
                                }
                            }
                            else
                            {
                                if ((System.Math.Abs(TableS[RowS, ColS]) > System.Math.Abs(TableS[RowD, ColD])) && TableS[RowD, ColD] > 0 && NoOfExistInReducedAttackList(Before, RowS, ColS, RowD, ColD) > 0)
                                {
                                    //if (Before)
                                    SetSupHuTrue();
                                    IsS = true;
                                }
                            }
                            if (!GoldenFinished && WinOcuuredatChiled == 0)
                            {  //Disturbe on huge traversal exchange prevention
                               //Ignore of atack and checkedmate at first until all Move
                                bool A = false, B = false, C = false;
                                if (Order == 1)
                                {
                                    A = ColleralationGray < 30;
                                    if (Order == AllDraw.OrderPlateDraw)
                                    {
                                        B = NoOfExistInAttackList(Before, RowS, ColS, RowD, ColD) > 0 && (System.Math.Abs(TableS[RowD, ColD]) != 0 && System.Math.Abs(TableS[RowS, ColS]) > 1);
                                        C = HeuristicCheckedMate < 0 && (IsThereMateOfSelf[IsThereMateOfSelf.Count - 1]);
                                    }
                                }
                                else
                                {
                                    A = ColleralationBrown < 30;
                                    if (Order == AllDraw.OrderPlateDraw)
                                    {
                                        B = NoOfExistInAttackList(Before, RowS, ColS, RowD, ColD) > 0 && (System.Math.Abs(TableS[RowD, ColD]) != 0 && System.Math.Abs(TableS[RowS, ColS]) > 1);
                                        C = HeuristicCheckedMate < 0 && (IsThereMateOfSelf[IsThereMateOfSelf.Count - 1]);
                                    }
                                }
                                if (A && ((B) || (C)))
                                {
                                    SetSupHuTrue();
                                    IsS = true;
                                }
                                //Every objects one Move at game begin
                                /*int Total = 0;
                                int Is = 0;
                                int NoOfBoardMoved = NoOfObjectNotMovable(CloneATable(TableS), Order, OrderColor(Order), ref Total, ref Is);
                                if (Order == 1)
                                {
                                    if (
                                            //(
                                            (NoOfBoardMoved + Is >= Total) &&
                                            TableInitiationPreventionOfMultipleMove[RowS, ColS] >= NoOfMovableAllObjectMove
                                    //)&& A && TableS[RowS, ColS] < 0 && TableS[RowD, ColD] >= 0
                                    )
                                    {
                                        IsS = true;
                                        SetSupHuTrue();
                                    }
                                }
                                else
                                {
                                    if (
                                            //(
                                            (NoOfBoardMoved + Is >= Total) &&
                                            TableInitiationPreventionOfMultipleMove[RowS, ColS] >= NoOfMovableAllObjectMove
                                    //)&& A && TableS[RowS, ColS] < 0 && TableS[RowD, ColD] >= 0
                                    )
                                    {
                                        IsS = true;
                                        SetSupHuTrue();
                                    }
                                }*/
                                //Empire more
                                if (A)
                                {
                                    if (ColleralationGray < 16)
                                    {
                                        if (NoOfExistInSupportList(Before, RowS, ColS, RowD, ColD) + NoOfExistInMoveList(Before, RowS, ColS, RowD, ColD) + NoOfExistInAttackList(Before, RowS, ColS, RowD, ColD) - NoOfExistInReducedSupportList(Before, RowD, ColD, RowS, ColS) - NoOfExistInReducedMoveList(Before, RowD, ColD, RowS, ColS) - NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) >= DifOfNoOfSupporteAndReducedSupportGray)
                                        {
                                            DifOfNoOfSupporteAndReducedSupportGray = NoOfExistInSupportList(Before, RowS, ColS, RowD, ColD) + NoOfExistInMoveList(Before, RowS, ColS, RowD, ColD) + NoOfExistInAttackList(Before, RowS, ColS, RowD, ColD) - NoOfExistInReducedSupportList(Before, RowD, ColD, RowS, ColS) - NoOfExistInReducedMoveList(Before, RowD, ColD, RowS, ColS) - NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS);
                                        }
                                        else
                                        if (DifOfNoOfSupporteAndReducedSupportGray < 0)
                                        {
                                            IsS = true;
                                            SetSupHuTrue();
                                        }
                                    }
                                }
                                //Hourse before elephants
                                if (((RowS == 2 && ColS == 7 && TableInitiation[RowS, ColS] == TableS[2, 7] && TableS[2, 7] == 2) && TableInitiationPreventionOfMultipleMove[2, 7] != 0) || ((RowS == 5 && ColS == 7 && TableInitiation[RowS, ColS] == TableS[5, 7] && TableS[5, 7] == 2) && TableInitiationPreventionOfMultipleMove[5, 7] != 0))
                                {
                                    Color a = Color.Gray;
                                    if (Order == -1)
                                    {
                                        a = Color.Brown;
                                    }

                                    if (((TableInitiation[1, 7] == TableS[1, 7] && TableS[1, 7] == 3) && TableInitiationPreventionOfMultipleMove[1, 7] == 0 && ObjectMovable(1, 7, CloneATable(TableS), Order, a)) || ((TableInitiation[6, 7] == TableS[6, 7] && TableS[6, 7] == 3) && TableInitiationPreventionOfMultipleMove[6, 7] == 0 && ObjectMovable(6, 7, CloneATable(TableS), Order, a)))
                                    {
                                        IsS = true;
                                        SetSupHuTrue();
                                    }
                                }
                            }
                            //when thre is most reduced support finding
                            int[] IsNo = MostOfFindMostHeuristicAllReducedSupportInList(Before, RowD, ColD);
                            if (IsNo != null)
                            {
                                if (IsNo[1] < HeuristicAllReducedSupport.Count)
                                {
                                    if (NoOfExistInAttackList(Before, RowS, ColS, HeuristicAllReducedSupport[IsNo[1]][0], HeuristicAllReducedSupport[IsNo[1]][1]) > 0)
                                    {
                                        ClearSupHuTrue();
                                    }
                                }
                            }
                            if (HDoubleAttack > 0)
                            {
                                if (!IsSupHuTrue())
                                {
                                    WinOcuuredatChiled = 5;
                                }
                            }
                        }
                        else
                        {
                            //Disturbe on huge traversal exchange prevention
                            //if ((System.Math.Abs(TableConst[RowS, ColS]) > System.Math.Abs(Killed)) && Killed != 0 && NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) > 0)
                            if (Order == AllDraw.OrderPlateDraw)
                            {
                                if (DisturbeOnNonSupportedTraversalExchangePrevention(Killed, Before, CloneATable(TableS), Order))
                                {
                                    //if (Before)
                                    SetSupHuTrue();
                                    IsS = true;
                                }
                                if (DisturbeOnHugeTraversalExchangePrevention(Before, CloneATable(TableS), Order))
                                {
                                    //if (Before)
                                    SetSupHuTrue();
                                    IsS = true;
                                }
                                else
                                {
                                    if (TableInitiationPreventionOfMultipleMove[RowS, ColS] == NoOfMovableAllObjectMove && IsSupHuTrue() && IsS)
                                    {
                                        TableInitiationPreventionOfMultipleMove[RowS, ColS] = NoOfMovableAllObjectMove - 1;
                                        IsS = false;
                                    }
                                }
                            }
                            if (!GoldenFinished)
                            {   //Ignore of atack and checkedmate at first until all Move
                                bool A = false, B = false, C = false;
                                if (Order == 1)
                                {
                                    A = ColleralationGray < 30;
                                    if (Order == AllDraw.OrderPlateDraw)
                                    {
                                        B = NoOfExistInAttackList(Before, RowS, ColS, RowD, ColD) > 0 && (Killed != 0 && Killed < TableS[RowD, ColD]);
                                        C = HeuristicCheckedMate < 0 && (IsThereMateOfSelf[IsThereMateOfSelf.Count - 1]);
                                    }
                                }
                                else
                                {
                                    A = ColleralationBrown < 30;
                                    if (Order == AllDraw.OrderPlateDraw)
                                    {
                                        B = NoOfExistInAttackList(Before, RowS, ColS, RowD, ColD) > 0 && (Killed != 0 && Killed < TableS[RowD, ColD]);
                                        C = HeuristicCheckedMate < 0 && (IsThereMateOfSelf[IsThereMateOfSelf.Count - 1]);
                                    }
                                }
                                if (A && ((B) || (C)))
                                {
                                    SetSupHuTrue();
                                    IsS = true;
                                }
                                else
                                {
                                    if (Order == AllDraw.OrderPlateDraw)
                                    {//if (TableInitiationPreventionOfMultipleMove[RowS, ColS] == NoOfMovableAllObjectMove && IsSupHuTrue() && (!IsS))
                                        //Empire more
                                        if (A)
                                        {
                                            if (ColleralationBrown < 16)
                                            {
                                                if (NoOfExistInSupportList(Before, RowS, ColS, RowD, ColD) + NoOfExistInMoveList(Before, RowS, ColS, RowD, ColD) + NoOfExistInAttackList(Before, RowS, ColS, RowD, ColD) - NoOfExistInReducedSupportList(Before, RowD, ColD, RowS, ColS) - NoOfExistInReducedMoveList(Before, RowD, ColD, RowS, ColS) - NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS) >= DifOfNoOfSupporteAndReducedSupportBrown)
                                                {
                                                    DifOfNoOfSupporteAndReducedSupportBrown = NoOfExistInSupportList(Before, RowS, ColS, RowD, ColD) + NoOfExistInMoveList(Before, RowS, ColS, RowD, ColD) + NoOfExistInAttackList(Before, RowS, ColS, RowD, ColD) - NoOfExistInReducedSupportList(Before, RowD, ColD, RowS, ColS) - NoOfExistInReducedMoveList(Before, RowD, ColD, RowS, ColS) - NoOfExistInReducedAttackList(Before, RowD, ColD, RowS, ColS);
                                                }
                                                else
                                                if (DifOfNoOfSupporteAndReducedSupportBrown < 0)
                                                {
                                                    {
                                                        SetSupHuTrue();
                                                        IsS = true;
                                                    }
                                                }
                                            }
                                        }
                                        //Hourse before elephants
                                        if (((RowS == 2 && ColS == 0 && TableInitiation[RowS, ColS] == TableS[RowS, ColS] && TableS[RowS, ColS] == -2) && TableInitiationPreventionOfMultipleMove[2, 0] != 0) || ((RowS == 5 && ColS == 0 && TableInitiation[RowS, ColS] == TableS[RowS, ColS] && TableConst[RowS, ColS] == -2) && TableInitiationPreventionOfMultipleMove[5, 0] != 0))
                                        {
                                            Color a = Color.Gray;
                                            if (Order == -1)
                                            {
                                                a = Color.Brown;
                                            }

                                            if (((TableInitiation[1, 0] == TableS[1, 0] && TableS[1, 0] == -3) && TableInitiationPreventionOfMultipleMove[1, 0] == 0 && ObjectMovable(1, 0, CloneATable(TableS), Order, a)) || ((TableInitiation[6, 0] == TableS[6, 0] && TableS[6, 0] == -3) && TableInitiationPreventionOfMultipleMove[6, 0] == 0 && ObjectMovable(6, 0, CloneATable(TableS), Order, a)))
                                            {
                                                SetSupHuTrue();
                                                IsS = true;
                                            }
                                        }
                                        //Every objects one Move at game begin
                                        /*int Total = 0;
                                        int Is = 0;
                                        int NoOfBoardMoved = NoOfObjectNotMovable(CloneATable(TableS), Order, OrderColor(Order), ref Total, ref Is);
                                        if (Order == 1)
                                        {
                                            if (
                                                    //(
                                                    (NoOfBoardMoved + Is >= Total) &&
                                                    TableInitiationPreventionOfMultipleMove[RowS, ColS] >= NoOfMovableAllObjectMove
                                            //)&& A && TableS[RowS, ColS] < 0 && TableS[RowD, ColD] >= 0
                                            )
                                            {
                                                IsS = true;
                                                SetSupHuTrue();
                                            }
                                        }
                                        else
                                        {
                                            if (
                                                    //(
                                                    (NoOfBoardMoved + Is >= Total) &&
                                                    TableInitiationPreventionOfMultipleMove[RowS, ColS] >= NoOfMovableAllObjectMove
                                            //)&& A && TableS[RowS, ColS] < 0 && TableS[RowD, ColD] >= 0
                                            )
                                            {
                                                IsS = true;
                                                SetSupHuTrue();
                                            }
                                        }*/
                                    }
                                    //when thre is most reduced support finding
                                    int[] IsNo = MostOfFindMostHeuristicAllReducedSupportInList(Before, RowD, ColD);
                                    if (IsNo != null)
                                    {
                                        if (IsNo[1] < HeuristicAllReducedSupport.Count && IsNo[1] >= HeuristicAllReducedSupportMidel)
                                        {
                                            if (NoOfExistInAttackList(Before, RowS, ColS, HeuristicAllReducedSupport[IsNo[1]][0], HeuristicAllReducedSupport[IsNo[1]][1]) > 0)
                                            {
                                                ClearSupHuTrue();
                                            }
                                        }
                                    }

                                    if (!IsS)
                                    {
                                        ClearSupHuTrue();
                                    }
                                }
                            }
                            if (HDoubleAttack > 0)
                            {
                                if (!IsSupHuTrue())
                                {
                                    WinOcuuredatChiled = 5;
                                }
                                else
                                {
                                    WinOcuuredatChiled = 0;
                                }
                            }
                        }
                    }
                    if (!IsSupHuTrue() && (Order == AllDraw.OrderPlateDraw))
                    {
                        H1 = Task.Factory.StartNew(() => Achmaz(CloneATable(TableS), Before, RowS, ColS, RowD, ColD, Order));
                        H1.Wait();
                        H1.Dispose();
                        if (Before)
                        {
                            int TotalS = 0;
                            int IsSC = 0;
                            NoOfObjectNotMovable(CloneATable(TableS), Order, OrderColor(Order), ref TotalS, ref IsSC);
                            if ((16 - ColleralationGray) + IsSC >= TotalS)
                            {
                                GoldenFinished = true;
                            }

                            HAchmaz = (RationalPenalty * (AchmazReducedBefore(Before, CloneATable(TableS)))) + (RationalRegard * (AchmazPuredBefore(Before, CloneATable(TableS))));
                            /* if (HAchmaz > 0)
                             {
                                 //IsSup[IsSup.Count - 1] = false;
                             }
                             else*/
                            if (HAchmaz < 0)
                            {
                                IsSupHu[IsSupHu.Count - 1] = true;
                                //IsSup[IsSup.Count - 1] = true;
                            }
                        }
                        else
                        {
                            int TotalS = 0;
                            int IsSC = 0;
                            NoOfObjectNotMovable(CloneATable(TableS), Order, OrderColor(Order), ref TotalS, ref IsSC);
                            if ((16 - ColleralationBrown) + IsSC >= TotalS)
                            {
                                GoldenFinished = true;
                            }

                            HAchmaz = (RationalPenalty * (AchmazReducedAfter(Before, CloneATable(TableS)))) + (RationalRegard * (AchmazPuredAfter(Before, CloneATable(TableS))));
                            /* if (HAchmaz > 0)
                              {
                                  //IsSup[IsSup.Count - 1] = false;
                              }
                              else*/
                            if (HAchmaz < 0)
                            {
                                IsSupHu[IsSupHu.Count - 1] = true;
                                //IsSup[IsSup.Count - 1] = true;
                            }
                        }
                    }
                    if (!IsSupHuTrue() && (Order == AllDraw.OrderPlateDraw))
                    {
                        H2 = Task.Factory.StartNew(() => HDoubleAttack = DoubleAttack(CloneATable(TableS), Before, Order));
                        H3 = Task.Factory.StartNew(() => HDoubleDefense = DoubleDefence(CloneATable(TableS), Before, Order));
                        H2.Wait();
                        H3.Wait();
                        H2.Dispose();
                        H3.Dispose();
                        if (HDoubleDefense < 0)
                        {
                            SetSupHuTrue();
                            IsS = true;
                        }
                    }
                    if (Before)
                    {
                        HeuristicReducedAttackValue = (Heuristic[0] * SignOrderToPlate(Order));
                        HeuristicAttackValue = (Heuristic[1] * SignOrderToPlate(Order));
                        HeuristicReducedSupport = (Heuristic[2] * SignOrderToPlate(Order));
                        HeuristicSelfSupportedValue = (Heuristic[3] * SignOrderToPlate(Order));
                        HeuristicMovementValue = (Heuristic[4] * SignOrderToPlate(Order));
                        HeuristicReducedMovementValue = ((Heuristic[5] + HExchangeInnovation + HExchangeSupport) * SignOrderToPlate(Order));
                        HeuristicCheckedMate = (((HCheck + HAchmaz + HWin + HLose) * SignOrderToPlate(Order)));
                        HeuristicDistributionValue = ((HDistance + HAchmaz + HDoubleAttack + HDoubleDefense) * SignOrderToPlate(Order));
                        HeuristicKingSafe = (HKingSafe * SignOrderToPlate(Order));
                        HeuristicKingDangour = (HKingDangour * SignOrderToPlate(Order));
                        HeuristicFromCenter = (HFromCenter * SignOrderToPlate(Order));
                    }
                    else
                    {
                        HeuristicReducedAttackValue += (Heuristic[0] * SignOrderToPlate(Order));
                        HeuristicAttackValue += (Heuristic[1] * SignOrderToPlate(Order));
                        HeuristicReducedSupport += (Heuristic[2] * SignOrderToPlate(Order));
                        HeuristicSelfSupportedValue += (Heuristic[3] * SignOrderToPlate(Order));
                        HeuristicMovementValue += (Heuristic[4] * SignOrderToPlate(Order));
                        HeuristicReducedMovementValue += ((Heuristic[5] + HExchangeInnovation + HExchangeSupport) * SignOrderToPlate(Order));
                        HeuristicCheckedMate += (((HCheck) * SignOrderToPlate(Order)));
                        HeuristicDistributionValue += ((HDistance + HAchmaz + HDoubleAttack) * SignOrderToPlate(Order));
                        HeuristicKingSafe += (HKingSafe * SignOrderToPlate(Order));
                        HeuristicKingDangour += (HKingDangour * SignOrderToPlate(Order));
                        HeuristicFromCenter += (HFromCenter * SignOrderToPlate(Order));
                    }
                }
            }
        }

        //find of "FindMostHeuristicAllReducedSupportIsCurrent" in board
        private int[] MostOfFindMostHeuristicAllReducedSupportInList(bool Before, int RowS, int ColS)
        {
            object O = new object();
            lock (O)
            {
                int[] IsNo = FindMostHeuristicAllReducedSupportIsCurrent(Before, RowS, ColS);

                for (int ii = 0; ii < 8; ii++)
                {
                    for (int jj = 0; jj < 8; jj++)
                    {
                        int[] Is = FindMostHeuristicAllReducedSupportIsCurrent(Before, ii, jj);
                        if (Is[0] > IsNo[0])
                        {
                            return null;
                        }
                    }
                }
                return IsNo;
            }
        }

        //find of most supported objects in enemy
        private int[] FindMostHeuristicAllReducedSupportIsCurrent(bool Before, int RowS, int ColS)
        {
            int[] IsNo = new int[2];
            if (!Before)
            {
                if (HeuristicAllReducedSupportMidel > 0 && HeuristicAllReducedSupportMidel < HeuristicAllReducedSupport.Count)
                {
                    for (int i = HeuristicAllReducedSupportMidel; i < HeuristicAllReducedSupport.Count; i++)
                    {
                        if (HeuristicAllReducedSupport[i][2] == RowS && HeuristicAllReducedSupport[i][3] == ColS)
                        {
                            for (int ii = 0; ii < 8; ii++)
                            {
                                for (int jj = 0; jj < 8; jj++)
                                {
                                    int s = IsNo[0];
                                    IsNo[0] += NoOfExistInReducedSupportList(Before, RowS, ColS, ii, jj);
                                    if (s < IsNo[0])
                                    {
                                        IsNo[1] = i;
                                    }
                                    else
                                    {
                                        IsNo[0] = s;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < HeuristicAllReducedSupport.Count; i++)
                {
                    if (HeuristicAllReducedSupport[i][2] == RowS && HeuristicAllReducedSupport[i][3] == ColS)
                    {
                        for (int ii = 0; ii < 8; ii++)
                        {
                            for (int jj = 0; jj < 8; jj++)
                            {
                                int s = IsNo[0];
                                IsNo[0] += NoOfExistInReducedSupportList(Before, RowS, ColS, ii, jj);
                                if (s < IsNo[0])
                                {
                                    IsNo[1] = i;
                                }
                                else
                                {
                                    IsNo[0] = s;
                                }
                            }
                        }
                    }
                }
            }
            return IsNo;
        }

        //determine if source objects is movable on board
        private bool ObjectMovable(int Row, int Col, int[,] Tab, int Order, Color a)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (Movable(CloneATable(Tab), Row, Col, i, j, a, Order))
                        {
                            return true;
                        }
                        if (Attack(CloneATable(Tab), Row, Col, i, j, a, Order))
                        {
                            return true;
                        }
                    }
                }
                return Is;
            }
        }

        //when exist "s" in list A
        private bool Exist(List<int[]> A, int[] s)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                for (int h = 0; h < A.Count; h++)
                {
                    if (A[h][0] == s[0] && A[h][1] == s[1])
                    {
                        Is = true;
                        break;
                    }
                }
                return Is;
            }
        }

        //when exist complete "s" in list A
        private bool ExistFull(List<int[]> A, int[] s)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                for (int h = 0; h < A.Count; h++)
                {
                    if (A[h][0] == s[0] && A[h][1] == s[1] && A[h][2] == s[2] && A[h][3] == s[3])
                    {
                        Is = true;
                        break;
                    }
                }
                return Is;
            }
        }

        //when exist complete "s" list in list A
        private bool ExistFullDoubleList(List<List<int[]>> A, List<int[]> s)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                for (int t = 0; t < s.Count; t++)
                {
                    bool IsI = false;
                    for (int h = 0; h < A.Count; h++)
                    {
                        if (ExistFull(A[h], s[t]))
                        {
                            IsI = true;
                        }
                    }
                    Is = IsI || Is;
                }
                return Is;
            }
        }

        //return number of un movable objects on board
        private int NoOfObjectNotMovable(int[,] Tab, int Order, Color a, ref int Total, ref int Is)
        {
            object O = new object();
            lock (O)
            {
                List<int[]> IsThere = new List<int[]>();
                for (int Row = 0; Row < 8; Row++)
                {
                    for (int Col = 0; Col < 8; Col++)
                    {
                        if (Order == 1 && Tab[Row, Col] > 0)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (Movable(CloneATable(Tab), Row, Col, i, j, a, Order) && ((TableInitiationPreventionOfMultipleMove[Row, Col] == 0 && TableInitiation[Row, Col] == Tab[Row, Col])))
                                    {
                                        int[] ij = new int[2];
                                        ij[0] = Row;
                                        ij[1] = Col;
                                        if (!(Exist(IsThere, ij)))
                                        {
                                            IsThere.Add(ij);
                                            Is++;
                                        }
                                    }
                                }
                            }
                            Total++;
                        }
                        if (Order == -1 && Tab[Row, Col] < 0)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                for (int j = 0; j < 8; j++)
                                {
                                    if (Movable(CloneATable(Tab), Row, Col, i, j, a, Order) && ((TableInitiationPreventionOfMultipleMove[Row, Col] == 0 && TableInitiation[Row, Col] == Tab[Row, Col])))
                                    {
                                        int[] ij = new int[2];
                                        ij[0] = Row;
                                        ij[1] = Col;
                                        if (!(Exist(IsThere, ij)))
                                        {
                                            IsThere.Add(ij);
                                            Is++;
                                        }
                                    }
                                }
                            }
                            Total++;
                        }
                    }
                }
                Is = Total - Is;
                return Is;
            }
        }

        //specific determination for Thinking main method
        public void CastleThinkingGray(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int[,] TableS, int RowSource, int ColumnSource, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, int RowDestination, int ColumnDestination, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                TableS = CloneATable(TableConst);
                double HeuristicAttackValue = new double();
                double HeuristicMovementValue = new double();
                double HeuristicSelfSupportedValue = new double();
                double HeuristicReducedMovementValue = new double();
                double HeuristicReducedSupport = new double();
                double HeuristicReducedAttackValue = new double();
                double HeuristicDistributionValue = new double();
                double HeuristicKingSafe = new double();
                double HeuristicFromCenter = new double();
                double HeuristicKingDangour = new double(); double HeuristicCheckedMate = new double();
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                int CheckedM = 0; bool PenaltyVCar = false;
                bool Sup = false;
                Task newTask1 = Task.Factory.StartNew(() => SupMethod(CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, ref Sup));
                newTask1.Wait(); newTask1.Dispose();
                if (!Sup)
                {
                    ///Add Table to List of Private.
                    HitNumberCastling.Add(TableS[RowDestination, ColumnDestination]);
                    object OO = new object();
                    lock (OO)
                    {
                        ThinkingRun = true;
                    }
                }
                ///Predict Heuristic.
                object A = new object();
                lock (A)
                {
                    int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, true, Order, 0, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                    newTask1.Wait(); newTask1.Dispose();
                    LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                }
                object A1 = new object();
                lock (A1)
                {
                    if (!Sup) { NumbersOfAllNode++; }
                }
                bool ac=false;
                int Killed = 0;
                newTask1 = Task.Factory.StartNew(() => ac=KilledMethod(ref Killed, Sup, RowSource, ColumnSource, RowDestination, ColumnDestination, ref TableS));
                newTask1.Wait(); newTask1.Dispose();
                if (!ac)
                {
                    HitNumberCastling.RemoveAt(HitNumberCastling.Count - 1);
                    return;
                }

                // if (!Sup)
                {
                    object A3 = new object();
                    lock (A3)
                    {
                        PenaltyVCar = false;
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        newTask1 = Task.Factory.StartNew(() => PenaltyMechanisam(ref PenaltyVCar, ref TmpL, ref TmpW, ref CheckedM, Killed, Kind, CloneATable(TableS), RowSource, ColumnSource, ref Current, DoEnemySelf, PenRegStrore, RowDestination, ColumnDestination));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
                ///Store of Indexes Changes and Table in specific List.
                newTask1 = Task.Factory.StartNew(() => ObjectIndexes(Kind, Sup, RowDestination, ColumnDestination, TableS));
                newTask1.Wait(); newTask1.Dispose();
                ///Wehn Predict of Operation Do operate a Predict of this movments.
                object A5 = new object();
                lock (A5)
                {
                    //Caused this for Stachostic results.
                    if (!Sup)
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled; newTask1 = Task.Factory.StartNew(() => CalculateHeuristics(TmpL, TmpW, false, Order, Killed, CloneATable(TableS), RowSource, ColumnSource, RowDestination, ColumnDestination, color, ref HeuristicAttackValue, ref HeuristicMovementValue, ref HeuristicSelfSupportedValue, ref HeuristicReducedMovementValue, ref HeuristicReducedSupport, ref HeuristicReducedAttackValue, ref HeuristicDistributionValue, ref HeuristicKingSafe, ref HeuristicFromCenter, ref HeuristicKingDangour, ref HeuristicCheckedMate));
                        newTask1.Wait(); newTask1.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
                //Calculate Heuristic and Add to List and Cal Syntax.
                if (!Sup)
                {
                    string H = "";
                    object A6 = new object();
                    lock (A6)
                    {
                        AsS(RowSource, ColumnSource, RowDestination, ColumnDestination);
                        double[] Hu = new double[10];
                        //if (!(IsSup[j]))
                        {
                            //if (IgnoreFromCheckandMateHeuristic)

                            newTask1 = Task.Factory.StartNew(() => HuMethod(ref Hu, HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                            newTask1.Wait(); newTask1.Dispose();
                            H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();
                            HeuristicListCastling.Add(Hu);
                        }
                    }
                    object O4 = new object();
                    lock (O4)
                    {
                    }
                }
                else
                {
                    newTask1 = Task.Factory.StartNew(() => HuMethodSup(HeuristicAttackValue, HeuristicMovementValue, HeuristicSelfSupportedValue, HeuristicReducedMovementValue, HeuristicReducedSupport, HeuristicReducedAttackValue, HeuristicDistributionValue, HeuristicKingSafe, HeuristicFromCenter, HeuristicKingDangour, HeuristicCheckedMate));
                    newTask1.Wait(); newTask1.Dispose();
                    double[] Hu = new double[10];
                    newTask1 = Task.Factory.StartNew(() => HuMethodSup(ref Hu));
                    newTask1.Wait(); newTask1.Dispose();

                    string H = " HAttack:" + ((Hu[0])).ToString() + " HMove:" + ((Hu[1])).ToString() + " HSelSup:" + ((Hu[2])).ToString() + " HCheckedMateDang:" + ((Hu[3])).ToString() + " HKiller:" + ((Hu[4])).ToString() + " HReduAttack:" + ((Hu[5])).ToString() + " HDisFromCurrentEnemyking:" + ((Hu[6])).ToString() + " HKingSafe:" + ((Hu[7])).ToString() + " HObjFromCeneter:" + ((Hu[8])).ToString() + " HKingDang:" + ((Hu[9])).ToString();

                    newTask1 = Task.Factory.StartNew(() => HeuristicInsertion(Kind, RowDestination, ColumnDestination, CloneATable(TableS), Hu));
                    newTask1.Wait(); newTask1.Dispose();
                }
            }
        }

        public void HeuristicPenaltyValuePerform(LearningMachine.QuantumAtamata Current, int Order, ref double HeuristicAttackValue, bool AllDrawClass = false)
        {
            object O1 = new object();
            lock (O1)
            {
                if (LearningVarsObject.Count == 0 || AllDrawClass)
                {
                    if (Order != AllDraw.OrderPlateDraw)
                    {
                        if (Current.IsPenaltyAction() == 0)
                        {
                            HeuristicAttackValue--;
                        }
                    }
                    else
                        if (AllDraw.OrderPlateDraw != Order)
                    {
                        if (Current.IsPenaltyAction() == 0)
                        {
                            HeuristicAttackValue++;
                        }
                    }
                    if (Order != AllDraw.OrderPlateDraw)
                    {
                        if (Current.IsRewardAction() == 1)
                        {
                            HeuristicAttackValue++;
                        }
                    }
                    else
                        if (AllDraw.OrderPlateDraw != Order)
                    {
                        if (Current.IsRewardAction() == 1)
                        {
                            HeuristicAttackValue++;
                        }
                    }
                }
                else
                {
                    if ((LearningVarsObject[LearningVarsObject.Count - 1][1] && !LearningVarsObject[LearningVarsObject.Count - 1][4]))
                    {
                        if (Order != AllDraw.OrderPlateDraw)
                        {
                            if (Current.IsPenaltyAction() == 0)
                            {
                                HeuristicAttackValue -= 2;
                            }
                        }
                        else
                          if (AllDraw.OrderPlateDraw != Order)
                        {
                            if (Current.IsPenaltyAction() == 0)
                            {
                                HeuristicAttackValue += 2;
                            }
                        }
                        if (Order != AllDraw.OrderPlateDraw)
                        {
                            if (Current.IsRewardAction() == 1)
                            {
                                HeuristicAttackValue += 2;
                            }
                        }
                        else
                            if (AllDraw.OrderPlateDraw != Order)
                        {
                            if (Current.IsRewardAction() == 1)
                            {
                                HeuristicAttackValue -= 2;
                            }
                        }
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingSoldierbase(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int i, int j, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O = new object();
            lock (O)
            {
                int[,] TableS = CloneATable(TableConst);
                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                if (Scop(ii, jj, i, j, 1) && System.Math.Abs(TableS[ii, jj]) == 1 && System.Math.Abs(Kind) == 1)
                {
                    Order = ord;
                    int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                    Task newTask = Task.Factory.StartNew(() => SolderThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, i, j, Castle));

                    newTask.Wait(); newTask.Dispose();
                    LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingSoldier(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Parallel.For(ii - 2, ii + 3, i =>
                //for (int i = ii - 2; i < ii + 3; i++)
                {
                    Parallel.For(jj - 2, jj + 3, j =>
                    //for (int j = jj - 2; j < jj + 3; j++)
                    {
                        int[,] TableS = new int[8, 8];
                        object O = new object();
                        lock (O)
                        {
                            if (Scop(ii, jj, i, j, 1))
                            {
                                for (int RowS = 0; RowS < 8; RowS++)
                                {
                                    for (int ColS = 0; ColS < 8; ColS++)
                                    {
                                        TableS[RowS, ColS] = TableConst[RowS, ColS];
                                    }
                                }

                                Task newTask = Task.Factory.StartNew(() => ThinkingSoldierbase(ref TmpL, ref TmpW, ord, ii, jj, i, j, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                                newTask.Wait(); newTask.Dispose();
                            }
                        }
                    });
                });
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
        }

        //specific determination for thinking main method
        public void ThinkingElephantbase(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int i, int j, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                object O = new object();
                lock (O)
                {
                    ///Else for Elephant Thinking.
                    if (Scop(ii, jj, i, j, 2) && System.Math.Abs(TableS[ii, jj]) == 2 && System.Math.Abs(Kind) == 2)
                    {
                        Order = ord;
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        Task newTask = Task.Factory.StartNew(() => ElefantThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, i, j, Castle));

                        newTask.Wait(); newTask.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingElephant(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O2 = new object();
            lock (O2)
            {
                object O1 = new object();
                lock (O1)
                {
                    int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                    Parallel.For(0, 8, i =>
                    //for (int i = 0; i < 8; i++)
                    {
                        Parallel.For(0, 8, j =>
                        //for (int j = 0; j < 8; j++)
                        {
                            object O = new object();
                            lock (O)
                            {
                                if (Scop(ii, jj, i, j, 2))
                                {
                                    Task newTask = Task.Factory.StartNew(() => ThinkingElephantbase(ref TmpL, ref TmpW, ord, ii, jj, i, j, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                                    newTask.Wait(); newTask.Dispose();
                                }
                            }
                        });
                    });
                    LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourseOne(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                object O = new object();
                lock (O)
                {
                    Order = ord;
                    if (Scop(ii, jj, ii + 2, jj + 1, 3))
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        Task newTask = Task.Factory.StartNew(() => HourseThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, ii + 2, jj + 1, Castle));

                        newTask.Wait(); newTask.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourseTwo(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);

                Order = ord;
                if (Scop(ii, jj, ii - 2, jj - 1, 3))
                {
                    int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                    Task newTask = Task.Factory.StartNew(() => HourseThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, ii - 2, jj - 1, Castle));

                    newTask.Wait(); newTask.Dispose();
                    LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourseThree(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                object O = new object();
                lock (O)
                {
                    Order = ord;
                    if (Scop(ii, jj, ii + 2, jj - 1, 3))
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        Task newTask = Task.Factory.StartNew(() => HourseThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, ii + 2, jj - 1, Castle));

                        newTask.Wait(); newTask.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourseFour(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);

                Order = ord;
                if (Scop(ii, jj, ii - 2, jj + 1, 3))
                {
                    int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                    Task newTask = Task.Factory.StartNew(() => HourseThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, ii - 2, jj + 1, Castle));

                    newTask.Wait(); newTask.Dispose();
                    LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourseFive(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                object O = new object();
                lock (O)
                {
                    Order = ord;
                    if (Scop(ii, jj, ii + 1, jj + 2, 3))
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        Task newTask = Task.Factory.StartNew(() => HourseThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, ii + 1, jj + 2, Castle));

                        newTask.Wait(); newTask.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourseSix(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                object O = new object();
                lock (O)
                {
                    Order = ord;
                    if (Scop(ii, jj, ii - 1, jj - 2, 3))
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        Task newTask = Task.Factory.StartNew(() => HourseThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, ii - 1, jj - 2, Castle));

                        newTask.Wait(); newTask.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourseSeven(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O = new object();
            lock (O)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                object O111 = new object();
                lock (O111)
                {
                    Order = ord;
                    if (Scop(ii, jj, ii + 1, jj - 2, 3))
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        Task newTask = Task.Factory.StartNew(() => HourseThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, ii + 1, jj - 2, Castle));

                        newTask.Wait(); newTask.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourseEight(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O111 = new object();
            lock (O111)
            {
                int[,] TableS = CloneATable(TableConst);

                ///Initiate a Local Variables.
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                object O = new object();
                lock (O)
                {
                    Order = ord;
                    if (Scop(ii, jj, ii - 1, jj + 2, 3))
                    {
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        Task newTask = Task.Factory.StartNew(() => HourseThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, ii - 1, jj + 2, Castle));

                        newTask.Wait(); newTask.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingHourse(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O = new object();
            lock (O)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask = Task.Factory.StartNew(() => ThinkingHourseOne(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                newTask.Wait(); newTask.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
            object O1 = new object();
            lock (O1)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask = Task.Factory.StartNew(() => ThinkingHourseTwo(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                newTask.Wait(); newTask.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
            object O2 = new object();
            lock (O2)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask = Task.Factory.StartNew(() => ThinkingHourseThree(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                newTask.Wait(); newTask.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
            object O3 = new object();
            lock (O3)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask = Task.Factory.StartNew(() => ThinkingHourseFour(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                newTask.Wait(); newTask.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
            object O4 = new object();
            lock (O4)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask = Task.Factory.StartNew(() => ThinkingHourseFive(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                newTask.Wait(); newTask.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
            object O5 = new object();
            lock (O5)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask = Task.Factory.StartNew(() => ThinkingHourseSix(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                newTask.Wait(); newTask.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
            object O6 = new object();
            lock (O6)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask = Task.Factory.StartNew(() => ThinkingHourseSeven(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                newTask.Wait(); newTask.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
            object O7 = new object();
            lock (O7)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask = Task.Factory.StartNew(() => ThinkingHourseEight(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                newTask.Wait(); newTask.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
        }

        //specific determination for thinking main method
        public void ThinkingCastleOne(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, i =>
                ParallelOptions po = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                };

                Parallel.For(0, 8, i =>
                {
                    object O = new object();
                    lock (O)
                    {
                        int j = jj;
                        ///Initiate a Local Variables.
                        int[,] TableS = CloneATable(TableConst);
                        ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                        LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                        if (Scop(ii, jj, i, j, 4) && System.Math.Abs(TableS[ii, jj]) == 4 && System.Math.Abs(Kind) == 4)
                        {
                            Order = ord;
                            Task newTask = Task.Factory.StartNew(() => CastlesThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, i, j, Castle));

                            newTask.Wait(); newTask.Dispose();
                        }
                    }
                });
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
        }

        //specific determination for thinking main method
        public void ThinkingCastleTow(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            //==================
            object O1 = new object();
            lock (O1)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                ////ParallelOptions po = new ParallelOptions();       po.MaxDegreeOfParallelism =PlatformHelper.ProcessorCount;                    Parallel.For(0, 8, j =>
                ParallelOptions po = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                };

                Parallel.For(0, 8, j =>
                {
                    object O = new object();
                    lock (O)
                    {
                        int i = ii;
                        ///Initiate a Local Variables.
                        int[,] TableS = CloneATable(TableConst);
                        ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                        LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);

                        if (Scop(ii, jj, i, j, 4) && System.Math.Abs(TableS[ii, jj]) == 4 && System.Math.Abs(Kind) == 4)
                        {
                            Order = ord;
                            Task newTask = Task.Factory.StartNew(() => CastlesThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, i, j, Castle));
                            newTask.Wait(); newTask.Dispose();
                        }
                    }
                });
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
        }

        //specific determination for thinking main method
        public void ThinkingCastle(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O = new object();
            lock (O)
            {
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Task newTask1 = Task.Factory.StartNew(() => ThinkingCastleOne(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));
                newTask1.Wait(); newTask1.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                Task newTask2 = Task.Factory.StartNew(() => ThinkingCastleTow(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));
                newTask2.Wait(); newTask2.Dispose();
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
        }

        //specific determination for thinking main method
        public void ThinkingMinisterbase(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int i, int j, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                ///Initiate a Local Variables.
                int[,] TableS = CloneATable(TableConst);
                ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                object O = new object();
                lock (O)
                {
                    if (Scop(ii, jj, i, j, 5) && System.Math.Abs(TableS[ii, jj]) == 5 && System.Math.Abs(Kind) == 5)
                    {
                        Order = ord;
                        int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                        Task newTask = Task.Factory.StartNew(() => MinisterThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, i, j, Castle));

                        newTask.Wait(); newTask.Dispose();
                        LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingMinister(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                int[,] TableS = new int[8, 8];
                int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                Parallel.For(0, 8, i =>
                //for (int i = 0; i < 8; i++)
                {
                    Parallel.For(0, 8, j =>
                    //for (int j = 0; j < 8; j++)
                    {
                        TableS = CloneATable(TableConst);
                        object O = new object();
                        lock (O)
                        {
                            Task newTask = Task.Factory.StartNew(() => ThinkingMinisterbase(ref TmpL, ref TmpW, ord, ii, jj, i, j, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                        }
                    });
                });
                LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
            }
        }

        //specific determination for thinking main method
        public void ThinkingCastleBrown(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O = new object();
            lock (O)
            {
                for (int i = ii - 2; i <= ii + 2; i++)
                {
                    ///Initiate a Local Variables.
                    int[,] TableS = CloneATable(TableConst);

                    ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                    LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);

                    if (Scop(ii, jj, i, jj, -7) && System.Math.Abs(TableS[ii, jj]) == 6 && System.Math.Abs(Kind) == 7)
                    {///Calculate of Castles of Brown.
                        if ((new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, -7, CloneATable(TableS), Order)).Rules(ii, jj, i, jj, -7) && (ChessRules.CastlingAllowedBrown))
                        {
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                            Task newTask = Task.Factory.StartNew(() => CastleThinkingBrown(ref TmpL, ref TmpW, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, i, jj, Castle));

                            newTask.Wait(); newTask.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingCastleGray(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O = new object();
            lock (O)
            {
                for (int i = ii - 2; i <= ii + 2; i++)
                {
                    ///Initiate a Local Variables.
                    int[,] TableS = CloneATable(TableConst);

                    if (Scop(ii, jj, i, jj, 7) && System.Math.Abs(TableS[ii, jj]) == 6 && System.Math.Abs(Kind) == 7)
                    { ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                        LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);

                        if ((new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, 7, CloneATable(TableS), Order)).Rules(ii, jj, i, jj, 7) && (ChessRules.CastlingAllowedGray))
                        {
                            int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                            Task newTask = Task.Factory.StartNew(() => CastleThinkingGray(ref TmpL, ref TmpW, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, i, jj, Castle));

                            newTask.Wait(); newTask.Dispose();
                            LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                        }
                    }
                }
            }
        }

        //specific determination for thinking main method
        public void ThinkingKing(ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled, int ord, int ii, int jj, int DummyOrder, int DummyCurrentOrder, bool DoEnemySelf, bool PenRegStrore, bool EnemyCheckMateActionsString, bool Castle)
        {
            object O1 = new object();
            lock (O1)
            {
                object O = new object();
                lock (O)
                {
                    int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                    Parallel.For(ii - 1, ii + 2, i =>
                    //for (int i = ii - 1; i < ii + 2; i++)
                    {
                        Parallel.For(jj - 1, jj + 2, j =>
                        //for (int j = jj - 1; j < jj + 2; j++)
                        {
                            if (i == ii && j == jj)
                            {
                                return;
                            }

                            ///Initiate a Local Variables.
                            int[,] TableS = CloneATable(TableConst);
                            ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                            LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                            if (Scop(ii, jj, i, j, 6) && System.Math.Abs(TableS[ii, jj]) == 6 && System.Math.Abs(Kind) == 6)
                            {
                                Order = ord;
                                Task newTask = Task.Factory.StartNew(() => KingThinkingChess(ref TmpL, ref TmpW, DummyOrder, DummyCurrentOrder, CloneATable(TableS), ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, i, j, Castle));

                                newTask.Wait(); newTask.Dispose();
                            }
                        });
                    });
                    LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                }
            }
        }

        ///Kernel of Thinking
        //specific thinking main method
        private void ThinkingWaite()
        {
            object O = new object();
            lock (O)
            {
                while (!ThinkingBegin)
                {
                    if (AllDraw.NumberOfLeafComputation != -1)
                    {
                        break;
                    }
                }
            }
        }

        //operantinal of creation of current deeper node and set string making
        private void FullGameThinkingTreeInitialization(AllDraw THIS, int ik, int j, int Order, int kind)
        {
            //soldier
            if (kind == 1)
            {
                //when valid do create of deeper node and string making
                if (TableListSolder.Count > AStarGreedy.Count)
                {
                    if (AStarGreedy.Count == 0)
                    {
                        AStarGreedy = new List<AllDraw>();
                    }

                    AStarGreedy.Add(new AllDraw(Order * -1, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged));
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Clear();
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Add(CloneATable(TableListSolder[j]));
                    AStarGreedy[AStarGreedy.Count - 1].SetRowColumn(0);
                    AStarGreedy[AStarGreedy.Count - 1].AStarGreedyString = THIS;
                }
            }
            else if (kind == 2)//elephant
            {
                //when valid do create of deeper node and string making
                if (TableListElefant.Count > AStarGreedy.Count)
                {
                    if (AStarGreedy.Count == 0)
                    {
                        AStarGreedy = new List<AllDraw>();
                    }

                    AStarGreedy.Add(new AllDraw(Order * -1, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged));
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Clear();
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Add(CloneATable(TableListElefant[j]));
                    AStarGreedy[AStarGreedy.Count - 1].SetRowColumn(0);
                    AStarGreedy[AStarGreedy.Count - 1].AStarGreedyString = THIS;
                }
            }
            else if (kind == 3)//hourse
            {
                //when valid do create of deeper node and string making
                if (TableListHourse.Count > AStarGreedy.Count)
                {
                    if (AStarGreedy.Count == 0)
                    {
                        AStarGreedy = new List<AllDraw>();
                    }

                    AStarGreedy.Add(new AllDraw(Order * -1, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged));
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Clear();
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Add(CloneATable(TableListHourse[j]));
                    AStarGreedy[AStarGreedy.Count - 1].SetRowColumn(0);
                    AStarGreedy[AStarGreedy.Count - 1].AStarGreedyString = THIS;
                }
            }
            else if (kind == 4)//Castle
            {
                //when valid do create of deeper node and string making
                if (TableListCastle.Count > AStarGreedy.Count)
                {
                    if (AStarGreedy.Count == 0)
                    {
                        AStarGreedy = new List<AllDraw>();
                    }

                    AStarGreedy.Add(new AllDraw(Order * -1, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged));
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Clear();
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Add(CloneATable(TableListCastle[j]));
                    AStarGreedy[AStarGreedy.Count - 1].SetRowColumn(0);
                    AStarGreedy[AStarGreedy.Count - 1].AStarGreedyString = THIS;
                }
            }
            else if (kind == 5)//minister
            {
                //when valid do create of deeper node and string making
                if (TableListMinister.Count > AStarGreedy.Count)
                {
                    if (AStarGreedy.Count == 0)
                    {
                        AStarGreedy = new List<AllDraw>();
                    }

                    AStarGreedy.Add(new AllDraw(Order * -1, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged));
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Clear();
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Add(CloneATable(TableListMinister[j]));
                    AStarGreedy[AStarGreedy.Count - 1].SetRowColumn(0);
                    AStarGreedy[AStarGreedy.Count - 1].AStarGreedyString = THIS;
                }
            }
            else if (kind == 6)//king
            {
                //when valid do create of deeper node and string making
                if (TableListKing.Count > AStarGreedy.Count)
                {
                    if (AStarGreedy.Count == 0)
                    {
                        AStarGreedy = new List<AllDraw>();
                    }

                    AStarGreedy.Add(new AllDraw(Order * -1, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged));
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Clear();
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Add(CloneATable(TableListKing[j]));
                    AStarGreedy[AStarGreedy.Count - 1].SetRowColumn(0);
                    AStarGreedy[AStarGreedy.Count - 1].AStarGreedyString = THIS;
                }
            }
            else if (kind == 7 || kind == -7)//king
            {
                //when valid do create of deeper node and string making
                if (TableListKing.Count > AStarGreedy.Count)
                {
                    if (AStarGreedy.Count == 0)
                    {
                        AStarGreedy = new List<AllDraw>();
                    }

                    AStarGreedy.Add(new AllDraw(Order * -1, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged));
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Clear();
                    AStarGreedy[AStarGreedy.Count - 1].TableList.Add(CloneATable(TableListCastling[j]));
                    AStarGreedy[AStarGreedy.Count - 1].SetRowColumn(0);
                    AStarGreedy[AStarGreedy.Count - 1].AStarGreedyString = THIS;
                }
            }
        }

        //Deeper than deeper
        private void ThinkingFullGame(int iAStarGreedy, AllDraw THIS)
        {
            object O = new object();
            lock (O)
            {
                if (AllDraw.Deeperthandeeper)
                {
                    FullGameAllow = true;
                    if (Kind == 1)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, TableListSolder.Count, i =>
                        {
                            FullGameThinkingTreeInitialization(THIS, iIndex, i, Order, Kind);
                            AStarGreedy[i].InitiateAStarGreedyt(iAStarGreedy, 0, 0, ColorOpposite(color), TableListSolder[i], Order * -1, false, false, 0);
                        });
                    }
                    else
                    if (Kind == 2)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, TableListElefant.Count, i =>
                        {
                            FullGameThinkingTreeInitialization(THIS, iIndex, i, Order, Kind);
                            AStarGreedy[i].InitiateAStarGreedyt(iAStarGreedy, 0, 0, ColorOpposite(color), TableListElefant[i], Order * -1, false, false, 0);
                        });
                    }
                    else
                    if (Kind == 3)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, TableListHourse.Count, i =>
                        {
                            FullGameThinkingTreeInitialization(THIS, iIndex, i, Order, Kind);
                            AStarGreedy[i].InitiateAStarGreedyt(iAStarGreedy, 0, 0, ColorOpposite(color), TableListHourse[i], Order * -1, false, false, 0);
                        });
                    }
                    else
                    if (Kind == 4)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, TableListCastle.Count, i =>
                        {
                            FullGameThinkingTreeInitialization(THIS, iIndex, i, Order, Kind);
                            AStarGreedy[i].InitiateAStarGreedyt(iAStarGreedy, 0, 0, ColorOpposite(color), TableListCastle[i], Order * -1, false, false, 0);
                        });
                    }
                    else
                    if (Kind == 5)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, TableListMinister.Count, i =>
                        {
                            FullGameThinkingTreeInitialization(THIS, iIndex, i, Order, Kind);
                            AStarGreedy[i].InitiateAStarGreedyt(iAStarGreedy, 0, 0, ColorOpposite(color), TableListMinister[i], Order * -1, false, false, 0);
                        });
                    }
                    else
                        if (Kind == 6)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, TableListKing.Count, i =>
                        {
                            FullGameThinkingTreeInitialization(THIS, iIndex, i, Order, Kind);
                            AStarGreedy[i].InitiateAStarGreedyt(iAStarGreedy, 0, 0, ColorOpposite(color), TableListKing[i], Order * -1, false, false, 0);
                        });
                    }
                    else
                        if (Kind == 7 || Kind == -7)
                    {
                        ParallelOptions po = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, TableListCastling.Count, i =>
                        {
                            FullGameThinkingTreeInitialization(THIS, iIndex, i, Order, Kind);
                            AStarGreedy[i].InitiateAStarGreedyt(iAStarGreedy, 0, 0, ColorOpposite(color), TableListCastling[i], Order * -1, false, false, 0);
                        });
                    }
                    FullGameAllow = false;
                }
            }
        }

        private Color ColorOpposite(Color a)
        {
            if (a == Color.Gray)
            {
                return Color.Brown;
            }

            return Color.Gray;
        }

        private bool MovableAllObjectsListMethos(int RowS, int ColS)
        {
            bool Is = false;
            if (MovableAllObjectsList.Count == 8)
            {
                if (MovableAllObjectsList[RowS].Count == 8)
                {
                    for (int i = 0; i < MovableAllObjectsList[RowS][ColS].Count; i++)
                    {
                        if (MovableAllObjectsList[RowS][ColS][i][5] == 1)
                        {
                            Is = true;
                        }
                    }
                }
            }
            return Is;
        }

        private void MovableAllObjectsListMethos(int[,] TableS, bool Before, int RowS, int ColS, int RowD, int ColD, int con, int movable = 1)
        {
            try
            {
                if (RowS < 0 || ColS < 0 || RowD < 0 || ColD < 0 || RowS > 7 | ColS > 7 || RowD > 7 || ColD > 7)
                    return;
                if (Before)
                {
                    if (MovableAllObjectsList.Count == 0)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            MovableAllObjectsList.Add(new List<List<int[]>>());

                        }
                        for (int i = 0; i < 8; i++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                MovableAllObjectsList[i].Add(new List<int[]>());
                            }
                        }
                    }
                    int[] B = new int[6];
                    B[0] = RowS;
                    B[1] = ColS;
                    B[2] = RowD;
                    B[3] = ColD;
                    if (con == 1)
                    {
                        B[4] = TableS[RowS, ColS];
                    }
                    else
                    {
                        B[4] = con;
                    }

                    B[5] = movable;
                    if (MovableAllObjectsList.Count < RowS)
                    {
                        if (MovableAllObjectsList[RowS].Count < ColS)
                        {
                            for (int i = 0; i < MovableAllObjectsList[RowS][ColS].Count; i++)
                            {
                                if (MovableAllObjectsList[RowS][ColS][i][2] == RowS && MovableAllObjectsList[RowS][ColS][i][3] == ColS && MovableAllObjectsList[RowS][ColS][i][4] == TableS[RowS, ColS])
                                {
                                    MovableAllObjectsList[RowS][ColS].RemoveAt(i);
                                }
                            }
                        }
                        MovableAllObjectsList[RowS][ColS].Add(B);
                    }
                }
                else
                {
                    if (MovableAllObjectsList.Count == 0)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            MovableAllObjectsList.Add(new List<List<int[]>>());

                        }
                        for (int i = 0; i < 8; i++)
                        {
                            for (int k = 0; k < 8; k++)
                            {
                                MovableAllObjectsList[i].Add(new List<int[]>());
                            }
                        }
                    }
                    int[] B = new int[6];
                    B[0] = RowS;
                    B[1] = ColS;
                    B[2] = RowD;
                    B[3] = ColD;
                    if (con == 1)
                    {
                        B[4] = TableS[RowD, ColD];
                    }
                    else
                    {
                        B[4] = con;
                    }

                    B[5] = movable;

                    if (MovableAllObjectsList.Count < RowS)
                    {
                        if (MovableAllObjectsList[RowS].Count < ColS)
                        {
                            for (int i = 0; i < MovableAllObjectsList[RowS][ColS].Count; i++)
                            {
                                if (MovableAllObjectsList[RowS][ColS][i][2] == RowS && MovableAllObjectsList[RowS][ColS][i][3] == ColS && MovableAllObjectsList[RowS][ColS][i][4] == TableS[RowS, ColS])
                                {
                                    MovableAllObjectsList[RowS][ColS].RemoveAt(i);
                                }
                            }
                            MovableAllObjectsList[RowD][ColD].Add(B);
                        }
                    }
                }
            }catch(Exception t) { Log(t); }
        }

        public void Thinking(int iAStarGreedy, AllDraw THIS, ref int[] LoseOcuuredatChiled, ref int WinOcuuredatChiled)
        {
            try
            {
                if (Order != AllDraw.OrderPlateDraw)
                {
                    //Combination of tow elephant s powerfull of tow hourse
                    if (Kind == 2)
                    {
                        RationalPenalty *= 2;
                        RationalRegard *= 2;
                    }
                    if (Kind == 3)
                    {
                        RationalPenalty /= 2;
                        RationalRegard /= 2;
                    }
                }
                else
                {
                    //defensive of tow elephant and primitative of tow hourse
                    if (Kind == 2)
                    {
                        RationalPenalty /= 2;
                        RationalRegard /= 2;
                    }
                    if (Kind == 3)
                    {
                        RationalPenalty *= 2;
                        RationalRegard *= 2;
                    }
                }

                int ord = Order;
                object O = new object();
                lock (O)
                {
                    /*if (CurrentAStarGredyMax > AllDraw.MaxAStarGreedy)
                    {
                        ThinkingBegin = false;
                        ThinkingFinished = true;

                        return;
                    }*/
                    Thread t = new Thread(new ThreadStart(ThinkingWaite));
                    t.Start();
                    t.Join();

                    NumberOfPenalties = 0;
                    SetObjectNumbers(CloneATable(TableConst));
                    bool PenRegStrore = true;
                    // if (Order == AllDraw.OrderPlateDraw)

                    object O1 = new object();
                    lock (O1)
                    {
                        BeginThread++;
                    }
                    {
                        /*if (//CheckMateOcuured ||
                            FoundFirstSelfMating > AllDraw.MaxAStarGreedy
                            )
                        {
                            object O2 = new object();
                            lock (O2)
                            {
                                ThinkingBegin = false;
                                ThinkingFinished = true;
                                EndThread++;
                            }
                            return;
                        }
                        if (//CheckMateOcuured ||
                            FoundFirstMating > AllDraw.MaxAStarGreedy
                            )
                        {
                            object O2 = new object();
                            lock (O2)
                            {
                                ThinkingBegin = false;
                                ThinkingFinished = true;
                                EndThread++;
                            }
                            return;
                        }*/
                    }
                    int DummyOrder = Order;
                    int DummyCurrentOrder = ChessRules.CurrentOrder;
                    //Initiate Locallly Global Variables.
                    IndexSoldier = 0;
                    IndexElefant = 0;
                    IndexHourse = 0;
                    IndexCastle = 0;
                    IndexMinister = 0;
                    IndexKing = 0;
                    int[,] TableS = new int[8, 8];
                    ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                    ///Most Dot Net FrameWork Hot Path
                    ///Create A Clone of Current Table Constant in ThinkingChess Object Tasble.
                    ///For Stored Location of Objects.
                    int ii = Row;
                    int jj = Column;
                    /*if (//CheckMateOcuured ||
                    FoundFirstMating > AllDraw.MaxAStarGreedy
                        )
                    {
                        object O2 = new object();
                        lock (O2)
                        {
                            ThinkingFinished = true;
                            ThinkingBegin = false;
                            EndThread++;
                        }
                        return;
                    }
                    if (//CheckMateOcuured ||
                    FoundFirstSelfMating > AllDraw.MaxAStarGreedy
                        )
                    {
                        object O2 = new object();
                        lock (O2)
                        {
                            ThinkingFinished = true;
                            ThinkingBegin = false;
                            EndThread++;
                        }
                        return;
                    }*/
                    IgnoreObjectDangour = -1;
                    ///Initiate a Local Variables.
                    TableS = new int[8, 8];
                    ///"Inizialization of This New Class (Current is Dynamic class Object) is MalFunction (Constant Variable Count).
                    LearningMachine.QuantumAtamata Current = new LearningMachine.QuantumAtamata(3, 3, 3);
                    ///Most Dot Net FrameWork Hot Path
                    ///Create A Clone of Current Table Constant in ThinkingChess Object Tasble.
                    for (int RowS = 0; RowS < 8; RowS++)
                    {
                        for (int ColS = 0; ColS < 8; ColS++)
                        {
                            TableS[RowS, ColS] = TableConst[RowS, ColS];
                        }
                    }

                    ///Deterimine for Castle King Wrongly Desision.
                    bool Castle = false;
                    bool DoEnemySelf = true;
                    ChessRules AAA = new ChessRules(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, TableS[ii, jj], CloneATable(TableS), Order);
                    if (AAA.CheckMate(CloneATable(TableS), Order))
                    {
                        if (AAA.CheckMateGray || AAA.CheckMateBrown)
                        {
                            object O2 = new object();
                            lock (O2)
                            {
                                ThinkingFinished = true;
                                CheckMateOcuured = true;
                                if (//(AAA.CheckGray && AllDraw.OrderPlateDraw == 1) || (AAA.CheckBrown && AllDraw.OrderPlateDraw == -1) ||
                                (AAA.CheckMateGray && AllDraw.OrderPlateDraw == 1) || (AAA.CheckMateBrown && AllDraw.OrderPlateDraw == -1))
                                {
                                    FoundFirstSelfMating++;
                                    if (Order == AllDraw.OrderPlateDraw)
                                    {
                                        LoseOcuuredatChiled[0] = -2;
                                    }

                                    IsThereMateOfSelf.Add(true);
                                }
                                if ((AAA.CheckMateGray && AllDraw.OrderPlateDraw == -1) || (AAA.CheckMateBrown && AllDraw.OrderPlateDraw == 1))
                                {
                                    if (Order == AllDraw.OrderPlateDraw)
                                    {
                                        WinOcuuredatChiled = 3;
                                    }

                                    FoundFirstMating++;
                                    IsThereMateOfEnemy.Add(true);
                                }
                                EndThread++;
                            }
                            return;
                        }
                    }
                    if (Order == -1 && AAA.CheckBrown)
                    {
                        IgnoreObjectDangour = 0;
                        IsCheck = true;
                        DoEnemySelf = false;
                    }
                    if (Order == -1 && AAA.CheckGray)
                    {
                        IgnoreObjectDangour = 0;
                        IsCheck = true;
                        DoEnemySelf = false;
                    }
                    if (Order == -1 && AAA.CheckGray)
                    {
                        IgnoreObjectDangour = 0;
                        IsCheck = true;
                        DoEnemySelf = false;
                    }
                    if (Order == 1 && AAA.CheckBrown)
                    {
                        IgnoreObjectDangour = 0;
                        IsCheck = true;
                        DoEnemySelf = false;
                    }
                    //When Root is CheckMate Benefit of Current Order No Consideration.
                    int CDumnmy = ChessRules.CurrentOrder;
                    bool EnemyCheckMateActionsString = false;
                    DummyOrder = Order;
                    DummyCurrentOrder = Order;
                    ChessRules.CurrentOrder = DummyCurrentOrder;
                    ///Calculate Castles of Gray King.
                    ///
                    int[] TmpL = LoseOcuuredatChiled; int TmpW = WinOcuuredatChiled;
                    switch (Kind)
                    {
                        case 7:

                            Task newTask = Task.Factory.StartNew(() => ThinkingCastleGray(ref TmpL, ref TmpW, ord, ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                            break;

                        case -7:

                            newTask = Task.Factory.StartNew(() => ThinkingCastleBrown(ref TmpL, ref TmpW, ord, ii, jj, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                            break;

                        case 1:///For Soldier Thinking

                            newTask = Task.Factory.StartNew(() => ThinkingSoldier(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                            break;

                        case 2:///For Elephant Thinking

                            newTask = Task.Factory.StartNew(() => ThinkingElephant(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                            break;

                        case 3:///For Hourse Thinking

                            newTask = Task.Factory.StartNew(() => ThinkingHourse(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                            break;
                        ///Else For Castles Thinking.
                        case 4:///For Castle Thinking
                            newTask = Task.Factory.StartNew(() => ThinkingCastle(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                            break;
                        ///Else for Minister Thinkings.
                        case 5:///For Minister Thinking

                            newTask = Task.Factory.StartNew(() => ThinkingMinister(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                            break;
                        ///Else For Kings Thinkings.
                        case 6:///For King Thinking
                            newTask = Task.Factory.StartNew(() => ThinkingKing(ref TmpL, ref TmpW, ord, ii, jj, DummyOrder, DummyCurrentOrder, DoEnemySelf, PenRegStrore, EnemyCheckMateActionsString, Castle));

                            newTask.Wait(); newTask.Dispose();
                            break;
                    }
                    LoseOcuuredatChiled[0] += TmpL[0]; WinOcuuredatChiled += TmpW;
                    object O3 = new object();
                    lock (O3)
                    {
                        ///Initiate Global Varibales at END.
                        ThinkingBegin = false;
                        ///This Variable Not Work!
                        ThinkingFinished = true;
                        Order = DummyOrder;
                        ChessRules.CurrentOrder = DummyCurrentOrder;
                        EndThread++;
                    }
                    //
                    ///Return at End.
                }
                ThinkingFullGame(iAStarGreedy, THIS);
                TowDistrurbProperUsePreferNotToClose(ref LoseOcuuredatChiled, TableConst);
                TowDistrurbProperUse(ref LoseOcuuredatChiled);
            }
            catch (Exception t)
            {
                Log(t);
                ThinkingBegin = false;
                ThinkingFinished = true;
            }
            return;
        }

        public bool IsTheeAtleastMAteSelf()
        {
            bool Is = false;
            for (int i = 0; i < IsThereMateOfSelf.Count; i++)
            {
                Is = Is || IsThereMateOfSelf[i];
            }

            return Is;
        }

        public void TowDistrurbProperUse(ref int[] LoseOcuuredatChiled)
        {
            object OI = new object();
            lock (OI)
            {



                if (!IsTheeAtleastMAteSelf())
                {
                    if (RemoveOfDisturbIndex == -1)
                    {
                        if (IsSupHu.Count > 0)
                        {
                            bool IsSup = true;
                            for (int i = 0; i < IsSupHu.Count; i++)
                            {
                                //if (IsThereMateOfSelf[i])
                                //LoseChiled[i] = -8;
                                IsSup = IsSup && IsSupHu[i];
                            }
                            if (IsSup)
                            {
                                if (HeuristicAllReducedAttackedMidel - 1 == (HeuristicAllReducedAttacked.Count - HeuristicAllReducedAttackedMidel))
                                {
                                    int i = IndexOfMoved();
                                    if (i != -1)
                                    {
                                        RemoveOfDisturbIndex = IndexOfIsSupTRUE(Kind, HeuristicAllReducedAttacked[i][0], HeuristicAllReducedAttacked[i][1]);
                                        if (RemoveOfDisturbIndex != -1)
                                        {
                                            IsSupHu[RemoveOfDisturbIndex] = false;
                                        }
                                        else
                                        {
                                            if (Order == AllDraw.OrderPlateDraw)
                                            {
                                                LoseOcuuredatChiled[0] = -4;
                                            }
                                        }
                                    }
                                    /* else
                                     {
                                         if (Order == AllDraw.OrderPlateDraw)
                                             LoseOcuuredatChiled[0] = -4;
                                     }*/
                                }
                                /*  else
                                  {
                                      if (Order == AllDraw.OrderPlateDraw)
                                          LoseOcuuredatChiled[0] = -4;
                                  }*/
                            }
                        }
                    }
                }

            }
        }

        public void TowDistrurbProperUsePreferNotToClose(ref int[] LoseOcuuredatChiled, int[,] Tab)
        {
            object OI = new object();
            lock (OI)
            {

                if (!IsTheeAtleastMAteSelf())
                {
                    if (RemoveOfDisturbIndex == -1)
                    {
                        if (IsSupHu.Count > 0)
                        {
                            bool IsSup = true;
                            for (int i = 0; i < IsSupHu.Count; i++)
                            {
                                //if (IsThereMateOfSelf[i])
                                //LoseChiled[i] = -8;
                                IsSup = IsSup && IsSupHu[i];
                            }
                            if (IsSup)
                            {
                                if (HeuristicDoubleDefenceIndexInOnGameMidle > (HeuristicDoubleDefenceIndexInOnGame.Count - HeuristicDoubleDefenceIndexInOnGameMidle)
                                    //(HeuristicDoubleDefenceIndexInOnGame.Count - HeuristicDoubleDefenceIndexInOnGameMidle) > 0


                                    )

                                    
                                {
                                    int[] i = IndexOfMovedDoubleDefence(Tab);
                                    if (i[0] != -1 & i[1] != -1)
                                    {
                                        if (Kind != i[2])
                                        {
                                            Console.WriteLine("Illegal kind");
                                            return;

                                   
                                        }

                                        RemoveOfDisturbIndex = IndexOfIsSupTRUE(Math.Abs(TableConst[HeuristicDoubleDefenceIndexInOnGame[i[0]][i[1]][2], HeuristicDoubleDefenceIndexInOnGame[i[0]][i[1]][3]]), HeuristicDoubleDefenceIndexInOnGame[i[0]]);
                                        bool a = MovableAllObjectsListMethos(HeuristicDoubleDefenceIndexInOnGame[i[0]][i[1]][2], HeuristicDoubleDefenceIndexInOnGame[i[0]][i[1]][3]);
                                        if (RemoveOfDisturbIndex != -1 && a)
                                        {
                                            IsSupHu[RemoveOfDisturbIndex] = false;
                                        }
                                        else if (!a)
                                        {
                                            if (Order == AllDraw.OrderPlateDraw)
                                            {
                                                LoseOcuuredatChiled[0] = 5;
                                                LoseOcuuredatChiled[1] = HeuristicDoubleDefenceIndexInOnGame[i[0]][i[1]][2];
                                                LoseOcuuredatChiled[2] = HeuristicDoubleDefenceIndexInOnGame[i[0]][i[1]][3];
                                            }
                                        }
                                    }
                                    /*else
                                    {
                                        if (Order == AllDraw.OrderPlateDraw)
                                            LoseOcuuredatChiled[0] = -4;
                                    }
                                    */
                                }
                            }
                        }
                    }
                }
            }
        }
 

        private int IndexOfMoved()
        {
            for (int j = 0; j < HeuristicAllReducedAttackedMidel; j++)
            {
                bool Is = false;
                for (int k = HeuristicAllReducedAttackedMidel; k < HeuristicAllReducedAttacked.Count; k++)
                {
                    if (HeuristicAllReducedAttacked[j][0] == HeuristicAllReducedAttacked[k][0]
                    && HeuristicAllReducedAttacked[j][1] == HeuristicAllReducedAttacked[k][1]
                    && HeuristicAllReducedAttacked[j][2] == HeuristicAllReducedAttacked[k][2]
                    && HeuristicAllReducedAttacked[j][3] == HeuristicAllReducedAttacked[k][3])
                    {
                        Is = true;
                    }
                }
                if (!Is)
                {
                    return j;
                }
            }
            return -1;
        }

        private int[] IndexOfMovedDoubleDefence(int[,] Tab)
        {
            int Object = 0;
            int[] ObjectIndex = { -1, -1, -1 };
            bool Is = false;
            for (int i = 0; i < HeuristicDoubleDefenceIndexInOnGameMidle; i++)
            {
                if (HeuristicDoubleDefenceIndexInOnGame[i].Count == 0)
                {
                    continue;
                }

                for (int j = 0; j < HeuristicDoubleDefenceIndexInOnGame[i].Count; j++)
                {
                    if (System.Math.Abs(Tab[HeuristicDoubleDefenceIndexInOnGame[i][j][2], HeuristicDoubleDefenceIndexInOnGame[i][j][3]]) > ObjectIndex[2])
                    {
                        Is = true;
                        ObjectIndex[0] = i;
                        ObjectIndex[1] = j;
                        ObjectIndex[2] = System.Math.Abs(Tab[HeuristicDoubleDefenceIndexInOnGame[i][j][2], HeuristicDoubleDefenceIndexInOnGame[i][j][3]]);
                    }
                }
            }
            if (HeuristicDoubleDefenceIndexInOnGameMidle > 0 && HeuristicDoubleDefenceIndexInOnGameMidle != HeuristicDoubleDefenceIndexInOnGame.Count)
            {
                Is = false;
                for (int i = HeuristicDoubleDefenceIndexInOnGameMidle; i < HeuristicDoubleDefenceIndexInOnGame.Count; i++)
                {
                    for (int j = 0; j < HeuristicDoubleDefenceIndexInOnGame[i].Count; j++)
                    {
                        if (System.Math.Abs(Tab[HeuristicDoubleDefenceIndexInOnGame[i][j][2], HeuristicDoubleDefenceIndexInOnGame[i][j][3]]) == Object)
                        {
                            Is = true;
                        }
                    }
                }
            }
            else
            {
            }

            if (Is)
            {
                ObjectIndex[0] = -1;
                ObjectIndex[1] = -1;
            }
            return ObjectIndex;
        }

        private int IndexOfIsSupTRUE(int Kind, int RowD, int ColD)
        {
            if (Kind == 1)
            {
                for (int j = 0; j < RowColumnSoldier.Count; j++)
                {
                    if (IsSup[j])
                    {
                        continue;
                    }

                    if (NoOfExistInReducedAttackList(false, RowColumnSoldier[j][0], RowColumnSoldier[j][1], RowD, ColD) == 0)
                    {
                        return j;
                    }
                }
            }
            else
            if (Kind == 2)
            {
                for (int j = 0; j < RowColumnElefant.Count; j++)
                {
                    if (IsSup[j])
                    {
                        continue;
                    }

                    if (NoOfExistInReducedAttackList(false, RowColumnElefant[j][0], RowColumnElefant[j][1], RowD, ColD) == 0)
                    {
                        return j;
                    }
                }
            }
            else
            if (Kind == 3)
            {
                for (int j = 0; j < RowColumnHourse.Count; j++)
                {
                    if (IsSup[j])
                    {
                        continue;
                    }

                    if (NoOfExistInReducedAttackList(false, RowColumnHourse[j][0], RowColumnHourse[j][1], RowD, ColD) == 0)
                    {
                        return j;
                    }
                }
            }
            else
            if (Kind == 4)
            {
                for (int j = 0; j < RowColumnCastle.Count; j++)
                {
                    if (IsSup[j])
                    {
                        continue;
                    }

                    if (NoOfExistInReducedAttackList(false, RowColumnCastle[j][0], RowColumnCastle[j][1], RowD, ColD) == 0)
                    {
                        return j;
                    }
                }
            }
            else
            if (Kind == 5)
            {
                for (int j = 0; j < RowColumnMinister.Count; j++)
                {
                    if (IsSup[j])
                    {
                        continue;
                    }

                    if (NoOfExistInReducedAttackList(false, RowColumnMinister[j][0], RowColumnMinister[j][1], RowD, ColD) == 0)
                    {
                        return j;
                    }
                }
            }
            else
            if (Kind == 6)
            {
                for (int j = 0; j < RowColumnKing.Count; j++)
                {
                    if (IsSup[j])
                    {
                        continue;
                    }

                    if (NoOfExistInReducedAttackList(false, RowColumnKing[j][0], RowColumnKing[j][1], RowD, ColD) == 0)
                    {
                        return j;
                    }
                }
            }
            else
            if (Kind == 7 || Kind == -7)
            {
                for (int j = 0; j < RowColumnKing.Count; j++)
                {
                    if (IsSup[j])
                    {
                        continue;
                    }

                    if (NoOfExistInReducedAttackList(false, RowColumnKing[j][0], RowColumnKing[j][1], RowD, ColD) == 0)
                    {
                        return j;
                    }
                }
            }
            return -1;
        }

        private int IndexOfIsSupTRUE(int Kind, List<int[]> Row)
        {
            int jj = -1;
            bool Is = false;
            if (Kind == 1)
            {
                for (int j = 0; j < RowColumnSoldier.Count; j++)
                {
                    for (int i = 0; i < Row.Count; i++)
                    {
                        if (IsSup[j])
                        {
                            continue;
                        }

                        if (NoOfExistInReducedAttackList(false, RowColumnSoldier[j][0], RowColumnSoldier[j][1], Row[i][0], Row[i][1]) != 0)
                        {
                            Is = true;
                        }
                        else
                        {
                            jj = j;
                        }
                    }
                }
            }
            else
            if (Kind == 2)
            {
                for (int j = 0; j < RowColumnElefant.Count; j++)
                {
                    for (int i = 0; i < Row.Count; i++)
                    {
                        if (IsSup[j])
                        {
                            continue;
                        }

                        if (NoOfExistInReducedAttackList(false, RowColumnElefant[j][0], RowColumnElefant[j][1], Row[i][0], Row[i][1]) != 0)
                        {
                            Is = true;
                        }
                        else
                        {
                            jj = j;
                        }
                    }
                }
            }
            else
            if (Kind == 3)
            {
                for (int j = 0; j < RowColumnHourse.Count; j++)
                {
                    for (int i = 0; i < Row.Count; i++)
                    {
                        if (IsSup[j])
                        {
                            continue;
                        }

                        if (NoOfExistInReducedAttackList(false, RowColumnHourse[j][0], RowColumnHourse[j][1], Row[i][0], Row[i][1]) != 0)
                        {
                            Is = true;
                        }
                        else
                        {
                            jj = j;
                        }
                    }
                }
            }
            else
            if (Kind == 4)
            {
                for (int j = 0; j < RowColumnCastle.Count; j++)
                {
                    for (int i = 0; i < Row.Count; i++)
                    {
                        if (NoOfExistInReducedAttackList(false, RowColumnCastle[j][0], RowColumnCastle[j][1], Row[i][0], Row[i][1]) != 0)
                        {
                            Is = true;
                        }
                        else
                        {
                            jj = j;
                        }
                    }
                }
            }
            else
            if (Kind == 5)
            {
                for (int j = 0; j < RowColumnMinister.Count; j++)
                {
                    for (int i = 0; i < Row.Count; i++)
                    {
                        if (IsSup[j])
                        {
                            continue;
                        }

                        if (NoOfExistInReducedAttackList(false, RowColumnMinister[j][0], RowColumnMinister[j][1], Row[i][0], Row[i][1]) != 0)
                        {
                            Is = true;
                        }
                        else
                        {
                            jj = j;
                        }
                    }
                }
            }
            else
            if (Kind == 6)
            {
                for (int j = 0; j < RowColumnKing.Count; j++)
                {
                    for (int i = 0; i < Row.Count; i++)
                    {
                        if (IsSup[j])
                        {
                            continue;
                        }

                        if (NoOfExistInReducedAttackList(false, RowColumnKing[j][0], RowColumnKing[j][1], Row[i][0], Row[i][1]) != 0)
                        {
                            Is = true;
                        }
                        else
                        {
                            jj = j;
                        }
                    }
                }
            }
            else
            if (Kind == 7 || Kind == -7)
            {
                for (int j = 0; j < RowColumnCastling.Count; j++)
                {
                    for (int i = 0; i < Row.Count; i++)
                    {
                        if (IsSup[j])
                        {
                            continue;
                        }

                        if (NoOfExistInReducedAttackList(false, RowColumnCastling[j][0], RowColumnCastling[j][1], Row[i][0], Row[i][1]) != 0)
                        {
                            Is = true;
                        }
                        else
                        {
                            jj = j;
                        }
                    }
                }
            }
            if (!Is)
            {
                return jj;
            }

            return -1;
        }     //objects value main method

        //objects value main method
        private int ObjectValueCalculator(int[,] Table//, int Order
            , int RowS, int ColS)
        {
            int Val = 1;

            if (System.Math.Abs(Table[RowS, ColS]) == 1)
            {
                Val = 1;
            }
            else
            if (System.Math.Abs(Table[RowS, ColS]) == 2)
            {
                Val = 3;
            }
            else
                        if (System.Math.Abs(Table[RowS, ColS]) == 3)
            {
                Val = 3;
            }
            else
                            if (System.Math.Abs(Table[RowS, ColS]) == 4)
            {
                Val = 5;
            }
            else
                                if (System.Math.Abs(Table[RowS, ColS]) == 5)
            {
                Val = 9;
            }
            else
                                if (System.Math.Abs(Table[RowS, ColS]) == 6)
            {
                Val = 10;
            }
            return Val;
        }

        private bool IsSupHuTrue(int j)
        {
            object O = new object();
            lock (O)
            {
                bool Is = false;
                if (!AllDraw.AllowedSupTrue)
                {
                    Is = IsSupHu[j];
                }
                else
                {
                    Is = IsSup[j];
                }
                return Is;
            }
        }

        private double Rational(double HA, double Ratio)
        {
            object o = new object();
            lock (o)
            {
                if (HA > 0 && Ratio < 0)
                {
                    return System.Math.Abs(HA);
                }
                else
                {
                    if (HA < 0 && Ratio > 0)
                    {
                        return System.Math.Abs(HA) * -1;
                    }
                }

                if (HA < 0 && Ratio < 0)
                    return System.Math.Abs(HA);

                return HA;
            }
        }
    }
}

//End of Documentation.