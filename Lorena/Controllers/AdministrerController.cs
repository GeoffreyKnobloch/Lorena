using Lorena.Models.Service.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lorena.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdministrerController : Controller
    {
        // GET: Administrer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LancerMaintenance()
        {
            // Appel des opérations de maintenance :

            //RendezVousServiceProxy.Instance().NettoyerCreneauxHoraires();

            return null;
        }

    }
}