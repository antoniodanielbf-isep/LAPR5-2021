using System;
using System.Collections.Generic;
using System.Linq;
using IA.Domain.Utilizadores;
using Prolog;
using static DDDSample1.Domain.Util.LerFicheiros;
using static IA.Domain.Prolog.TrataDadosRedeSocial;

namespace IA.Domain.Prolog
{
    public class RedeSocialService
    {
        const string X_TAGS_EM_COMUM = "Domain/Algoritmos/X_TAGS_EM_COMUM.pl";
        const string TAMANHO_REDE = "Domain/Algoritmos/TAMANHO_REDE.pl";
        static RedeSocialHTTPClient redeSocialHTTPClient = new RedeSocialHTTPClient();
        const string SUGESTAO = "Domain/Algoritmos/SUGESTAO.pl";
        
        public string XTagsEmComum(int numeroTagsComum)
        {
            var listaUsers =  redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            
            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criarBaseConhecimentoUers(listaUsers);

            Console.Write(conhecimento + "\n\n\n\n\n");
            
            prolog.ConsultFromString(conhecimento);


            prolog.ConsultFromString(lerFicheiro(X_TAGS_EM_COMUM));


            var solution = prolog.GetFirstSolution($"xTagsComum({numeroTagsComum}).");

            Console.WriteLine(solution.ToString());

            return solution.ToString();
        }


        public TamanhoDTO TamanhoRede(string utilizador, int nivelMaximo)
        {
            var listaUsers =  redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            
            if (getIndexOfUser(listaUsers, utilizador) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }
            if (listaUsers == null || responseRelacoes == null)
            {
                throw new Exception("Erro de ligação à BD");
            }
            
            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criaBaseConhecimentoRelacoes(listaUsers, responseRelacoes);
            Console.Write(conhecimento);
            prolog.ConsultFromString(conhecimento);
            
            prolog.ConsultFromString(lerFicheiro(TAMANHO_REDE));
            
            var solution = prolog.GetFirstSolution($"tamanho({getIndexOfUser(listaUsers, utilizador)},{nivelMaximo},Le,ListaDeUtilizadores).");

            return criarTamanho(solution.ToString(), listaUsers);
        }

        public List<string> GrafoAmigosEmComum(string utilizador1, string utilizador2)
        {
            var listaUsers =  redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;
            if (getIndexOfUser(listaUsers, utilizador1) == -1 || getIndexOfUser(listaUsers, utilizador2) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }

            return getAmigosEmComum(utilizador1, utilizador2, responseRelacoes);
        }
        
        
        
        public int FortalezaDaRede(string user)
        {
            //var listaUsers =  redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            int somatorio = 0;
            foreach (var rel in responseRelacoes)
            {
                if (rel.UtilizadorDestino.Equals(user) || rel.UtilizadorOrigem.Equals(user))
                {
                    somatorio += rel.ForcaLigacaoDestOrig + rel.ForcaLigacaoOrigDest;
                }
            }

            return somatorio;
        }
        
        public GrupoDTO SugestaoGrupos(string user, int n, int t, string listaTags)
        {
            var listaUsers =  redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;

            string conhecimento = criarBaseConhecimentoUers(listaUsers);
            
            var prolog = new PrologEngine(persistentCommandHistory: false);
            prolog.ConsultFromString(conhecimento);

            string ficheiro = lerFicheiro(SUGESTAO);
            
            prolog.ConsultFromString(ficheiro);
            if (listaTags == null)
            {
                listaTags = "";
            }
            var solution =
                prolog.GetFirstSolution(
                    $"ttagsComum({getIndexOfUser(listaUsers, user)},{n},{t},[{listaTags}],Final).");
            
            return new GrupoDTO(getGrupo(listaUsers,solution.ToString()));
        }


    }
}