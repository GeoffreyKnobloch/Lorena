using Lorena.Models.Entity.Roles;
using Lorena.Models.Service.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Lorena
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
           
        }

     
        public void Application_AuthenticateRequest(object src, EventArgs e)
        {
            if (!(HttpContext.Current.User == null))
            {
                if (HttpContext.Current.User.Identity.AuthenticationType == "Forms")
                {
                    System.Web.Security.FormsIdentity id;
                    id = (System.Web.Security.FormsIdentity)HttpContext.Current.User.Identity;

                    // Intélligence pour récupérer les rôles de l'utilisateur :
                    List<string> rolesLabel = new List<string>();

                    List<Role> roles = UtilisateurServiceProxy.Instance().ObtenirRolesUtilisateur(id.Name);

                    foreach (var role in roles)
                    {
                        rolesLabel.Add(role.Name);
                    }

                    HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(id, rolesLabel.ToArray());
                }
            }
        }
    }
}
