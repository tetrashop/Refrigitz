/***********************************************************************************
 * Ramin Edjlal*********************************************************************
 CopyRighted 1398/0802**************************************************************
 TetraShop.Ir***********************************************************************
 ***********************************************************************************/
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using ImageTextDeepLearning;
using GalleryStudio;
using ContourAnalysisNS;
using ContourAnalysisDemo;

namespace Refrigtz
{
    [Serializable]
    //Main class od serialization
    class TakeRoot
    {
        //Constructed
        public TakeRoot() { }
        //Load 
        public AllKeyboardOfWorld Load(String AllKeyboardOfWorldKindString)
        {
            //Create deserilized constructor object
            AllKeyboardOfWorldMemmoty tr = new AllKeyboardOfWorldMemmoty();

            bool DrawDrawen = false;
            //Load Middle Targets.
            try
            {
                //when file exist
                if (File.Exists(AllKeyboardOfWorldKindString))
                {

                    //load
                    tr.Load(AllKeyboardOfWorldKindString);
                    //when successfull
                    if (tr.Current != null)
                    {
                        //prompt
                        DrawDrawen = true;
                        System.Windows.Forms.MessageBox.Show("Load Completed.");
                    }
                    //delete file
                    File.Delete(AllKeyboardOfWorldKindString);
                }
            }
            catch (Exception t) { }
            return tr.Current;
        }
        //save main file
        public bool Save(AllKeyboardOfWorld Curent, String AllKeyboardOfWorldKindString
            )
        {

            try
            {
                //when there is not file
                if (!File.Exists(AllKeyboardOfWorldKindString))
                {
                    //Create constructor object
                    AllKeyboardOfWorldMemmoty rt = new AllKeyboardOfWorldMemmoty();
                    //assign main object
                    rt.Current = Curent;
                    //write on disk
                    rt.RewriteAllKeyboardOfWorld(AllKeyboardOfWorldKindString);
                }
                else//when there is
                      if (File.Exists(AllKeyboardOfWorldKindString))
                {
                    //delete existence file on disk
                    File.Delete(AllKeyboardOfWorldKindString);
                    //create constructor object
                    AllKeyboardOfWorldMemmoty rt = new AllKeyboardOfWorldMemmoty();
                    //Assign main object
                    rt.Current = Curent;
                    //write on disk
                    rt.RewriteAllKeyboardOfWorld(AllKeyboardOfWorldKindString);
                }
                //when there is no exeption return successfull
                return true;
            }
            catch (Exception t)
            {
                //when there is exeption return unsuccessfull
                return false;
            }
        }
    }
}
