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
using System.IO;
using System.Drawing;
using ContourAnalysisDemo;
namespace ImageTextDeepLearning
{
    //To Store All Keyboard literals
    [Serializable]
    class AllKeyboardOfWorld
    {
        public static List<string> fonts = new List<string>();

        public AllKeyboardOfWorld()
        {
            if (fonts.Count == 0)
            {
                fonts.Clear();
                bool Do = ListAllFonts();
                if (!Do)
                    fonts.Clear();
            }

        }

    //Initiate global vars
    int Width =10, Height =10;
        public List<String> KeyboardAllStrings = new List<String>();
        public List<Image> KeyboardAllImage = new List<Image>();
        public List<bool[,]> KeyboardAllConjunctionMatrix = new List<bool[,]>();
        public List<bool[,]> KeyboardAllConjunctionMatrixList = new List<bool[,]>();
        //Crate all able chars on List indevidully
        public bool CreateString()
        {
            //when not existence
            if (KeyboardAllStrings.Count == 0)
            {
                //clear
                KeyboardAllStrings.Clear();
                try
                {
                    //for all possible
                    for (int i = 0; i < char.MaxValue; i++)
                    {
                        //get type of current
                        Type t = ((char)i).GetType();
                        //when is char and visible and is serializable
                        if (t.Equals(typeof(char)) && t.IsVisible && t.IsSerializable)
                        {
                            //if (((char)i).ToString().Contains("\\u"))
                            //continue;
                            //when existemnce of this conditions continue
                            int ch = i;
                            if ((ch >= 0x0020 && ch <= 0xD7FF) ||
                                    (ch >= 0xE000 && ch <= 0xFFFD) ||
                                    ch == 0x0009 ||
                                    ch == 0x000A ||
                                    ch == 0x000D)
                            {
                                //sdetermine and Store
                                if (!KeyboardAllStrings.Contains(((char)i).ToString()))
                                    KeyboardAllStrings.Add(((char)i).ToString());
                            }
                        }

                    }

                }
                catch (Exception t)
                {
                    return false;
                }
            }
            return true;
        }
        bool ListAllFonts()
        {
            try
            {
                foreach (FontFamily font in System.Drawing.FontFamily.Families)
                {
                    fonts.Add(font.Name);
                }
            }
            catch (Exception t) { return false; }
            return true;
        }
        //Savle all
        bool SaveAll()
        {
            try
            {
                //when file dos not exist
                if (!File.Exists("KeyboardAllStrings.asd"))
                {

                    /*   lock (KeyboardAllStrings)
                       {
                           for (int i = 0; i < KeyboardAllStrings.Count; i++)
                           {
                               File.AppendAllText("KeyboardAllStrings.asd", KeyboardAllStrings[i]);
                           }
                       }*/
                       //serialized on take root
                    if (this.KeyboardAllImage.Count > 0)
                    {
                        Refrigtz.TakeRoot t = new Refrigtz.TakeRoot();
                        t.Save(this, "KeyboardAllStrings.asd");

                    }
                }
                else {//delete and serilized take root
                    File.Delete("KeyboardAllStrings.asd");
                    if (this.KeyboardAllImage.Count > 0)
                    {
                        Refrigtz.TakeRoot t = new Refrigtz.TakeRoot();
                        t.Save(this, "KeyboardAllStrings.asd");

                    }
                }

            }

            catch (Exception t)
            {
                //System.Windows.Forms.MessageBox.Show("Fatual Error!" + t.ToString()); return false;
            }
            return true;
        }
        //read all
        bool ReadAll()
        {
            try
            {
                //when existence
                if (File.Exists("KeyboardAllStrings.asd"))
                {
                    //clear
                    KeyboardAllStrings.Clear();
                    KeyboardAllImage.Clear();
                    KeyboardAllConjunctionMatrix.Clear();


                    /* String Tem = File.ReadAllText("KeyboardAllStrings.asd");
                     if (Tem.Length > 0)
                     {
                         for (int i = 0; i < Tem.Length; i++)
                         {
                             KeyboardAllStrings.Add(Tem[i].ToString());
                         }
                     }
                     else
                     {
                         bool Do = CreateString();
                         if (!Do)
                             return false;
                     }*/
                     //serilized
                    Refrigtz.TakeRoot tr = new Refrigtz.TakeRoot();
                    AllKeyboardOfWorld t = tr.Load("KeyboardAllStrings.asd");
                    this.KeyboardAllConjunctionMatrix = t.KeyboardAllConjunctionMatrix;
                    this.KeyboardAllConjunctionMatrixList = t.KeyboardAllConjunctionMatrixList;
                    this.KeyboardAllImage = t.KeyboardAllImage;
                    this.KeyboardAllStrings = t.KeyboardAllStrings;

                }
                else//others retiurn unsuccessfull
                    return false;
            }
            catch (Exception t) {
                //when unsuccessfull return false
                return false;
            }
            //return true
            return true;
        }
        //Founf Min of Y
        int MinY(Bitmap Im)
        {
            int Mi = 0;
            int j = 0;
            for (int k = 0; k < Im.Height; k++)
            {
                if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                {
                    Mi = k;
                    break;
                }
            }
            return Mi;

        }
        //Cropping and fitting image
        Bitmap cropImage(Bitmap img, Rectangle cropArea)
        {
               int X = cropArea.X;
            int Y = cropArea.Y;
            int XX = cropArea.Width;
            int YY = cropArea.Height;




            Bitmap bmp = new Bitmap(Width, Height);
            using (Graphics gph = Graphics.FromImage(bmp))
            {
                gph.DrawImage(img, new Rectangle(0, 0, Width, Height), new Rectangle(X, Y, XX, YY), GraphicsUnit.Pixel);
            }
            return bmp;
        }
        //Found of Min of X
        int MinX(Bitmap Im)
        {
            int Mi = 0;
            int k = 0;
            for (int j = 0; j < Im.Width; j++)
            {
                if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                {
                    Mi = j;
                    break;
                }
            }
            return Mi;

        }
        //Found of Max Of Y
        int MaxY(Bitmap Im)
        {
            int Ma = 0;
            int j = 0;
            for (int k = Im.Height-1; k >= 0; k--)
            {
                if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                {
                    Ma = k;
                    break;
                }
            }
            return Ma;

        }
        //Found of Max of X
        int MaxX(Bitmap Im)
        {
            int Ma = 0;
            int k = 0;
            for (int j = Im.Width-1; j >= 0; j--)
            {
                if (!(Im.GetPixel(j, k).A == 255 && Im.GetPixel(j, k).R == 255 && Im.GetPixel(j, k).B == 255 && Im.GetPixel(j, k).G == 255))
                {
                    Ma = j;
                    break;
                }
            }
            return Ma;

        }
        //store all strings list to proper  images themselves list
        public bool ConvertAllStringToImage(MainForm d)
        {
            try
            {
                bool Do = false;
                //when is not ok
                if (!ReadAll())
                {
                    //create list
                    Do = CreateString();
                    //when is successfull 
                    if (Do)//Save
                        Do = SaveAll();
                    //when not return successfull
                    if (!Do)
                    {
                        //System.Windows.Forms.MessageBox.Show("Fatual Error!");
                        return false;
                    }
                }
                else//else return successfull
                {
                    Do = true;

                }
                //when existence os string list and empty od image list
                if (Do && KeyboardAllImage.Count == 0)
                {
                    //clear
                    KeyboardAllImage.Clear();
                    //for all lists items
                    for (int i = 0; i < KeyboardAllStrings.Count; i++)
                    {
                        //For all font prototype
                        if (fonts.Count > 0)
                        {
                            //Do literal Database for All fonts
                            for (int h = 0; h < fonts.Count; h++)
                            {    //proper empty image coinstruction object
                                Bitmap Temp = new Bitmap(Width, Height);
                                //create proper image graphics
                                Graphics e = Graphics.FromImage(Temp);

                                //Draw fill white image
                                e.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));

                                //draw string
                                e.DrawString(KeyboardAllStrings[i], new Font(fonts[h], 1F * ((Width + Height) / 2)), Brushes.Black, new Rectangle(0, 0, Width, Height));
                                //retrive min and max of tow X and Y
                                int MiX = MinX(Temp), MiY = MinY(Temp), MaX = MaxX(Temp), MaY = MaxY(Temp);

                                //crop to proper space
                                Bitmap Te = cropImage(Temp, new Rectangle(MiX, MiY, MaX - MiX, MaY - MiY));

                                //Add
                                //KeyboardAllImage.Add(Te);
                                //create proper conjunction matrix
                                bool[,] Tem = new bool[Width, Height];
                                for (int k = 0; k < Width; k++)
                                    for (int p = 0; p < Height; p++)
                                    {
                                        // Tem[k, p] = Temp.GetPixel(k, p).ToArgb();
                                        if (!(Te.GetPixel(k, p).A == 255 && Te.GetPixel(k, p).R == 255 && Te.GetPixel(k, p).B == 255 && Te.GetPixel(k, p).G == 255))
                                            Tem[k, p] = true;
                                        else
                                            Tem[k, p] = false;

                                    }
                                //Add
                                KeyboardAllConjunctionMatrix.Add(Tem);
                                e.Dispose();
                            }
                        }
                        else//When font not installed
                        {
                            Bitmap Temp = new Bitmap(Width, Height);
                            //create proper image graphics
                            Graphics e = Graphics.FromImage(Temp);

                            //Draw fill white image
                            e.FillRectangle(Brushes.White, new Rectangle(0, 0, Width, Height));

                            //draw string
                            e.DrawString(KeyboardAllStrings[i], new Font("Arial", 1F * ((Width + Height) / 2)), Brushes.Black, new Rectangle(0, 0, Width, Height));
                            //retrive min and max of tow X and Y
                            int MiX = MinX(Temp), MiY = MinY(Temp), MaX = MaxX(Temp), MaY = MaxY(Temp);

                            //crop to proper space
                            Bitmap Te = cropImage(Temp, new Rectangle(MiX, MiY, MaX - MiX, MaY - MiY));

                            //Add
                            //KeyboardAllImage.Add(Te);
                            //create proper conjunction matrix
                            bool[,] Tem = new bool[Width, Height];
                            for (int k = 0; k < Width; k++)
                                for (int p = 0; p < Height; p++)
                                {
                                    // Tem[k, p] = Temp.GetPixel(k, p).ToArgb();
                                    if (!(Te.GetPixel(k, p).A == 255 && Te.GetPixel(k, p).R == 255 && Te.GetPixel(k, p).B == 255 && Te.GetPixel(k, p).G == 255))
                                        Tem[k, p] = true;
                                    else
                                        Tem[k, p] = false;

                                }
                            //Add
                            KeyboardAllConjunctionMatrix.Add(Tem);
                            e.Dispose();
                        }
                    }
                    //save all
                    Do = SaveAll();
                    //if (!Do)
                    //System.Windows.Forms.MessageBox.Show("Fatual Error!");
                    //else
                    //System.Windows.Forms.MessageBox.Show("Completed " + KeyboardAllConjunctionMatrix.Count + " .");
                }
                //else
                //System.Windows.Forms.MessageBox.Show("Fatual Error!");

            }
            catch (Exception t)
            {
                //when existence of exeptio return false
                //System.Windows.Forms.MessageBox.Show("Fatual Error!");
                return false;
            }
            //return successfulll
            //KeyboardAllImage.Clear();
            return true;
        }
        //Convert image list to conjunction matrix
        public bool ConvertAllTempageToMatrix(List<Bitmap> Temp)
        {
            try
            {
                //when list is empty
                if (KeyboardAllConjunctionMatrixList.Count == 0)
                {
                    //clear
                    KeyboardAllConjunctionMatrixList.Clear();
                    //for all list count
                    for (int i = 0; i < Temp.Count; i++)
                    {
                        //matrix boolean object constructor list
                        List<bool[,]> Te = new List<bool[,]>();

                        //boolean object constructor
                        bool[,] Tem = new bool[Width, Height];
                        //for all width
                        for (int k = 0; k < Width; k++)
                        {
                            //for all height
                            for (int p = 0; p < Height; p++)
                            {
                                //assigne proper matrix
                                //Tem[k, p] = Temp[i].GetPixel(k, p).ToArgb();
                                if (!(Temp[i].GetPixel(k, p).A == 255 && Temp[i].GetPixel(k, p).R == 255 && Temp[i].GetPixel(k, p).B == 255 && Temp[i].GetPixel(k, p).G == 255))

                                    Tem[k, p] = true;
                                else
                                    Tem[k, p] = false;

                            }
                        }
                        //add
                        KeyboardAllConjunctionMatrixList.Add(Tem);

                    }
                }
                else//othewise return successfull
                    return true;
            }
            catch (Exception t)
            {
                //when is exeption return unsuccessfull
                return false;
            }
            //when save is not valid return successfull
            if (!SaveAll())
            {
                return false;
            }
            //return successfull
            return true;
        }
    }
}
