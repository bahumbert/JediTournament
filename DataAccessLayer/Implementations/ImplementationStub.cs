using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EntitiesLayer;
using System.Reflection;

namespace DataAccessLayer.Implementations
{
    class ImplementationStub : Bridge
    {
        private List<Jedi> listJedi;
        private int availableJediID;
        private List<Caracteristique> listCarac;
        private int availableCaracID;
        private List<Stade> listStades;
        private int availableStadeID;
        private List<Match> listMatch;
        private int availableMatchID;
        private List<Utilisateur> listUtil;
        private int availableTournoiID;
        private List<Tournoi> listTournoi;

        /// <summary>
        /// Constructeur.
        /// </summary>
        public ImplementationStub()
        {
            listJedi = new List<Jedi>();
            listCarac = new List<Caracteristique>();
            listStades = new List<Stade>();
            listMatch = new List<Match>();
            listUtil = new List<Utilisateur>();
            listTournoi = new List<Tournoi>();

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
            availableCaracID = 15;

            // Caractéristiques éventuelles des jedis
            List<Caracteristique> caracsDarthVador = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 2 || c.ID == 7 || c.ID == 11)).ToList();
            List<Caracteristique> caracsDarthMaul = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 3 || c.ID == 10)).ToList();
            List<Caracteristique> caracsLukeS = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 2 || c.ID == 4 || c.ID == 8)).ToList();
            List<Caracteristique> caracsYoda = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 6 || c.ID == 9 || c.ID == 11)).ToList();
            List<Caracteristique> caracsObiwan = listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == 5 || c.ID == 8)).ToList();

            // Crée la liste des jedis
            // TODO trouver une manière plus élégante de charger les images en relatif
            listJedi.Add(new Jedi(1, "Darth Vador", true, caracsDarthVador, "\\Resources\\Jedis\\darthvador.png"));
            listJedi.Add(new Jedi(2, "Count Dooku", true, null, "\\Resources\\Jedis\\dooku.png"));
            listJedi.Add(new Jedi(3, "Darth Maul", true, caracsDarthMaul, "\\Resources\\Jedis\\darthmaul.png"));
            listJedi.Add(new Jedi(4, "Luke Skywalker", false, caracsLukeS, "\\Resources\\Jedis\\luke.png"));
            listJedi.Add(new Jedi(5, "Yoda", false, caracsYoda, "\\Resources\\Jeu\\yoda.png"));
            listJedi.Add(new Jedi(6, "Qui-Gon Jinn", false, null, "\\Resources\\Jedis\\quigon.png"));
            listJedi.Add(new Jedi(7, "Obi-Wan Kenobi", false, caracsObiwan, "\\Resources\\Jedis\\obiwan.png"));
            listJedi.Add(new Jedi(8, "Emperor Palpatine", true, null, "\\Resources\\Jedis\\palpatine.png"));
            availableJediID = 9;

            // Caractéristiques éventuelles des stades
            List<Caracteristique> caracsTatooine = listCarac.Where(s => s.Type == ETypeCaracteristique.Stade && s.ID == 13).ToList();
            List<Caracteristique> caracsHoth = listCarac.Where(s => s.Type == ETypeCaracteristique.Stade && (s.ID == 12 || s.ID == 13)).ToList();
            List<Caracteristique> caracsCoruscant = listCarac.Where(s => s.Type == ETypeCaracteristique.Stade && s.ID == 14).ToList();

            // Crée la liste des stades
            listStades.Add(new Stade(1, "Le Drake", 150, "Jakku", null, "\\Resources\\Stades\\drake.png"));
            listStades.Add(new Stade(2, "Le Sandstorm", 2000, "Tatooine", caracsTatooine, "\\Resources\\Stades\\sandstorm.png"));
            listStades.Add(new Stade(3, "Le Cyberia", 10000, "Hoth", caracsHoth, "\\Resources\\Stades\\cyberia.png"));
            listStades.Add(new Stade(4, "L'Impérial", 110000, "Coruscant", caracsCoruscant, "\\Resources\\Stades\\imperial.png"));
            availableStadeID = 5;

            // Match sur Jakku
            listMatch.Add(new Match(1,
                                    listJedi.Where(j => j.ID == 3).First(),
                                    listJedi.Where(j => j.ID == 4).First(),
                                    EPhaseTournoi.QuartFinale,
                                    listStades.Where(s => s.ID == 1).First()));

            // Match sur Tatooine
            listMatch.Add(new Match(2,
                                    listJedi.Where(j => j.ID == 1).First(),
                                    listJedi.Where(j => j.ID == 2).First(),
                                    EPhaseTournoi.QuartFinale,
                                    listStades.Where(s => s.ID == 2).First()));

            listMatch.Add(new Match(3,
                                    listJedi.Where(j => j.ID == 5).First(),
                                    listJedi.Where(j => j.ID == 6).First(),
                                    EPhaseTournoi.QuartFinale,
                                    listStades.Where(s => s.ID == 2).First()));

            // Matchs sur Hoth
            listMatch.Add(new Match(4,
                                    listJedi.Where(j => j.ID == 7).First(),
                                    listJedi.Where(j => j.ID == 8).First(),
                                    EPhaseTournoi.QuartFinale,
                                    listStades.Where(s => s.ID == 3).First()));
            availableMatchID = 5;

            // Pwds azerty
            addUtilisateur(new Utilisateur("RABERIN", "Alexandre", "KeRNeLith", "1081252467025311320979120222918467701381901472421615814468203197202184186122941456162"));
            addUtilisateur(new Utilisateur("PERSCHER", "Marc", "Marc", "1081252467025311320979120222918467701381901472421615814468203197202184186122941456162"));
            addUtilisateur(new Utilisateur("HUMBERT", "Bastien", "Bastien", "1081252467025311320979120222918467701381901472421615814468203197202184186122941456162"));
            addUtilisateur(new Utilisateur("BELIN", "Mathieu", "Math", "1081252467025311320979120222918467701381901472421615814468203197202184186122941456162"));
            
            // Pwd : admin
            addUtilisateur(new Utilisateur("ADMIN", "Admin", "admin", "9122168229210971191938212919884403313824317523613821694180185253055411771149498"));


            listTournoi.Add(new Tournoi(1, "Tournoi 1", new List<Match>(listMatch)));
            availableTournoiID = 2;
        }

        #region "Liés aux Jedis"
        /// <summary>
        /// Permet d'obtenir la liste de tous les jedis connus.
        /// </summary>
        /// <returns>Liste des jedis.</returns>
        public List<Jedi> getAllJedis()
        {
            return listJedi;
        }

        public void addJedi(Jedi jedi)
        {
            jedi.ID = availableJediID++;
            listJedi.Add(jedi);
        }

        public void modJedi(Jedi jedi)
        {
            int index = listJedi.FindIndex(j => j.ID == jedi.ID);
            if (index >= 0)
                listJedi[index] = jedi;
            // Ne devrai jamais arriver
            else
                listJedi.Add(jedi);
        }

        public void delJedi(Jedi jedi)
        {
            listJedi.Remove(jedi);
        }

        #endregion
        #region "Liés aux Stades"
        /// <summary>
        /// Permet d'obtenir la liste de tous les stades connus.
        /// </summary>
        /// <returns>Liste des stades.</returns>
        public List<Stade> getAllStades()
        {
            return listStades;
        }

        public void addStade(Stade stade)
        {
            stade.ID = availableStadeID++;
            listStades.Add(stade);
        }

        public void modStade(Stade stade)
        {
            int index = listStades.FindIndex(s => s.ID == stade.ID);
            if (index >= 0)
                listStades[index] = stade;
            // Ne devrai jamais arriver
            else
                listStades.Add(stade);
        }

        public void delStade(Stade stade)
        {
            foreach (Match m in listMatch.Where(m => m.Stade == stade).ToList())
            {
                delMatch(m);
            }
            listStades.Remove(stade);
        }

        #endregion
        #region "Liés aux Matchs"

        public int useAvailableIdMatch()
        {
            int res = availableMatchID;
            availableMatchID++;
            return res;
        }

        /// <summary>
        /// Permet d'obtenir la liste de tous les matchs connus.
        /// </summary>
        /// <returns>Liste des matchs.</returns>
        public List<Match> getAllMatchs()
        {
            return listMatch;
        }

        public void addMatch(Match match)
        {
            match.ID = availableMatchID++;
            listMatch.Add(match);
        }

        public void modMatch(Match match)
        {
            int index = listMatch.FindIndex(m => m.ID == match.ID);
            if (index >= 0)
                listMatch[index] = match;
            // Ne devrai jamais arriver
            else
                listMatch.Add(match);
        }

        public void delMatch(Match match)
        {
            foreach (Tournoi t in listTournoi.Where(t => t.Matchs.Contains(match)).ToList())
            {
                listTournoi.Remove(t);
            }
            listMatch.Remove(match);
        }

        #endregion
        #region "Liés aux Tournois"

        public Tournoi getTournoi(int id)
        {
            return listTournoi.ElementAt(id);
        }

        public List<Tournoi> getGoodTournois()
        {
            List<Tournoi> tmp = new List<Tournoi>();

            foreach (Tournoi t in getAllTournois())
            {
                if (t.Matchs.Count == 4)
                {
                    tmp.Add(t);
                }
            }

            return tmp;
        }

        public List<Tournoi> getAllTournois()
        {
            // WARNING : tout est pointeur en C#
            // Clone la liste de tournoi 
            // (nécessaire dans le stub car sinon les modifications non désirées 
            // pour la sauvegarde sont quand même appliquées)
            List<Tournoi> list = new List<Tournoi>();
            foreach (Tournoi t in listTournoi)
            {
                Tournoi tbis = new Tournoi(t.ID, t.Nom, new List<Match>(t.Matchs));
                list.Add(tbis);
            }

            return list;
        }

        public void addTournoi(Tournoi tournoi)
        {
            listTournoi.Add(tournoi);
            availableTournoiID++;
        }

        public void modTournoi(Tournoi tournoi)
        {
            int index = listTournoi.FindIndex(t => t.ID == tournoi.ID);
            if (index >= 0)
                listTournoi[index] = tournoi;
            // Ne devrai jamais arriver
            else
                listTournoi.Add(tournoi);
        }

        public void delTournoi(Tournoi tournoi)
        {
            listTournoi.Remove(tournoi);
        }

        #endregion
        #region "Liés aux Caractéristiques"

        /// <summary>
        /// Permet d'obtenir la liste de toutes les caractéristiques enregistrées.
        /// </summary>
        /// <returns>Liste des caractéritiques.</returns>
        public List<Caracteristique> getAllCaracs()
        {
            return listCarac;
        }

        /// <summary>
        /// Permet d'obtenir la liste des caractéristiques de jedi enregistrées.
        /// </summary>
        /// <returns>Liste des caractéritiques.</returns>
        public List<Caracteristique> getAllJediCaracs()
        {
            return listCarac.Where(c => c.Type == ETypeCaracteristique.Jedi).ToList();
        }

        /// <summary>
        /// Permet d'obtenir la liste des caractéristiques de stade enregistrées.
        /// </summary>
        /// <returns>Liste des caractéritiques.</returns>
        public List<Caracteristique> getAllStadeCaracs()
        {
            return listCarac.Where(c => c.Type == ETypeCaracteristique.Stade).ToList();
        }

        public void addCarac(Caracteristique carac)
        {
            carac.ID = availableCaracID++;
            listCarac.Add(carac);
        }

        public void modCarac(Caracteristique carac)
        {
            int index = listCarac.FindIndex(c => c.ID == carac.ID);
            if (index >= 0)
                listCarac[index] = carac;
            // Ne devrai jamais arriver
            else
                listCarac.Add(carac);
        }

        public void delCarac(Caracteristique carac)
        {
            listCarac.Remove(carac);
        }

        #endregion
        #region "Liés aux Users"

        /// <summary>
        /// Permet de récupérer un utilisateur par login.
        /// </summary>
        /// <param name="login">Login de l'utilisateur à récupérer.</param>
        /// <returns>Utilisateur correspondant.</returns>
        public Utilisateur getUtilisateurByLogin(string login)
        {
            return listUtil.Where(u => u.Login == login).FirstOrDefault();
        }

        public void addUtilisateur(Utilisateur user)
        {
            listUtil.Add(user);
        }

        public void modUtilisateur(Utilisateur user)
        {
            int index = listUtil.FindIndex(u => u.Nom == user.Nom);
            if (index >= 0)
                listUtil[index] = user;
            // Ne devrai jamais arriver
            else
                listUtil.Add(user);
        }

        public void delUtilisateur(Utilisateur user)
        {
            listUtil.Remove(user);
        }

        #endregion
    }
}
