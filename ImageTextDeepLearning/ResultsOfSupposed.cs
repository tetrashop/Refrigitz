using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContourAnalysisNS
{
    public class ResultsOfSupposed
    {
        public static List<string> mined = new List<string>();
        public static bool MindedIsVerb(List<string> te, List<List<string>> telo)
        {
            bool Is = false;

            for (int p = 0; p < telo.Count; p++)
            {
                for (int q = 0; q < telo.Count; q++)
                {
                    if (p == q)
                        continue;

                    for (int r = 0; r < telo.Count; r++)
                    {
                        if (p == r)
                            continue;
                        if (q == r)
                            continue;

                        for (int s = 0; s < telo.Count; s++)
                        {
                            if (s == p)
                                continue;
                            if (s == q)
                                continue;
                            if (s == r)
                                continue;


                        }
                    }
                }
            }
            return Is;
        }
        public static List<string> MindedIsVerb(List<string> te, List<List<string>> telo,bool p,bool q,bool r,bool s)
        {
            string se = "";
            List<string> Strlogic = new List<string>();
            PandPgivesQIsQ(p, q, ref se);
            Strlogic.Add(se);
            se = "";

            PgivesQandQgivesRIsPgivesR(p, q, r, ref se);
            Strlogic.Add(se);
            se = "";

            PgivesQandnotQIsnotP(p, q, ref se);
            Strlogic.Add(se);
            se = "";

            PQIsPANDQ(p, q, ref se);
            Strlogic.Add(se);
            se = "";

            Contradiction(p, q, ref se);
            Strlogic.Add(se);
            se = "";

            ContradictionSample(p, q, r, ref se);
            Strlogic.Add(se);
            se = "";

            PandQ(p, q, ref se);
            Strlogic.Add(se);
            se = "";

            PorQandnotPgivesQ(p, q, ref se);
            Strlogic.Add(se);
            se = "";

            PQandPgivesQgivesRisR(p, q, r, ref se);
            Strlogic.Add(se);
            se = "";

            PgivesQandQgivesRisPOrQgivesR(p, q, r, ref se);
            Strlogic.Add(se);
            se = "";

            PgivesQandRgivesSandPorRIsQorS(p, q, r, s, ref se);
            Strlogic.Add(se);
            se = "";

            PgivesQandRgivesSandnotPornotRIsnotPornotQ(p, q, r, s, ref se);
            Strlogic.Add(se);
            se = "";

            return Strlogic;

        }
        public static bool PandPgivesQIsQ(bool p, bool q, ref string Re)
        {
            bool s = p && ((!p) || q);
            if (s)
            {
                Re = "q";
                return true;
            }
            else
                return false;
        }
        public static bool PgivesQandQgivesRIsPgivesR(bool p, bool q, bool r, ref string Re)
        {
            bool s = ((!p) || q) && ((!p) || r);
            if (s)
            {
                Re = "p->r";
                return true;
            }
            else
                return false;
        }
        public static bool PgivesQandnotQIsnotP(bool p, bool q, ref string Re)
        {
            bool s = ((!p) || q) && ((!q));
            if (s)
            {
                Re = "~p";
                return true;
            }
            else
                return false;
        }
        public static bool PQIsPANDQ(bool p, bool q, ref string Re)
        {
            bool s = (p) && (q);
            if (s)
            {
                Re = "p&&Q";
                return true;
            }
            else
                return false;
        }
        public static bool Contradiction(bool s, bool f, ref string Re)
        {
            bool a = (s) || (f);
            if (a)
            {
                Re = "S";
                return true;
            }
            else
                return false;
        }
        public static bool ContradictionSample(bool p, bool q, bool f, ref string Re)
        {
            bool a = ((!(p && (!q))) || (f));
            if (a)
            {
                Re = "p->q";
                return true;
            }
            else
                return false;
        }
        public static bool PandQ(bool p, bool q, ref string Re)
        {
            bool a = (p && q);
            if (a)
            {
                Re = "p";
                return true;
            }
            else
                return false;
        }
        public static bool PorQandnotPgivesQ(bool p, bool q, ref string Re)
        {
            bool a = (p || q) && (!p);
            if (a)
            {
                Re = "q";
                return true;
            }
            else
                return false;
        }
        public static bool PQandPgivesQgivesRisR(bool p, bool q, bool r, ref string Re)
        {
            bool a = (p && q) && (((!p) || ((!q) || r)));
            if (a)
            {
                Re = "r";
                return true;
            }
            else
                return false;
        }
        public static bool PgivesQandQgivesRisPOrQgivesR(bool p, bool q, bool r, ref string Re)
        {
            bool a = ((!p) || q) && ((!q) || r);
            if (a)
            {
                Re = "p||q->r";
                return true;
            }
            else
                return false;
        }
        public static bool PgivesQandRgivesSandPorRIsQorS(bool p, bool q, bool r, bool s, ref string Re)
        {
            bool a = ((!p) || q) && ((!r) || s) && (p || q);
            if (a)
            {
                Re = "q||s";
                return true;
            }
            else
                return false;
        }
        public static bool PgivesQandRgivesSandnotPornotRIsnotPornotQ(bool p, bool q, bool r, bool s, ref string Re)
        {
            bool a = ((!p) || q) && ((!r) || s) && ((!q) || (!!s));
            if (a)
            {
                Re = "!p||!q";
                return true;
            }
            else
                return false;
        }
    }
}
