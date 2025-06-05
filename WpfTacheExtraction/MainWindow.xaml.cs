using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfTacheExtraction.scripts;

namespace WpfTacheExtraction
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private static char pathSep = System.IO.Path.DirectorySeparatorChar;
        private static string baseDir = @"_fichiers" + pathSep;

        private static string tacheID = "A25H26_";
        private static string inputTxtNameA = @"inputA.txt";
        private static string inputTxtNameH = @"inputH.txt";
        private static string outputTxtName = @"output.txt";

        private string fichierPath;
        private string tachePath;

        private string dateDuJour;

        private string inputFileAPath;
        private string inputFileHPath;
        private string outputFilePath;



        public MainWindow() {
            InitializeComponent();
            initInfo();
            sauverInputs();
            extraire();

            Util.ouvrirFichierTexteEditeurDefaut(outputFilePath);

            Application.Current.Shutdown();
        }

        private void sauverInputs()
        {
            string prefix;

            prefix = tachePath + pathSep + tacheID + dateDuJour;
            File.Copy(inputFileAPath, prefix + inputTxtNameA, true);  // Copie le fichier (écrase s’il existe déjà)
            File.Copy(inputFileHPath, prefix + inputTxtNameH, true);  // Copie le fichier (écrase s’il existe déjà)
        }

        private void initInfo()
        {
            dateDuJour = DateTime.Now.ToString("yyyy-MM-dd");

            fichierPath = Util.GetProjectPath() + pathSep + baseDir;

            tachePath = Util.CreerRepertoireAvecDate(fichierPath, tacheID);

            inputFileAPath = fichierPath + inputTxtNameA;
            inputFileHPath = fichierPath + inputTxtNameH;
            outputFilePath = tachePath + pathSep + tacheID + dateDuJour + '_' + outputTxtName;
        }

        private void extraire() {
            Extracteur extracteur = new Extracteur(@"420-", outputFilePath);
            extracteur.lireAppend(inputFileAPath);
            extracteur.lireAppend(inputFileHPath);
            extracteur.extraireSessionAvecCampus();
        }
    }
}
