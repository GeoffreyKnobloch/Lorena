using Lorena.Models.Entity;
using Lorena.Models.Service.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Rules
{
    public static class ProduitRules
    {
        #region Constantes

        public const int LargeurPhotoPdtOptimale = 300;
        public const int HauteurPhotoPdtOptimale = 200;
        public const double RatioTolereMin = 1.4;
        public const double RatioTolereMax = 1.63;
        public const string DossierPhoto = "ImagesPdts";
        public const int LargeurPhotoPdtMax = 700;

        #endregion Constantes


        public static void AjouterProduitAuPanier(int id)
        {
            if (HttpContext.Current.Session["ChoixProduits"] == null)
            {
                HttpContext.Current.Session["ChoixProduits"] = new List<Produit>();
            }
            var pdt = ProduitServiceProxy.Instance().ObtenirProduit(id);
            if (pdt != null)
            {
                ((List<Produit>)HttpContext.Current.Session["ChoixProduits"]).Add(pdt);
            }
        }

        internal static double ObtenirPrixTotal(List<Produit> produits)
        {
            double total = 0;
            foreach (var item in produits)
            {
                total += item.Prix;
            }
            return total;
        }

        internal static List<Produit> ObtenirProduitsDansPanier()
        {
            if (HttpContext.Current.Session["ChoixProduits"] == null)
            {
                HttpContext.Current.Session["ChoixProduits"] = new List<Produit>();
            }
            return (List<Produit>) HttpContext.Current.Session["ChoixProduits"];
        }

        internal static void AnnulerCommandeProduit()
        {
            HttpContext.Current.Session["ChoixProduits"] = new List<Produit>();
        }

        public static void RetirerProduitDuPanier(int id)
        {
            if (HttpContext.Current.Session["ChoixProduits"] == null)
            {
                HttpContext.Current.Session["ChoixProduits"] = new List<Produit>();
            }
            var pdt = ProduitServiceProxy.Instance().ObtenirProduit(id);
            if (pdt != null)
            {
                // Récupération du produit dans la liste :
                var pdtDansListe = ((List<Produit>)HttpContext.Current.Session["ChoixProduits"]).First(p => p.Id == pdt.Id);

                if (pdtDansListe!= null)
                {
                    // Suppression du produit de la liste :
                    ((List<Produit>)HttpContext.Current.Session["ChoixProduits"]).Remove(pdtDansListe);                  
                }

            }

        }
    }
}