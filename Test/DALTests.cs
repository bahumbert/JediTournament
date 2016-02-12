using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using EntitiesLayer;
using DataAccessLayer;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Security.Cryptography;
using System.Text;

namespace Test
{
	[TestClass]
	public class DALTests
	{
		[TestMethod]
		public void TestAddJedi()
		{
			Jedi toto = new Jedi(42, "Jedi Toto", false, null, "");
			DalManager m_data = DalManager.Instance;
			m_data.addJedi(toto);
			List<Jedi> list = m_data.getAllJedis().Where(j => j.Nom == toto.Nom).ToList();
			
			Assert.AreEqual(toto.Nom, list.ElementAt(0).Nom);
			Assert.AreEqual(toto.IsSith, list.ElementAt(0).IsSith);
			m_data.delJedi(list.ElementAt(0));
		}

		[TestMethod]
		public void TestModJedi()
		{
			Jedi toto = new Jedi(42, "Jedi Toto", false, null, "");
			DalManager m_data = DalManager.Instance;
			m_data.addJedi(toto);
			List<Jedi> list = m_data.getAllJedis().Where(j => j.Nom == toto.Nom).ToList();
			
			list.ElementAt(0).Nom = "Nouveau Nom";
			list.ElementAt(0).IsSith = true;
			m_data.modJedi(list.ElementAt(0));

			List<Jedi> list2 = m_data.getAllJedis().Where(j => j.Nom == list.ElementAt(0).Nom).ToList();
			Assert.AreEqual(list.ElementAt(0).Nom, list2.ElementAt(0).Nom);
			m_data.delJedi(list2.ElementAt(0));
		}

		[TestMethod]
		public void TestDelJedi()
		{
			Jedi toto = new Jedi(42, "Jedi Toto", false, null, "");
			DalManager m_data = DalManager.Instance;
			m_data.addJedi(toto);
			List<Jedi> list = m_data.getAllJedis().Where(j => j.Nom == toto.Nom).ToList();
			m_data.delJedi(list.ElementAt(0));

			List<Jedi> list2 = m_data.getAllJedis().Where(j => j.Nom == toto.Nom).ToList();
			Assert.IsTrue(!list2.Any(j => string.IsNullOrEmpty(j.Nom)));
		}

		[TestMethod]
		public void TestAddStade()
		{
			Stade toto = new Stade(42, "Test", 42000, "Tata Ouine", null, null);
			DalManager m_data = DalManager.Instance;
			m_data.addStade(toto);
			List<Stade> list = m_data.getAllStades().Where(s => s.Nom == toto.Nom && s.NbPlaces == toto.NbPlaces && s.Planete == toto.Planete).ToList();
			
			Assert.AreEqual(toto.Nom, list.ElementAt(0).Nom);
			Assert.AreEqual(toto.NbPlaces, list.ElementAt(0).NbPlaces);
			Assert.AreEqual(toto.Planete, list.ElementAt(0).Planete);
			m_data.delStade(list.ElementAt(0));
		}

		[TestMethod]
		public void TestModStade()
		{
			Stade toto = new Stade(42, "Test", 42000, "Tata Ouine", null, null);
			DalManager m_data = DalManager.Instance;
			m_data.addStade(toto);
			List<Stade> list = m_data.getAllStades().Where(s => s.Nom == toto.Nom && s.NbPlaces == toto.NbPlaces && s.Planete == toto.Planete).ToList();
			
			list.ElementAt(0).Nom = "Nouveau Nom";
			list.ElementAt(0).NbPlaces = 180;
			list.ElementAt(0).Planete = "Naboo";
			m_data.modStade(list.ElementAt(0));

			List<Stade> list2 = m_data.getAllStades().Where(s => s.Nom == list.ElementAt(0).Nom && s.NbPlaces == list.ElementAt(0).NbPlaces && s.Planete == list.ElementAt(0).Planete).ToList();
			Assert.AreEqual(list.ElementAt(0).Nom, list2.ElementAt(0).Nom);
			Assert.AreEqual(list.ElementAt(0).NbPlaces, list2.ElementAt(0).NbPlaces);
			Assert.AreEqual(list.ElementAt(0).Planete, list2.ElementAt(0).Planete);
			m_data.delStade(list2.ElementAt(0));
		}

