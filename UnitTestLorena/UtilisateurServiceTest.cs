using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lorena.Models.Entity;
using Lorena.Models.Service.Proxy;
using Lorena.Models.Service.DAL;
using System.Data.Entity;

namespace UnitTestLorena
{
    [TestClass]
    public class UtilisateurServiceTest
    {
        

        [TestInitialize]
        public void Init_AvantChaqueTest()
        {
            IDatabaseInitializer<BddContext> init = new DropCreateDatabaseAlways<BddContext>();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());
        }

        [TestMethod]
        public void AjouterClienteTest()
        { 
            Cliente cliente = new Cliente()
            {
                // 
                Nom = "Knobloch",
                Prenom = "Cathy",
                DateCreation = DateTime.Now,
                Email = "knoblochcathy@gmail.com",
                Login = "knoblochcathy@gmail.com",
                MdpCripte = "mdp"
                //DateNaissance = new DateTime(1990, 01, 01)
            };
            UtilisateurServiceProxy.Instance().AjouterCliente(cliente);
            
        }
    }
}