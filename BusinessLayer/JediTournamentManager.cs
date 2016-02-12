using System.Collections.Generic;
using System.Linq;
using EntitiesLayer;
using System.IO;
using System.Xml.Serialization;
using DataAccessLayer;
using System.Security.Cryptography;
using System.Text;
using System;

namespace BusinessLayer
{
    public class JediTournamentManager
    {
        private DalManager m_data;    /// Gestionnaire d'interactions avec la base de données.

        /// <summary>
        /// Constructeur.
        /// </summary>
        public JediTournamentManager()
        {
            m_data = DalManager.Instance;
        }

        public int useAvailableIDMatch()
        {
           return m_data.useAvailableIdMatch();
        }

        #region "Liés aux Jedis"

        /// <summary>
        /// Donne la liste des noms des jedis siths.
        /// </summary>
        /// <returns>Liste des noms des jedis siths.</returns>
        public List<string> getSithsNames()
        {
            return m_data.getAllJedis().Where(j => j.IsSith).Select(j => j.Nom).ToList();
        }

        /// <summary>
        /// Donne la liste des jedis ayant plus de 3 points de Force et 50 points de vie.
        /// </summary>
        /// <returns>Liste des noms des jedis avec plus de 3 points de Force et 50 points de vie.</returns>
        public List<string> getJedis3F50PV()
        {
            return m_data.getAllJedis().Where(j => j.Caracteristiques != null
                                                && j.Caracteristiques.Any(c => c.Type == ETypeCaracteristique.Jedi && c.Definition == EDefCaracteristique.Force && c.Valeur > 3)
                                                && j.Caracteristiques.Any(c => c.Type == ETypeCaracteristique.Jedi && c.Definition == EDefCaracteristique.Sante && c.Valeur > 50)
                                              ).Select(j => j.Nom).ToList();
        }

        /// <summary>
        /// Donne la liste de tous les jedis.
        /// </summary>
        /// <returns>Liste de tous les jedis.</returns>
        public List<Jedi> getAllJedis()
        {
            return m_data.getAllJedis();
        }

        public void addJedi(Jedi jedi)
        {
            m_data.addJedi(jedi);
        }

        public void modJedi(Jedi jedi)
        {
            m_data.modJedi(jedi);
        }

        public void delJedi(Jedi jedi)
        {
            m_data.delJedi(jedi);
        }

        #endregion
        #region "Liés aux Stades"

        /// <summary>
        /// Donne la liste des stades disponibles.
        /// </summary>
        /// <returns>Liste des noms des stades.</returns>
        public List<string> getStades()
        {
            return m_data.getAllStades().Select(s => s.Planete).ToList();
        }

        /// <summary>
        /// Donne la liste de tous les stades.
        /// </summary>
        /// <returns>Liste de tous les stades.</returns>
        public List<Stade> getAllStades()
        {
            return m_data.getAllStades();
        }

        public void addStade(Stade stade)
        {
            m_data.addStade(stade);
        }

        public void modStade(Stade stade)
        {
            m_data.modStade(stade);
        }

        public void delStade(Stade stade)
        {
            m_data.delStade(stade);
        }

        #endregion
        #region "Liés aux Matchs"

        /// <summary>
        /// Donne la liste des matchs qui ont eu lieu dans un stade de plus de 200 places et ou deux Siths se sont affrontés.
        /// </summary>
        /// <returns>Liste des matchs qui ont eu lieu dans un stade de plus de 200 places et ou deux Siths se sont affrontés.</returns>
        public List<Match> getMatch200P2S()
        {
            List<Match> list = m_data.getAllMatchs().Where(m => m.Stade.NbPlaces > 200 && m.Jedi1.IsSith && m.Jedi2.IsSith).ToList();
            return new HashSet<Match>(list).ToList();   // Rend unique
        }

        /// <summary>
        /// Donne la liste de l'ensemble des objets matchs.
        /// </summary>
        /// <returns>Liste des objets matchs.</returns>
        public List<Match> getAllMatchs()
        {
            return m_data.getAllMatchs();
        }

