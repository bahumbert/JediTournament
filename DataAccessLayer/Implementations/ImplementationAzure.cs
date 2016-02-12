using System.Collections.Generic;
using System.Linq;
using EntitiesLayer;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Data.SqlTypes;

namespace DataAccessLayer.Implementations
{
    class ImplementationAzure : Bridge
    {
        /// <summary>
        /// Chaine de connexion.
        /// </summary>
        private string m_connexionString = "Data Source = teamcinqfoisdeux.database.windows.net; Initial Catalog = JediTournament; User ID = teamadmin; Password = Team5fois2";

        /// <summary>
        /// Constructeur.
        /// </summary>
        public ImplementationAzure()
        {

        }

        #region "Liés aux Jedis"
        /// <summary>
        /// Permet d'obtenir la liste de tous les jedis connus.
        /// </summary>
        /// <returns>Liste des jedis.</returns>
        public List<Jedi> getAllJedis()
        {
            List<Jedi> listJedis = new List<Jedi>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandJedi = new SqlCommand("SELECT j.id, j.nom, j.isSith, j.image FROM dbo.jedi j", sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReaderJedi = sqlCommandJedi.ExecuteReader();

                List<Caracteristique> toutesCarac = getAllJediCaracs();

                while (sqlDataReaderJedi.Read())
                {
                    List<Caracteristique> listCarac = new List<Caracteristique>();
                    using (SqlConnection sqlConnection2 = new SqlConnection(m_connexionString))
                    {
                        SqlCommand sqlCommandCarac = new SqlCommand("SELECT id_carac FROM dbo.carac_jedi WHERE id_jedi=" + sqlDataReaderJedi.GetInt32(0), sqlConnection2);
                        sqlConnection2.Open();
                        SqlDataReader sqlDataReaderCarac = sqlCommandCarac.ExecuteReader();



                        while (sqlDataReaderCarac.Read())
                        {
                            List<Caracteristique> carac = toutesCarac.Where(c => c.Type == ETypeCaracteristique.Jedi && (c.ID == sqlDataReaderCarac.GetInt32(0))).ToList();
                            listCarac.Add(carac[0]);
                        }
                        sqlConnection2.Close();
                    }                   

                    listJedis.Add(new Jedi(sqlDataReaderJedi.GetInt32(0), sqlDataReaderJedi.GetString(1), sqlDataReaderJedi.GetBoolean(2), listCarac, sqlDataReaderJedi.GetString(3)));

                }

                sqlConnection.Close();

            }

            return listJedis;
        }

