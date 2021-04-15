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
using System.Threading.Tasks;
using System.Windows.Forms;
using howto_WPF_3D_triangle_normalsuser;
namespace ContourAnalysisNS
{
    public class GraphS
    {
        List<float> SumWeighEveryLines = new List<float>();
        public static float SumWeighEveryLinesDiffferention = 1;
        public static GraphS Z = null;
        public static bool Drawn = false;
        public static SameRikhEquvalent ZB = null;
        public SameRikhEquvalent A, B;
        private readonly int N, M;
        public GraphS(bool[,] Ab, bool[,] Bb, int n, int m, bool Achange)
        {
            try
            {
                Drawn = false;
                N = n;
                M = m;
                A = new SameRikhEquvalent(Ab, n, m);
                if (A != null)
                {

                    A.CreateClosedCurved();
                    A.CreateAngleAndLines();
                    Drawn = true;
                }
                B = new SameRikhEquvalent(Bb, n, m);
                if (B != null)
                {

                    B.CreateClosedCurved();
                    B.CreateAngleAndLines();
                }
                Drawn = true;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                return;
            }
        }
        public GraphS()
        {
        }
        bool TowSubGraphEqualiity(List<SameRikhEquvalent> A, List<SameRikhEquvalent> B)
        {
            bool Is = false;
            if (A.Count != B.Count)
                return Is;
            for (int i = 0; i < A.Count; i++)
                Is = Is && (A[i].Angle == B[i].Angle);
            return Is;
        }
        float Mean(List<float> z)
        {
            float mean = 0;
            for (int i = 0; i < z.Count; i++)
                mean += z[i];
            mean /= (float)z.Count;
            return mean;
        }
        float Divesion(List<float> t)
        {
            float div = 0;
            float mean = Mean(t);
            for (int i = 0; i < t.Count; i++)
                div += (float)Math.Pow(t[i] - mean, 2);
            div /= ((float)t.Count * ((float)t.Count - 1));
            return div;
        }
        public bool SameSumWeigth()
        {
            if (A.SumWeighEveryLines.Count != B.SumWeighEveryLines.Count)
                return false;

            for (int i = 0; i < A.SumWeighEveryLines.Count; i++)
            {
                SumWeighEveryLines.Add(A.SumWeighEveryLines[i] / B.SumWeighEveryLines[i]);
            }
            if (SumWeighEveryLines.Count > 1)
            {
                float ad = Divesion(SumWeighEveryLines);
                if (ad < 1)
                    return true;
            }
            else
                return true;
            return false;
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
        public bool GraphSameRikht(bool[,] Ab, bool[,] Bb, int n, int m, bool Achange)
        {
            /*if (Z != null)
            {
                if (Z.A == null && Z.B == null)
                {
                    return true;
                }
                if (Z.A == null || Z.B == null)
                {
                    return false;
                }
                Z.A.Reco(Ab, n, m, Achange);
                Z.B.Reco(Bb, n, m, false);
            }
            else*/
            Z = new GraphS(Ab, Bb, n, m, Achange);
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
                if (Z.A.numberOfClosedCurved > 1)
                {
                    bool Is = true;
                    if (Z.TowSubGraphEqualiity(Z.A.GreaterThanOneCuurvved, Z.B.GreaterThanOneCuurvved))
                    {
                        for (int i = 0; i < Z.A.GreaterThanOneCuurvved.Count; i++)
                            Is = Is && (new GraphS()).GraphSameRikht(Z.A.GreaterThanOneCuurvvedMatrix[i], Z.B.GreaterThanOneCuurvvedMatrix[i], Z.N, Z.M, false);
                        return Is;
                    }
                }
                else
                {
                    if (Z.A.Angle == Z.B.Angle)
                    {
                        if (Z.SameSumWeigth())
                        {
                            return true;
                        }
                        else
                            return Z.SameRikhtThisIsLessVertex(Ab, Bb);
                    }
                }
            }

            return false;
        }

        public bool SameRikhtThisIsLessVertex(bool[,] Ab, bool[,] Bb)
        {
            bool Is = false;
            List<Vertex> K = new List<Vertex>();
            List<Vertex> ChechOnFinisshed = new List<Vertex>();
            if (A.Xv.Count < B.Xv.Count)
            {
                Is = A.IsSameRikhtVertex(Ab, B, ref K, ref ChechOnFinisshed);

            }
            else
            {
                Is = B.IsSameRikhtVertex(Bb, A, ref K, ref ChechOnFinisshed);

            }
            return Is;
        }
    }
    public class GraphDivergenceMatrix
    {
        public List<Vertex> Xv = new List<Vertex>();
        public List<Line> Xl = new List<Line>();
        public int N, M;
        public List<Line> XlOverLap = new List<Line>();

