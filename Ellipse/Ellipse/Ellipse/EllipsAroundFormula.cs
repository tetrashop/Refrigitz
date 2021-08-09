using System;

namespace Ellipse
{
    internal class EllipsAroundFormula
    {
        private double around = 0;
        private double aroundP2 = 0;
        private double Aaround = 0;
        private readonly double around0 = 0;
        private readonly double aroundPiPer2 = 0;
        private readonly double aroundPiatanpi2 = 0;
        private readonly double around1 = 0;
        private readonly double p = 0;
        private readonly double e = 0;
        public EllipsAroundFormula(double a, double b, double c)
        {
            // Params rr = new Params(a, b);
            //Initiate Parameters of Ellipse.
            p = Math.Pow(b, 2) / a;
            e = System.Math.Sqrt(1.0 - (p / a));
            c = e * a;
            double teta = Math.PI - Math.Atan((b / c)// * (Math.PI / 180)//b / c)// * (Math.PI / 180)
                );
            double u = (Math.PI - Math.Atan((b / c))) * (Math.PI / 180);
            double r = (p) / (1 + e * Math.Cos(teta));
            aroundPiatanpi2 = 2 * (p * (-1 * u * Math.Log(1 + e * Math.Cos(teta), Math.E) - e * u * Math.Cos(teta) - 0.5 * Math.Sin(teta) + (e / 2) * Math.Pow(Math.Log(1 + e * Math.Cos(teta), Math.E), 2)) + 0.5 * Math.Pow(r, 2)) + u;


            teta = Math.PI / 2;

            u = (Math.PI / 2) * (180 / Math.PI);

            r = (p) / (1 + e * Math.Cos(teta));
            aroundPiPer2 = 2 * (p * (-1 * u * Math.Log(1 + e * Math.Cos(teta), Math.E) - e * u * Math.Cos(teta) - 0.5 * Math.Sin(teta) + (e / 2) * Math.Pow(Math.Log(1 + e * Math.Cos(teta), Math.E), 2)) + 0.5 * Math.Pow(r, 2)) + u;


            u = 0;
            teta = 0;// * (180 / Math.PI);
            r = (p) / (1 + e * Math.Cos(teta));
            around0 = 2 * (p * (-1 * u * Math.Log(1 + e * Math.Cos(teta), Math.E) - e * u * Math.Cos(teta) - 0.5 * Math.Sin(teta) + (e / 2) * Math.Pow(Math.Log(1 + e * Math.Cos(teta), Math.E), 2)) + 0.5 * Math.Pow(r, 2)) + u;
            //Aaround = 4 * (Math.Abs(Math.Sqrt(Math.Abs(aroundPiatanpi2)) - Math.Sqrt(Math.Abs(aroundPiPer2))) + Math.Abs(Math.Sqrt(Math.Abs(aroundPiPer2)) - Math.Sqrt(Math.Abs(around0))));
            //Aaround = 4 * (Math.Abs(Math.Sqrt(Math.Abs(aroundPiatanpi2)) - Math.Sqrt(Math.Abs(around0))));

            //Aaround = 4 * (Math.Abs(Math.Sqrt(Math.Abs(aroundPiatanpi2 - around0))));
            Aaround = 4 * (Math.Abs(Math.Sqrt(Math.Abs(aroundPiatanpi2 - aroundPiPer2))) + Math.Abs(Math.Sqrt(Math.Abs(aroundPiPer2 - around0))));
            //Aaround = 4 * (Math.Sqrt(Math.Abs(Math.Abs(aroundPiatanpi2 - aroundPiPer2))) + Math.Abs(Math.Abs(Math.Abs(aroundPiPer2 - around0))));
            //Aaround = 4 * (Math.Sqrt(Math.Abs(aroundPiatanpi2 - around0)));
            /*  //Second Integral priciple first parameters 
              Laround1 = (2 * p) * (-1 * teta * Math.Log(1 + e * Math.Cos(teta), Math.E) - Math.E * teta * Math.Cos(teta) - 0.5 * Math.Sin(teta) + (Math.E / 2) * Math.Pow(Math.Log(1 + e * Math.Cos(teta), Math.E), 2)) + 0.5 * Math.Pow(r, 2) + teta;
              //Second Integral priciple second parameters 
              u = Math.PI - Math.Atan(b / c);
              teta = u;// * (180 / Math.PI);
              r = (p) / (1 + e * Math.Cos(u));
                 Laround0 = (2 * p) * (-1 * teta * Math.Log(1 + e * Math.Cos(teta), Math.E) - Math.E * teta * Math.Cos(teta) - 0.5 * Math.Sin(teta) + (Math.E / 2) * Math.Pow(Math.Log(1 + e * Math.Cos(teta), Math.E), 2)) + 0.5 * Math.Pow(r, 2) + teta;
                u = Math.PI - Math.Atan(b / c);
              teta = u;// * (180 / Math.PI);
              r = (p) / (1 + e * Math.Cos(u));

              //Second Integral priciple first parameters 
              Paround1 = (2 * p) * (-1 * teta * Math.Log(1 + e * Math.Cos(teta), Math.E) - Math.E * teta * Math.Cos(teta) - 0.5 * Math.Sin(teta) + (Math.E / 2) * Math.Pow(Math.Log(1 + e * Math.Cos(teta), Math.E), 2)) + 0.5 * Math.Pow(r, 2) + teta;
              u = 0;
              teta = u;// * (180 / Math.PI);
              r = (p) / (1 + e * Math.Cos(u));
              //Second Integral priciple first parameters 
              Paround0 = (2 * p) * (-1 * teta * Math.Log(1 + e * Math.Cos(teta), Math.E) - Math.E * teta * Math.Cos(teta) - 0.5 * Math.Sin(teta) + (Math.E / 2) * Math.Pow(Math.Log(1 + e * Math.Cos(teta), Math.E), 2)) + 0.5 * Math.Pow(r, 2) + teta;

              Paround = 4 * (Math.Sqrt(Math.Abs(Paround1)) - Math.Sqrt(Math.Abs(Paround0)));
              around = Paround + Laround;
              aroundP2 = 4 * (Math.Sqrt(Math.Abs(Laround1)) - Math.Sqrt(Math.Abs(Paround0)));
              ;
          */
        }
        public double aroundAccess
        {
            get => around;
            set => around = value;
        }
        public double AaroundAccess
        {
            get => Aaround;
            set => Aaround = value;
        }
        public double aroundP2Access
        {
            get => aroundP2;
            set => aroundP2 = value;
        }

    }
}
