using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Service.DAL;

namespace Lorena.Models.Service.Implementation
{
    public class ProduitService : IProduitService
    {
        #region CRUD
        public int AjouterProduit(Produit produit)
        {
            int produitId;
            using (ProduitDAL dal = new ProduitDAL())
            {
                produitId = dal.AjouterProduit(produit);
            }
            return produitId;
        }

        public void ModifierProduit(Produit produit)
        {
            using (ProduitDAL dal = new ProduitDAL())
            {
                dal.ModifierProduit(produit);
            }
        }

        public Produit ObtenirProduit(int id)
        {
            Produit produit;
            using (ProduitDAL dal = new ProduitDAL())
            {
                produit = dal.ObtenirProduit(id);
            }
            return produit;
        }

        public List<Produit> ObtenirProduits(bool uniquementEnVente)
        {
            List<Produit> liste;

            using (ProduitDAL dal = new ProduitDAL())
            {
                if (!uniquementEnVente)
                {
                    liste = dal.ObtenirProduits();
                }
                else
                {
                    liste = dal.ObtenirProduits().Where(p => p.EnVente).ToList();
                }
                
            }
            return liste;
        }

        public List<Produit> ObtenirProduitsParSoin(int soinId)
        {
            List<Produit> liste;

            using (ProduitDAL dal = new ProduitDAL())
            {
                
                    liste = dal.ObtenirProduitsParSoin(soinId);
               

            }
            return liste;
        }

        public void SupprimerProduit(int produitId)
        {
            using (ProduitDAL dal = new ProduitDAL())
            {
                dal.SupprimerProduit(produitId);
            }
        }

        #endregion CRUD
    }
}