using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.ViewModel
{
    public class RendezVousViewModel
    {
        /// <summary>
        /// L'entité à valoriser pour que l'utilisateur choisisse un RDV
        /// </summary>
        public RendezVous RendezVous { get; set; }

        /// <summary>
        /// L'ensemble des créneaux horraires indisponibles (RDV futurs)
        /// </summary>
        public List<CreneauHoraire> CreneauIndisponible { get; set; }

        /// <summary>
        /// Horraires d'ouverture
        /// </summary>
        /// <example>HorraireOuverture["lundi"] = new double[4] {9, 12, 13, 17} => horraire d'ouverture le lundi 9h-12h 13h-17h</example>
        public Dictionary<string, double[]> HoraireOuverture { get; set; }

    }
}