		[TestMethod]
		public void TestDelStade()
		{
			Stade toto = new Stade(42, "Test", 42000, "Tata Ouine", null, null);
			DalManager m_data = DalManager.Instance;
			m_data.addStade(toto);
			List<Stade> list = m_data.getAllStades().Where(s => s.Nom == toto.Nom && s.NbPlaces == toto.NbPlaces && s.Planete == toto.Planete).ToList();
			m_data.delStade(list.ElementAt(0));

			List<Stade> list2 = m_data.getAllStades().Where(s => s.Nom == toto.Nom && s.NbPlaces == toto.NbPlaces && s.Planete == toto.Planete).ToList();
			Assert.IsTrue(!list2.Any(s => string.IsNullOrEmpty(s.Nom)));
		}

		[TestMethod]
		public void TestAddMatch()
		{
			DalManager m_data = DalManager.Instance;
            Jedi jedi1 = new Jedi(18, "Jedi 1", false, null, "");
            Jedi jedi2 = new Jedi(68, "Jedi 2", true, null, "");
            Stade stade = new Stade(42, "Test 3", 42000, "Tata Ouine", null, null);
			m_data.addJedi(jedi1);
			m_data.addJedi(jedi2);
			m_data.addStade(stade);

			List<Stade> list1 = m_data.getAllStades().Where(s => s.Nom == stade.Nom && s.NbPlaces == stade.NbPlaces && s.Planete == stade.Planete).ToList();
			List<Jedi> list2 = m_data.getAllJedis().Where(j => (j.Nom == jedi1.Nom || j.Nom == jedi2.Nom)).ToList();

			Match toto = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(0));
			m_data.addMatch(toto);
			List<Match> list = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list2.ElementAt(0).ID && m.Jedi2.ID == list2.ElementAt(1).ID && m.Stade.ID == list1.ElementAt(0).ID).ToList();
			
			Assert.AreEqual(list2.ElementAt(0), list.ElementAt(0).Jedi1);
			Assert.AreEqual(list2.ElementAt(1), list.ElementAt(0).Jedi2);
			Assert.AreEqual(list1.ElementAt(0), list.ElementAt(0).Stade);

