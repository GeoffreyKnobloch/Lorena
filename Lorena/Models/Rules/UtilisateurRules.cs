using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Lorena.Models.Rules
{
    public static class UtilisateurRules
    {
        /// <summary>
        /// Méthode permettant d'encoder un motDePasse
        /// </summary>
        /// <param name="motDePasse">Mot de passe à cripter</param>
        /// <returns>mot de passe cripté</returns>
        /// <remarks>l'ensallage n'est pas parfait et ne devrait à priori pas apparaître dans le code source</remarks>
        public static string EncodeMD5(string motDePasse)
        {
            string motDePasseSel = "Lorena" + motDePasse + "ASP.NET MVC";
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.Default.GetBytes(motDePasseSel)));
        }
    }
}