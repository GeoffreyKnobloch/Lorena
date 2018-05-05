using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{
    public class RendezVousSoin
    {
        [Key]
        public int Id { get; set; }

        public virtual RendezVous RendezVous { get; set; }

        public virtual Soin Soin { get; set; }
        
    }
}