using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Service.Proxy;

namespace Lorena.Models.Rules
{
    public static class RendezVousRules
    {
        /// <summary>
        /// Donne les horaires de l'entreprise, ici stocké en dur. Une futur implémentation prévoit un module permettant de modifier ces données en bdd.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, double[]> ObtenirHoraires()
        {
            Dictionary<string, double[]> horaires = new Dictionary<string, double[]>();
            horaires["lundi"] = new double[4] { 9, 12, 14, 17 };
            horaires["mardi"] = new double[4] { 9, 12, 14, 17 };
            horaires["mercredi"] = new double[4] { 9, 12, 14, 17 };
            horaires["jeudi"] = new double[4] { 9, 12, 14, 17 };
            horaires["vendredi"] = new double[4] { 9, 12, 14, 17 };
            horaires["samedi"] = new double[4] { 9, 12, 14, 17 };
            horaires["dimanche"] = new double[4] { 10, 12, 14, 17 };

            return horaires;
        }

        internal static RendezVous ObtenirRendezVousDansPanier()
        {
            return (RendezVous) HttpContext.Current.Session["ChoixRDV"];
        }

        internal static void AnnulerRendezVous()
        {
            HttpContext.Current.Session["ChoixRDV"] = null;
            HttpContext.Current.Session["ChoixSoins"] = null;
            HttpContext.Current.Session["MonCreneau"] = null;
        }

        /// <summary>
        /// Construit un rendez-vous et renvoie le message d'erreur si ça n'a pas été possible
        /// </summary>
        /// <returns>Message d'erreur (empty si ok)</returns>
        /// <remarks>Vraiment pas propre de retourner un string comme ça ^^</remarks>
        internal static string ConstruireRendezVousSiPossible()
        {
            RendezVous rdv = new RendezVous();

            rdv.ChoixSoins = (List<Soin>)HttpContext.Current.Session["ChoixSoins"];
            if (rdv.ChoixSoins != null)
            {
                
                rdv.Cliente = (Cliente) UtilisateurServiceProxy.Instance().ObtenirUtilisateur(HttpContext.Current.User.Identity.Name);
                if (rdv.Cliente != null)
                {
                    rdv.Creneau = (CreneauHoraire) HttpContext.Current.Session["MonCreneau"];

                    if (rdv.Creneau != null)
                    {
                        var masseusesDispos = RendezVousServiceProxy.Instance().ObtenirMasseusesDisponibles(rdv.Creneau);
                        if (masseusesDispos.Count > 0)
                        {
                            // Assignation parmi les masseuses disponibles
                            rdv.Masseuse = masseusesDispos[new Random().Next(0, masseusesDispos.Count)];

                            HttpContext.Current.Session["ChoixRDV"] = rdv;

                            return string.Empty;
                        }
                        else
                        {
                            return "Malheureusement, il n'y a plus de masseuses disponibles pour le créneau que vous avez choisit. Veuillez choisir un autre rendez-vous.";
                        }
                    }
                    else
                    {
                        return "Vous n'avez pas sélectionnés de Rendez-Vous.";
                    }
                }
                else
                {
                    return "Vous devez vous authentifier.";
                }
            }
            else
            {
                return "Vous n'avez pas choisit de soins.";
            }
            
        }
    }
}