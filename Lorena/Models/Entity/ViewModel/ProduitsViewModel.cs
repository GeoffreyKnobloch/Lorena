using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.ViewModel
{
    public class ProduitsViewModel
    {
        public List<Produit> Produits { get; set; }
        public int ProduitSelectionneId { get; set; }
    }
}