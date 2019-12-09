/***********************************************************************************
 * Ramin Edjlal*********************************************************************
 CopyRighted 1398/0802**************************************************************
 TetraShop.Ir***********************************************************************
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using ContourAnalysisDemo;
namespace ImageTextDeepLearning
{
    //detection of literal class
    class DetectionOfLitteral
    {
        //initiate global vars
        int Width =10, Heigh =10;
        double Threashold = 0.01;
        public AllKeyboardOfWorld t = new AllKeyboardOfWorld();
        public ConjunctedShape tt = null;
        //AllKeyLocation
        public List<String> Detected = new List<String>();

        MainForm dd = null;
        AllKeyboardOfWorld ConjunctedShapeListRequired = null;
        //Constructor
        public DetectionOfLitteral(ref ImageTextDeepLearning.FormImageTextDeepLearning This, MainForm d)
        {
            try
            {
                dd = d;
                //This.SetCallSetLablr("Initiate All Key...");
                //This.RefCallSetLablr();
                t.ConvertAllStringToImage(d);
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
        double DifferentBool(bool[,] Key, bool[,] Src, int Wi, int Hei)
        {
            double Dif = 0;
            if (Wi != Hei)
                Dif = 0;
            try
            {
                Dif = LearningMachine.Interpolate.SimilarityB(Key, Src, Wi);
            }
            catch (Exception t) { return 0; }
            return Dif;
        }
        //detection main method
        public bool Detection(int Wi, int Hei)
        {
            try
            {
                //clear list and initate...
                Detected.Clear();

                bool Do = false;

                //for evey conjuncted shape retrived matrix items
                for (int i = 0; i < ConjunctedShapeListRequired.KeyboardAllConjunctionMatrixList.Count; i++)
                {

                    //initate
                    StringBuilder TempDetected = new StringBuilder();

                    int IndecCurrent = -1;
                    double KeyBoardDif = double.MinValue;
                    //for evey all keyboard able to char matrix of conjunction
                    for (int k = 0; k < t.KeyboardAllConjunctionMatrix.Count; k++)
                    {
                        //retrive similarity value
                        //double KeyDif = DifferentBool(ConjunctedShapeListRequired.KeyboardAllConjunctionMatrixList[i], t.KeyboardAllConjunctionMatrix[k], Wi, Hei);
                        double KeyDif = Colleralation.GetCorrelationScore(ConjunctedShapeListRequired.KeyboardAllConjunctionMatrixList[i], t.KeyboardAllConjunctionMatrix[k], Width);
                        //when is ready and proper
                        //if (System.Math.Abs(1- KeyDif ) < Threashold)
                        if (KeyDif > KeyBoardDif)
                        {
                            //set
                            IndecCurrent = k;
                            KeyBoardDif = KeyDif;
                            if (KeyDif >= Width * Heigh)
                                break;

                        }
                    }
                    //when found
                    if (IndecCurrent > -1)
                    {
                        //set items
                        Do = true;
                        TempDetected.Append(t.KeyboardAllStrings[IndecCurrent]);
                        Detected.Add(TempDetected.ToString());
                    }
                    ///else
                       // return false;
                       //Add created items string to list
                    
                }

            }
            catch (Exception t)
            {
                //when exist exeption return unsuccessfull 
                return false;
            }
            //when successfull return validity
            return true;
        }
     }
}
