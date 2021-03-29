using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourAnalysisNS
{
    public class GraphS
    {
        GrafeDivergenceMatrix A, B;
        public GraphS(bool[,] Ab, bool[,] Bb, int n, int m)
        {
            A = new GrafeDivergenceMatrix(Ab, n, m);
            B = new GrafeDivergenceMatrix(Bb, n, m);
        }
    }
    class GrafeDivergenceMatrix
    {
        List<Vertex> Xv = new List<Vertex>();
        List<Line> Xl = new List<Line>();
        int N, M;
        public GrafeDivergenceMatrix(bool[,] A, int n, int m)
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
    }
    class Vertex
    {
        public int VertexNumber;
        int X, Y;
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
    }
}
