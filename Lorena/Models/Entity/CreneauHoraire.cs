using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{
    /// <summary>
    /// Information concernant un creneau horaire
    /// </summary>
    public class CreneauHoraire
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Texte affiché dans le calendrier
        /// </summary>
        [Required]
        public string Text { get; set; }

        /// <summary>
        /// Date début
        /// </summary>
        [Required]
        public DateTime DateDebut { get; set; }

        /// <summary>
        /// Date fin
        /// </summary>
        [Required]
        public DateTime DateFin { get; set; }
 
    }
}