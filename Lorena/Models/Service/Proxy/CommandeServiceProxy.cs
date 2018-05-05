using Lorena.Models.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;

namespace Lorena.Models.Service.Proxy
{
    public class CommandeServiceProxy : ICommandeService
    {
        #region Singleton

        /// <summary>
        /// Singleton :
        /// </summary>
        private static readonly CommandeServiceProxy _instance = new CommandeServiceProxy();
        private CommandeService _commandeService;

        /// <summary>
        /// Protection pour appel multithread unsafe 
        /// </summary>
        private static object syncLock = new object();

        protected CommandeServiceProxy()
        {
            _commandeService = new CommandeService();
        }

        public static CommandeServiceProxy Instance()
        {
            return _instance;
        }

        #endregion

        #region Implémentation

        public int AjouterCommande(Commande commande)
        {
            return _commandeService.AjouterCommande(commande);
        }

        public List<Commande> ObtenirCommandesDuClient(int clientId, bool aTraiter = false)
        {
            return _commandeService.ObtenirCommandesDuClient(clientId, aTraiter);
        }

        public List<Commande> ObtenirCommandesDeMasseuse(int masseuseId, bool aTraiter = false)
        {
            return _commandeService.ObtenirCommandesDeMasseuse(masseuseId, aTraiter);
        }

        public List<Commande> ObtenirCommandesSansMassage(bool aTraiter = false)
        {
            return _commandeService.ObtenirCommandesSansMassage(aTraiter);
        }

        public void ModifierCommande(Commande commande)
        {
            _commandeService.ModifierCommande(commande);
        }

        public void TraiterCommande(int commandeId)
        {
            _commandeService.TraiterCommande(commandeId);
        }

        public void OuvrirCommande(int commandeId)
        {
            _commandeService.OuvrirCommande(commandeId);
        }

        #endregion

    }
}