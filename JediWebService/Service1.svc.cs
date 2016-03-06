using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EntitiesLayer;
using BusinessLayer;

namespace JediWebService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" dans le code, le fichier svc et le fichier de configuration.
    // REMARQUE : pour lancer le client test WCF afin de tester ce service, sélectionnez Service1.svc ou Service1.svc.cs dans l'Explorateur de solutions et démarrez le débogage.
    public class Service1 : IService1
    {
        JediTournamentManager jtm;

        private Service1()
        {
            jtm = new JediTournamentManager();
        }

        #region "Liés aux Jedis"
        public List<JediContract> GetJedis()
        {

            List<JediContract> list = new List<JediContract>();
            List<Jedi> listjedi = new List<Jedi>();
            listjedi = jtm.getAllJedis();
            foreach(Jedi j in listjedi)
            {

                List<CaracteristiqueContract> listC = new List<CaracteristiqueContract>();

                foreach (Caracteristique c in j.Caracteristiques)
                {
                    listC.Add(new CaracteristiqueContract(c.Nom,c.Valeur));
                }

                JediContract jedi = new JediContract(j.Nom,listC,j.IsSith);

                list.Add(jedi);
            }
            return list;
         }

        public List<CaracteristiqueContract> GetCaracteristiquesByJedi(JediContract j)
        {
            List<CaracteristiqueContract> list = new List<CaracteristiqueContract>();
            List<Caracteristique> listcaract = new List<Caracteristique>();
            List<Jedi> listjedi = new List<Jedi>();
            listjedi = jtm.getAllJedis();
            Jedi jedi = listjedi.Find(x => x.Nom == j.Nom);
            foreach (Caracteristique c in jedi.Caracteristiques)
            {
                CaracteristiqueContract carac = new CaracteristiqueContract(c.Nom, c.Valeur);
                list.Add(carac);
            }
            return list;
        }

        public void AddJedi(string nom, bool isSith, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante)
        {
            List<Caracteristique> carac = new List<Caracteristique>();
            if (force != null)
            {
                Caracteristique force2 = new Caracteristique(1, force.Nom, EDefCaracteristique.Force, ETypeCaracteristique.Jedi, force.Valeur);
                carac.Add(force2);
            }
            if (chance != null)
            {
                Caracteristique chance2 = new Caracteristique(2, chance.Nom, EDefCaracteristique.Chance, ETypeCaracteristique.Jedi, chance.Valeur);
                carac.Add(chance2);
            }
            if (sante != null)
            {
                Caracteristique sante2 = new Caracteristique(3, sante.Nom, EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, sante.Valeur);
                carac.Add(sante2);
            }
            if (defense != null)
            {
                Caracteristique defense2 = new Caracteristique(4, defense.Nom, EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, defense.Valeur);
                carac.Add(defense2);
            }
            Jedi jedi = new Jedi(0, nom, isSith, carac, "");
            jtm.addJedi(jedi);
        }

        public void modJedi(int id, bool isSith, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante)
        {

            List<Caracteristique> carac = new List<Caracteristique>();
            if (force != null)
            {
                Caracteristique force2 = new Caracteristique(1, force.Nom, EDefCaracteristique.Force, ETypeCaracteristique.Jedi, force.Valeur);
                carac.Add(force2);
            }
            if (chance != null)
            {
                Caracteristique chance2 = new Caracteristique(2, chance.Nom, EDefCaracteristique.Chance, ETypeCaracteristique.Jedi, chance.Valeur);
                carac.Add(chance2);
            }
            if (sante != null)
            {
                Caracteristique sante2 = new Caracteristique(3, sante.Nom, EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, sante.Valeur);
                carac.Add(sante2);
            }
            if (defense != null)
            {
                Caracteristique defense2 = new Caracteristique(4, defense.Nom, EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, defense.Valeur);
                carac.Add(defense2);
            }

            List<Jedi> listJedi = jtm.getAllJedis();

            Jedi jedi = listJedi.Find(j => j.ID == id);

            jedi.IsSith = isSith;
            if (carac != null)
            {
                jedi.Caracteristiques = carac;
            }
            jtm.modJedi(jedi);
        }

        public void delJedi(string nom)
        {
            List<Jedi> listJedi = jtm.getAllJedis();
            Jedi jedi = listJedi.Find(j => j.Nom == nom);
            jtm.delJedi(jedi);
        }

        #endregion
        #region "Liés aux Stades"
        public List<StadeContract> GetStades()
        {
            List<StadeContract> list = new List<StadeContract>();
            List<Stade> liststade = new List<Stade>();
            liststade = jtm.getAllStades();
            foreach (Stade s in liststade)
            {
                List<CaracteristiqueContract> listCarac = new List<CaracteristiqueContract>();

                foreach (Caracteristique c in s.Caracteristiques)
                {
                    listCarac.Add(new CaracteristiqueContract(c.Nom,c.Valeur));
                }

                StadeContract stade = new StadeContract(s.NbPlaces,s.Nom,s.Planete,listCarac);
                list.Add(stade);
            }
            return list;
        }

        public List<CaracteristiqueContract> GetCaracteristiquesByStade(StadeContract s)
        {
            List<CaracteristiqueContract> list = new List<CaracteristiqueContract>();
            List<Caracteristique> listcaract = new List<Caracteristique>();
            List<Stade> listStade = new List<Stade>();
            listStade = jtm.getAllStades();
            Stade stade = listStade.Find(x => x.Planete == s.Planete);
            foreach (Caracteristique c in stade.Caracteristiques)
            {
                CaracteristiqueContract carac = new CaracteristiqueContract(c.Nom, c.Valeur);
                list.Add(carac);
            }
            return list;
        }

        public void AddStade(int nbPlaces, string nom, string planete, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante)
        {

            List<Caracteristique> carac = new List<Caracteristique>();
            if (force != null)
            {
                Caracteristique force2 = new Caracteristique(1, force.Nom, EDefCaracteristique.Force, ETypeCaracteristique.Stade, force.Valeur);
                carac.Add(force2);
            }
            if (chance != null)
            {
                Caracteristique chance2 = new Caracteristique(2, chance.Nom, EDefCaracteristique.Chance, ETypeCaracteristique.Stade, chance.Valeur);
                carac.Add(chance2);
            }
            if (sante != null)
            {
                Caracteristique sante2 = new Caracteristique(3, sante.Nom, EDefCaracteristique.Sante, ETypeCaracteristique.Stade, sante.Valeur);
                carac.Add(sante2);
            }
            if (defense != null)
            {
                Caracteristique defense2 = new Caracteristique(4, defense.Nom, EDefCaracteristique.Defense, ETypeCaracteristique.Stade, defense.Valeur);
                carac.Add(defense2);
            }

            Stade stade = new Stade(0,nom,nbPlaces, planete, carac,"");
            jtm.addStade(stade);
      }

      public void modStade(int id, int nbPlaces, string nom, string planete, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante)
      {

            List<Caracteristique> carac = new List<Caracteristique>();
            if (force != null)
            {
                Caracteristique force2 = new Caracteristique(1, force.Nom, EDefCaracteristique.Force, ETypeCaracteristique.Stade, force.Valeur);
                carac.Add(force2);
            }
            if (chance != null)
            {
                Caracteristique chance2 = new Caracteristique(2, chance.Nom, EDefCaracteristique.Chance, ETypeCaracteristique.Stade, chance.Valeur);
                carac.Add(chance2);
            }
            if (sante != null)
            {
                Caracteristique sante2 = new Caracteristique(3, sante.Nom, EDefCaracteristique.Sante, ETypeCaracteristique.Stade, sante.Valeur);
                carac.Add(sante2);
            }
            if (defense != null)
            {
                Caracteristique defense2 = new Caracteristique(4, defense.Nom, EDefCaracteristique.Defense, ETypeCaracteristique.Stade, defense.Valeur);
                carac.Add(defense2);
            }

            List<Stade> listStade = jtm.getAllStades();

            Stade stade = listStade.Find(s => s.ID == id);

            if (nbPlaces != 0)
            {
                stade.NbPlaces = nbPlaces;
            }
            if (nom != null)
            {
                stade.Nom = nom;
            }
            if (planete != null)
            {
                stade.Planete = planete;
            }
            stade.Caracteristiques = carac;
            jtm.modStade(stade);
      }

      public void delStade(string nom)
      {
          List<Stade> listStade = jtm.getAllStades();
          Stade stade = listStade.Find(s => s.Nom == nom);
          jtm.delStade(stade);
      }

        #endregion
        #region "Liés aux Matchs"
        public List<MatchContract> GetMatchs()
        {
            List<MatchContract> list = new List<MatchContract>();
            List<Match> listmatch = new List<Match>();
            List<JediContract> listJedi = new List<JediContract>();
            List<StadeContract> listStade = new List<StadeContract>();
            listJedi = GetJedis();
            listStade = GetStades();
            listmatch = jtm.getAllMatchs();
            foreach (Match m in listmatch)
            {
                MatchContract match = new MatchContract(m.ID,m.IdJediVainqueur, listJedi.Find(x => x.Nom == m.Jedi1.Nom), listJedi.Find(x => x.Nom == m.Jedi2.Nom), listStade.Find(x => x.Planete == m.Stade.Planete));
                list.Add(match);
            }
            return list;
        }

        public void AddMatch(int idJediVainqueur, string nomJedi1, string nomJedi2, string nomStade)
        {

            List<Jedi> listJedi = jtm.getAllJedis();

            Jedi jedi1 = listJedi.Find(j => j.Nom == nomJedi1);
            Jedi jedi2 = listJedi.Find(j => j.Nom == nomJedi2);

            List<Stade> listStade = jtm.getAllStades();

            Stade stade = listStade.Find(s => s.Planete == nomStade);

            Match match = new Match(0,jedi1,jedi2,EPhaseTournoi.QuartFinale,stade);

            jtm.addMatch(match);
        }

        public void modMatch(int id, int idJediVainqueur, string nomJedi1, string nomJedi2, string nomStade, EPhaseTournoi phase)
        {
            List<Jedi> listJedi = jtm.getAllJedis();

            Jedi jedi1 = listJedi.Find(j => j.Nom == nomJedi1);
            Jedi jedi2 = listJedi.Find(j => j.Nom == nomJedi2);

            List<Stade> listStade = jtm.getAllStades();

            Stade stade = listStade.Find(s => s.Planete == nomStade);

            List<Match> listMatch = jtm.getAllMatchs();

            Match match = listMatch.Find(m => m.ID == id);

            if (jedi1 != null) {
                match.Jedi1 = jedi1;
            }
            if (jedi2 != null)
            {
                match.Jedi2 = jedi2;
            }
            match.PhaseTournoi = phase;
            if (stade != null)
            {
                match.Stade = stade;
            }

            jtm.modMatch(match);
        }

        public void delMatch(int id)
        {
            List<Match> listMatch = jtm.getAllMatchs();
            Match match = listMatch.Find(m => m.ID == id);
            jtm.delMatch(match);
        }
        #endregion
        #region "Liés aux Tournois"
        public List<TournoiContract> GetTournois()
        {
            List<TournoiContract> list = new List<TournoiContract>();
            List<Tournoi> listtournoi = new List<Tournoi>();
            List<MatchContract> listmatch = new List<MatchContract>();
            listmatch = GetMatchs();
            listtournoi = jtm.getAllTournois();
            foreach (Tournoi t in listtournoi)
            {
                TournoiContract tournoi = new TournoiContract();
                tournoi.Matchs = new List<MatchContract>();
                Tournoi tournoi2 = listtournoi.Find(x => x.Nom == t.Nom);
                foreach(Match m in tournoi2.Matchs)
                {
                    MatchContract match = new MatchContract();
                    match = listmatch.Find(x => x.Id == m.ID);
                    if (match != null)
                    {
                        tournoi.Matchs.Add(match);
                    }
                }
                tournoi.Nom = t.Nom;
                list.Add(tournoi);
            }
            return list;
        }
        public void AddTournoi(string nom, List<MatchContract> listMatchc)
        {

            List<Match> listMatch = new List<Match>();

            List<Jedi> listJedi = jtm.getAllJedis();
       
            List<Stade> listStade = jtm.getAllStades();

            Jedi jedi1 = new Jedi();
            Jedi jedi2 = new Jedi();
            Stade stade = new Stade();

            foreach (MatchContract m in listMatchc)
            {

                jedi1 = listJedi.Find(j => j.Nom == m.Jedi1.Nom);
                jedi2 = listJedi.Find(j => j.Nom == m.Jedi2.Nom);
                stade = listStade.Find(s => s.Planete == m.Stade.Planete);

                listMatch.Add(new Match(m.Id, jedi1, jedi2, EPhaseTournoi.QuartFinale, stade));
            }

            Tournoi tournoi = new Tournoi(0, nom, listMatch);

            jtm.addTournoi(tournoi);
        }

        public void modTournoi(int id, string nom, List<MatchContract> listMatchc)
        {
            List<Match> listMatch = new List<Match>();

            List<Jedi> listJedi = jtm.getAllJedis();

            List<Stade> listStade = jtm.getAllStades();

            Jedi jedi1 = new Jedi();
            Jedi jedi2 = new Jedi();
            Stade stade = new Stade();

            foreach (MatchContract m in listMatchc)
            {

                jedi1 = listJedi.Find(j => j.Nom == m.Jedi1.Nom);
                jedi2 = listJedi.Find(j => j.Nom == m.Jedi2.Nom);
                stade = listStade.Find(s => s.Planete == m.Stade.Planete);

                listMatch.Add(new Match(m.Id, jedi1, jedi2, EPhaseTournoi.QuartFinale, stade));
            }

            Tournoi tournoi = jtm.getAllTournois().Find(t => t.ID == id);

            if (nom != null)
            {
                tournoi.Nom = nom;
            }
            if (listMatch != null)
            {
                tournoi.Matchs = listMatch;
            }

            jtm.modTournoi(tournoi);
        }

        public void delTournoi(string nom)
        {
            List<Tournoi> listTournoi = jtm.getAllTournois();
            Tournoi tournoi = listTournoi.Find(t => t.Nom == nom);
            jtm.delTournoi(tournoi);
        }
        #endregion
        #region "Liés aux Caracteristiques"
        public List<CaracteristiqueContract> GetCaracteristiques()
            {
                List<CaracteristiqueContract> listCarac = new List<CaracteristiqueContract>();
                List<Caracteristique> listC = new List<Caracteristique>();

                listC = jtm.getAllJediCaracs();

                foreach (Caracteristique c in jtm.getAllStadeCaracs())
                {
                    listC.Add(c);
                }

                foreach (Caracteristique c in listC)
                {
                    listCarac.Add(new CaracteristiqueContract(c.Nom, c.Valeur));
                }

                return listCarac;
            }
        public void AddCarac(string nom, int valeur, EDefCaracteristique definition, ETypeCaracteristique type)
        {

            Caracteristique carac = new Caracteristique(0, nom, definition,type, valeur);

            jtm.addCarac(carac);
        }

        public void modCarac(int id, string nom, int valeur, EDefCaracteristique definition, ETypeCaracteristique type)
        {
            List<Caracteristique> listC = jtm.getAllJediCaracs();

            foreach (Caracteristique c in jtm.getAllStadeCaracs())
            {
                listC.Add(c);
            }

            Caracteristique carac = listC.Find(c => c.ID == id);

            if(nom != null)
            {
                carac.Nom = nom;
            }
            carac.Valeur = valeur;
            carac.Definition = definition;
            carac.Type = type;

            jtm.modCarac(carac);
        }

        public void delCarac(int id)
        {
            List<Caracteristique> listC = jtm.getAllJediCaracs();

            foreach (Caracteristique c in jtm.getAllStadeCaracs())
            {
                listC.Add(c);
            }
            Caracteristique carac = listC.Find(c => c.ID == id);
            jtm.delCarac(carac);
        }



        #endregion
    }
   }

