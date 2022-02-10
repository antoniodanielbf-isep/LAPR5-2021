using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Domain.Relacoes.DTO;
using IA.Domain.Utilizadores;

namespace IA.Domain.Prolog
{
    public class TrataDadosRedeSocial
    {
        public static string criaBaseConhecimentoRelacoes(List<UtilizadorDTO> responseUtilizadores,
            List<RelacaoDTO> responseRelacoes)
        {
            String baseConhecimento = "";

            foreach (var rel in responseRelacoes)
            {
                baseConhecimento += "ligacao(" + getIndexOfUser(responseUtilizadores, rel.UtilizadorOrigem) + ","
                                    + getIndexOfUser(responseUtilizadores, rel.UtilizadorDestino) + ","
                                    + rel.ForcaLigacaoOrigDest + ","
                                    + rel.ForcaLigacaoDestOrig + ").\n";
            }

            return baseConhecimento;
        }
        
        public static string criaBaseConhecimentoRelacoesSPRINTCD(List<UtilizadorDTO> responseUtilizadores,
            List<RelacaoDTO> responseRelacoes)
        {
            String baseConhecimento = "";

            foreach (var rel in responseRelacoes)
            {
                baseConhecimento += "edge(" + getIndexOfUser(responseUtilizadores, rel.UtilizadorOrigem) + ","
                                    + getIndexOfUser(responseUtilizadores, rel.UtilizadorDestino) + ","
                                    + rel.ForcaLigacaoOrigDest + ").\n";
                baseConhecimento += "edge(" + getIndexOfUser(responseUtilizadores, rel.UtilizadorDestino) + ","
                                    + getIndexOfUser(responseUtilizadores, rel.UtilizadorOrigem) + ","
                                    + rel.ForcaLigacaoDestOrig + ").\n";
                
            }

            return baseConhecimento;
        }

        public static int getIndexOfUser(List<UtilizadorDTO> lista, string email)
        {
            int num = 0;
            foreach (var VARIABLE in lista)
            {
                if (VARIABLE.Email.Equals(email))
                {
                    return num;
                }

                num++;
            }

            return -1;
        }

        public static CaminhoDTO criaCaminho(List<UtilizadorDTO> lista, string resultado)
        {
            try
            {
                
                string[] s1 = resultado.Split("D = ");
                
                string[] s2 = s1[0].Split("[");
                string[] s3 = s2[1].Split("]");
                string[] s4 = s3[0].Split(", ");

                List<String> listaCaminhos = new List<string>();
                foreach (var V in s4)
                {
                    listaCaminhos.Add(lista[int.Parse(V)].Email);
                }
                 
                return new CaminhoDTO(listaCaminhos, int.Parse(s1[1]));
            }
            catch (Exception)
            {
                throw new Exception("Nenhum caminho encontrado");
            }
        }

        public static CaminhoFLigacaoRelacaoDTO criaCaminho2(List<UtilizadorDTO> lista, string resultado,
            List<RelacaoDTO> responseRelacoes)
        {
            try
            {
               
                string[] s1 = resultado.Split("D = ");
                string[] s2 = s1[0].Split("[");
                string[] s3 = s2[1].Split("]");
                string[] s4 = s3[0].Split(", ");
            
                List<String> listaCaminhos = new List<string>();
                List<String> forcasAB = new List<string>();
                List<String> forcasBA = new List<string>();
                List<String> relacaoAB = new List<string>();
                List<String> relacaoBA = new List<string>();
                
                for (int j = 0; j < s4.Length; j++)
                {
                    if (j > 0)
                    {
                        forcasAB.Add(getRelacao(lista[int.Parse(s4[j-1])].Email, lista[int.Parse(s4[j])].Email,responseRelacoes) + "");
                        forcasBA.Add(getRelacao(lista[int.Parse(s4[j])].Email, lista[int.Parse(s4[j-1])].Email,responseRelacoes) + "");
                        relacaoAB.Add("10");
                        relacaoBA.Add("11");
                        
                    }
                    listaCaminhos.Add(lista[int.Parse(s4[j])].Email);
                }
                
                return new CaminhoFLigacaoRelacaoDTO (new CaminhoDTO(listaCaminhos, 30), forcasAB, forcasBA, relacaoAB, relacaoBA);
            }
            catch (Exception)
            {
                throw new Exception("Nenhum caminho encontrado");
            }
        }
        
