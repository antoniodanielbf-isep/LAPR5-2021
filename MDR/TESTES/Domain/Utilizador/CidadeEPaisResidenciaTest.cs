using System;
using DDDNetCore.Domain.Utilizadores;
using NUnit.Framework;

namespace api.tests.Domain.Utilizador
{
    public class CidadeEPaisResidenciaTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void testarRecusaCidadeEPaisResidenciaNuloOuVazio()
        {
            Exception exception = null;
            try
            {
                var cidadeEPaisResidencia = new CidadeEPaisResidencia(" ");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }

        [Test]
        public void aceitarCidadeEPaisResidenciaValido()
        {
            var cidadeEPaisResidencia = new CidadeEPaisResidencia("Porto, Portugal");

            Assert.IsNotNull(cidadeEPaisResidencia);
        }
    }
}