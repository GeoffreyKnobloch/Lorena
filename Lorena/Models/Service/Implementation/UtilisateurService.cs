using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Service.DAL;
using Lorena.Models.Rules;
using Lorena.Models.Entity.Roles;
using System.Security.Claims;

namespace Lorena.Models.Service.Implementation
{
    public class UtilisateurService : IUtilisateurService
    {
        /// <summary>
        /// Méthode temporaire à supprimer 
        /// </summary>
        /// <param name="cliente"></param>
        public void AjouterCliente(Cliente cliente)
        {
            using (UtilisateurDAL dal = new UtilisateurDAL())
            {
                dal.AjouterCliente(cliente);
            }
        }

        /// <summary>
        /// Ajout d'une masseuse : Réservé à l'admin
        /// </summary>
        /// <param name="masseuse"></param>
        /// <returns></returns>
        public int AjouterMasseuse(Masseuse masseuse)
        {
            int id;
            // Encription du mdp :
            masseuse.MdpCripte = UtilisateurRules.EncodeMD5(masseuse.MdpCripte);
            using (UtilisateurDAL dal = new UtilisateurDAL())
            {
                id = dal.AjouterMasseuse(masseuse);
            }
            return id;
        }

        /// <summary>
        /// Ajout d'une cliente à partir de son login et de son mot de passe non crypté
        /// </summary>
        /// <param name="login"></param>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        public int AjouterCliente(string login, string motDePasse)
        {
            int idUtilisateur = -1;
            string motDePasseCrypte = UtilisateurRules.EncodeMD5(motDePasse);
            using (UtilisateurDAL dal = new UtilisateurDAL())
            {
                idUtilisateur = dal.AjouterCliente(login, motDePasseCrypte);
            }

            return idUtilisateur;
        }

        /// <summary>
        /// Identification d'un utilisateur à partir de son login et de son mot de passe non crypté
        /// </summary>
        /// <param name="login"></param>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        public Utilisateur IdentifierUtilisateur(string login, string motDePasse)
        {
            Utilisateur utilisateur;
            string motDePasseCrypte = UtilisateurRules.EncodeMD5(motDePasse);
            using (UtilisateurDAL dal = new UtilisateurDAL())
            {
                utilisateur = dal.IdentifierUtilisateur(login, motDePasseCrypte);
            }

            return utilisateur;
        }

        /// <summary>
        /// Obtention d'un utilisateur à partir de son Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Utilisateur ObtenirUtilisateur(int id)
        {
            Utilisateur utilisateur;

            using (UtilisateurDAL dal = new UtilisateurDAL())
            {
                utilisateur = dal.ObtenirUtilisateur(id);
            }

            return utilisateur;
        }

        public Utilisateur ObtenirUtilisateur(string idStr)
        {
            return ObtenirUtilisateur(int.Parse(idStr));
        }

        public void AjouterUtilisateurRole(UtilisateurRole utilisateurRole)
        {
            using (UtilisateurDAL dal = new UtilisateurDAL())
            {
                dal.AjouterUtilisateurRole(utilisateurRole);
            }
        }

        public List<Role> ObtenirRolesUtilisateur(int utilisateurId)
        {
            List<Role> roleUtilisateur;
            using (UtilisateurDAL dal = new UtilisateurDAL())
            {
                roleUtilisateur = dal.ObtenirRolesUtilisateur(utilisateurId);
            }
            return roleUtilisateur;
        }

        public List<Role> ObtenirRolesUtilisateur(string utilisateurId)
        {
            return this.ObtenirRolesUtilisateur(int.Parse(utilisateurId));
        }
    }
}