        public List<Match> getAllQuartFinaleMatch()
        {
            return m_data.getAllMatchs().Where(m => m.PhaseTournoi == EPhaseTournoi.QuartFinale).ToList();
        }

        public void addMatch(Match match)
        {
            m_data.addMatch(match);
        }

        public void modMatch(Match match)
        {
            m_data.modMatch(match);
        }

        public void delMatch(Match match)
        {
            m_data.delMatch(match);
        }

        #endregion
        #region "Liés aux Caractéristiques"

        /// <summary>
        /// Donne la liste des caractéristiques liées aux jedis.
        /// </summary>
        /// <returns>Liste des caractéristiques liées aux jedis.</returns>
        public List<Caracteristique> getAllJediCaracs()
        {
            return m_data.getAllJediCaracs();
        }

        /// <summary>
        /// Donne la liste des caractéristiques liées aux stades.
        /// </summary>
        /// <returns>Liste des caractéristiques liées aux stades.</returns>
        public List<Caracteristique> getAllStadeCaracs()
        {
            return m_data.getAllStadeCaracs();
        }

        public void addCarac(Caracteristique carac)
        {
            m_data.addCarac(carac);
        }

        public void modCarac(Caracteristique carac)
        {
            m_data.modCarac(carac);
        }

        public void delCarac(Caracteristique carac)
        {
            m_data.delCarac(carac);
        }

        #endregion
        #region "Liés aux Tournois"

        /// <summary>
        /// Donne le tournoi a lancer.
        /// </summary>
        /// <returns>le tournoi a lancer.</returns>
        public Tournoi getTournoi(int id)
        {
            return m_data.getTournoi(id);
        }

        public List<Tournoi> getAllTournois()
        {
            return m_data.getAllTournois();
        }

        public List<Tournoi> getGoodTournois()
        {
            return m_data.getGoodTournois();
        }

        public void addTournoi(Tournoi tournoi)
        {
            m_data.addTournoi(tournoi);
        }

        public void modTournoi(Tournoi tournoi)
        {
            m_data.modTournoi(tournoi);
        }

        public void delTournoi(Tournoi tournoi)
        {
            m_data.delTournoi(tournoi);
        }

        #endregion
        #region "Liés aux Users"

        /// <summary>
        /// Vérifie que le mot de passe correspond au login entré.
        /// </summary>
        /// <param name="login">Login de l'utilisateur.</param>
        /// <param name="passwd">Mot de passe à vérifier.</param>
        /// <returns>Vrai si le mot de passe correspond, sinon faux.</returns>
        public bool checkConnexionUser(string login, string passwd)
        {
            passwd = GetSHA256Hash(passwd);

            Utilisateur user = m_data.getUtilisateurByLogin(login);
            return user != null && user.Password == passwd;
        }

        public void addUtilisateur(Utilisateur user)
        {
            user.Password = GetSHA256Hash(user.Password);
            m_data.addUtilisateur(user);
        }

        public void modUtilisateur(Utilisateur user)
        {
            m_data.modUtilisateur(user);
        }

        public void delUtilisateur(Utilisateur user)
        {
            m_data.delUtilisateur(user);
        }

        #endregion
        #region "Autres"


        public void addUser()
        {
            addUtilisateur(new Utilisateur("admin", "admin", "admin", "admin"));
        }

