using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ellipse
{
    class Mathematic
    {
        int Step = 12;
        public static double Sin(double x)
        {
            double Sum = 0;
            double Sign = 1;
            for (int i = 1; i < 12; i += 2)
            {
                Sum += ((Sign * Pow(x, i)) / LearningMachine.HermitInterpolation.Factorial(i));
                Sign *= -1;
            }
            return Sum;
        }
        public static double Cos(double x)
        {
            double Sum = 0;
            double Sign = -1;
            for (int i = 0; i < 12; i += 2)
            {

                Sum += ((Sign * Pow(x, i)) / LearningMachine.HermitInterpolation.Factorial(i));
                Sign *= -1;
            }
            return Sum;
        }
        public static double Pow(double a,int po)
        {
            double sum = 1;
            for (int i = 0; i < po; i++)
                sum *= a;
            return sum;

        }
    }
}
