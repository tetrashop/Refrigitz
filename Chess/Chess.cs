using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ilf.pgn;
using ilf.pgn.Data;
namespace Chess
{
    public partial class Chess : Form
    {
        PgnReader reader = null;
        Database gameDb = null;

        String[] SS;
        String PgnGames = "";
        RefrigtzChessPortable.RefrigtzChessPortableForm S = null;
        ChessFirst.ChessFirstForm F = null;
        public bool W = true;
        public bool B = true;
        ChessCom.ChessComForm BB = null;
        public static ChessCom.ChessComForm BBS = null;
        public bool frize = true;
        public Chess()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            B = false;
            if (W) {
                frize = true;
                (new ChessFirst.ChessFirstForm()).ShowDialog();
                frize = false;
                W = false;
            }
            else
            {
                frize = true;
                (new RefrigtzChessPortable.RefrigtzChessPortableForm()).ShowDialog();
                frize = false;
                W = true;
            }
            B = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            W = false;
            if (!B)
            {
                frize = true;
                (new ChessFirst.ChessFirstForm()).ShowDialog();
                frize = false;
                B = true;
            }
            else
            {
                frize = true;
                (new RefrigtzChessPortable.RefrigtzChessPortableForm()).ShowDialog();
                frize = false;
                B = false;
            }
            W = true;
        }
        void Play()
        {
            do {
                if (W)
                {
                    int C = 0;
                    C += F.Play(-1, -1);
                    C += S.Play(F.R.CromosomRowFirst, F.R.CromosomColumnFirst);
                    C += S.Play(F.R.CromosomRow, F.R.CromosomColumn);
                    C += BB.Play(F.R.CromosomRowFirst, F.R.CromosomColumnFirst);
                    C += BB.Play(F.R.CromosomRow, F.R.CromosomColumn);
                    if (C != 0)
                    {
                        MessageBox.Show("خطای بحرانی!");
                        return;
                    }
                    W = false;
                    B = true;
                }
                else
                {
                    int C = 0;
                    C += S.Play(-1, -1);
                    C += F.Play(S.R.CromosomRowFirst, S.R.CromosomColumnFirst);
                    C += F.Play(S.R.CromosomRow, S.R.CromosomColumn);
                    C += BB.Play(S.R.CromosomRowFirst, S.R.CromosomColumnFirst);
                    C += BB.Play(S.R.CromosomRow, S.R.CromosomColumn);
                    if (C != 0)
                    {
                        MessageBox.Show("خطای بحرانی!");
                        return;
                    }
                    W = true;
                    B = false;
                }


            } while (true);

        }
        public void PlayTeach()
        {
            do
            {
                do { System.Threading.Thread.Sleep(1000); } while (BBS.rf == -1 || BBS.cf == -1 || BBS.rs == -1 || BBS.cs == -1);
                if (W)
                {
                    int C = 0;
                    C += F.Play(BBS.rf, BBS.cf);
                    C += F.Play(BBS.rs, BBS.cs);

                    C += S.Play(BBS.rf, BBS.cf);
                    C += S.Play(BBS.rs, BBS.cs);
                    BBS.rf = -1;
                    BBS.cf = -1;
                    BBS.rs = -1;
                    BBS.cs = -1;

                    ChessCom.ChessComForm.freezBoard = false;
                    if (C != 0)
                    {
                        MessageBox.Show("خطای بحرانی!");
                        return;
                    }
                    W = false;
                    B = true;
                }
                else
                {
                    int C = 0;
                    C += F.Play(BBS.rf, BBS.cf);
                    C += F.Play(BBS.rs, BBS.cs);
                    C += S.Play(BBS.rf, BBS.cf);
                    C += S.Play(BBS.rs, BBS.cs);
                    BBS.rf = -1;
                    BBS.cf = -1;
                    BBS.rs = -1;
                    BBS.cs = -1;

                    ChessCom.ChessComForm.freezBoard = false;
                    if (C != 0)
                    {
                        MessageBox.Show("خطای بحرانی!");
                        return;
                    }
                    W = true;
                    B = false;
                }


            } while (true);
        }
        int SetRowColumn(String A)
        {
            Object O = new Object();
            lock (O)
            {

                try
                {
            
                   
                        if (A[0] == 'a')
                            BBS.rf = 0;
                        else
                            if (A[0] == 'b')
                            BBS.rf = 1;
                        else
                                if (A[0] == 'c')
                            BBS.rf = 2;
                        else
                                    if (A[0] == 'd')
                            BBS.rf = 3;
                        else
                                        if (A[0] == 'e')
                            BBS.rf = 4;
                        else
                                            if (A[0] == 'f')
                            BBS.rf = 5;
                        else
                                                if (A[0] == 'g')
                            BBS.rf = 6;
                        else
                                                    if (A[0] == 'h')
                            BBS.rf = 7;
                        /* if(!Sugar)
                         BBS.cf = 7 - ((System.Convert.ToInt32(A[1]) - 48) - 1);
                         else
                          */
                        BBS.cf = ((System.Convert.ToInt32(A[1]) - 48) - 1);

                        if (A[0] == 'a')
                            BBS.rs = 0;
                        else
                            if (A[2] == 'b')
                            BBS.rs = 1;
                        else
                                if (A[2] == 'c')
                            BBS.rs = 2;
                        else
                                    if (A[2] == 'd')
                            BBS.rs = 3;
                        else
                                        if (A[2] == 'e')
                            BBS.rs = 4;
                        else
                                            if (A[2] == 'f')
                            BBS.rs = 5;
                        else
                                                if (A[2] == 'g')
                            BBS.rs = 6;
                        else
                                                    if (A[2] == 'h')
                            BBS.rs = 7;
                        /*if (!Sugar)
                             BBS.cs = 7 - ((System.Convert.ToInt32(A[3]) - 48) - 1);
                         else*/
                        BBS.cs = ((System.Convert.ToInt32(A[3]) - 48) - 1);

                    if (A.Length == 5)
                    {
                        if (A[4] == 'p')
                            return -1;
                        else
                            if (A[4] == 'n')
                            return -3;
                        else
                                if (A[4] == 'b')
                            return -2;
                        else
                                    if (A[4] == 'r')
                            return -4;
                        else
                                        if (A[4] == 'q')
                            return -5;
                        else
                                            if (A[4] == 'P')
                            return 1;
                        else
                                                if (A[4] == 'N')
                            return 3;
                        else
                                                    if (A[4] == 'B')
                            return 2;
                        else
                                                        if (A[4] == 'R')
                            return 4;
                        else
                                                            if (A[4] == 'Q')
                            return 5;
                    }
                    else
                 if (BBS.rf != -1 && BBS.cf != -1 && BBS.rs != -1 && BBS.cs != -1)
                        return 10;


                }
                catch (Exception t)
                {
                    return -10;
                }
                return 0;

            }
        }
        int SetRowColumnA(String A)
        {
            Object O = new Object();
            lock (O)
            {

                try
                {
                    int Obj = 0;
                    if (A[0] == 'K')
                        Obj = 6;
                    else
                    if (A[0] == 'Q')
                        Obj = 5;
                    else
                    if (A[0] == 'R')
                        Obj = 4;
                    else
                    if (A[0] == 'N')
                        Obj = 3;
                    else
                     if (A[0] == 'B')
                        Obj = 2;
                    else
                        Obj = 1;

                    A = A.Replace("+", "");

                    A = A.Replace("#", "");
                    if (Obj == 1)
                    {
                        if (Obj == 1 && A.Length == 2)//common pawn move
                        {

                            //destination
                            if (A[0] == 'a')
                                BBS.rs = 0;
                            else
                                if (A[0] == 'b')
                                BBS.rs = 1;
                            else
                                    if (A[0] == 'c')
                                BBS.rs = 2;
                            else
                                        if (A[0] == 'd')
                                BBS.rs = 3;
                            else
                                            if (A[0] == 'e')
                                BBS.rs = 4;
                            else
                                                if (A[0] == 'f')
                                BBS.rs = 5;
                            else
                                                    if (A[0] == 'g')
                                BBS.rs = 6;
                            else
                                                        if (A[0] == 'h')
                                BBS.rs = 7;
                            /*if (!Sugar)
                                 BBS.cs = 7 - ((System.Convert.ToInt32(A[3]) - 48) - 1);
                             else*/
                            BBS.cs = ((System.Convert.ToInt32(A[1]) - 48) - 1);


                            //source
                            for (int i = BBS.cs - 1; i >= 0; i--)
                            {
                                /*if (W)
                                {
                                    if (F.brd.GetTable()[BBS.rs, i] == 0)
                                    {

                                    }
                                    else
                                    if (F.brd.GetTable()[BBS.rs, i] > 0)
                                    {
                                        BBS.rf = BBS.rs;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                    else
                                        break;
                                }
                                else*/
                                if (B)
                                {
                                    if (F.brd.GetTable()[BBS.rs, i] == 0)
                                    {

                                    }
                                    else
                                    if (F.brd.GetTable()[BBS.rs, i] < 0)
                                    {
                                        BBS.rf = BBS.rs;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                    else
                                        break;
                                }
                            }
                            for (int i = 0; i < BBS.cs; i++)
                            {
                                if (W)
                                {
                                    if (F.brd.GetTable()[BBS.rs, i] == 0)
                                    {

                                    }
                                    else
                                    if (F.brd.GetTable()[BBS.rs, i] > 0)
                                    {
                                        BBS.rf = BBS.rs;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                    else
                                        break;
                                }
                                /*else
                                if (B)
                               {
                                    if (F.brd.GetTable()[BBS.rs, i] == 0)
                                    {

                                    }
                                    else
                                    if (F.brd.GetTable()[BBS.rs, i] < 0)
                                    {
                                        BBS.rf = BBS.rs;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                    else
                                        break;
                                }*/
                            }
                        }
                        else if (Obj == 1 && A.Length == 3)
                        {
                            int i = -1;
                            if (A[0] == 'x')
                            {                         //destination
                                if (A[1] == 'a')
                                    BBS.rs = 0;
                                else
                                    if (A[1] == 'b')
                                    BBS.rs = 1;
                                else
                                        if (A[1] == 'c')
                                    BBS.rs = 2;
                                else
                                            if (A[1] == 'd')
                                    BBS.rs = 3;
                                else
                                                if (A[1] == 'e')
                                    BBS.rs = 4;
                                else
                                                    if (A[1] == 'f')
                                    BBS.rs = 5;
                                else
                                                        if (A[1] == 'g')
                                    BBS.rs = 6;
                                else
                                                            if (A[1] == 'h')
                                    BBS.rs = 7;
                                /*if (!Sugar)
                                     BBS.cs = 7 - ((System.Convert.ToInt32(A[3]) - 48) - 1);
                                 else*/
                                BBS.cs = ((System.Convert.ToInt32(A[2]) - 48) - 1);

                                //source
                                /*if (W)
                                   {
                                       if (F.brd.GetTable()[BBS.rs - 1, i] > 0)
                                       {
                                           BBS.rf = BBS.rs - 1;
                                           BBS.cf = i;
                                           return 1;
                                       }
                                       else
                                           if (F.brd.GetTable()[BBS.rs + 1, i] > 0)
                                       {
                                           BBS.rf = BBS.rs + 1;
                                           BBS.cf = i;
                                           return 1;
                                       }
                                    }
                                   else*/
                                if (B)
                                {
                                    i = BBS.cs - 1;
                                    if (F.brd.GetTable()[BBS.rs - 1, i] < 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                    else
                                       if (F.brd.GetTable()[BBS.rs + 1, i] < 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                }

                             
                                if (W)
                                {
                                    i = BBS.cs + 1;
                                    if (F.brd.GetTable()[BBS.rs - 1, i] > 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                    else
                                    if (F.brd.GetTable()[BBS.rs + 1, i] > 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                }
                                /*else
                                if (B)
                               {
                                       if (F.brd.GetTable()[BBS.rs - 1, i] < 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                    else
                                       if (F.brd.GetTable()[BBS.rs + 1, i] < 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                  }*/

                            }
                        }
                        else if (Obj == 1 && A.Length == 4)
                        {
                            if (A[1] == 'x')
                            {
                                int src = -1;
                                if (A[0] == 'a')
                                    src = 0;
                                else
                                     if (A[0] == 'b')
                                    src = 1;
                                else
                                         if (A[0] == 'c')
                                    src = 2;
                                else
                                             if (A[0] == 'd')
                                    src = 3;
                                else
                                                 if (A[0] == 'e')
                                    src = 4;
                                else
                                                     if (A[0] == 'f')
                                    src = 5;
                                else
                                                         if (A[0] == 'g')
                                    src = 6;
                                else
                                                             if (A[0] == 'h')
                                    src = 7;

                                //destination
                                if (A[2] == 'a')
                                    BBS.rs = 0;
                                else
                                    if (A[2] == 'b')
                                    BBS.rs = 1;
                                else
                                        if (A[2] == 'c')
                                    BBS.rs = 2;
                                else
                                            if (A[2] == 'd')
                                    BBS.rs = 3;
                                else
                                                if (A[2] == 'e')
                                    BBS.rs = 4;
                                else
                                                    if (A[2] == 'f')
                                    BBS.rs = 5;
                                else
                                                        if (A[2] == 'g')
                                    BBS.rs = 6;
                                else
                                                            if (A[2] == 'h')
                                    BBS.rs = 7;
                                /*if (!Sugar)
                                     BBS.cs = 7 - ((System.Convert.ToInt32(A[3]) - 48) - 1);
                                 else*/
                                BBS.cs = ((System.Convert.ToInt32(A[3]) - 48) - 1);

                                if (src == -1)
                                    return -1;


                                //source

                                /*if (W)
                                {
                                    if (F.brd.GetTable()[BBS.rs - 1, src] > 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                    else
                                if (F.brd.GetTable()[BBS.rs + 1, src] > 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                }else*/
                                if (B)
                                {
                                    if (F.brd.GetTable()[BBS.rs - 1, src] < 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                    else
                                           if (F.brd.GetTable()[BBS.rs + 1, src] < 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                }



                                if (W)
                                {
                                    if (F.brd.GetTable()[BBS.rs - 1, src] > 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                    else
                                if (F.brd.GetTable()[BBS.rs + 1, src] > 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                }
                                /*else{
                                    if (B)
                             {
                            if (F.brd.GetTable()[BBS.rs - 1, src] < 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                    else
                                       if (F.brd.GetTable()[BBS.rs + 1, src] < 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = i;
                                        return 1;
                                    }
                                    }}
                                */
                            }
                            if (A[2] == '=')
                            {
                                int src = -1;
                                int srcObj = -1;
                                if (A[3] == 'a')
                                        srcObj = 0;
                                else
                                     if (A[3] == 'b')
                                    srcObj = 1;
                                else
                                         if (A[3] == 'c')
                                    srcObj = 2;
                                else
                                             if (A[3] == 'd')
                                    srcObj = 3;
                                else
                                                 if (A[3] == 'e')
                                    srcObj = 4;
                                else
                                                     if (A[3] == 'f')
                                    srcObj = 5;
                                else
                                                         if (A[3] == 'g')
                                    srcObj = 6;
                                else
                                                             if (A[3] == 'h')
                                    srcObj = 7;

                              

                                //destination
                                if (A[0] == 'a')
                                    BBS.rs = 0;
                                else
                                    if (A[0] == 'b')
                                    BBS.rs = 1;
                                else
                                        if (A[0] == 'c')
                                    BBS.rs = 2;
                                else
                                            if (A[0] == 'd')
                                    BBS.rs = 3;
                                else
                                                if (A[0] == 'e')
                                    BBS.rs = 4;
                                else
                                                    if (A[0] == 'f')
                                    BBS.rs = 5;
                                else
                                                        if (A[0] == 'g')
                                    BBS.rs = 6;
                                else
                                                            if (A[0] == 'h')
                                    BBS.rs = 7;
                                /*if (!Sugar)
                                     BBS.cs = 7 - ((System.Convert.ToInt32(A[3]) - 48) - 1);
                                 else*/
                                BBS.cs = ((System.Convert.ToInt32(A[1]) - 48) - 1);

                                if (srcObj == -1)
                                    return -1;

                                Obj = srcObj;

                                //source

                                /*if (W)
                                {
                                    if (F.brd.GetTable()[BBS.rs - 1, src] > 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                    else
                                if (F.brd.GetTable()[BBS.rs + 1, src] > 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                }else*/
                                if (B)
                                {
                                    src = BBS.cs - 1;
                                    if (F.brd.GetTable()[BBS.rs - 1, src] < 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                    else
                                           if (F.brd.GetTable()[BBS.rs + 1, src] < 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = src;
                                        return 1;
                                    }

                                    
                                }



                                if (W)
                                {
                                    src = BBS.cs + 1;


                                    if (F.brd.GetTable()[BBS.rs - 1, src] > 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                    else
                                if (F.brd.GetTable()[BBS.rs + 1, src] > 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                }
                                /*else
                                if (B)
                              {
                                  if (F.brd.GetTable()[BBS.rs - 1, src] < 0)
                                    {
                                        BBS.rf = BBS.rs - 1;
                                        BBS.cf = src;
                                        return 1;
                                    }
                                    else
                                       if (F.brd.GetTable()[BBS.rs + 1, src] < 0)
                                    {
                                        BBS.rf = BBS.rs + 1;
                                        BBS.cf = i;
                                        return 1;
                                    }
}
                                */
                            }

                        }
                    }
                    else
                    { 
                    if (A[0] == 'a')
                        BBS.rf = 0;
                    else
                        if (A[0] == 'b')
                        BBS.rf = 1;
                    else
                            if (A[0] == 'c')
                        BBS.rf = 2;
                    else
                                if (A[0] == 'd')
                        BBS.rf = 3;
                    else
                                    if (A[0] == 'e')
                        BBS.rf = 4;
                    else
                                        if (A[0] == 'f')
                        BBS.rf = 5;
                    else
                                            if (A[0] == 'g')
                        BBS.rf = 6;
                    else
                                                if (A[0] == 'h')
                        BBS.rf = 7;
                    /* if(!Sugar)
                     BBS.cf = 7 - ((System.Convert.ToInt32(A[1]) - 48) - 1);
                     else
                      */
                    BBS.cf = ((System.Convert.ToInt32(A[1]) - 48) - 1);
                      
                    if (A[0] == 'a')
                        BBS.rs = 0;
                    else
                        if (A[2] == 'b')
                        BBS.rs = 1;
                    else
                            if (A[2] == 'c')
                        BBS.rs = 2;
                    else
                                if (A[2] == 'd')
                        BBS.rs = 3;
                    else
                                    if (A[2] == 'e')
                        BBS.rs = 4;
                    else
                                        if (A[2] == 'f')
                        BBS.rs = 5;
                    else
                                            if (A[2] == 'g')
                        BBS.rs = 6;
                    else
                                                if (A[2] == 'h')
                        BBS.rs = 7;
                     /*if (!Sugar)
                          BBS.cs = 7 - ((System.Convert.ToInt32(A[3]) - 48) - 1);
                      else*/
                    BBS.cs = ((System.Convert.ToInt32(A[3]) - 48) - 1);

                        if (A.Length == 5)
                        {
                            if (A[4] == 'p')
                                return -1;
                            else
                                if (A[4] == 'n')
                                return -3;
                            else
                                    if (A[4] == 'b')
                                return -2;
                            else
                                        if (A[4] == 'r')
                                return -4;
                            else
                                            if (A[4] == 'q')
                                return -5;
                            else
                                                if (A[4] == 'P')
                                return 1;
                            else
                                                    if (A[4] == 'N')
                                return 3;
                            else
                                                        if (A[4] == 'B')
                                return 2;
                            else
                                                            if (A[4] == 'R')
                                return 4;
                            else
                                                                if (A[4] == 'Q')
                                return 5;
                        }
                    }
                }
                catch (Exception t)
                {
                    return -1;
                }
                return 0;

            }
        }
        public void PlayTeachPGN()
        {
            do
            {
                int I = 0;
                do
                {
                    if (SS[I].Contains("1. "))
                        break;
                    I++;
                } while (I < SS.Length);
                if (I >= SS.Length)
                    return;
                String A = SS[I];
                PgnGames = A;
                I = 1;
                do
                {


                    do
                    {
                        string z = "";
                        try
                        {
                            z = A.Substring(A.IndexOf(I.ToString() + ". "), A.IndexOf((I + 1).ToString() + ". ") - A.IndexOf(I.ToString() + ". "));
                            A = A.Replace(z, "");

                        }
                        catch (Exception t)
                        {
                            return;
                        }
                        if (W)
                        {
                            z = z.Remove(0, 2);
                            string b = z.Substring(2, z.IndexOf(" "));
                            if (SetRowColumn(b) == -1)
                                return;
                        }
                        else
                        {
                            string b = z.Substring(1, z.IndexOf(" "));
                            if (SetRowColumn(b) == -1)
                                return;

                        }
                    } while (BBS.rf == -1 || BBS.cf == -1 || BBS.rs == -1 || BBS.cs == -1);
                    if (W)
                    {
                        int C = 0;
                        C += F.Play(BBS.rf, BBS.cf);
                        C += F.Play(BBS.rs, BBS.cs);

                        C += S.Play(BBS.rf, BBS.cf);
                        C += S.Play(BBS.rs, BBS.cs);

                        C += BBS.Play(BBS.rf, BBS.cf);
                        C += BBS.Play(BBS.rs, BBS.cs);
                        BBS.rf = -1;
                        BBS.cf = -1;
                        BBS.rs = -1;
                        BBS.cs = -1;
                         ChessCom.ChessComForm.freezBoard = false;
                        if (C != 0)
                        {
                            MessageBox.Show("خطای بحرانی!");
                            return;
                        }
                        W = false;
                        B = true;
                    }
                    else
                    {
                        int C = 0;
                        C += F.Play(BBS.rf, BBS.cf);
                        C += F.Play(BBS.rs, BBS.cs);
                        C += S.Play(BBS.rf, BBS.cf);
                        C += S.Play(BBS.rs, BBS.cs);
                        C += BBS.Play(BBS.rf, BBS.cf);
                        C += BBS.Play(BBS.rs, BBS.cs);
                        BBS.rf = -1;
                        BBS.cf = -1;
                        BBS.rs = -1;
                        BBS.cs = -1;
                        I++;
                        ChessCom.ChessComForm.freezBoard = false;
                        if (C != 0)
                        {
                            MessageBox.Show("خطای بحرانی!");
                            return;
                        }
                        W = true;
                        B = false;
                    }


                } while (A != "" && A.Length > 3);
                MessageBox.Show("یک بازی ذخیره شد");
            } while (SS.Length > 0);
            MessageBox.Show("بازی ها تمام شد.");

        }
        public void PlayTeachPGNConvertedToChessBase()
        {
            Game game = null;
            int I = 0;

            do
            {

                String A = gameDb.Games[I].MoveText.ToString();
                PgnGames = A;
                do
                {


                    do
                    {
                        string z = "";
                        try
                        {
                            z = A.Substring(A.IndexOf(I.ToString() + ". "), A.IndexOf((I + 1).ToString() + ". ") - A.IndexOf(I.ToString() + ". "));
                            A = A.Replace(z, "");

                        }
                        catch (Exception t)
                        {
                            return;
                        }
                        if (W)
                        {
                            z = z.Remove(0, 2);
                            string b = z.Substring(0, z.IndexOf(" "));
                            if (SetRowColumn(b) == -10 || SetRowColumn(b) == 0)
                                Application.Exit();
                        }
                        else
                        {
                            string b = z.Substring(1, z.IndexOf(" "));
                            if (SetRowColumn(b) == -10 || SetRowColumn(b) == 0)
                                Application.Exit();

                        }
                    } while (BBS.rf == -1 || BBS.cf == -1 || BBS.rs == -1 || BBS.cs == -1);
                    if (W)
                    {
                        int C = 0;
                        C += F.Play(BBS.rf, BBS.cf);
                        C += F.Play(BBS.rs, BBS.cs);

                        C += S.Play(BBS.rf, BBS.cf);
                        C += S.Play(BBS.rs, BBS.cs);

                        C += BBS.Play(BBS.rf, BBS.cf);
                        C += BBS.Play(BBS.rs, BBS.cs);
                        BBS.rf = -1;
                        BBS.cf = -1;
                        BBS.rs = -1;
                        BBS.cs = -1;
                        ChessCom.ChessComForm.freezBoard = false;
                        if (C != 0)
                        {
                            MessageBox.Show("خطای بحرانی!");
                            return;
                        }
                        W = false;
                        B = true;
                    }
                    else
                    {
                        int C = 0;
                        C += F.Play(BBS.rf, BBS.cf);
                        C += F.Play(BBS.rs, BBS.cs);
                        C += S.Play(BBS.rf, BBS.cf);
                        C += S.Play(BBS.rs, BBS.cs);
                        C += BBS.Play(BBS.rf, BBS.cf);
                        C += BBS.Play(BBS.rs, BBS.cs);
                        BBS.rf = -1;
                        BBS.cf = -1;
                        BBS.rs = -1;
                        BBS.cs = -1;
                        I++;
                        ChessCom.ChessComForm.freezBoard = false;
                        if (C != 0)
                        {
                            MessageBox.Show("خطای بحرانی!");
                            return;
                        }
                        W = true;
                        B = false;
                    }


                } while (A != "" && A.Length > 4);
                MessageBox.Show("یک بازی ذخیره شد");
            } while (I < gameDb.Games.Count);
            MessageBox.Show("بازی ها تمام شد.");

        }
        private void button3_Click(object sender, EventArgs e)
        {
            frize = true;
          
            F = new ChessFirst.ChessFirstForm();
            F.ComStop = true;
            F.Show();
            S = (new RefrigtzChessPortable.RefrigtzChessPortableForm());
            S.ComStop = true;
            S.Show();
            do { System.Threading.Thread.Sleep(2000); } while (!(S.LoadP || F.LoadP));
            F.Hide();
            S.Hide();
            W = true;
            B = false;
            BB = new ChessCom.ChessComForm();
            BB.Show();
            var th = Task.Factory.StartNew(() => Play());

            frize = false;

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            frize = true;

            F = new ChessFirst.ChessFirstForm();
            F.ComStop = true;
            F.Show();
            S = (new RefrigtzChessPortable.RefrigtzChessPortableForm());
            S.ComStop = true;
            S.Show();
            do { System.Threading.Thread.Sleep(2000); } while (!(S.LoadP || F.LoadP));
            F.Hide();
            S.Hide();
            W = true;
            B = false;
            BBS = new ChessCom.ChessComForm();
            BBS.Show();
            var th = Task.Factory.StartNew(() => PlayTeach());

            frize = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frize = true;
            openFileDialog1.Filter = "PGN|*.pgn";
            openFileDialog1.ShowDialog();

             reader = new PgnReader();
             gameDb = reader.ReadFromFile(openFileDialog1.FileName);

            MessageBox.Show("PGN load completed.");
            //PlayTeachPGNConvertedToChessBase();

            F = new ChessFirst.ChessFirstForm();
            F.ComStop = true;
            F.Show();
            S = (new RefrigtzChessPortable.RefrigtzChessPortableForm());
            S.ComStop = true;
            S.Show();
            do { System.Threading.Thread.Sleep(2000); } while (!(S.LoadP || F.LoadP));
            F.Hide();
            S.Hide();
            W = true;
            B = false;
            BBS = new ChessCom.ChessComForm();
            BBS.Show();
            var th = Task.Factory.StartNew(() => PlayTeachPGNConvertedToChessBase());

            frize = false;

        }
    }
}
