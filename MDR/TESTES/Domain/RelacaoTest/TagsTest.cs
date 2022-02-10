using System;
using DDDNetCore.Domain.Relacoes;
using NUnit.Framework;

namespace api.tests.RelacaoTest
{
    public class TagsTest
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
                var tags = new TagRelacao("");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }
    }
}