        public static CaminhoEForcaLigacaoDTO criaCaminhoForcasLigacao(List<UtilizadorDTO> lista, string resultado,
            List<RelacaoDTO> responseRelacoes)
        {
            try
            {
                
                string[] s1 = resultado.Split("D = ");
                
                string[] s2 = s1[0].Split("[");
                string[] s3 = s2[1].Split("]");
                string[] s4 = s3[0].Split(", ");

                List<String> listaCaminhos = new List<string>();
                List<String> forcasAB = new List<string>();
                List<String> forcasBA = new List<string>();
                
                for (int j = 0; j < s4.Length; j++)
                {
                    if (j > 0)
                    {
                        forcasAB.Add(getRelacao(lista[int.Parse(s4[j-1])].Email, lista[int.Parse(s4[j])].Email,responseRelacoes) + "");
                        forcasBA.Add(getRelacao(lista[int.Parse(s4[j])].Email, lista[int.Parse(s4[j-1])].Email,responseRelacoes) + "");
                    }
                    listaCaminhos.Add(lista[int.Parse(s4[j])].Email);
       
                }
                 
                return new CaminhoEForcaLigacaoDTO (new CaminhoDTO(listaCaminhos, int.Parse(s1[1])),forcasAB,forcasBA);
            }
            catch (Exception)
            {
                throw new Exception("Nenhum caminho encontrado");
            }
        }

        public static string criarBaseConhecimentoUers(List<UtilizadorDTO> lista)
        {
            String baseConhecimento = "";

            foreach (var usr in lista)
            {
                baseConhecimento += "no(" + getIndexOfUser(lista, usr.Email) + " ," +
                                    "nomeX" + // não precisa do nome para o script do prolog 
                                    ",[" + usr.TagsUtilizador.ToLower() + "]).\n";
            }

            return baseConhecimento;
        }
        
        public static string criarBaseConhecimentoUersSPRINTCD(List<UtilizadorDTO> lista)
        {
            String baseConhecimento = "";

            foreach (var usr in lista)
            {
                baseConhecimento += "node(" + getIndexOfUser(lista, usr.Email) + " ,20,10).\n";
            }

            return baseConhecimento;
        }
        
        public static string criarBaseConhecimentoUersSentimento(List<UtilizadorDTO> lista)
        {
            String baseConhecimento = "";

            foreach (var usr in lista)
            {
                baseConhecimento += "noSentimentos(" + getIndexOfUser(lista, usr.Email) + ",0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5,0.5).\n";
            }

            return baseConhecimento;
        }

        public static TamanhoDTO criarTamanho(string solution, List<UtilizadorDTO> listaUsers)
        {
            try
            {
                string[] s1 = solution.Split("[");
                string[] s2 = s1[1].Split("]");
                string[] s3 = s2[0].Split(", ");
                string[] result = s3.Distinct().ToArray();

                List<string> lista = new List<string>();
                foreach (var s in result)
                {
                    lista.Add(listaUsers[int.Parse(s)].Email);
                }

                return new TamanhoDTO(lista, lista.Count);
            }
            catch (Exception)
            {
                throw new Exception("Impossível calcular tamanho");
            }
        }

        public static int getRelacao(string userA, string userB, List<RelacaoDTO> responseRelacoes)
        {
            foreach (var RELACAO in responseRelacoes)
            {
                if (RELACAO.UtilizadorOrigem.Equals(userA) && RELACAO.UtilizadorDestino.Equals(userB))
                {
                    return RELACAO.ForcaLigacaoOrigDest;
                } 
                else if (RELACAO.UtilizadorOrigem.Equals(userB) && RELACAO.UtilizadorDestino.Equals(userA))
                {
                    return RELACAO.ForcaLigacaoDestOrig;
                }
                
            }

            return 0;
        }

        public static List<string> getAmigosUser(string user, List<RelacaoDTO> relacoes)
        {
            List<string> amigos = new List<string>();
            foreach (var rel in relacoes)
            {
                if (rel.UtilizadorDestino.Equals(user))
                {
                    amigos.Add(rel.UtilizadorOrigem);
                }
                else if (rel.UtilizadorOrigem.Equals(user))
                {
                    amigos.Add(rel.UtilizadorDestino);
                }
            }

            return amigos;
        }

        public static List<string> getAmigosEmComum(string u1, string u2, List<RelacaoDTO> relacoes)
        {
            List<string> user1 = getAmigosUser(u1, relacoes);
            List<string> user2 = getAmigosUser(u2, relacoes);

            List<string> emComum = new List<string>();
            for (int i = 0; i < user1.Count; i++)
            {
                Console.Write(user1[i] + "\n");
                for (int j = 0; j < user2.Count; j++)
                {
                    if (user1[i].Equals(user2[j]))
                    {
                        emComum.Add(user1[i]);
                    }
                }
            }

            return emComum;
        }

        public static List<string> getGrupo(List<UtilizadorDTO> listaUsers, string resultado)
        {
            try
            {
                List<string> lista = new List<string>();

                string[] aux1 = resultado.Split("[");
                string[] aux2 = aux1[1].Split("]");
                string[] aux3 = aux2[0].Split(", ");

                for (int i = 0; i < aux3.Length; i++)
                {
                    lista.Add(listaUsers[int.Parse(aux3[i])].Email);
                }

                return lista;
            }
            catch (Exception)
            {
                throw new Exception("Nenhum grupo encontrado com estas características");
            }
        }
    }
}