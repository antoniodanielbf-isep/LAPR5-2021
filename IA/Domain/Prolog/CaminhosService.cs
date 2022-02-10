using System;
using IA.Domain.Utilizadores;
using Prolog;
using static DDDSample1.Domain.Util.LerFicheiros;
using static IA.Domain.Prolog.TrataDadosRedeSocial;


namespace IA.Domain.Prolog
{
    public class CaminhosService
    {
        const string CAMINHO_MAIS_CURTO = "Domain/Algoritmos/CAMINHO_MAIS_CURTO.pl";
        const string CAMINHO_MAIS_FORTE = "Domain/Algoritmos/CAMINHO_MAIS_FORTE.pl";
        const string CAMINHO_MAIS_SEGURO = "Domain/Algoritmos/CAMINHO_MAIS_SEGURO.pl";
        const string A_STAR = "Domain/Algoritmos/A_START.pl";
        const string A_STAR_MULTI_CRITERIO = "Domain/Algoritmos/A_STAR_MULTI_CRITERIO.pl";
        const string A_STAR_SENTIMENTO = "Domain/Algoritmos/A_STAR_SENTIMENTO.pl";
        const string BESTFIRST_SENTIMENTO = "Domain/Algoritmos/BEST_FIRST_EMOCOES.pl";
        


        static RedeSocialHTTPClient redeSocialHTTPClient = new RedeSocialHTTPClient();

        public CaminhoDTO CaminhoMaisCurtoPL(string userEmail1, string userEmail2)
        {
            var listaUsers = redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            if (getIndexOfUser(listaUsers, userEmail1) == -1 || getIndexOfUser(listaUsers, userEmail2) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }

            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criaBaseConhecimentoRelacoes(listaUsers, responseRelacoes);

            prolog.ConsultFromString(conhecimento);

            string ficheiro = lerFicheiro(CAMINHO_MAIS_CURTO);

            prolog.ConsultFromString(ficheiro);

            var solution =
                prolog.GetFirstSolution(
                    $"shortestPath({getIndexOfUser(listaUsers, userEmail1)},{getIndexOfUser(listaUsers, userEmail2)},L,D).");


            return criaCaminho(listaUsers, solution.ToString());
        }

        public CaminhoDTO CaminhoMaisFortePL(string userEmail1, string userEmail2)
        {
            var listaUsers = redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            if (getIndexOfUser(listaUsers, userEmail1) == -1 || getIndexOfUser(listaUsers, userEmail2) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }

            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criaBaseConhecimentoRelacoes(listaUsers, responseRelacoes);

            prolog.ConsultFromString(conhecimento);

            string ficheiro = lerFicheiro(CAMINHO_MAIS_FORTE);

            prolog.ConsultFromString(ficheiro);

            var solution =
                prolog.GetFirstSolution(
                    $"strongestPathUni({getIndexOfUser(listaUsers, userEmail1)},{getIndexOfUser(listaUsers, userEmail2)},L,D).");


            return criaCaminho(listaUsers, solution.ToString());
        }

        public CaminhoDTO CaminhoMaisSeguroPL(string userEmail1, string userEmail2, int valorMinimo)
        {
            var listaUsers = redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            if (getIndexOfUser(listaUsers, userEmail1) == -1 || getIndexOfUser(listaUsers, userEmail2) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }

            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criaBaseConhecimentoRelacoes(listaUsers, responseRelacoes);

            prolog.ConsultFromString(conhecimento);

            string ficheiro = lerFicheiro(CAMINHO_MAIS_SEGURO);

            prolog.ConsultFromString(ficheiro);

            var solution =
                prolog.GetFirstSolution(
                    $"shortestPath({getIndexOfUser(listaUsers, userEmail1)},{getIndexOfUser(listaUsers, userEmail2)},L,D,{valorMinimo}).");


            return criaCaminho(listaUsers, solution.ToString());
        }


