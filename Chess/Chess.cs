using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chess
{
    public partial class Chess : Form
    {
        RefrigtzChessPortable.RefrigtzChessPortableForm S = null;
        ChessFirst.ChessFirstForm F = null;
        public bool W = true;
        public bool B = true;
        ChessCom.ChessComForm BB = null;
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
                if(W)
                {
                    F.Play(-1, -1);
                    S.Play(F.R.CromosomRowFirst, F.R.CromosomColumnFirst);
                    S.Play(F.R.CromosomRow, F.R.CromosomColumn);
                    BB.Play(F.R.CromosomRowFirst, F.R.CromosomColumnFirst);
                    BB.Play(F.R.CromosomRow, F.R.CromosomColumn);
                    W = false;
                    B = true;
                }
                else
                {
                    S.Play(-1, -1);
                    F.Play(S.R.CromosomRowFirst, S.R.CromosomColumnFirst);
                    F.Play(S.R.CromosomRow, S.R.CromosomColumn);
                    BB.Play(S.R.CromosomRowFirst, S.R.CromosomColumnFirst);
                    BB.Play(S.R.CromosomRow, S.R.CromosomColumn);
                    W = true;
                    B = false;
                }


            } while (true);

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
    }
}
