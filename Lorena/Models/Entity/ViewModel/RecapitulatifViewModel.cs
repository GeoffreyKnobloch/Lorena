using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.ViewModel
{
    /// <summary>
    /// View model permettant d'afficher un récapitulatif de la commande du client
    /// </summary>
    public class RecapitulatifViewModel
    {

        /// <summary>
        /// Rendez-vous que le client a pris
        /// </summary>
        public RendezVous RendezVous { get; set; }

        /// <summary>
        /// Produits que le client souhaite acheter
        /// </summary>
        public List<Produit> Produits { get; set; }

        /// <summary>
        /// Prix total 
        /// </summary>
        public double PrixTotal { get; set; }

        /// <summary>
        /// Message d'erreur éventuel
        /// </summary>
        public string MessageErreur { get; set; }

    }
}