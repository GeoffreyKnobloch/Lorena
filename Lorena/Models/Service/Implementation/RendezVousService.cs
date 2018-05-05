using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Service.DAL;

namespace Lorena.Models.Service.Implementation
{
    public class RendezVousService : IRendezVousService
    {
        public int AjouterRendezVous(RendezVous rendezVous)
        {
            int rdvId;
            using (RendezVousDAL dal = new RendezVousDAL())
            {
                rdvId = dal.AjouterRendezVous(rendezVous);
            }
            return rdvId;
        }

        public CreneauHoraire ObtenirCreneau(int id)
        {
            CreneauHoraire creneau;
            using (RendezVousDAL dal = new RendezVousDAL())
            {
                creneau = dal.ObtenirCreneau(id);
            }
            return creneau;
        }

        public List<CreneauHoraire> ObtenirCreneauxIndisponibles()
        {
            List<CreneauHoraire> liste;
            using (RendezVousDAL dal = new RendezVousDAL())
            {
                liste = dal.ObtenirCreneauxIndisponibles(DateTime.Now);
            }
            return liste;
        }

        public List<Masseuse> ObtenirMasseusesDisponibles(CreneauHoraire creneau)
        {
            List<Masseuse> liste;
            using (RendezVousDAL dal = new RendezVousDAL())
            {
                liste = dal.ObtenirMasseusesDisponibles(creneau);
            }
            return liste;
        }
    }
}