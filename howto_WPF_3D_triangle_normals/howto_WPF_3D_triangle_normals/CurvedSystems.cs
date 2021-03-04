using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using LearningMachine;

namespace howto_WPF_3D_triangle_normals
{
    class CurvedSystems
    {
      public  List<double[]> qsystem = new List<double[]>();
        List<double[]> q = new List<double[]>();
        List<double> qddd = new List<double>();
        List<Point3D> qdddpoints = new List<Point3D>();
        List<List<Point3D>> listofssemipoints = null;

        List<Point3D> source = null;
        public CurvedSystems(List<Point3D> ss)
        {
            source = ss;
            listofssemipoints = getlistOfSemilineuniqe(ss);
        }
        public double[] CreateQuficientofCurved()
        {
            for (int i = 0; i < listofssemipoints.Count; i++)
            {
                for (int j = 0; j < listofssemipoints[i].Count; j += 4)
                {
                    double[,] qcurve = new double[3, 3];

                    double[] ddd = new double[3];
                    qcurve[0, 0] = listofssemipoints[i][j].X;
                    qcurve[0, 1] = listofssemipoints[i][j].Y;
                    qcurve[0, 2] = listofssemipoints[i][j].Z;

                    qcurve[1, 0] = listofssemipoints[i][j + 1].X;
                    qcurve[1, 1] = listofssemipoints[i][j + 1].Y;
                    qcurve[1, 2] = listofssemipoints[i][j + 1].Z;

                    qcurve[2, 0] = listofssemipoints[i][j + 2].X;
                    qcurve[2, 1] = listofssemipoints[i][j + 2].Y;
                    qcurve[2, 2] = listofssemipoints[i][j + 2].Z;

                    ddd[0] = listofssemipoints[i][j + 3].X;
                    ddd[1] = listofssemipoints[i][j + 3].Y;
                    ddd[2] = listofssemipoints[i][j + 3].Z;

                    qdddpoints.Add(new Point3D(listofssemipoints[i][j + 3].X, listofssemipoints[i][j + 3].Y, listofssemipoints[i][j + 3].Z));
                    q.Add(Interpolate.Quaficient(qcurve, ddd, 3));
                }
            }
            for (int i = 0; i < q.Count; i++)
            {
                double[] ddd = new double[3];

                ddd[0] = q[i][0] * qdddpoints[i].X;
                ddd[1] = q[i][1] * qdddpoints[i].Y;
                ddd[2] = q[i][2] * qdddpoints[i].Z;


                qddd.Add(ddd[0] + qddd[1] + qddd[2]);


            }

            bool done = false;
            List<double[]> qq = q;
            do
            {

                for (int j = 0; j < qq.Count; j += 3)
                {
                    double[,] qcurve = new double[3, 3];

                    double[] ddd = new double[3];
                    qcurve[0, 0] = qq[j][0];
                    qcurve[0, 1] = qq[j][1];
                    qcurve[0, 2] = qq[j][2];

                    qcurve[1, 0] = qq[j + 1][0];
                    qcurve[1, 1] = qq[j + 1][1];
                    qcurve[1, 2] = qq[j + 1][2];

                    qcurve[2, 0] = qq[j + 2][0];
                    qcurve[2, 1] = qq[j + 2][1];
                    qcurve[2, 2] = qq[j + 2][2];

                    ddd[0] = qddd[j];
                    ddd[1] = qddd[j + 1];
                    ddd[2] = qddd[j + 2];

                    qsystem.Add(Interpolate.Quaficient(qcurve, ddd, 3));
                }
                qq = qsystem;
                qsystem.Clear();
            } while (qq.Count >= 3);
            return qq[0];
        }
        bool exist(Point3D ss, List<List<Point3D>> d)
        {
            if (d.Count == 0)
                return false;

            for (int i = 0; i < d.Count; i++)
            {
                for (int j = 0; j < d[i].Count; j++)
                {

                    if (ss.X == d[i][j].X && ss.Y == d[i][j].Y && ss.Z == d[i][j].Z)
                        return true;
                }
            }
            return false;
        }
        public bool boundry(int i, int j, int k)
        {
            if (i == j)
                return true;
            if (i == k)
                return true;
            if (j == k)
                return true;

            return false;
        }
        //create list of semi curved; continusly
        List<List<Point3D>> getlistOfSemilineuniqe(List<Point3D> s)
        {
            List<List<Point3D>> ListOfSemiLineUniq = new List<List<Point3D>>();
            bool found = false;
            double min = double.MaxValue;
            Point3D next = new Point3D();
            int semiscount = 0;
            int ii = -1, jj = -1, kk = -1;
            do
            {

                var output = Task.Factory.StartNew(() =>

                {

                    found = false;
                    min = double.MaxValue;
                    ii = -1;
                    jj = -1;
                    if (next == null)
                        kk = -1;
                    for (int i = 0; i < s.Count; i++)
                    {
                        if (next != null)
                        {
                            i = kk;
                            kk = -1;
                        }
                        if (boundry(i, -1, -1))
                            return;
                        for (int j = 0; j < s.Count; j++)
                        {
                            if (boundry(i, j, -1))
                                return;

                            for (int k = 0; k < s.Count; k++)
                            {

                                if (boundry(i, j, k))
                                    return;
                                //external point
                                Line l0 = new Line(s[i], s[j]);
                                Line l1 = new Line(s[j], s[k]);
                                double d = Line.getAlpha(l0, l1);
                                if (d < min)
                                {
                                    ii = i;
                                    jj = j;
                                    kk = k;
                                    min = d;
                                    found = true;
                                    next = s[k];
                                }
                            }
                        }
                    }
                });
                output.Wait();
                if (found)
                {
                    if (((!exist(s[ii], ListOfSemiLineUniq)) || ((s[ii] == next) && (exist(next, ListOfSemiLineUniq)))) && (!exist(s[jj], ListOfSemiLineUniq)) && (!exist(s[kk], ListOfSemiLineUniq)))
                    {
                        if ((!exist(s[ii], ListOfSemiLineUniq)))
                            ListOfSemiLineUniq[semiscount].Add(s[ii]);
                        if ((!exist(s[jj], ListOfSemiLineUniq)))
                            ListOfSemiLineUniq[semiscount].Add(s[jj]);
                        if ((!exist(s[kk], ListOfSemiLineUniq)))
                            ListOfSemiLineUniq[semiscount].Add(s[kk]);
                    }
                }
                if (!found)
                {
                    ListOfSemiLineUniq = new List<List<Point3D>>();
                    semiscount++;
                }
            } while (found);
            return ListOfSemiLineUniq;
        }

    }
}
