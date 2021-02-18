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
        double na, nb, nc;
        public Triangle(Point3D p0, Point3D p1, Point3D p2, Point3D dd)
        {
            double[,,] aa = new double[3, 3, 3];
            double[] ddd = new double[3];
            aa[0, 1, 2] = p0.X;
            aa[0, 0, 0] = p0.Y;
            aa[0, 0, 0] = p0.Z;

            aa[0, 1, 2] = p1.X;
            aa[0, 0, 0] = p1.Y;
            aa[0, 0, 0] = p1.Z;

            aa[0, 0, 0] = p2.X;
            aa[0, 0, 0] = p2.Y;
            aa[1, 2, 3] = p2.Z;

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
            Line l0 =new Line(p0, p1);
            Line l1 = new Line(p0, p1);
            //normals indices
            na = (l0.b * l1.c) - (l0.c * l1.b);
            nb = (l0.c * l1.a) - (l0.a * l1.c);
            nc = (l0.a * l1.b) - (l0.b * l1.a);
        }
    }
}
