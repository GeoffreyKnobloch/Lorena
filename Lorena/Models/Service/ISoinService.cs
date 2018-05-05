using Lorena.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lorena.Models.Service
{
    interface ISoinService
    {
        List<Soin> ObtenirSoins(bool uniquementEnVente);

        Soin ObtenirSoin(int id);

        void AjouterSoin(Soin soin);

        void SupprimerSoin(int id);

        void ModifierSoin(Soin soin);

        

    }
}
