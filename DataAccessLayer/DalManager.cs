using System.Collections.Generic;
using EntitiesLayer;
using DataAccessLayer.Implementations;

namespace DataAccessLayer
{
    /// <summary>
    /// Classe Singleton.
    /// </summary>
    public class DalManager
    {
        /// <summary>
        /// Bridge vers le SGBD utilisé.
        /// </summary>
        private Bridge m_bridge;

        /// <summary>
        /// Instance unique.
        /// </summary>
        private static DalManager _instance;

        /// <summary>
        /// Mutex.
        /// </summary>
        private static readonly object _padlock = new object();

        /// <summary>
        /// Constructeur.
        /// </summary>
        private DalManager()
        {
            m_bridge = new ImplementationStub();
        }

        /// <summary>
        /// Récupération de l'instance.
        /// </summary>
        public static DalManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_padlock)
                    {
                        if (_instance == null)
                        {
                            _instance = new DalManager();
                        }
                    }
                }
                return _instance;
            }

            private set { _instance = value; }
        }

        #region "Liés aux Jedis"

        /// <summary>
        /// Permet d'obtenir la liste de tous les jedis connus.
        /// </summary>
        /// <returns>Liste des jedis.</returns>
        public List<Jedi> getAllJedis()
        {
            return m_bridge.getAllJedis();
        }

        public void addJedi(Jedi jedi)
        {
            m_bridge.addJedi(jedi);
        }

        public void modJedi(Jedi jedi)
        {
            m_bridge.modJedi(jedi);
        }

        public void delJedi(Jedi jedi)
        {
            m_bridge.delJedi(jedi);
        }

        #endregion
        #region "Liés aux Stades"

        /// <summary>
        /// Permet d'obtenir la liste de tous les stades connus.
        /// </summary>
        /// <returns>Liste des stades.</returns>
        public List<Stade> getAllStades()
        {
            return m_bridge.getAllStades();
        }

        public void addStade(Stade stade)
        {
            m_bridge.addStade(stade);
        }

        public void modStade(Stade stade)
        {
            m_bridge.modStade(stade);
        }

        public void delStade(Stade stade)
        {
            m_bridge.delStade(stade);
        }

        #endregion
        #region "Liés aux Matchs"

        /// <summary>
        /// Permet d'obtenir la liste de tous les matchs connus.
        /// </summary>
        /// <returns>Liste des matchs.</returns>
        public List<Match> getAllMatchs()
        {
            return m_bridge.getAllMatchs();
        }

        public void addMatch(Match match)
        {
            m_bridge.addMatch(match);
        }

        public void modMatch(Match match)
        {
            m_bridge.modMatch(match);
        }

        public void delMatch(Match match)
        {
            m_bridge.delMatch(match);
        }

        public int useAvailableIdMatch()
        {
           return m_bridge.useAvailableIdMatch();
        }

        #endregion
        #region "Liés aux Caractéristiques"

        /// <summary>
        /// Permet d'obtenir la liste de toutes les caractéristiques enregistrées.
        /// </summary>
        /// <returns>Liste des caractéritiques.</returns>
        public List<Caracteristique> getAllCaracs()
        {
            return m_bridge.getAllCaracs();
        }

        public List<Caracteristique> getAllJediCaracs()
        {
            return m_bridge.getAllJediCaracs();
        }

        public List<Caracteristique> getAllStadeCaracs()
        {
            return m_bridge.getAllStadeCaracs();
        }

        public void addCarac(Caracteristique carac)
        {
            m_bridge.addCarac(carac);
        }

        public void modCarac(Caracteristique carac)
        {
            m_bridge.modCarac(carac);
        }

        public void delCarac(Caracteristique carac)
        {
            m_bridge.delCarac(carac);
        }

        #endregion
        #region "Liés aux Tournois"

        public Tournoi getTournoi(int id)
        {
            return m_bridge.getTournoi(id);
        }

        public List<Tournoi> getGoodTournois()
        {
            return m_bridge.getGoodTournois();
        }

        public List<Tournoi> getAllTournois()
        {
            return m_bridge.getAllTournois();
        }

        public void addTournoi(Tournoi tournoi)
        {
            m_bridge.addTournoi(tournoi);
        }

        public void modTournoi(Tournoi tournoi)
        {
            m_bridge.modTournoi(tournoi);
        }

        public void delTournoi(Tournoi tournoi)
        {
            m_bridge.delTournoi(tournoi);
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
            return m_bridge.getUtilisateurByLogin(login);
        }

        public void addUtilisateur(Utilisateur user)
        {
            m_bridge.addUtilisateur(user);
        }

        public void modUtilisateur(Utilisateur user)
        {
            m_bridge.modUtilisateur(user);
        }

        public void delUtilisateur(Utilisateur user)
        {
            m_bridge.delUtilisateur(user);
        }

        #endregion
    }
}