        public bool ExistV(int x1, int y1)
        {
            bool Is = false;

            for (int i = 0; i < Xv.Count; i++)
            {


                {
                    object hh = new object();
                    lock (hh)
                    {
                        if (Xv[i].X == x1 && Xv[i].Y == y1)
                        {
                            Is = true;
                        }

                    }
                }
            }
            return Is;
        }
        public bool ExistK(int x1, int y1, List<Vertex> K)
        {
            bool Is = false;

            for (int i = 0; i < K.Count; i++)
            {

                for (int j = 0; j < K.Count; j++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                        if (K[i].X == x1 && K[i].Y == y1)
                        {
                            Is = true;
                        }

                    }
                }
            }
            return Is;
        }
        public bool ExistL(int x1, int y1)
        {
            bool Is = false;

            if (ExistLOverLap(x1, y1))
                return true;

            for (int i = 0; i < Xl.Count; i++)
            {
                object hh = new object();
                lock (hh)
                {
                    if (Xl[i].VertexIndexX == x1 && Xl[i].VertexIndexY == y1)
                    {
                        Is = true;
                    }
                    if (Xl[i].VertexIndexY == x1 && Xl[i].VertexIndexX == y1)
                    {
                        Is = true;
                    }
                }
            }
            return Is;
        }
        public bool ExistLOverLap(int x1, int y1)
        {
            bool Is = false;

            for (int i = 0; i < XlOverLap.Count; i++)
            {
                object hh = new object();
                lock (hh)
                {
                    if (XlOverLap[i].VertexIndexX == x1 && XlOverLap[i].VertexIndexY == y1)
                    {
                        Is = true;
                    }
                    if (XlOverLap[i].VertexIndexY == x1 && XlOverLap[i].VertexIndexX == y1)
                    {
                        Is = true;
                    }
                }
            }
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
            }
            catch (Exception t)
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

                for (int i = 0; i < Xv.Count; i++)
                {

                    for (int j = 0; j < Xv.Count; j++)
                    {

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
                                if ((!(Xv[k].X > Xv[i].X && Xv[k].X > Xv[j].X)) || k >= Xv.Count || i >= Xv.Count)
                                {
                                    continue;
                                }
                                Line ds = d(Xv[i], Xv[j]);
                                if (ds != null)
                                {
                                    if (Xv[i].VertexNumber == Xv[k].VertexNumber)
                                    {
                                        Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv);
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
                        }
                    }
                }
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

