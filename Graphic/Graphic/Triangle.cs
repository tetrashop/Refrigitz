using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningMachine;
using Point3Dspaceuser;

namespace howto_WPF_3D_triangle_normalsuser
{
    class Triangle
    {

        double a, b, c, d;
        //normal of plate
        public double na, nb, nc;
        public Triangle()
        { }
        public Triangle(Point3D p0, Point3D p1, Point3D p2)
        {
            Point3D dd = getd(p0, p1);
            double[,] aa = new double[3, 3];

            double[] ddd = new double[3];

            aa[0, 0] = p0.X;
            aa[0, 1] = p0.Y;
            aa[0, 2] = p0.Z;

            aa[1, 0] = p1.X;
            aa[1, 1] = p1.Y;
            aa[1, 2] = p1.Z;

            aa[2, 0] = p2.X;
            aa[2, 1] = p2.Y;
            aa[2, 2] = p2.Z;

            ddd[0] = dd.X;
            ddd[1] = dd.Y;
            ddd[2] = dd.Z;

            double[] cc = Interpolate.Quaficient(aa, ddd, 3);

            //plate
            a = cc[0];
            b = cc[1];
            c = cc[2];
            d = a * (p0.X) + b * (p0.Y) + c * (p0.Z);

            //create vectors contain plate
            Line l0 = new Line(p0, p1);
            Line l1 = new Line(p0, p1);
            //normals indices
            na = (l0.b * l1.c) - (l0.c * l1.b);
            nb = (l0.c * l1.a) - (l0.a * l1.c);
            nc = (l0.a * l1.b) - (l0.b * l1.a);
        }
        //point external mul vectors is zero (0 degree)
        bool externalMulIsEqual(Point3D p0, Point3D p1, Point3D p2, Point3D externalp0)
        {
            Triangle t0 = new Triangle(p0, p1, p2);
            Line l1 = new Line(t0, externalp0);
            double na = (t0.nb * l1.c) - (t0.nc * l1.b);
            double nb = (t0.nc * l1.a) - (t0.na * l1.c);
            double nc = (t0.na * l1.b) - (t0.nb * l1.a);
            return (na == nb) && (na == nc) & (na == 0);

        }
        //point external mul vectors is zero (180 degree)
        bool externalMulIsEqualiInverse(Point3D p0, Point3D p1, Point3D p2, Point3D externalp0)
        {
            Triangle t0 = new Triangle(p0, p1, p2);
            Line l1 = new Line(t0, externalp0);
            double na = ((-1 * t0.nb) * l1.c) - ((-1 * t0.nc) * l1.b);
            double nb = ((-1 * t0.nc) * l1.a) - ((-1 * t0.na) * l1.c);
            double nc = ((-1 * t0.na) * l1.b) - ((-1 * t0.nb) * l1.a);
            return (na == nb) && (na == nc) & (na == 0);

        }
        public int externalMuliszerotow(Point3D p0, Point3D p1, Point3D p2, List<Point3D> externalp0)
        {
            object o = new object();
            lock (o)
            {
                int count = 0;
                for (int i = 0; i < externalp0.Count; i++)
                {
                    if (!(externalp0.Contains(p0) || externalp0.Contains(p1) || externalp0.Contains(p2)))
                    {
                        if (externalMulIsEqual(p0, p1, p2, externalp0[i]))
                            count++;
                        if (externalMulIsEqualiInverse(p0, p1, p2, externalp0[i]))
                            count++;
                    }
                }
                return count;
            }
        }
        public bool distancesaticfied(Point3D p0, Point3D p1, Point3D p2, double d)
        {
            object o = new object();
            lock (o)
            {
                double count = (Math.Sqrt((p0.X - p1.X) * (p0.X - p1.X) + (p0.Y - p1.Y) * (p0.Y - p1.Y) + (p0.Z - p1.Z) * (p0.Z - p1.Z)) + Math.Sqrt((p0.X - p2.X) * (p0.X - p2.X) + (p0.Y - p2.Y) * (p0.Y - p2.Y) + (p0.Z - p2.Z) * (p0.Z - p2.Z)) + Math.Sqrt((p1.X - p2.X) * (p1.X - p2.X) + (p1.Y - p2.Y) * (p1.Y - p2.Y) + (p1.Z - p2.Z) * (p1.Z - p2.Z))) / 3;
                if (count <= 2 * d)
                    return true;
                return false;
            }
        }
        bool exist(Point3D ss, List<Point3D> d)
        {
            if (d.Count == 0)
                return false;
            for (int i = 0; i < d.Count; i++)
            {
                if (ss.X == d[i].X && ss.Y == d[i].Y && ss.Z == d[i].Z)
                    return true;
            }
            return false;
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
        Point3D getd(Point3D p0, Point3D p1)
        {
            Line l0 = new Line(p0, p1);
            return new Point3D(p1.X + l0.a * 2, p1.Y + l0.b * 2, p1.Z + l0.c * 2);
        }
        bool exist(Point3D[] ss, List<Point3D[]> d)
        {
            if (d.Count == 0)
                return false;
            for (int i = 0; i < d.Count; i++)
            {
                if (ss[0].X == d[i][0].X && ss[0].Y == d[i][0].Y && ss[0].Z == d[i][0].Z)
                    return true;
            }
            return false;
        }
        bool boundry(int i, int j, int k, int b, int scount, double countb, double percent)
        {
            if (i == j)
                return true;
            if (i == k)
                return true;
            if (i == b)
                return true;
            if (j == k)
                return true;
            if (j == b)
                return true;
            if (k == b)
                return true;
            if ((double)countb / (double)scount < percent)
                return true;
            if (b >= scount)
                return true;
            if (k >= scount)
                return true;
            if (j >= scount)
                return true;
            if (i >= scount)
                return true;
            return false;
        }
        bool boundryout(int i, int j, int k, int b, int scount, double countb, double percent)
        {
            if ((double)countb / (double)scount <= percent)
                return true;
            if (b >= scount)
                return true;
            if (k >= scount)
                return true;
            if (j >= scount)
                return true;
            if (i >= scount)
                return true;
            return false;
        }
        bool distancereduced(Point3D aa, Point3D bb, Point3D cc, ref bool Done, ref List<Point3D> s, double ht, int i, int j, int k)
        {
            bool dos = false;
            double r0 = Math.Sqrt((aa.X - bb.X) * (aa.X - bb.X) + (aa.Y - bb.Y) * (aa.Y - bb.Y) + (aa.Z - bb.Z) * (aa.Z - bb.Z));
            double r1 = Math.Sqrt((aa.X - cc.X) * (aa.X - cc.X) + (aa.Y - cc.Y) * (aa.Y - cc.Y) + (aa.Z - cc.Z) * (aa.Z - cc.Z));

            double r2 = Math.Sqrt((bb.X - cc.X) * (bb.X - cc.X) + (bb.Y - cc.Y) * (bb.Y - cc.Y) + (bb.Z - cc.Z) * (bb.Z - cc.Z));

            if ((r0 < ht * 3) && (r0 > ht))
            {
                s.RemoveAt(i);
                Done = true;
                dos = true;
            }
            else
          if ((r1 < ht * 3) && (r1 > ht))
            {
                s.RemoveAt(j);
                Done = true;
                dos = true;
            }
            else
          if ((r2 < ht * 3) && (r2 > ht))
            {
                s.RemoveAt(k);
                Done = true;
                dos = true;
            }
            return dos;
        }
        void removeitem(Triangle at, ref List<Point3D> s, int i, int b, int j, int k, ref bool Done, double ht)
        {
            double h = System.Math.Abs(at.a * s[b].X + at.b * s[b].Y + at.c * s[b].Z - at.d) / Math.Sqrt(at.a * at.a + at.b * at.b + at.c * at.c);
            if (h < ht && h != 0)
            {
                if (System.Math.Abs(s[b].X - s[i].X) == System.Math.Abs(s[b].X - s[j].X) && System.Math.Abs(s[b].X - s[j].X) == System.Math.Abs(s[b].X - s[k].X))
                {
                    s.RemoveAt(b);
                    Done = true;
                }
                else
                     if (System.Math.Abs(s[b].Y - s[i].Y) == System.Math.Abs(s[b].Y - s[j].Y) && System.Math.Abs(s[b].Y - s[j].Y) == System.Math.Abs(s[b].Y - s[k].Y))
                {
                    s.RemoveAt(b);
                    Done = true;
                }
                else if (System.Math.Abs(s[b].Z - s[i].Z) == System.Math.Abs(s[b].Z - s[j].Z) && System.Math.Abs(s[b].Z - s[j].Z) == System.Math.Abs(s[b].Z - s[k].Z))
                {
                    s.RemoveAt(b);
                    Done = true;
                }

            }

        }

       double minraddpoints(List<Point3D> p0)
        {
            double r = double.MaxValue;
            for (int i = 0; i < p0.Count; i++)
            {
                for (int j = 0; j < p0.Count; j++)
                {

                    double a = Math.Sqrt((p0[i].X - p0[j].X) * (p0[i].X - p0[j].X) + (p0[i].Y - p0[j].Y) * (p0[i].Y - p0[j].Y) + (p0[i].Z - p0[j].Z) * (p0[i].Z - p0[j].Z));

                    if (a < r && a != 0)
                        r = a;
                }
            }
            return r;
        }
        List<Point3D> reductionSetOfPointsToNumberOfSets(List<Point3D> s)
        {
            bool reduced = false;
            List<Point3D> sss = s;
            Point3D p = new Point3D(-1, -1, -1);

            List<Point3D> xxx = new List<Point3D>();
            List<List<Point3D>> xxxAddedClonies = new List<List<Point3D>>();
            double minr = minraddpoints(s);
            bool add = false;
            int index = 0;
            do
            {
                add = false;
                minr = minraddpoints(sss);
                var output = Task.Factory.StartNew(() =>
                {

                    ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = 2; Parallel.For(0, sss.Count, i =>
                    {


                        ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = 2; Parallel.For(0, sss.Count, j =>
                        {//float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                            if (boundryout(i, 0, 0, 0, sss.Count, 0, 0))
                                return;
                            else
                            {
                                Point3D p0 = sss[i];
                                Point3D p1 = sss[j];
                                bool a = exist(p0, xxxAddedClonies);
                                bool b = exist(p1, xxxAddedClonies);

                                if ((!(a || b)) && (!add))
                                {
                                    double count = Math.Sqrt((p0.X - p1.X) * (p0.X - p1.X) + (p0.Y - p1.Y) * (p0.Y - p1.Y) + (p0.Z - p1.Z) * (p0.Z - p1.Z));
                                    if (count <= minr)
                                    {
                                        xxx.Add(p0);
                                        add = true;
                                        if (!(a))
                                            xxxAddedClonies[index].Add(s[i]);
                                        if (!(b))
                                            xxxAddedClonies[index].Add(s[j]);
                                        sss.RemoveAt(i);
                                        sss.RemoveAt(j);
                                    }
                                }
                                else
                                {

                                    p0 = p;
                                    if (p.X != -1 && p.Y != -1 && p.Z != -1)
                                    {
                                        double count = Math.Sqrt((p0.X - p1.X) * (p0.X - p1.X) + (p0.Y - p1.Y) * (p0.Y - p1.Y) + (p0.Z - p1.Z) * (p0.Z - p1.Z));
                                        if (count <= xxxAddedClonies[index].Count * minr)
                                        {
                                            if (!(b))
                                                xxxAddedClonies[index].Add(s[j]);
                                            sss.RemoveAt(j);
                                        }
                                    }
                                }
                            }
                        });
                    });
                });
                output.Wait();
                xxxAddedClonies.Add(new List<Point3D>());
                index++;
                p = new Point3D(-1, -1, -1);
            } while (sss.Count > 0 && add);

            return xxx;
        }
        public int reduceCountOfpoints(ref List<Point3D> sss, double ht, double percent,ref List<Point3D> xxx)
        {
            xxx = reductionSetOfPointsToNumberOfSets(sss);
            if (xxx.Count > 1)
                return xxx.Count;



            double countb = sss.Count;

            List<Point3D[]> d = new List<Point3D[]>();
            bool Done = false;
            List<Point3D> s = sss;
            do
            {
                Done = false;
                var output = Task.Factory.StartNew(() =>
                {

                    ParallelOptions po = new ParallelOptions(); po.MaxDegreeOfParallelism = 2; Parallel.For(0, s.Count, i =>
                    {

                        if (boundryout(i, 0, 0, 0, s.Count, countb, percent))
                            return;
                        ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = 2; Parallel.For(0, s.Count, j =>
                        {//float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                            if (boundryout(i, j, 0, 0, s.Count, countb, percent))
                                return;
                            ParallelOptions ppoio = new ParallelOptions(); ppoio.MaxDegreeOfParallelism = 2; Parallel.For(0, s.Count, k =>
                            {            //external point
                                if (boundryout(i, j, k, 0, s.Count, countb, percent))
                                    return;
                                ParallelOptions ppopio = new ParallelOptions(); ppopio.MaxDegreeOfParallelism = 2; Parallel.For(0, s.Count, b =>
                                {
                                    if (boundry(i, j, k, b, s.Count, countb, percent))
                                        return;
                                    else
                                    if (i < s.Count && j < s.Count && k < s.Count && b < s.Count)
                                    {
                                        Point3D aa = new Point3D(s[i].X, s[i].Y, s[i].Z);
                                        Point3D bb = new Point3D(s[j].X, s[j].Y, s[j].Z);
                                        Point3D cc = new Point3D(s[k].X, s[k].Y, s[k].Z);
                                        if (!distancereduced(aa, bb, cc, ref Done, ref s, ht, i, j, k))
                                        {
                                            Triangle at = new Triangle(aa, bb, cc);

                                            Point3D[] ss = new Point3D[3];
                                            ss[0] = new Point3D(aa.X, aa.Y, aa.Z);
                                            ss[1] = new Point3D(bb.X, bb.Y, bb.Z);
                                            ss[2] = new Point3D(cc.X, cc.Y, cc.Z);
                                            ss = ImprovmentSort.Do(ss);
                                            if (!exist(ss, d))
                                            {
                                                d.Add(ss);

                                                removeitem(at, ref s, i, b, j, k, ref Done, ht);
                                            }
                                        }
                                    }
                                });
                            });
                        });
                    });
                });
                output.Wait();

            } while ((double)s.Count / countb >= percent && (Done));
            sss = s;
            return sss.Count;
        }
    }
}
