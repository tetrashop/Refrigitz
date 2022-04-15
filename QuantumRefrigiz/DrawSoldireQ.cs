using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace QuantumRefrigiz
{
    [Serializable]
    public class DrawSoldierQ : ThingsConverter
    {
        private readonly StringBuilder Space = new StringBuilder("&nbsp;");

        //#pragma warning disable CS0414 // The field 'DrawSoldierQ.Spaces' is ASsigned but its value is never used
        private readonly int Spaces = 0;
        //#pragma warning restore CS0414 // The field 'DrawSoldierQ.Spaces' is ASsigned but its value is never used

        public static int[,,] QuntumTable = {
            {{-1, -1, - 1, -1, -1, -1, -1, -1 },
             {-1, -1, - 1, -1, -1, -1, -1, -1 },
             {-1, -1, - 1, -1, -1, -1, -1, -1 },
             {-1, -1, - 1, -1, -1, -1, -1, -1 },
             {-1, -1, - 1, -1, -1, -1, -1, -1 },
             {-1, -1, - 1, -1, -1, -1, -1, -1 },
             {-1, -1, - 1, -1, -1, -1, -1, -1 },
             {-1, -1, - 1, -1, -1, -1, -1, -1 }},
             {{-1, -1, - 1, -1, -1, -1, -1, -1 },
              {-1, -1, - 1, -1, -1, -1, -1, -1 },
              {-1, -1, - 1, -1, -1, -1, -1, -1 },
              {-1, -1, - 1, -1, -1, -1, -1, -1 },
              {-1, -1, - 1, -1, -1, -1, -1, -1 },
              {-1, -1, - 1, -1, -1, -1, -1, -1 },
              {-1, -1, - 1, -1, -1, -1, -1, -1 },
              {-1, -1, - 1, -1, -1, -1, -1, -1 }}};

        //Pawns cannot make quantum moves.
        //A quantum move cannot be used to take a piece.
        public bool IsQuntumMove = false;

        //Pieces have rings around them, filled in with colour. These rings show the probability that the piece is in that square.
        private bool RingHalf = false;
        public int WinOcuuredatChiled = 0; public int[] LoseOcuuredatChiled = { 0, 0, 0 };

        //Iniatate Global Variables.


        private List<int[]> ValuableSelfSupported = new List<int[]>();







        public bool MovementsAStarGreedyHeuristicFoundT = false;
        public bool IgnoreSelfObjectsT = false;
        public bool UsePenaltyRegardMechnisamT = false;
        public bool BestMovmentsT = false;
        public bool PredictHeuristicT = true;
        public bool OnlySelfT = false;
        public bool AStarGreedyHeuristicT = false;
        public bool ArrangmentsChanged = true;
        public static double MaxHeuristicxS = double.MinValue;
        public float Row, Column;
        public Color color;



        public ThinkingQuantumChess[] SoldierThinkingQuantum = new ThinkingQuantumChess[AllDraw.SodierMovments];



        public int[,] Table = null;
        public int Order = 0;
        public int Current = 0;
        private readonly int CurrentAStarGredyMax = -1;

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
        public bool AccessIsQuntumMove => IsQuntumMove;
        public void Dispose()
        {

            ValuableSelfSupported = null;

        }
        public bool MaxFound(ref bool MaxNotFound)
        {


            double a = ReturnHeuristic();
            if (MaxHeuristicxS < a)
            {
                object O2 = new object();
                lock (O2)
                {
                    MaxNotFound = false;
                    if (ThinkingQuantumChess.MaxHeuristicx < MaxHeuristicxS)
                    {
                        ThinkingQuantumChess.MaxHeuristicx = a;
                    }

                    MaxHeuristicxS = a;
                }

                return true;
            }

            MaxNotFound = true;

            return false;
        }
        public double ReturnHeuristic()
        {
            int HaveKilled = 0;

            double a = 0;
            for (int ii = 0; ii < AllDraw.SodierMovments; ii++)
            {
                a += SoldierThinkingQuantum[ii].ReturnHeuristic(-1, -1, Order, false, ref HaveKilled);
            }

            return a;
        }
        //Constructor 1.

        //Constructor 2.
        public DrawSoldierQ(int CurrentAStarGredy, bool MovementsAStarGreedyHeuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments, float i, float j, Color a, int[,] Tab, int Ord, bool TB, int Cur//, ref AllDraw. THIS
            ) :
            base(Arrangments, (int)i, (int)j, Ord)
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
                //Initiate Global Variables.  
                Table = new int[8, 8];
                for (int ii = 0; ii < 8; ii++)
                {
                    for (int jj = 0; jj < 8; jj++)
                    {
                        Table[ii, jj] = Tab[ii, jj];
                    }
                }

                if (i >= 8 || j >= 8)
                {
                    i = 7;
                }

                for (int ii = 0; ii < AllDraw.SodierMovments; ii++)
                {
                    SoldierThinkingQuantum[ii] = new ThinkingQuantumChess(ii, 1, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, (int)i, (int)j, a, CloneATable(Tab), 4, Ord, TB, Cur, 16, 1);
                }
                Row = i;
                Column = j;
                color = a;
                Order = Ord;
                Current = Cur;
            }

        }
        //Clone a Copy Method.
        public void Clone(ref DrawSoldierQ AA//, ref AllDraw. THIS
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
            //Initiate a Object and ASsignemt of a Clone to Construction of a Copy.

            AA = new DrawSoldierQ(CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, Row, Column, color, CloneATable(Tab), Order, false, Current
                )
            {
                ArrangmentsChanged = ArrangmentsChanged
            };
            for (int i = 0; i < AllDraw.SodierMovments; i++)
            {

                AA.SoldierThinkingQuantum[i] = new ThinkingQuantumChess(i, 1, CurrentAStarGredyMax, MovementsAStarGreedyHeuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHeuristicT, OnlySelfT, AStarGreedyHeuristicT, ArrangmentsChanged, (int)Row, (int)Column);
                SoldierThinkingQuantum[i].Clone(ref AA.SoldierThinkingQuantum[i]);

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

        private bool Quantum(int[,] Tab, int Order, int Row, int Column, int LastRowQ, int LastColumnQ)
        {

            if (ArrangmentsChanged)
            {
                if (LastRowQ == 6)
                {
                    if (Column == LastColumnQ + 2)
                    {
                        if (Table[Row, LastColumnQ + 1] < 0)
                        {
                            if (Table[Row, Column] == 1)
                            {

                                return true;
                            }
                        }
                    }
                }
                if (System.Math.Abs(Column - LastColumnQ) == 1 && System.Math.Abs(Row - LastRowQ) == 1)
                {
                    if (Table[Row, Column] == 1 || Table[LastRowQ, LastColumnQ] == 1)
                    {

                        return true;
                    }
                }
            }
            else
            {
                if (LastRowQ == 1)
                {
                    if (Column == LastColumnQ - 2)
                    {
                        if (Table[Row, LastColumnQ - 1] > 0)
                        {
                            if (Table[Row, Column] == -1)
                            {

                                return true;
                            }
                        }
                    }
                }
                if (System.Math.Abs(Column - LastColumnQ) == 1 && System.Math.Abs(Row - LastRowQ) == 1)
                {
                    if (Table[Row, Column] == -1 || Table[LastRowQ, LastColumnQ] == -1)
                    {

                        return true;
                    }
                }
            }



            return false;
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

        //Drawing Soldiers On the Table Method..
        public void DrawSoldierOnTable(ref Graphics g, int CellW, int CellH)
        {
            object balancelockS = new object();

            lock (balancelockS)
            {
                if (g == null)
                {
                    return;
                }

                int LastRowQ = 0, LastColumnQ = 0;


                if (AllDraw.LastRowQ != Row && AllDraw.LastColumnQ != Column && AllDraw.LastRowQ != -1 && AllDraw.LastColumnQ != -1)

                {
                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1)
                    {
                        LastRowQ = AllDraw.QuntumTable[0, (int)Row, (int)Column];
                        LastColumnQ = AllDraw.QuntumTable[1, (int)Row, (int)Column];
                        IsQuntumMove = true;
                    }
                    else
                    if (AllDraw.LastRowQ != -1 && AllDraw.LastColumnQ != -1)
                    {
                        LastRowQ = AllDraw.LastRowQ;
                        LastColumnQ = AllDraw.LastColumnQ;
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
                    LastColumnQ = AllDraw.QuntumTable[1, (int)Row, (int)Column];
                    RingHalf = true;
                }
                else
                    if (IsQuntumMove)
                {
                    RingHalf = true;
                    if (AllDraw.LastRowQ != -1 && AllDraw.LastColumnQ != -1)
                    {
                        LastRowQ = AllDraw.LastRowQ;
                        LastColumnQ = AllDraw.LastColumnQ;
                        AllDraw.LastRowQ = -1;
                        AllDraw.LastColumnQ = -1;
                    }
                }

                if (!Quantum(CloneATable(Table), Order, (int)Row, (int)Column, LastRowQ, LastColumnQ))
                {
                    RingHalf = false;
                }


                //When Conversion Solders Not Occured.
                if (!ConvertOperation((int)Row, (int)Column, CloneATable(Table), Order))
                {

                    //Gray Color.
                    if (((int)Row >= 0) && ((int)Row < 8) && ((int)Column >= 0) && ((int)Column < 8))
                    {


                        //If Order is Gray.
                        if (Order == 1)
                        {
                            object O1 = new object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Draw an Instant from File of Gray Soldeirs.
                                 //Draw a Gray Castles Instatnt Image On hte Tabe.

                                if (ArrangmentsChanged)
                                {
                                    if (Table[(int)Row, (int)(Column - 1)] < 0)
                                    {
                                        RingHalf = false;
                                    }
                                }
                                else
                                {
                                    if (Table[(int)Row, (int)(Column + 1)] > 0)
                                    {
                                        RingHalf = false;
                                    }
                                }

                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "SG.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumnQ != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "SG.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumnQ] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumnQ] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "SG.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }

                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }
                        else
                        {
                            object O1 = new object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Draw an Instatnt of Brown Soldier File On the Table.

                                if (ArrangmentsChanged)
                                {
                                    if (Table[(int)Row, (int)(Column - 1)] > 0)
                                    {
                                        RingHalf = false;
                                    }
                                }
                                else
                                {
                                    if (Table[(int)Row, (int)(Column + 1)] < 0)
                                    {
                                        RingHalf = false;
                                    }
                                }

                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "SB.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumnQ != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "SB.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumnQ] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumnQ] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "SB.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }


                    }
                    else//If Minsister Conversion Occured.
                        if (ConvertedToMinister)
                    {

                        //Color of Gray.
                        if (Order == 1)
                        {
                            object O1 = new object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Draw of Gray Minsister Image File By an Instant.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "MG.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumnQ != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "MG.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumnQ] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumnQ] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "MG.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }
                        else
                        {
                            object O1 = new object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Draw a Image File on the Table Form n Instatnt One.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "MB.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumnQ != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "MB.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumnQ] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumnQ] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "MB.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }

                    }
                    else if (ConvertedToCastle)//When Castled Converted.
                    {

                        //Color of Gray.
                        if (Order == 1)
                        {
                            object O1 = new object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Create on the Inststant of Gray Castles Images.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "BrG.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumnQ != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "BrG.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumnQ] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumnQ] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "BrG.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }

                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }
                        else
                        {
                            object O1 = new object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Creat of an Instant of Brown Image Castles.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "BrB.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumnQ != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "BrB.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumnQ] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumnQ] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "BrB.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }

                    }
                    else if (ConvertedToHourse)//When Hourse Conversion Occured.
                    {


                        //Color of Gray.
                        if (Order == 1)
                        {
                            object O1 = new object();
                            lock (O1)
                            {//Draw an Instatnt of Gray Hourse Image File.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EG.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumnQ != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EG.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumnQ] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumnQ] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EG.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }
                        else
                        {
                            object O1 = new object();
                            lock (O1)
                            {//Creat of an Instatnt Hourse Image File.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EB.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    if (LastRowQ != -1 && LastColumnQ != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EB.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                        AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                        AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                        AllDraw.QuntumTable[0, LastRowQ, LastColumnQ] = -1;
                                        AllDraw.QuntumTable[1, LastRowQ, LastColumnQ] = -1;
                                    }
                                    else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                    {
                                        g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EB.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }


                    }
                    else if (ConvertedToElefant)//When Elephant Conversion.
                    {

                        //Color of Gray.
                        if (Order == 1)
                        {
                            object O1 = new object();
                            lock (O1)
                            {//Draw an Instatnt Image of Gray Elephant.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EG.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    //if (Prob > 0)
                                    {
                                        if (LastRowQ != -1 && LastColumnQ != -1)
                                        {
                                            g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EG.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                            if (AllDraw.QuntumTable[0, (int)Row, (int)Column] == -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] == -1)
                                            {
                                                AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                                AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                            }
                                        }
                                        else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EG.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                        }
                                        AllDraw.QuntumTable[0, AllDraw.QuntumTable[0, (int)Row, (int)Column], AllDraw.QuntumTable[1, (int)Row, (int)Column]] = -1;
                                        AllDraw.QuntumTable[1, AllDraw.QuntumTable[0, (int)Row, (int)Column], AllDraw.QuntumTable[1, (int)Row, (int)Column]] = -1;

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }
                        else
                        {
                            object O1 = new object();
                            lock (O1)
                            {//Draw of Instant Image of Brown Elephant.
                                g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EB.png"), new Rectangle((int)(Row * CellW), (int)(Column * CellH), CellW, CellH));
                                if (RingHalf)
                                {
                                    float Prob = 180;
                                    if (AllDraw.Less != 0)
                                    {
                                        Prob = (float)(180 * (AllDraw.Less / double.MaxValue));
                                    }

                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, Prob);
                                    //if (Prob > 0)
                                    {
                                        if (LastRowQ != -1 && LastColumnQ != -1)
                                        {
                                            g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EB.png"), new Rectangle(LastRowQ * CellW, LastColumnQ * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((LastRowQ * CellW), (int)(LastColumnQ * (float)CellH), CellW, CellH), -45, Prob);
                                            if (AllDraw.QuntumTable[0, (int)Row, (int)Column] == -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] == -1)
                                            {
                                                AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRowQ;
                                                AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumnQ;
                                            }
                                        }
                                        else
                                    if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(Image.FromFile(AllDraw.ImagesSubRoot + "EB.png"), new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, Prob);

                                        }
                                        AllDraw.QuntumTable[0, AllDraw.QuntumTable[0, (int)Row, (int)Column], AllDraw.QuntumTable[1, (int)Row, (int)Column]] = -1;
                                        AllDraw.QuntumTable[1, AllDraw.QuntumTable[0, (int)Row, (int)Column], AllDraw.QuntumTable[1, (int)Row, (int)Column]] = -1;

                                    }
                                }
                                else
                                {
                                    g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * CellW)), (int)(Column * CellH), CellW, CellH), -45, 360);
                                }
                            }
                        }


                    }
                }
            }

        }
    }

}
//End of Documentation.
