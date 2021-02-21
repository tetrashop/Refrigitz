using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using LearningMachine;

namespace howto_WPF_3D_triangle_normals
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
                double count = (Math.Sqrt(p0.X * p0.X + p0.Y * p0.Y + p0.Z * p0.Z) + Math.Sqrt(p1.X * p1.X + p1.Y * p1.Y + p1.Z * p1.Z) + Math.Sqrt(p2.X * p2.X + p2.Y * p2.Y + p2.Z * p2.Z)) / 3;
                if (count <= 2 * d)
                    return true;
                return false;
            }
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
        Point3D getd(Point3D p0, Point3D p1)
        {
            Line l0 = new Line(p0, p1);
            return new Point3D(p1.X + l0.a * 2, p1.Y + l0.b * 2, p1.Z + l0.c * 2);
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
            if (countb / (double)scount <= percent)
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
            double r0 = Math.Sqrt((aa.X - bb.X) * (aa.X - bb.X) + (aa.Y - bb.Y) * (aa.Y - bb.Y) + (aa.Z - bb.Z) * (aa.Z - bb.Z));
            double r1 = Math.Sqrt((aa.X - cc.X) * (aa.X - cc.X) + (aa.Y - cc.Y) * (aa.Y - cc.Y) + (aa.Z - cc.Z) * (aa.Z - cc.Z));

            double r2 = Math.Sqrt((bb.X - cc.X) * (bb.X - cc.X) + (bb.Y - cc.Y) * (bb.Y - cc.Y) + (bb.Z - cc.Z) * (bb.Z - cc.Z));

            if ((r0 > ht * 3))
            {
                s.RemoveAt(i);
                Done = true;
            }
            else
            if ((r1 > ht * 3))
            {
                s.RemoveAt(j);
                Done = true;
            }
            else
            if ((r2 > ht * 3))
            {
                s.RemoveAt(k);
                Done = true;
            }
            return Done;
        }
        public int reduceCountOfpoints(ref List<Point3D> sss, double ht, double percent)
        {
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

                        ParallelOptions poo = new ParallelOptions(); poo.MaxDegreeOfParallelism = 2; Parallel.For(i + 1, s.Count, j =>
                        {//float[,,] cc = new float[(maxr - minr + 1), (maxteta - minteta + 1), 3];
                            ParallelOptions ppoio = new ParallelOptions(); ppoio.MaxDegreeOfParallelism = 2; Parallel.For(j + 1, s.Count, k =>
                            {            //external point
                                ParallelOptions ppopio = new ParallelOptions(); ppopio.MaxDegreeOfParallelism = 2; Parallel.For(0, s.Count, b =>
                                {
                                    if (boundry(i, j, k, b, s.Count, countb, percent))
                                        return;
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
                                    }
                                    if (b >= s.Count)
                                        return;
                                });
                                if (k >= s.Count)
                                    return;
                            });
                            if (j >= s.Count)
                                return;
                        });
                        if (i >= s.Count)
                            return;
                    });
                });
                output.Wait();

            } while ((double)s.Count / countb > percent && (Done));
            sss = s;
            return sss.Count;
        }
    }
}
