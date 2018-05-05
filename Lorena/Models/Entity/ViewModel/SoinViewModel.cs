using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity.ViewModel
{
    public class SoinViewModel
    {
        public Soin Soin { get; set; }

        [Display (Name = "Choisir")]
        public bool EstSelectionne { get; set; }
    }
}