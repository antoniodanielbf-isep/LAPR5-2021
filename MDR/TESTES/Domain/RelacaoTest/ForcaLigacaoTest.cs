using System;
using DDDNetCore.Domain.Relacoes;
using DDDSample1.Domain.Shared;
using NUnit.Framework;

namespace api.tests.RelacaoTest
{
    public class ForcaLigacaoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void introduzirValorLigacaoAlfanumerico()
        {
            Exception exception = null;
            try
            {
                var forcaLigacao = new ForcaLigacaoRelacao(14);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNull(exception);
        }

        [Test]
        public void introduzirValorLigacaoMenorQueUm()
        {
            Exception exception = null;
            try
            {
                var forcaLigacao = new ForcaLigacaoRelacao(-1);
            }
            catch (BusinessRuleValidationException ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }

        [Test]
        public void introduzirValorLigacaoMaiorQueCem()
        {
            Exception exception = null;
            try
            {
                var forcaLigacao = new ForcaLigacaoRelacao(300);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.IsNotNull(exception);
        }
    }
}