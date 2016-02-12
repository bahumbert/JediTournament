using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EntitiesLayer;
using System.Collections.Generic;

/* Pour exécuter : onglet Test dans la barre de menu puis exécuter -> tous les tests */

namespace Test
{
    [TestClass]
    public class EntitiesTests
    {
        [TestMethod]
        public void testCarac()
        {
            Caracteristique carac = new Caracteristique();
            Assert.AreEqual(carac.ToString(), "Default Name 0 (" + EDefCaracteristique.Chance + ")");
            Assert.AreEqual(carac.ID, 0);
            Assert.AreEqual(carac.Nom, "Default Name");
            Assert.AreEqual(carac.Definition, EDefCaracteristique.Chance);
            Assert.AreEqual(carac.Type, ETypeCaracteristique.Jedi);
            Assert.AreEqual(carac.Valeur, 0);

            Caracteristique carac2 = new Caracteristique(1, "Test", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 42);
            Assert.AreEqual(carac2.ToString(), "Test 42 (" + EDefCaracteristique.Force + ")");
            Assert.AreEqual(carac2.ID, 1);
            Assert.AreEqual(carac2.Nom, "Test");
            Assert.AreEqual(carac2.Definition, EDefCaracteristique.Force);
            Assert.AreEqual(carac2.Type, ETypeCaracteristique.Jedi);
            Assert.AreEqual(carac2.Valeur, 42);

            carac2.Nom = "Nouveau Nom";
            carac2.Definition = EDefCaracteristique.Defense;
            carac2.Type = ETypeCaracteristique.Stade;
            carac2.Valeur = 18;

            Assert.AreEqual(carac2.Nom, "Nouveau Nom");
            Assert.AreEqual(carac2.Definition, EDefCaracteristique.Defense);
            Assert.AreEqual(carac2.Type, ETypeCaracteristique.Stade);
            Assert.AreEqual(carac2.Valeur, 18);
        }

        [TestMethod]
        public void testJedi()
        {
            Jedi jedi1 = new Jedi();
            Assert.AreEqual(jedi1.ToString(), "Default Name (Jedi)");
            Assert.AreEqual(jedi1.ID, 0);
            Assert.AreEqual(jedi1.Nom, "Default Name");
            Assert.IsFalse(jedi1.IsSith);
            Assert.IsNull(jedi1.Caracteristiques);
            Assert.AreEqual(jedi1.Image, "");

            Jedi jedi2 = new Jedi(51, "Nom Du Jedi", false, null, "");
            Assert.AreEqual(jedi2.ToString(), "Nom Du Jedi (Jedi)");
            Assert.AreEqual(jedi2.ID, 51);
            Assert.AreEqual(jedi2.Nom, "Nom Du Jedi");
            Assert.IsFalse(jedi2.IsSith);
            Assert.IsNull(jedi2.Caracteristiques);
            Assert.AreEqual(jedi2.Image, "");

			List<Caracteristique> listCarac =  new List<Caracteristique>();
			listCarac.Add(new Caracteristique(1, "Force", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 2));
            listCarac.Add(new Caracteristique(2, "Force Supérieure", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 4));
            
            jedi2.Nom = "Le Jedi";
            jedi2.IsSith = true;
            jedi2.Caracteristiques = listCarac;
            //jedi2.Image = ;

            Assert.AreEqual(jedi2.Nom, "Le Jedi");
            Assert.IsTrue(jedi2.IsSith);
            Assert.AreEqual(jedi2.Caracteristiques, listCarac);
            //Assert.AreEqual(jedi2.Image, "");
        }

        [TestMethod]
        public void testMatch()
        {
            Jedi jedi1 = new Jedi();
            Jedi jedi2 = new Jedi();
            Stade stade = new Stade(42, "Test", 42000, "Tata Ouine", null, null);
            Stade stade2 = new Stade(18, "Test2", 2000, "Ouine", null, null);

            Match match = new Match(42, jedi1, jedi2, EPhaseTournoi.Finale, stade);
            Assert.AreEqual(match.ID, 42);
            Assert.AreEqual(match.Jedi1, jedi1);
            Assert.AreEqual(match.Jedi2, jedi2);
            Assert.AreEqual(match.PhaseTournoi, EPhaseTournoi.Finale);
            Assert.AreEqual(match.Stade, stade);

            match.Jedi1 = jedi2;
            match.Jedi2 = jedi1;
            match.PhaseTournoi = EPhaseTournoi.DemiFinale;
            match.Stade = stade2;

            Assert.AreEqual(match.Jedi1, jedi2);
            Assert.AreEqual(match.Jedi2, jedi1);
            Assert.AreEqual(match.PhaseTournoi, EPhaseTournoi.DemiFinale);
            Assert.AreEqual(match.Stade, stade2);
        }

