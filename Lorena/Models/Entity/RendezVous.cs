using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{
    public class RendezVous
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual Masseuse Masseuse { get; set; }

        [Display(Name = "Créneau horaire")]
        public virtual CreneauHoraire Creneau { get; set; }

        [Display(Name = "Soins choisis")]
        [NotMapped]
        public virtual List<Soin> ChoixSoins { get; set; }

        public string Commentaire { get; set; }
                
    }
}