using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
namespace JSONReader
{
    public partial class FormJSONReader : Form
    {
        private JSONReader Reader = null;
        private Thread t = null;
        public FormJSONReader()
        {
            InitializeComponent();
        }

        private string PersianDate(DateTime A)
        {
            string S = "";
            PersianCalendar p = new PersianCalendar();
            DateTime dmiladi = new DateTime();
            dmiladi = A;
            //string year = p.GetYear(dmiladi).ToString();
            if (A.Year != 1)
            {
                S = p.GetYear(dmiladi).ToString() + "/" + p.GetMonth(dmiladi).ToString() + "/" + p.GetDayOfMonth(dmiladi).ToString() + "/" + p.GetDayOfWeek(dmiladi).ToString() + ": " + p.GetHour(dmiladi) + ":" + p.GetMinute(dmiladi) + ":" + p.GetSecond(dmiladi);
            }

            return S;
        }
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }

            try
            {
                openFileDialogJSONFile.Filter = "JSON Files|*.JSON";
                openFileDialogJSONFile.ShowDialog();
                Reader = new JSONReader(openFileDialogJSONFile.FileName);
            }
            catch (Exception)
            {
            }

        }

        private void FormJSONReader_Load(object sender, EventArgs e)
        {
            if (t != null)
            {
                t.Abort();
            }
        }
        private void ReadJSON()
        {
            int Len = 0;
            if (Reader.OKResult)
            {
                while (Len < Reader.ItemSearch.Count)
                {
                    string Dummy = "";
                    if (radioButtonPersianDate.Checked)
                    {
                        Dummy = "timestamp_usec:  " + PersianDate(Reader.ItemSearch[Len].timestamp_usec) + " ,query_text: " + Reader.ItemSearch[Len].query_text;
                    }
                    else
                    {
                        Dummy = "timestamp_usec:  " + Reader.ItemSearch[Len].timestamp_usec.ToString() + " ,query_text: " + Reader.ItemSearch[Len].query_text;
                    }

                    textBoxReadJSON(textBoxJSON, Dummy);
                    AppendtextBoxReadJSON(textBoxJSON, "\r\n");
                    Len++;

                }
            }

        }

        private delegate void SettextBoxReadJSONCalBack(TextBox JSON, string Text);
        private void textBoxReadJSON(TextBox JSON, string Text)
        {
            try
            {
                if (InvokeRequired)
                {
                    SettextBoxReadJSONCalBack d = new SettextBoxReadJSONCalBack(textBoxReadJSON);
                    Invoke(new Action(() => JSON.Text += Text));
                }
                else
                {
                    JSON.Text += Text;
                }
            }
            catch (Exception)
            {
            }

        }

        private delegate void AppendtextBoxReadJSONCalBack(TextBox JSON, string Text);
        private void AppendtextBoxReadJSON(TextBox JSON, string Text)
        {
            try
            {
                if (InvokeRequired)
                {
                    AppendtextBoxReadJSONCalBack d = new AppendtextBoxReadJSONCalBack(textBoxReadJSON);
                    Invoke(new Action(() => JSON.AppendText(Text)));
                }
                else
                {
                    JSON.AppendText(Text);
                }
            }
            catch (Exception)
            {
            }
        }
        private void buttonRead_Click(object sender, EventArgs e)
        {
            t = new Thread(new ThreadStart(ReadJSON));
            t.Start();
        }

        private void aboutToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            (new AboutBoxJSONReader()).ShowDialog();
        }
    }
}
