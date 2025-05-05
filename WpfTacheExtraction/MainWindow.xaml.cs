using System;
using System.Collections.Generic;
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

namespace WpfTacheExtraction {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        private string DEF_inputFileATxt = @"_fichiers\Cours_A25.txt";
        private string DEF_inputFileHTxt = @"_fichiers\Cours_H26VB.txt";
        private string DEF_outFileTxt = @"_fichiers\ouputA25H26VB.txt";

        public MainWindow() {
            InitializeComponent();
            initRep();
            extraire();
            Application.Current.Shutdown();
        }

        private void initRep()
        {
            string? projetctPath = Util.GetProjectPath() + System.IO.Path.DirectorySeparatorChar;

            DEF_inputFileATxt = projetctPath + DEF_inputFileATxt;
            DEF_inputFileHTxt = projetctPath + DEF_inputFileHTxt;
            DEF_outFileTxt = projetctPath + DEF_outFileTxt;
        }

        private void extraire() {
            Extracteur extracteur = new Extracteur(@"420-", DEF_outFileTxt);
            extracteur.lireAppend(DEF_inputFileATxt);
            extracteur.lireAppend(DEF_inputFileHTxt);
            extracteur.extraireSessionAvecCampus();
        }
    }
}
