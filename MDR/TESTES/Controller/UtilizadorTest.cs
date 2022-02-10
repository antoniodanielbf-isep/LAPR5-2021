using System;
using System.Collections.Generic;
using DDDNetCore.Controllers;
using DDDNetCore.Domain.Utilizadores;
using DDDNetCore.Domain.Utilizadores.DTO;
using Moq;
using NUnit.Framework;

namespace api.tests.Controller
{
    public class UtilizadorTest
    {
        private UtilizadorController controller;
        private Mock<UtilizadorService> service;
        private UtilizadorDTO user;

        [SetUp]
        public void Setup()
        {
            service = new Mock<UtilizadorService>();
           // controller = new UtilizadorController(service.Object);
            user = new UtilizadorDTO("Frageiro",
                "Sou rica e poderosa",
                "fragiro@isep.ipp.pt",
                "963636927",
                "25/04/1974",
                10,
                "https://www.facebook.com/profile.php?id=100013513501470",
                "https://www.linkedin.com/in/taniachavesmkt/",
                "",
                "Vila Franca de Xira",
                "https://upload.wikimedia.org/wikipedia/commons/e/ec/RandomBitmap.png",
                "Password",
                "14/11/2021");
        }


        [Test]
        public void adicionarUtilizador()
        {
          //  Setup();
           // var v = controller.Create(user);
            //Console.Write(v.Result.Value.Email);
        }

        public void getALL()
        {
            
        }

        public void Update()
        {
            
        }
    }
}