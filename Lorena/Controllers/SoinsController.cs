using Lorena.Models.Entity.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lorena.Models.Entity;
using Lorena.Models.Service.Proxy;
using System.IO;
using System.Web.Helpers;
using Lorena.Models.Rules;

namespace Lorena.Controllers
{
    public class SoinsController : Controller
    {
        
        const double ratioTolereLargeurHauteurBas = 1.4;
        const double ratioTolereLargeurHauteurHaut = 1.63;


        // GET: Soins
        /// <summary>
        /// Action menant à la carte des soins, l'utilisateur peut les consulter, et quand il y en a un qui lui plaît,
        /// il peut le sélectionner et aller le réserver dans la view de la réservation, avec ce soin déjà pré-selectionné.
        /// Les soins sont stockés en base avec leur image correspondante (ajoutable en backoffice par un admin)
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {

            //Appel service pour obtenir la liste des soins à afficher :
            SoinsViewModel soinsVM = new SoinsViewModel();
                var soins = SoinServiceProxy.Instance().ObtenirSoins(!User.IsInRole("admin"));
            soinsVM.Soins = new List<SoinViewModel>();
            foreach (var soin in soins)
            {
                soinsVM.Soins.Add(new SoinViewModel()
                {
                    EstSelectionne = false,
                    Soin = soin
                });
            }            
            return View(soinsVM);
        }

        /// <summary>
        /// Ajouter un soin : Fenêtre de saisie des caractèristiques d'un soin.
        /// </summary>
        /// <returns></returns>
        public ActionResult Ajouter()
        {
            AjoutSoinViewModel ajoutSoinVM = new AjoutSoinViewModel();
            ajoutSoinVM.Produits = new List<ProduitVIewModel>();

            // Appel service pour récupérer la liste des produits :

            foreach (var produit in ProduitServiceProxy.Instance().ObtenirProduits(false))
            {
                ajoutSoinVM.Produits.Add(new ProduitVIewModel()
                {
                    Produit = produit,
                    EstSelectionne = false
                });
            }

            return View(ajoutSoinVM);
        }

        /// <summary>
        /// Post de l'ajout du soin
        /// </summary>
        /// <param name="soinVM"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Ajouter(AjoutSoinViewModel ajoutSoinVM, HttpPostedFileBase file)
        {
            // si le model est valide :
            if (ModelState.IsValid)
            {

                ajoutSoinVM.Soin.ImageURL = ImageHelper.EnregistrerImage(
                    HttpContext,
                    file,
                    SoinRules.LargeurPhotoSoinOptimale,
                    SoinRules.HauteurPhotoSoinOptimale,
                    SoinRules.RatioTolereMin,
                    SoinRules.RatioTolereMax,
                    SoinRules.LargeurPhotoSoinMax,
                    SoinRules.DossierPhoto);
                
                    if (ajoutSoinVM.Soin.Produits == null)
                    ajoutSoinVM.Soin.Produits = new List<Produit>();
                if (ajoutSoinVM.Produits != null)
                {
                    foreach (var produitVM in ajoutSoinVM.Produits)
                    {
                        if (produitVM.EstSelectionne)
                        {
                            ajoutSoinVM.Soin.Produits.Add(produitVM.Produit);
                        }
                    }
                }
                // Appel service pour ajouter le soin à la liste des soins :
                SoinServiceProxy.Instance().AjouterSoin(ajoutSoinVM.Soin);

                // On affiche la liste des soins
                return Redirect("/Soins/Index");
            }            

            return View(ajoutSoinVM);
        }
    }
}