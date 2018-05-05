using Lorena.Models.Entity;
using Lorena.Models.Entity.Roles;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Lorena.Models.Service.DAL
{
    public class BddContext : DbContext
    {
        #region RendezVous

        public DbSet<RendezVous> RendezVous { get; set; }
        public DbSet<CreneauHoraire> CreneauxHoraires { get; set; }

        /// <summary>
        /// Lien entre un RendezVous et un soin, 3 enregistrement avec même id de rendezvous => le rendezvous est composé de 3 soins.
        /// </summary>
        public DbSet<RendezVousSoin> RendezVousSoin { get; set; }


        public DbSet<Commande> Commandes { get; set; }
        public DbSet<CommandeProduit> CommandeProduit { get; set; }
        // Pas besoin de créer un DbSet pour l'énuméré de l'état de la commande. Ce sera stocké en base 0, 1, 2, ... 

        #endregion RendezVous

        #region SoinProduits

        public DbSet<Soin> Soins { get; set; }
        public DbSet<Produit> Produits { get; set; }
        public DbSet<SoinProduit> SoinProduits { get; set; }

        #endregion SoinProduits

        #region Utilisateurs

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Masseuse> Masseuses { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UtilisateurRole> UtilisateurRoles { get; set; }

        #endregion Utilisateurs

    }
}