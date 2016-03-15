using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JediWebApplication.Views.Home
{
    public class CaractéristiqueController : Controller
    {

        private ServiceReference1.Service1Client client;

        public CaractéristiqueController()
        {
           client = new ServiceReference1.Service1Client();
        }

        public ActionResult index()
        {
            return RedirectToAction("GestionCaractéristiques");
        }


        // GET: Caractéristique
        public ActionResult GestionCaractéristiques()
        {
            
            ViewBag.Caractéristiques = client.GetCaracteristiques() ;
            return View();
        }

        // GET: Caractéristique/Details/5
        /*public ActionResult Details(int id)
        {
            ViewBag.Carac = client.GetCaracteristiques().Where(c => c.Id == id);
            return View();
        }*/

        // GET: Caractéristique/Ajouter
        public ActionResult Ajouter()
        {
            return View();
        }

        // POST: Caractéristique/Ajouter
        [HttpPost]
        public ActionResult Ajouter(FormCollection collection)
        {
            try
            {
                //client.AddCarac(collection.GetValues("Nom").First(), collection.GetValues("Valeur").First(), collection.GetValues("Definition").First(), collection.GetValues("Type").First());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Caractéristique/Editer/5
        public ActionResult Editer(int id)
        {
            ViewBag.Carac = client.GetCaracteristiques().Where(c => c.Id == id).First();
            return View();
        }

        // POST: Caractéristique/Editer/5
        [HttpPost]
        public ActionResult Editer(int id, FormCollection collection)
        {
            try
            {
                //client.modCarac(id, collection.GetValues("Nom").First(), collection.GetValues("Valeur").First(), collection.GetValues("Definition").First(), collection.GetValues("Type").First());

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Caractéristique/Supprimer/5
        public ActionResult Supprimer(int id)
        {
            ViewBag.Carac = client.GetCaracteristiques().Where(c => c.Id == id).First();
            return View();
        }

        // POST: Caractéristique/Supprimer/5
        [HttpPost]
        public ActionResult Supprimer(int id, FormCollection collection)
        {
            try
            {
                client.delCarac(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
