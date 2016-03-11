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
            client.Close();
            return View();
        }

        // GET: Jedi/Details/5
        public ActionResult Details(int id)
        {

            return View();
            //return RedirectToAction("GestionJedis");
        }

        // GET: Jedi/Create
        public ActionResult Ajouter()
        {
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
                
                client.AddJedi(collection.GetValues("Nom").First(), isSith, null, null, null, null);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jedi/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Jedi/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Jedi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Jedi/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
