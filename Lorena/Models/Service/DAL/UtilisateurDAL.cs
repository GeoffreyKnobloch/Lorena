using Lorena.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Lorena.Models.Entity.Roles;
using System.Web.Mvc;

namespace Lorena.Models.Service.DAL
{
    public class UtilisateurDAL : IDalBase
    {
        /***********************Pour réinitialiser la base, exécuter ce code***********************************
            IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());
            */


        

        #region BddContext

        private BddContext bdd;

        public UtilisateurDAL()
        {
            bdd = new BddContext();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }
        #endregion

        public void AjouterCliente(Cliente cliente)
        {
            bdd.Clientes.Add(cliente);
            bdd.SaveChanges();
        }


        /// <summary>
        /// Ajout d'une masseuse par l'admin avec son rôle de masseuse
        /// </summary>
        /// <param name="masseuse"></param>
        /// <returns></returns>
        internal int AjouterMasseuse(Masseuse masseuse)
        {
            masseuse.DateCreation = DateTime.Now.Date;

            var masseuseAjoutee = bdd.Masseuses.Add(masseuse);

            bdd.SaveChanges();

            // tmp :
            bdd.Roles.Add(new Role()
            {
                Name = "masseuse",
                Description = "Employee masseuse"
            });
            bdd.SaveChanges();

            // Récupération du rôle en base :
            Role role = bdd.Roles.First(p => p.Name == "masseuse");

            // Association du rôle à l'utilisatrice masseuse :

            bdd.UtilisateurRoles.Add(new UtilisateurRole()
            {
                Utilisateur = masseuseAjoutee,
                Role = role
            });

            bdd.SaveChanges();
            return masseuseAjoutee.Id;


        }

        /// <summary>
        /// Ajout un utilisateur à partir de son login mdp crypte
        /// </summary>
        /// <param name="login"></param>
        /// <param name="motDePasseCrypte"></param>
        /// <returns></returns>
        public int AjouterCliente(string login, string motDePasseCrypte)
        {
            var cliente = bdd.Clientes.Add(new Cliente()
            {
                Login = login,
                MdpCripte = motDePasseCrypte,
                DateCreation = DateTime.Now.Date
            });

            bdd.SaveChanges();

            if (login == "admin") // à la première création d'un compte admin
            {
                Role role = bdd.Roles.Add(new Role()
                {
                    Name = "admin",
                    Description = "Administrateur du site"
                }
                );

                // Création du rôle masseuse au passage

                bdd.Roles.Add(new Role()
                {
                    Name = "masseuse",
                    Description = "Employee masseuse"
                });

                // Création du rôle cliente au passage

                bdd.Roles.Add(new Role()
                {
                    Name = "cliente",
                    Description = "Cliente du site"
                });

                bdd.SaveChanges();

                AjouterUtilisateurRole(
                    new UtilisateurRole()
                    {
                        Role = role,
                        Utilisateur = cliente

                    });
                bdd.SaveChanges();
            }
            else // Si cliente normale 
            {
                Role role = bdd.Roles.First(p => p.Name == "cliente");
                AjouterUtilisateurRole(
                    new UtilisateurRole()
                    {
                        Role = role,
                        Utilisateur = cliente
                    });
            }
            
            return cliente.Id;
        }

        /// <summary>
        /// Identifie l'utilisateur et renvoie la cliente ou la masseuse correspondante
        /// </summary>
        /// <param name="login"></param>
        /// <param name="motDePasseCrypte"></param>
        /// <returns></returns>
        public Utilisateur IdentifierUtilisateur(string login, string motDePasseCrypte)
        {
            //IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            //Database.SetInitializer(init);
            //init.InitializeDatabase(new BddContext());

            // à l'identification, on test d'abord si c'est un client :
            Utilisateur user = bdd.Clientes.FirstOrDefault(p => p.Login == login && p.MdpCripte == motDePasseCrypte);

            // Si ce n'est pas un client, on test si c'est un employé :
            if (user == null)
            {
                user = bdd.Masseuses.FirstOrDefault(p => p.Login == login && p.MdpCripte == motDePasseCrypte);
            }
            return user;
        }

        /// <summary>
        /// Obtenir un utilisateur par son id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Utilisateur ObtenirUtilisateur(int id)
        {
            //IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            //Database.SetInitializer(init);
            //init.InitializeDatabase(new BddContext());

            Utilisateur user = bdd.Clientes.FirstOrDefault(p => p.Id == id);
            if (user == null)
            {
                user = bdd.Masseuses.FirstOrDefault(p => p.Id == id);
            }
            return user;
        }

        /// <summary>
        /// Ajout d'un couple utilisateur - role
        /// Si l'utilisateur a déjà un role, il sera supprimé avant de se faire remplacer
        /// </summary>
        /// <param name="utilisateurRole"></param>
        internal void AjouterUtilisateurRole(UtilisateurRole utilisateurRole)
        {
            var entree = bdd.UtilisateurRoles.SingleOrDefault(p => p.Utilisateur.Id == utilisateurRole.Id);
            if (entree != null)
            {
                bdd.UtilisateurRoles.Remove(entree);                
            }
            bdd.UtilisateurRoles.Add(utilisateurRole);
            bdd.SaveChanges();
        }

        internal List<Role> ObtenirRolesUtilisateur(int utilisateurId)
        {
            //IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            //Database.SetInitializer(init);
            //init.InitializeDatabase(new BddContext());

            List<Role> listeRole = new List<Role>();
            foreach (var utilisateurRole in bdd.UtilisateurRoles.Where(p => p.Utilisateur.Id == utilisateurId))
            {
                listeRole.Add(bdd.Roles.First(p => p.Id == utilisateurRole.Role.Id));
            }
            return listeRole;
            //return new List<Role>();
        }
    }
}