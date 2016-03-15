using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JediWebApplication.Views.Home
{
    public class TournoiController : Controller
    {
        private ServiceReference1.Service1Client client;

        public TournoiController()
        {
            client = new ServiceReference1.Service1Client();
        }

        public ActionResult index()
        {
            return RedirectToAction("GestionTournois");
        }

        // GET: Tournoi
        public ActionResult GestionTournois()
        {
            ViewBag.Tournois = client.GetTournois();
            return View();
        }

        // GET: Tournoi/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Tournois = client.GetTournois().Where(t => t.Id == id).First();
            return View();
        }

        // GET: Tournoi/Ajouter
        public ActionResult Ajouter()
        {
            return View();
        }

        // POST: Tournoi/Ajouter
        [HttpPost]
        public ActionResult Ajouter(FormCollection collection)
        {
            try
            {
                client.AddTournoi(collection.GetValues("Nom").First(),null,null,null,null);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tournoi/Editer/5
        public ActionResult Editer(int id)
        {
            ViewBag.Tournois = client.GetTournois().Where(t => t.Id == id).First();
            ViewBag.Matchs = client.GetMatchs();
            return View();
        }

        // POST: Tournoi/Editer/5
        [HttpPost]
        public ActionResult Editer(int id, FormCollection collection)
        {
            try
            {
                client.ModTournoi(id,collection.GetValues("Nom").First(), null, null, null, null);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Tournoi/Supprimer/5
        public ActionResult Supprimer(int id)
        {
            ViewBag.Tournois = client.GetTournois().Where(t => t.Id == id).First();
            return View();
        }

        // POST: Tournoi/Supprimer/5
        [HttpPost]
        public ActionResult Supprimer(int id, FormCollection collection)
        {
            try
            {
                client.delTournoi(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
