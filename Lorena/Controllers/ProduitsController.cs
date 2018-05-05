using Lorena.Models.Entity;
using Lorena.Models.Entity.ViewModel;
using Lorena.Models.Rules;
using Lorena.Models.Service.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lorena.Controllers
{
    public class ProduitsController : Controller
    {
        // GET: Produits
        public ActionResult Index(int ? id)
        {
            ProduitsViewModel produitsVM = new ProduitsViewModel();
            if (!id.HasValue || id.Value == 0)
            {
                produitsVM.Produits = ProduitServiceProxy.Instance().ObtenirProduits(!User.IsInRole("admin"));
            }
            else
            {
                produitsVM.Produits = ProduitServiceProxy.Instance().ObtenirProduitsParSoin(id.Value);
                ViewData["Soin"] = SoinServiceProxy.Instance().ObtenirSoin(id.Value).Libelle;
            }
            return View(produitsVM);
        }

        public ActionResult Ajouter()
        {
            Produit produit = new Produit();
            return View(produit);
        }

        [HttpPost]
        public ActionResult Ajouter(Produit produit, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                produit.ImageURL = ImageHelper.EnregistrerImage(
                    HttpContext,
                    file,
                    ProduitRules.LargeurPhotoPdtOptimale,
                    ProduitRules.HauteurPhotoPdtOptimale,
                    ProduitRules.RatioTolereMin,
                    ProduitRules.RatioTolereMax,
                    ProduitRules.LargeurPhotoPdtMax,
                    ProduitRules.DossierPhoto);

                ProduitServiceProxy.Instance().AjouterProduit(produit);

                // Affiche la liste des produits :
                return Redirect("/Produits/Index");
            }
            return View(produit);
        }

       
        public ActionResult Ajouterpanier(int id)
        {
            // Ajouter dans la session.
            ProduitRules.AjouterProduitAuPanier(id);
                        
            return new EmptyResult();
        }
    }
}