using Lorena.Models.Entity.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{

    /// <summary>
    /// Entité représentant une commande
    /// </summary>
    public class Commande
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Cliente")]
        public virtual Cliente Cliente { get; set; }
                
        [NotMapped]
        public List<Produit> Produits { get; set; }

        [Display(Name ="Rendez-vous")]
        public virtual RendezVous RendezVous { get; set; }

        [Display(Name ="Date de la prise de commande")]
        public DateTime DateCreation { get; set; }

        [Display(Name ="Date de la résolution de la commande")]
        public DateTime ? DateReception { get; set; }

        [Display(Name ="État de la commande")]
        public EnumEtatCommande EtatCommande { get; set; }

    }
}