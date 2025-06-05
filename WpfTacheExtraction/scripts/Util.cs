using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using System.IO;
using System.Diagnostics;

namespace WpfTacheExtraction.scripts
{
    internal class Util
    {
        public static string? GetProjectPath([CallerFilePath] string sourceFilePath = "")
        {
            return Path.GetDirectoryName(sourceFilePath);
        }

        public static string CreerRepertoireAvecDate(string cheminParent, string prefix = "")
        {
            // Formater la date du jour
            string nomDossier = DateTime.Now.ToString("yyyy-MM-dd");

            // Créer le chemin complet
            string cheminComplet = Path.Combine(cheminParent, prefix + nomDossier);

            // Créer le répertoire s'il n'existe pas déjà
            if (!Directory.Exists(cheminComplet))
            {
                Directory.CreateDirectory(cheminComplet);
            }

            return cheminComplet;
        }


        /// <summary>
        /// ouvrir un fichier texte dans l’éditeur par défaut du système (comme le Bloc-notes sur Windows)
        /// </summary>
        /// <param name="cheminFichier"></param>
        /// <returns></returns>
        public static void ouvrirFichierTexteEditeurDefaut(string cheminFichier)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = cheminFichier,
                UseShellExecute = true
            };

            Process.Start(psi);
        }
    }
}
