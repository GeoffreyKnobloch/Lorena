using Lorena.Models.Entity;
using Lorena.Models.Entity.ViewModel;
using Lorena.Models.Service.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Lorena.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        /// <summary>
        /// Action de base du controller Login : écran de login
        /// Normalement l'user n'est pas authentifié, tout est vide
        /// si il est utilisateur mais va là quand même, tout est préremplis
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            UtilisateurViewModel utilisateurVM = new UtilisateurViewModel();

            utilisateurVM.EstIdentifie = HttpContext.User.Identity.IsAuthenticated;

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                utilisateurVM.Utilisateur = UtilisateurServiceProxy.Instance().ObtenirUtilisateur(HttpContext.User.Identity.Name);
            }
            return View(utilisateurVM);
        }


        [HttpPost]
        public ActionResult Index(UtilisateurViewModel utilisateurVM, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                Utilisateur utilisateur = UtilisateurServiceProxy.Instance().IdentifierUtilisateur(utilisateurVM.Utilisateur.Login, utilisateurVM.Utilisateur.MdpCripte);
                if (utilisateur != null)
                {
                    
                    FormsAuthentication.SetAuthCookie(utilisateur.Id.ToString(), true);

                    //FormsAuthentication.
                    if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return Redirect("/");
                    }
                }
                else
                {
                    ModelState.AddModelError("Utilisateur.Login", "Login et/ou mot de passe incorrect");
                }
            }
                return View(utilisateurVM);
        }

        public ActionResult CreerCompte()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreerCompte(Utilisateur utilisateur)
        {
            if (ModelState.IsValid)
            {
                

                int id = UtilisateurServiceProxy.Instance().AjouterCliente(utilisateur.Login, utilisateur.MdpCripte);
                FormsAuthentication.SetAuthCookie(id.ToString(), true);
                return Redirect("/");
            }
            return View(utilisateur);
        }

        /// <summary>
        /// Création d'un compte de masseuse, uniquement accessible par l'admin
        /// </summary>
        /// <returns></returns>
        [Authorize (Roles = "admin")]
        public ActionResult AjouterMasseuse()
        {
            Masseuse masseuse = new Masseuse();
            return View(masseuse);
        }



        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult AjouterMasseuse(Masseuse masseuse)
        {
            if (ModelState.IsValid)
            {
                int id = UtilisateurServiceProxy.Instance().AjouterMasseuse(masseuse);

                // Future implémentation : Renvoie vers la liste des masseuses du site
                return Redirect("/");
            }
            return View(masseuse);
        }

    }
}