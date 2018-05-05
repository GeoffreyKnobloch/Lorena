using Lorena.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorena.Models.Service
{
    interface ICommandeService
    {
        int AjouterCommande(Commande commande);

        // Paramètre optionnel aTraiter => Va filtrer sur les commandes qui sont encore à traiter.

        /// <summary>
        /// Liste des commandes du client
        /// </summary>
        /// <param name="clientId"></param>
        /// <returns></returns>
        List<Commande> ObtenirCommandesDuClient(int clientId, bool aTraiter = false);

        /// <summary>
        /// Liste des commandes affectées à la masseuse (Se voit par son RDV)
        /// </summary>
        /// <param name="masseuseId"></param>
        /// <returns></returns>
        List<Commande> ObtenirCommandesDeMasseuse(int masseuseId, bool aTraiter = false);

        /// <summary>
        /// Obtenir les commandes nécéssitant un envoie.
        /// </summary>
        /// <param name="aTraiter"></param>
        /// <returns></returns>
        List<Commande> ObtenirCommandesSansMassage(bool aTraiter = false);


        void ModifierCommande(Commande commande);

        /// <summary>
        /// Passer la commande à "traitée". Doit être fait par un admin ou une masseuse lors de l'envoie ou après exécution du massage
        /// </summary>
        /// <param name="commandeId"></param>
        void TraiterCommande(int commandeId);

        /// <summary>
        /// Réouvrir une commande qui a été passée traitée par erreur.
        /// </summary>
        /// <param name="commandeId"></param>
        void OuvrirCommande(int commandeId);
        

    }
}
