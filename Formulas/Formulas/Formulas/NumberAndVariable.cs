using System;
using System.Windows.Forms;

namespace NumberVar
{
    public partial class NumberAndVariable : Form
    {
        private readonly Formulas.Equation EqationVariable = null;
        private static string Contained = "";
        public NumberAndVariable(ref NumberAndVariable E, ref Formulas.Equation A)
        {
            InitializeComponent();
            E = this;
            EqationVariable = A;



        }

        private void NumberAndVariable_Load(object sender, EventArgs e)
        {

        }

        private void NumberAndVariable_Load_1(object sender, EventArgs e)
        {

        }
        public string GetContained()
        {
            return Contained;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Contained = textBox1.Text;
        }

    }
}