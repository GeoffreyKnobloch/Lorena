using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace Lorena.Models.Rules
{
    public static class ImageHelper
    {

        /// <summary>
        /// Enregistrer une image et renvoyer l'url à stocker en base pour l'afficher dans une balise img
        /// </summary>
        /// <param name="httpContext">HttpContext du controlleur appelant</param>
        /// <param name="file">Fichier posté</param>
        /// <param name="largeurOpti">Largeur optimale de l'image attendue</param>
        /// <param name="hauteurOpti">Hauteur optimale de l'image attendue</param>
        /// <param name="ratioTolereBas">Ratio Largeur/Hauteur toléré plage basse</param>
        /// <param name="ratioTolereHaut">Ratio Largeur/Hauteur toléré plage haute</param>
        /// <param name="largeurAccepteeMax">Largeur max acceptée (sinon on modifiera l'image pour l'approcher de la taille optimale attendue)</param>
        /// <param name="dossierServeur">Nom du dossier contenant l'image sur le serveur</param>
        /// <returns>URL qui permettra d'afficher l'image par la suite</returns>
        public static string EnregistrerImage(HttpContextBase httpContext, HttpPostedFileBase file, int largeurOpti, int hauteurOpti, double ratioTolereBas, double ratioTolereHaut, int largeurAccepteeMax, string dossierServeur)
        {
            string urlEnBase = string.Empty;

            if (file != null)
            {
                string nomFichier = Guid.NewGuid() + "_" + Path.GetFileName(file.FileName);

                WebImage image = new WebImage(file.InputStream);

                double ratio = (double)image.Width / (double)image.Height;

                if (ratio > ratioTolereHaut || ratio < ratioTolereBas) // Si le ratio n'est pas ok
                {
                    // Garde une trace sur le serveur que le fichier n'est pas opti (pour en parler avec le client)
                    nomFichier = "Resized_" + nomFichier;
                    // Resize :
                    image.Resize(largeurOpti, hauteurOpti, false, false);
                }
                else if (image.Width > largeurAccepteeMax)
                {
                    // Garde une trace :
                    nomFichier = "Minimized_" + nomFichier;

                    double multiplicateur = (double)largeurOpti / (double)image.Width;
                    double largeurVoulue = (double)image.Width * multiplicateur;
                    double hauteurVoulue = (double)image.Height * multiplicateur;

                    image.Resize(Convert.ToInt32(largeurVoulue), Convert.ToInt32(hauteurVoulue), false, false);


                }

                image.Save(httpContext.Server.MapPath("~/" + dossierServeur + "/") + nomFichier);

                urlEnBase = "/" + dossierServeur + "/" + nomFichier;
            }
            return urlEnBase;            
        }

    }
}