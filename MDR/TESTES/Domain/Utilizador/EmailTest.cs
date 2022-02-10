using System;
using DDDNetCore.Domain.Utilizadores;
using NUnit.Framework;

namespace api.tests.Domain.Utilizador
{
    public class EmailTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void testarRecusaDeEmailInvalido()
        {
            Exception exception = null;
            try
            {
                var email = new EmailUtilizador("antonio@isep@ipp.pt");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }

        [Test]
        public void aceitarMailValido()
        {
            var email = new EmailUtilizador("antoniodbfernandes@outlook.pt");
            Assert.IsNotNull(email);
            Assert.IsTrue(email.GetType() == typeof(EmailUtilizador));
        }
    }
}