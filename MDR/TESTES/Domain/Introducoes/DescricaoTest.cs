using System;
using DDDNetCore.Domain.Introducoes;
using NUnit.Framework;

namespace api.tests.Domain.Introducoes
{
    public class DescricaoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void aceitarDescricaoEntre1E1000Caracteres()
        {
            Exception aux = null;
            Descricao d = null;
            try
            {
                d = new Descricao("Descrito");
            }
            catch (Exception ex)
            {
                aux = ex;
            }

            Assert.IsNull(aux);
            Assert.IsNotNull(d);
            Assert.True("Descrito".Equals(d.ToString()));
        }

        [Test]
        public void recusarDescricaoComMenosDe1OuMaisDe1000Caracteres()
        {
            Exception aux = null;
            Descricao d = null;
            try
            {
                d = new Descricao("");
            }
            catch (Exception ex)
            {
                aux = ex;
            }

            Assert.IsNotNull(aux);
            Assert.IsNull(d);
        }
    }
}