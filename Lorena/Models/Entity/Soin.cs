using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{
    public class Soin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Prix du soin
        /// </summary>
        [Required(ErrorMessage = "Le prix du soin doit être saisit")]
        public double Prix { get; set; }

        /// <summary>
        /// Libellé du soin
        /// </summary>
        [Required(ErrorMessage = "Veuillez spécifier un nom pour le soin")]
        [Display(Name = "Libellé")]
        public string Libelle { get; set; }

        /// <summary>
        /// URL de l'image stockée sur le serveur correspondant au soin
        /// </summary>
        [Display(Name = "Image")]
        public string ImageURL { get; set; }
                
        /// <summary>
        /// Description du soin
        /// </summary>
        [Display(Name="Description")]
        public string Description { get; set; }

        /// <summary>
        /// Liste des produits utilisés pour le soin
        /// </summary>
        [Display(Name = "Produits utilisés")]
        [NotMapped]
        public List<Produit> Produits { get; set; }

        /// <summary>
        /// Durée en minute du soin
        /// </summary>
        [Display (Name ="Durée")]
        [Required(ErrorMessage = "Veuillez spécifier combien de temps dure le soin")]
        public int Duree { get; set; }


        /// <summary>
        /// Détermine si le soin est proposé aux clients ou non
        /// </summary>
        [Display(Name = "Disponible")]
        public bool EnVente { get; set; }
    }
}