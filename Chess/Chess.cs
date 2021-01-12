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
        public bool W = true;
        public bool B = true;
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

        private void button3_Click(object sender, EventArgs e)
        {
            frize = true;
            (new ChessCom.ChessComForm()).ShowDialog();
            frize = false;

        }
    }
}
