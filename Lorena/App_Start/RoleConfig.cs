using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Lorena.App_Start
{
    public class RoleConfig
    {
        public static void createRoles()
        {
            Roles.CreateRole("client");
            Roles.CreateRole("membre");
            Roles.CreateRole("admin");
        }
   

    }
}