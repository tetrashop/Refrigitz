using System;

namespace Ellipse
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //I/O of Parameters.
            double a, b, c;
            Console.WriteLine("Enter a:");
            a = (double)System.Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Enter b:");
            b = (double)System.Convert.ToDouble(Console.ReadLine());
            if (b > a)
            {
                double d = b;
                b = a;
                a = d;
            }
            //Intiate Ellipse Three Paramenters.
            double p = Math.Pow(b, 2) / a;
            double e = (double)System.Math.Sqrt(1 - (p / a));
            c = e * a;

            //Found of Diferentiation of Ellipse.
            EllipseDifferentialAroundCalculating AEDAC = new EllipseDifferentialAroundCalculating(a, b, c);
            //Found of Stmuluse of Ellipse.
            EllipseDifferentialAroundCalculating AEDACP = new EllipseDifferentialAroundCalculating(a, b);
            //Found of paper menthod new startegy.
            EllipsAroundFormula AEAF = new EllipsAroundFormula(a, b, c);
            //I/O Paramenters of output.
            /* Console.WriteLine("The Differential is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEDACP.aroundAccess - AEAF.AaroundAccess);
             Console.WriteLine("The Differential is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEDACP.aroundOilerAccess - AEAF.AaroundAccess);
             Console.WriteLine("The Differential is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEDAC.aroundAccess - AEAF.aroundP2Access);
             Console.WriteLine("The Differential is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEDACP.aroundAccess - AEAF.aroundP2Access);
             Console.WriteLine("The Differential is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEDAC.aroundAccess - AEAF.aroundAccess);


             Console.WriteLine("The Actual First is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEAF.AaroundAccess);
             Console.WriteLine("The Actual Second is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEAF.aroundP2Access);
             Console.WriteLine("The Actual Third is :");
             Console.WriteLine(AEAF.aroundAccess);
             Console.WriteLine("The Apoximate First is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEDAC.aroundAccess);
             Console.WriteLine("The Apoximate Second is :");
             //Should be of minuse to zero boundry
             Console.WriteLine(AEDACP.aroundAccess);
             //Wait.
             Console.ReadLine();
             */

            Console.WriteLine("The Differential is :(between desired and one common limit) : ");
            //Should be of minuse to zero boundry
            Console.WriteLine(AEDACP.aroundOilerAccess - AEAF.AaroundAccess);
            Console.WriteLine("The Differential is :(between desired and Oilder limit ) : ");
            //Should be of minuse to zero boundry
            Console.WriteLine(AEDAC.aroundOilerAccess - AEAF.AaroundAccess);
            Console.WriteLine("The Differential is (between desired cal and deferential) : ");
            //Should be of minuse to zero boundry
            Console.WriteLine(AEDAC.aroundcalAccess - AEAF.AaroundAccess);


            Console.WriteLine("The Actual First is (desired) : ");
            //Should be of minuse to zero boundry
            Console.WriteLine(AEAF.AaroundAccess);
            Console.WriteLine("The Actual Second is (Oiler) : ");
            //Should be of minuse to zero boundry
            Console.WriteLine(AEDAC.aroundOilerAccess);
            Console.WriteLine("The Actual Third is (one limit) : ");
            Console.WriteLine(AEDACP.aroundOilerAccess);
            Console.WriteLine("The Apoximate is (desired calculation) :");
            //Should be of minuse to zero boundry
            Console.WriteLine(AEDAC.aroundcalAccess);
             Console.ReadLine();

        }
    }
}
