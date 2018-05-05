using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;
using Lorena.Models.Entity.Enum;

namespace Lorena.Models.Service.DAL
{
    public class CommandeDAL : IDalBase
    {
        #region BddContext

        private BddContext bdd;
        public CommandeDAL()
        {
            bdd = new BddContext();
        }

        public void Dispose()
        {
            bdd.Dispose();
        }



        #endregion BddContext

        #region Services

        /// <summary>
        /// Ajout d'une commande. Crée le rendez-vous à la volée si il est innexistant.
        /// </summary>
        /// <param name="commande"></param>
        /// <returns></returns>
        internal int AjouterCommande(Commande commande)
        {
            // Deux cas : On crée le rendez-vous à la volée, ou alors le rendez-vous est existant.

            RendezVous rendezVous = null;

            if (commande.RendezVous != null && commande.RendezVous.Creneau != null) // Si il y a un rendezVous dans la commande :
            {
                rendezVous = bdd.RendezVous.FirstOrDefault(p => p.Id == commande.RendezVous.Id); //Soit le rendezVous existe déjà en base

                if (rendezVous == null) // Si le rendezVous n'existe pas en base, on le crée
                {
                    int rendezVousId;
                    using (RendezVousDAL rdvDAL = new RendezVousDAL())
                    {
                        rendezVousId = rdvDAL.AjouterRendezVous(commande.RendezVous);
                    }
                    // On a modifié la bdd dans une autre instance de BddContext. On revient à 0 pour avoir un context cohérent.
                    // Pas d'appel à saveChange car il a été fait dans l'autre instance.        
                    bdd.Dispose();
                    bdd = new BddContext();
                    rendezVous = bdd.RendezVous.First(p => p.Id == rendezVousId);
                }
            }

            // à ce stade, l'objet RendezVous correspond à la propriété RendezVous en base de l'objet Commande. (qu'il existe ou non) :
            commande.RendezVous = rendezVous;

            // La cliente existe forcément en base.
            commande.Cliente = bdd.Clientes.First(p => p.Id == commande.Cliente.Id);

            commande.DateCreation = DateTime.Now.Date;
            commande.EtatCommande = EnumEtatCommande.NonTraitee;


            Commande commandeEnBase = bdd.Commandes.Add(commande);
            bdd.SaveChanges();

            // Ajout des liens avec les produits une fois que la commande est ajoutée :
            if (commande.Produits != null && commande.Produits.Count > 0)
            {
                foreach (var produit in commande.Produits)
                {
                    var produitEnBase = bdd.Produits.First(p => p.Id == produit.Id);
                    bdd.CommandeProduit.Add(new CommandeProduit()
                    {
                        Commande = commandeEnBase, // l'objet commande fraichement mis en base
                        Produit = produitEnBase // l'objet Produit issu de la base
                    });
                }
                bdd.SaveChanges();
            }

            

            return commandeEnBase.Id;

        }

        internal void ModifierCommande(Commande commande)
        {
            throw new NotImplementedException("La modification de commande n'est pas implémentée.");
        }

        /// <summary>
        /// Renvoie les commandes à traiter si aTraiter = true, renvoie tout sinon.
        /// </summary>
        /// <param name="masseuseId"></param>
        /// <param name="aTraiter"></param>
        /// <returns></returns>
        internal List<Commande> ObtenirCommandesDeMasseuse(int masseuseId, bool aTraiter)
        {
            if (aTraiter)
            {
                return ObtenirCommandes().Where(p => p.RendezVous != null && p.RendezVous.Masseuse.Id == masseuseId && p.EtatCommande == EnumEtatCommande.NonTraitee).ToList();
            }
            else
            {
                return ObtenirCommandes().Where(p => p.RendezVous != null && p.RendezVous.Masseuse.Id == masseuseId).ToList();
            }
        }

        /// <summary>
        /// renvoie les commandes à traiter si aTraiter = true, renvoie tout sinon.
        /// </summary>
        /// <param name="clientId"></param>
        /// <param name="aTraiter"></param>
        /// <returns></returns>
        internal List<Commande> ObtenirCommandesDuClient(int clientId, bool aTraiter)
        {
            if (aTraiter)
            {
                return ObtenirCommandes().Where(p => p.Cliente.Id == clientId && p.EtatCommande == EnumEtatCommande.NonTraitee).ToList();
            }
            else
            {
                return ObtenirCommandes().Where(p => p.Cliente.Id == clientId).ToList();
            }
        }

        /// <summary>
        /// Passe l'état de la commande à Traitee
        /// </summary>
        /// <param name="commandeId"></param>
        internal void TraiterCommande(int commandeId)
        {
            var commande = bdd.Commandes.FirstOrDefault(p => p.Id == commandeId);
            if (commande != null)
            {
                commande.EtatCommande = EnumEtatCommande.Traitee;
                commande.DateReception = DateTime.Now.Date;
            }
            bdd.SaveChanges();
        }

        /// <summary>
        /// Réouvrir une commande
        /// </summary>
        /// <param name="commandeId"></param>
        internal void OuvrirCommande(int commandeId)
        {
            var commande = bdd.Commandes.FirstOrDefault(p => p.Id == commandeId);
            if (commande != null)
            {
                commande.EtatCommande = EnumEtatCommande.NonTraitee;
            }
            bdd.SaveChanges();
        }

        internal List<Commande> ObtenirCommandesSansMassage(bool aTraiter)
        {
            if (aTraiter)
            {
                return ObtenirCommandes().Where(p => p.RendezVous == null && p.EtatCommande == EnumEtatCommande.NonTraitee).ToList();
            }
            else
            {
                return ObtenirCommandes().Where(p => p.RendezVous == null).ToList();
            }
        }

        private List<Commande> ObtenirCommandes()
        {
            List<Commande> commandes = new List<Commande>();

            
            foreach (var lazyCommande in bdd.Commandes.Include("Cliente").Include("RendezVous").ToList())
            {
                Commande commandeValorisee = new Commande();

                commandeValorisee.DateCreation = lazyCommande.DateCreation;
                commandeValorisee.DateReception = lazyCommande.DateReception;
                commandeValorisee.Id = lazyCommande.Id;
                commandeValorisee.EtatCommande = lazyCommande.EtatCommande;

                commandeValorisee.Cliente = bdd.Clientes.First(p => p.Id == lazyCommande.Cliente.Id);

                var commandeProduits = bdd.CommandeProduit.Include("Produit").Where(p => p.Commande.Id == lazyCommande.Id);
                commandeValorisee.Produits = new List<Produit>();
                foreach (var item in commandeProduits)
                {
                    commandeValorisee.Produits.Add(item.Produit);
                }
                RendezVous rdv = new RendezVous();

                if (lazyCommande.RendezVous != null) // Si il y a un rdv dans la commande
                {
                    commandeValorisee.RendezVous = bdd.RendezVous.Include("Creneau").Include("Cliente").Include("Masseuse").FirstOrDefault(p => p.Id == lazyCommande.RendezVous.Id);


                    var rendezVousSoins = bdd.RendezVousSoin.Include("Soin").Where(p => p.RendezVous.Id == commandeValorisee.RendezVous.Id).ToList();

                    commandeValorisee.RendezVous.ChoixSoins = new List<Soin>();
                    foreach (var item in rendezVousSoins)
                    {
                        commandeValorisee.RendezVous.ChoixSoins.Add(item.Soin);
                    }
                }
          
                commandes.Add(commandeValorisee);
            }
            return commandes;
        }

        #endregion

    }
}