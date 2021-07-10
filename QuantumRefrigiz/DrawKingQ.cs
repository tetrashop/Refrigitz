using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
namespace QuantumRefrigiz
{
    [Serializable]
    public class DrawKingQ
    {
        private readonly StringBuilder Space = new StringBuilder("&nbsp;");

        //#pragma warning disable CS0414 // The field 'DrawKingQ.Spaces' is ASsigned but its value is never used
        private readonly int Spaces = 0;
        //#pragma warning restore CS0414 // The field 'DrawKingQ.Spaces' is ASsigned but its value is never used

        //A quantum move cannot be used to take a piece.
        public bool IsQuntumMove = false;
        //Pieces have rings around them, filled in with colour. These rings show the probability that the piece is in that square.
        public static bool KingGrayNotCheckedByQuantumMove = false;
        public static bool KingBrownNotCheckedByQuantumMove = false;
        public bool RingHalf = false;
        public int WinOcuuredatChiled = 0; public int[] LoseOcuuredatChiled = { 0, 0, 0 };



        //Initiate Global Variables.
        private List<int[]> ValuableSelfSupported = new List<int[]>();

        public bool MovementsAStarGreedyHeuristicFoundT = false;
        public bool IgnoreSelfObjectsT = false;
        public bool UsePenaltyRegardMechnisamT = false;
        public bool BestMovmentsT = false;
        public bool PredictHeuristicT = true;
        public bool OnlySelfT = false;
        public bool AStarGreedyHeuristicT = false;

        public bool ArrangmentsChanged = true;
        public static long MaxHeuristicxK = -20000000000000000;
        public float Row, Column;
        public Color color;
        public int[,] Table = null;



        public ThinkingQuantumChess[] KingThinkingQuantum = new ThinkingQuantumChess[AllDraw.KingMovments];



        public int Current = 0;
        public int Order;
        private readonly int CurrentAStarGredyMax = -1;



        public ThinkingQuantumChess ThinkingQuantumString = null;

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
        public void Dispose()
        {

            ValuableSelfSupported = null;

        }

        public int ReturnHeuristic()
        {
            int HaveKilled = 0;

            int a = 0;
            for (int ii = 0; ii < AllDraw.KingMovments; ii++)
            {
                a += KingThinkingQuantum[ii].ReturnHeuristic(-1, -1, Order, false, ref HaveKilled);
            }

            return a;
        }
        public bool MaxFound(ref bool MaxNotFound)
        {


            int a = ReturnHeuristic();
            if (MaxHeuristicxK < a)
            {
                object O2 = new object();
                lock (O2)
                {
                    MaxNotFound = false;
                    if (ThinkingQuantumChess.MaxHeuristicx < MaxHeuristicxK)
                    {
                        ThinkingQuantumChess.MaxHeuristicx = a;
                    }

                    MaxHeuristicxK = a;
                }

                return true;
            }

            MaxNotFound = true;

            return false;
        }
        //Constructor 1.

        //Constructor 2.
        public DrawKingQ(int CurrentAStarGredy, bool MovementsAStarGreedyHeuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments, float i, float j, Color a, int[,] Tab, int Ord, bool TB, int Cur//, ref AllDraw. THIS
            )
        {

            object balancelock = new object();

            lock (balancelock)
            {


                CurrentAStarGredyMax = CurrentAStarGredy;
                MovementsAStarGreedyHeuristicFoundT = MovementsAStarGreedyHeuristicTFou;
                IgnoreSelfObjectsT = IgnoreSelfObject;
                UsePenaltyRegardMechnisamT = UsePenaltyRegardMechnisa;
                BestMovmentsT = BestMovment;
                PredictHeuristicT = PredictHurist;
                OnlySelfT = OnlySel;
                AStarGreedyHeuristicT = AStarGreedyHuris;
                ArrangmentsChanged = Arrangments;
                //Iniatite Global Variables.
                Table = new int[8, 8];
                for (int ii = 0; ii < 8; ii++)
                {
                    for (int jj = 0; jj < 8; jj++)
                    {
                        Table[ii, jj] = Tab[ii, jj];
                    }
                }

                for (int ii = 0; ii < AllDraw.KingMovments; ii++)
                {
                    KingThinkingQuantum[ii] = new ThinkingQuantumChess(ii, 6, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, (int)i, (int)j, a, CloneATable(Tab), 8, Ord, TB, Cur, 2, 6);
                }

                Row = i;
                Column = j;
                color = a;
                Order = Ord;
                Current = Cur;
            }

        }

