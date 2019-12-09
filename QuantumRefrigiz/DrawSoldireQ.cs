using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

namespace QuantumRefrigiz
{
    [Serializable]
    public class DrawSoldierQ : ThingsConverter
    {
        StringBuilder Space = new StringBuilder("&nbsp;");
#pragma warning disable CS0414 // The field 'DrawSoldierQ.Spaces' is assigned but its value is never used
        int Spaces = 0;
#pragma warning restore CS0414 // The field 'DrawSoldierQ.Spaces' is assigned but its value is never used

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
        bool RingHalf = false;
        public int WinOcuuredatChiled = 0;public int LoseOcuuredatChiled = 0;
        //Iniatate Global Variables.
        //private readonly object balancelock = new object();
        //private readonly object balancelockS = new object();
        List<int[]> ValuableSelfSupported = new List<int[]>();

        public static Image[] S = new Image[2];
        public bool MovementsAStarGreedyHuristicFoundT = false;
        public bool IgnoreSelfObjectsT = false;
        public bool UsePenaltyRegardMechnisamT = true;
        public bool BestMovmentsT = false;
        public bool PredictHuristicT = true;
        public bool OnlySelfT = false;
        public bool AStarGreedyHuristicT = false;
        public bool ArrangmentsChanged = false;
        public static int MaxHuristicxS = int.MinValue;
        public float Row, Column;
        public Color color;
        public ThinkingQuantumChess[] SoldierThinkingQuantum = new ThinkingQuantumChess[AllDraw.SodierMovments];
        public int[,] Table = null;
        public int Order = 0;
        public int Current = 0;
        int CurrentAStarGredyMax = -1;
        
