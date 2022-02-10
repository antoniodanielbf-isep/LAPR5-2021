using System;
using System.Collections.Generic;
using DDDNetCore.Domain.RedeSocial;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores;
using NUnit.Framework;

namespace api.tests.RedeSocial
{
    public class RedeSocialPerspetivaUtilizadorTest
    {
        public List<Relacao> listaRelacoes;
        public List<string> listaTotalUsers;
        public Utilizador userDaPerspetiva;

        [SetUp]
        public void Setup()
        {
            listaTotalUsers = new List<string>();
            listaRelacoes = new List<Relacao>();
            
            var utilizador1 =
                new Utilizador("user1", "1@isep.pt", "912345678", "12/12/1990", "test", "Password");
            var utilizador2 =
                new Utilizador("user2", "2@isep.pt", "912345678", "12/12/1990", "test", "Password");
            var utilizador3 =
                new Utilizador("user3", "3@isep.pt", "912345678", "12/12/1990", "test", "Password");
            var utilizador4 =
                new Utilizador("user4", "4@isep.pt", "912345678", "12/12/1990", "test", "Password");
            var utilizador5 =
                new Utilizador("user5", "5@isep.pt", "912345678", "12/12/1990", "test", "Password");
            var utilizador6 =
                new Utilizador("user6", "6@isep.pt", "912345678", "12/12/1990", "test", "Password");
            var utilizador7 =
                new Utilizador("user7", "7@isep.pt", "912345678", "12/12/1990", "test", "Password");
            var utilizador8 =
                new Utilizador("user8", "8@isep.pt", "912345678", "12/12/1990", "test", "Password");

            userDaPerspetiva = utilizador1;
            //cria lista tags
            
            var listaTags = new List<String>();
            listaTags.Add("isep");
            listaTags.Add("porto");

            var r1 = new Relacao("1", utilizador1.Id.ToString(), utilizador2.Id.ToString(), listaTags, listaTags, (3), (3) );
            var r3 = new Relacao("3",utilizador1.Id.ToString(), utilizador3.Id.ToString(), listaTags,listaTags, (3), (3) );
            var r5 = new Relacao("5",utilizador2.Id.ToString(), utilizador4.Id.ToString(), listaTags, listaTags, (3), (3));
             var r7 = new Relacao("7",utilizador4.Id.ToString(), utilizador5.Id.ToString(), listaTags, listaTags, (3), (3));
            var r9 = new Relacao("9",utilizador3.Id.ToString(), utilizador6.Id.ToString(), listaTags,listaTags, (3),  (3));
            var r11 = new Relacao("11",utilizador6.Id.ToString(), utilizador7.Id.ToString(), listaTags, listaTags, (3), (3));
            var r13 = new Relacao("13",utilizador8.Id.ToString(), utilizador4.Id.ToString(), listaTags, listaTags, (3), (3));
           

            listaTotalUsers.Add(utilizador1.Id.ToString());
            listaTotalUsers.Add(utilizador2.Id.ToString());
            listaTotalUsers.Add(utilizador3.Id.ToString());
            listaTotalUsers.Add(utilizador4.Id.ToString());
            listaTotalUsers.Add(utilizador5.Id.ToString());
            listaTotalUsers.Add(utilizador6.Id.ToString());
            listaTotalUsers.Add(utilizador7.Id.ToString());
            listaTotalUsers.Add(utilizador8.Id.ToString());
            listaRelacoes.Add(r1);
            listaRelacoes.Add(r3);
            listaRelacoes.Add(r5);
            listaRelacoes.Add(r7);
            listaRelacoes.Add(r9);
            listaRelacoes.Add(r11);
            listaRelacoes.Add(r13);
        }

         [Test]
         public void testarCriarRedeSocial()
         {
             Setup();
             
             
             //cria rede social
             var pers =
                 new RedeSocialPerspetiva(userDaPerspetiva.Id.ToString(), listaRelacoes, listaTotalUsers, 1);
             Console.Write(pers.ToString());
             // verifica que  a rede social de nível 1 contém 3 vértices
             Assert.AreEqual(pers.grafo.getNumVertex(), 3);
             // verifica que há 4 relações apenas no grafo a->b, b->a, a->c, c->a
             Assert.AreEqual(pers.grafo.getNumeroRelacoes(), 4);
             
             
        
             // aumenta nível da rede social para 2
             pers = new RedeSocialPerspetiva(userDaPerspetiva.Id.ToString(), listaRelacoes, listaTotalUsers, 2);
             Console.Write(pers.ToString());
             // verifica que  a rede social de nível 2 contém 5 vértices
             Assert.AreEqual(pers.grafo.getNumVertex(), 5);
             // verifica que há 8 relações no grafo da rede social
             Assert.AreEqual(pers.grafo.getNumeroRelacoes(), 8);
        
             // aumenta nível da rede social para 3
             pers = new RedeSocialPerspetiva(userDaPerspetiva.Id.ToString(), listaRelacoes, listaTotalUsers, 3);
        
             //rede social agora deve conter os 8 nós criados (8 utilizadores)
             Assert.AreEqual(pers.grafo.getNumVertex(), 8);
             //verifica que todas as relações estão no grafo
             Assert.AreEqual(pers.grafo.getNumeroRelacoes(), 14);
        
        
             // tentamos um nível da rede superior ao máximo de toda a rede social
             pers = new RedeSocialPerspetiva(userDaPerspetiva.Id.ToString(), listaRelacoes, listaTotalUsers, 6);
        
             //verificamos que os valores não se alteram e não dá erro
             Assert.AreEqual(pers.grafo.getNumVertex(), 8);
             Assert.AreEqual(pers.grafo.getNumeroRelacoes(), 14);
        
             pers = new RedeSocialPerspetiva(userDaPerspetiva.Id.ToString(), listaRelacoes, listaTotalUsers, 0);
             //verificamos que os valores não se alteram e não dá erro
             Assert.AreEqual(pers.grafo.getNumVertex(), 1);
             Assert.AreEqual(pers.grafo.getNumeroRelacoes(), 0);
             
             
         }
        
         [Test]
         public void testarErros()
         {
             // testa utilizador NUll
             Exception exception = null;
             try
             {
                 var pers =
                     new RedeSocialPerspetiva(null, listaRelacoes, listaTotalUsers, 1);
             }
             catch (Exception ex)
             {
                 exception = ex;
             }
        
             Assert.AreEqual("Utilizador Inválido", exception.Message);
        
             // testa lista relações vazia
             try
             {
                 var pers =
                     new RedeSocialPerspetiva(userDaPerspetiva.Id.ToString(), new List<Relacao>(), listaTotalUsers, 1);
             }
             catch (Exception ex)
             {
                 exception = ex;
             }
        
             Assert.AreEqual("Lista de Relações vazia", exception.Message);
        
             // testa lista relações vazia
             try
             {
                 var pers =
                     new RedeSocialPerspetiva(userDaPerspetiva.Id.ToString(), listaRelacoes, new List<string>(), 1);
             }
             catch (Exception ex)
             {
                 exception = ex;
             }
        
             Assert.AreEqual("Lista de Utilizadores vazia", exception.Message);
        
             // testa lista relações vazia
             try
             {
                 var pers =
                     new RedeSocialPerspetiva(userDaPerspetiva.Id.ToString(), listaRelacoes, listaTotalUsers, -1);
             }
             catch (Exception ex)
             {
                 exception = ex;
             }
        
             Assert.AreEqual("Nível Inválido", exception.Message);
         }
    }   
}