        private int[,] CloneATable(int[,] Tab)
        {

            object O = new object();
            lock (O)
            {
                //Create and new an Object.
                int[,] Table = new int[8, 8];
                //ASsigne Parameter To New Objects.
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
                //ASsigne Parameter To New Objects.
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
        //Clone a Copy.
        public void Clone(ref DrawKingQ AA//, ref AllDraw. THIS
            )
        {

            int[,] Tab = new int[8, 8];
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Tab[i, j] = Table[i, j];
                }
            }
            //Initiate a Construction Object and Clone a Copy.
            AA = new DrawKingQ(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Row, Column, color, CloneATable(Table), Order, false, Current)
            {
                ArrangmentsChanged = ArrangmentsChanged
            };
            for (int i = 0; i < AllDraw.KingMovments; i++)
            {

                AA.KingThinkingQuantum[i] = new ThinkingQuantumChess(i, 6, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, (int)Row, (int)Column);
                KingThinkingQuantum[i].Clone(ref AA.KingThinkingQuantum[i]);

            }
            AA.Table = new int[8, 8];
            for (int ii = 0; ii < 8; ii++)
            {
                for (int jj = 0; jj < 8; jj++)
                {
                    AA.Table[ii, jj] = Tab[ii, jj];
                }
            }

            AA.Row = Row;
            AA.Column = Column;
            AA.Order = Order;
            AA.Current = Current;
            AA.color = color;

        }
        //Draw an Instatnt King on the Table Method.
        public void DrawKingOnTable(ref Graphics g, int CellW, int CellH)
        {
            object balancelockS = new object();

            lock (balancelockS)
            {
                if (g == null)
                {
                    return;
                }

                int LastRowQ = -1, LastColumn = -1;


                if (AllDraw.LastRowQ != Row && AllDraw.LastColumnQ != Column && AllDraw.LastRowQ != -1 && AllDraw.LastColumnQ != -1)

                {
                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1)
                    {
                        LastRowQ = AllDraw.QuntumTable[0, (int)Row, (int)Column];
                        LastColumn = AllDraw.QuntumTable[1, (int)Row, (int)Column];
                        IsQuntumMove = true;
                    }
                    else
                    if (AllDraw.LastRowQ != -1 && AllDraw.LastColumnQ != -1)
                    {
                        LastRowQ = AllDraw.LastRowQ;
                        LastColumn = AllDraw.LastColumnQ;
                        AllDraw.LastRowQ = -1;
                        AllDraw.LastColumnQ = -1;
                        IsQuntumMove = true;
                    }
                    AllDraw.LastRowQ = -1;
                    AllDraw.LastColumnQ = -1;
                    AllDraw.NextRowQ = -1;
                    AllDraw.NextColumnQ = -1;

                }

                if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                {
                    LastRowQ = AllDraw.QuntumTable[0, (int)Row, (int)Column];
                    LastColumn = AllDraw.QuntumTable[1, (int)Row, (int)Column];
                    RingHalf = true;
                }
                else
                    if (IsQuntumMove)
                {
                    RingHalf = true;
                    if (AllDraw.LastRowQ != -1 && AllDraw.LastColumnQ != -1)
                    {
                        LastRowQ = AllDraw.LastRowQ;
                        LastColumn = AllDraw.LastColumnQ;
                        AllDraw.LastRowQ = -1;
                        AllDraw.LastColumnQ = -1;
                    }
                }


                lock (balancelockS)
                {

                    if (((int)Row >= 0) && ((int)Row < 8) && ((int)Column >= 0) && ((int)Column < 8))
                    { //Gray Order.
                        if (Order == 1)
                        {
                            object O1 = new object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Draw an Instatnt Gray King Image On the Table.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "KG.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    int Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = 180 * (AllDraw.Less / int.MaxValue);
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumn != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "KG.png"), new Rectangle(LastRowQ * CellW, LastColumn * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumn * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumn] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumn] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "KG.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }

                                //12/16/2018

                            }
                        }
                        else
                        {
                            object O1 = new object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Draw an Instatnt Brown King Image On the Table.

                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "KB.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    int Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = 180 * (AllDraw.Less / int.MaxValue);
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumn != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "KB.png"), new Rectangle(LastRowQ * CellW, LastColumn * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumn * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumn] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumn] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "KB.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                                //12/16/2018

                            }
                        }

                    }
                }


            }
        }
    }
}
//End of Documentation.
