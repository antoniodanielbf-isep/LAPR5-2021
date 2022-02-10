using System;
using System.Collections.Generic;
using DDDNetCore.Domain.RedeSocial;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores;
using NUnit.Framework;

namespace api.tests.RedeSocial

{
    public class GraphTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void adicionarUtilizadorInvalido()
        {
            // ************************************************************
            // ********* Utilizador null
            // ************************************************************

            string utilizador = "";
            var testeGrafo = new Graph();
            Exception exception = null;
            try
            {
                testeGrafo.Add(utilizador);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            Assert.AreEqual("Utilizador inválido", exception.Message);
        }

        [Test]
        public void adicionarUtilizador()
        {
            //cria utilizador
            var testeGrafo = new Graph();

            // testa grafo com 0 vértices
            Assert.AreEqual(testeGrafo.getNumVertex(), 0);

            //adiciona utilizador ao grafo
            testeGrafo.Add("user1");

            // testa grafo com 1 vértice
            Assert.AreEqual(testeGrafo.getNumVertex(), 1);

            // altera valores do utilizador e volta a adicional ao grafo
            testeGrafo.Add("user2");
            testeGrafo.Add("user3");
            testeGrafo.Add("user4");
            // testa grafo com 4 users
            Assert.AreEqual(testeGrafo.getNumVertex(), 4);

            // tenta adicionar utilizador repetido
            Exception exception = null;
            try
            {
                testeGrafo.Add("user4");
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //testa mensagem de erro
            Assert.AreEqual("Utilizador já existente", exception.Message);

            //volta a testar grafo com 4 users 
            Assert.AreEqual(testeGrafo.getNumVertex(), 4);
        }

        [Test]
        public void adicionarRelacao()
        {
            //instancia grafo
            var testeGrafo = new Graph();

            //adiciona 10 users ao grafo
            var list = new List<string>();
            for (var i = 0; i < 10; i++)
            {
                // adiciona ao grafo 
                testeGrafo.Add("user" + i + "@isep.ipp.pt");
            }

            // cria objeto Relacao

            // Cria Tags
            var tag1 = new Tag("História");
            var tag2 = new Tag("Cristiano Ronaldo");

            //cria lista tags
            var listaTags = new List<string>();
            listaTags.Add("tag1");
            listaTags.Add("tag2");

            // instancia objeto relação entre user 0 e 1 
            var r = new Relacao("123", "user0@isep.ipp.pt", "user1@isep.ipp.pt", listaTags,listaTags,3, (3));

            //     // adiciona relacao ao grafo
            testeGrafo.AddConnection(r);
            //
            //     //testa relação introduzida na matriz
            Assert.AreEqual(3, testeGrafo.getMatrixForcaRelacao()[0][1]);
            //
            //     //testa relação nula da matriz
            Assert.AreEqual(0, testeGrafo.getMatrixForcaRelacao()[0][6]);
            Assert.AreEqual(0, testeGrafo.getMatrixForcaRelacao()[4][1]);
            //
            //     //cria utilizador que não está no grafo e tenta adicionar uma relação dela ao grafo
          
            r = new Relacao("123", "user@isep.pt", "user1@isep.ipp.pt", listaTags,listaTags,3, (3));

            Exception exception = null;
            try
            {
                testeGrafo.AddConnection(r);
            }
            catch (Exception ex)
            {
                exception = ex;
            }

            //testa mensagem de erro
            Assert.AreEqual("Utilizador(es) inexistente(s)", exception.Message);
        }

        [Test]
        public void getUserAtIndex()
        {
            //cria utilizador
            var testeGrafo = new Graph();

            //adiciona utilizador ao grafo
            testeGrafo.Add("123@isep.pt");


            // testa grafo com 0 vértices
            Assert.AreEqual(testeGrafo.getUserAtIndex(0), "123@isep.pt");
        }
    }
}