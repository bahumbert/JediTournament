using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JediWebApplication.Views.Home
{
    public class StadeController : Controller
    {

        private ServiceReference1.Service1Client client;

        public StadeController()
        {
            client = new ServiceReference1.Service1Client();
        }

        public ActionResult index()
        {
            return RedirectToAction("GestionStades");
        }

        // GET: Stade
        public ActionResult GestionStades()
        {
            ViewBag.Stades = client.GetStades();
            return View();
        }

        // GET: Stade/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Stades = client.GetStades().Where(s => s.Id == id).First();
            return View();
        }

        // GET: Stade/Ajouter
        public ActionResult Ajouter()
        {
            ViewBag.Force = client.GetCaracteristiquesStadeForce();
            ViewBag.Defense = client.GetCaracteristiquesStadeDefense();
            ViewBag.Chance = client.GetCaracteristiquesStadeChance();
            ViewBag.Sante = client.GetCaracteristiquesStadeSante();
            return View();
        }

        // POST: Stade/Ajouter
        [HttpPost]
        public ActionResult Ajouter(FormCollection collection)
        {
            try
            {
                client.AddStade(int.Parse(collection.GetValues("NbPlaces").First()), collection.GetValues("Nom").First(), collection.GetValues("Planete").First(),null,null,null,null);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stade/Editer/5
        public ActionResult Editer(int id)
        {
            ViewBag.Stades = client.GetStades().Where(s => s.Id == id).First();
            ViewBag.Caracteristiques = client.GetCaracteristiquesByStade(id);
            return View();
        }

        // POST: Stade/Editer/5
        [HttpPost]
        public ActionResult Editer(int id, FormCollection collection)
        {
            try
            {
                client.modStade(id, int.Parse(collection.GetValues("NbPlaces").First()), collection.GetValues("Nom").First(), collection.GetValues("Planete").First(), null, null, null, null);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Stade/Supprimer/5
        public ActionResult Supprimer(int id)
        {
            ViewBag.Stades = client.GetStades().Where(s => s.Id == id).First();
            return View();
        }

        // POST: Stade/Supprimer/5
        [HttpPost]
        public ActionResult Supprimer(int id, FormCollection collection)
        {
            try
            {
                client.delStade(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
