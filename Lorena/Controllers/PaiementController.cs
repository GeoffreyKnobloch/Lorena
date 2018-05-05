using Lorena.Models.Entity;
using Lorena.Models.Entity.Enum;
using Lorena.Models.Entity.ViewModel;
using Lorena.Models.Rules;
using Lorena.Models.Service.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lorena.Controllers
{
    /// <summary>
    /// Besoin d'être connecter pour accéder au module de paiement
    /// </summary>
    /// <returns></returns>
    [Authorize]
    public class PaiementController : Controller
    {
         


        // GET: Paiement
        public ActionResult Index()
        {            
            return View();
        }


        public ActionResult Recapitulatif()
        {
            RecapitulatifViewModel recapitulatifVM = new RecapitulatifViewModel();

            // Les produits dans le panier : Soit une liste instanciée vide, soit remplie avec les choix
            recapitulatifVM.Produits = ProduitRules.ObtenirProduitsDansPanier();

            // Pour qu'un RDV soit faisable : l'utilisateur est authentifié (c'est fait car on est en Authorize),
            // Il a sélectionné un soin -> Dans le cache de la session
            // Il a sélectionné un créneau horaire -> idem
            // Les masseuses sont encore disponibles pour ce créneau -> Susceptible de bouger en multi user

            recapitulatifVM.MessageErreur = RendezVousRules.ConstruireRendezVousSiPossible();

            if (String.IsNullOrEmpty(recapitulatifVM.MessageErreur))
            {
                recapitulatifVM.RendezVous = RendezVousRules.ObtenirRendezVousDansPanier();
            }

            recapitulatifVM.PrixTotal = PanierRules.DeterminerPrixTotal(recapitulatifVM.RendezVous, recapitulatifVM.Produits);
            
            return View(recapitulatifVM);
        }

        public ActionResult AnnulerCommande()
        {
            // Annuler la commande
            RendezVousRules.AnnulerRendezVous();
            ProduitRules.AnnulerCommandeProduit();

            // Redirection vers la page d'accueil
            return RedirectToAction("Recapitulatif");
        }

        [HttpPost]
        public ActionResult Confirmer(RecapitulatifViewModel recapitulatifVM)
        {
            // ViewModel de paiement comprenant le récapitulatif de la commande, puis
            // les propriétés permettant d'implémenter le paiement
            PaiementViewModel paiementVM = new PaiementViewModel()
            {
                RecapitulatifVM = recapitulatifVM
            };

            // View de paiement
            return View(paiementVM);
        }

        [HttpPost]
        [Authorize(Roles = "cliente")]
        public ActionResult Payer(PaiementViewModel paiementVM)
        {

            //Ajout du rdv si il existe :

            if (paiementVM.RecapitulatifVM.RendezVous != null)
                RendezVousServiceProxy.Instance().AjouterRendezVous(paiementVM.RecapitulatifVM.RendezVous);

            Commande commande = new Commande();
            commande.Cliente = (Cliente)UtilisateurServiceProxy.Instance().ObtenirUtilisateur(Int32.Parse(User.Identity.Name));
            commande.RendezVous = paiementVM.RecapitulatifVM.RendezVous;
            commande.Produits = paiementVM.RecapitulatifVM.Produits;
            // Ajout de la commande
            CommandeServiceProxy.Instance().AjouterCommande(commande);

            // Appels services pour enregistrer le rendez-vous en base :

                       
            
            // Une fois l'intélligence appelée ici, on se redirige vers la view de suivi de ses commandes.
            return RedirectToAction("SuiviCommande");
        }


        [Authorize]
        public ActionResult SuiviCommande()
        {
            // Appels services pour voir les commandes de l'utilisateur

            SuiviCommandeViewModel suiviCommandeVM = new SuiviCommandeViewModel();

            List<Commande> commandesPassees = new List<Commande>();
            List<Commande> commandesAVenir = new List<Commande>();

            if (User.IsInRole("cliente"))
            {
                suiviCommandeVM.Message = "Suivi de mes commandes";

                foreach (var cmd in CommandeServiceProxy.Instance().ObtenirCommandesDuClient(Int32.Parse(User.Identity.Name)))
                {
                    if (cmd.EtatCommande != EnumEtatCommande.Traitee)
                    {
                        commandesAVenir.Add(cmd);
                    }
                    else
                    {
                        commandesPassees.Add(cmd);
                    }
                }
            }
            
            if (User.IsInRole("masseuse"))
            {
                suiviCommandeVM.Message = "Suivi des commandes qui me sont affectées";

                foreach (var cmd in CommandeServiceProxy.Instance().ObtenirCommandesDeMasseuse(Int32.Parse(User.Identity.Name)))
                {
                    if (cmd.EtatCommande != EnumEtatCommande.Traitee)
                    {
                        commandesAVenir.Add(cmd);
                    }
                    else
                    {
                        commandesPassees.Add(cmd);
                    }
                }
            }
            
            if (User.IsInRole("admin"))
            {
                suiviCommandeVM.Message = "Suivi des commandes nécessitants un envoi postal";

                foreach (var cmd in CommandeServiceProxy.Instance().ObtenirCommandesSansMassage())
                {
                    if (cmd.EtatCommande != EnumEtatCommande.Traitee)
                    {
                        commandesAVenir.Add(cmd);
                    }
                    else
                    {
                        commandesPassees.Add(cmd);
                    }
                }
            }

            suiviCommandeVM.CommandesAVenir = commandesAVenir;
            suiviCommandeVM.CommandesPassees = commandesPassees;

            return View(suiviCommandeVM);
        }

        public ActionResult ValiderCommande(int id)
        {
            CommandeServiceProxy.Instance().TraiterCommande(id);

            return PartialView("OuvrirCommande", id);
        }

        public ActionResult OuvrirCommande(int id)
        {
            CommandeServiceProxy.Instance().OuvrirCommande(id);

            return PartialView("ValiderCommande", id);
        }

    }
}