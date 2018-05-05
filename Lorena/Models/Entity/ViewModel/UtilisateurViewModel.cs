using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.ViewModel
{
    public class UtilisateurViewModel
    {
        /// <summary>
        /// Utilisateur (Cliente, masseuse, admin ni client ni masseuse)
        /// </summary>
        public Utilisateur Utilisateur { get; set; }

        /// <summary>
        /// Atteste si l'utilisateur est identifié
        /// </summary>
        public bool EstIdentifie { get; set; }

    }
}