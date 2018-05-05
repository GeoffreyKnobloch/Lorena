using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.Roles
{
    public class UtilisateurRole
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Utilisateur Utilisateur { get; set; }
        public virtual Role Role { get; set; }

    }
}