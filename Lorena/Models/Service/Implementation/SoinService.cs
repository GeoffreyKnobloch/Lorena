using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Service.DAL;

namespace Lorena.Models.Service.Implementation
{
    public class SoinService : ISoinService
    {
        public void AjouterSoin(Soin soin)
        {
            using (SoinDAL dal = new SoinDAL())
            {
                dal.AjouterSoin(soin);
            }
        }

        public void ModifierSoin(Soin soin)
        {
            using (SoinDAL dal = new SoinDAL())
            {
                dal.ModifierSoin(soin);
            }
        }

        public Soin ObtenirSoin(int id)
        {
            Soin soin;
            using (SoinDAL dal = new SoinDAL())
            {
                soin = dal.ObtenirSoin(id);
            }
            return soin;
        }

        public List<Soin> ObtenirSoins(bool uniquementEnVente)
        {
            List<Soin> soin;
            using (SoinDAL dal = new SoinDAL())
            {
               soin = dal.ObtenirSoins(uniquementEnVente);
            }
            return soin;
        }

        public void SupprimerSoin(int id)
        {
            using (SoinDAL dal = new SoinDAL())
            {

            }
        }
    }
}