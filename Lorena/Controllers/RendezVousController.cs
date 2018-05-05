using Lorena.Models.Calendrier;
using Lorena.Models.Entity;
using Lorena.Models.Entity.ViewModel;
using Lorena.Models.Rules;
using Lorena.Models.Service.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lorena.Controllers
{
    public class RendezVousController : Controller
    {      
        

        public ActionResult Index(SoinsViewModel soinsVM)
        {
            // Remise à 0 de ChoixSoin :
            List<Soin> choixSoins = new List<Soin>();

            // Déterminer les soins sélectionnés :
            if (soinsVM.Soins != null && soinsVM.Soins.Count > 0)
            {
                System.Web.HttpContext.Current.Session["ChoixSoins"] = choixSoins;

                // Remise à 0 de Mon créneau  :

                System.Web.HttpContext.Current.Session["MonCreneau"] = null;

                // Si la date n'a pas encore été définie :
                if (System.Web.HttpContext.Current.Session["DateCalendrier"] == null)
                {
                    System.Web.HttpContext.Current.Session["DateCalendrier"] = DateTime.Now.Date;
                }

                // Rmq : Si on clique sur suivant, on se base sur la date mise à now ici.
                // on se fait en suite rediriger ici, et on s'assure de ne pas réécraser la date.
                

                foreach (var soinVM in soinsVM.Soins)
                {
                    if (soinVM.EstSelectionne)
                    {
                        choixSoins.Add(soinVM.Soin);
                    }
                }
            }
            if (choixSoins.Count == 0)
            {
                return Redirect("~/Soins/Index/");
            }

            // Si ChoixSoins a été valorisé : on le valorise dans l'HttpContext de la session :
            System.Web.HttpContext.Current.Session["ChoixSoins"] = choixSoins;
            return View(choixSoins);
        }        

        public ActionResult Backend()
        {
            return new Dpc()
            {
                ChoixSoins = (List<Soin>) System.Web.HttpContext.Current.Session["ChoixSoins"]
            }.CallBack(this);
        }

        /// <summary>
        /// Semaine suivante du calendrier
        /// </summary>
        /// <param name="jSONChoixSoins"></param>
        /// <returns></returns>
        public ActionResult Suivant(string jSONChoixSoins)
        {
            // Désérialization :
            var choixSoins = SerializationHelper.Deserialiser<List<Soin>>(jSONChoixSoins);

            // Blindage :
            if (System.Web.HttpContext.Current.Session["DateCalendrier"] != null)
            {
                // On passe la date du calendrier à J+7 :
                System.Web.HttpContext.Current.Session["DateCalendrier"] = ((DateTime)System.Web.HttpContext.Current.Session["DateCalendrier"]).AddDays(7);
            }
            return View("Index", choixSoins);
        }

        /// <summary>
        /// Semaine précédente du calendrier
        /// </summary>
        /// <param name="jSONChoixSoins"></param>
        /// <returns></returns>
        public ActionResult Precedent(string jSONChoixSoins)
        {
            var choixSoins = SerializationHelper.Deserialiser<List<Soin>>(jSONChoixSoins);

            // Blindage et refus de naviguer dans le passé :
            if (System.Web.HttpContext.Current.Session["DateCalendrier"] != null &&
                ((DateTime)System.Web.HttpContext.Current.Session["DateCalendrier"]).AddDays(-7) >= DateTime.Now.Date)
            {
                System.Web.HttpContext.Current.Session["DateCalendrier"] = ((DateTime)System.Web.HttpContext.Current.Session["DateCalendrier"]).AddDays(-7);
            }
            return View("Index", choixSoins);
        }

    }
}