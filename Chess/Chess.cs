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
        public bool WB = true;
        public bool frize = true;
        public Chess()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (WB) {
                frize = true;
                (new ChessFirst.ChessFirstForm()).ShowDialog();
                frize = false;
                WB = false;
            }
            else
            {
                frize = true;
                (new RefrigtzChessPortable.RefrigtzChessPortableForm()).ShowDialog();
                frize = false;
                WB = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (WB)
            {
                frize = true;
                (new ChessFirst.ChessFirstForm()).ShowDialog();
                frize = false;
                WB = false;
            }
            else
            {
                frize = true;
                (new RefrigtzChessPortable.RefrigtzChessPortableForm()).ShowDialog();
                frize = false;
                WB = true;
            }
        }
    }
}
