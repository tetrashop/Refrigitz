﻿using System;
using System.Collections.Generic;
using System.IO;
namespace Refregitz_12
{
    [Serializable]
    public class DrawCastle
    {




        public int WinOcuuredatChiled = 0; public int LoseOcuuredatChiled = 0;
        private readonly object balanceLock = new object();
        private readonly object balanceLockS = new object();
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
            try
            {
                Object a = new Object();
                lock (a)
                {
                    string stackTrace = ex.ToString();
                    File.AppendAllText(AllDraw.Root + "\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString()); // path of file where stack trace will be stored.
                }
            }
            catch (Exception t) { Log(t); }
        }
        public void Dispose()
        {
            ValuableSelfSupported = null;
            C = null;
        }
        public bool MaxFound(ref bool MaxNotFound)
        {
            try
            {
                double a = ReturnHuristic();
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
                    return true;
                }
            }
            catch (Exception t)
            {
                Log(t);

            }
            MaxNotFound = true;
            return false;
        }
        public double ReturnHuristic()
        {
            double a = 0;
            for (int ii = 0; ii < AllDraw.CastleMovments; ii++)
                try
                {
                    a += CastleThinking[ii].ReturnHuristic(-1, -1, Order, false);
                }
                catch (Exception t)
                {
                    Log(t);
                }

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

            lock (balanceLock)
            {
                if (C[0] == null && C[1] == null)
                {
                    C[0] = Image.FromFile(AllDraw.ImagesSubRoot + "BrG.png");
                    C[1] = Image.FromFile(AllDraw.ImagesSubRoot + "BrB.png");
                }

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
                for (int ii = 0; ii < 8; ii++)
                    for (int jj = 0; jj < 8; jj++)
                        Table[ii, jj] = Tab[ii, jj];
                for (int ii = 0; ii < AllDraw.CastleMovments; ii++)
                    CastleThinking[ii] = new ThinkingChess(CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, (int)i, (int)j, a, Tab, 16, Ord, TB, Cur, 4, 4);

                Row = i;
                Column = j;
                color = a;
                Order = Ord;
                Current = Cur;
            }

        }
        //Clone a Copy.
        public void Clone(ref DrawCastle AA//, ref AllDraw. THIS
            )
        {
            int[,] Tab = new int[8, 8];
            for (int i = 0; i < 8; i++)
                for (int j = 0; j < 8; j++)
                    Tab[i, j] = this.Table[i, j];
            //Initiate a Constructed Brideges an Clone a Copy.
            AA = new DrawCastle(CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, this.Row, this.Column, this.color, this.Table, this.Order, false, this.Current);
            AA.ArrangmentsChanged = ArrangmentsChanged;
            for (int i = 0; i < AllDraw.CastleMovments; i++)
            {
                try
                {
                    AA.CastleThinking[i] = new ThinkingChess(CurrentAStarGredyMax, MovementsAStarGreedyHuristicFoundT, IgnoreSelfObjectsT, UsePenaltyRegardMechnisamT, BestMovmentsT, PredictHuristicT, OnlySelfT, AStarGreedyHuristicT, ArrangmentsChanged, (int)this.Row, (int)this.Column);
                    this.CastleThinking[i].Clone(ref AA.CastleThinking[i]);
                }
                catch (Exception t)
                {
                    Log(t);
                    AA.CastleThinking[i] = null;
                }
            }
            AA.Table = new int[8, 8];
            for (int ii = 0; ii < 8; ii++)
                for (int jj = 0; jj < 8; jj++)
                    AA.Table[ii, jj] = Tab[ii, jj];
            AA.Row = Row;
            AA.Column = Column;
            AA.Order = Order;
            AA.Current = Current;
            AA.color = color;

        }
        //Draw An Instatnt Brideges Images On the Table Method.
        public void DrawCastleOnTable(ref Graphics g, int CellW, int CellH)
        {
            try
            {

                lock (balanceLockS)
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
        }
    }
}
//End of Documents.