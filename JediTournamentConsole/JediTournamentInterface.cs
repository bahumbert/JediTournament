using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer;
using EntitiesLayer;

namespace JediTournamentConsole
{
    class JediTournamentInterface
    {
        /// <summary>
        /// Business manager.
        /// </summary>
        private JediTournamentManager m_manager;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public JediTournamentInterface()
        {
            this.m_manager = new JediTournamentManager();
        }

        /// <summary>
        /// Lance l'application console (Boucle principale du menu).
        /// </summary>
        public void launchMenu()
        {
            ConsoleKeyInfo key;

            // Boucle principale du menu
            do
            {
                // Affichage menu
                displayMenu();

                // Récupère la touche saisie
                key = Console.ReadKey();

                // Gestion de l'appui des touches
                handleKey(key);
            } while (key.Key != ConsoleKey.Q);
        }

        /// <summary>
        /// Affiche le menu de l'application console.
        /// </summary>
        private void displayMenu()
        {
            Console.Clear();
            Console.WriteLine("Menu - Jedi Tournament Console\n");
            Console.WriteLine("1 - Afficher la liste des stades.");
            Console.WriteLine("2 - Afficher la liste des matchs.");
            Console.WriteLine("3 - Afficher la liste des matchs (Sith Vs Sith et Stade >200 places).");
            Console.WriteLine("4 - Afficher la liste des siths.");
            Console.WriteLine("5 - Afficher la liste des jedis avec plus de 3 de force et 50 points de vie.");
            //Console.WriteLine("6 - Créer ou Reset la BDD.");
            Console.WriteLine("Q - Quitter.");

            Console.WriteLine("\nVotre choix : ");
        }

        /// <summary>
        /// Gère les interactions possible par les touches.
        /// </summary>
        /// <param name="key">Touche appuyée.</param>
        private void handleKey(ConsoleKeyInfo key)
        {
            Console.Clear();

            switch (key.Key)
            {
                case ConsoleKey.NumPad1:
                    displayStades();
                    break;
                case ConsoleKey.NumPad2:
                    displayMatchs();
                    break;
                case ConsoleKey.NumPad3:
                    displaySithsMatchs();
                    break;
                case ConsoleKey.NumPad4:
                    displaySiths();
                    break;
                case ConsoleKey.NumPad5:
                    displayJedis3F50PV();
                    break;
                /*case ConsoleKey.NumPad6:
                    CreateDataBase();
                    break;*/
                /*case ConsoleKey.Z:
                    addUser();
                    break;*/
            }
        }

        /// <summary>
        /// Affiche la liste des stades.
        /// </summary>
        private void displayStades()
        {
            Console.WriteLine("Liste des stades :\n");

            List<string> stades = m_manager.getStades();
            foreach (string s in stades)
            {
                Console.WriteLine("Stade de " + s + ".");
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Affiche la liste des matchs.
        /// </summary>
        private void displayMatchs()
        {
            Console.WriteLine("Liste des matchs :\n");

            List<Match> matchs = m_manager.getAllMatchs();
            foreach (Match m in matchs)
            {
                displayMatch(m);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Affiche la liste des matchs opposants 2 jedis siths dans un stade de plus de 200 places.
        /// </summary>
        private void displaySithsMatchs()
        {
            Console.WriteLine("Liste des matchs opposant 2 siths dans un stade à plus de 200 places :\n");

            List<Match> matchs = m_manager.getMatch200P2S();
            foreach (Match m in matchs)
            {
                displayMatch(m);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Affiche les informations relatives à un match.
        /// </summary>
        /// <param name="m">Match à afficher.</param>
        private void displayMatch(Match m)
        {
            Console.WriteLine("Math opposant " + m.Jedi1.Nom + " a " + m.Jedi2.Nom);
            Console.WriteLine("\\-> en " + m.PhaseTournoi.ToString());
            Console.WriteLine("\\-> Stade de " + m.Stade.Planete);
            Console.WriteLine("\\-> " + (m.IdJediVainqueur == -1 ? "Le match n'a pas encore été joué" : "Remporté par " + (m.IdJediVainqueur == m.Jedi1.ID ? m.Jedi1.Nom : m.Jedi2.Nom)) + "\n");
        }

        /// <summary>
        /// Affiche le liste des siths.
        /// </summary>
        private void displaySiths()
        {
            Console.WriteLine("Liste des Siths :\n");

            List<string> siths = m_manager.getSithsNames();
            foreach (string s in siths)
            {
                Console.WriteLine(" - " + s);
            }

            Console.ReadKey();
        }

        /// <summary>
        /// Affiche la liste des jedis avec plus de 3 points de force et plus de 50 points de vie.
        /// </summary>
        private void displayJedis3F50PV()
        {
            Console.WriteLine("Liste des jedis avec plus de 3 en Force et plus de 50 points de vie :\n");

            List<string> jedis = m_manager.getJedis3F50PV();
            foreach (string j in jedis)
            {
                Console.WriteLine(" - " + j);
            }

            Console.ReadKey();
        }

        private void CreateDataBase()
        {
            Console.WriteLine("Création de la BDD :\n");

            m_manager.createDataBase();
            
            Console.ReadKey();
        }

        private void addUser()
        {
            m_manager.addUser();
        }
    }
}
