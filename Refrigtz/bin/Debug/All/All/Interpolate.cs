/******************************
 * Ramin Edjlal CopyRigth 2015
 * Polynomial Interpolate 
 * Implementation recursivley.
 * Determinant
 * TransPoset
 * Recurve Matrix.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LearningMachine
{
    [Serializable]
    public class Interpolate
    {
        static Double[,] D;
        static Double[] F;
        
        public static double[] Quaficient(double[,] AMinuseOnea, double[] b, int n)
        {
            double[] ans = new double[n];
            double[,] x = AMinuseOne(AMinuseOnea, n);
            for (int i = 0; i <n; i++)
            {
                for (int j = 0; j <n; j++)
                {
                    
                        ans[i] += x[i, j] * b[j];
                   
                }
            }
            return ans;
        }
        public static double[] Quaficient(double[,,] AMinuseOnea, double[] b, int n)
        {
            double[] ans = new double[n];
            double[,,] x = AMinuseOne(AMinuseOnea, n);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    for (int k = 0; k < n; k++)
                    {
                        ans[i] += x[i, j, k] * b[k];
                    }
                }
            }
            return ans;
        }
        static public Double[] Array(Double[] ArrayInput, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                Double[] ArrayOutputA = new Double[n];
                Double[] ArrayOutput;
                Double[] Array = new Double[n];
                ArrayOutput = Answer(ArrayInput, n);
                for (int i = 0; i < n; i++)
                    Array[i] = (Double)ArrayOutput[i];
                return Array;
            }
        }
       static  Double[] Answer(Double[] a, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                Double[] Ans = new Double[n];
                D = new Double[n, n];
                F = new Double[n];
                for (int i = 0; i < n; i++)
                    D[i, 0] = 1;
                for (int i = 0; i < n; i++)
                    for (int j = 1; j < n; j++)
                        D[i, j] = System.Math.Pow(i, j);
                for (int i = 0; i < n; i++)
                    F[i] = a[i];

                D = AMinuseOne(D, n);

                for (Int32 i = 0; i < n; i++)
                    for (Int32 j = 0; j < n; j++)
                        Ans[i] = Ans[i] + D[i, j] * F[j];
                return Ans;
            }
        }
        static Double[,] AMinuseOne(Double[,] A, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                Double[,] N = new Double[n, n];
                Double[,] Ast = new Double[n - 1, n - 1];
                for (Int32 ii = 0; ii < n; ii++)
                    for (Int32 jj = 0; jj < n; jj++)
                        N[ii, jj] = System.Math.Pow(-1, ii + jj) * Det(AStar(A, n, ii, jj), n - 1);

                for (int i = 0; i < n; i++)
                    for (int j = i + 1; j < n; j++)
                    {
                        Double AS = N[i, j];
                        N[i, j] = N[j, i];
                        N[j, i] = AS;
                    }
                Double SAS = 1 / Det(A, n);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        N[i, j] = SAS * N[i, j];
                return N;
            }
        }
           static Double[,,] AMinuseOne(Double[,,] A, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                Double[,,] N = new Double[n, n,n];
                Double[,,] Ast = new Double[n - 1, n - 1,n-1];
                for (Int32 ii = 0; ii < n; ii++)
                    for (Int32 jj = 0; jj < n; jj++)
                        for (Int32 kk = 0; kk < n; kk++)
                            N[ii, jj, kk] = System.Math.Pow(-1, ii + jj + kk) * Det(AStar(A, n, ii, jj, kk), n - 1);

                for (int i = 0; i < n; i++)
                    for (int j = i + 1; j < n; j++)
                        for (int k = j + 1; k < n; k++)
                        {
                            Double AS = N[i, j, k];
                            N[i, j, k] = N[j, i, k];
                            N[j, i, k] = AS;
                        }
                Double SAS = 1 / Det(A, n);
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        for (int k = 0; k < n; k++)
                            N[i, j, k] = SAS * N[i, j, k];
                return N;
            }
        }
        static Double Det(Double[,] A, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                if (n == 0)
                    return 0;
                if (n == 1)
                    return A[0, 0];
                if (n == 2)
                    return A[0, 0] * A[1, 1] - A[0, 1] * A[1, 0];
                Double AA = 0;
                for (int i = 0; i < n; i++)
                    AA = AA + A[0, i] * System.Math.Pow(-1, i) * Det(AStar(A, n, 0, i), n - 1);
                return AA;
            }
        }
        static Double Det(Double[,,] A, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                if (n == 0)
                    return 0;
                if (n == 1)
                    return A[0, 0,0];
             
                Double AA = 0;
                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n; j++)
                        AA = AA + A[0, 0, i] * System.Math.Pow(-1, i + j) * Det(AStar(A, n, 0, i, j), n - 1);
                return AA;
            }
        }
        static bool Det(bool[,] A, int n)
        {
            Object o = new Object();
            lock (o)
            {
                if (n == 0)
                    return false;
                if (n == 1)
                    return A[0, 0];
                if (n == 2)
                    return ((((A[0, 0] && A[1, 1])) || (!(A[0, 1]) && A[1, 0])));
                bool AA = false;
                for (int i = 0; i < n; i++)
                    AA = AA || (A[0, i] && System.Convert.ToBoolean(System.Math.Pow(-1, i)) && Det(AStar(A, n, 0, i), n - 1));
                return AA;
            }
        }
        static double DetB(bool[,] A, int n)
        {
            Object o = new Object();
            lock (o)
            {
                if (n == 0)
                    return 0 ;
                if (n == 1)
                    return System.Convert.ToDouble(A[0, 0]);
                if (n == 2)
                    return System.Convert.ToDouble(A[0, 0])* System.Convert.ToDouble(A[1, 1])- System.Convert.ToDouble(A[0, 1]) * System.Convert.ToDouble(A[1, 0]);
                double AA = 0;
                for (int i = 0; i < n; i++)
                    AA = AA + System.Convert.ToDouble(A[0, i]) * System.Convert.ToDouble(System.Math.Pow(-1, i)) * DetB(AStar(A, n, 0, i), n - 1); ;
                return AA;
            }
        }
        static Double[,] AStar(Double[,] A, Int32 n, Int32 ii, Int32 jj)
        {
            Object o = new Object();
            lock (o)
            {
                Double[,] Ast = new Double[n - 1, n - 1];
                Int32 ni = 0, nj = 0;
                for (int i = 0; i < n; i++)
                {
                    nj = 0;
                    if ((i == ii))
                        i++;
                    if (i == n)
                        break;
                    for (int j = 0; j < n; j++)
                    {
                        if ((j != jj))
                        {
                            Ast[ni, nj] = A[i, j];
                            nj++;
                        }
                    }
                    ni++;

                }
                return Ast;
            }
        }
        static Double[,,] AStar(Double[,,] A, Int32 n, Int32 ii, Int32 jj,int kk)
        {
            Object o = new Object();
            lock (o)
            {
                Double[,,] Ast = new Double[n - 1, n - 1,n-1];
                Int32 ni = 0, nj = 0, nk = 0;
                for (int i = 0; i < n; i++)
                {
                    nj = 0;
                    if ((i == ii))
                        i++;
                    if (i == n)
                        break;
                    for (int j = 0; j < n; j++)
                    {
                        nk = 0;
                        if ((j != jj))
                        {
                            for (int k = 0; k < n; k++)
                            {
                                if ((k != kk))
                                {
                                    Ast[ni, nj, nk] = A[i, j, k];
                                    nk++;
                                }
                            }
                            nj++;

                        }
                    }
                    ni++;

                }
                return Ast;
            }
        }
        static bool[,] AStar(bool[,] A, Int32 n, Int32 ii, Int32 jj)
        {
            Object o = new Object();
            lock (o)
            {
                bool[,] Ast = new bool[n - 1, n - 1];
                Int32 ni = 0, nj = 0;
                for (int i = 0; i < n; i++)
                {
                    nj = 0;
                    if ((i == ii))
                        i++;
                    if (i == n)
                        break;
                    for (int j = 0; j < n; j++)
                    {
                        if ((j != jj))
                        {
                            Ast[ni, nj] = A[i, j];
                            nj++;
                        }
                    }
                    ni++;

                }
                return Ast;
            }
        }
        public static bool Similarity(bool[,] A, bool[,] B, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                bool[,] Ast = new bool[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Ast[i, j] = ((((A[i, j])) || (!B[i, j])));
                    }


                }
                return ((false || (!Det(Ast, n))));
            }
        }
        public static double SimilarityB(bool[,] A, bool[,] B, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                bool[,] Ast = new bool[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Ast[i, j] = System.Convert.ToBoolean(System.Convert.ToDouble(A[i, j]) - System.Convert.ToDouble(B[i, j]));
                    }


                }
                return 1- DetB(Ast, n);
            }
        }
        public static double SimilarityB(double[,] A, double[,] B, Int32 n)
        {
            Object o = new Object();
            lock (o)
            {
                double[,] Ast = new double[n, n];
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        Ast[i, j] = System.Convert.ToDouble(A[i, j]) - System.Convert.ToDouble(B[i, j]);
                    }


                }
                return 1 - Det(Ast, n);
            }
        }
    }
}
