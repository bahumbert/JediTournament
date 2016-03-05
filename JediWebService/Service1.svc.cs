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
                JediContract jedi = new JediContract();
                jedi.Nom = j.Nom;
                jedi.IsSith = j.IsSith;
                jedi.Caracteristiques = new List<CaracteristiqueContract>();
                CaracteristiqueContract force = new CaracteristiqueContract();
                CaracteristiqueContract sante = new CaracteristiqueContract();
                CaracteristiqueContract defense = new CaracteristiqueContract();
                CaracteristiqueContract chance = new CaracteristiqueContract();

                force.Nom = j.Caracteristiques.Find(x => x.Definition == EDefCaracteristique.Force).Nom;
                force.Valeur = j.Caracteristiques.Find(x => x.Definition == EDefCaracteristique.Force).Valeur;
                jedi.Caracteristiques.Add(force);
                chance.Nom = j.Caracteristiques.Find(x => x.Definition == EDefCaracteristique.Chance).Nom;
                chance.Valeur = j.Caracteristiques.Find(x => x.Definition == EDefCaracteristique.Chance).Valeur;
                jedi.Caracteristiques.Add(chance);
                defense.Nom = j.Caracteristiques.Find(x => x.Definition == EDefCaracteristique.Defense).Nom;
                defense.Valeur = j.Caracteristiques.Find(x => x.Definition == EDefCaracteristique.Defense).Valeur;
                jedi.Caracteristiques.Add(defense);
                sante.Nom = j.Caracteristiques.Find(x => x.Definition == EDefCaracteristique.Sante).Nom;
                sante.Valeur = j.Caracteristiques.Find(x => x.Definition == EDefCaracteristique.Sante).Valeur;
                jedi.Caracteristiques.Add(sante);
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
                CaracteristiqueContract carac = new CaracteristiqueContract();
                carac.Nom = c.Nom;
                carac.Valeur = c.Valeur;
                list.Add(carac);
            }
            return list;
        }
        public void AddJedi(string nom, bool isSith, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante)
        {
            List<Caracteristique> carac = new List<Caracteristique>();
            Caracteristique force2 = new Caracteristique(1, force.Nom, EDefCaracteristique.Force, ETypeCaracteristique.Jedi, force.Valeur);
            Caracteristique chance2 = new Caracteristique(2, chance.Nom, EDefCaracteristique.Chance, ETypeCaracteristique.Jedi, chance.Valeur);
            Caracteristique sante2 = new Caracteristique(3, sante.Nom, EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, sante.Valeur);
            Caracteristique defense2 = new Caracteristique(4, defense.Nom, EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, defense.Valeur);
            carac.Add(force2);
            carac.Add(defense2);
            carac.Add(chance2);
            carac.Add(sante2);
            Jedi jedi = new Jedi(0, nom, isSith, carac, "");
            jtm.addJedi(jedi);
        }

        public void modJedi(int id, bool isSith, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante)
        {

            List<Caracteristique> carac = new List<Caracteristique>();
            Caracteristique force2 = new Caracteristique(1, force.Nom, EDefCaracteristique.Force, ETypeCaracteristique.Jedi, force.Valeur);
            Caracteristique chance2 = new Caracteristique(2, chance.Nom, EDefCaracteristique.Chance, ETypeCaracteristique.Jedi, chance.Valeur);
            Caracteristique sante2 = new Caracteristique(3, sante.Nom, EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, sante.Valeur);
            Caracteristique defense2 = new Caracteristique(4, defense.Nom, EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, defense.Valeur);
            carac.Add(force2);
            carac.Add(defense2);
            carac.Add(chance2);
            carac.Add(sante2);

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
                StadeContract stade = new StadeContract();
                stade.NbPlace = s.NbPlaces;
                stade.Planete = s.Planete;
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
                CaracteristiqueContract carac = new CaracteristiqueContract();
                carac.Nom = c.Nom;
                carac.Valeur = c.Valeur;
                list.Add(carac);
            }
            return list;
        }
        /*public void AddStade(int nbPlaces, string planete)
        {
            /*List<Caracteristique> carac = new List<Caracteristique>();
            Caracteristique force2 = new Caracteristique(1, force.Nom, EDefCaracteristique.Force, ETypeCaracteristique.Jedi, force.Valeur);
            Caracteristique chance2 = new Caracteristique(2, chance.Nom, EDefCaracteristique.Chance, ETypeCaracteristique.Jedi, chance.Valeur);
            Caracteristique sante2 = new Caracteristique(3, sante.Nom, EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, sante.Valeur);
            Caracteristique defense2 = new Caracteristique(4, defense.Nom, EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, defense.Valeur);*/



        /*  carac.Add(force2);
          carac.Add(defense2);
          carac.Add(chance2);
          carac.Add(sante2);
          Stade stade = new Stade();
          stade.
          jtm.addStade(stade);
      }

      public void modJedi(int id, bool isSith, CaracteristiqueContract force, CaracteristiqueContract defense, CaracteristiqueContract chance, CaracteristiqueContract sante)
      {

          List<Caracteristique> carac = new List<Caracteristique>();
          Caracteristique force2 = new Caracteristique(1, force.Nom, EDefCaracteristique.Force, ETypeCaracteristique.Jedi, force.Valeur);
          Caracteristique chance2 = new Caracteristique(2, chance.Nom, EDefCaracteristique.Chance, ETypeCaracteristique.Jedi, chance.Valeur);
          Caracteristique sante2 = new Caracteristique(3, sante.Nom, EDefCaracteristique.Sante, ETypeCaracteristique.Jedi, sante.Valeur);
          Caracteristique defense2 = new Caracteristique(4, defense.Nom, EDefCaracteristique.Defense, ETypeCaracteristique.Jedi, defense.Valeur);
          carac.Add(force2);
          carac.Add(defense2);
          carac.Add(chance2);
          carac.Add(sante2);

          List<Jedi> listJedi = jtm.getAllJedis();

          Jedi jedi = listJedi.Find(j => j.ID == id);

          jedi.IsSith = isSith;
          jedi.Caracteristiques = carac;
          jtm.modJedi(jedi);
      }

      public void delJedi(string nom)
      {
          List<Jedi> listJedi = jtm.getAllJedis();
          Jedi jedi = listJedi.Find(j => j.Nom == nom);
          jtm.delJedi(jedi);
      }*/

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
                MatchContract match = new MatchContract();
                match.Id = m.ID;
                match.IdJediVainqueur = m.IdJediVainqueur;
                match.Jedi1 = listJedi.Find(x => x.Nom == m.Jedi1.Nom);
                match.Jedi2 = listJedi.Find(x => x.Nom == m.Jedi2.Nom);
                match.Stade = listStade.Find(x => x.Planete == m.Stade.Planete);
                list.Add(match);
            }
            return list;
        }

        public void AddMatch(int idJediVainqueur, JediContract jedic1, JediContract jedic2, StadeContract stadec )
        {

            List<Jedi> listJedi = jtm.getAllJedis();

            Jedi jedi1 = listJedi.Find(j => j.Nom == jedic1.Nom);
            Jedi jedi2 = listJedi.Find(j => j.Nom == jedic2.Nom);

            List<Stade> listStade = jtm.getAllStades();

            Stade stade = listStade.Find(s => s.Planete == stadec.Planete);

            Match match = new Match(0,jedi1,jedi2,EPhaseTournoi.QuartFinale,stade);

            jtm.addMatch(match);
        }

        public void modMatch(int id, int idJediVainqueur, JediContract jedic1, JediContract jedic2, StadeContract stadec, EPhaseTournoi phase)
        {
            List<Jedi> listJedi = jtm.getAllJedis();

            Jedi jedi1 = listJedi.Find(j => j.Nom == jedic1.Nom);
            Jedi jedi2 = listJedi.Find(j => j.Nom == jedic2.Nom);

            List<Stade> listStade = jtm.getAllStades();

            Stade stade = listStade.Find(s => s.Planete == stadec.Planete);

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
        public void AddCarac(string nom, int valeur, EDefCaracteristique definition/*, ETypeCaracteristique type*/)
        {

            Caracteristique carac = new Caracteristique(0, nom, definition,ETypeCaracteristique.Jedi, valeur);

            jtm.addCarac(carac);
        }

        public void modCarac(int id, string nom, int valeur, EDefCaracteristique definition/*, ETypeCaracteristique type*/)
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
            carac.Type = ETypeCaracteristique.Jedi;

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