        public void createDataBase()
        {

            /*********** SCRIPT DE CREATION DE LA BDD (ET RESET) **********************/


            /* Avant de reset il peut être utile d'exécuter la commande "DBCC CHECKIDENT (table, RESEED, 0);" sur chaque table pour reset les IDs */


            /* Supression */

            //addUtilisateur(new Utilisateur("admin", "admin", "admin", "admin"));

            int i;

            List<Caracteristique> listCaracteristiques = m_data.getAllCaracs();
            for (i = 0; i<listCaracteristiques.Count; i++)
            {
                m_data.delCarac(listCaracteristiques.ElementAt(i));
            }

            List<Stade> listStade = m_data.getAllStades();
            for (i = 0; i < listStade.Count; i++)
            {
                m_data.delStade(listStade.ElementAt(i));
            }

            List<Match> listMatch = m_data.getAllMatchs();
            for (i = 0; i < listMatch.Count; i++)
            {
                m_data.delMatch(listMatch.ElementAt(i));
            }

            List<Jedi> listJedi = m_data.getAllJedis();
            for (i = 0; i < listJedi.Count; i++)
            {
                m_data.delJedi(listJedi.ElementAt(i));
            }

            List<Tournoi> listTournoi = m_data.getAllTournois();
            for (i = 0; i < listTournoi.Count; i++)
            {
                m_data.delTournoi(listTournoi.ElementAt(i));
            }

            /* Création */

            List<Caracteristique> listCarac = new List<Caracteristique>();

             // Caracs de Jedis
             listCarac.Add(new Caracteristique(1, "Force", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 2));
             listCarac.Add(new Caracteristique(2, "Force Supérieure", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 4));
             listCarac.Add(new Caracteristique(3, "Force Herculéenne", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 6));
             listCarac.Add(new Caracteristique(4, "Defense Mineure", EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, 2));
             listCarac.Add(new Caracteristique(5, "Defense Supérieure", EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, 4));
             listCarac.Add(new Caracteristique(6, "Le Mur", EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, 6));
             listCarac.Add(new Caracteristique(7, "Vie", EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, 40));
             listCarac.Add(new Caracteristique(8, "Vie Supérieure", EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, 51));
             listCarac.Add(new Caracteristique(9, "Sac à PV", EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, 60));
             listCarac.Add(new Caracteristique(10, "Unlucky Boy", EDefCaracteristique.Chance, ETypeCaracteristique.Jedi, -7));
             listCarac.Add(new Caracteristique(11, "Lucky Boy", EDefCaracteristique.Chance, ETypeCaracteristique.Jedi, 7));

             // Caracs de stades
             listCarac.Add(new Caracteristique(12, "Froid Glaciale", EDefCaracteristique.Defense, ETypeCaracteristique.Stade, -2));
             listCarac.Add(new Caracteristique(13, "Désert", EDefCaracteristique.Force, ETypeCaracteristique.Stade, -2));
             listCarac.Add(new Caracteristique(14, "Ovation", EDefCaracteristique.Force, ETypeCaracteristique.Stade, 3));

             foreach(Caracteristique c in listCarac)
             {
                 m_data.addCarac(c);
             }

             List<Jedi> listJedis = new List<Jedi>();

             // Caractéristiques éventuelles des jedis
             List<Caracteristique> caracsDarthVador = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 2 || c.ID == 7 || c.ID == 11)).ToList();
             List<Caracteristique> caracsDarthMaul = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 3 || c.ID == 10)).ToList();
             List<Caracteristique> caracsLukeS = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 2 || c.ID == 4 || c.ID == 8)).ToList();
             List<Caracteristique> caracsYoda = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 6 || c.ID == 9 || c.ID == 11)).ToList();
             List<Caracteristique> caracsObiwan = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 5 || c.ID == 8)).ToList();

             // Crée la liste des jedis
             // TODO trouver une manière plus élégante de charger les images en relatif

             listJedis.Add(new Jedi(1, "Darth Vador", true, caracsDarthVador, "\\Resources\\Jedis\\darthvador.png"));
             listJedis.Add(new Jedi(2, "Count Dooku", true, null, "\\Resources\\Jedis\\dooku.png"));
             listJedis.Add(new Jedi(3, "Darth Maul", true, caracsDarthMaul, "\\Resources\\Jedis\\darthmaul.png"));
             listJedis.Add(new Jedi(4, "Luke Skywalker", false, caracsLukeS, "\\Resources\\Jedis\\luke.png"));
             listJedis.Add(new Jedi(5, "Yoda", false, caracsYoda, "\\Resources\\Jedis\\yoda.png"));
             listJedis.Add(new Jedi(6, "Qui-Gon Jinn", false, null, "\\Resources\\Jedis\\quigon.png"));
             listJedis.Add(new Jedi(7, "Obi-Wan Kenobi", false, caracsObiwan, "\\Resources\\Jedis\\obiwan.png"));
             listJedis.Add(new Jedi(8, "Emperor Palpatine", true, null, "\\Resources\\Jedis\\palpatine.png"));

             foreach(Jedi j in listJedis)
             {
                 m_data.addJedi(j);
             }


             List<Stade> listStades = new List<Stade>();

             // Caractéristiques éventuelles des stades
             List<Caracteristique> caracsTatooine = listCarac.Where(s => s.Type == ETypeCaracteristique.Stade && s.ID == 13).ToList();
             List<Caracteristique> caracsHoth = listCarac.Where(s => s.Type == ETypeCaracteristique.Stade && (s.ID == 12 || s.ID == 13)).ToList();
             List<Caracteristique> caracsCoruscant = listCarac.Where(s => s.Type == ETypeCaracteristique.Stade && s.ID == 14).ToList();

             // Crée la liste des stades

             listStades.Add(new Stade(1, "Le Drake", 150, "Jakku", null, "\\Resources\\Stades\\drake.png"));
             listStades.Add(new Stade(2, "Le Sandstorm", 2000, "Tatooine", caracsTatooine, "\\Resources\\Stades\\sandstorm.png"));
             listStades.Add(new Stade(3, "Le Cyberia", 10000, "Hoth", caracsHoth, "\\Resources\\Stades\\cyberia.png"));
             listStades.Add(new Stade(4, "L''Impérial", 110000, "Coruscant", caracsCoruscant, "\\Resources\\Stades\\imperial.png"));

             foreach(Stade s in listStades)
             {
                 m_data.addStade(s);
             }




             List<Match> listMatchs = new List<Match>();

             // Match sur Jakku
             listMatchs.Add(new Match(1,
                                     listJedis.Where(j => j.ID == 3).First(),
                                     listJedis.Where(j => j.ID == 4).First(),
                                     EPhaseTournoi.QuartFinale,
                                     listStades.Where(s => s.ID == 1).First()));

             // Match sur Tatooine
             listMatchs.Add(new Match(2,
                                     listJedis.Where(j => j.ID == 1).First(),
                                     listJedis.Where(j => j.ID == 2).First(),
                                     EPhaseTournoi.QuartFinale,
                                     listStades.Where(s => s.ID == 2).First()));

             listMatchs.Add(new Match(3,
                                     listJedis.Where(j => j.ID == 5).First(),
                                     listJedis.Where(j => j.ID == 6).First(),
                                     EPhaseTournoi.QuartFinale,
                                     listStades.Where(s => s.ID == 2).First()));

             // Matchs sur Hoth
             listMatchs.Add(new Match(3,
                                     listJedis.Where(j => j.ID == 7).First(),
                                     listJedis.Where(j => j.ID == 8).First(),
                                     EPhaseTournoi.QuartFinale,
                                     listStades.Where(s => s.ID == 3).First()));

             foreach(Match m in listMatchs)
             {
                 m_data.addMatch(m);
             }

             List<Tournoi> listTournois = new List<Tournoi>();
             listTournois.Add(new Tournoi(1, "Tournoi 1", listMatchs));
             
            Console.WriteLine("C'est bon !");


        }

        public string GetSHA256Hash(string data)
        {

            SHA256 mySHA256 = SHA256.Create();
            byte[] hashValue = mySHA256.ComputeHash(Encoding.Default.GetBytes(data+"thisisasalt"));

            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashValue.Length; i++)
            {
                returnValue.Append(hashValue[i].ToString());
            }

            return returnValue.ToString();
        }

        /// <summary>
        /// Sérialize les jedis.
        /// </summary>
        /// <param name="path">Chemin d'enregistrement du fichier.</param>
        public void serializeJedis(string path)
        {
            // La sérialization peut être placé soit dans BusinessLayer, Presentation ou DataAccessLayer (Jamais dans EntityLayer)
            // Il peut être préférable de le placer dans la couche métier (Business Layer)
            StreamWriter stream = new StreamWriter(path);
            XmlSerializer serializer = new XmlSerializer(typeof(Jedi));
            foreach (Jedi j in m_data.getAllJedis())
                serializer.Serialize(stream, j);

            stream.Close();
        }

        #endregion
    }
}
