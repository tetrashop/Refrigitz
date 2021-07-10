/******************************************************************************
 * Ramin Edjlal Copyrights 2015.************************************************
 * Learning Autamata.**********************************************************
 * The every sum of probability is one.****************************************(*_)
 * four formula .tow for Regard regime and tow for penalty regime.***************(-)
 * Derived Quantum Automata Penalty All Objects of Derived Automata************(-)
 * Malfunction Reward and Penalty Detection**********************************(_*)
 * Penalty Reward Action Failure************************************************(*_)
 * Mistuning of Penalty and Regard Data in IsRegard and IsPenalty Values*******(+)
 * No Reason For Malfunction of Reward and Penalty Mechanism******************(_)
 * 1395/1/2********************************************************************(*:Sum(3)) (_:Sum(4)) (-:Sum(2)) (All Errors:(7))
 * Penalty Regard Action is Useful For One Time Per AllDraw Object.************
 * No Solution to Overcome to static Behavior Of Quantum Variables Inhererete.*
 ******************************************************************************/


using System;

namespace RefrigtzW
{
    [Serializable]
    public class LearningKrinskyAtamata
    {
        private int r = 100;
        private int m = 100;
        private int k = 100;
        public double[] alpha;
        private bool beta = true;
        private double[] fi;
        public bool IsReward = false;
        public bool IsPenalty = false;
        protected double Reward;
        protected double Penalty;
        protected int Success = 0, Failer = 0;
        protected int State = 0;
        //int State = 1;
        public void Initiate()
        {
            object o = new object();
            lock (o)
            {
                IsPenalty = false;
                IsReward = false;
            }
        }
        public LearningKrinskyAtamata(int r0, int m0, int k0)
        {
            object o = new object();
            lock (o)
            {
                IsReward = new bool();
                IsPenalty = new bool();
                IsReward = false;
                IsPenalty = false;
                Success = new int();
                State = new int();
                beta = new bool();
                beta = true;
                Reward = new double();
                Penalty = new double();
                r = new int();
                m = new int();
                k = new int();


                if (r0 >= m0)
                {
                    r = r0;
                    m = m0;
                    k = k0;
                    alpha = new double[r];
                    fi = new double[k];
                    fi = new double[r];
                    for (int i = 0; i < r; i++)
                    {
                        alpha[i] = 1.0 / r;
                    }

                    for (int i = 0; i < k; i++)
                    {
                        fi[i] = 1.0 / k;
                    }

                    //Reward[i] = (double)(new Random()).Next(0, 100000) / 100000.0;
                    Reward = 1.0 / r;
                    //Penalty[i] = (double)(new Random()).Next(0, 100000) / 100000.0;
                    Penalty = 1.0 / r;
                }
            }
        }
        public void Clone(ref QuantumAtamata AA)
        {
            object o = new object();
            lock (o)
            {
                AA.r = r;
                AA.m = m;
                AA.k = k;
                alpha = new double[AA.r];
                for (int i = 0; i < AA.r; i++)
                {
                    AA.alpha[i] = alpha[i];
                }

                AA.beta = beta;
                AA.Failer = Failer;
                fi = new double[AA.k];
                for (int i = 0; i < AA.k; i++)
                {
                    AA.fi[i] = fi[i];
                }

                AA.IsPenalty = IsPenalty;
                AA.IsReward = IsReward;
                AA.Reward = Reward;
                AA.Penalty = Penalty;
                AA.Success = Success;
                AA.Failer = Failer;
                AA.State = State;
            }
        }

        public void FailureState()
        {
            object o = new object();
            lock (o)
            {
                Failer++;
                if (Success < Failer && State < r - 1)
                {
                    State++;
                }
                else if (State > 0)
                {
                    State--;
                }
            }
        }
        public void SuccessState()
        {
            object o = new object();
            lock (o)
            {
                Success++;
                if (Success > Failer && State < r - 1)
                {
                    State++;
                }
                else if (State > 0)
                {
                    State--;
                }
            }
        }
        public int IsSecondDerivitionIsPositive()
        {
            object o = new object();
            lock (o)
            {
                for (int i = 0; i < r - 2; i++)
                {
                    if (((alpha[i + 2] - 2 * alpha[i + 1] + alpha[i]) / (1.0 / r)) < 0)
                    {
                        return -1;
                    }
                }
                return 1;
            }
        }
        public double LearningAlgorithmRegard()
        {
            object o = new object();
            lock (o)
            {
                SuccessState();
                IsReward = true;
                IsPenalty = false;
                alpha[State] += Reward * (1 - alpha[State]);
                for (int i = 0; i < r; i++)
                {
                    if (i != State)
                    {
                        alpha[i] -= Reward * alpha[i];
                    }
                }

                beta = false;
                return alpha[State];
            }
        }
        public int IsRewardAction()
        {
            object o = new object();
            lock (o)
            {
                if (IsReward)
                {
                    return 1;
                }

                return -1;
            }
        }

        public double IsPenaltyAction()
        {
            object o = new object();
            lock (o)
            {
                if (IsPenalty)
                {
                    return 0;
                }

                return -1;
            }
        }
        public double LearningAlgorithmPenalty()
        {
            object o = new object();
            lock (o)
            {
                FailureState();
                IsPenalty = true;
                IsReward = false;
                alpha[State] -= Penalty * alpha[State];
                for (int i = 0; i < r; i++)
                {
                    if (i != State)
                    {
                        alpha[i] -= Penalty * alpha[i];
                        alpha[i] += (Penalty / (r - 1));
                    }
                }

                beta = true;
                return alpha[State];
            }
        }
    }
}
