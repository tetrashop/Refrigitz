﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourAnalysisNS
{
    public class GraphS
    {
        public GraphDivergenceMatrix A, B;
        int N, M;
        public GraphS(bool[,] Ab, bool[,] Bb, int n, int m)
        {
            N = n;
            M = m;
            A = new GraphDivergenceMatrix(Ab, n, m);
            if (A != null)
            {
                A.IJBelongToLineHaveFalseBolleanA(Ab);
            }
            B = new GraphDivergenceMatrix(Bb, n, m);
            
            if (B != null)
            {
               B.IJBelongToLineHaveFalseBolleanA(Bb);
            }
        }
        public static bool GraphSameRikht(bool[,] Ab, bool[,] Bb, int n, int m)
        {
            GraphS Z = new GraphS(Ab, Bb, n, m);
            if (Z.A == null && Z.B == null)
                return true;
            if (Z.A == null || Z.B == null)
                return false;

            return Z.SameRikhtThisIsLessVertex(Ab,Bb);
        }

        //When the matrix iss  the same  return true;
        bool SameRikhtThisIsLessVertex(bool[,] Ab, bool[,] Bb)
        {
            bool Is = false;
            List<Vertex> K = new List<Vertex>();
            List<Vertex> ChechOnFinisshed = new List<Vertex>();
            if (A.Xv.Count < B.Xv.Count)
            {
                Is = A.IsSameRikhtVertex(Ab,B, ref K, ref ChechOnFinisshed);
                GraphDivergenceMatrix RecreatedB = new GraphDivergenceMatrix(ChechOnFinisshed, B.Xl, N, M);
                if (Is)
                {
                    if (!GraphDivergenceMatrix.CheckedIsSameRikhtOverLap(A, RecreatedB))
                        Is = false;
                }

            }

            else
            {
                Is = B.IsSameRikhtVertex(Ab,A, ref K, ref ChechOnFinisshed);
                GraphDivergenceMatrix RecreatedA = new GraphDivergenceMatrix(ChechOnFinisshed, A.Xl, N, M);

                if (Is)
                {
                    if (!GraphDivergenceMatrix.CheckedIsSameRikhtOverLap(B, RecreatedA))
                        Is = false;
                }
            }
            return Is;
        }
        
    }
    public class GraphDivergenceMatrix
    {
        public List<Vertex> Xv = new List<Vertex>();
        public List<Line> Xl = new List<Line>();
        public int N, M;

        public bool ExistV(int x1, int y1, int x2, int y2)
        {
            for (int i = 0; i < Xv.Count; i++)
            {
                for (int j = 0; j < Xv.Count; j++)
                {
                    if (Xv[i].X == x1 && Xv[i].Y == y1 && Xv[j].X == x2 && Xv[j].Y == y2)
                        return true;
                    if (Xv[j].X == x1 && Xv[j].Y == y1 && Xv[i].X == x2 && Xv[i].Y == y2)
                        return true;
                }
            }
            return false;
        }
        public bool ExistL(int x1, int y1, int x2, int y2)
        {
            
                for (int i = 0; i < Xl.Count; i++)
                {
                    if (Xl[i].VertexIndexX == x1 && Xl[i].VertexIndexX == y1 && Xl[i].VertexIndexY == x2 && Xl[i].VertexIndexY == y2)
                        return true;
                    if (Xl[i].VertexIndexY == x1 && Xl[i].VertexIndexY == y1 && Xl[i].VertexIndexX == x2 && Xl[i].VertexIndexX == y2)
                        return true;
                }
            return false;
        }
        public void XiXjDelete()
        {
            XiXjDeleteLess();
            XiXjDeleteGreat();
           
        }
        public void XiXjDeleteGreat()
        {
            try
            {
                List<Vertex> K = new List<Vertex>();
                for (int i = 0; i < Xv.Count; i++)
                {
                    for (int j = 0; j < Xv.Count; j++)
                    {
                        for (int k = 0; k < Xv.Count; k++)
                        {
                            if (i == j)
                                continue;
                            if (i == k)
                                continue;
                            if (j == k)
                                continue;
                            if ((!(Xv[k].X > Xv[i].X && Xv[k].X > Xv[i].X)) && (!(Xv[k].Y > Xv[i].Y && Xv[k].Y > Xv[i].Y)))
                                continue;
                            Line ds = d(Xv[i], Xv[j]);
                            if (ds != null)
                            {
                                bool Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv);
                                if (Is)
                                {
                                    Xl.Remove(ds);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                return;
            }
        }
        public void XiXjDeleteLess()
        {
            try
            {
                List<Vertex> K = new List<Vertex>();
                for (int i = 0; i < Xv.Count; i++)
                {
                    for (int j = 0; j < Xv.Count; j++)
                    {
                        for (int k = 0; k < Xv.Count; k++)
                        {
                            if (i == j)
                                continue;
                            if (j == k)
                                continue;
                            if (k == j)
                                continue;
                            if ((!(Xv[k].X < Xv[i].X && Xv[k].X < Xv[i].X)) && (!(Xv[k].Y < Xv[i].Y && Xv[k].Y < Xv[i].Y)))
                                continue;
                            Line ds = d(Xv[i], Xv[j]);
                            if (ds != null)
                            {
                                bool Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv);
                                if (Is)
                                {
                                    Xl.Remove(ds);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception t)
            {
                return;
            }
        }
        Line d(Vertex A,Vertex B)
        {
           for(int i = 0; i < Xl.Count; i++)
            {
                if (A.VertexNumber == Xl[i].VertexIndexX && B.VertexNumber == Xl[i].VertexIndexY)
                    return Xl[i];
                if (B.VertexNumber == Xl[i].VertexIndexX && A.VertexNumber == Xl[i].VertexIndexY)
                    return Xl[i];
                if (A.VertexNumber == Xl[i].VertexIndexY && B.VertexNumber == Xl[i].VertexIndexX)
                    return Xl[i];
                if (B.VertexNumber == Xl[i].VertexIndexY && A.VertexNumber == Xl[i].VertexIndexX)
                    return Xl[i];
            }
            return null;
        }
        bool IsXixJisDeletable(ref Line C, Vertex A, Vertex B, Vertex Next, ref List<Vertex> K, ref List<Vertex> xlv)
        {
            bool Is = false;
            for (int i = 0; i < xlv.Count - 1; i++)
            {
                
                if (Next.VertexNumber == B.VertexNumber)
                    return true;

                if (xlv[i].VertexNumber == Next.VertexNumber)
                {
                    K.Add(xlv[i]);
                    Is = Is || IsXixJisDeletable(ref C, A, B, xlv[i + 1], ref K, ref xlv);
                }


            }
            return Is;
        }
       public void IJBelongToLineHaveFalseBolleanA(bool[,] A)
        {
            for (int i = 0; i < Xv.Count; i++)
            {
                for (int k = 0; k < Xv.Count; k++)
                {
                    if (ExistL(Xv[i].X, Xv[i].Y, Xv[k].X, Xv[k].Y))
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
                                        if (!A[x, y])
                                        {
                                            Line ds = d(Xv[i], Xv[k]);
                                            if (ds != null)
                                            {
                                                if (Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                                    Xl.Remove(ds);
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
                                        if (!A[x, y])
                                        {
                                            Line ds = d(Xv[i], Xv[k]);
                                            if (ds != null)
                                            {
                                                if (Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                                    Xl.Remove(ds);
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
                                        if (!A[x, y])
                                        {
                                            Line ds = d(Xv[i], Xv[k]);
                                            if (ds != null)
                                            {
                                                if (Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                                    Xl.Remove(ds);
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
                                        if (!A[x, y])
                                        {
                                            Line ds = d(Xv[i], Xv[k]);
                                            if (ds != null)
                                            {
                                                if (Line.IsPointsInVertexes(Xv[i], Xv[k], x, y))
                                                    Xl.Remove(ds);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                        // if (Line.IsPointsInVertexes(Xv[i], Xv[k],))
                    }

                }
            }
        }
        public GraphDivergenceMatrix(bool[,] A, int n, int m)
        {
            N = n;
            M = m;
            int indv = 0;
            //To Do Create Graph mininimum graph
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        for (int p = 0; p < m; p++)
                        {
                            if (i == k)
                                continue;
                            if (j == p)
                                continue;
                            if (i == j)
                                continue;
                            if (k == p)
                                continue;
                            if (A[i, j] && A[k, p])
                            {
                                if (!ExistV(i, j, k, p))
                                {
                                    Xv.Add(new Vertex(++indv, i, j));
                                    Xv.Add(new Vertex(++indv, k, p));
                                }
                                if (!ExistL(i, j, k, p))
                                {
                                    Xl.Add(new Line((float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p)), Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                }
                            }
                        }
                    }
                }
            }
            XiXjDelete();

        }
        //reconstruction;
        public GraphDivergenceMatrix(List<Vertex> A, List<Line> Xl, int n, int m)
        {
            N = n;
            M = m;
            //To Do Create Graph mininimum graph
            for (int k = 0; k < Xl.Count; k++)
            {
                for (int i = 0; i < A.Count; i++)
                {
                    for (int j = 0; j < A.Count; j++)
                    {
                        if (i == Xl[k].VertexIndexX && j == Xl[k].VertexIndexY)
                        {
                            if (!ExistV(i, j, Xl[k].VertexIndexX, Xl[k].VertexIndexY))
                            {
                                Xv.Add(new Vertex(Xl[k].VertexIndexX, A[i].X, A[i].Y));
                                Xv.Add(new Vertex(Xl[k].VertexIndexY, A[j].X, A[j].Y));
                            }
                            if (!ExistL(i, j, Xl[k].VertexIndexX, Xl[k].VertexIndexY))
                            {
                                Xl.Add(new Line((float)Math.Sqrt((A[i].X - A[j].X) * (A[i].X - A[j].X) + (A[i].Y - A[j].Y) * (A[i].Y - A[j].Y)), Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                            }
                        }
                    }
                }
            }
            XiXjDelete();
        }
        public static bool CheckedIsSameRikhtOverLap(GraphDivergenceMatrix sma, GraphDivergenceMatrix Rec)
        {
            bool Is = false;
            List<Vertex> Sames = new List<Vertex>();
            for (int i = 0; i < sma.Xv.Count; i++)
            {
                for (int j = 0; j < Rec.Xv.Count; j++)
                {
                    if (sma.Xv[i].X == Rec.Xv[j].X)
                    {
                        if (sma.Xv[i].Y == Rec.Xv[j].Y)
                        {
                            Sames.Add(new Vertex(sma.Xv[i].VertexNumber, sma.Xv[i].X, sma.Xv[i].Y));
                        }
                    }
                }
            }
            if (Sames.Count == Rec.Xv.Count)
                Is = true;
            else
            {
                for (int i = 0; i < Sames.Count; i++)
                {
                    int crein = -1;
                    for (int j = 0; j < Rec.Xv.Count; j++)
                    {
                        if (Sames[i].VertexNumber != Rec.Xv[j].VertexNumber)
                        {
                            crein = j;
                        }
                        else
                        {
                            crein = -1;
                            break;
                        }
                    }
                    if (crein > -1)
                    {
                        for (int j = 0; j < Sames.Count; j++)
                        {
                            if (j == i)
                                continue;
                            if (Line.IsPointsInVertexes(Sames[i], Sames[j], Rec.Xv[crein].X, Rec.Xv[crein].Y))
                            {
                                Sames.Add(new Vertex(Rec.Xv[i].VertexNumber, Rec.Xv[i].X, Rec.Xv[i].Y));
                                break;
                            }
                        }
                    }
                }

            }
            if (Sames.Count == Rec.Xv.Count)
                Is = true;
            return Is;
        }

        public bool IsSameRikhtVertex(bool[,] Ab, GraphDivergenceMatrix BB, ref List<Vertex> K, ref List<Vertex> ChechOnFinisshed)
        {
            bool Is = false;

            for (int i = 0; i < Xv.Count; i++)
            {
                Is = Is || IsSameRikht(Ab, Xv[i].X, Xv[i].Y, BB, ref K, ref ChechOnFinisshed);
            }
            return Is;
        }
        bool IsSameRikht(bool[,] Ab, int x, int y, GraphDivergenceMatrix BB, ref List<Vertex> K, ref List<Vertex> ChechOnFinisshed)
        {
            bool Is = false;
            if (x < 0 || y < 0 || x >= M || y >= N)
                return false;

            if (K.Count >= BB.Xv.Count)
                return true;
            for (int i = 0; i < BB.Xv.Count; i++)
            {
                if (K.Count > 0)
                {
                    if (K.Contains(BB.Xv[i]))
                        continue;
                }
                if (x == BB.Xv[i].X && y == BB.Xv[i].Y)
                {
                    K.Add(BB.Xv[i]);
                    ChechOnFinisshed.Add(new Vertex(BB.Xv[i].VertexNumber, x, y));
                    return false;
                }
                for (int ii = 0; ii < K.Count; ii++)
                {
                    for (int j = 0; j < K.Count; j++)
                    {
                        if (i == j)
                            continue;
                        if (Line.IsPointsInVertexes(K[ii], K[j], x, y))
                        {
                            K.Add(BB.Xv[i]);
                            ChechOnFinisshed.Add(new Vertex(BB.Xv[i].VertexNumber, x, y));
                            return false;
                        }

                    }
                }
            }

            if (x + 1 >= 0 && y + 1  >= 0 && x + 1 < M && y + 1 < N)
            {
                if (!Ab[x + 1, y + 1])
                    Is = Is || IsSameRikht(Ab, x + 1, y + 1, BB, ref K, ref ChechOnFinisshed);
            }
            if (x - 1  >= 0 && y - 1  >= 0 && x - 1 < M && y - 1 < N)
            {
                if (!Ab[x - 1, y - 1])
                    Is = Is || IsSameRikht(Ab, x - 1, y - 1, BB, ref K, ref ChechOnFinisshed);
            }
            if (x + 1  >= 0 && y - 1  >= 0 && x + 1 < M && y - 1 < N)
            {
                if (!Ab[x + 1, y - 1])
                    Is = Is || IsSameRikht(Ab, x + 1, y - 1, BB, ref K, ref ChechOnFinisshed);
            }
            if (x - 1  >= 0 && y + 1  >= 0 && x - 1 < M && y + 1 < N)
            {
                if (!Ab[x - 1, y + 1])
                    Is = Is || IsSameRikht(Ab, x - 1, y + 1, BB, ref K, ref ChechOnFinisshed);
            }
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
        float Weigth;
        public Line(float Weit, int inx, int iny)
        {
            VertexIndexX = inx;
            VertexIndexY = iny;
            Weigth = Weit;
        }
        public static bool IsPointsInVertexes(Vertex v1,Vertex v2,int x,int y)
        {
            bool Is = false;
            if (((y - v1.Y) - (((v1.X - v2.X) / (v1.Y - v2.Y)) * (x - v1.X))) < 1)
                Is = true;
            return Is;
        }
    }
}