        public CaminhoEForcaLigacaoDTO CaminhoAStar(string origem, string destino, string N)
        {
            var listaUsers = redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            if (getIndexOfUser(listaUsers, origem) == -1 || getIndexOfUser(listaUsers, destino) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }

            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criaBaseConhecimentoRelacoesSPRINTCD(listaUsers, responseRelacoes);

            prolog.ConsultFromString(conhecimento);

            string ficheiro = lerFicheiro(A_STAR);


            prolog.ConsultFromString(ficheiro);
            Console.Write(conhecimento);
            Console.Write(ficheiro);
            Console.Write($"aStar({getIndexOfUser(listaUsers, origem)},{getIndexOfUser(listaUsers, destino)},{N},D).");
            
            var solution = prolog.GetFirstSolution(
                $"aStar({getIndexOfUser(listaUsers, origem)},{getIndexOfUser(listaUsers, destino)},L,D,{N}).");
            
            
            return criaCaminhoForcasLigacao(listaUsers,solution.ToString(),responseRelacoes);
        }

        public CaminhoFLigacaoRelacaoDTO CaminhoAStarMultiCriterio(string origem, string destino, int N)
        {
            var listaUsers = redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            if (getIndexOfUser(listaUsers, origem) == -1 || getIndexOfUser(listaUsers, destino) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }

            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criaBaseConhecimentoRelacoesSPRINTCD(listaUsers, responseRelacoes);
            conhecimento += criarBaseConhecimentoUersSPRINTCD(listaUsers);
            
            prolog.ConsultFromString(conhecimento);

            string ficheiro = lerFicheiro(A_STAR_MULTI_CRITERIO);

            prolog.ConsultFromString(ficheiro);
            Console.Write(conhecimento);
            Console.Write(ficheiro);
            Console.Write($"aStar({getIndexOfUser(listaUsers, origem)},{getIndexOfUser(listaUsers, destino)},{N},D).");
            var solution =
                prolog.GetFirstSolution(
                    $"aStar({getIndexOfUser(listaUsers, origem)},{getIndexOfUser(listaUsers, destino)},L,D,{N}).");
            
            
            return criaCaminho2(listaUsers, solution.ToString(),responseRelacoes );
        }
        public CaminhoDTO CaminhoEstadosEmocionais(string origem, string destino,int N)
        {
            var listaUsers = redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            if (getIndexOfUser(listaUsers, origem) == -1 || getIndexOfUser(listaUsers, destino) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }

            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criaBaseConhecimentoRelacoesSPRINTCD(listaUsers, responseRelacoes);
            conhecimento += criarBaseConhecimentoUersSPRINTCD(listaUsers);
            conhecimento += criarBaseConhecimentoUersSentimento(listaUsers);
            prolog.ConsultFromString(conhecimento);

            string ficheiro = lerFicheiro(A_STAR_SENTIMENTO);
            
            prolog.ConsultFromString(ficheiro);

            var solution = prolog.GetFirstSolution(
                $"aStar({getIndexOfUser(listaUsers, origem)},{getIndexOfUser(listaUsers, destino)},L,D,{N}).");


            return criaCaminho(listaUsers, solution.ToString());
        }
        
        public CaminhoDTO CaminhoEstadosEmocionaisBESTFIRST(string origem, string destino,int N)
        {
            var listaUsers = redeSocialHTTPClient.getAllUtilizadoresViaMDR().Result;
            var responseRelacoes = redeSocialHTTPClient.getAllRelacoesViaMDR().Result;

            if (getIndexOfUser(listaUsers, origem) == -1 || getIndexOfUser(listaUsers, destino) == -1)
            {
                throw new Exception("Utilizadores não reconhecidos");
            }

            var prolog = new PrologEngine(persistentCommandHistory: false);

            string conhecimento = criaBaseConhecimentoRelacoesSPRINTCD(listaUsers, responseRelacoes);
            conhecimento += criarBaseConhecimentoUersSPRINTCD(listaUsers);
            conhecimento += criarBaseConhecimentoUersSentimento(listaUsers);
            prolog.ConsultFromString(conhecimento);

            string ficheiro = lerFicheiro(BESTFIRST_SENTIMENTO);
            
            prolog.ConsultFromString(ficheiro);

            var solution = prolog.GetFirstSolution(
                $"bestfs1({getIndexOfUser(listaUsers, origem)},{getIndexOfUser(listaUsers, destino)},L,D,{N}).");

            // AQUI VAI IMPRIMIR A SOLUÇÃO E VÊS O QUE O PROLOOOOGUE RETORNA
            Console.Write(solution.ToString());
            return criaCaminho(listaUsers, solution.ToString());
        }

        
    }
}

