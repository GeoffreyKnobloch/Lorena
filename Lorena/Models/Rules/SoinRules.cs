using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lorena.Models.Entity;

namespace Lorena.Models.Rules
{
    public static class SoinRules
    {
        #region Constantes

        public const int LargeurPhotoSoinOptimale = 300;
        public const int HauteurPhotoSoinOptimale = 200;
        public const double RatioTolereMin = 1.4;
        public const double RatioTolereMax = 1.63;
        public const string DossierPhoto = "ImagesSoins";
        public const int LargeurPhotoSoinMax = 700;

        #endregion Constantes


        internal static int ObtenirTempsTotal(List<Soin> choixSoins)
        {
            int total = 0;
            if (choixSoins != null)
            {
                foreach (var item in choixSoins)
                {
                    total += item.Duree;
                }
            }
            return total;
        }

        internal static double ObtenirPrixTotal(List<Soin> choixSoins)
        {
            double prix = 0;
            if (choixSoins != null)
            {
                foreach (var item in choixSoins)
                {
                    prix += item.Prix;
                }
            }
            return prix;
        }
    }
}