using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lorena.Models.Entity
{
    public class Cliente : Utilisateur
    {
        public virtual List<RendezVous> Historique { get; set; }
        
    }
}