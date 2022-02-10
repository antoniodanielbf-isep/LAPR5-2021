using System;
using DDDNetCore.Domain.Missoes;
using NUnit.Framework;

namespace api.tests.Domain.Missao
{
    public class NivelDeDificuldadeTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void aceitarNivelDeDificuldadeEntre1E10()
        {
            Exception aux = null;
            NivelDeDificuldade p = null;
            try
            {
                p = new NivelDeDificuldade(5);
            }
            catch (Exception ex)
            {
                aux = ex;
            }

            Assert.IsNull(aux);
            Assert.IsNotNull(p);
            Assert.True(p.obterDificuldade() == 5);
        }
        
        [Test]
        public void recusarNivelDeDificuldadeMenor1Maior10()
        {
            Exception aux = null;
            NivelDeDificuldade p = null;
            try
            {
                p = new NivelDeDificuldade(-1);
            }
            catch (Exception ex)
            {
                aux = ex;
            }

            Assert.IsNotNull(aux);
            Assert.IsNull(p);
        }
    }
}