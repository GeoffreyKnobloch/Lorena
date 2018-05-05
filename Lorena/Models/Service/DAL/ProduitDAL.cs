using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;

namespace Lorena.Models.Service.DAL
{
    public class ProduitDAL : IDalBase
    {
        #region BddContext

        private BddContext bdd;
        public ProduitDAL()
        {
            bdd = new BddContext();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        #endregion BddContext

        #region CRUD

        internal int AjouterProduit(Produit produit)
        {
            var pdt = bdd.Produits.Add(produit);
            bdd.SaveChanges();
            return pdt.Id;
        }

        internal void ModifierProduit(Produit produit)
        {
            var actuel = bdd.Produits.FirstOrDefault(p => p.Id == produit.Id);
            actuel.Libelle = produit.Libelle;
            actuel.Description = produit.Description;
            
            bdd.SaveChanges();
        }

        internal Produit ObtenirProduit(int id)
        {
            return bdd.Produits.First(p => p.Id == id);
        }

        internal List<Produit> ObtenirProduits()
        {
            return bdd.Produits.ToList();
        }

        internal void SupprimerProduit(int produitId)
        {
            var produitASupprimer = bdd.Produits.FirstOrDefault(p => p.Id == produitId);
            bdd.Produits.Remove(produitASupprimer);
            bdd.SaveChanges();
        }

        internal List<Produit> ObtenirProduitsParSoin(int soinId)
        {
            var soinProduits = bdd.SoinProduits.Where(p => p.Soin.Id == soinId);

            List<Produit> resultat = new List<Produit>();
            foreach (var soinProduit in soinProduits)
            {
                Produit produit = bdd.Produits.FirstOrDefault(p => p.Id == soinProduit.Produit.Id);
                if (produit != null)
                {
                    resultat.Add(produit);
                }
            }
            return resultat;            
        }

        #endregion CRUD


    }
}