			m_data.delMatch(list.ElementAt(0));
			m_data.delStade(list1.ElementAt(0));
			m_data.delJedi(list2.ElementAt(0));
			m_data.delJedi(list2.ElementAt(1));
		}

		[TestMethod]
		public void TestModMatch()
		{
			DalManager m_data = DalManager.Instance;
            Jedi jedi1 = new Jedi(18, "Jedi 1", false, null, "");
            Jedi jedi2 = new Jedi(68, "Jedi 2", true, null, "");
            Stade stade = new Stade(42, "Test", 42000, "Tata Win", null, null);
            Stade stade2 = new Stade(42, "Test Test", 1000, "Tonton Lose", null, null);
			m_data.addJedi(jedi1);
			m_data.addJedi(jedi2);
			m_data.addStade(stade);
			m_data.addStade(stade2);

			List<Stade> list1 = m_data.getAllStades().Where(s => (s.Nom == stade.Nom && s.NbPlaces == stade.NbPlaces && s.Planete == stade.Planete) || (s.Nom == stade2.Nom && s.NbPlaces == stade2.NbPlaces && s.Planete == stade2.Planete)).ToList();
			List<Jedi> list2 = m_data.getAllJedis().Where(j => (j.Nom == jedi1.Nom || j.Nom == jedi2.Nom)).ToList();

			Match toto = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(0));
			m_data.addMatch(toto);
			List<Match> list = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list2.ElementAt(0).ID && m.Jedi2.ID == list2.ElementAt(1).ID && m.Stade.ID == list1.ElementAt(0).ID).ToList();

			list.ElementAt(0).Jedi1 = list2.ElementAt(1);
			list.ElementAt(0).Jedi2 = list2.ElementAt(0);
			list.ElementAt(0).Stade = list1.ElementAt(1);
			m_data.modMatch(list.ElementAt(0));
			
			List<Match> list3 = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list.ElementAt(0).Jedi1.ID && m.Jedi2.ID == list.ElementAt(0).Jedi2.ID && m.Stade.ID == list.ElementAt(0).Stade.ID).ToList();

			Assert.AreEqual(list3.ElementAt(0).Jedi1, list.ElementAt(0).Jedi1);
			Assert.AreEqual(list3.ElementAt(0).Jedi2, list.ElementAt(0).Jedi2);
			Assert.AreEqual(list3.ElementAt(0).Stade, list.ElementAt(0).Stade);

			m_data.delMatch(list.ElementAt(0));
			m_data.delStade(list1.ElementAt(0));
			m_data.delStade(list1.ElementAt(1));
			m_data.delJedi(list2.ElementAt(0));
			m_data.delJedi(list2.ElementAt(1));
		}

		[TestMethod]
		public void TestDelMatch()
		{
			DalManager m_data = DalManager.Instance;
            Jedi jedi1 = new Jedi(18, "Jedi 1", false, null, "");
            Jedi jedi2 = new Jedi(68, "Jedi 2", true, null, "");
            Stade stade = new Stade(42, "Test 4", 42000, "Tata Ouine", null, null);
			m_data.addJedi(jedi1);
			m_data.addJedi(jedi2);
			m_data.addStade(stade);

			List<Stade> list1 = m_data.getAllStades().Where(s => s.Nom == stade.Nom && s.NbPlaces == stade.NbPlaces && s.Planete == stade.Planete).ToList();
			List<Jedi> list2 = m_data.getAllJedis().Where(j => (j.Nom == jedi1.Nom || j.Nom == jedi2.Nom)).ToList();

			Match toto = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(0));
			m_data.addMatch(toto);

			List<Match> list = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list2.ElementAt(0).ID && m.Jedi2.ID == list2.ElementAt(1).ID && m.Stade.ID == list1.ElementAt(0).ID).ToList();
			m_data.delMatch(list.ElementAt(0));
			m_data.delStade(list1.ElementAt(0));
			m_data.delJedi(list2.ElementAt(0));
			m_data.delJedi(list2.ElementAt(1));

			List<Match> list4 = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list2.ElementAt(0).ID && m.Jedi2.ID == list2.ElementAt(1).ID && m.Stade.ID == list1.ElementAt(0).ID).ToList();
			Assert.IsTrue(!list4.Any(m => string.IsNullOrEmpty(m.Jedi1.Nom)));
		}

		[TestMethod]
		public void TestGetAllJedisCaracs()
		{
			Caracteristique carac1 = new Caracteristique(42, "JediCarac", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 42);
			Caracteristique carac2 = new Caracteristique(51, "StadeCarac", EDefCaracteristique.Chance, ETypeCaracteristique.Stade, 51);
			DalManager m_data = DalManager.Instance;
			m_data.addCarac(carac1);
			m_data.addCarac(carac2);
			List<Caracteristique> list1 = m_data.getAllCaracs().Where(c => c.Type == carac1.Type).ToList();
			List<Caracteristique> list2 = m_data.getAllJediCaracs();
			
			int i = 0;
			foreach ( Caracteristique carac3 in list1)
			{
				Assert.AreEqual(carac3, list2.ElementAt(i));
				++i;
			}
			--i;
			List<Caracteristique> list3 = m_data.getAllCaracs().Where(c => c.Nom == carac2.Nom && c.Definition == carac2.Definition && c.Type == carac2.Type && c.Valeur == carac2.Valeur).ToList();
			m_data.delCarac(list2.ElementAt(i));
			m_data.delCarac(list3.ElementAt(0));
		}

		[TestMethod]
		public void TestGetAllStadesCaracs()
		{
			Caracteristique carac1 = new Caracteristique(42, "JediCarac", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 42);
			Caracteristique carac2 = new Caracteristique(51, "StadeCarac", EDefCaracteristique.Chance, ETypeCaracteristique.Stade, 51);
			DalManager m_data = DalManager.Instance;
			m_data.addCarac(carac1);
			m_data.addCarac(carac2);
			List<Caracteristique> list1 = m_data.getAllCaracs().Where(c => c.Type == carac2.Type).ToList();
			List<Caracteristique> list2 = m_data.getAllStadeCaracs();
			
			int i = 0;
			foreach ( Caracteristique carac3 in list1)
			{
				Assert.AreEqual(carac3, list2.ElementAt(i));
				++i;
			}
			--i;
			List<Caracteristique> list3 = m_data.getAllCaracs().Where(c => c.Nom == carac1.Nom && c.Definition == carac1.Definition && c.Type == carac1.Type && c.Valeur == carac1.Valeur).ToList();
			m_data.delCarac(list2.ElementAt(i));
			m_data.delCarac(list3.ElementAt(0));
		}

		[TestMethod]
		public void TestAddCarac()
		{
			Caracteristique carac = new Caracteristique(1, "Test", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 42);
			DalManager m_data = DalManager.Instance;
			m_data.addCarac(carac);
			List<Caracteristique> list = m_data.getAllCaracs().Where(c => c.Nom == carac.Nom && c.Definition == carac.Definition && c.Type == carac.Type && c.Valeur == carac.Valeur).ToList();
			
			Assert.AreEqual(carac.Nom, list.ElementAt(0).Nom);
			Assert.AreEqual(carac.Definition, list.ElementAt(0).Definition);
			Assert.AreEqual(carac.Type, list.ElementAt(0).Type);
			Assert.AreEqual(carac.Valeur, list.ElementAt(0).Valeur);
			m_data.delCarac(list.ElementAt(0));
		}

		[TestMethod]
		public void TestModCarac()
		{
			Caracteristique carac = new Caracteristique(1, "Test", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 42);
			DalManager m_data = DalManager.Instance;
			m_data.addCarac(carac);
			List<Caracteristique> list = m_data.getAllCaracs().Where(c => c.Nom == carac.Nom && c.Definition == carac.Definition && c.Type == carac.Type && c.Valeur == carac.Valeur).ToList();
			
			list.ElementAt(0).Nom = "Nouveau Nom";
			list.ElementAt(0).Definition = EDefCaracteristique.Sante;
			list.ElementAt(0).Type = ETypeCaracteristique.Stade;
			list.ElementAt(0).Valeur = 18;
			m_data.modCarac(list.ElementAt(0));

			List<Caracteristique> list2 = m_data.getAllCaracs().Where(c => c.Nom == list.ElementAt(0).Nom && c.Definition == list.ElementAt(0).Definition && c.Type == list.ElementAt(0).Type && c.Valeur == list.ElementAt(0).Valeur).ToList();
			Assert.AreEqual(list.ElementAt(0).Nom, list2.ElementAt(0).Nom);
			Assert.AreEqual(list.ElementAt(0).Definition, list2.ElementAt(0).Definition);
			Assert.AreEqual(list.ElementAt(0).Type, list2.ElementAt(0).Type);
			Assert.AreEqual(list.ElementAt(0).Valeur, list2.ElementAt(0).Valeur);
			m_data.delCarac(list2.ElementAt(0));
		}

		[TestMethod]
		public void TestDelCaracs()
		{
			Caracteristique carac = new Caracteristique(1, "Test", EDefCaracteristique.Force, ETypeCaracteristique.Jedi, 42);
			DalManager m_data = DalManager.Instance;
			m_data.addCarac(carac);
			List<Caracteristique> list = m_data.getAllCaracs().Where(c => c.Nom == carac.Nom && c.Definition == carac.Definition && c.Type == carac.Type && c.Valeur == carac.Valeur).ToList();
			m_data.delCarac(list.ElementAt(0));

			List<Caracteristique> list2 = m_data.getAllCaracs().Where(c => c.Nom == carac.Nom && c.Definition == carac.Definition && c.Type == carac.Type && c.Valeur == carac.Valeur).ToList();
			Assert.IsTrue(!list2.Any(c => string.IsNullOrEmpty(c.Nom)));
		}

		[TestMethod]
		public void TestAddTournoi()
		{
			DalManager m_data = DalManager.Instance;

            Jedi jedi1 = new Jedi();
            Jedi jedi2 = new Jedi();
            Stade stade1 = new Stade(12, "Test", 2000, "Tata Win", null, null);
            Stade stade2 = new Stade(13, "Test2", 4000, "Tonton Lose", null, null);
			m_data.addJedi(jedi1);
			m_data.addJedi(jedi2);
			m_data.addStade(stade1);
			m_data.addStade(stade2);

			List<Stade> list1 = m_data.getAllStades().Where(s => (s.Nom == stade1.Nom && s.NbPlaces == stade1.NbPlaces && s.Planete == stade1.Planete) || (s.Nom == stade2.Nom && s.NbPlaces == stade2.NbPlaces && s.Planete == stade2.Planete)).ToList();
			List<Jedi> list2 = m_data.getAllJedis().Where(j => (j.Nom == jedi1.Nom || j.Nom == jedi2.Nom)).ToList();
			Match match1 = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(0));
			Match match2 = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(1));
			m_data.addMatch(match1);
			m_data.addMatch(match2);

			List<Match> listMatchs = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list2.ElementAt(0).ID && m.Jedi2.ID == list2.ElementAt(1).ID && (m.Stade.ID == list1.ElementAt(0).ID || m.Stade.ID == list1.ElementAt(1).ID)).ToList();
			
			Tournoi toto = new Tournoi(27, "Le Premier Tournoi", listMatchs);
			m_data.addTournoi(toto);
			List<Tournoi> list = m_data.getAllTournois().Where(t => t.Nom == toto.Nom).ToList();
			
			Assert.AreEqual(toto.Nom, list.ElementAt(0).Nom);
			m_data.delTournoi(list.ElementAt(0));
			m_data.delMatch(listMatchs.ElementAt(0));
			m_data.delMatch(listMatchs.ElementAt(1));
			m_data.delStade(list1.ElementAt(0));
			m_data.delStade(list1.ElementAt(1));
			m_data.delJedi(list2.ElementAt(0));
			m_data.delJedi(list2.ElementAt(1));
		}

		[TestMethod]
		public void TestModTournoi()
		{
			DalManager m_data = DalManager.Instance;

            Jedi jedi1 = new Jedi();
            Jedi jedi2 = new Jedi();
            Stade stade1 = new Stade(12, "Test", 2000, "Tata Win", null, null);
            Stade stade2 = new Stade(13, "Test2", 4000, "Tonton Lose", null, null);
			m_data.addJedi(jedi1);
			m_data.addJedi(jedi2);
			m_data.addStade(stade1);
			m_data.addStade(stade2);

			List<Stade> list1 = m_data.getAllStades().Where(s => (s.Nom == stade1.Nom && s.NbPlaces == stade1.NbPlaces && s.Planete == stade1.Planete) || (s.Nom == stade2.Nom && s.NbPlaces == stade2.NbPlaces && s.Planete == stade2.Planete)).ToList();
			List<Jedi> list2 = m_data.getAllJedis().Where(j => (j.Nom == jedi1.Nom || j.Nom == jedi2.Nom)).ToList();
			Match match1 = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(0));
			Match match2 = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(1));
			m_data.addMatch(match1);
			m_data.addMatch(match2);

			List<Match> listMatchs = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list2.ElementAt(0).ID && m.Jedi2.ID == list2.ElementAt(1).ID && (m.Stade.ID == list1.ElementAt(0).ID || m.Stade.ID == list1.ElementAt(1).ID)).ToList();
			
			Tournoi toto = new Tournoi(27, "Le Premier Tournoi", listMatchs);
			m_data.addTournoi(toto);
			List<Tournoi> list = m_data.getAllTournois().Where(t => t.Nom == toto.Nom).ToList();
			List<Match> listMatchs2 = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list2.ElementAt(0).ID && m.Jedi2.ID == list2.ElementAt(1).ID && m.Stade.ID == list1.ElementAt(0).ID).ToList();			
			list.ElementAt(0).Matchs = listMatchs2;
			list.ElementAt(0).Nom = "SecondTournoi";
			m_data.modTournoi(list.ElementAt(0));
			List<Tournoi> list5 = m_data.getAllTournois().Where(t => t.Nom == list.ElementAt(0).Nom).ToList();			

			Assert.AreEqual(list.ElementAt(0).Nom, list5.ElementAt(0).Nom);
			m_data.delTournoi(list5.ElementAt(0));
			m_data.delMatch(listMatchs.ElementAt(0));
			m_data.delMatch(listMatchs.ElementAt(1));
			m_data.delStade(list1.ElementAt(0));
			m_data.delStade(list1.ElementAt(1));
			m_data.delJedi(list2.ElementAt(0));
			m_data.delJedi(list2.ElementAt(1));
		}

		[TestMethod]
		public void TestDelTournoi()
		{
			DalManager m_data = DalManager.Instance;

            Jedi jedi1 = new Jedi();
            Jedi jedi2 = new Jedi();
            Stade stade1 = new Stade(12, "Test", 2000, "Tata Win", null, null);
            Stade stade2 = new Stade(13, "Test2", 4000, "Tonton Lose", null, null);
			m_data.addJedi(jedi1);
			m_data.addJedi(jedi2);
			m_data.addStade(stade1);
			m_data.addStade(stade2);

			List<Stade> list1 = m_data.getAllStades().Where(s => (s.Nom == stade1.Nom && s.NbPlaces == stade1.NbPlaces && s.Planete == stade1.Planete) || (s.Nom == stade2.Nom && s.NbPlaces == stade2.NbPlaces && s.Planete == stade2.Planete)).ToList();
			List<Jedi> list2 = m_data.getAllJedis().Where(j => (j.Nom == jedi1.Nom || j.Nom == jedi2.Nom)).ToList();
			Match match1 = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(0));
			Match match2 = new Match(42, list2.ElementAt(0), list2.ElementAt(1), EPhaseTournoi.Finale, list1.ElementAt(1));
			m_data.addMatch(match1);
			m_data.addMatch(match2);

			List<Match> listMatchs = m_data.getAllMatchs().Where(m => m.Jedi1.ID == list2.ElementAt(0).ID && m.Jedi2.ID == list2.ElementAt(1).ID && (m.Stade.ID == list1.ElementAt(0).ID || m.Stade.ID == list1.ElementAt(1).ID)).ToList();
			
			Tournoi toto = new Tournoi(27, "Le Premier Tournoi", listMatchs);
			m_data.addTournoi(toto);
			List<Tournoi> list = m_data.getAllTournois().Where(t => t.Nom == toto.Nom).ToList();
			
			m_data.delTournoi(list.ElementAt(0));

			List<Tournoi> list5 = m_data.getAllTournois().Where(t => t.Nom == toto.Nom).ToList();
			Assert.IsTrue(!list5.Any(t => string.IsNullOrEmpty(t.Nom)));

			m_data.delMatch(listMatchs.ElementAt(0));
			m_data.delMatch(listMatchs.ElementAt(1));
			m_data.delStade(list1.ElementAt(0));
			m_data.delStade(list1.ElementAt(1));
			m_data.delJedi(list2.ElementAt(0));
			m_data.delJedi(list2.ElementAt(1));
		}

		/* 
			Les tests sur utilisateurs ne fonctionnent pas en Implémentation BDD
			Meme avec cette fonction
		*/
		public string GetSHA256Hash(string data)
        {
            SHA256 mySHA256 = SHA256.Create();
            byte[] hashValue = mySHA256.ComputeHash(Encoding.Default.GetBytes(data+ "thisisasalt"));

            StringBuilder returnValue = new StringBuilder();

            for (int i = 0; i < hashValue.Length; i++)
            {
                returnValue.Append(hashValue[i].ToString());
            }
            return returnValue.ToString();
        }


		[TestMethod]
		public void TestAddUtilisateur()
		{
			Utilisateur toto = new Utilisateur("Raconte", "Jean", "JR", "mdp");
			toto.Password = GetSHA256Hash(toto.Password);
			DalManager m_data = DalManager.Instance;
			m_data.addUtilisateur(toto);
			Utilisateur compar = m_data.getUtilisateurByLogin("JR");
			
			Assert.AreEqual(toto.Nom, compar.Nom);
			Assert.AreEqual(toto.Prenom, compar.Prenom);
			Assert.AreEqual(toto.Login, compar.Login);
			m_data.delUtilisateur(compar);
		}

		[TestMethod]
		public void TestModUtilisateur()
		{
			Utilisateur toto = new Utilisateur("Raconte", "Jean", "JR", "mdp");
			toto.Password = GetSHA256Hash(toto.Password);
			DalManager m_data = DalManager.Instance;
			m_data.addUtilisateur(toto);
			Utilisateur compar = m_data.getUtilisateurByLogin("JR");
			
			toto.Nom = "Nouvelle";
			toto.Prenom = "Description";
			m_data.modUtilisateur(toto);

			Utilisateur compar2 = m_data.getUtilisateurByLogin("JR");
			Assert.AreEqual(toto.Nom, compar2.Nom);
			Assert.AreEqual(toto.Prenom, compar2.Prenom);
			Assert.AreEqual("Nouvelle", compar2.Nom);
			Assert.AreEqual("Description", compar2.Prenom);
			m_data.delUtilisateur(compar2);
		}

		[TestMethod]
		public void TestDelUtilisateur()
		{
			Utilisateur toto = new Utilisateur("Raconte", "Jean", "JR", "mdp");
			toto.Password = GetSHA256Hash(toto.Password);
			DalManager m_data = DalManager.Instance;
			m_data.addUtilisateur(toto);
			Utilisateur compar = m_data.getUtilisateurByLogin("JR");
			m_data.delUtilisateur(compar);

			Utilisateur compar2 = m_data.getUtilisateurByLogin("JR");
			Assert.IsNull(compar2);
		}
	}
}
