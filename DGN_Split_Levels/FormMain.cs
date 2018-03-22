using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Application = MicroStationDGN.Application;

namespace DGN_Split_Levels
{
    public partial class FormMain : Form
    {
        private Application oMstn;

        public FormMain(Application oMstn)
        {
            this.oMstn = oMstn;
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            buttonRozdziel.Text = @"Rozdziel";
            textBoxFileName.Text = @"c:\karty_map.dgn";
        }

        private void ButtonRozdziel_Click(object sender, EventArgs e)
        {
            string iuputFileName = textBoxFileName.Text; // nazwa pliku wejściowego DGN
            string logFileName = iuputFileName.Substring(0, iuputFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".log"; // nazwa pliku LOG

            StreamWriter logFile = new StreamWriter(new FileStream(logFileName, FileMode.Create), Encoding.UTF8);
            


            logFile.Close();

        }


    }
}
