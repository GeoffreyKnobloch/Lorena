using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Service.Implementation;

namespace Lorena.Models.Service.Proxy
{
    public class SoinServiceProxy : ISoinService
    {

        #region Singleton
        /// <summary>
        /// Singleton :
        /// </summary>
        private static readonly SoinServiceProxy _instance = new SoinServiceProxy();
        private SoinService _soinService;

        /// <summary>
        /// Protection pour appel multithread unsafe 
        /// </summary>
        private static object syncLock = new object();

        protected SoinServiceProxy()
        {
            _soinService = new SoinService();
        }

        public static SoinServiceProxy Instance()
        {
            return _instance;
        }

        #endregion

        public void AjouterSoin(Soin soin)
        {
            _soinService.AjouterSoin(soin);
        }

        public void ModifierSoin(Soin soin)
        {
            _soinService.ModifierSoin(soin);
        }

        public List<Soin> ObtenirSoins(bool uniquementEnVente)
        {
            return _soinService.ObtenirSoins(uniquementEnVente);
        }

        public void SupprimerSoin(int id)
        {
            _soinService.SupprimerSoin(id);
        }

        public Soin ObtenirSoin(int id)
        {
            return _soinService.ObtenirSoin(id);
        }
    }
}