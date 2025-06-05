using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;

namespace WpfTacheExtraction.scripts
{
    internal class Extracteur
    {
        public const string DEF_commenceAvecPgm = @"420-";
        public const string DEF_inputTxtUTF8 = @"..\..\input.txt";
        public const string DEF_outputTxt = @"..\..\ouput.txt";

        public const string regex_commenceAvecSession = @"^(Automne|Hiver)";
        public const string regex_commenceAvecCampus = @"^Discipline\s+(CHA|QUE)";
        public const string regex_groupe = @"\t\d{5}$"; //regex détecter une tabulation suivie de 5 chiffres à la fin d'une ligne

        public const string seperateur = @"|";

        private readonly string commenceAvecPgm;
        private readonly string outputTxt;

        private List<string> listLignes = new List<string>();


        // A|CHA|420-005-LI - Micro-informatique de la mécanique du bâtiment (Pondération: 1-2-0, Nb. étudiants: 20)|1
        // session|campus|cours|nbGroupe

        public Extracteur(string commenceAvecPgm = DEF_commenceAvecPgm, string outputTxt = DEF_outputTxt)
        {
            this.commenceAvecPgm = commenceAvecPgm;
            this.outputTxt = outputTxt;
        }

        public void extraireSessionAvecCampus()
        {
            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                string session = "";
                string campus = "";
                string ligne;
                string ligneCours;
                int nbGroupe;

                for (int i = 0; i < listLignes.Count;)
                {
                    ligne = listLignes[i++].Trim();
                    if (Regex.IsMatch(ligne, regex_commenceAvecSession))
                    {
                        session = extrairePremiereOccurence(ligne, regex_commenceAvecSession);
                        session = session[0] + seperateur;
                    }
                    else if (Regex.IsMatch(ligne, regex_commenceAvecCampus))
                    {
                        campus = extrairePremiereOccurence(ligne, regex_commenceAvecCampus);
                        campus = campus.Substring(campus.Length - 3) + seperateur;
                    }
                    else if (ligne.StartsWith(commenceAvecPgm))
                    {
                        ligneCours = ligne;
                        nbGroupe = 0;

                        do //compter les groupes
                        {
                            ligne = listLignes[i].Trim();
                            if (Regex.IsMatch(ligne, regex_groupe))
                            {
                                nbGroupe++;
                            }
                            if (!ligne.StartsWith(commenceAvecPgm) && !Regex.IsMatch(ligne, regex_commenceAvecSession) && !Regex.IsMatch(ligne, regex_commenceAvecCampus))
                            {
                                i++;
                            }
                        } while (i < listLignes.Count && !ligne.StartsWith(commenceAvecPgm) && !Regex.IsMatch(ligne, regex_commenceAvecSession) && !Regex.IsMatch(ligne, regex_commenceAvecCampus));

                        writer.WriteLine(session + campus + ligneCours + seperateur + nbGroupe);

                    }
                }
            }
        }
        public void extraireAvecCampus()
        {
            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                string campus = "";
                string ligne;
                foreach (string line in listLignes)
                {
                    ligne = line.Trim();
                    if (Regex.IsMatch(ligne, regex_commenceAvecCampus))
                    {
                        campus = extrairePremiereOccurence(ligne, regex_commenceAvecCampus);
                        campus = campus.Substring(campus.Length - 3);
                    }
                    if (ligne.StartsWith(commenceAvecPgm))
                    {
                        writer.WriteLine(campus + "-" + ligne);
                    }
                }
            }
        }
        public void extraire()
        {
            using (StreamWriter writer = new StreamWriter(outputTxt))
            {
                string ligne;
                foreach (string line in listLignes)
                {
                    ligne = line.Trim();
                    if (ligne.StartsWith(commenceAvecPgm))
                    {
                        writer.WriteLine(ligne);
                    }
                }
            }
        }

        public string[] lireAppend(string fichierIn)
        {
            string[] lignes;
            lignes = File.ReadAllLines(fichierIn);
            listLignes.AddRange(lignes);
            return lignes;
        }

        private string extrairePremiereOccurence(string text, string pattern)
        {
            Regex myRegex = new Regex(pattern, RegexOptions.IgnoreCase);
            string strMatch = "";

            Match m = myRegex.Match(text);   // m is the first match
            if (m.Success)
            {
                strMatch = m.Value;
                //                m = m.NextMatch();              // more matches
            }
            return strMatch;
        }
    }
}
