/***********************************************************************************
 * Ramin Edjlal*********************************************************************
 CopyRighted 1398/0802**************************************************************
 TetraShop.Ir***********************************************************************
 ***********************************************************************************/
using ContourAnalysisDemo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
namespace ImageTextDeepLearning
{
    //detection of literal class
    internal class DetectionOfLitteral
    {
        //initiate global vars
        private readonly int Width = 10, Heigh = 10;
        private readonly double Threashold = 0.01;
        public AllKeyboardOfWorld t = null;
        public ConjunctedShape tt = null;
        //AllKeyLocation
        public List<string> Detected = new List<string>();
        private readonly MainForm dd = null;
        public AllKeyboardOfWorld ConjunctedShapeListRequired = null;
        //Constructor
        public DetectionOfLitteral(ref ImageTextDeepLearning.FormImageTextDeepLearning This, MainForm d)
        {
            try
            {
                dd = d;
                //This.SetCallSetLablr("Initiate All Key...");
                //This.RefCallSetLablr();
                t = new AllKeyboardOfWorld();
                t.ConvertAllStringToImage(d);
                //t.ConvertAllTempageToMatrix(t.KeyboardAllImage);
                //This.SetCallSetLablr("Initiate Conjunction...");
                //This.RefCallSetLablr();

                tt = new ConjunctedShape(d);
                //This.SetCallSetLablr("Cretion Conjuncted untile Mattix...");
                //This.RefCallSetLablr();
                tt.CreateSAhapeFromConjucted(Width, Heigh);
                //This.SetCallSetLablr("Initiate...");
                //This.RefCallSetLablr();
                ConjunctedShapeListRequired = new AllKeyboardOfWorld();
                //This.SetCallSetLablr("Initiate For Key Matrix...");
                //This.RefCallSetLablr();

                ConjunctedShapeListRequired.ConvertAllTempageToMatrix(tt.AllImage);
                //This.SetCallSetLablr("Detection...");
                //This.RefCallSetLablr();

                Detection(Width, Heigh);
            }
            catch (Exception te)
            {
                System.Windows.Forms.MessageBox.Show("Fatual Error!" + te.ToString());
            }
            finally
            {
                System.Windows.Forms.MessageBox.Show("Completed Detetcted " + Detected.Count);
            }
        }

        //Detection main similarity method
        private int DifferentBool(bool[,] Key, bool[,] Src, int Wi, int Hei,bool Ach)
        {
            int Dif = 0;
            if (Wi != Hei)
            {
                Dif = 0;
            }

            try
            {
                // Dif = LearningMachine.Interpolate.SimilarityC(Key, Src, Wi, Hei)//* (int)LearningMachine.Interpolate.SimilarityB(Key, Src, Wi, Hei)
                if (ContourAnalysisNS.GraphS.GraphSameRikht(Key, Src, Wi, Hei,Ach))
                {
                    Dif = Wi * Hei;
                }
                //(new FormImageTextDeepLearning()).GraphsDrawn(ContourAnalysisNS.GraphS.Z.A, ContourAnalysisNS.GraphS.Z.B);
                //MessageBox.Show("Next!");
            }
            catch (Exception t) { 
                MessageBox.Show(t.ToString()); 
                return 0; 
            }
            return Dif;
        }

        private bool IssampleallFalse(bool[,] A, int w, int h)
        {
            bool Is = true;
            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    Is = Is && (!(A[i, j]));
                }
            }
            return Is;
        }
        //detection main method
        public bool Detection(int Wi, int Hei)
        {
            try
            {
                //clear list and initate...
                Detected.Clear();
                bool Ach = true;
                //for evey conjuncted shape retrived matrix items
              for (int i = 0; i < ConjunctedShapeListRequired.KeyboardAllConjunctionMatrix.Count; i++)                  
                {

                    Ach = false;
                    //initate
                    StringBuilder TempDetected = new StringBuilder();

                    int IndecCurrent = -1;
                    int KeyBoardDif = 0;
                    //for evey all keyboard able to char matrix of conjunction
                    int KeyDif = 0;
                    for (int k = 0; k < t.KeyboardAllConjunctionMatrix.Count; k++)
                    {
                        bool a = IssampleallFalse(ConjunctedShapeListRequired.KeyboardAllConjunctionMatrix[i], Wi, Hei);
                        bool b = IssampleallFalse(t.KeyboardAllConjunctionMatrix[k], Wi, Hei);
                        //retrive similarity value
                        if ((!a) && (!b))
                        {
                            KeyDif = DifferentBool(t.KeyboardAllConjunctionMatrix[k], ConjunctedShapeListRequired.KeyboardAllConjunctionMatrix[i], Wi, Hei, Ach);
                        }
                        else
                            if ((!a))
                        {  //double KeyDif = Colleralation.GetCorrelationScore(ConjunctedShapeListRequired.KeyboardAllConjunctionMatrix[i], t.KeyboardAllConjunctionMatrix[k], Width);
                           //when is ready and proper
                           //if (System.Math.Abs(1- KeyDif ) < Threashold)
                            if (t.KeyboardAllStringsWithfont[k] != " ")
                            {
                                Ach = true;

                                continue;
                            }
                            else
                            {
                                IndecCurrent = k;
                                KeyDif = Width * Heigh;
                                Ach = true;

                                break;
                            }
                            /* if (IssampleallFalse(ConjunctedShapeListRequired.KeyboardAllConjunctionMatrix[i], Wi, Hei))
                             {
                                 if (t.KeyboardAllStringsWithfont[k] != " ")
                                     continue;
                             }
                             else
                               if (IssampleallFalse(t.KeyboardAllConjunctionMatrix[k], Wi, Hei))
                                 continue;*/
                        }
                        Ach = true;

                        if (KeyDif > KeyBoardDif)
                        {
                            //set
                            IndecCurrent = k;
                            KeyBoardDif = KeyDif;
                            break;
                            //if (KeyDif >= Width * Heigh)
                            //break;

                        }
                    }
                    //when found
                    if (IndecCurrent > -1)
                    {
                        TempDetected.Append(t.KeyboardAllStringsWithfont[IndecCurrent]);
                        //Detected.Add(TempDetected.ToString());
                        Detected.Add(t.KeyboardAllStringsWithfont[IndecCurrent]);
                    }
                    ///else
                    // return false;
                    //Add created items string to list

                }

            }
            catch (Exception t)
            {
                MessageBox.Show(t.ToString());
                //when exist exeption return unsuccessfull 
                return false;
            }
            //when successfull return validity
            return true;
        }
    }
}
