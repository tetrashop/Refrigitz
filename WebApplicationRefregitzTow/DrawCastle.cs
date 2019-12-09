using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
namespace RefrigtzW
{
    [Serializable]
    public class DrawCastle
    {
       

        StringBuilder Space = new StringBuilder("&nbsp;");
#pragma warning disable CS0414 // The field 'DrawCastle.Spaces' is assigned but its value is never used
        int Spaces = 0;
#pragma warning restore CS0414 // The field 'DrawCastle.Spaces' is assigned but its value is never used



        public int WinOcuuredatChiled = 0; public int LoseOcuuredatChiled = 0;
        //private readonly object balancelock = new object();
        //private readonly object balancelockS = new object();
        public static Image[] C = new Image[2];
        //Iniatite Global Variable.
        List<int[]> ValuableSelfSupported = new List<int[]>();

        public bool MovementsAStarGreedyHuristicFoundT = false;
        public bool IgnoreSelfObjectsT = false;
        public bool UsePenaltyRegardMechnisamT = true;
        public bool BestMovmentsT = false;
        public bool PredictHuristicT = true;
        public bool OnlySelfT = false;
        public bool AStarGreedyHuristicT = false;
        public bool ArrangmentsChanged = false;
        public static double MaxHuristicxB = -20000000000000000;
        public float Row, Column;
        public Color color;
        public ThinkingChess[] CastleThinking = new ThinkingChess[AllDraw.CastleMovments];
        public int[,] Table = null;
        public int Current = 0;
        public int Order;
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
        public void Dispose()
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            ValuableSelfSupported = null;
            C = null;
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("Dispose:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;

        }
        public bool MaxFound(ref bool MaxNotFound)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;

            int a = ReturnHuristic();
            if (MaxHuristicxB < a)
            {
                MaxNotFound = false;
                Object O = new Object();
                lock (O)
                {
                    if (ThinkingChess.MaxHuristicx < MaxHuristicxB)
                        ThinkingChess.MaxHuristicx = a;
                    MaxHuristicxB = a;
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
            for (var ii = 0; ii < AllDraw.CastleMovments; ii++)

                a += CastleThinking[ii].ReturnHuristic(-1, -1, Order, false,ref HaveKilled);

            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("ReturnHuristic:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
            return a;
        }


        //Constructor 1.
        /*  public DrawCastle(int CurrentAStarGredy, bool MovementsAStarGreedyHuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments)
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
          }*/
        //constructor 2.
        public DrawCastle(int CurrentAStarGredy, bool MovementsAStarGreedyHuristicTFou, bool IgnoreSelfObject, bool UsePenaltyRegardMechnisa, bool BestMovment, bool PredictHurist, bool OnlySel, bool AStarGreedyHuris, bool Arrangments, float i, float j, Color a, int[,] Tab, int Ord, bool TB, int Cur//, ref AllDraw. THIS
            )
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
                //Initiate Global Variable By Local Parmenter.
                Table = new int[8, 8];
                for (var ii = 0; ii < 8; ii++)
                    for (var jj = 0; jj < 8; jj++)
                        Table[ii, jj] = Tab[ii, jj];
                for (var ii = 0; ii < AllDraw.CastleMovments; ii++)
                    CastleThinking[ii] = new ThinkingChess(ii,4,CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, (int)i, (int)j, a, Tab, 16, Ord, TB, Cur, 4, 4);

                Row = i;
                Column = j;
                color = a;
                Order = Ord;
                Current = Cur;
            }
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("DrawCastle:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
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
        //Clone a Copy.
        public void Clone(ref DrawCastle AA//, ref AllDraw. THIS
            )
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            int[,] Tab = new int[8, 8];
            for (var i = 0; i < 8; i++)
                for (var j = 0; j < 8; j++)
                    Tab[i, j] = this.Table[i, j];
            //Initiate a Constructed Brideges an Clone a Copy.
            AA = new DrawCastle(CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, this.Row, this.Column, this.color, this.CloneATable(Table), this.Order, false, this.Current);
            AA.ArrangmentsChanged = ArrangmentsChanged;
            for (var i = 0; i < AllDraw.CastleMovments; i++)
            {

                AA.CastleThinking[i] = new ThinkingChess(i,4,CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, (int)this.Row, (int)this.Column);
                this.CastleThinking[i].Clone(ref AA.CastleThinking[i]);

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
        //Draw An Instatnt Brideges Images On the Table Method.
        public void DrawCastleOnTable(ref Graphics g, int CellW, int CellH)
        {
            //long Time = TimeElapced.TimeNow();Spaces++;
            try
            {
                object balancelockS = new object();

                lock (balancelockS)
                {

                    if (C[0] == null || C[1] == null)
                    {
                        C[0] = Image.FromFile(AllDraw.ImagesSubRoot + "BrG.png");
                        C[1] = Image.FromFile(AllDraw.ImagesSubRoot + "BrB.png");
                    }
                    if (((int)Row >= 0) && ((int)Row < 8) && ((int)Column >= 0) && ((int)Column < 8))
                    { //Gray Color.
                        if (Order == 1)
                        {
                            Object O1 = new Object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Draw a Gray Castles Instatnt Image On hte Tabe.
                                g.DrawImage(C[0], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                            }
                        }
                        else
                        {
                            Object O1 = new Object();
                            lock (O1)
                            {    //Draw an Instant from File of Gray Soldeirs.
                                 //Draw an Instatnt of Brown Castles On the Table.
                                g.DrawImage(C[1], new Rectangle((int)(Row * (float)CellW), (int)(Column * (float)CellH), CellW, CellH));
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                Log(t);
            }
            ////{ AllDraw.OutPut.Append("\r\n");for (int l = 0; l < Spaces; l++) AllDraw.OutPut.Append(Space);  AllDraw.OutPut.Append("DrawCastleOnTable:" + (TimeElapced.TimeNow() - Time).ToString());}Spaces--;
        }
    }
}
//End of Documents.