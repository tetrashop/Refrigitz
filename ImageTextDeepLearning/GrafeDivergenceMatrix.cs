using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourAnalysisNS
{
    public class GraphS
    {
        GraphDivergenceMatrix A, B;
        public GraphS(bool[,] Ab, bool[,] Bb, int n, int m)
        {
            A = new GraphDivergenceMatrix(Ab, n, m);
            B = new GraphDivergenceMatrix(Bb, n, m);
        }
        //When the matrix iss  the same  return true;
        public bool SameRikhtThisIsLessVertex(int x, int y, ref List<Vertex> K)
        {
            bool Is = false;
            if (A.Xv.Count < B.Xv.Count)
                Is = A.IsSameRikht(0, 0, B, ref K);
            else
                Is = B.IsSameRikht(0, 0, A, ref K);
            return Is;
        }
        
    }
    class GraphDivergenceMatrix
    {
        public List<Vertex> Xv = new List<Vertex>();
        List<Line> Xl = new List<Line>();
        public int N, M;
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
                                Xv.Add(new Vertex(++indv, i, j));
                                Xv.Add(new Vertex(++indv, k, p));
                                Xl.Add(new Line((float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p)), Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                            }
                        }
                    }
                }
            }

        }
        public bool IsSameRikht(int x, int y, GraphDivergenceMatrix B, ref List<Vertex> K)
        {
            bool Is = false;
            if (x < 0 || y < 0 || x >= M || y >= N)
                return false;

            if (K.Count >= this.Xv.Count)
                return true;
            for (int i = 0; i < B.Xv.Count; i++)
            {
                if (K.Contains(B.Xv[i]))
                    continue;
                if (x == B.Xv[i].X && y == B.Xv[i].Y)
                {
                    K.Add(B.Xv[i]);
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
                            K.Add(B.Xv[i]);
                            return false;
                        }

                    }
                }
            }

            for (int i = 0; i < Xv.Count; i++)
            {
                Is = Is || IsSameRikht(x++, y++, B, ref K);
                Is = Is || IsSameRikht(x--, y--, B, ref K);
                Is = Is || IsSameRikht(x++, y--, B, ref K);
                Is = Is || IsSameRikht(x--, y++, B, ref K);
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
    class Line
    {
        int VertexIndexX, VertexIndexY;
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
