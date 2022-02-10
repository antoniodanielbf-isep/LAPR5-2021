using System;
using DDDNetCore.Domain.Utilizadores;
using NUnit.Framework;

namespace api.tests.Domain.Utilizador
{
    public class DataDeNascimentoTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void aceitarDataValida()
        {
            Exception aux = null;
            DataDeNascimento d = null;
            try
            {
                d = new DataDeNascimento("26/12/2001");
            }
            catch (Exception ex)
            {
                aux = ex;
            }

            Assert.IsNull(aux);
            Assert.IsNotNull(d);
        }

        [Test]
        public void recusarDataComFormatoInvalido1()
        {
            Exception aux = null;
            DataDeNascimento d = null;
            try
            {
                d = new DataDeNascimento("");
            }
            catch (Exception ex)
            {
                aux = ex;
            }

            Assert.IsNotNull(aux);
            Assert.IsNull(d);
        }
        [Test]
        public void aceitarDataComFormatoSimplificado()
        {
            Exception aux = null;
            DataDeNascimento d = null;
            try
            {
                d = new DataDeNascimento("4/4/2004");
            }
            catch (Exception ex)
            {
                aux = ex;
            }

            Assert.IsNull(aux);
            Assert.IsNotNull(d);
        }
        [Test]
        public void recusarDataMenor16()
        {
            Exception aux = null;
            DataDeNascimento d = null;
            try
            {
                d = new DataDeNascimento("14/11/2021");
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