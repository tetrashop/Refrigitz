using System;


namespace Ellipse
{
    internal class EllipseDifferentialAroundCalculating
    {
        private double around = 0;
        private double aroundOiler = 0;
        private double aroundcal = 0;
        //double Paround = 0;
        public EllipseDifferentialAroundCalculating(double a, double b, double c)
        {
            //Initiate Ellipse Paramenters.

            double e = (double)System.Math.Sqrt(Math.Pow(a, 2) - Math.Pow(b, 2)) / a;
            double p = 2 * Math.PI * a;

            double dx = 0;

            //double dx =System.(double.MinValue);
            double aa = 30;
            //Found
            for (int i = 1; i < aa; i++)
            {
                dx += ((Math.Pow((LearningMachine.HermitInterpolation.Factorial(2 * i)), 2) / (Math.Pow(Math.Pow(2, i) * (LearningMachine.HermitInterpolation.Factorial(i)), 4))) * (Math.Pow(e, 2 * i) / (2 * i - 1)));
            }
            //Multiply for orthogonality.
            around = p * (1 - dx);
            aroundOiler = Math.PI * (Math.Sqrt(2 * (Math.Pow(a, 2) + Math.Pow(b, 2))));


            double dteta = 0.000001;

            p = Math.Pow(b, 2) / a;
            e = System.Math.Sqrt(1.0 - (p / a));
            c = e * a;
            for (double teta = 0; teta < Math.PI - Math.Atan(b / c);teta+=dteta)
            {
                double dr = (p * e * Math.Sin(teta) / Math.Pow(1 + e * Math.Cos(teta), 2)) * dteta;

                aroundcal += Math.Sqrt(((Math.Pow(p / (1 + e * Math.Cos(teta)), 2) * dteta * dteta) + (dr * dr)));

            }
            aroundcal = 4 * (aroundcal);
            /* double h = Math.PI * (Math.Pow(a - b, 2) / Math.Pow(a + b, 2));
             double Pp = Math.PI * (a + b);
             dx = 0;

             for (int i = 1; i < aa; i++)
             {
                 dx += ((Math.Pow(LearningMachine.HermitInterpolation.Factorial(2 * i), 2) / (Math.Pow(Math.Pow(2, i) * LearningMachine.HermitInterpolation.Factorial(i), 4))) * (Math.Pow(e, 2 * i) / (2 * i - 1)));
             }*/
        }
       
        public EllipseDifferentialAroundCalculating(double a, double b)
        {
            //Initiate Ellipse Paramenters.

            double p = 2 * Math.PI * Math.Sqrt((Math.Pow(a, 2) + Math.Pow(b, 2)) / 2);

            around = p;
            aroundOiler = Math.PI * (Math.Sqrt(2 * (Math.Pow(a, 2) + Math.Pow(b, 2))));
            /* double h = Math.PI * (Math.Pow(a - b, 2) / Math.Pow(a + b, 2));
                double Pp = Math.PI * (a + b);
                dx = 0;

                for (int i = 1; i < aa; i++)
                {
                    dx += ((Math.Pow(LearningMachine.HermitInterpolation.Factorial(2 * i), 2) / (Math.Pow(Math.Pow(2, i) * LearningMachine.HermitInterpolation.Factorial(i), 4))) * (Math.Pow(e, 2 * i) / (2 * i - 1)));
                }*/
        }
        public double aroundAccess
        {
            get => around;
            set => around = value;
        }
        public double aroundcalAccess
        {
            get => aroundcal;
            set => aroundcal = value;
        }
        public double aroundOilerAccess
        {
            get => aroundOiler;
            set => aroundOiler = value;
        }
    }
}
