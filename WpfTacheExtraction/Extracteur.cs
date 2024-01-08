using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfTacheExtraction {
    internal class Extracteur {
        public const string DEF_commenceAvecPgm = @"420-";
        public const string DEF_inputTxtUTF8 = @"..\..\input.txt";
        public const string DEF_outputTxt = @"..\..\ouput.txt";

        public const string DEF_commenceAvecSession = @"^(Automne|Hiver)";
        public const string DEF_commenceAvecCampus = @"^Discipline\s+(CHA|QUE)";

        public const string DEF_seperateur = @"|";

        private readonly string commenceAvec;
        private readonly string outputTxt;

        private List<string> listLignes = new List<string>();

        public Extracteur(string commenceAvec = DEF_commenceAvecPgm, string outputTxt = DEF_outputTxt) {
            this.commenceAvec = commenceAvec;
            this.outputTxt = outputTxt;
        }

        public void extraireSessionAvecCampus() {
            using (StreamWriter writer = new StreamWriter(outputTxt)) {
                string session = "";
                string campus = "";
                string ligne;
                foreach (string line in listLignes) {
                    ligne = line.Trim();
                    if (Regex.IsMatch(ligne, DEF_commenceAvecSession)) {
                        session = extrairePremiereOccurence(ligne, DEF_commenceAvecSession);
                        session = session[0] + DEF_seperateur;
                    }
                    else if (Regex.IsMatch(ligne, DEF_commenceAvecCampus)) {
                        campus = extrairePremiereOccurence(ligne, DEF_commenceAvecCampus);
                        campus = campus.Substring(campus.Length - 3) + DEF_seperateur;
                    }
                    else if (ligne.StartsWith(commenceAvec)) {
                        writer.WriteLine(session + campus + ligne);
                    }
                }
            }
        }
        public void extraireAvecCampus() {
            using (StreamWriter writer = new StreamWriter(outputTxt)) {
                string campus = "";
                string ligne;
                foreach (string line in listLignes) {
                    ligne = line.Trim();
                    if (Regex.IsMatch(ligne, DEF_commenceAvecCampus)) {
                        campus = extrairePremiereOccurence(ligne, DEF_commenceAvecCampus);
                        campus = campus.Substring(campus.Length - 3);
                    }
                    if (ligne.StartsWith(commenceAvec)) {
                        writer.WriteLine(campus + "-" + ligne);
                    }
                }
            }
        }
        public void extraire() {
            using (StreamWriter writer = new StreamWriter(outputTxt)) {
                string ligne;
                foreach (string line in listLignes) {
                    ligne = line.Trim();
                    if (ligne.StartsWith(commenceAvec)) {
                        writer.WriteLine(ligne);
                    }
                }
            }
        }

        public string[] lireAppend(string fichierIn) {
            string[] lignes;
            lignes = File.ReadAllLines(fichierIn);
            listLignes.AddRange(lignes);
            return lignes;
        }

        private string extrairePremiereOccurence(string text, string pattern) {
            Regex myRegex = new Regex(pattern, RegexOptions.IgnoreCase);
            string strMatch = "";

            Match m = myRegex.Match(text);   // m is the first match
            if (m.Success) {
                strMatch = m.Value;
                //                m = m.NextMatch();              // more matches
            }
            return strMatch;
        }
    }
}
