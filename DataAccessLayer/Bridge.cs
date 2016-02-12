using System.Collections.Generic;
using EntitiesLayer;

namespace DataAccessLayer
{
    interface Bridge
    {
        #region "Liés aux Jedis"
        /// <summary>
        /// Permet d'obtenir la liste de tous les jedis connus.
        /// </summary>
        /// <returns>Liste des jedis.</returns>
        List<Jedi> getAllJedis();
        void addJedi(Jedi jedi);
        void modJedi(Jedi jedi);
        void delJedi(Jedi jedi);
        #endregion
        #region "Liés aux Stades"
        List<Stade> getAllStades();
        void addStade(Stade stade);
        void modStade(Stade stade);
        void delStade(Stade stade);
        #endregion
        #region "Liés aux Matchs"
        List<Match> getAllMatchs();
        void addMatch(Match match);
        void modMatch(Match match);
        void delMatch(Match match);
        int useAvailableIdMatch();
        #endregion
        #region "Liés aux Caractéristiques"
        List<Caracteristique> getAllCaracs();
        List<Caracteristique> getAllJediCaracs();
        List<Caracteristique> getAllStadeCaracs();
        void addCarac(Caracteristique carac);
        void modCarac(Caracteristique carac);
        void delCarac(Caracteristique carac);
        #endregion
        #region "Liés aux Users"
        Utilisateur getUtilisateurByLogin(string login);
        void addUtilisateur(Utilisateur user);
        void modUtilisateur(Utilisateur user);
        void delUtilisateur(Utilisateur user);
        #endregion
        #region "Liés aux Tournois"
        Tournoi getTournoi(int id);
        List<Tournoi> getAllTournois();
        List<Tournoi> getGoodTournois();
        void addTournoi(Tournoi tournoi);
        void modTournoi(Tournoi tournoi);
        void delTournoi(Tournoi tournoi);
        #endregion
    }
}
