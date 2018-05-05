using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{
    public class SoinProduit
    {
        [Key]
        public int Id { get; set; }
        public virtual Soin Soin { get; set; }
        public virtual Produit Produit { get; set; }

    }
}