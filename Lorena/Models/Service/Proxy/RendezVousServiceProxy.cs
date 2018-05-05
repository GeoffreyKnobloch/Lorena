using Lorena.Models.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;

namespace Lorena.Models.Service.Proxy
{

    /// <summary>
    /// Proxy permettant l'accès au service RendezVous (singleton)
    /// </summary>
    public class RendezVousServiceProxy : IRendezVousService
    {
        #region singleton
        private static RendezVousServiceProxy _instance;
        private RendezVousService _rendezVousService;

        /// <summary>
        /// Protection pour appel multithread unsafe 
        /// </summary>
        private static object syncLock = new object();

        /// <summary>
        /// Consstructeur Protected
        /// </summary>
        protected RendezVousServiceProxy()
        {
            _rendezVousService = new RendezVousService();
        }
          
        /// <summary>
        /// Instance du singleton RendezVousServiceProxy
        /// </summary>
        /// <returns></returns>
        public static RendezVousServiceProxy Instance()
        {
            // Support multithreaded applications through
            // 'Double checked locking' pattern which (once
            // the instance exists) avoids locking each
            // time the method is invoked
            if (_instance == null)
            {
                lock (syncLock)
                {
                    if (_instance == null)
                    {
                        _instance = new RendezVousServiceProxy();
                    }
                }
            }
            return _instance;
        }

        //Remarque :

        // Static members are 'eagerly initialized', that is, 
        // immediately when class is loaded for the first time.
        // .NET guarantees thread safety for static initialization
        //private static readonly LoadBalancer _instance =
        //  new LoadBalancer();

        #endregion

        #region Implémentation

        public int AjouterRendezVous(RendezVous rendezVous)
        {
            return _rendezVousService.AjouterRendezVous(rendezVous);
        }

        public List<CreneauHoraire> ObtenirCreneauxIndisponibles()
        {
            return _rendezVousService.ObtenirCreneauxIndisponibles();
        }

        public List<Masseuse> ObtenirMasseusesDisponibles(CreneauHoraire creneau)
        {
            return _rendezVousService.ObtenirMasseusesDisponibles(creneau);
        }

        public CreneauHoraire ObtenirCreneau(int id)
        {
            return _rendezVousService.ObtenirCreneau(id);
        }

        #endregion

    }
}