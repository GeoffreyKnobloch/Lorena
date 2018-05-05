using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.ViewModel
{
    public class SuiviCommandeViewModel
    {

        public string Message { get; set; }

        public List<Commande> CommandesPassees { get; set; }

        public List<Commande> CommandesAVenir { get; set; }

    }
}