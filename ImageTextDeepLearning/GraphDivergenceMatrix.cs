﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContourAnalysisNS
{
    public class GraphS
    {
        public static GraphS Z = null;
        public static bool Drawn = false;
        public static GraphDivergenceMatrix ZB = null;
        public GraphDivergenceMatrix A, B;
        private readonly int N, M;
        public GraphS(bool[,] Ab, bool[,] Bb, int n, int m, bool Achange)
        {
            Drawn = false;
            N = n;
            M = m;
            // if (!Achange)
            {

                A = new GraphDivergenceMatrix(Ab, n, m);
                if (A != null)
                {
                    A.IJBelongToLineHaveFalseBolleanA(Ab);
                    A.CreateClosedCurved();
                }
                Drawn = true;
            }
            B = new GraphDivergenceMatrix(Bb, n, m);

            if (B != null)
            {
                B.IJBelongToLineHaveFalseBolleanA(Bb);
                B.CreateClosedCurved();
            }
            Drawn = true;
        }
        protected void Dispose()
        {
            A.M = 0;
            A.N = 0;
            A.Xl.Clear();
            A.Xv.Clear();
            B.Xl.Clear();
            B.Xv.Clear();

        }
        public static bool GraphSameRikht(bool[,] Ab, bool[,] Bb, int n, int m, bool Achange)
        {
            //if (Z != null)
            //xZ.Dispose();
            /*GraphDivergenceMatrix A = null;
            if (Z != null)
            {
                A = Z.A;
                
            }*/
            Z = new GraphS(Ab, Bb, n, m, Achange);
            /*if (Achange)
                Z.A = A;*/
            if (Z.A == null && Z.B == null)
            {
                return true;
            }

            if (Z.A == null || Z.B == null)
            {
                return false;
            }
            if (Z.A.numberOfClosedCurved == Z.B.numberOfClosedCurved)
            {
                return Z.SameRikhtThisIsLessVertex(Ab, Bb);
            }
            return false;
        }

        //When the matrix iss  the same  return true;
        private bool SameRikhtThisIsLessVertex(bool[,] Ab, bool[,] Bb)
        {
            bool Is = false;
            List<Vertex> K = new List<Vertex>();
            List<Vertex> ChechOnFinisshed = new List<Vertex>();
            if (A.Xv.Count < B.Xv.Count)
            {
                Is = A.IsSameRikhtVertex(Ab, B, ref K, ref ChechOnFinisshed);
              /* GraphDivergenceMatrix RecreatedB = new GraphDivergenceMatrix(ChechOnFinisshed, B.Xl, N, M);
                if (Is)
                {
                    if (!GraphDivergenceMatrix.CheckedIsSameRikhtOverLap(A, RecreatedB))
                    {
                        ZB = RecreatedB;
                        Is = false;
                    }
                }*/

            }

            else
            {
                Is = B.IsSameRikhtVertex(Bb, A, ref K, ref ChechOnFinisshed);
                /* GraphDivergenceMatrix RecreatedA = new GraphDivergenceMatrix(ChechOnFinisshed, A.Xl, N, M);

                 if (Is)
                 {
                     if (!GraphDivergenceMatrix.CheckedIsSameRikhtOverLap(B, RecreatedA))
                     {
                         ZB = RecreatedA;

                         Is = false;
                     }

                 }*/
            }
            return Is;
        }

    }
    public class GraphDivergenceMatrix
    {
        public int numberOfClosedCurved = 0;
        List<List<Vertex>> ClosedCurved = new List<List<Vertex>>();
        List<bool> IsClosedCurved = new List<bool>();
        List<int>  ClosedCurvedIndexI = new List<int>();

        public List<Vertex> Xv = new List<Vertex>();
        public List<Line> Xl = new List<Line>();
        public int N, M;
        int NoCloCur()
        {
            int Is = 0;
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, ClosedCurved.Count, i =>
            */
            for (int i = 0; i < ClosedCurved.Count; i++)
            {
                /* ParallelOptions poo = new ParallelOptions
                 {
                     MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                 }; Parallel.For(0, ClosedCurved[i].Count, j =>
                */ //
                for (int j = 0; j < ClosedCurved[i].Count; j++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                        //   if (i >= Xv.Count)
                        // return;

                        Is++;
                    }
                }//);
            }//);
            return Is;
        }
        bool ExistCloCur(int x1, int y1)
        {
            bool Is = false;
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, ClosedCurved.Count, i =>
            */
            for (int i = 0; i < ClosedCurved.Count; i++)
            {
                /* ParallelOptions poo = new ParallelOptions
                 {
                     MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                 }; Parallel.For(0, ClosedCurved[i].Count, j =>
                */ //
                for (int j = 0; j < ClosedCurved[i].Count; j++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                     //   if (i >= Xv.Count)
                           // return;

                        if (ClosedCurved[i][j].X == x1 && ClosedCurved[i][j].Y == y1)
                        {
                            Is = true;
                        }
                    }
                }//);
            }//);
            return Is;
        }
        bool ExistCloCurContinuse(int x1, int y1)
        {
            bool Is = false;
            /* ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, ClosedCurved.Count, i =>*/
            for (int i = 0; i < ClosedCurved.Count; i++)
            {

                object hh = new object();
                lock (hh)
                {
                    if (i >= Xv.Count)
                        continue;
                    if (ClosedCurved[i].Count == 0)
                        continue;
                    if (ClosedCurved[i][ClosedCurved[i].Count - 1].X == x1 && ClosedCurved[i][ClosedCurved[i].Count - 1].Y == y1)
                    {
                        Is = true;
                    }
                }

            }//);
            return Is;
        }
        int GetVerInd(int VertNo)
        {
            int vern = 0;
            for(int i = 0; i < Xv.Count; i++)
            {
                if (Xv[i].VertexNumber == VertNo)
                    return i;
            }
            return vern;
        }
        int GetVerId(int id)
        {

            return Xv[id].VertexNumber;
        }
        int GetNotClosedCurvedIndexI()
        {
            int v = -1;
            for (int i = 0; i < Xv.Count; i++)
            {
                bool Is = false;
                for (int j = 0; j < ClosedCurvedIndexI.Count; j++)
                {
                    
                        if (ClosedCurvedIndexI[j] == (i))
                        {
                            Is = true;
                            break;
                        }
                   
                }
                if (!Is)
                    return i;
            }
            return v;
        }
        public void CreateClosedCurved()
        {
            bool IsNext = false;
            int noIsNext = 0;
            int i = 0;
            try
            {
                do
                {
                    IsNext = false;
                    ClosedCurved.Add(new List<Vertex>());

                    int j = 0;
                    while (j < Xv.Count)
                    {
                        if (ClosedCurved[ClosedCurved.Count - 1].Count > 0)
                        {
                            i = GetVerInd(ClosedCurved[ClosedCurved.Count - 1][ClosedCurved[ClosedCurved.Count - 1].Count - 1].VertexNumber);
                            ClosedCurvedIndexI.Add(i);
                        }
                        else
                        {
                            if (!ExistCloCur(Xv[i].X, Xv[i].Y))
                            {
                                ClosedCurved[ClosedCurved.Count - 1].Add(Xv[i]);
                                i = GetVerInd(ClosedCurved[ClosedCurved.Count - 1][ClosedCurved[ClosedCurved.Count - 1].Count - 1].VertexNumber);
                                ClosedCurvedIndexI.Add(i);
                            }
                        }

                        if (ExistCloCur(Xv[j].X, Xv[j].Y))
                        {
                            j++;
                            continue;
                        }
                        Line ds = d(Xv[i], Xv[j]);
                        if (ds == null)
                        {
                            j++;
                            continue;
                        }
                        if (!(ds.VertexIndexX == Xv[j].VertexNumber || ds.VertexIndexY == Xv[j].VertexNumber))
                        {
                            j++;
                            continue;
                        }
                        if (j < Xv.Count && ClosedCurved[ClosedCurved.Count - 1].Count > 0)
                        {
                            if ((ClosedCurved[ClosedCurved.Count - 1][0].VertexNumber == Xv[j].VertexNumber || ClosedCurved[ClosedCurved.Count - 1][0].VertexNumber == Xv[j].VertexNumber))
                            {
                                IsNext = true;
                                ClosedCurved[ClosedCurved.Count - 1].Add(Xv[j]);
                                numberOfClosedCurved++;
                                break;
                            }
                         }
                        if (j < Xv.Count)
                            ClosedCurved[ClosedCurved.Count - 1].Add(Xv[j]);
                        j = 0;
                    }

                    IsClosedCurved.Add(IsNext);

                    if (!IsNext)
                    {
                         i = GetNotClosedCurvedIndexI();
                        if (i == -1)
                            noIsNext = Xv.Count;
    for (int h = 0; h < IsClosedCurved.Count; h++)
                        {
                            if (!IsClosedCurved[h])
                            {
                                IsClosedCurved.RemoveAt(h);
                                ClosedCurved.RemoveAt(h);
                            }
                        }
                     
                        noIsNext++;
                    }

                } while (NoCloCur() != Xv.Count && noIsNext < Xv.Count);
            }catch(Exception t) { 
                MessageBox.Show(t.ToString()); }
        }

        public bool ExistV(int x1, int y1)
        {
            bool Is = false;
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, Xv.Count, i =>*/
            for (int i = 0; i < Xv.Count; i++)
            {
                /*ParallelOptions poo = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Xv.Count, j =>*/
                for (int j = 0; j < Xv.Count; j++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                        if (i >= Xv.Count)
                            continue;

                        if (Xv[i].X == x1 && Xv[i].Y == y1 )
                        {
                            Is = true;
                        }

                        
                    }
                }//);
            }//);
            return Is;
        }
        public bool ExistL(int x1, int y1)
        {

            bool Is = false;
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, Xl.Count, i =>*/
            for (int i = 0; i < Xl.Count; i++)
            {
                object hh = new object();
                lock (hh)
                {
                    if (i >= Xl.Count)
                        continue;
                    if (Xl[i].VertexIndexX == x1 && Xl[i].VertexIndexY  == y1&& i < Xl.Count)
                    {
                        Is = true;
                    }

                    if (Xl[i].VertexIndexY  == x1 && Xl[i].VertexIndexX == y1 && i < Xl.Count)
                    {
                        Is = true;
                    }
                }
            }//);
            return Is;
        }
        public void XiXjDelete()
        {
            try
            {
                XiXjDeleteLessX();
                XiXjDeleteGreatX();
                XiXjDeleteLessY();
                XiXjDeleteGreatY();
            }catch(Exception t)
            {
                MessageBox.Show(t.ToString());
            }
        }
        public void XiXjDeleteGreatX()
        {
            try
            {
                List<Vertex> K = new List<Vertex>();
                bool Is = false;
                /*ParallelOptions po = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Xv.Count, i =>*/
                for (int i = 0; i < Xv.Count; i++)
                {
                    /*ParallelOptions poo = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, Xv.Count, j =>*/
                    for (int j = 0; j < Xv.Count; j++)
                    {
                        /*ParallelOptions pooo = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, Xv.Count, k =>*/
                        for (int k = 0; k < Xv.Count; k++)
                        {
                            object hh = new object();
                            lock (hh)
                            {
                                if (i == j)
                                {
                                    continue;
                                }

                                if (i == k)
                                {
                                    continue;
                                }

                                if (j == k)
                                {
                                    continue;
                                }
                                if (i == k)
                                {
                                    continue;
                                }
                                if ((!(Xv[k].X > Xv[i].X && Xv[k].X > Xv[j].X))|| k >= Xv.Count|| i >= Xv.Count)
                                {
                                    continue;
                                }

                                Line ds = d(Xv[i], Xv[j]);
                                if (ds != null)
                                {
                                    if (Xv[i].VertexNumber == Xv[k].VertexNumber)
                                    {
                                        Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv, i + 1);
                                        if (Is)
                                        {
                                            try
                                            {
                                                Xl.Remove(ds);
                                            }
                                            catch (Exception) { }
                                        }
                                    }
                                }
                            }
                        }//);
                    }//);
                }//);
            }
            catch (Exception)
            {
                return;
            }
        }
        public void XiXjDeleteGreatY()
        {
            try
            {
                List<Vertex> K = new List<Vertex>();
                bool Is = false;
                /*ParallelOptions po = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Xv.Count, i =>*/
                for (int i = 0; i < Xv.Count; i++)
                {
                    /*ParallelOptions poo = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, Xv.Count, j =>*/
                    for (int j = 0; j < Xv.Count; j++)
                    {
                        /*ParallelOptions pooo = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, Xv.Count, k =>*/
                        for (int k = 0; k < Xv.Count; k++)
                        {
                            object hh = new object();
                            lock (hh)
                            {
                                if (i == j)
                                {
                                    continue;
                                }

                                if (i == k)
                                {
                                    continue;
                                }

                                if (j == k)
                                {
                                    continue;
                                }
                                if (i == k)
                                {
                                    continue;
                                }

                                if ((!(Xv[k].Y > Xv[i].Y && Xv[k].Y > Xv[j].Y)))
                                {
                                    continue;
                                }
                                Line ds = d(Xv[i], Xv[j]);
                                if (ds != null)
                                {
                                    if (Xv[i].VertexNumber == Xv[k].VertexNumber)
                                    {
                                        Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv, i + 1);
                                        if (Is)
                                        {
                                            try
                                            {
                                                Xl.Remove(ds);
                                            }
                                            catch (Exception) { }
                                        }
                                    }
                                }
                            }
                        }//);
                    }//);
                }//);
            }
            catch (Exception)
            {
                return;
            }
        }
        public void XiXjDeleteLessX()
        {
            try
            {
                List<Vertex> K = new List<Vertex>();
                bool Is = false;
                /*ParallelOptions po = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Xv.Count, i =>*/
                for (int i = 0; i < Xv.Count; i++)
                {
                    /*ParallelOptions poo = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, Xv.Count, j =>*/
                    for (int j = 0; j < Xv.Count; j++)
                    {
                        /*ParallelOptions pooo = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, Xv.Count, k =>*/
                        for (int k = 0; k < Xv.Count; k++)
                        {
                            object hh = new object();
                            lock (hh)
                            {
                                if (i == j)
                                {
                                    continue;
                                }

                                if (i == k)
                                {
                                    continue;
                                }

                                if (j == k)
                                {
                                    continue;
                                }
                                if (i == k)
                                {
                                    continue;
                                }
                                if ((!(Xv[k].X < Xv[i].X && Xv[k].X < Xv[j].X)) || k >= Xv.Count || i >= Xv.Count)
                                {
                                    continue;
                                }



                                Line ds = d(Xv[i], Xv[j]);
                                if (ds != null)
                                {
                                    if (Xv[i].VertexNumber == Xv[k].VertexNumber)
                                    {
                                        Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv, i + 1);
                                        if (Is)
                                        {
                                            try
                                            {
                                                Xl.Remove(ds);
                                            }
                                            catch (Exception) { }
                                        }
                                    }
                                }
                            }
                        }//);
                    }//);
                }//);
            }
            catch (Exception)
            {
                return;
            }
        }
        public void XiXjDeleteLessY()
        {
            try
            {
                List<Vertex> K = new List<Vertex>();
                bool Is = false;
                /*ParallelOptions po = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Xv.Count, i =>*/
                for (int i = 0; i < Xv.Count; i++)
                {
                    /*ParallelOptions poo = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, Xv.Count, j =>*/
                    for (int j = 0; j < Xv.Count; j++)
                    {
                        /*ParallelOptions pooo = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, Xv.Count, k =>*/
                        for (int k = 0; k < Xv.Count; k++)
                        {
                            object hh = new object();
                            lock (hh)
                            {
                                if (i == j)
                                {
                                    continue;
                                }

                                if (i == k)
                                {
                                    continue;
                                }

                                if (j == k)
                                {
                                    continue;
                                }
                                if (i == k)
                                {
                                    continue;
                                }
                                if ((!(Xv[k].Y < Xv[i].Y && Xv[k].Y < Xv[j].Y)) || k >= Xv.Count || i >= Xv.Count)
                                {
                                    continue;
                                }
                               

                                Line ds = d(Xv[i], Xv[j]);
                                if (ds != null)
                                {
                                    if (Xv[i].VertexNumber == Xv[k].VertexNumber)
                                    {
                                        Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv, i + 1);
                                        if (Is)
                                        {
                                            try
                                            {
                                                Xl.Remove(ds);
                                            }
                                            catch (Exception) { }
                                        }
                                    }
                                }
                            }
                        }//);
                    }//);
                }//);
            }
            catch (Exception)
            {
                return;
            }
        }

        public Line d(Vertex A, Vertex B)
        {
            Line dd = null;
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, Xl.Count, i =>*/
            for (int i = 0; i < Xl.Count; i++)
            {
                object hh = new object();
                lock (hh)
                {
                    if (Xl.Count <= i)
                    {
                        continue;
                    }

                    try
                    {
                        if (A.VertexNumber == Xl[i].VertexIndexX && B.VertexNumber == Xl[i].VertexIndexY && (Xl.Count > i))
                        {
                            dd = Xl[i];
                        }

                        if (Xl.Count <= i)
                        {
                            continue;
                        }

                        if (B.VertexNumber == Xl[i].VertexIndexX && A.VertexNumber == Xl[i].VertexIndexY && (Xl.Count > i))
                        {
                            dd = Xl[i];
                        }

                        if (Xl.Count <= i)
                        {
                            continue;
                        }

                        if (A.VertexNumber == Xl[i].VertexIndexY && B.VertexNumber == Xl[i].VertexIndexX && (Xl.Count > i))
                        {
                            dd = Xl[i];
                        }

                        if (Xl.Count <= i)
                        {
                            continue;
                        }

                        if (B.VertexNumber == Xl[i].VertexIndexY && A.VertexNumber == Xl[i].VertexIndexX && (Xl.Count > i))
                        {
                            dd = Xl[i];
                        }
                    }
                    catch (Exception) { }
                }
            }//);
            return dd;

        }

        private bool IsXixJisDeletable(ref Line C, Vertex A, Vertex B, Vertex Next, ref List<Vertex> K, ref List<Vertex> xlv,int i)
        {
            bool Is = false;
            //for (int i = 0; i < xlv.Count - 1; i++)
            {
                if (Is)
                    return Is;
                if (i < xlv.Count)
                {
                    if (Next.VertexNumber == B.VertexNumber)
                    {
                        return true;
                    }

                    if (xlv[i].VertexNumber == Next.VertexNumber)
                    {

                        K.Add(xlv[i]);
                        Is = Is || IsXixJisDeletable(ref C, A, B, xlv[i + 1], ref K, ref xlv, i + 1);

                    }

                }
            }
            return Is;
        }
        public void IJBelongToLineHaveFalseBolleanA(bool[,] A)
        {
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, Xv.Count, i =>*/
            for (int i = 0; i < Xv.Count; i++)
            {
               /* ParallelOptions poo = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Xv.Count, k =>*/
                for(int k = 0; k < Xv.Count; k++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                        if (Xl.Count <= i)
                        {
                            continue;
                        }

                        if (Xl.Count <= k)
                        {
                            continue;
                        }

                        if (ExistL(Xv[i].VertexNumber, Xv[k].VertexNumber))
                        {

                            int x1 = Xv[i].X;
                            int y1 = Xv[i].Y;
                            int x2 = Xv[k].X;
                            int y2 = Xv[k].Y;
                            if (x1 < x2)
                            {
                                if (y1 < y2)
                                {
                                    for (int x = x1 + 1; x < x2 - 1; x++)
                                    {
                                        for (int y = y1 + 1; y < y2 - 1; y++)
                                        {
                                            if ((!A[x, y]) && Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                            {
                                                Line ds = d(Xv[i], Xv[k]);
                                                if (ds != null)
                                                {
                                                    if (Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                                    {
                                                        Xl.Remove(ds);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                    if (y1 > y2)
                                {
                                    for (int x = x1 + 1; x < x2 - 1; x++)
                                    {
                                        for (int y = y2 + 1; y < y1 - 1; y++)
                                        {
                                            if ((!A[x, y]) && Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                            {
                                                Line ds = d(Xv[i], Xv[k]);
                                                if (ds != null)
                                                {
                                                    if (Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                                    {
                                                        Xl.Remove(ds);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            else
                                if (x2 < x1)
                            {
                                if (y1 < y2)
                                {
                                    for (int x = x2 + 1; x < x1 - 1; x++)
                                    {
                                        for (int y = y1 + 1; y < y2 - 1; y++)
                                        {
                                            if ((!A[x, y]) && Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                            {
                                                Line ds = d(Xv[i], Xv[k]);
                                                if (ds != null)
                                                {
                                                    if (Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                                    {
                                                        Xl.Remove(ds);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                 if (y1 > y2)
                                {
                                    for (int x = x2 + 1; x < x1 - 1; x++)
                                    {
                                        for (int y = y2 + 1; y < y1 - 1; y++)
                                        {
                                            if ((!A[x, y]) && Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                            {
                                                Line ds = d(Xv[i], Xv[k]);
                                                if (ds != null)
                                                {
                                                    if (Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                                    {
                                                        Xl.Remove(ds);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            }
                            // if (Line.IsPointsInVertexes(Xv[i], Xv[k],))
                        }
                    }
                }//);
            }//);
        }
        public GraphDivergenceMatrix(bool[,] A, int n, int m)
        {
            int First = 0;
            N = n;
            M = m;
            int indv = 0;
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, n, i =>*/
           for (int i = 0; i < n; i++)
            {
                /*ParallelOptions poo = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, m, j =>*/
               for (int j = 0; j < m; j++)
                {
                   /* ParallelOptions pooo = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, n, k =>*/
                    for (int k = 0; k < n; k++)
                    {
                        /*ParallelOptions pooooo = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, m, p =>*/
                        for (int p = 0; p < m; p++)
                        {
                            object h = new object();
                            lock (h)
                            {


                                if (i == k)
                                {
                                    continue;
                                }
                                if (i == j)
                                {
                                    continue;
                                }
                                if (i == p)
                                {
                                    continue;
                                }
                                if (j == k)
                                {
                                    continue;
                                }

                                if (j == p)
                                {
                                    continue;
                                }
                                if (k == p)
                                {
                                    continue;
                                }


                                if (A[i, j] && A[k, p])
                                {
                                    if (Xv.Count > 0)
                                    {
                                        if (Xv[First].X == k && Xv[First].Y == p)
                                        {
                                            Xv.Add(new Vertex(indv, i, j));
                                            Xv.Add(new Vertex(First, k, p));
                                            if (!ExistL(indv - 1, First))
                                            {
                                                float we = (float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p));
                                                Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                First = indv + 1;
                                            }
                                        }
                                        else
                                        {

                                            int inh = indv;
                                            int inh1 = indv;
                                            int inh2 = indv;
                                            if (!ExistV(i, j))
                                            {
                                                Xv.Add(new Vertex(++indv, i, j));
                                                inh1 = indv;
                                            }
                                            if (!ExistV(k, p))
                                            {
                                                Xv.Add(new Vertex(++indv, k, p));
                                                inh2 = indv;
                                            }
                                            if (inh1 != inh2)
                                            {
                                                if (inh == indv - 1)
                                                {
                                                    if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                    {
                                                        float we = (float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p));
                                                        Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                    }
                                                }
                                                else
                                                {
                                                    if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                    {
                                                        float we = (float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p));
                                                        Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                    }
                                                }
                                            }
                                        }

                                    }
                                    else
                                    {

                                        int inh = indv;
                                        int inh1 = indv;
                                        int inh2 = indv;
                                        if (!ExistV(i, j))
                                        {
                                            Xv.Add(new Vertex(++indv, i, j));
                                            inh1 = indv;
                                        }
                                        if (!ExistV(k, p))
                                        {
                                            Xv.Add(new Vertex(++indv, k, p));
                                            inh2 = indv;
                                        }
                                        if (inh1 != inh2)
                                        {
                                            if (inh == indv - 1)
                                            {
                                                if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                {
                                                    float we = (float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p));
                                                    Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                }
                                            }
                                            else
                                            {
                                                if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                {
                                                    float we = (float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p));
                                                    Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }//);
                    }//);
                }//);
            }//);
            XiXjDelete();

        }
        //reconstruction;
        public GraphDivergenceMatrix(List<Vertex> A, List<Line> Xl, int n, int m)
        {
            N = n;
            M = m;
            //To Do Create Graph mininimum graph
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, Xl.Count, k =>*/
            for (int k = 0; k < Xl.Count; k++)
            {
                /*ParallelOptions poo = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Xl.Count, i =>*/
                for (int i = 0; i < A.Count; i++)
                {
                    /* ParallelOptions pooo = new ParallelOptions
                     {
                         MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                     }; Parallel.For(0, Xl.Count, j =>*/
                    for (int j = 0; j < A.Count; j++)
                    {

                        object h = new object();
                        lock (h)
                        {

                            if (i == k)
                            {
                                continue;
                            }
                            if (i == j)
                            {
                                continue;
                            }
                           
                            if (j == k)
                            {
                                continue;
                            }

                            

                            if (!ExistV(Xv[i].X, Xv[j].Y) && (Xl.Count > k) && (A.Count > j))
                            {
                                Xv.Add(new Vertex(Xl[k].VertexIndexX, A[i].X, A[i].Y));
                            
                            }
                            if (!ExistV(Xv[GetVerInd(Xl[k].VertexIndexX)].X, Xv[GetVerInd(Xl[k].VertexIndexY)].Y) && (Xl.Count > k) && (A.Count > j))
                            {
                                Xv.Add(new Vertex(Xl[k].VertexIndexY, A[j].X, A[j].Y));

                            }
                            if (!ExistL(Xl[k].VertexIndexX, Xl[k].VertexIndexY) && (Xl.Count > k) && (A.Count > j) && (A.Count > i))
                            {
                                float we = (float)Math.Sqrt((A[i].X - A[j].X) * (A[i].X - A[j].X) + (A[i].Y - A[j].Y) * (A[i].Y - A[j].Y));
                                Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                            }



                        }
                    }//);
                }//);
            }//);
            XiXjDelete();
        }
        public static bool CheckedIsSameRikhtOverLap(GraphDivergenceMatrix sma, GraphDivergenceMatrix Rec)
        {
            bool Is = false;
            List<Vertex> Sames = new List<Vertex>();
            /*ParallelOptions po = new ParallelOptions
            {
                MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
            }; Parallel.For(0, sma.Xv.Count, i =>*/
           for (int i = 0; i < sma.Xv.Count; i++)
            {
                /*ParallelOptions poo = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Rec.Xv.Count, j =>*/
                for (int j = 0; j < Rec.Xv.Count; j++)
                {
                    object h = new object();
                    lock (h)
                    {
                        if (sma.Xv[i].X == Rec.Xv[j].X)
                        {
                            if (sma.Xv[i].Y == Rec.Xv[j].Y)
                            {
                                Sames.Add(new Vertex(sma.Xv[i].VertexNumber, sma.Xv[i].X, sma.Xv[i].Y));
                            }
                        }
                    }
                }//);
            }//);
            if (Sames.Count == Rec.Xv.Count)
            {
                Is = true;
            }
            else
            {
                if (Is)
                {
                    return Is;
                }

               /* ParallelOptions pop = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Sames.Count, i =>*/
                for (int i = 0; i < Sames.Count; i++)
                {
                    int crein = -1;
                    /*ParallelOptions poop = new ParallelOptions
                    {
                        MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                    }; Parallel.For(0, Rec.Xv.Count, j =>*/
                    for (int j = 0; j < Rec.Xv.Count; j++)
                    {
                        object h = new object();
                        lock (h)
                        {
                            if (Sames[i].VertexNumber != Rec.Xv[j].VertexNumber)
                            {
                                crein = j;
                            }
                            else
                            {
                                crein = -1;
                                continue;
                            }
                        }
                    }//);
                    if (crein > -1)
                    {
                        /*ParallelOptions poopp = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, Sames.Count, j =>*/
                        for (int j = 0; j < Sames.Count; j++)
                        {
                            object h = new object();
                            lock (h)
                            {
                                if (Is)
                                {
                                    continue;
                                }

                                if (j == i)
                                {
                                    continue;
                                }

                                if (Rec.Xv.Count <= i)
                                {
                                    continue;
                                }

                                if (Line.IsPointsInVertexes(Sames[i], Sames[j], Rec.Xv[crein].X, Rec.Xv[crein].Y) && (Rec.Xv.Count > i))
                                {
                                    Sames.Add(new Vertex(Rec.Xv[i].VertexNumber, Rec.Xv[i].X, Rec.Xv[i].Y));
                                    Is = true;
                                    continue;
                                }
                            }
                        }//);
                    }
                }//);

            }
            if (Sames.Count == Rec.Xv.Count)
            {
                Is = true;
            }

            return Is;
        }

        public bool IsSameRikhtVertex(bool[,] Ab, GraphDivergenceMatrix BB, ref List<Vertex> Kk, ref List<Vertex> ChechOnFinisshedI)
        {
            bool Is = false;
            List<Vertex> ChechOnFinisshed = ChechOnFinisshedI;
            List<Vertex> K = Kk;
           bool kcounnt = false;
            /*    ParallelOptions pop = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, Xv.Count, i =>*/
            for (int i = 0; i < Xv.Count; i++)
            {
                object h = new object();
                lock (h)
                {
                    if (Is)
                    {
                        return Is;
                    }

                    if (kcounnt)
                    {
                        return Is;
                    }

                    Is = Is || IsSameRikht(Ab, Xv[i].X, Xv[i].Y, BB, ref K, ref ChechOnFinisshed);
                    if (K.Count == 0)
                    {
                        kcounnt = true;
                    }
                }
            }//);
            Kk = K;
            ChechOnFinisshedI = ChechOnFinisshed;
            return Is;
        }

        private bool IsSameRikht(bool[,] Ab, int x, int y, GraphDivergenceMatrix BB, ref List<Vertex> Kk, ref List<Vertex> ChechOnFinisshedI)
        {
            bool exit = false;
            bool Is = false;
            List<Vertex> ChechOnFinisshed = ChechOnFinisshedI;
            List<Vertex> K = Kk;
            if (x < 0 || y < 0 || x >= M || y >= N)
            {
                return false;
            }

            if (K.Count >= BB.Xv.Count)
            {
                return true;
            }

            try
            {
                /*ParallelOptions pop = new ParallelOptions
                {
                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                }; Parallel.For(0, BB.Xv.Count, i =>*/
                for (int i = 0; i < BB.Xv.Count; i++)
                {
                    try
                    {
                        object h = new object();
                        lock (h)
                        {
                            if (exit)
                            {
                                return Is; 
                            }

                            if (K.Count > 0)
                            {
                                if (K.Contains(BB.Xv[i]))
                                {
                                    continue;
                                }
                            }
                            if (x == BB.Xv[i].X && y == BB.Xv[i].Y)
                            {
                                K.Add(BB.Xv[i]);
                                ChechOnFinisshed.Add(new Vertex(BB.Xv[i].VertexNumber, x, y));
                                Is = false;
                                exit = true;
                                return Is;
                            }
                        }
                        /*ParallelOptions poop = new ParallelOptions
                        {
                            MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                        }; Parallel.For(0, K.Count, ii =>*/
                        for (int ii = 0; ii < K.Count; ii++)
                        {
                            if (exit)
                            {
                                return Is;
                            }

                            try
                            {
                                /*ParallelOptions poopp = new ParallelOptions
                                {
                                    MaxDegreeOfParallelism = System.Threading.PlatformHelper.ProcessorCount
                                }; Parallel.For(0, K.Count, j =>*/
                                for (int j = 0; j < K.Count; j++)
                                {
                                    object hh = new object();
                                    lock (hh)
                                    {
                                        if (exit)
                                        {
                                            return Is;
                                        }

                                        if (i == j)
                                        {
                                            continue;
                                        }

                                        if (Line.IsPointsInVertexes(K[ii], K[j], x, y))
                                        {
                                            K.Add(BB.Xv[i]);
                                            ChechOnFinisshed.Add(new Vertex(BB.Xv[i].VertexNumber, x, y));
                                            exit = true;
                                            return Is;
                                        }
                                    }
                                }//);
                            }
                            catch (Exception) { }
                        }//);
                    }
                    catch (Exception) { }
                }//);
            }
            catch (Exception) { }
            if (x + 1 >= 0 && y + 1 >= 0 && x + 1 < M && y + 1 < N)
            {
                if (!Ab[x + 1, y + 1])
                {
                    Is = Is || IsSameRikht(Ab, x + 1, y + 1, BB, ref K, ref ChechOnFinisshed);
                }
            }
            if (x - 1 >= 0 && y - 1 >= 0 && x - 1 < M && y - 1 < N)
            {
                if (!Ab[x - 1, y - 1])
                {
                    Is = Is || IsSameRikht(Ab, x - 1, y - 1, BB, ref K, ref ChechOnFinisshed);
                }
            }
            if (x + 1 >= 0 && y - 1 >= 0 && x + 1 < M && y - 1 < N)
            {
                if (!Ab[x + 1, y - 1])
                {
                    Is = Is || IsSameRikht(Ab, x + 1, y - 1, BB, ref K, ref ChechOnFinisshed);
                }
            }
            if (x - 1 >= 0 && y + 1 >= 0 && x - 1 < M && y + 1 < N)
            {
                if (!Ab[x - 1, y + 1])
                {
                    Is = Is || IsSameRikht(Ab, x - 1, y + 1, BB, ref K, ref ChechOnFinisshed);
                }
            }
            Kk = K;
            ChechOnFinisshedI = ChechOnFinisshed;
            return Is;
        }
    }
    public class Vertex
    {

        public int VertexNumber;
        public int X, Y;
        public Vertex(int Vno, int x, int y)
        {
            VertexNumber = Vno;
            X = x;
            Y = y;
        }
    }
    public class Line
    {
        public int VertexIndexX, VertexIndexY;
        private readonly float Weigth;
        public Line(float Weit, int inx, int iny)
        {
            VertexIndexX = inx;
            VertexIndexY = iny;
            Weigth = Weit;
        }
        public static bool IsPointsInVertexes(Vertex v1, Vertex v2, int x, int y)
        {
            bool Is = false;
            if (v1.Y == v2.Y)
            {
                return false;
            }

            if (((y - v1.Y) - (((v1.X - v2.X) / (v1.Y - v2.Y)) * (x - v1.X))) < 1)
            {
                Is = true;
            }

            return Is;
        }
    }
}
