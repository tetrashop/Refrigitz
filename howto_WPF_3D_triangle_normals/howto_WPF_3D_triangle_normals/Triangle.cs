using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using LearningMachine;

namespace howto_WPF_3D_triangle_normals
{  class Triangle
    {

        double a, b, c, d;
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


            a = cc[0];
            b = cc[1];
            c = cc[2];
            d = a * (p0.X) + b * (p0.Y) + c * (p0.Z);
        }
    }
}
