using Lorena.Models.Entity;
using Lorena.Models.Entity.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorena.Models.Service
{
    interface IUtilisateurService
    {
        void AjouterCliente(Cliente cliente);

        /// <summary>
        /// Identifie l'utilisateur à partir de son login et de son mot de passe
        /// </summary>
        /// <param name="login"></param>
        /// <param name="motDePasse">Mot de passe non crypté entré par l'utilisateur</param>
        /// <returns></returns>
        Utilisateur IdentifierUtilisateur(string login, string motDePasse);

        Utilisateur ObtenirUtilisateur(int id);

        Utilisateur ObtenirUtilisateur(string idStr);

        /// <summary>
        /// Ajoute un utilisateur à partir de son login et de son mot de passe
        /// </summary>
        /// <param name="login"></param>
        /// <param name="motDePasse">Mot de passe non crypté entré par l'utilisateur</param>
        /// <returns></returns>
        int AjouterCliente(string login, string motDePasse);

        /// <summary>
        /// Ajout d'une masseuse (par l'admin)
        /// </summary>
        /// <param name="login"></param>
        /// <param name="motDePasse"></param>
        /// <returns></returns>
        int AjouterMasseuse(Masseuse masseuse);

        /// <summary>
        /// Ajoute un rôle associé à un utilisateur : utilisateurRole
        /// </summary>
        /// <param name="utilisateurRole"></param>
        void AjouterUtilisateurRole(UtilisateurRole utilisateurRole);

        /// <summary>
        /// Obtient les rôles de l'utilisateur
        /// </summary>
        /// <param name="utilisateurId"></param>
        /// <returns></returns>
        List<Role> ObtenirRolesUtilisateur(int utilisateurId);

        /// <summary>
        /// Obtient les rôles de l'utilisateur
        /// </summary>
        /// <param name="utilisateurId"></param>
        /// <returns></returns>
        List<Role> ObtenirRolesUtilisateur(string utilisateurId);


    }
}
