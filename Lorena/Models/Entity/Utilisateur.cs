using Lorena.Models.Entity.Roles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{
    /// <summary>
    /// Utilisateur du site
    /// </summary>
    public class Utilisateur
    {
        [Key, Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Index(IsUnique = true)]
        [Required(ErrorMessage = "Le login est obligatoire")]
        [Key, Column(Order = 1)]
        public string Login { get; set; }

        [Required(ErrorMessage = "Le mot de passe est obligatoire")]
        [Display(Name ="Mot de passe")]
        public string MdpCripte { get; set; }

        public string Nom { get; set; }
                
        public string Prenom { get; set; }

        public virtual ICollection<UtilisateurRole> Roles { get; set; }

        public string Email { get; set; }

        public string Commentaire { get; set; }

        /// <summary>
        /// Date de création du compte
        /// </summary>
        public DateTime DateCreation { get; set; }

        /// <summary>
        /// Date de naissance
        /// </summary>
        /// <remarks>A été mis nullable pour éviter un conflit en bdd lorsqu'on ne renseignait pas de date de naissance</remarks>
        [Display(Name= "Date de naissance")]
        public DateTime ? DateNaissance { get; set; }

        /// <summary>
        /// Liste des rendezVous
        /// </summary>
        [NotMapped]
        public virtual List<RendezVous> RendezVous { get; set; }
    }
}