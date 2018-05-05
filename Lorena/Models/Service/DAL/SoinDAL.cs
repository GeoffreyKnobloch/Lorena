using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;

namespace Lorena.Models.Service.DAL
{
    public class SoinDAL : IDalBase
    {
        #region BddContext

        private BddContext bdd;
        public SoinDAL()
        {
            bdd = new BddContext();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        internal void AjouterSoin(Soin soin)
        {
            var soinAjoute = bdd.Soins.Add(soin);
            bdd.SaveChanges();

            //Ajout des SoinProduit qui référencent les liens entre le soin et les produits:

            List<SoinProduit> soinProduit = new List<SoinProduit>();
            foreach (var produit in soin.Produits)
            {
                //Récupére le produit existant en base pour ne pas 
                //en créer un nouveau
                var produitEnBase = bdd.Produits.FirstOrDefault(p => p.Id == produit.Id);

                if (produitEnBase != null) // Utilisation du produit existant
                    soinProduit.Add(
                        new SoinProduit()
                        {
                            Produit = produitEnBase,
                            Soin = soinAjoute
                        });
                else // Possibilité de créer un produit à la volée si il n'existe pas
                    soinProduit.Add(
                        new SoinProduit()
                        {
                            Produit = produit,
                            Soin = soinAjoute
                        });
            }

            bdd.SoinProduits.AddRange(soinProduit);

            bdd.SaveChanges();

        }

        internal Soin ObtenirSoin(int id)
        {
            return bdd.Soins.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Modifier un soin
        /// </summary>
        /// <param name="soin"></param>
        internal void ModifierSoin(Soin soin)
        {
            bdd.Soins.Attach(soin);
            bdd.Entry(soin).State = System.Data.Entity.EntityState.Modified;
            bdd.SaveChanges();

            // Update des produits :
            // Solution adoptée : Supprimer les liens puis les réajouter.
            List<SoinProduit> soinProduits = new List<SoinProduit>();

            foreach (var produit in soin.Produits)
            {
                soinProduits.Add(new SoinProduit()
                {
                    Produit = produit,
                    Soin = soin
                });
            }

            var soinProduitExistantant = bdd.SoinProduits.Where(p => p.Soin.Id == soin.Id);
            bdd.SoinProduits.RemoveRange(soinProduitExistantant);
            bdd.SaveChanges();

            bdd.SoinProduits.AddRange(soinProduits);
            bdd.SaveChanges();

        }

        internal List<Soin> ObtenirSoins(bool uniquementEnVente)
        {
            if (uniquementEnVente)
            {
                return bdd.Soins.Where(p => p.EnVente == true).ToList();
            }
            else
            {
                return bdd.Soins.ToList();
            }
        }



        #endregion

    }
}