using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Service.DAL;

namespace Lorena.Models.Service.Implementation
{
    public class CommandeService : ICommandeService
    {
        public int AjouterCommande(Commande commande)
        {
            int id;
            using (CommandeDAL dal = new CommandeDAL())
            {
                id = dal.AjouterCommande(commande);
            }
            return id;
        }

        public void ModifierCommande(Commande commande)
        {
            using (CommandeDAL dal = new CommandeDAL())
            {
                dal.ModifierCommande(commande);
            }
            
        }

        public List<Commande> ObtenirCommandesDeMasseuse(int masseuseId, bool aTraiter = false)
        {
            List<Commande> commandes;
            using (CommandeDAL dal = new CommandeDAL())
            {
                commandes = dal.ObtenirCommandesDeMasseuse(masseuseId, aTraiter);
            }
            return commandes;
        }

        public List<Commande> ObtenirCommandesDuClient(int clientId, bool aTraiter = false)
        {
            List<Commande> commandes;
            using (CommandeDAL dal = new CommandeDAL())
            {
                commandes = dal.ObtenirCommandesDuClient(clientId, aTraiter);
            }
            return commandes;
        }

        public List<Commande> ObtenirCommandesSansMassage(bool aTraiter = false)
        {
            List<Commande> commandes;
            using (CommandeDAL dal = new CommandeDAL())
            {
                commandes = dal.ObtenirCommandesSansMassage(aTraiter);
            }
            return commandes;
        }

        public void OuvrirCommande(int commandeId)
        {
            using (CommandeDAL dal = new CommandeDAL())
            {
                dal.OuvrirCommande(commandeId);
            }
        }

        public void TraiterCommande(int commandeId)
        {
            using (CommandeDAL dal = new CommandeDAL())
            {
                dal.TraiterCommande(commandeId);
            }
        }
    }
}