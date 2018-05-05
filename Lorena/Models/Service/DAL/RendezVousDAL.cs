using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;

namespace Lorena.Models.Service.DAL
{
    public class RendezVousDAL : IDalBase
    {
        #region BddContext

        private BddContext bdd;
        public RendezVousDAL()
        {
            bdd = new BddContext();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }

        #endregion BddContext

        /// <summary>
        /// Ajouter un RDV
        /// </summary>
        /// <param name="rendezVous"></param>
        /// <returns></returns>
        internal int AjouterRendezVous(RendezVous rendezVous)
        {
            // Se base sur cliente et masseuse existante :
            rendezVous.Cliente = bdd.Clientes.First(p => p.Id == rendezVous.Cliente.Id);
            rendezVous.Masseuse = bdd.Masseuses.First(p => p.Id == rendezVous.Masseuse.Id);

            // Création du créneau à la volée :
            rendezVous.Creneau = bdd.CreneauxHoraires.Add(rendezVous.Creneau);
            
            var rdv = bdd.RendezVous.Add(rendezVous);
            

            bdd.SaveChanges();

            // Gestion du choix des soins :
            foreach (var soin in rendezVous.ChoixSoins)
            {
                var soinEnBase = bdd.Soins.FirstOrDefault(p => p.Id == soin.Id);

                RendezVousSoin rdvSoin = new RendezVousSoin()
                {
                    RendezVous = rdv,
                    Soin = soinEnBase
                };
                bdd.RendezVousSoin.Add(rdvSoin);

               
            }
            bdd.SaveChanges();

            return rdv.Id;
        }

        internal CreneauHoraire ObtenirCreneau(int id)
        {
            return bdd.CreneauxHoraires.FirstOrDefault(p => p.Id == id);
        }

        /// <summary>
        /// Obtenir les créneaux indisponibles, ou toutes les masseuses sont en RDV.
        /// </summary>
        /// <param name="now"></param>
        /// <returns></returns>
        internal List<CreneauHoraire> ObtenirCreneauxIndisponibles(DateTime now)
        {
            List<CreneauHoraire> creneauFuturIndispo = new List<CreneauHoraire>();
            // Obtention des rdv après la date passée en paramètre :
            var rdvs = bdd.RendezVous.Where(p => p.Creneau.DateDebut > now);

            foreach (var rdv in rdvs)
            {
                // Si il y n'y a pas une autre masseuse de dispo :
                var masseusesDispo = ObtenirMasseusesDisponibles(rdv.Creneau);
                if (masseusesDispo == null || masseusesDispo.Count == 0)
                {
                    // Alors on ajoute ce créneau aux créneaux indisponibles :
                    creneauFuturIndispo.Add(rdv.Creneau);
                }
            }

            for (int i = 0; i < 30; i++)
            {
                // Impossible de commander en arrière (30 jours sont supportés) :
                var date = DateTime.Now.Date.AddDays(-i);

                string afficherText = string.Empty;
                if (i==0)
                {
                    afficherText = "Vous pouvez choisir un rendez-vous pour demain si vous le souhaitez, mais pas le jour même";
                }

                creneauFuturIndispo.Add(new CreneauHoraire()
                {
                    DateDebut = date,
                    DateFin = date.AddDays(1),
                    Text = afficherText
                });

                //Impossible de commander avant 9h du mat et après 18h : (30 jours sont supportés)
                var dateFutur = DateTime.Now.Date.AddDays(i + 1);
                creneauFuturIndispo.Add(new CreneauHoraire()
                {
                    DateDebut = dateFutur,
                    DateFin = dateFutur.AddHours(9),
                    Text = "Nos horaires : 9h-18h"
                });
                creneauFuturIndispo.Add(new CreneauHoraire()
                {
                    DateDebut = dateFutur.AddHours(17),
                    DateFin = dateFutur.AddDays(1),
                    Text = String.Empty
                });

            }


            return creneauFuturIndispo;
        }

        /// <summary>
        /// Obtenir les masseuses disponibles à un créneau donné
        /// </summary>
        /// <param name="creneau"></param>
        /// <returns></returns>
        internal List<Masseuse> ObtenirMasseusesDisponibles(CreneauHoraire creneau)
        {
            List<Masseuse> masseusesDisponiblePourCreneau = bdd.Masseuses.ToList();

            // Recherche des RDV qui empiètent sur ce créneau :
            var rdvsSurCreneau = bdd.RendezVous.Where(p => !(creneau.DateDebut > p.Creneau.DateFin || creneau.DateFin < p.Creneau.DateDebut));

            if (rdvsSurCreneau != null && rdvsSurCreneau.Count() != 0)
            {
                foreach (var rdv in rdvsSurCreneau)
                {
                    masseusesDisponiblePourCreneau.RemoveAll(p => p.Id == rdv.Masseuse.Id);
                }
            }
            return masseusesDisponiblePourCreneau;
            
        }

        

    }
}