using Lorena.Models.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;

namespace Lorena.Models.Service.Proxy
{
    public class ProduitServiceProxy : IProduitService
    {
        #region Singleton

        /// <summary>
        /// Singleton :
        /// </summary>
        private static readonly ProduitServiceProxy _instance = new ProduitServiceProxy();
        private ProduitService _produitService;

        /// <summary>
        /// Protection pour appel multithread unsafe 
        /// </summary>
        private static object syncLock = new object();

        protected ProduitServiceProxy()
        {
            _produitService = new ProduitService();
        }

        public static ProduitServiceProxy Instance()
        {
            return _instance;
        }



        #endregion

        #region CRUD

        public List<Produit> ObtenirProduits(bool uniquementEnVente)
        {
            return _produitService.ObtenirProduits(uniquementEnVente);
        }

        public int AjouterProduit(Produit produit)
        {
            return _produitService.AjouterProduit(produit);
        }

        public void ModifierProduit(Produit produit)
        {
            _produitService.ModifierProduit(produit);
        }

        public void SupprimerProduit(int produitId)
        {
            _produitService.SupprimerProduit(produitId);
        }

        public List<Produit> ObtenirProduitsParSoin(int soinId)
        {
            return _produitService.ObtenirProduitsParSoin(soinId);
        }

        public Produit ObtenirProduit(int id)
        {
            return _produitService.ObtenirProduit(id);
        }

        #endregion CRUD
    }
}