        static void Log(Exception ex)
        {
            
                Object a = new Object();
                lock (a)
                {
                    string stackTrace = ex.ToString();
                    File.AppendAllText(AllDraw.Root + "\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString()); // path of file where stack trace will be stored.
                }
           
        }
        public bool AccessIsQuntumMove
        {
            get { return IsQuntumMove; }
        }
        public void Dispose()
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            ValuableSelfSupported = null;
            S = null;
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("Dispose:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
        }
        public bool MaxFound(ref bool MaxNotFound)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;

            int a = ReturnHuristic();
                if (MaxHuristicxS < a)
                {
                    Object O2 = new Object();
                    lock (O2)
                    {
                        MaxNotFound = false;
                        if (ThinkingQuantumChess.MaxHuristicx < MaxHuristicxS)
                            ThinkingQuantumChess.MaxHuristicx = a;
                        MaxHuristicxS = a;
                    }
                ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("MaxFound:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
                return true;
                }
           
            MaxNotFound = true;
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("MaxFound:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
            return false;
        }
        public int ReturnHuristic()
        {
            int HaveKilled = 0;
            //long Time = TimeElapced.TimeNow();Spaces++;
            int a = 0;
            for (var ii = 0; ii < AllDraw.SodierMovments; ii++)
                
                    a += SoldierThinkingQuantum[ii].ReturnHuristic(-1, -1, Order,false,ref HaveKilled);
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("ReturnHuristic:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
            return a;
        }
        //Constructor 1.
        /* public DrawSoldierQ(int CurrentAStarGredy, bool MovementsAStarGreedyHuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments)
         {
             CurrentAStarGredyMax = CurrentAStarGredy;
             MovementsAStarGreedyHuristicFoundT = MovementsAStarGreedyHuristicTFou;
             IgnoreSelfObjectsT = IgnoreSelfObject;
             UsePenaltyRegardMechnisamT = UsePenaltyRegardMechnisa;
             BestMovmentsT = BestMovment;
             PredictHuristicT = PredictHurist;
             OnlySelfT = OnlySel;
             AStarGreedyHuristicT = AStarGreedyHuris;
             ArrangmentsChanged = Arrangments;
         }
         */
        //Constructor 2.
        public DrawSoldierQ(int CurrentAStarGredy, bool MovementsAStarGreedyHuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments, float i, float j, Color a, int[,] Tab, int Ord, bool TB, int Cur//, ref AllDraw. THIS
            ) :
            base(Arrangments, (int)i, (int)j, a, Tab, Ord, TB, Cur)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            object balancelock = new object();
            lock (balancelock)
            {
                

                CurrentAStarGredyMax = CurrentAStarGredy;
                MovementsAStarGreedyHuristicFoundT = MovementsAStarGreedyHuristicTFou;
                IgnoreSelfObjectsT = IgnoreSelfObject;
                UsePenaltyRegardMechnisamT = UsePenaltyRegardMechnisa;
                BestMovmentsT = BestMovment;
                PredictHuristicT = PredictHurist;
                OnlySelfT = OnlySel;
                AStarGreedyHuristicT = AStarGreedyHuris;
                ArrangmentsChanged = Arrangments;
                //Initiate Global Variables.  
                Table = new int[8, 8];
                for (var ii = 0; ii < 8; ii++)
                    for (var jj = 0; jj < 8; jj++)
                        Table[ii, jj] = Tab[ii, jj];
                if (i >= 8 || j >= 8)
                    i = 7;
                for (var ii = 0; ii < AllDraw.SodierMovments; ii++)
                {
                    SoldierThinkingQuantum[ii] = new ThinkingQuantumChess(ii,1,CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, (int)i, (int)j, a, Tab, 4, Ord, TB, Cur, 16, 1);                    
                }
                Row = i;
                Column = j;
                color = a;
                Order = Ord;
                Current = Cur;
            }
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("DrawSoldierQ:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
        }
        //Clone a Copy Method.
        public void Clone(ref DrawSoldierQ AA//, ref AllDraw. THIS
            )
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            int[,] Tab = new int[8, 8];
            for (var i = 0; i < 8; i++)
                for (var j = 0; j < 8; j++)
                    Tab[i, j] = this.Table[i, j];
            //Initiate a Object and Assignemt of a Clone to Construction of a Copy.

            AA = new DrawSoldierQ(CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, this.Row, this.Column, this.color, Tab, this.Order, false, this.Current
                );
            AA.ArrangmentsChanged = ArrangmentsChanged;
            for (var i = 0; i < AllDraw.SodierMovments; i++)
            {
                
                    AA.SoldierThinkingQuantum[i] = new ThinkingQuantumChess(i,1,CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, (int)this.Row, (int)this.Column);
                    this.SoldierThinkingQuantum[i].Clone(ref AA.SoldierThinkingQuantum[i]);
               
            }
            AA.Table = new int[8, 8];
            for (var ii = 0; ii < 8; ii++)
                for (var jj = 0; jj < 8; jj++)
                    AA.Table[ii, jj] = Tab[ii, jj];
            AA.Row = Row;
            AA.Column = Column;
            AA.Order = Order;
            AA.Current = Current;
            AA.color = color;
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("Clone:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
        }
        bool Quantum(int[,] Tab, int Order, int Row, int Column, int LastRow, int LastColumn)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            if (ArrangmentsChanged)
            {
                if (LastRow == 6)
                {
                    if (Column == LastColumn + 2)
                        if (Table[Row, LastColumn + 1] < 0)
                            if (Table[Row, Column] == 1)
                            {
                                ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("Quantum:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
                                return true;
                            }
                }
                if (System.Math.Abs(Column - LastColumn) == 1 && System.Math.Abs(Row - LastRow) == 1)
                    if (Table[Row, Column] == 1 || Table[LastRow, LastColumn] == 1)
                    {
                        ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("Quantum:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
                        return true;
                    }
            }
            else
            {
                if (LastRow == 1)
                {
                    if (Column == LastColumn - 2)
                        if (Table[Row, LastColumn - 1] > 0)
                            if (Table[Row, Column] == -1)
                            {
                                ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("Quantum:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
                                return true;
                            }
                }
                if (System.Math.Abs(Column - LastColumn) == 1 && System.Math.Abs(Row - LastRow) == 1)
                    if (Table[Row, Column] == -1 || Table[LastRow, LastColumn] == -1)
                    {
                        ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("Quantum:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
                        return true;
                    }
            }

            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("Quantum:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;

            return false;
        }
        int[,] CloneATable(int[,] Tab)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            Object O = new Object();
            lock (O)
            {
                //Create and new an Object.
                int[,] Table = new int[8, 8];
                //Assigne Parameter To New Objects.
                for (var i = 0; i < 8; i++)
                    for (var j = 0; j < 8; j++)
                        Table[i, j] = Tab[i, j];
                //Return New Object.
                ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("CloneATable:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
                return Table;
            }

        }
        bool[,] CloneATable(bool[,] Tab)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            Object O = new Object();
            lock (O)
            {
                //Create and new an Object.
                bool[,] Table = new bool[8, 8];
                //Assigne Parameter To New Objects.
                for (var i = 0; i < 8; i++)
                    for (var j = 0; j < 8; j++)
                        Table[i, j] = Tab[i, j];
                //Return New Object.
                ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("CloneATable:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
                return Table;
            }

        }
       
        //Drawing Soldiers On the Table Method..
        public void DrawSoldierOnTable(ref Graphics g, int CellW, int CellH)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            object balancelockS = new object();

            int LastRow = 0, LastColumn = 0;

            lock (balancelockS)
            {
                if (AllDraw.LastRowQ != Row && AllDraw.LastColumnQ != Column&&AllDraw.LastRowQ!=-1&&AllDraw.LastColumnQ!=-1)
                    
                    {
                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1)
                        {
                            LastRow = AllDraw.QuntumTable[0, (int)Row, (int)Column];
                        LastColumn = AllDraw.QuntumTable[1, (int)Row, (int)Column];
                        IsQuntumMove = true;
                    }
                        else
                        if (AllDraw.LastRowQ != -1 && AllDraw.LastColumnQ != -1)
                        {
                            LastRow = AllDraw.LastRowQ;
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
                    LastRow = AllDraw.QuntumTable[0, (int)Row, (int)Column];
                    LastColumn = AllDraw.QuntumTable[1, (int)Row, (int)Column];
                    RingHalf = true;
                }
                else
                    if (IsQuntumMove)
                {
                    RingHalf = true;
                    if (AllDraw.LastRowQ != -1 && AllDraw.LastColumnQ != -1)
                    {
                        LastRow = AllDraw.LastRowQ;
                        LastColumn = AllDraw.LastColumnQ;
                        AllDraw.LastRowQ = -1;
                        AllDraw.LastColumnQ = -1;
                    }
                }

                if (!Quantum(CloneATable(Table), Order, (int)Row, (int)Column, LastRow, LastColumn))
                    RingHalf = false;
                if (S[0] == null || S[1] == null)
                {
                    S[0] = Image.FromFile(AllDraw.ImagesSubRoot + "SG.png");
                    S[1] = Image.FromFile(AllDraw.ImagesSubRoot + "SB.png");
                }
                //When Conversion Solders Not Occured.
                if (!ConvertOperation((int)Row, (int)Column, color, CloneATable(Table), Order, false, Current))
                {

                    //Gray Color.
                    if (((int)Row >= 0) && ((int)Row < 8) && ((int)Column >= 0) && ((int)Column < 8))
                    {
                        

                            //If Order is Gray.
                            if (Order == 1)
                            {
                                Object O1 = new Object();
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

                                    g.DrawImage(S[0], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        if (LastRow != -1 && LastColumn != -1)
                                        {
                                            g.DrawImage(S[0], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                            AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                            AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                              AllDraw.QuntumTable[0, LastRow, LastColumn] = -1;
                                             AllDraw.QuntumTable[1, LastRow, LastColumn] = -1;
                                        }
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(S[0], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                        }

                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);

                                }
                            }
                            else
                            {
                                Object O1 = new Object();
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

                                    g.DrawImage(S[1], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        if (LastRow != -1 && LastColumn != -1)
                                        {
                                            g.DrawImage(S[1], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                            AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                            AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                              AllDraw.QuntumTable[0, LastRow, LastColumn] = -1;
                                             AllDraw.QuntumTable[1, LastRow, LastColumn] = -1;
                                        }
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(S[1], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                        }
                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);
                                }
                            }
                       

                    }
                    else//If Minsister Conversion Occured.
                        if (ConvertedToMinister)
                    {
                        
                            //Color of Gray.
                            if (Order == 1)
                            {
                                Object O1 = new Object();
                                lock (O1)
                                {    //Draw an Instant from File of Gray Soldeirs.
                                     //Draw of Gray Minsister Image File By an Instant.
                                    g.DrawImage(DrawMinisterQ.M[0], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        if (LastRow != -1 && LastColumn != -1)
                                        {
                                            g.DrawImage(DrawMinisterQ.M[0], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                            AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                            AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                              AllDraw.QuntumTable[0, LastRow, LastColumn] = -1;
                                             AllDraw.QuntumTable[1, LastRow, LastColumn] = -1;
                                        }
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(DrawMinisterQ.M[0], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                        }
                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);
                                }
                            }
                            else
                            {
                                Object O1 = new Object();
                                lock (O1)
                                {    //Draw an Instant from File of Gray Soldeirs.
                                     //Draw a Image File on the Table Form n Instatnt One.
                                    g.DrawImage(DrawMinisterQ.M[1], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        if (LastRow != -1 && LastColumn != -1)
                                        {
                                            g.DrawImage(DrawMinisterQ.M[1], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                            AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                            AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                              AllDraw.QuntumTable[0, LastRow, LastColumn] = -1;
                                             AllDraw.QuntumTable[1, LastRow, LastColumn] = -1;
                                        }
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(DrawMinisterQ.M[1], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                        }
                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);
                                }
                            }
                       
                    }
                    else if (ConvertedToCastle)//When Castled Converted.
                    {
                        
                            //Color of Gray.
                            if (Order == 1)
                            {
                                Object O1 = new Object();
                                lock (O1)
                                {    //Draw an Instant from File of Gray Soldeirs.
                                     //Create on the Inststant of Gray Castles Images.
                                    g.DrawImage(DrawCastleQ.C[0], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        if (LastRow != -1 && LastColumn != -1)
                                        {
                                            g.DrawImage(DrawCastleQ.C[0], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                            AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                            AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                              AllDraw.QuntumTable[0, LastRow, LastColumn] = -1;
                                             AllDraw.QuntumTable[1, LastRow, LastColumn] = -1;
                                        }
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(DrawCastleQ.C[0], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                        }

                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);
                                }
                            }
                            else
                            {
                                Object O1 = new Object();
                                lock (O1)
                                {    //Draw an Instant from File of Gray Soldeirs.
                                     //Creat of an Instant of Brown Image Castles.
                                    g.DrawImage(DrawCastleQ.C[1], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        if (LastRow != -1 && LastColumn != -1)
                                        {
                                            g.DrawImage(DrawCastleQ.C[1], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                            AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                            AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                              AllDraw.QuntumTable[0, LastRow, LastColumn] = -1;
                                             AllDraw.QuntumTable[1, LastRow, LastColumn] = -1;
                                        }
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(DrawCastleQ.C[1], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                        }
                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);
                                }
                            }
                       
                    }
                    else if (ConvertedToHourse)//When Hourse Conversion Occured.
                    {

                        
                            //Color of Gray.
                            if (Order == 1)
                            {
                                Object O1 = new Object();
                                lock (O1)
                                {//Draw an Instatnt of Gray Hourse Image File.
                                    g.DrawImage(DrawElefantQ.E[0], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        if (LastRow != -1 && LastColumn != -1)
                                        {
                                            g.DrawImage(DrawElefantQ.E[0], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                            AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                            AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                              AllDraw.QuntumTable[0, LastRow, LastColumn] = -1;
                                             AllDraw.QuntumTable[1, LastRow, LastColumn] = -1;
                                        }
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(DrawElefantQ.E[0], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                        }
                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);
                                }
                            }
                            else
                            {
                                Object O1 = new Object();
                                lock (O1)
                                {//Creat of an Instatnt Hourse Image File.
                                    g.DrawImage(DrawElefantQ.E[1], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        if (LastRow != -1 && LastColumn != -1)
                                        {
                                            g.DrawImage(DrawElefantQ.E[1], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                            AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                            AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                              AllDraw.QuntumTable[0, LastRow, LastColumn] = -1;
                                             AllDraw.QuntumTable[1, LastRow, LastColumn] = -1;
                                        }
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                        {
                                            g.DrawImage(DrawElefantQ.E[1], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                            g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                        }
                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);
                                }
                            }
                       

                    }
                    else if (ConvertedToElefant)//When Elephant Conversion.
                    {
                        
                            //Color of Gray.
                            if (Order == 1)
                            {
                                Object O1 = new Object();
                                lock (O1)
                                {//Draw an Instatnt Image of Gray Elephant.
                                    g.DrawImage(DrawElefantQ.E[0], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        //if (Prob > 0)
                                        {
                                            if (LastRow != -1 && LastColumn != -1)
                                            {
                                                g.DrawImage(DrawElefantQ.E[0], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                                g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                                if (AllDraw.QuntumTable[0, (int)Row, (int)Column] == -1 && AllDraw.QuntumTable[1, (int)Row,(int)Column] == -1)
                                                {
                                                    AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                                    AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                                }
                                            }
                                            else                                                
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                            {
                                                g.DrawImage(DrawElefantQ.E[0], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                                g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                            }
                                            AllDraw.QuntumTable[0, AllDraw.QuntumTable[0, (int)Row, (int)Column], AllDraw.QuntumTable[1, (int)Row,(int)Column]] = -1;
                                            AllDraw.QuntumTable[1, AllDraw.QuntumTable[0, (int)Row, (int)Column], AllDraw.QuntumTable[1, (int)Row,(int)Column]] = -1;

                                        }
                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);                                    
                                }
                            }
                            else
                            {
                                Object O1 = new Object();
                                lock (O1)
                                {//Draw of Instant Image of Brown Elephant.
                                    g.DrawImage(DrawElefantQ.E[1], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                                    if (RingHalf)
                                    {
                                        int Prob = 180;
                                        if (AllDraw.Less != 0)
                                            Prob = 180 * (AllDraw.Less / int.MaxValue);
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, (int)Prob);
                                        //if (Prob > 0)
                                        {
                                            if (LastRow != -1 && LastColumn != -1)
                                            {
                                                g.DrawImage(DrawElefantQ.E[1], new Rectangle(LastRow * CellW, LastColumn * CellH, CellW, CellH));
                                                g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((LastRow * CellW)), (int)(LastColumn * (float)CellH), CellW, CellH), -45, (int)Prob);
                                                if (AllDraw.QuntumTable[0, (int)Row, (int)Column] == -1 && AllDraw.QuntumTable[1, (int)Row,(int)Column] == -1)
                                                {
                                                    AllDraw.QuntumTable[0, (int)Row, (int)Column] = LastRow;
                                                    AllDraw.QuntumTable[1, (int)Row, (int)Column] = LastColumn;
                                                }
                                            }                                            
                                        else
                                        if (AllDraw.QuntumTable[0, (int)Row, (int)Column] != -1 && AllDraw.QuntumTable[1, (int)Row, (int)Column] != -1)
                                            {
                                                g.DrawImage(DrawElefantQ.E[1], new Rectangle(AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW, AllDraw.QuntumTable[1, (int)Row, (int)Column] * CellH, CellW, CellH));
                                                g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((AllDraw.QuntumTable[0, (int)Row, (int)Column] * CellW)), (int)(AllDraw.QuntumTable[1, (int)Row, (int)Column] * (float)CellH), CellW, CellH), -45, (int)Prob);

                                            }
                                            AllDraw.QuntumTable[0, AllDraw.QuntumTable[0, (int)Row, (int)Column], AllDraw.QuntumTable[1, (int)Row,(int)Column]] = -1;
                                            AllDraw.QuntumTable[1, AllDraw.QuntumTable[0, (int)Row, (int)Column], AllDraw.QuntumTable[1, (int)Row,(int)Column]] = -1;

                                        }
                                    }
                                    else
                                        g.DrawArc(new Pen(new SolidBrush(Color.Red)), new Rectangle((int)((Row * (float)CellW)), (int)(Column * (float)CellH), CellW, CellH), -45, 360);

                                }
                            }

                       
                    }
                }
            }
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("DrawSoldierOnTable:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
        }
    }
}
//End of Documentation.