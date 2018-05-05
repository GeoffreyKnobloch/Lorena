using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.ViewModel
{
    public class CalendrierViewModel
    {
        public List<Soin> ChoixSoins { get; set; }

        public DateTime DateDebutCalendrier { get; set; }
    }
}