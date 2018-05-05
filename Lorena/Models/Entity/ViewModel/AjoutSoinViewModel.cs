using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.ViewModel
{
    public class AjoutSoinViewModel
    {
        public Soin Soin { get; set; }

        public List<ProduitVIewModel> Produits { get; set; }

    }
}