        [TestMethod]
        public void testStade()
        {
            Stade stade = new Stade(42, "Test", 42000, "Tata Ouine", null, null);
            Assert.AreEqual(stade.ToString(), "Test (Tata Ouine)");
            Assert.AreEqual(stade.ID, 42);
            Assert.AreEqual(stade.Nom, "Test");
            Assert.AreEqual(stade.NbPlaces, 42000);
            Assert.AreEqual(stade.Planete, "Tata Ouine");
            Assert.IsNull(stade.Caracteristiques);
            Assert.IsNull(stade.Image);

			List<Caracteristique> listCarac =  new List<Caracteristique>();
            listCarac.Add(new Caracteristique(12, "Froid Glaciale", EDefCaracteristique.Defense, ETypeCaracteristique.Stade, -2));
            listCarac.Add(new Caracteristique(13, "Désert", EDefCaracteristique.Force, ETypeCaracteristique.Stade, -2));

            stade.Nom = "Nom Du Stade";
            stade.NbPlaces = 18000;
            stade.Planete = "Mars";
            stade.Caracteristiques = listCarac;
            //stade.Image = ;

            Assert.AreEqual(stade.ToString(), "Nom Du Stade (Mars)");
            Assert.AreEqual(stade.Nom, "Nom Du Stade");
            Assert.AreEqual(stade.NbPlaces, 18000);
            Assert.AreEqual(stade.Planete, "Mars");
            Assert.AreEqual(stade.Caracteristiques, listCarac);
            //Assert.IsNull(stade.Image);
        }

        [TestMethod]
        public void testTournoi()
        {
            Jedi jedi1 = new Jedi();
            Jedi jedi2 = new Jedi();
            Jedi jedi3 = new Jedi();
            Jedi jedi4 = new Jedi();

            Stade stade = new Stade(12, "Test", 2000, "Ouine", null, null);
            Stade stade2 = new Stade(13, "Test2", 4000, "Tata", null, null);

            List<Match> listMatchs = new List<Match>();
                listMatchs.Add(new Match(42, jedi1, jedi2, EPhaseTournoi.QuartFinale, stade));
                listMatchs.Add(new Match(56, jedi3, jedi4, EPhaseTournoi.DemiFinale, stade2));

            List<Match> listMatchs2 = new List<Match>();
                listMatchs2.Add(new Match(2, jedi3, jedi1, EPhaseTournoi.Finale, stade));

            Tournoi tournoi = new Tournoi(27, "Le Premier Tournoi", listMatchs);

            Assert.AreEqual(tournoi.ID, 27);
            Assert.AreEqual(tournoi.Nom, "Le Premier Tournoi");
            Assert.AreEqual(tournoi.Matchs, listMatchs);

            tournoi.Nom = "Un Nouveau Tournoi";
            tournoi.Matchs = listMatchs2;

            Assert.AreEqual(tournoi.Nom, "Un Nouveau Tournoi");
            Assert.AreEqual(tournoi.Matchs, listMatchs2);
        }

        [TestMethod]
        public void testUtilisateur()
        {
            Utilisateur user = new Utilisateur("Nouvel", "Utilisateur", "nouvelUtilisateur", "password");
            Assert.AreEqual(user.Nom, "Nouvel");
            Assert.AreEqual(user.Prenom, "Utilisateur");
            Assert.AreEqual(user.Login, "nouvelUtilisateur");
            Assert.AreEqual(user.Password, "password");

            user.Nom = "Toto";
            user.Prenom = "Tata";
            user.Login = "Titi";
            user.Password = "motDePasse";

            Assert.AreEqual(user.Nom, "Toto");
            Assert.AreEqual(user.Prenom, "Tata");
            Assert.AreEqual(user.Login, "Titi");
            Assert.AreEqual(user.Password, "motDePasse");
        }
    }
}
