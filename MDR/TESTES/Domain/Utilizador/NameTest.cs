using System;
using DDDNetCore.Domain.Utilizadores;
using NUnit.Framework;

namespace api.tests.Domain.Utilizador
{
    public class NameTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void testarRecusaDeNomeIniciadoPorEspaco()
        {
            Exception exception = null;
            try
            {
                var nome = new Nome(" rosa");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }

        [Test]
        public void testarRecusaDeNomeNuloOuVazio()
        {
            Exception exception = null;
            try
            {
                var nome = new Nome(" ");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }

        [Test]
        public void testaraceitarNomeValido()
        {
            var nome = new Nome("Rosa");

            Assert.IsNotNull(nome);
        }
    }
}