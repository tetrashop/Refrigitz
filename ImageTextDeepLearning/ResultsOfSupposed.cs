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
        static List< List<string>> Strlogic = new List<List<string>>();
        static List<List<int[]>> StrlogicIndex = new List<List<int[]>>();
        public static bool MindedIsVerb(List<string> te, List<List<string>> telo)
        {
            bool Is = false;
            Strlogic.Clear();
            mined.Clear();
            StrlogicIndex.Clear();

            for (int p = 0; p < telo.Count; p++)
            {
                for (int q = 0; q < telo[p].Count; q++)
                {
                    MindedIsVerb(te, telo, true, true, false, false, p, q, -1, -1);

                    for (int r = 0; r < telo.Count; r++)
                    {
                        if (p == r)
                            continue;
                        MindedIsVerb(te, telo, true, true, true, false, p, q, r, -1);

                        for (int s = 0; s < telo[r].Count; s++)
                        {
                            if (s == q)
                                continue;

                            MindedIsVerb(te, telo, true, true, true, true, p, q, r, s);

                        }
                    }
                }
            }
            for (int p = 0; p < Strlogic.Count; p++)
            {
                for (int q = 0; q < Strlogic[p].Count; q++)
                {

                    if (Strlogic[p][q] == "q")
                    {
                        string s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][1]] + "! \r\n";
                        mined.Add(s);
                    }

                    if (Strlogic[p][q] == "p->r")
                    {
                        string s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][0]] + " " + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][2]] + " است. \r\n";
                        mined.Add(s);
                    }

                    if (Strlogic[p][q] == "~p")
                    {
                        string s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][0]] + " نیست. \r\n";
                        mined.Add(s);
                    }

                    if (Strlogic[p][q] == "p&&q")
                    {
                        string s = " نیست" + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][0]] + " نباشد که " + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][1]] + " نیست. \r\n";
                        mined.Add(s);
                    }

                    if (Strlogic[p][q] == "S")
                    {
                        string s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][0]] + "! \r\n";
                        mined.Add(s);
                    }

                    if (Strlogic[p][q] == "p->q")
                    {
                        string s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][0]] + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][1]] + " است. \r\n";
                    }

                    if (Strlogic[p][q] == "p")
                    {
                        string s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][0]] + "! \r\n";
                    }

                    if (Strlogic[p][q] == "r")
                    {
                        string s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][2]] + "! \r\n";
                        mined.Add(s);
                    }

                    if (Strlogic[p][q] == "p||q->r")
                    {
                        string s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][0]] + " یا " + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][1]] + " " + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][2]] + " است. \r\n";
                        mined.Add(s);
                    }

                    if (Strlogic[p][q] == "q||s")
                    {
                        string s = "";
                        if (StrlogicIndex[p][q][2] != -1)
                            s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][1]] + " یا " + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][2]] + "! \r\n";
                        else
                            s = telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][1]] + "! \r\n";
                        mined.Add(s);
                    }

                    if (Strlogic[p][q] == "!p||!q")
                    {
                        string s = "نه " + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][0]] + " نه " + telo[StrlogicIndex[p][q][0]][StrlogicIndex[p][q][1]] + "! \r\n";
                        mined.Add(s);
                    }

                }

            }

            return Is;
        }
        public static void MindedIsVerb(List<string> te, List<List<string>> telo, bool p, bool q, bool r, bool s, int pp, int qq, int rr, int ss)
        {
            string se = "";
            int[] n = new int[4];
            n[0] = pp;
            n[1] = qq;
            n[2] = rr;
            n[3] = ss;
            Strlogic.Add(new List<string>());
            StrlogicIndex.Add(new List<int[]>());

            PandPgivesQIsQ(p, q, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            PgivesQandQgivesRIsPgivesR(p, q, r, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            PgivesQandnotQIsnotP(p, q, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            PQIsPANDQ(p, q, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            Contradiction(p, q, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            ContradictionSample(p, q, r, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            /*PandQ(p, q, ref se);
            Strlogic[Strlogic.Count-1].Add(se);
                 StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
      se = "";*/

            /*PorQandnotPgivesQ(p, q, ref se);
            Strlogic[Strlogic.Count-1].Add(se);
     StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
      se = "";*/

            PQandPgivesQgivesRisR(p, q, r, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            PgivesQandQgivesRisPOrQgivesR(p, q, r, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            PgivesQandRgivesSandPorRIsQorS(p, q, r, s, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";

            PgivesQandRgivesSandnotPornotRIsnotPornotQ(p, q, r, s, ref se);
            Strlogic[Strlogic.Count - 1].Add(se);
            StrlogicIndex[StrlogicIndex.Count - 1].Add(n);
            se = "";
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
        /*public static bool PandQ(bool p, bool q, ref string Re)
        {
            bool a = (p && q);
            if (a)
            {
                Re = "p";
                return true;
            }
            else
                return false;
        }*/
        /*public static bool PorQandnotPgivesQ(bool p, bool q, ref string Re)
        {
            bool a = (p || q) && (!p);
            if (a)
            {
                Re = "q";
                return true;
            }
            else
                return false;
        }*/
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
            bool a = ((!p) || q) && ((!r) || s) && (p || r);
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
