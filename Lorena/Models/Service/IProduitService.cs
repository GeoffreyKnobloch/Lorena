using Lorena.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorena.Models.Service
{
    interface IProduitService
    {
        #region CRUD

        List<Produit> ObtenirProduits(bool uniquementEnVente);

        Produit ObtenirProduit(int id);

        int AjouterProduit(Produit produit);

        void ModifierProduit(Produit produit);

        void SupprimerProduit(int produitId);

        #endregion CRUD

        List<Produit> ObtenirProduitsParSoin(int soinId);



    }
}
