/******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************                  ******************************************************
**************************************************                           **************************************************
***********************************************                                 ***********************************************
********************************************                                       ********************************************
******************************************                                            *****************************************
****************************************                                                ***************************************
**************************************                                                   **************************************
************************************                                                       ************************************
**********************************                                                           **********************************
*********************************                                                             *********************************
********************************                                                                *******************************
******************************                                                                   ******************************
*****************************                                                                     *****************************
****************************                                         *******                        ***************************
***************************                                        ****                              **************************
**************************                                        ***                                 *************************
*************************                                        ***                                   ************************
************************                                         **                                    ************************
***********************                                          **                                     ***********************
***********************                                          **                                      **********************
**********************                                           **                                      **********************
*********************                                            **            *                          *********************
*********************   *****************                        ***                                ****   ********************
********************  ***********************************************           ***********************************************
*********************************************************************          ************************************************
********************************************************************          *************************************************
*****************************************************************             *************************************************
***************************************************************               *************************************************
**************************************************************             ****************************************************
*************************************************************               ***************************************************
***********************************************************                  **************************************************
**********************************************************                   **************************************************
*********************************************************                    **************************************************
********************************************************                     **************************************************
*******************************************************                      **************************************************
******************************************************        *               *******   ***************************************
******************************************************       **               ******     **************************************
******************************************************       **                *****     **************************************
*******************************************************      **                *****      *************************************
*******************************************************      **                ****      **************************************
********************************************************     **               ***       ***************************************
*********************************************************                             *****************************************
**********************************************************                          *******************************************
**********************************************************                         ********************************************
***********************************************************                      **********************************************
************************************************************                   ************************************************
************************************************************              *  **************************************************
************************************************************              *****************************************************
************************************************************              *****************************************************
***********************************************************              ******************************************************
**********************************************************               ******************************************************
**********************************************************              *******************************************************
*********************************************************               *******************************************************
********************************************************               ********************************************************
********************************************************                *******************************************************
*******************************************************                  ******************************************************
*******************************************************                    ****************************************************
******************************************************                      ***************************************************
******************************************************                       **************************************************
*****************************************************                          ************************************************
****************************************************                            ***********************************************
******************************************************                           **********************************************
*****************************************************                              ********************************************
****************************************************                                *******************************************
****************************************************                                *******************************************
***************************************************            *****  ***           *******************************************
**************************************************             ***************      *******************************************
*************************************************             ****************      *******************************************
*************************************************            *****************      *******************************************
************************************************            *****************       *******************************************
********* *********************************               *******************       *******************************************
***************************************                  ********************       *******************************************
**************************************                  *********************       *******************************************
********** **************************                 ***********************      ********************************************
*****************************                        ************************      ********************************************
****************************                   ******************************      ********************************************
*********** ****************              ************************************     ********************************************
****************************         *****************************************     ********************************************
************ ***************      ********************************************     ********************************************
************ **************      **********************************************    ********************************************
************* *************     ***********************************************    ********************************************
***************************    ************************************************    ********************************************
***************************    ************************************************     *******************************************
***************************   *************************************************      ******************************************
***************************   *************************************************      ******************************************
***************************  **************************************************       *****************************************
**************** **************************************************************          **************************************
***************** *************************************************************            ************************************
*******************************************************************************            ************************************
****************** *******************************************************************    *************************************
******************* ***********************************************************************************************************
******************** **********************************************************************************************************
********************* *********************************************************************************************************
********************** ********************************************************************************************************
**********************  *******************************************************************************************************
***********************  ******************************************************************************************************
************************  *****************************************************************************************************
*************************  ****************************************************************************************************
*************************** ***************************************************************************************************
**************************** **************************************************************************************************
***************************** *************************************************************************************************
******************************  ***********************************************************************************************
******************************** **********************************************************************************************
********************************************************************************************* *********************************
*******************************************************************************************************************************
**************************************************************      ***********************************************************
*********************************************************               *******************************************************
*******************************************************                   *********** *****************************************
********************************************  *******                       ***************************************************
**********************************************    ***                      ****  **********************************************
**************************************************                           **************************************************
*******************************************************                 *******************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
*******************************************************************************************************************************
******************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace TryCatchRemover
{
    public partial class FormTryCatchRemover : Form
    {
        static void Log(Exception ex)
        {
            try
            {
                Object a = new Object();
                lock (a)
                {
                    string stackTrace = ex.ToString();
                    File.AppendAllText("\\ErrorProgramRun.txt", stackTrace + ": On" + DateTime.Now.ToString()); // path of file where stack trace will be stored.
                }
            }
            catch (Exception t)
            {
                Log(t);
            }
        }
        public FormTryCatchRemover()
        {
            InitializeComponent();
        }
        bool IgnoreofCommentsDoubleSlash(String Contain, int Main)
        {
            int A = 0;
            int Begin = 0, End = 0;
            do
            {
                String S = Contain.Substring(A, Contain.Length - A);

                Begin = S.IndexOf("//");
                End = S.IndexOf('\0');
                if (Begin < Main && End > Main)
                {
                    return true;

                }
                else
                {
                    Begin = S.IndexOf("/*");
                    End = S.IndexOf("*/");
                    if (Begin < Main && End > Main)
                    {
                        return true;

                    }
                    //else
                    //return false;
                }

                A++;
            } while (A < Contain.Length);
            return false;
        }
        int FoundOfProperClosedBracetincatch(String Contain, int MainOpenBracketIndex)
        {
            try
            {
                int IsMainClosedBraket = 0;
                do
                {
                    bool Ign = IgnoreofCommentsDoubleSlash(Contain, MainOpenBracketIndex);

                    if (Contain[MainOpenBracketIndex].Equals('{') && (!Ign))
                        IsMainClosedBraket++;

                    if (Contain[MainOpenBracketIndex].Equals('}') && IsMainClosedBraket == 1 && (!Ign))
                    {
                        return MainOpenBracketIndex;
                    }
                    else
                    if (Contain[MainOpenBracketIndex].Equals('}') && IsMainClosedBraket >= 1 && (!Ign))
                        IsMainClosedBraket--;
                    MainOpenBracketIndex++;
                } while (MainOpenBracketIndex < Contain.Length);
            }
            catch (Exception t)
            {
                Log(t);
            }
            return MainOpenBracketIndex;
        }

        void CatchRemove(ref String Contain, int S, int A2)
        {
            try
            {



                while (A2 < Contain.Length && A2 > 0)
                {
                    A2 = Contain.IndexOf("catch");
                    int P = A2;
                    try
                    {

                        while (!(Contain[A2].Equals('}')))
                            A2--;
                        A2--;
                        int A1 = FoundOfProperClosedBracetincatch(Contain, P);

                        Contain = Contain.Remove(A2, A1 - A2 + 1);

                    }
                    catch (Exception t) { Log(t); }
                }
            }
            catch (Exception t) { Log(t); }
        }
        private void FormTryCatchRemover_Load(object sender, EventArgs e)
        {

        }
        int IndexofType(String A, ref int lenght)
        {
            if (A.ToLower().Contains("int "))
            {
                if (A.ToLower().Substring(4, A.Length - 8).Contains("="))
                {
                    lenght = 3;
                    return A.ToLower().IndexOf("int ");
                }
            }
            else
            if (A.ToLower().Contains("int "))
            {
                if (A.ToLower().Substring(6, A.Length - 12).Contains("="))
                {
                    lenght = 5;
                    return A.ToLower().IndexOf("int ");
                }
            }
            else
            if (A.ToLower().Contains("float "))
            {
                if (A.ToLower().Substring(5, A.Length - 10).Contains("="))
                {
                    lenght = 5;
                    return A.ToLower().IndexOf("float ");
                }

            }
            else
            if (A.ToLower().Contains("task "))
            {
                if (A.ToLower().Substring(4, A.Length - 8).Contains("="))
                {
                    lenght = 4;
                    return A.ToLower().IndexOf("task ");
                }
            }

            return -1;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (CheckBoxVarConvertOnly.Checked)
            {
                OpenFileDialogTryCatchRemover.ShowDialog();
                String A = OpenFileDialogTryCatchRemover.FileName;
                String Contain = System.IO.File.ReadAllText(A);
                int lengh = 0;
                int A1 = 0;
                int A2 = A1;
                try
                {
                    while (A1 < Contain.Length && A1 > 0)
                    {
                        A1 = IndexofType(Contain, ref lengh);
                        Contain = Contain.Replace(Contain.Substring(A1, lengh + 1), "var ");
                    }
                }
                catch (Exception t) { Log(t); }

            }
            else
            {
                try
                {
                    OpenFileDialogTryCatchRemover.ShowDialog();
                    String A = OpenFileDialogTryCatchRemover.FileName;
                    String Contain = System.IO.File.ReadAllText(A);
                    int A1 = Contain.IndexOf("try");
                    int A2 = A1;
                    try
                    {
                        while (A1 < Contain.Length && A1 > 0)
                        {
                            A1 = Contain.IndexOf("try");
                            A2 = A1;
                            if (A1 < 0)
                                break;
                            while ((!(Contain[A2].Equals('{'))) && (A2 < Contain.Length))
                                A2++;
                            Contain = Contain.Remove(A1, A2 - A1 + 1);
                        }
                    }
                    catch (Exception t) { Log(t); }
                    try
                    {
                        A2 = Contain.IndexOf("catch");
                        CatchRemove(ref Contain, A2, A2);
                    }
                    catch (Exception t) { Log(t); }
                    saveFileDialogTryCatchRemover.ShowDialog();
                    System.IO.File.WriteAllText(saveFileDialogTryCatchRemover.FileName, Contain);
                    MessageBox.Show("Done!");

                }
                catch (Exception t)
                {
                    Log(t);
                }
            }
        }

        private void FormTryCatchRemover_Load_1(object sender, EventArgs e)
        {

        }

        private void OpenFileDialogTryCatchRemover_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void OpenFileDialogTryCatchRemover_FileOk_1(object sender, CancelEventArgs e)
        {

        }

        private void CheckBoxVarConvertOnly_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            (new AboutBoxTryCatchRemover()).Show();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CheckBoxVarConvertOnly.Checked)
            {
                OpenFileDialogTryCatchRemover.ShowDialog();
                String A = OpenFileDialogTryCatchRemover.FileName;
                String Contain = System.IO.File.ReadAllText(A);
                int lengh = 0;
                int A1 = 0;
                int A2 = A1;
                try
                {
                    while (A1 < Contain.Length && A1 > 0)
                    {
                        A1 = IndexofType(Contain, ref lengh);
                        Contain = Contain.Replace(Contain.Substring(A1, lengh + 1), "var ");
                    }
                }
                catch (Exception t) { Log(t); }

            }
            else
            {
                try
                {
                    OpenFileDialogTryCatchRemover.ShowDialog();
                    String A = OpenFileDialogTryCatchRemover.FileName;
                    String Contain = System.IO.File.ReadAllText(A);
                    int A1 = Contain.IndexOf("try");
                    int A2 = A1;
                    try
                    {
                        while (A1 < Contain.Length && A1 > 0)
                        {
                            A1 = Contain.IndexOf("try");
                            A2 = A1;
                            if (A1 < 0)
                                break;
                            while ((!(Contain[A2].Equals('{'))) && (A2 < Contain.Length))
                                A2++;
                            Contain = Contain.Remove(A1, A2 - A1 + 1);
                        }
                    }
                    catch (Exception t) { Log(t); }
                    try
                    {
                        A2 = Contain.IndexOf("catch");
                        CatchRemove(ref Contain, A2, A2);
                    }
                    catch (Exception t) { Log(t); }
                    saveFileDialogTryCatchRemover.ShowDialog();
                    System.IO.File.WriteAllText(saveFileDialogTryCatchRemover.FileName, Contain);
                    MessageBox.Show("Done!");

                }
                catch (Exception t)
                {
                    Log(t);
                }
            }
        }

        private void buttonComment_Click(object sender, EventArgs e)
        {
            OpenFileDialogTryCatchRemover.ShowDialog();
            String A = OpenFileDialogTryCatchRemover.FileName;
            String Contain = System.IO.File.ReadAllText(A);
            int i = 0;
            do
            {
                if (!(Contain.Contains("/*") && Contain.Contains("*/")))
                    break;
                if (Contain.IndexOf("*/") == -1)
                    break;
                if (Contain.IndexOf("/*") == -1)
                    break;
                if (Contain.IndexOf("*/") + 2 > Contain.Length)
                    break;
                Contain = Contain.Replace(Contain.Substring(Contain.IndexOf("/*"), Contain.IndexOf("*/") + 2 - Contain.IndexOf("/*")), "");

            } while (Contain.Contains("*/"));
            saveFileDialogTryCatchRemover.ShowDialog();
            System.IO.File.WriteAllText(saveFileDialogTryCatchRemover.FileName, Contain);
            MessageBox.Show("Done!");

        }

        private void buttonCodeComment_Click(object sender, EventArgs e)
        {
            OpenFileDialogTryCatchRemover.ShowDialog();
            String A = OpenFileDialogTryCatchRemover.FileName;
            String[] Contain = System.IO.File.ReadAllLines(A);
            int i = 0;
            do
            {
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf(";") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("for") != -1 && Contain[i].IndexOf("(") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("if") != -1 && Contain[i].IndexOf("(") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("while") != -1 && (Contain[i].Length == 5 && Contain[i].IndexOf("}") != -1) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("do") != -1 && (Contain[i].IndexOf("{") != -1 || Contain[i].Length == 2) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("Parallel.") != -1 && Contain[i].IndexOf("=>") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("var") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("int") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("double") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("float") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("char") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("string") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("String") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("Int32") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("Int64") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("Double") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("//") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("new") != -1 && (Contain[i].IndexOf("(") != -1 || Contain[i].IndexOf(")") != -1) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("//");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }


                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("for") != -1 && Contain[i].IndexOf("(") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("if") != -1 && Contain[i].IndexOf("(") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("while") != -1 && (Contain[i].Length == 5 && Contain[i].IndexOf("}") != -1) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("do") != -1 && (Contain[i].IndexOf("{") != -1 || Contain[i].Length == 2) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("Parallel.") != -1 && Contain[i].IndexOf("=>") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("var") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("int") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("double") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("float") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("char") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("string") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("String") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("Int32") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("Int64") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("Double") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("///") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("new") != -1 && (Contain[i].IndexOf("(") != -1 || Contain[i].IndexOf(")") != -1) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("///");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("for") != -1 && Contain[i].IndexOf("(") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("if") != -1 && Contain[i].IndexOf("(") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("while") != -1 && (Contain[i].Length == 5 && Contain[i].IndexOf("}") != -1) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("do") != -1 && (Contain[i].IndexOf("{") != -1 || Contain[i].Length == 2) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("Parallel.") != -1 && Contain[i].IndexOf("=>") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("var") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("int") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("double") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("float") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("char") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("string") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("String") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("Int32") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("Int64") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("Double") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                if (Contain[i].IndexOf("////") != -1 && Contain[i].IndexOf("=") != -1 && Contain[i].IndexOf("new") != -1 && (Contain[i].IndexOf("(") != -1 || Contain[i].IndexOf(")") != -1) && Contain[i].IndexOf("/r") != -1)
                {
                    int ii = Contain[i].IndexOf("////");
                    int jj = Contain[i].Length;
                    if (jj > ii)
                        Contain[i] = Contain[i].Replace(Contain[i].Substring(ii, jj - ii), "");
                }
                i++;
            } while (i < Contain.Length);
            saveFileDialogTryCatchRemover.ShowDialog();
            System.IO.File.WriteAllLines(saveFileDialogTryCatchRemover.FileName, Contain);
            MessageBox.Show("Done!");
        }
        String[] Remove(String[] In, int i)
        {
            String[] Re = new String[In.Length - 1];
            for (int o = 0; o < i; o++)
                Re[o] = In[o];
            for (int o = i; o < In.Length - 1; o++)
                Re[o] = In[o + 1];
            return Re;
        }
        private void buttonSpaceLine_Click(object sender, EventArgs e)
        {
            OpenFileDialogTryCatchRemover.ShowDialog();
            String A = OpenFileDialogTryCatchRemover.FileName;
            String[] Contain = System.IO.File.ReadAllLines(A);
            int i = 0;
            do
            {
                if (Contain[i].Length != 0)
                {

                    i++;
                    continue;
                }

                Contain = Remove(Contain, i);
                i++;
            } while (i < Contain.Length);
            saveFileDialogTryCatchRemover.ShowDialog();
            System.IO.File.WriteAllLines(saveFileDialogTryCatchRemover.FileName, Contain);
            MessageBox.Show("Done!");

        }

        private void buttonTetraShop_Click(object sender, EventArgs e)
        {
            String[] TetraShop = new String[40];
            TetraShop[0] = "/";

            for (int j = 0; j < 40; j++)
            {
                int i = 0;
                if (j != 0)
                    TetraShop[j] = "";
                bool SetB = true;
                do
                {


                    if (j < 39)
                    {
                        if (j == 20)
                        {
                            int B = -1;
                            if (SetB)
                            {
                                B = TetraShop[j].Length;
                            }

                            if (SetB && i == 13)
                            {
                                TetraShop[j] += "TETRASHOP.IR";
                                SetB = false;
                                i += "TETRASHOP.IR".Length - 1;
                            }

                            if (TetraShop[j].Length == 39)
                            {
                                TetraShop[j] += "\n";
                            }

                            if (TetraShop[j].Length < 39)
                            {
                                TetraShop[j] += "*";

                            }
                        }
                        else
                        {
                            if (TetraShop[j].Length < 39)
                            {
                                TetraShop[j] += "*";

                            }

                            if (TetraShop[j].Length == 39)
                            {
                                TetraShop[j] += "\n";
                            }
                        }
                    }
                    else
                    {
                        if (i < 38)
                        {
                            TetraShop[j] += "*";

                        }
                        if (TetraShop[j].Length == 38)
                        {
                            TetraShop[j] += "/";

                        }
                        if (TetraShop[j].Length == 39)
                        {
                            TetraShop[j] += "\n";
                        }
                    }
                    i++;
                } while (i < 40);
            }


            OpenFileDialogTryCatchRemover.ShowDialog();
            String A = OpenFileDialogTryCatchRemover.FileName;
            String Contain = System.IO.File.ReadAllText(A);
            String Brand = "";

            for (int i = 0; i < 40; i++)
                Brand += TetraShop[i];
            Contain = Brand + Contain;
            saveFileDialogTryCatchRemover.ShowDialog();
            System.IO.File.WriteAllText(saveFileDialogTryCatchRemover.FileName, Contain);
            MessageBox.Show("Done!");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Select Image:");
            OpenFileDialogTryCatchRemover.ShowDialog();
            String AImage = OpenFileDialogTryCatchRemover.FileName;
            int Wi = Image.FromFile(AImage).Width;
            int He = Image.FromFile(AImage).Height;
            int n = 0;
            if (Wi > He)
            {
                n = Wi;
               // double a = 40.0 / (double)Wi;
                //Wi = (int)((double)Wi * a);
               // He = (int)((double)He * a);
            }
            else 
            {
                n = He;
               // double a = 40.0 / (double)He;
//Wi = (int)((double)Wi * a);
               // He = (int)((double)He * a);

            }
            Bitmap TetraShopImage = new Bitmap(Image.FromFile(AImage), new Size(Wi,He));
            Graphics g = Graphics.FromImage(TetraShopImage);
            String[] TetraShop = new String[n];
            TetraShop[0] = "/*";

            for (int j = 0; j < n; j++)
            {
                  if (j != 0)
                    TetraShop[j] = "";
                for (int i = 0; i < n; i++)
                {


                    if (j < n-1)
                    {
                        if (TetraShop[j].Length == n-1)
                        {
                            TetraShop[j] += "\n";
                        }
                        if (TetraShop[j].Length < n-1)
                        {
                            if (He > j && Wi > i)
                            {

                                if (((TetraShopImage.GetPixel(i, j).R + TetraShopImage.GetPixel(i, j).B + TetraShopImage.GetPixel(i, j).G) / 3) > 125)
                                    TetraShop[j] += " ";
                                else TetraShop[j] += "*";

                            }
                            else
                            {
                                
                                if (TetraShop[j].Length < n-2)
                                {
                                    TetraShop[j] += "*";

                                }
                                if (TetraShop[j].Length == n-1)
                                {
                                    TetraShop[j] += "\n";
                                }
                            }
                        }
                        else
                        {
                            if (TetraShop[j].Length <= n-3)
                            {
                                TetraShop[j] += "*";

                            }
                            if (TetraShop[j].Length == n-2)
                            {
                                TetraShop[j] += "/";

                            }
                            if (TetraShop[j].Length == n-1)
                            {
                                TetraShop[j] += "\n";
                            }
                        }
                    }
                    else
                    {
                        if (i < n-3)
                        {
                            if (TetraShop[j].Length < n-3)
                            {
                                if (He > j && Wi > i)
                                {

                                    if (((TetraShopImage.GetPixel(i, j).R + TetraShopImage.GetPixel(i, j).B + TetraShopImage.GetPixel(i, j).G) / 3) > 125)
                                        TetraShop[j] += " ";
                                    else TetraShop[j] += "*";

                                }
                            }                            
                        }
                        if (TetraShop[j].Length == n-3)
                        {
                            TetraShop[j] += "*";

                        }
                        if (TetraShop[j].Length == n-2)
                        {
                            TetraShop[j] += "/";

                        }
                        if (TetraShop[j].Length == n-1)
                        {
                            TetraShop[j] += "\n";
                        }
                    }
                }
            }


            MessageBox.Show("Select Text To Open:");
            OpenFileDialogTryCatchRemover.ShowDialog();
            String A = OpenFileDialogTryCatchRemover.FileName;
            String Contain = System.IO.File.ReadAllText(A);
            String Brand = "";

            for (int i = 0; i < n; i++)
                Brand += TetraShop[i];
            Contain = Brand + Contain;
            MessageBox.Show("Select Text To Save:");
            saveFileDialogTryCatchRemover.ShowDialog();
            System.IO.File.WriteAllText(saveFileDialogTryCatchRemover.FileName, Contain);
            MessageBox.Show("Done!");

        }
    }
}