                for (int i = 0; i < Xv.Count; i++)
                {

                    for (int j = 0; j < Xv.Count; j++)
                    {

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
                                        Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv);
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
                        }
                    }
                }
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

                for (int i = 0; i < Xv.Count; i++)
                {

                    for (int j = 0; j < Xv.Count; j++)
                    {

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
                                        Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv);
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
                        }
                    }
                }
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

                for (int i = 0; i < Xv.Count; i++)
                {

                    for (int j = 0; j < Xv.Count; j++)
                    {

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
                                        Is = IsXixJisDeletable(ref ds, Xv[i], Xv[j], Xv[k], ref K, ref Xv);
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
                        }
                    }
                }
            }
            catch (Exception)
            {
                return;
            }
        }
        public Line d(Vertex A, Vertex B)
        {
            Line dd = null;

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
            }
            return dd;
        }
        private bool IsXixJisDeletable(ref Line C, Vertex A, Vertex B, Vertex Next, ref List<Vertex> K, ref List<Vertex> xlv)
        {
            bool Is = false;
            if (A.VertexNumber == B.VertexNumber)
                return Is;
            if (K.Count >= xlv.Count)
                return Is;
            for (int i = 0; i < xlv.Count - 1; i++)
            {
                if (Is)
                    return Is;
                if (i < xlv.Count)
                {
                    if (Next.VertexNumber == B.VertexNumber)
                    {
                        return true;
                    }
                    if (Next.VertexNumber == xlv[i + 1].VertexNumber)
                        continue;
                    Line ds = d(Next, xlv[i + 1]);
                    if (ds == null)
                        continue;
                    if (ExistK(xlv[i + 1].X, xlv[i + 1].Y, K))
                        continue;
                    if (xlv[i].VertexNumber == Next.VertexNumber)
                    {
                        K.Add(xlv[i]);
                        Is = Is || IsXixJisDeletable(ref C, A, B, xlv[i + 1], ref K, ref xlv);
                    }
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
                }
            }
        }
        public GraphDivergenceMatrix Reco(bool[,] A, int n, int m, bool Achange)
        {
            if (!Achange)
            {
                SameRikhEquvalent AA = new SameRikhEquvalent(A, n, m);
                if (AA != null)
                {
                    AA.CreateClosedCurved();
                    AA.CreateAngleAndLines();
                    return AA;
                }
            }
            return this;
        }
        bool IsLineMinimumNotInXl(bool[,] A, float weB, int n, int m)
        {
            bool Is = true;
            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {
                    for (int k = 0; k < n; k++)
                    {

                        for (int p = 0; p < m; p++)
                        {
                            if (i == k)
                            {
                                continue;
                            }


                            if (j == p)
                            {
                                continue;
                            }
                            if (A[i, j] && A[k, p])
                            {
                                float wei = float.MaxValue;
                                object h = new object();
                                lock (h)
                                {
                                    if (ExistV(i, j) && ExistV(k, p))
                                        continue;
                                    float we = (float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p));
                                    if (we < weB)
                                        return false;
                                }
                            }
                        }
                    }
                }
            }
            return Is;
        }

        public GraphDivergenceMatrix(bool[,] A, int n, int m)
        {
            bool FirstCchanged = false;
            int First = 0;
            N = n;
            M = m;
            int indv = 0;
            bool Occurred = false;
            bool OccurredBreak = false;
            int I = -1, J = -1;
            bool KPNotFound;
            bool LessNootFound = false;
            bool IgnoreLess = false;
            bool OverAgain = false;
            do
            {
                do
                {
                    KPNotFound = true;
                    LessNootFound = true;
                    Occurred = false;

                    for (int i = 0; i < n; i++)
                    {

                        for (int j = 0; j < m; j++)
                        {
                            if (KPNotFound && LessNootFound)
                            {
                                IgnoreLess = true;
                                KPNotFound = false;
                                LessNootFound = false;
                                if (I != -1 && J != -1)
                                {
                                    i = I;
                                    j = J;
                                    I = -1;
                                    J = -1;
                                    OccurredBreak = false;
                                }
                            }
                            else
                            {
                                IgnoreLess = false;
                                KPNotFound = false;
                                LessNootFound = false;
                                if (I != -1 && J != -1)
                                {
                                    i = I;
                                    j = J;
                                    OccurredBreak = false;
                                }
                            }
                            for (int k = 0; k < n; k++)
                            {

                                if (OccurredBreak)
                                    break;
                                for (int p = 0; p < m; p++)
                                {
                                    if (OccurredBreak)
                                        break;
                                    float wei = float.MaxValue;
                                    object h = new object();
                                    lock (h)
                                    {

                                        if (i == k)
                                        {
                                            continue;
                                        }


                                        if (j == p)
                                        {
                                            continue;
                                        }
                                        if (A[i, j])
                                        {
                                            if (!A[k, p])
                                                continue;
                                            KPNotFound = true;
                                        }
                                        if (A[i, j] && A[k, p])
                                        {
                                            float weB = (float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p));
                                            if (Xv.Count > First)
                                            {
                                                if (!IgnoreLess)
                                                {
                                                    if (!(Xv[First].X == i && Xv[First].Y == j))
                                                    {
                                                        if (!IsLineMinimumNotInXl(A, weB, n, m))
                                                        {
                                                            continue;
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (!IgnoreLess)
                                                {
                                                    if (!IsLineMinimumNotInXl(A, weB, n, m))
                                                        continue;
                                                }
                                            }
                                            LessNootFound = true;

                                            if (Xv.Count > 0)
                                            {
                                                if ((Xv[First].X == i && Xv[First].Y == j) && (Xv.Count > First))
                                                {
                                                    if (!FirstCchanged)
                                                    {
                                                        if (!ExistL(Xv[Xv.Count - 1].VertexNumber, Xv[First].VertexNumber))
                                                        {

                                                            float we = (float)Math.Sqrt((Xv[First].X - Xv[Xv.Count - 1].X) * (Xv[First].X - Xv[Xv.Count - 1].X) + (Xv[First].Y - Xv[Xv.Count - 1].Y) * (Xv[First].Y - Xv[Xv.Count - 1].Y));
                                                            Xl.Add(new Line(we, Xv[Xv.Count - 1].VertexNumber, Xv[First].VertexNumber));
                                                            First = Xv.Count - 1;
                                                            FirstCchanged = true;
                                                            I = -1;
                                                            J = -1;
                                                            Occurred = true;
                                                            OccurredBreak = true;
                                                            continue;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (FirstCchanged)
                                                    {
                                                        int inh = indv;
                                                        int inh1 = indv;
                                                        int inh2 = indv;
                                                        bool Is1 = false;
                                                        bool Is2 = false;
                                                        if (!ExistV(i, j))
                                                        {
                                                            FirstCchanged = false;
                                                            Xv.Add(new Vertex(++indv, i, j));
                                                            inh1 = indv;
                                                        }
                                                        else
                                                            Is1 = true;
                                                        if (!ExistV(k, p))
                                                        {
                                                            FirstCchanged = false;
                                                            Xv.Add(new Vertex(++indv, k, p));
                                                            inh2 = indv;
                                                        }
                                                        else
                                                            Is2 = true;
                                                        if (Is1 && Is2)
                                                            continue;
                                                        if (inh1 != inh2)
                                                        {
                                                            if (inh == indv - 1)
                                                            {
                                                                if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                                {
                                                                    float we = (float)Math.Sqrt((Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) * (Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) + (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y) * (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y));
                                                                    Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                                    I = k;
                                                                    J = p;
                                                                    Occurred = true;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!ExistL(inh1, inh2))
                                                                {
                                                                    float we = (float)Math.Sqrt((Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) * (Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) + (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y) * (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y));
                                                                    Xl.Add(new Line(we, inh1, inh2));
                                                                    I = k;
                                                                    J = p;
                                                                    Occurred = true;
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {

                                                        int inh = indv;
                                                        int inh1 = indv;
                                                        int inh2 = indv;
                                                        bool Is1 = false;
                                                        bool Is2 = false;
                                                        if (!ExistV(i, j))
                                                        {
                                                            Xv.Add(new Vertex(++indv, i, j));
                                                            inh1 = indv;
                                                        }
                                                        else
                                                            Is1 = true;
                                                        if (!ExistV(k, p))
                                                        {
                                                            Xv.Add(new Vertex(++indv, k, p));
                                                            inh2 = indv;
                                                        }
                                                        else
                                                            Is2 = true;
                                                        if (Is1 && Is2)
                                                            continue;
                                                        if (inh1 != inh2)
                                                        {
                                                            if (inh == indv - 1)
                                                            {
                                                                if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                                {
                                                                    float we = (float)Math.Sqrt((Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) * (Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) + (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y) * (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y));
                                                                    Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                                    I = k;
                                                                    J = p;
                                                                    Occurred = true;
                                                                }
                                                            }
                                                            else
                                                            {
                                                                if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                                {
                                                                    float we = (float)Math.Sqrt((Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) * (Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) + (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y) * (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y));
                                                                    Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                                    I = k;
                                                                    J = p;
                                                                    Occurred = true;
                                                                }
                                                            }
                                                        }

                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (FirstCchanged)
                                                {
                                                    int inh = indv;
                                                    int inh1 = indv;
                                                    int inh2 = indv;
                                                    bool Is1 = false;
                                                    bool Is2 = false;
                                                    if (!ExistV(i, j))
                                                    {
                                                        FirstCchanged = false;
                                                        Xv.Add(new Vertex(++indv, i, j));
                                                        inh1 = indv;
                                                    }
                                                    else
                                                        Is1 = true;
                                                    if (!ExistV(k, p))
                                                    {
                                                        FirstCchanged = false;
                                                        Xv.Add(new Vertex(++indv, k, p));
                                                        inh2 = indv;
                                                    }
                                                    else
                                                        Is2 = true;
                                                    if (Is1 && Is2)
                                                        continue;
                                                    if (inh1 != inh2)
                                                    {
                                                        if (inh == indv - 1)
                                                        {
                                                            if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                            {
                                                                float we = (float)Math.Sqrt((Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) * (Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) + (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y) * (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y));
                                                                Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                                I = k;
                                                                J = p;
                                                                Occurred = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (!ExistL(inh1, inh2))
                                                            {
                                                                float we = (float)Math.Sqrt((Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) * (Xv[Xv.Count - 2].X - Xv[Xv.Count - 1].X) + (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y) * (Xv[Xv.Count - 2].Y - Xv[Xv.Count - 1].Y));
                                                                Xl.Add(new Line(we, inh1, inh2));
                                                                I = k;
                                                                J = p;
                                                                Occurred = true;
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    int inh = indv;
                                                    int inh1 = indv;
                                                    int inh2 = indv;
                                                    bool Is1 = false;
                                                    bool Is2 = false;
                                                    if (!ExistV(i, j))
                                                    {
                                                        Xv.Add(new Vertex(++indv, i, j));
                                                        inh1 = indv;
                                                    }
                                                    else
                                                        Is1 = true;
                                                    if (!ExistV(k, p))
                                                    {
                                                        Xv.Add(new Vertex(++indv, k, p));
                                                        inh2 = indv;
                                                    }
                                                    else
                                                        Is2 = true;
                                                    if (Is1 && Is2)
                                                        continue;
                                                    if (inh1 != inh2)
                                                    {
                                                        if (inh == indv - 1)
                                                        {
                                                            float we = (float)Math.Sqrt((Xv[Xv.Count - 2].X - k) * (Xv[Xv.Count - 2].X - k) + (Xv[Xv.Count - 2].Y - p) * (Xv[Xv.Count - 2].Y - p));
                                                            Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                            I = k;
                                                            J = p;
                                                            Occurred = true;
                                                        }
                                                        else
                                                        {
                                                            if (!ExistL(Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber))
                                                            {
                                                                float we = (float)Math.Sqrt((i - k) * (i - k) + (j - p) * (j - p));
                                                                Xl.Add(new Line(we, Xv[Xv.Count - 2].VertexNumber, Xv[Xv.Count - 1].VertexNumber));
                                                                I = k;
                                                                J = p;
                                                                Occurred = true;
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
                    }
                } while (Occurred);

                XiXjDelete();
                IJBelongToLineHaveFalseBolleanA(A);

                OverAgain = false;
                for (int i = 0; i < Xl.Count; i++)
                {
                    for (int j = 0; j < Xl.Count; j++)
                    {
                        if (i == j)
                            continue;
                        if (Line.IsTowLineIsIntersect(GetVerIdO(Xl[i].VertexIndexX), GetVerIdO(Xl[i].VertexIndexY), GetVerIdO(Xl[j].VertexIndexX), GetVerIdO(Xl[j].VertexIndexY), n, m))
                        {
                            if (!ExistLOverLap(Xl[i].VertexIndexX, Xl[i].VertexIndexY))
                            {
                                XlOverLap.Add(Xl[i]);
                                OverAgain = true;
                            }
                            if (!ExistLOverLap(Xl[j].VertexIndexX, Xl[j].VertexIndexY))
                            {
                                OverAgain = true;
                                XlOverLap.Add(Xl[j]);
                            }
                        }
                    }
                }
                if (OverAgain)
                {
                    Xv.Clear();
                    Xl.Clear();
                    FirstCchanged = false;
                    First = 0;
                    N = n;
                    M = m;
                    indv = 0;
                    Occurred = false;
                    OccurredBreak = false;
                    I = -1;
                    J = -1;
                    LessNootFound = false;
                    IgnoreLess = false;
                 }
            } while (OverAgain);
        }
        Vertex GetVerIdO(int VertNo)
        {
            int vern = 0;
            for (int i = 0; i < Xv.Count; i++)
            {
                if (Xv[i].VertexNumber == VertNo)
                    return Xv[i];
            }
            return null;
        }
        int GetVerInd(int VertNo)
        {
            int vern = 0;
            for (int i = 0; i < Xv.Count; i++)
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
                        object h = new object();
                        lock (h)
                        {

                            if (i == j)
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
                }
            }
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

                for (int i = 0; i < Sames.Count; i++)
                {
                    int crein = -1;

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
                    }
                    if (crein > -1)
                    {

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
                        }
                    }
                }
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
            }
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

                        for (int ii = 0; ii < K.Count; ii++)
                        {
                            if (exit)
                            {
                                return Is;
                            }
                            try
                            {

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
                                }
                            }
                            catch (Exception) { }
                        }
                    }
                    catch (Exception) { }
                }
            }
            catch (Exception) { }
            if (Ab[x, y])
            {
                Kk = K;
                ChechOnFinisshedI = ChechOnFinisshed;
                return false;
            }
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
        public float Weigth;
        public Line(float Weit, int inx, int iny)
        {
            VertexIndexX = inx;
            VertexIndexY = iny;
            Weigth = Weit;
        }
        public static bool IsTowLineIsIntersect(Vertex v0, Vertex v1, Vertex v2, Vertex v3, int n, int m)
        {
            bool Is = false;
            howto_WPF_3D_triangle_normalsuser.Line l0 = new howto_WPF_3D_triangle_normalsuser.Line(new Point3Dspaceuser.Point3D(v0.X, v0.Y, 0), new Point3Dspaceuser.Point3D(v1.X, v1.Y, 0));
            int X1 = v0.X, X2 = v1.X;
            if (X1 > X2)
            {
                int v = X1;
                X1 = X2;
                X2 = v;
            }
            for (int i = X1 + 1; i < X2 - 1; i++)
            {
                if (Line.IsPointsInVertexes(v2, v3, i, (int)((l0.y0 - l0.a * i) / l0.b)))
                    return true;                
            }
            return Is;
        }
        public static bool IsPointsInVertexes(Vertex v1, Vertex v2, int x, int y)
        {
            bool Is = false;
            if (v1.Y == v2.Y)
            {
                return false;
            }
            if ((float)((float)(y - v1.Y) - (((float)(v1.X - v2.X) / (float)(v1.Y - v2.Y)) * (float)(x - v1.X))) < 1)
            {
                Is = true;
            }
            return Is;
        }
    }
    public class SameRikhEquvalent : GraphDivergenceMatrix
    {
        double Maxan = Math.PI / 90;
        public List<bool[,]> GreaterThanOneCuurvvedMatrix = new List<bool[,]>();
        public List<SameRikhEquvalent> GreaterThanOneCuurvved = new List<SameRikhEquvalent>();
        public float diverstionSumWeighEveryLines = 0;
        public List<float> SumWeighEveryLines = new List<float>();
        List<howto_WPF_3D_triangle_normalsuser.Line> ldLine = new List<howto_WPF_3D_triangle_normalsuser.Line>();
        List<List<float>> sd = new List<List<float>>();
        List<List<Point3Dspaceuser.Point3D[]>> ad = new List<List<Point3Dspaceuser.Point3D[]>>();
        List<List<Line>> ld = new List<List<Line>>();
        public int Angle = 0, Liens = 0;
        public List<howto_WPF_3D_triangle_normalsuser.Line> XlLineshow = new List<howto_WPF_3D_triangle_normalsuser.Line>();
        public int numberOfClosedCurved = 0;
        List<List<Vertex>> ClosedCurved = new List<List<Vertex>>();
        List<bool> IsClosedCurved = new List<bool>();
        List<int> ClosedCurvedIndexI = new List<int>();
        public SameRikhEquvalent(bool[,] A, int n, int m) : base(A, n, m)
        {
        }
        public SameRikhEquvalent(List<Vertex> A, List<Line> Xl, int n, int m) : base(A, Xl, n, m)
        {
        }
        public bool ExistAd(int x1, int y1)
        {
            bool Is = false;

            for (int i = 0; i < ad.Count; i++)
            {

                for (int j = 0; j < ad[i].Count; j++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                        if ((int)(ad[i][j][0].X) == x1 && ((int)ad[i][j][0].Y) == y1 && (int)(ad[i][j][1].X) == x1 && ((int)ad[i][j][1].Y) == y1)
                        {
                            Is = true;
                        }
                    }
                }
            }
            return Is;
        }
        public bool ExistLd(int x1, int y1)
        {
            bool Is = false;

            for (int i = 0; i < ld.Count; i++)
            {
                for (int j = 0; j < ld[i].Count; j++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                        if (ld[i][j].VertexIndexX == x1 && ld[i][j].VertexIndexY == y1)
                        {
                            Is = true;
                        }
                        if (ld[i][j].VertexIndexY == x1 && ld[i][j].VertexIndexX == y1)
                        {
                            Is = true;
                        }
                    }
                }
            }
            return Is;
        }
        bool[,] CreateBooleanSubGraph(int i)
        {
            bool[,] c = new bool[N, M];
            if (numberOfClosedCurved > 0)
            {
                for (int j = 0; j < ClosedCurved[i].Count; j++)
                {
                    c[ClosedCurved[i][j].X, ClosedCurved[i][j].Y] = true;
                }
            }
            return c;
        }
        void CreateBolleanMatrixOfSubGraph()
        {
            if (numberOfClosedCurved > 0)
            {
                for (int i = 0; i < ClosedCurved.Count; i++)
                {
                    GreaterThanOneCuurvvedMatrix.Add(CreateBooleanSubGraph(i));
                }
            }
        }
        public void CreateSubSameEquValentGraph()
        {
            if (GreaterThanOneCuurvvedMatrix.Count > 0)
            {
                for (int i = 0; i < GreaterThanOneCuurvvedMatrix.Count; i++)
                    GreaterThanOneCuurvved.Add(new SameRikhEquvalent(GreaterThanOneCuurvvedMatrix[i], N, M));
                GreaterThanOneCuurvved = ImageTextDeepLearning.ImprovmentSort.Do(GreaterThanOneCuurvved, ref GreaterThanOneCuurvvedMatrix);
            }
        }
        float Mean(List<float> z)
        {
            float mean = 0;
            for (int i = 0; i < z.Count; i++)
                mean += z[i];
            mean /= (float)z.Count;
            return mean;
        }
        float Divesion(List<float> t)
        {
            float div = 0;
            float mean = Mean(t);
            for (int i = 0; i < t.Count; i++)
                div += (float)Math.Pow(t[i] - mean, 2);
            div /= ((float)t.Count * ((float)t.Count - 1));
            return div;
        }
        bool AddIng(Point3Dspaceuser.Point3D p0, Point3Dspaceuser.Point3D p1, ref int ii, ref int jj)
        {
            Point3Dspaceuser.Point3D[] c = new Point3Dspaceuser.Point3D[2];
            c[0] = p0;
            c[1] = p1;
            if (ad.Count == 0)
            {
                Maxan = Math.PI / 90;
                ad.Add(new List<Point3Dspaceuser.Point3D[]>());
                ad[ad.Count - 1].Add(c);
                ii = ad.Count - 1;
                jj = 0;
                return true;
            }
            if (ii != -1 && jj != -1)
                ad[ii].Add(c);

            else
            {
                for (int i = 0; i < ad.Count; i++)
                {

                    for (int j = 0; j < ad[i].Count; j++)
                    {
                        double an = 0;
                        howto_WPF_3D_triangle_normalsuser.Line.AngleBetweenTowLineS(ad[i][j][0], ad[i][j][1], p0, p1, ref an);
                        if (an < Maxan)
                        {

                            ad[i].Add(c);
                            ii = i;
                            jj = j;
                            return true;
                        }
                    }


                }
                Maxan = Math.PI / 90;
                ad[ad.Count - 1].Add(c);
                ii = ad.Count - 1;
                jj = 0;
            }
            return true;
        }
        bool AddIngL(Line l0, int i, int j, float we)
        {
            if (ld.Count == i)
            {
                ld.Add(new List<Line>());
                sd.Add(new List<float>());
                ld[ld.Count - 1].Add(l0);
                sd[ld.Count - 1].Add(we);
                return true;
            }
            if (i < ld.Count)
            {
                ld[i].Add(l0);
                sd[i].Add(we);
                return true;
            }
            return false;
        }
        int GetVerInd(int VertNo)
        {
            int vern = 0;
            for (int i = 0; i < Xv.Count; i++)
            {
                if (Xv[i].VertexNumber == VertNo)
                    return i;
            }
            return vern;
        }
        int GetXlIndY(int XltNo)
        {
            int vern = 0;
            for (int i = 0; i < Xl.Count; i++)
            {
                if (Xl[i].VertexIndexY == XltNo)
                    return i;
            }
            return vern;
        }
        int GetXlIndX(int XltNo)
        {
            int vern = 0;
            for (int i = 0; i < Xl.Count; i++)
            {
                if (Xl[i].VertexIndexX == XltNo)
                    return i;
            }
            return vern;
        }
        Vertex GetVerId(int VertNo)
        {
            int vern = 0;
            for (int i = 0; i < Xv.Count; i++)
            {
                if (Xv[i].VertexNumber == VertNo)
                    return Xv[i];
            }
            return null;
        }
        public void CreateAngleAndLines()
        {
            do
            {
                for (int i = 0; i < Xv.Count; i++)
                {
                    Point3Dspaceuser.Point3D p0 = new Point3Dspaceuser.Point3D(Xv[i].X, Xv[i].Y, 0);
                    for (int j = 0; j < Xv.Count; j++)
                    {
                        Point3Dspaceuser.Point3D p1 = new Point3Dspaceuser.Point3D(Xv[j].X, Xv[j].Y, 0);
                        bool Is = false;
                        for (int k = 0; k < Xv.Count; k++)
                        {
                            Point3Dspaceuser.Point3D p2 = new Point3Dspaceuser.Point3D(Xv[k].X, Xv[k].Y, 0);
                            for (int p = 0; p < Xv.Count; p++)
                            {
                                Point3Dspaceuser.Point3D p3 = new Point3Dspaceuser.Point3D(Xv[p].X, Xv[p].Y, 0);
                                if (i == j)
                                    continue;
                                //if (i == k)
                                //;
                                // if (i == p)
                                //  continue;
                                //if (j == k)
                                // continue;
                                //if (j == p)
                                //continue;
                                if (k == p)
                                    continue;
                                double an = 0;
                                howto_WPF_3D_triangle_normalsuser.Line.AngleBetweenTowLineS(p0, p1, p2, p3, ref an);
                                if (an > Maxan)
                                    continue;

                                bool AB = Point3Dspaceuser.Point3D.Exist(ad, p0), BB = Point3Dspaceuser.Point3D.Exist(ad, p1);
                                if ((!AB) || (!BB))
                                {
                                    AB = Point3Dspaceuser.Point3D.Exist(ad, p2); BB = Point3Dspaceuser.Point3D.Exist(ad, p3);
                                    if ((!AB) || (!BB))
                                    {
                                        int ii = -1, jj = -1;
                                        float we = (float)Math.Sqrt((Xv[i].X - Xv[j].X) * (Xv[i].X - Xv[j].X) + (Xv[i].Y - Xv[j].Y) * (Xv[i].Y - Xv[j].Y));
                                        Line ll0 = new Line(we, Xv[i].VertexNumber, Xv[j].VertexNumber);
                                        if (ExistL(ll0.VertexIndexX, ll0.VertexIndexY))
                                        {
                                            Is = AddIng(p0, p1, ref ii, ref jj);
                                            if (Is)
                                            {

                                                if (!ExistLd(ll0.VertexIndexX, ll0.VertexIndexY))
                                                    AddIngL(ll0, ii, jj, we);
                                            }
                                        }
                                        //ii = -1;
                                        //jj = -1;
                                        float wep = (float)Math.Sqrt((Xv[k].X - Xv[p].X) * (Xv[k].X - Xv[p].X) + (Xv[k].Y - Xv[p].Y) * (Xv[k].Y - Xv[p].Y));
                                        Line ll0p = new Line(we, Xv[k].VertexNumber, Xv[p].VertexNumber);
                                        if (ExistL(ll0p.VertexIndexX, ll0p.VertexIndexY))
                                        {
                                            Is = AddIng(p2, p3, ref ii, ref jj);
                                            if (Is)
                                            {
                                                if (!ExistLd(ll0p.VertexIndexX, ll0p.VertexIndexY))
                                                    AddIngL(ll0p, ii, jj, wep);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (Maxan > Math.PI)
                    break;
                if (GetldCount() < Xl.Count)
                {
                    Maxan += 0.1;
                }
            } while (GetldCount() < Xl.Count);
            if (ad.Count > 0)
            {
                if (ad[0].Count == 0)
                {
                    ad.Clear();
                    sd.Clear();
                    ld.Clear();
                }
                Liens = ad.Count;
                Angle = ad.Count;
                for (int i = 0; i < sd.Count; i++)
                {
                    SumWeighEveryLines.Add(new float());
                    for (int j = 0; j < sd[i].Count; j++)
                    {
                        SumWeighEveryLines[SumWeighEveryLines.Count - 1] += sd[i][j];
                    }
                }
                SumWeighEveryLines = howto_WPF_3D_triangle_normalsuser.ImprovmentSort.Do(SumWeighEveryLines);
                diverstionSumWeighEveryLines = Divesion(SumWeighEveryLines);
            }
            CreateBolleanMatrixOfSubGraph();
            CreateSubSameEquValentGraph();
        }
        int GetsdCount()
        {
            int ind = 0;
            for (int i = 0; i < sd.Count; i++)
            {
                ind += sd[i].Count;
            }
            return ind;
        }
        int GetldCount()
        {
            int ind = 0;
            for (int i = 0; i < ld.Count; i++)
            {
                ind += ld[i].Count;
            }
            return ind;
        }
        int GetadCount()
        {
            int ind = 0;
            for (int i = 0; i < ad.Count; i++)
            {
                ind += ad[i].Count;
            }
            return ind;
        }
        int NoCloCur()
        {
            int Is = 0;

            for (int i = 0; i < ClosedCurved.Count; i++)
            {
                //
                for (int j = 0; j < ClosedCurved[i].Count; j++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                        //   if (i >= Xv.Count)

                        Is++;
                    }
                }
            }
            return Is;
        }
        bool ExistCloCur(int x1, int y1)
        {
            bool Is = false;

            for (int i = 0; i < ClosedCurved.Count; i++)
            {
                //
                for (int j = 0; j < ClosedCurved[i].Count; j++)
                {
                    object hh = new object();
                    lock (hh)
                    {
                        //   if (i >= Xv.Count)

                        if (ClosedCurved[i][j].X == x1 && ClosedCurved[i][j].Y == y1)
                        {
                            Is = true;
                        }
                    }
                }
            }
            return Is;
        }
        bool ExistCloCurContinuse(int x1, int y1)
        {
            bool Is = false;

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
            }
            return Is;
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
            ClosedCurved.Add(new List<Vertex>());
            try
            {

                int first = 1;
                for (int h = 0; h < Xl.Count - 1; h++)
                {
                    if ((Xl[h + 1].VertexIndexX == first || Xl[h + 1].VertexIndexY == first) && (h != 0))
                    {
                        int vxx = Xl[h + 1].VertexIndexX;
                        int vyy = Xl[h + 1].VertexIndexY;
                        Vertex tx = GetVerId(vxx);
                        Vertex ty = GetVerId(vyy);
                        if (vxx < vyy)
                        {
                            first = GetVerId(ty.VertexNumber).VertexNumber;
                            if (!ExistCloCur(tx.X, tx.Y))
                            {
                                if (tx != null)
                                    ClosedCurved[ClosedCurved.Count - 1].Add(tx);
                                else
                                    return;
                            }
                        }
                        else
                        {
                            first = GetVerId(tx.VertexNumber).VertexNumber;
                            if (!ExistCloCur(ty.X, ty.Y))
                            {
                                if (ty != null)
                                    ClosedCurved[ClosedCurved.Count - 1].Add(ty);
                                else
                                    return;
                            }
                        }
                        IsClosedCurved.Add(true);
                        ClosedCurved.Add(new List<Vertex>());
                    }
                    else
                        if ((Xl[h].VertexIndexX != Xl[h + 1].VertexIndexX) && (Xl[h].VertexIndexY != Xl[h + 1].VertexIndexX) && (Xl[h].VertexIndexX != Xl[h + 1].VertexIndexY) && (Xl[h].VertexIndexY != Xl[h + 1].VertexIndexY))
                    {
                        int vxx = Xl[h].VertexIndexX;
                        int vyy = Xl[h].VertexIndexY;
                        Vertex tx = GetVerId(vxx);
                        Vertex ty = GetVerId(vyy);
                        if (vxx < vyy)
                        {
                            first = GetVerId(ty.VertexNumber).VertexNumber;
                        }
                        else
                        {
                            first = GetVerId(tx.VertexNumber).VertexNumber;

                        }
                        IsClosedCurved.Add(false);
                        ClosedCurved.Add(new List<Vertex>());
                        continue;
                    }
                    else
                    {
                        int vx = Xl[h].VertexIndexX;
                        int vy = Xl[h].VertexIndexY;
                        if (vx > vy)
                        {
                            Vertex tx = GetVerId(vx);
                            Vertex ty = GetVerId(vy);
                            if (!ExistCloCur(tx.X, tx.Y))
                            {
                                if (tx != null)
                                    ClosedCurved[ClosedCurved.Count - 1].Add(tx);
                                else
                                    return;
                            }
                            if (!ExistCloCur(ty.X, ty.Y))
                            {
                                if (ty != null)
                                    ClosedCurved[ClosedCurved.Count - 1].Add(ty);
                                else
                                    return;
                            }
                        }
                        else
                        {
                            Vertex tx = GetVerId(vx);
                            Vertex ty = GetVerId(vy);
                            if (!ExistCloCur(ty.X, ty.Y))
                            {
                                if (ty != null)
                                    ClosedCurved[ClosedCurved.Count - 1].Add(ty);
                                else
                                    return;
                            }
                            if (!ExistCloCur(tx.X, tx.Y))
                            {
                                if (tx != null)
                                    ClosedCurved[ClosedCurved.Count - 1].Add(tx);
                                else
                                    return;
                            }
                        }
                    }
                }
                if (ClosedCurved.Count == IsClosedCurved.Count + 1)
                    IsClosedCurved.Add(false);
                for (int h = 0; h < IsClosedCurved.Count; h++)
                {
                    if (ClosedCurved[h].Count == 0)
                    {
                        ClosedCurved.RemoveAt(h);
                        IsClosedCurved.RemoveAt(h);
                        h = 0;
                        continue;
                    }
                    if (!IsClosedCurved[h])
                    {
                        IsClosedCurved.RemoveAt(h);
                        ClosedCurved.RemoveAt(h);
                        h = 0;
                    }
                }
                if (ClosedCurved.Count > 0)
                {
                    if (!IsClosedCurved[0])
                    {
                        ClosedCurved.Clear();
                        IsClosedCurved.Clear();
                    }
                }
                numberOfClosedCurved = ClosedCurved.Count;
            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
            }
        }
    };
}
