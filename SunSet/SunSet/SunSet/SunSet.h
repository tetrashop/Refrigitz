
#pragma once
#include <math.h>
#include <string.h>
#include <string>
#include <vector>
static class Pertubsation
{
public:
    static int factrial(int n)
    {
        if (n <= 1)
            return 1;
        return n * factrial(n - 1);
    }
    static int pertubsation(int n, int m)
    {
        if (n < m)
            return 0;
        return factrial(n) * factrial(n - m) / factrial(m);
    }
};
class Mass
{
public:
    Mass()
    {
    }
    int i = 0;
    double M = 0;
    double X = 0, Y = 0, Z = 0;
    Mass(double m, double x, double y, double z, int ii)
    {
        i = ii;
        M = m;
        X = x;
        Y = y;
        Z = z;
    }
};
class MassPertubation
{
public:
    MassPertubation()
    {
    }
    double M1 = 0, M2 = 0;
    double R = 0;
    double a = 0, b = 0, c = 0;

    MassPertubation(Mass* MM1, Mass* MM2)
    {
        M1 = MM1->M;
        M2 = MM2->M;
        R = sqrt(pow(MM1->X - MM2->X, 2) + pow(MM1->Y - MM2->Y, 2) + pow(MM1->Z - MM2->Z, 2));
        a = MM1->X - MM2->X;
        b = MM1->Y - MM2->Y;
        c = MM1->Z - MM2->Z;
    }

};

using namespace std;
class ResultOfGravitationalForces
{
    //inline Mass* operator[](unsigned a) { return (Mass*)(a * sizeof(Mass)); };

public:
    ResultOfGravitationalForces()
    {
    }
    double* GetResults(double x[], double y[], double z[], double m[], int N)
    {
        double* result = new double[3];
        Mass** masses;
        *masses = new Mass[3];

        for (int i = 0; i < N; i++)
        {
            masses[i] = new Mass(m[i], x[i], y[i], z[i], i);
        }
        int NP = Pertubsation::pertubsation(N, 2);
        if (NP > 0)
        {
            int k = 0;
            MassPertubation** maassespertubation;
            *maassespertubation = new MassPertubation[NP];
            std::vector<int*> ZZ = std::vector<int*>();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (masses[i]->i != masses[j]->i && (!ExistPairInList(i, j, ZZ)))
                    {
                        int* a = new int[2];
                        a[0] = i;
                        a[1] = j;
                        ZZ.push_back(a);
                        maassespertubation[k] = new MassPertubation(masses[i], masses[j]);
                        k++;
                    }
                }
            }
            NP = k;
            double G = 6.75 * pow(10, -11);
            double X = 0, Y = 0, Z = 0;
            for (int i = 0; i < NP; i++)
            {
                X += (maassespertubation[i]->a * G * (maassespertubation[i]->M1 * maassespertubation[i]->M2) / pow(maassespertubation[i]->R, 2));
                Y += (maassespertubation[i]->b * G * (maassespertubation[i]->M1 * maassespertubation[i]->M2) / pow(maassespertubation[i]->R, 2));
                Z += (maassespertubation[i]->c * G * (maassespertubation[i]->M1 * maassespertubation[i]->M2) / pow(maassespertubation[i]->R, 2));
            }
            result[0] = X;
            result[1] = Y;
            result[2] = Z;
        }
        return result;
    }

    static bool ExistPairInList(int i, int j, std::vector<int*> z)
    {
        bool Ex = false;
        for (int f = 0; f <(int) z.size(); f++)
        {
            if (z[f][0] == i && z[f][1] == j)
                return true;
            if (z[f][0] == j && z[f][1] == i)
                return true;
        }
        return Ex;
    }
};
