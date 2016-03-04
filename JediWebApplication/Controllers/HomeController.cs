using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JediWebApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GestionTournois()
        {
            return View();
        }
        public ActionResult GestionMatchs()
        {
            return View();
        }
        public ActionResult GestionCaractéristiques()
        {
            return View();
        }
    }
}