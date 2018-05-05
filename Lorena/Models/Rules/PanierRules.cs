using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;

namespace Lorena.Models.Rules
{
    public class PanierRules
    {
        internal static double DeterminerPrixTotal(RendezVous rendezVous, List<Produit> produits)
        {
            double prixTotal = 0;

            if (rendezVous != null)
                prixTotal += SoinRules.ObtenirPrixTotal(rendezVous.ChoixSoins);

            if (produits != null)
                prixTotal += ProduitRules.ObtenirPrixTotal(produits);
            
            return prixTotal;
        }
    }
}