        public void addJedi(Jedi jedi)
        {
            string commande = "";

            List<Caracteristique> carac = jedi.Caracteristiques;
            if (carac != null)
            {
                foreach (Caracteristique c in carac)
                {
                    commande += "; INSERT INTO dbo.carac_jedi (id_jedi, id_carac) VALUES ((SELECT IDENT_CURRENT('jedi'))," + c.ID + ")";
                }
            }

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {

                jedi.Nom = jedi.Nom.Replace("'", "''");


                SqlCommand sqlAddJedi = new SqlCommand("INSERT INTO dbo.jedi (nom, isSith, image) VALUES('" + jedi.Nom + "', '" + jedi.IsSith + "', '" + jedi.Image + "')" + commande, sqlConnection);

                sqlAddJedi.Connection = sqlConnection;
                sqlConnection.Open();
                sqlAddJedi.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void modJedi(Jedi jedi)
        {
            string commande = "";

            List<Caracteristique> caracs_prev = getJediCaracs(jedi); //.Where(c => c.ID == jedi.ID).ToList();

            foreach(Caracteristique c in jedi.Caracteristiques)
            {
                if (!caracs_prev.Contains(c))
                {
                    commande += "; INSERT INTO carac_jedi (id_jedi, id_carac) VALUES (" + jedi.ID + "," + c.ID + ")";
                }
            }
            foreach (Caracteristique cprev in caracs_prev)
            {
                if (!jedi.Caracteristiques.Contains(cprev))
                {
                    commande += "; DELETE FROM carac_jedi WHERE id_jedi=" + jedi.ID + " AND id_carac=" + cprev.ID;
                }
            }

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                jedi.Nom = jedi.Nom.Replace("'", "''");

                SqlCommand sqlModJedi = new SqlCommand("UPDATE dbo.jedi SET nom='" + jedi.Nom + "', isSith='" + jedi.IsSith + "', image='" + jedi.Image + "' WHERE id=" + jedi.ID + ""+ commande, sqlConnection);
                sqlModJedi.Connection = sqlConnection;
                sqlConnection.Open();
                sqlModJedi.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
        
        public void delJedi(Jedi jedi)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlDelJedi = new SqlCommand("DELETE FROM dbo.jedi WHERE id="+jedi.ID, sqlConnection);
                sqlDelJedi.Connection = sqlConnection;
                sqlConnection.Open();
                sqlDelJedi.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        #endregion
        #region "Liés aux Stades"
        /// <summary>
        /// Permet d'obtenir la liste de tous les stades connus.
        /// </summary>
        /// <returns>Liste des stades.</returns>
        public List<Stade> getAllStades()
        {
             List<Stade> listStades = new List<Stade>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandStade = new SqlCommand("SELECT s.id, s.nom, s.nbPlaces, s.planete, s.image FROM dbo.stade s", sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReaderStade = sqlCommandStade.ExecuteReader();

                List<Caracteristique> toutesCarac = getAllStadeCaracs();

                while (sqlDataReaderStade.Read())
                {
                    List<Caracteristique> listCarac = new List<Caracteristique>();
                    using (SqlConnection sqlConnection2 = new SqlConnection(m_connexionString))
                    {
                        SqlCommand sqlCommandCarac = new SqlCommand("SELECT id_carac FROM dbo.carac_stade WHERE id_stade=" + sqlDataReaderStade.GetInt32(0), sqlConnection2);
                        sqlConnection2.Open();
                        SqlDataReader sqlDataReaderCarac = sqlCommandCarac.ExecuteReader();



                        while (sqlDataReaderCarac.Read())
                        {
                            List<Caracteristique> carac = toutesCarac.Where(c => c.Type == ETypeCaracteristique.Stade && (c.ID == sqlDataReaderCarac.GetInt32(0))).ToList();
                            listCarac.Add(carac[0]);
                        }
                        sqlConnection2.Close();
                    }

                    listStades.Add(new Stade(sqlDataReaderStade.GetInt32(0), sqlDataReaderStade.GetString(1), sqlDataReaderStade.GetInt32(2), sqlDataReaderStade.GetString(3), listCarac, sqlDataReaderStade.GetString(4)));
                }
                sqlConnection.Close();
            }

            return listStades;
        }

        public void addStade(Stade stade)
        {
            string commande = "";

            List<Caracteristique> carac = stade.Caracteristiques;
            if (carac != null)
            {
                foreach (Caracteristique c in carac)
                {
                    addCarac(c);
                    commande += "; INSERT INTO dbo.carac_stade (id_stade, id_carac) VALUES ((SELECT IDENT_CURRENT( 'stade' ))," + c.ID + ")";
                }
            }

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                stade.Nom = stade.Nom.Replace("'", "''");
                stade.Planete = stade.Planete.Replace("'", "''");
                SqlCommand sqlAddStade = new SqlCommand("INSERT INTO dbo.stade (nom, nbPlaces, planete, image) VALUES ('" + stade.Nom+"', " + stade.NbPlaces + ", '" + stade.Planete + "', '" + stade.Image + "')" + commande, sqlConnection);
                sqlAddStade.Connection = sqlConnection;
                sqlConnection.Open();
                sqlAddStade.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
        
        public void modStade(Stade stade)
        {

            string commande = "";

            List<Caracteristique> caracs_prev = getStadeCaracs(stade); //.Where(c => c.ID == jedi.ID).ToList();

            foreach (Caracteristique c in stade.Caracteristiques)
            {
                if (!caracs_prev.Contains(c))
                {
                    commande += "; INSERT INTO carac_stade (id_stade, id_carac) VALUES (" + stade.ID + "," + c.ID + ")";
                }
            }
            foreach (Caracteristique cprev in caracs_prev)
            {
                if (!stade.Caracteristiques.Contains(cprev))
                {
                    commande += "; DELETE FROM carac_stade WHERE id_stade=" + stade.ID + " AND id_carac=" + cprev.ID;
                }
            }

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                stade.Nom = stade.Nom.Replace("'","''");
                stade.Planete = stade.Planete.Replace("'", "''");
                SqlCommand sqlModStade = new SqlCommand("UPDATE dbo.stade SET nom='" + stade.Nom + "', nbPlaces=" + stade.NbPlaces + ", planete='" + stade.Planete + "', image='" + stade.Image+"' WHERE id=" + stade.ID + "" + commande, sqlConnection);
                sqlModStade.Connection = sqlConnection;
                sqlConnection.Open();
                sqlModStade.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
        
        public void delStade(Stade stade)
        {
            List<Match> listMatch = getAllMatchs().Where(m => m.Stade.ID == stade.ID).ToList();
            if (listMatch != null)
            {
                foreach (Match m in listMatch)
                {
                    delMatch(m);
                }
            }

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlDelStade = new SqlCommand("DELETE FROM dbo.stade WHERE id=" + stade.ID, sqlConnection);
                sqlDelStade.Connection = sqlConnection;
                sqlConnection.Open();
                sqlDelStade.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        #endregion
        #region "Liés aux Matchs"

        public int useAvailableIdMatch()
        {
            return 0;
        }

        /// <summary>
        /// Permet d'obtenir la liste de tous les matchs connus.
        /// </summary>
        /// <returns>Liste des matchs.</returns>
        public List<Match> getAllMatchs()
        {
            List<Match> listMatch = new List<Match>();
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandMatch = new SqlCommand("SELECT m.id, m.id_vainqueur, m.id_jedi1, m.id_jedi2, m.phaseTournoi, m.id_tournoi, m.id_stade FROM dbo.match m", sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReaderMatch = sqlCommandMatch.ExecuteReader();
                List<Jedi> listJedis = getAllJedis();
                List<Stade> listStades = getAllStades();

                while (sqlDataReaderMatch.Read())
                {
                    EPhaseTournoi phaseTournoi = (EPhaseTournoi)Enum.Parse(typeof(EPhaseTournoi), sqlDataReaderMatch.GetString(4));
                    listMatch.Add(new Match(sqlDataReaderMatch.GetInt32(0), listJedis.Where(j => j.ID == sqlDataReaderMatch.GetInt32(2)).First(), listJedis.Where(j => j.ID == sqlDataReaderMatch.GetInt32(3)).First(), phaseTournoi, listStades.Where(s => s.ID == sqlDataReaderMatch.GetInt32(6)).First()));
                }
                sqlConnection.Close();
            }

             return listMatch;
        }

        public void addMatch(Match match)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqladdMatch = new SqlCommand("INSERT INTO match (id_jedi1, id_jedi2, phaseTournoi, id_tournoi, id_stade) VALUES (" + match.Jedi1.ID + ", " + match.Jedi2.ID + ", '" + match.PhaseTournoi.ToString() + "', null ," + match.Stade.ID + ")", sqlConnection);
                sqladdMatch.Connection = sqlConnection;
                sqlConnection.Open();
                sqladdMatch.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void modMatch(Match match)
        {
            string command;

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {

                command = "UPDATE dbo.match SET id_jedi1 = " + match.Jedi1.ID + ", id_jedi2 = " + match.Jedi2.ID + ", phaseTournoi = '" + match.PhaseTournoi + "', id_stade = " + match.Stade.ID;

                if (match.IdJediVainqueur != -1)
                {
                    command += ", id_vainqueur = " + match.IdJediVainqueur;
                }
            
                command += " WHERE id = " + match.ID;

                SqlCommand sqlmodMatch = new SqlCommand(command, sqlConnection);
                sqlmodMatch.Connection = sqlConnection;
                sqlConnection.Open();
                sqlmodMatch.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void delMatch(Match match)
        {
            int id = 0;
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandMatch = new SqlCommand("SELECT m.id_tournoi FROM dbo.match m WHERE id=" + match.ID, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReaderMatch = sqlCommandMatch.ExecuteReader();
                List<Jedi> listJedis = getAllJedis();
                List<Stade> listStades = getAllStades();

                while (sqlDataReaderMatch.Read())
                {
                    try
                    {
                        id = sqlDataReaderMatch.GetInt32(0);
                    }
                    catch(Exception e)
                    {

                    }
                }
            }
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlDelMatch = new SqlCommand("DELETE FROM dbo.match WHERE id=" + match.ID, sqlConnection);
                sqlDelMatch.Connection = sqlConnection;
                sqlConnection.Open();
                sqlDelMatch.ExecuteNonQuery();
                sqlConnection.Close();
            }
            if (id != 0)
            { 
                Tournoi tournoi = getAllTournois().Where(t => t.ID == id).First();
                delTournoi(tournoi);
            }
        }

        private void changeTournoi(List<Match> listMatch, int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {

                SqlCommand sqlmodMatch = new SqlCommand("UPDATE dbo.match SET id_tournoi = null WHERE id_tournoi=" + id, sqlConnection);
                sqlmodMatch.Connection = sqlConnection;
                sqlConnection.Open();
                sqlmodMatch.ExecuteNonQuery();
                sqlConnection.Close();

                foreach (Match m in listMatch)
                {

                    SqlCommand sqlmodMatch2 = new SqlCommand("UPDATE dbo.match SET id_tournoi=" + id + " WHERE id=" + m.ID, sqlConnection);
                    sqlmodMatch2.Connection = sqlConnection;
                    sqlConnection.Open();
                    sqlmodMatch2.ExecuteNonQuery();
                    sqlConnection.Close();

                }
            }
        }

        #endregion
        #region "Liés aux Tournois"

        private List<Match> getAllTournoiMatchs(int idTournoi)
        {
            List<Match> listMatch = new List<Match>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandMatch = new SqlCommand("SELECT m.id, m.id_vainqueur, m.id_jedi1, m.id_jedi2, m.phaseTournoi, m.id_tournoi, m.id_stade FROM dbo.match m WHERE id_tournoi="+ idTournoi, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReaderMatch = sqlCommandMatch.ExecuteReader();
                List<Jedi> listJedis = getAllJedis();
                List<Stade> listStades = getAllStades();

                while (sqlDataReaderMatch.Read())
                {
                    EPhaseTournoi phaseTournoi = (EPhaseTournoi)Enum.Parse(typeof(EPhaseTournoi), sqlDataReaderMatch.GetString(4));
                    listMatch.Add(new Match(sqlDataReaderMatch.GetInt32(0), listJedis.Where(j => j.ID == sqlDataReaderMatch.GetInt32(2)).First(), listJedis.Where(j => j.ID == sqlDataReaderMatch.GetInt32(3)).First(), phaseTournoi, listStades.Where(s => s.ID == sqlDataReaderMatch.GetInt32(6)).First()));
                }
                sqlConnection.Close();
            }
            return listMatch;
        }

        public Tournoi getTournoi (int id)
        {
            List<Match> listMatch = getAllTournoiMatchs(id);

            //int id = 1;
            string nom = "Default";

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandTournoi = new SqlCommand("SELECT id, nom FROM dbo.tournoi t WHERE id="+id, sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReaderTournoi = sqlCommandTournoi.ExecuteReader();

                while (sqlDataReaderTournoi.Read())
                {
                    id = sqlDataReaderTournoi.GetInt32(0);
                    nom = sqlDataReaderTournoi.GetString(1);
                }
                sqlConnection.Close();
            }

            return new Tournoi(1,"test",null);//(id, nom, listMatch);

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
            List<Tournoi> listTournoi = new List<Tournoi>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandTournoi = new SqlCommand("SELECT id, nom FROM dbo.tournoi", sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReaderTournoi = sqlCommandTournoi.ExecuteReader();
                while (sqlDataReaderTournoi.Read())
                {
                    listTournoi.Add(new Tournoi(sqlDataReaderTournoi.GetInt32(0), sqlDataReaderTournoi.GetString(1), getAllTournoiMatchs(sqlDataReaderTournoi.GetInt32(0))));
                }
                sqlConnection.Close();
            }
            return listTournoi;
        }

        public void addTournoi(Tournoi tournoi)
        {

            int id = 0;

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlAddTournoi = new SqlCommand("INSERT INTO dbo.tournoi (nom) VALUES ('" + tournoi.Nom + "')", sqlConnection);
                sqlAddTournoi.Connection = sqlConnection;
                sqlConnection.Open();
                sqlAddTournoi.ExecuteNonQuery();
                sqlConnection.Close();
            }

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandTournoi = new SqlCommand("SELECT IDENT_CURRENT('tournoi')", sqlConnection);
                sqlConnection.Open();

                SqlDataReader sqlDataReaderTournoi = sqlCommandTournoi.ExecuteReader();

                while (sqlDataReaderTournoi.Read())
                {
                    id = (int)sqlDataReaderTournoi.GetDecimal(0);
                }

            }

            if (id != 0)
            {
                changeTournoi(tournoi.Matchs, id);
            }

        }

        public void modTournoi(Tournoi tournoi)
        {
            changeTournoi(tournoi.Matchs, tournoi.ID);

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlModTournoi = new SqlCommand("UPDATE dbo.tournoi SET nom='" + tournoi.Nom + "' WHERE id=" + tournoi.ID, sqlConnection);
                sqlModTournoi.Connection = sqlConnection;
                sqlConnection.Open();
                sqlModTournoi.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void delTournoi(Tournoi tournoi)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlDelTournoi = new SqlCommand("UPDATE dbo.match SET id_tournoi=null WHERE id_tournoi=" + tournoi.ID + ";DELETE FROM dbo.tournoi WHERE id=" + tournoi.ID, sqlConnection);
                sqlDelTournoi.Connection = sqlConnection;
                sqlConnection.Open();
                sqlDelTournoi.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        #endregion
        #region "Liés aux Caractéristiques"

        /// <summary>
        /// Permet d'obtenir la liste de toutes les caractéristiques enregistrées.
        /// </summary>
        /// <returns>Liste des caractéritiques.</returns>
        public List<Caracteristique> getAllCaracs()
        {
            List<Caracteristique> listCaracs = new List<Caracteristique>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandCarac = new SqlCommand("SELECT id, nom, definition, type, valeur FROM dbo.caracteristique", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReaderCarac = sqlCommandCarac.ExecuteReader();

                while (sqlDataReaderCarac.Read())
                {
                    EDefCaracteristique definition = (EDefCaracteristique)Enum.Parse(typeof(EDefCaracteristique), sqlDataReaderCarac.GetString(2));
                    ETypeCaracteristique type = (ETypeCaracteristique)Enum.Parse(typeof(ETypeCaracteristique), sqlDataReaderCarac.GetString(3));

                    listCaracs.Add(new Caracteristique(sqlDataReaderCarac.GetInt32(0), sqlDataReaderCarac.GetString(1), definition, type, sqlDataReaderCarac.GetInt32(4)));
                }

                sqlConnection.Close();

                return listCaracs;
            }
        }

        public List<Caracteristique> getAllJediCaracs()
        {
            List<Caracteristique> listCarac = new List<Caracteristique>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandCarac = new SqlCommand("SELECT id, nom, definition, type, valeur FROM dbo.caracteristique WHERE type='Jedi'", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReaderCarac = sqlCommandCarac.ExecuteReader();

                while (sqlDataReaderCarac.Read())
                {
                    EDefCaracteristique definition = (EDefCaracteristique)Enum.Parse(typeof(EDefCaracteristique), sqlDataReaderCarac.GetString(2));
                    ETypeCaracteristique type = (ETypeCaracteristique)Enum.Parse(typeof(ETypeCaracteristique), sqlDataReaderCarac.GetString(3));

                    listCarac.Add(new Caracteristique(sqlDataReaderCarac.GetInt32(0), sqlDataReaderCarac.GetString(1), definition, type, sqlDataReaderCarac.GetInt32(4)));
                }

                sqlConnection.Close();
            }

            return listCarac;
        }

        public List<Caracteristique> getJediCaracs(Jedi jedi)
        {
            List<Caracteristique> listCarac = new List<Caracteristique>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandCarac = new SqlCommand("SELECT c.id, c.nom, c.definition, c.type, c.valeur FROM dbo.caracteristique c JOIN carac_jedi cj ON cj.id_carac=c.id WHERE c.type='Jedi' AND cj.id_jedi="+jedi.ID, sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReaderCarac = sqlCommandCarac.ExecuteReader();

                while (sqlDataReaderCarac.Read())
                {
                    EDefCaracteristique definition = (EDefCaracteristique)Enum.Parse(typeof(EDefCaracteristique), sqlDataReaderCarac.GetString(2));
                    ETypeCaracteristique type = (ETypeCaracteristique)Enum.Parse(typeof(ETypeCaracteristique), sqlDataReaderCarac.GetString(3));

                    listCarac.Add(new Caracteristique(sqlDataReaderCarac.GetInt32(0), sqlDataReaderCarac.GetString(1), definition, type, sqlDataReaderCarac.GetInt32(4)));
                }

                sqlConnection.Close();
            }

            return listCarac;
        }

        public List<Caracteristique> getAllStadeCaracs()
        {
            List<Caracteristique> listCarac = new List<Caracteristique>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandCarac = new SqlCommand("SELECT id, nom, definition, type, valeur FROM dbo.caracteristique WHERE type='Stade'", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReaderCarac = sqlCommandCarac.ExecuteReader();

                while (sqlDataReaderCarac.Read())
                {
                    EDefCaracteristique definition = (EDefCaracteristique)Enum.Parse(typeof(EDefCaracteristique), sqlDataReaderCarac.GetString(2));
                    ETypeCaracteristique type = (ETypeCaracteristique)Enum.Parse(typeof(ETypeCaracteristique), sqlDataReaderCarac.GetString(3));

                    listCarac.Add(new Caracteristique(sqlDataReaderCarac.GetInt32(0), sqlDataReaderCarac.GetString(1), definition, type, sqlDataReaderCarac.GetInt32(4)));
                }

                sqlConnection.Close();
            }

            return listCarac;
        }

        public List<Caracteristique> getStadeCaracs(Stade stade)
        {
            List<Caracteristique> listCarac = new List<Caracteristique>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandCarac = new SqlCommand("SELECT c.id, c.nom, c.definition, c.type, c.valeur FROM dbo.caracteristique c JOIN carac_stade cs ON cs.id_carac=c.id WHERE c.type='Stade' AND cs.id_stade=" + stade.ID, sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReaderCarac = sqlCommandCarac.ExecuteReader();

                while (sqlDataReaderCarac.Read())
                {
                    EDefCaracteristique definition = (EDefCaracteristique)Enum.Parse(typeof(EDefCaracteristique), sqlDataReaderCarac.GetString(2));
                    ETypeCaracteristique type = (ETypeCaracteristique)Enum.Parse(typeof(ETypeCaracteristique), sqlDataReaderCarac.GetString(3));

                    listCarac.Add(new Caracteristique(sqlDataReaderCarac.GetInt32(0), sqlDataReaderCarac.GetString(1), definition, type, sqlDataReaderCarac.GetInt32(4)));
                }

                sqlConnection.Close();
            }

            return listCarac;
        }

        public void addCarac(Caracteristique carac)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {

                carac.Nom = carac.Nom.Replace("'", "''");
                
                SqlCommand sqlAddCarac = new SqlCommand("INSERT INTO dbo.caracteristique (nom, definition, type, valeur) VALUES ('" + carac.Nom + "', '" + carac.Definition + "', '" + carac.Type + "', " + carac.Valeur + ")", sqlConnection);
                sqlAddCarac.Connection = sqlConnection;
                sqlConnection.Open();
                sqlAddCarac.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
        
        public void modCarac(Caracteristique carac)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                carac.Nom = carac.Nom.Replace("'", "''");

                SqlCommand sqlModCarac = new SqlCommand("UPDATE dbo.caracteristique SET nom='" + carac.Nom + "', definition='" + carac.Definition + "', type='" + carac.Type + "', valeur=" + carac.Valeur+" WHERE id=" + carac.ID, sqlConnection);
                sqlModCarac.Connection = sqlConnection;
                sqlConnection.Open();
                sqlModCarac.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
        
        public void delCarac(Caracteristique carac)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlDelCarac = new SqlCommand("DELETE FROM dbo.caracteristique WHERE id=" + carac.ID, sqlConnection);
                sqlDelCarac.Connection = sqlConnection;
                sqlConnection.Open();
                sqlDelCarac.ExecuteNonQuery();
                sqlConnection.Close();
            }
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
            List<Utilisateur> users = new List<Utilisateur>();

            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlCommandUtil = new SqlCommand("SELECT nom, prenom, login, password FROM dbo.utilisateur", sqlConnection);
                sqlConnection.Open();
                SqlDataReader sqlDataReaderUtil = sqlCommandUtil.ExecuteReader();

                while (sqlDataReaderUtil.Read())
                {
                    //string pass = System.Text.Encoding.UTF8.GetString(sqlDataReaderUtil.GetSqlBinary(3).Value);
                    //string pass = System.Text.Encoding.UTF8.GetString((byte[])sqlDataReaderUtil.GetSqlBinary(3)));
                        //string pass =  (string)sqlDataReaderUtil.GetSqlBytes(3).ToSqlBinary().ToSqlGuid().ToSqlString();
                    users.Add(new Utilisateur(sqlDataReaderUtil.GetString(0), sqlDataReaderUtil.GetString(1), sqlDataReaderUtil.GetString(2), sqlDataReaderUtil.GetString(3)));
                }
                sqlConnection.Close();
            }

                    return users.Where(u => u.Login == login).FirstOrDefault();
        }

        public void addUtilisateur(Utilisateur user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {

                SqlCommand sqlAddUtil = new SqlCommand("INSERT INTO dbo.utilisateur VALUES('" + user.Nom + "', '" + user.Prenom + "', '" + user.Login + "', '" + user.Password + "')", sqlConnection);
                sqlAddUtil.Connection = sqlConnection;
                sqlConnection.Open();
                sqlAddUtil.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void modUtilisateur(Utilisateur user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {

                SqlCommand sqlModUtil = new SqlCommand("UPDATE dbo.utilisateur SET nom='" + user.Nom + "', prenom='" + user.Prenom + "', password='" + user.Password + "' WHERE login='" + user.Login + "'", sqlConnection);
                sqlModUtil.Connection = sqlConnection;
                sqlConnection.Open();
                sqlModUtil.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void delUtilisateur(Utilisateur user)
        {
            using (SqlConnection sqlConnection = new SqlConnection(m_connexionString))
            {
                SqlCommand sqlDelUtil = new SqlCommand("DELETE FROM dbo.utilisateur WHERE login='" + user.Login + "'", sqlConnection);
                sqlDelUtil.Connection = sqlConnection;
                sqlConnection.Open();
                sqlDelUtil.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        #endregion
    }
}
