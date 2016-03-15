using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JediWebApplication.Views.Home
{
    public class MatchController : Controller
    {

        private ServiceReference1.Service1Client client;

        public MatchController()
        {
            client = new ServiceReference1.Service1Client();
        }

        public ActionResult index()
        {
            return RedirectToAction("GestionMatchs");
        }

        // GET: Match
        public ActionResult GestionMatchs()
        {
            ViewBag.Matchs = client.GetMatchs();
            return View();
        }

        // GET: Match/Details/5
       /* public ActionResult Details(int id)
        {
            return View();
        }*/

        // GET: Match/Ajouter
        public ActionResult Ajouter()
        {
            ViewBag.Jedi = client.GetJedis();
            return View();
        }

        // POST: Match/Ajouter
        [HttpPost]
        public ActionResult Ajouter(FormCollection collection)
        {
            try
            {
                client.AddMatch(int.Parse(collection.GetValues("IdJediVainqueur").First()), collection.GetValues("Jedi1").First(), collection.GetValues("Jedi2").First(), collection.GetValues("Stade").First());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Match/Editer/5
        public ActionResult Editer(int id)
        {
            ViewBag.Matchs = client.GetMatchs().Where(s => s.Id == id).First();
            ViewBag.Jedi = client.GetJedis();
            return View();
        }

        // POST: Match/Editer/5
        [HttpPost]
        public ActionResult Editer(int id, FormCollection collection)
        {
            try
            {
                client.modMatch(id, int.Parse(collection.GetValues("IdJediVainqueur").First()), collection.GetValues("Jedi1").First(), collection.GetValues("Jedi2").First(), collection.GetValues("Stade").First());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Match/Supprimer/5
        public ActionResult Supprimer(int id)
        {
            ViewBag.Matchs = client.GetMatchs().Where(s => s.Id == id).First();
            return View();
        }

        // POST: Match/Supprimer/5
        [HttpPost]
        public ActionResult Supprimer(int id, FormCollection collection)
        {
            try
            {
                client.delMatch(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
