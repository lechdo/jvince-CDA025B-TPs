using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Module03TP
{
    public class Program
    {
        private const string INTITULE_LISTE = "Liste({0}), {1} :";
        private const string INTITULE_UNITE = "{0}, {1} : {2}\n";

        public static void Main(string[] args)
        {
            Data data = Data.Instance;

            var prenomsAuteurs = data.ListeAuteurs.Select(n => n.Nom);
            Console.WriteLine(String.Format(INTITULE_LISTE, nameof(Auteur), "nom commence par 'G'"));
            foreach (string prenom in prenomsAuteurs)
            {
                Console.WriteLine(prenom);
            }


            IGrouping<Auteur, Livre> auteurMaxLivres = data.ListeLivres.GroupBy(l => l.Auteur).OrderByDescending(l => l.Count()).FirstOrDefault();
            Console.WriteLine(String.Format(INTITULE_UNITE, nameof(Auteur), "le plus de livres à son nom", auteurMaxLivres.Key.Nom));


            double nombreMoyenPageParLivres = data.ListeLivres.Average(n => n.NbPages);
            Console.WriteLine(String.Format("Nb moyen de pages par livre : {0}", nombreMoyenPageParLivres));


            Livre livreMaxPages = data.ListeLivres.Select(n => n).OrderByDescending(n => n.NbPages).FirstOrDefault();
            Console.WriteLine(String.Format(INTITULE_UNITE, nameof(Livre), "nombre de page le plus élevé", livreMaxPages.Titre));

            var listeMontantGainAuteurs = data.ListeAuteurs.SelectMany(n => n.Factures).Select(f => f.Montant);
            Console.WriteLine(String.Format(INTITULE_UNITE, nameof(Auteur), "gain moyen", listeMontantGainAuteurs.Sum() /listeMontantGainAuteurs.Count()));


            foreach (Auteur auteur in data.ListeAuteurs)
            {
                Console.WriteLine(">>> Auteur : " + auteur.Nom);
                var listeLivreAuteur = data.ListeLivres.Where(n => n.Auteur.Equals(auteur));
                foreach (Livre livre in listeLivreAuteur)
                {
                    Console.WriteLine(livre.Titre);
                }
            }

             List<string> listeLivreOrdAlpha = data.ListeLivres.Select(l => l.Titre).OrderBy(n => n).ToList();
            Console.WriteLine(">>> Liste des livres, ordre alphanumérique");
            foreach (string titre in listeLivreOrdAlpha)
            {
                Console.WriteLine(titre);
            }

            List<Livre> listeLivreNbPageSupMoyenne = data.ListeLivres
                .Where(l => l.NbPages > nombreMoyenPageParLivres)
                .OrderByDescending(n => n).ToList();
            Console.WriteLine(INTITULE_LISTE, nameof(Livre), "nb pages supérieur à la moyenne");
            foreach (Livre livre in listeLivreNbPageSupMoyenne)
            {
                Console.WriteLine(livre.Titre);
            }

            Auteur auteurMoinsDeLivres = data.ListeAuteurs.OrderBy(a => data.ListeLivres.Count(l => l.Auteur == a)).FirstOrDefault();
            Console.WriteLine(String.Format(INTITULE_UNITE, nameof(Auteur), "écrit le moin de livres", auteurMoinsDeLivres.Nom)); 

            Console.ReadKey();



        }
            

    }   
 }


