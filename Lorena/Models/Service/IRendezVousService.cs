using Lorena.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorena.Models.Service
{
    interface IRendezVousService
    {
        /// <summary>
        /// Ajoute le rendezVous en base.
        /// Lie le client à la masseuse pour le creneau horaire donné.
        /// </summary>
        /// <param name="rendezVous"></param>
        /// <returns></returns>
        int AjouterRendezVous(RendezVous rendezVous);

        /// <summary>
        /// Renvoie la liste des créneaux horaires futures ou aucune masseuse n'est disponible
        /// </summary>
        /// <returns></returns>
        List<CreneauHoraire> ObtenirCreneauxIndisponibles();

        /// <summary>
        /// Renvoie la liste des masseuses disponibles à un créneau horaire donné.
        /// </summary>
        /// <param name="creneau"></param>
        /// <returns></returns>
        List<Masseuse> ObtenirMasseusesDisponibles(CreneauHoraire creneau);

        /// <summary>
        /// Obtenir un creneau horaire par son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CreneauHoraire ObtenirCreneau(int id);

    }
}
