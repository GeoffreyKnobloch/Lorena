using DayPilot.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DayPilot.Web.Mvc.Events.Calendar;
using Lorena.Models.Service.DAL;
using Lorena.Models.Entity;
using DayPilot.Web.Mvc.Enums;
using Lorena.Models.Rules;
using Lorena.Controllers;
using Lorena.Models.Service.Proxy;

namespace Lorena.Models.Calendrier
{
    public class Dpc : DayPilotCalendar
    {
        #region Propriétées

        private BddContext db = new BddContext();
        public List<Soin> ChoixSoins { get; set; }

        private int _tempsSoinChoisis;

        public int TempsSoinChoisis
        {
            get
            {
                if (_tempsSoinChoisis == 0)
                {
                    _tempsSoinChoisis = SoinRules.ObtenirTempsTotal(ChoixSoins);
                }
                return _tempsSoinChoisis;
            }
        }

        #endregion Propriétées


        protected override void OnInit(InitArgs e)
        {
            // Chargement des Events :

            /*old :
            Events = from ev in db.CreneauxHoraires select ev;
            */

            // Affichage des créneaux horaires correspondants à des vrais RDV :

            // Gros code dupliqué avec OnFinish

            List<CreneauHoraire> creneauxAAfficher = new List<CreneauHoraire>();

            var nonDispos = RendezVousServiceProxy.Instance().ObtenirCreneauxIndisponibles();
            // liste des rendezVous :
            //var rdvs = db.RendezVous.ToList();
            if (nonDispos != null && nonDispos.Count > 0)
            {
                foreach (var rdv in nonDispos)
                {
                    rdv.Text = "Non disponible";
                    creneauxAAfficher.Add(rdv);
                }
            }

            CreneauHoraire creneauUtilisateur = (CreneauHoraire)HttpContext.Current.Session["MonCreneau"];
            // Si l'utilisateur est en train de faire un créneau à lui :
            if (creneauUtilisateur != null)
            {
                // on va l'afficher :
                creneauxAAfficher.Add(creneauUtilisateur);
            }

                Events = creneauxAAfficher;

            DataIdField = "Id";
            DataTextField = "Text";
            DataStartField = "DateDebut";
            DataEndField = "DateFin";
            
            Update();
        }


        protected override void OnEventResize(EventResizeArgs e)
        {
            int idElement = Convert.ToInt32(e.Id);

            if (0 == idElement) // Si il s'agit bien de mon élément, qui n'est pas en base, donc id = 0
            {
                //var toBeResized = (from ev in db.CreneauxHoraires where ev.Id == idElement select ev).First();

                var toBeResized = (CreneauHoraire)HttpContext.Current.Session["MonCreneau"];

                if (e.NewEnd != e.OldEnd)
                {
                    // On a tiré par le bas :
                    toBeResized.DateFin = e.NewEnd;
                    toBeResized.DateDebut = e.NewEnd.AddMinutes(-TempsSoinChoisis);
                }
                else
                {
                    // On a tiré par le haut :
                    toBeResized.DateDebut = e.NewStart;
                    toBeResized.DateFin = e.NewStart.AddMinutes(TempsSoinChoisis);
                }

                var masseusesDispo = RendezVousServiceProxy.Instance().ObtenirMasseusesDisponibles(toBeResized);
                // Si il y a bien une masseuse dispo :
                if (masseusesDispo == null || masseusesDispo.Count == 0)
                {
                    //Rollback :
                    toBeResized.DateDebut = e.OldStart;
                    toBeResized.DateFin = e.OldEnd;
                }

                // Ligne inutile : ref = même ref.
                HttpContext.Current.Session["MonCreneau"] = toBeResized;

                //db.SaveChanges();
            }
            Update();
        }

        protected override void OnEventMove(EventMoveArgs e)
        {
            
            int idElement = Convert.ToInt32(e.Id);

            if (0 == idElement) // Si il s'agit bien de mon RDV (sinon j'ai pas le droit de le bouger)
            {                
                var toBeResized = (CreneauHoraire)HttpContext.Current.Session["MonCreneau"];

                toBeResized.DateDebut = e.NewStart;
                toBeResized.DateFin = e.NewStart.AddMinutes(TempsSoinChoisis);
                //db.SaveChanges();

                // Vérifier la légalité :

                var masseusesDispo = RendezVousServiceProxy.Instance().ObtenirMasseusesDisponibles(toBeResized);

                // Si créneau illégal :
                if (masseusesDispo == null || masseusesDispo.Count == 0)
                {
                    // Rollback :
                    toBeResized.DateDebut = e.OldStart;
                    toBeResized.DateFin = e.OldEnd;
                }
            }


            Update();
        }

        protected override void OnTimeRangeSelected(TimeRangeSelectedArgs e)
        {
                
                

                string texteConventionne = string.Empty;
                foreach (var soin in ChoixSoins)
                {
                    texteConventionne += soin.Libelle + ", ";
                }
                texteConventionne += " pour " + TempsSoinChoisis + "minutes.";

                var toBeCreated = new CreneauHoraire
                {
                    DateDebut = e.Start,
                    DateFin = e.Start.AddMinutes(TempsSoinChoisis), // Le temps est fixé par le choix du soin
                    Text = texteConventionne
                };

                var masseusesDispo = RendezVousServiceProxy.Instance().ObtenirMasseusesDisponibles(toBeCreated);

            // Si le creneau choisit est légal : Si il y a bien une masseuse dispo :
                if (masseusesDispo != null && masseusesDispo.Count > 0)
                    {
                        HttpContext.Current.Session["MonCreneau"] = toBeCreated;
                    }
            
            Update();
        }

        protected override void OnFinish()
        {       

            if (UpdateType == CallBackUpdateType.None)
            {
                return;
            }

            //Events = from ev in db.CreneauxHoraires select ev;

            List<CreneauHoraire> creneauxAAfficher = new List<CreneauHoraire>();

            var nonDispos = RendezVousServiceProxy.Instance().ObtenirCreneauxIndisponibles();
            // liste des rendezVous :
            //var rdvs = db.RendezVous.ToList();
            if (nonDispos != null && nonDispos.Count > 0)
            {
                foreach (var rdv in nonDispos)
                {
                    rdv.Text = "Non disponible";
                    creneauxAAfficher.Add(rdv);
                }
            }

            CreneauHoraire elementUtilisateur = (CreneauHoraire)HttpContext.Current.Session["MonCreneau"];
            // Si l'utilisateur est en train de faire un créneau à lui :
            if (elementUtilisateur != null)
            {
                // on va l'afficher :
                creneauxAAfficher.Add(elementUtilisateur);
            }

            Events = creneauxAAfficher;

            DataIdField = "Id";
            DataTextField = "Text";
            DataStartField = "DateDebut";
            DataEndField = "DateFin";
        }


    }
}