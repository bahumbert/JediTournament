using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JediWebApplication.Views.Home
{
    public class JediController : Controller
    {

        ServiceReference1.Service1Client client;

        public JediController()
        {
            client = new ServiceReference1.Service1Client();
        }

        public ActionResult index()
        {
            return RedirectToAction("GestionJedis");
        }

        // GET: Jedi
        public ActionResult GestionJedis()
        {
            //ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            ViewBag.Jedis = client.GetJedis();
            return View();
        }

        // GET: Jedi/Details/5
        public ActionResult Details(int id)
        {
            ViewBag.Jedi = client.GetJedis().Where(c => c.Id == id).First();
            ViewBag.Caracteristiques = client.GetCaracteristiquesByJedi(id);
            return View();
            //return RedirectToAction("GestionJedis");
        }

        // GET: Jedi/Create
        public ActionResult Ajouter()
        {

            ViewBag.Force = client.GetCaracteristiquesJediForce();
            ViewBag.Defense = client.GetCaracteristiquesJediDefense();
            ViewBag.Chance = client.GetCaracteristiquesJediChance();
            ViewBag.Sante = client.GetCaracteristiquesJediSante();

            return View();
        }

        // POST: Jedi/Create
        [HttpPost]
        public ActionResult Ajouter(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                
                bool isSith;

                if (collection.GetValues("IsSith").First() == "true")
                {
                    isSith = true;
                }
                else isSith = false;

                /*ViewBag.caracForce = client.GetCaracteristiques().Where(c => c.Nom == collection.GetValues("Force").First());
                Console.WriteLine(client.GetCaracteristiques().Where(c => c.Nom == collection.GetValues("Force").First()));*/
                /*if (Request.Form["force"] != null)
                {
                    Console.WriteLine("HAHAHAHAHAHAHAHAH" + Request.Form["force"]);
                }
                else Console.WriteLine("KOUKOU");*/

                /*ViewBag.test = client.GetCaracteristiques().Where(c => c.Nom == Request.Form["force"]);*/

                client.AddJedi(collection.GetValues("Nom").First(), isSith, null, null,null,null);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jedi/Editer/5
        public ActionResult Editer(int id)
        {
            ViewBag.Jedi = client.GetJedis().Where(c => c.Id == id).First();
            return View();
        }

        // POST: Jedi/Editer/5
        [HttpPost]
        public ActionResult Editer(int id, FormCollection collection)
        {
            try
            {

                bool isSith;

                if (collection.GetValues("IsSith").First() == "true")
                {
                    isSith = true;
                }
                else isSith = false;

                client.modJedi(id, collection.GetValues("Nom").First(), isSith, null, null, null, null);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jedi/Supprimer/5
        public ActionResult Supprimer(int id)
        {
            ViewBag.Jedi = client.GetJedis().Where(c => c.Id == id).First();
            return View();
        }

        // POST: Jedi/Supprimer/5
        [HttpPost]
        public ActionResult Supprimer(int id, FormCollection collection)
        {
            try
            {
                client.delJedi(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
