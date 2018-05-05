using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{
    public class Produit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Veuillez spécifier un libellé pour le produit")]
        [Display(Name = "Libellé")]
        public string Libelle { get; set; }

        public double Prix { get; set; }

        [Display(Name = "Disponible à la vente")]
        public bool EnVente { get; set; }

        public string Description { get; set; }

        [Display(Name ="Image")]
        public string ImageURL { get; set; }
    }
}