using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JediWebApplication.Views.Home
{
    public class CaractéristiqueController : Controller
    {
        // GET: Caractéristique
        public ActionResult GestionCaractéristiques()
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            ViewBag.Caractéristiques = client.GetCaracteristiques() ;
            client.Close();
            return View();
        }

        // GET: Caractéristique/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Caractéristique/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Caractéristique/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Caractéristique/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Caractéristique/Edit/5
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

        // GET: Caractéristique/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Caractéristique/Delete/5
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
