using System;
using DDDNetCore.Domain.Utilizadores;
using NUnit.Framework;

namespace api.tests.Domain.Utilizador
{
    public class BreveDescricaoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void rejeitarBreveDescricaoNulaOuVazia()
        {
            Exception exception = null;
            try
            {
                var breveDescricao = new BreveDescricao("");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }

        [Test]
        public void aceitarBreveDescricaoValida()
        {
            var breveDescricao = new BreveDescricao("Descricao");

            Assert.IsNotNull(breveDescricao);
        }
    }
}