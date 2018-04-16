using MicroStationDGN;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace DGN_Split_Levels
{
    public partial class FormMain : Form
    {
        private MicroStationDGN.Application _oMstn;

        public FormMain()
        {
            InitializeComponent();
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            buttonRozdziel.Text = @"Rozdziel";
            textBoxFileName.Text = @"c:\321603_4000.dgn";

            _oMstn = new MicroStationDGN.Application();
        }

        private void ButtonRozdziel_Click(object sender, EventArgs e)
        {
            // nazwa pliku wejściowego DGN
            string inputFileName = textBoxFileName.Text; 
            
            // nazwa pliku LOG
            string logFileName = inputFileName.Substring(0, inputFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".log"; 
            
            // lista wartstw z pliku DGN
            List<string> levelsList = new List<string>();

            StreamWriter logFile = new StreamWriter(new FileStream(logFileName, FileMode.Create), Encoding.UTF8);

            logFile.WriteLine("Rozdzielanie pliku: {0}\n", inputFileName);
            
            DesignFile inputFile = _oMstn.OpenDesignFile(inputFileName, false);

            String outputPath = inputFile.Path + "\\" + inputFile.Name.Substring(0, inputFile.Name.LastIndexOf(".", StringComparison.Ordinal));

            #region Tworzenie katalogu i kasownie plików
            
            if (Directory.Exists(outputPath))
            {
                DirectoryInfo di = new DirectoryInfo(outputPath);

                foreach (FileInfo file in di.GetFiles())
                {
                    try
                    {
                        file.Delete();
                    }
                    catch (IOException ex)
                    {
                        MessageBox.Show(ex.Message, @"Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(outputPath);
            }

            #endregion

            foreach (Level levelIn in inputFile.Levels)
            {
                string levelInName = levelIn.Name;

                if (levelIn.IsInUse)
                {
                    levelsList.Add(levelInName);
                    File.Copy(inputFileName, outputPath + "\\" + levelInName + ".dgn");
                    logFile.WriteLine("warstwa: {0}\t+", levelInName);
                }
                else
                {
                    logFile.WriteLine("warstwa: {0}", levelInName);
                }
            }

            inputFile.Close();

            List<string> levelToRemove = new List<string>();

            foreach (string levelName in levelsList)
            {
                inputFile = _oMstn.OpenDesignFile(outputPath + "\\" + levelName + ".dgn", false);

                _oMstn.CadInputQueue.SendKeyin("delete unused levels");

                foreach (Level levelIn in inputFile.Levels)
                {
                    if (levelIn.Name != levelName)
                    {
                        _oMstn.CadInputQueue.SendKeyin("level element delete \"" + levelIn.Name + "\"");
                    }
                }

                _oMstn.CadInputQueue.SendKeyin("delete unused levels");

                inputFile.Levels.Rewrite();

                inputFile.Save();

                inputFile.Close();
            }

            logFile.Close();

        }

        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _oMstn.Quit();
        }
    }
}
