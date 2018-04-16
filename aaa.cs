using MicroStationDGN;
using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Application = System.Windows.Forms.Application;

namespace WindowsFormsApp1
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            buttonRozdziel.Text = @"Rozdziel";
            textBoxFileName.Text = @"c:\karty_map.dgn";
        }

        private void ButtonRozdziel_Click(object sender, EventArgs e)
        {
            string iFileName = textBoxFileName.Text;

            StreamWriter logFile = new StreamWriter(new FileStream(iFileName.Substring(0, iFileName.LastIndexOf(".", StringComparison.Ordinal)) + ".log", FileMode.Create), Encoding.UTF8);

            logFile.WriteLine("Rozdielanie pliku: {0}\n", iFileName);

            MicroStationDGN.Application oMstn = new MicroStationDGN.Application();

            DesignFile iFile = oMstn.OpenDesignFile(iFileName, true);

            if (iFile.Models.Count > 1)
            {
                MessageBox.Show(@"Ilość modeli w pliku większa niż 1 !");
                Application.Exit(); 
            }

            ModelReference modelIn = iFile.Models[1];

            foreach (Level levelIn in iFile.Levels)
            {
                string levelInName = levelIn.Name;

                if (levelIn.IsInUse)
                {
                    logFile.WriteLine("warstwa {0}: uzywana", levelInName);

                    File.Copy(iFileName, @"C:\temp\" + levelIn.Name + ".dgn", true);

                    DesignFile oFile = oMstn.OpenDesignFileForProgram(@"C:\temp\" + levelIn.Name + ".dgn", false);

                    ModelReference modelOut = oFile.Models[1];

                    foreach (Level levelOut in oFile.Levels)
                    {
                        string levelOutName = levelOut.Name;

                        if (levelOutName != levelInName)
                        {

                            if (levelOut.IsInUseWithinModel(modelOut))
                            {
                                ElementScanCriteria elementScanCriteriaOut = new ElementScanCriteriaClass();
                                elementScanCriteriaOut.ExcludeAllLevels();
                                elementScanCriteriaOut.IncludeLevel(levelOut);    

                                ElementEnumerator elementEnumeratorOut = modelOut.Scan(elementScanCriteriaOut);

                                //Array elementOut = elementEnumeratorOut.BuildArrayFromContents();

                                while (elementEnumeratorOut.MoveNext())
                                {
                                    modelOut.RemoveElement(elementEnumeratorOut.Current);
                                }
                            }

                            if (!levelOut.IsInUse && levelOut.Number != 0) oFile.DeleteLevel(levelOut);

                        }
                        
                    }

                    oFile.RewriteLevels();

                    oFile.Save();

                    oFile.Close();

                }
                else
                {
                    logFile.WriteLine("warstwa {0}: brak", levelInName);
                }

            }

            logFile.Close();

            iFile.Close();

            oMstn.Quit();
        }
    }
}
