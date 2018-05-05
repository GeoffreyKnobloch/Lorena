using Lorena.Models.Service.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Entity.Roles;

namespace Lorena.Models.Service.Proxy
{
    /// <summary>
    /// Proxy permettant l'accès au service UtilisateurService
    /// </summary>
    public class UtilisateurServiceProxy : IUtilisateurService
    {
        #region Singleton
        /// <summary>
        /// Singleton :
        /// </summary>
        private static readonly UtilisateurServiceProxy _instance = new UtilisateurServiceProxy();
        private UtilisateurService _utilisateurService;

        /// <summary>
        /// Protection pour appel multithread unsafe 
        /// </summary>
        private static object syncLock = new object();

        protected UtilisateurServiceProxy()
        {
            _utilisateurService = new UtilisateurService();
        }

        public static UtilisateurServiceProxy Instance()
        {
            return _instance;
        }

        #endregion
        
        #region Ajout

        public void AjouterCliente(Cliente cliente)
        {
            _utilisateurService.AjouterCliente(cliente);
        }

        public int AjouterCliente(string login, string motDePasse)
        {
            return _utilisateurService.AjouterCliente(login, motDePasse);
        }

        public int AjouterMasseuse(Masseuse masseuse)
        {
            return _utilisateurService.AjouterMasseuse(masseuse);
        }

        #endregion

        #region Obtention

        public Utilisateur IdentifierUtilisateur(string login, string motDePasse)
        {
            return _utilisateurService.IdentifierUtilisateur(login, motDePasse);
        }

        public Utilisateur ObtenirUtilisateur(int id)
        {
            return _utilisateurService.ObtenirUtilisateur(id);
        }

        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            return _utilisateurService.ObtenirUtilisateur(idStr);
        }

        #endregion

        #region Role

        public void AjouterUtilisateurRole(UtilisateurRole utilisateurRole)
        {
            _utilisateurService.AjouterUtilisateurRole(utilisateurRole);
        }

        public List<Role> ObtenirRolesUtilisateur(int utilisateurId)
        {
            return _utilisateurService.ObtenirRolesUtilisateur(utilisateurId);
        }

        public List<Role> ObtenirRolesUtilisateur(string utilisateurId)
        {
            return _utilisateurService.ObtenirRolesUtilisateur(utilisateurId);
        }

        #endregion

    }
}