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
        private readonly string DEF_inputFileATxt = @"D:\c#\projetPerso\WpfTacheExtraction\WpfTacheExtraction\Fichiers\inputA24.txt";
        private readonly string DEF_inputFileHTxt = @"D:\c#\projetPerso\WpfTacheExtraction\WpfTacheExtraction\Fichiers\inputH25.txt";
        private readonly string DEF_outFileTxt = @"D:\c#\projetPerso\WpfTacheExtraction\WpfTacheExtraction\Fichiers\ouputA24H25.txt";

        public MainWindow() {
            InitializeComponent();
            extraire();
            Application.Current.Shutdown();
        }

        private void extraire() {
            Extracteur extracteur = new Extracteur(@"420-", DEF_outFileTxt);
            extracteur.lireAppend(DEF_inputFileATxt);
            extracteur.lireAppend(DEF_inputFileHTxt);
            extracteur.extraireSessionAvecCampus();
        }
    }
}
