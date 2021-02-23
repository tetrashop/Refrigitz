﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using howto_WPF_3D_triangle_normalsuser;
using Point3Dspaceuser;

namespace howto_WPF_3D_triangle_normalsuser
{
    class Line
    {
        public double a, b, c, x0, y0, z0;
        public Line(Point3D p0,Point3D p1)
        {
            x0 = p0.X;
            y0 = p0.Y;
            z0 = p0.Z;
            a = (p1.X - p1.X);
            b = (p1.Y - p1.Y);
            c = (p1.Z - p1.Z);
        }
        //constructore for orthogonal line travers from p0 on plate normal0
        public Line(Triangle normal0, Point3D p0)
        {
            x0 = p0.X;
            y0 = p0.Y;
            z0 = p0.Z;
            a = normal0.na;
            b = normal0.nb;
            c = normal0.nc;
        }
        bool exist(Point3D p)
        {
            if (a == 0 || b == 0 || c == 0)
                return false;
            return (((p.X - x0) / a) == (p.Y - y0) / b) && ((p.X - x0) / a) == ((p.Z - z0) / c);
        }
        bool externalMulIsEqual(Triangle t0, Point3D p0)
        {
            Line l1 = new Line(t0, p0);
            double na = (t0.nb * l1.c) - (t0.nc * l1.b);
            double nb = (t0.nc * l1.a) - (t0.na * l1.c);
            double nc = (t0.na * l1.b) - (t0.nb * l1.a);
            return (na == nb) && (na == nc) & (na == 0);

        }
        
    }
}