using System;
using DDDNetCore.Domain.Utilizadores;
using NUnit.Framework;

namespace api.tests.Domain.Utilizador
{
    public class TagTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void rejeitarTagNulaOuVazia()
        {
            Exception exception = null;
            try
            {
                var tag = new Tag(" ");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }

        [Test]
        public void aceitarTagValida()
        {
            var tag = new Tag("ISEP");

            Assert.IsNotNull(tag);
        }
    }
}