using System;
using System.Collections.Generic;
using System.IO;

namespace jeux_du_pendu
{
    class Program
    {

        static void AfficherMot(string mot, List<char> lettres)
        {
            for(int i = 0; i < mot.Length; i++)
            {
                char lettre = mot[i];
                if (lettres.Contains(lettre))
                {
                    Console.Write(lettre + " ");
                }
                else
                {
                    Console.Write("_ ");
                }
            }
            Console.WriteLine();
            
        }


        static bool ToutesLettresDevine(string mots, List<char> lettres)
        {
            
        foreach(var lettre in lettres)
            {
                mots = mots.Replace(lettre.ToString(), "");
            }
            if(mots.Length == 0)
            {
                return true;
            }
            return false;
        }





        static char DemanderUneLettre(string message = "Choisissez une lettre : ")
        {


            while (true)
            {
                Console.Write(message);
                string reponse = Console.ReadLine();
                if (reponse.Length == 1)
                {
                    reponse = reponse.ToUpper();
                    return reponse[0];
                }
                Console.WriteLine("ERREUR: vous devez rentrer une lettre");

            }

        }




        static void DevinerMot(string mot)
        {

            var lettreDeviner = new List<char>();
            const int NB_VIES = 6;
            int vieRestant = NB_VIES;

            var lettreExclue = new List<char>();
            while (vieRestant > 0)
            {
                Console.WriteLine(Ascii.PENDU[NB_VIES - vieRestant]);
                Console.WriteLine();

                AfficherMot(mot, lettreDeviner);
                Console.WriteLine();
                var lettre = DemanderUneLettre();
               Console.Clear();


                if (mot.Contains(lettre))
                {

                    Console.WriteLine("Cette lettre est dans le mot ");
                    lettreDeviner.Add(lettre);

                    // GAGNE
                    if (ToutesLettresDevine(mot, lettreDeviner))

                    {
                        // Console.WriteLine("Vous avez gagné");
                        // return;
                        break;

                    }

                }
                else
                {
                    if (!lettreExclue.Contains(lettre))
                    {
                        vieRestant--;
                        lettreExclue.Add(lettre);
                    }
               
                    
                    Console.WriteLine("Il vous restes : "+ vieRestant +" vies"); 
                }
                if(lettreExclue.Count > 0)
                {
                    Console.WriteLine("Le mot ne contient pas les lettres : " + String.Join(", ", lettreExclue));

                }
                


                Console.WriteLine();

            }
            Console.WriteLine(Ascii.PENDU[NB_VIES - vieRestant]);

            if (vieRestant == 0)
            {
                Console.WriteLine("Vous avez perdu le mot était : " + mot);


            }
            else

            {
               
                Console.WriteLine("Vous avez gagné le mot était : "+mot);
                Console.WriteLine();
               
            }
        }

        static string[] ChargerLesMots(string nomFichier)
        {
            try
            {
                return File.ReadAllLines(nomFichier);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Erreur de lecture du fichier : "+nomFichier + " ("+ex.Message + ")");
            }
            return null;
        }


        static bool Rejouer()
        {
          
            char reponse = DemanderUneLettre("Voulez vous rejouer ? (o/n) : ");

            if ((reponse == 'o') || (reponse == 'O'))
            {
                // Rejouer 
                return true;
            }
            else if ((reponse == 'n') || (reponse == 'N'))
            {
                return false;
            }
            else
            {
                Console.WriteLine("Erreur : Vous devez répondre avec o ou n");
                return Rejouer();
            }
        }


        static void Main(string[] args)
        {

            var mots = ChargerLesMots("mots.txt");



            if((mots == null) || (mots.Length == 0))
            {
                Console.WriteLine("La liste de mots est vide");
            }
            else
            {


                while (true)
                {


                    Random rand = new Random();
                    int i = rand.Next(mots.Length);
                    string mot = mots[i].Trim().ToUpper();


                    DevinerMot(mot);

                    if (!Rejouer())
                    {
                        break;
                    }
                    Console.Clear();
                }
                Console.Clear();
                Console.WriteLine("Merci et à bientôt !");
            }
             
            // char lettre = DemanderUneLettre();
            // AfficherMot(mot, new List<char> { lettre });

           
        }
    }
}
