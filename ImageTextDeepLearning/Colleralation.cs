/*https://stackoverflow.com/questions/8869006/multiple-dimension-correlation-in-c-sharp*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageTextDeepLearning
{
    class Colleralation
    {
        static double Threshold = 0.2;
        public static int GetCorrelationScore(bool[,] seriesA, bool[,] seriesB,int n)
        {
            int correlationScore = 0;

            for (var i = 0; i < //seriesA.Length
                n; i++)
            {
                bool A = true;
                for (var j = 0; j < n; j++)
                {
                    A = areEqual(System.Convert.ToDouble(seriesA[i, j]), System.Convert.ToDouble(seriesB[i, j]), Threshold
                        );
                    if (A)
                        correlationScore++;
                    else
                        correlationScore--;
                }
            }
            return correlationScore;
        }

        static bool areEqual(double value1, double value2, double allowedVariance)
        {
            var lowValue1 = value1 - allowedVariance;
            var highValue1 = value1 + allowedVariance;

            return (lowValue1 < value2 && highValue1 > value2);
        }


    }
}