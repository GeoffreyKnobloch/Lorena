using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lorena.Controllers
{
    /// <summary>
    /// Controller Home
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "A propos";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact";

            return View();
        }
    }
}