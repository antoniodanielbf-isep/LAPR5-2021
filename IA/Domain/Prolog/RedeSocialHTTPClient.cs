using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading.Tasks;
using DDDNetCore.Domain.Relacoes.DTO;
using IA.Domain.Utilizadores;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace IA.Domain.Prolog
{
    public class RedeSocialHTTPClient
    {
        private HttpClientHandler handler;
        private HttpClient client;

        const string URL_BASE = "http://10.9.20.237:10237/api/";
        const string URL_REDE_SOCIAL = "RedeSocial";
        const string URL_REDE_UTILIZADOR = "Utilizador";
        const string URL_TODAS_RELACOES = "Relacao";
        const string URL_TODOS_UTILIZADORES = "Utilizador";


        private const int NIVEl_PESQUISA_REDE_DEFAULT = 3;

        public RedeSocialHTTPClient()
        {
            handler = new HttpClientHandler();
            client = new HttpClient(handler);
            client.BaseAddress = new Uri(URL_BASE);
            restartClient();
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
        }

        

        public async Task<List<RelacaoDTO>> getAllRelacoesViaMDR()
        {
            restartClient();
            
            var request = new HttpRequestMessage(HttpMethod.Get, URL_BASE + URL_TODAS_RELACOES);

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    return convertRelacaoStringToDto( await response.Content.ReadAsStringAsync());
                }
                catch (NotSupportedException) // When content type is not valid
                {
                    Console.WriteLine("The content type is not supported.");
                }
            }
            return null;
        }


        public async Task<List<UtilizadorDTO>> getAllUtilizadoresViaMDR()
        {
            restartClient();


            var request = new HttpRequestMessage(HttpMethod.Get, URL_BASE + URL_TODOS_UTILIZADORES);

            using var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);

            if (response.IsSuccessStatusCode)
            {
                // perhaps check some headers before deserialising

                try
                {
                    return convertUserStringToDto(await response.Content.ReadAsStringAsync());
                }
                catch (NotSupportedException) // When content type is not valid
                {
                    Console.WriteLine("The content type is not supported.");
                }
            }

            return null;
        }

        public List<UtilizadorDTO> convertUserStringToDto(String s)
        {
            string[] s1 = s.Split("{\"nomeUtilizador\":\"");

            List<UtilizadorDTO> lista = new List<UtilizadorDTO>();
            for (int i = 1; i < s1.Length; i++)
            {
                // Nome
                string[] s2 = s1[i].Split("\",\"breveDescricaoUtilizador\":\"");
                string nome = s2[0];

                // Descrição
                string[] s3 = s2[1].Split("\",\"email\":\"");
                string descricao = s3[0];

                //Email
                string[] s4 = s3[1].Split("\",\"numeroDeTelefoneUtilizador\":\"");
                string email = s4[0];

                //Número Telefone
                string[] s5 = s4[1].Split("\",\"dataDeNascimentoUtilizador\":\"");
                string numeroTelefone = s5[0];


                //Data Naciemnto
                string[] s6 = s5[1].Split("\",\"estadoEmocionalUtilizador\":");
                string dataNasciemnto = s6[0];

                //estado emocional
                string[] s7 = s6[1].Split(",\"perfilFacebookUtilizador\":\"");
                string estadoE = s7[0];

                //perfilLinkedinUtilizador
                string[] s8 = s7[1].Split("\",\"perfilLinkedinUtilizador\":\"");
                string perfilFace = s8[0];

                //perfil Linked
                string[] s9 = s8[1].Split("\",\"tagsUtilizador\":\"");
                string perfilLink = s9[0];

                //Tags
                string[] s10 = s9[1].Split("\",\"cidadePaisResidencia\":\"");
                string Tags = s10[0];

                //cidade
                string[] s11 = s10[1].Split("\",\"urlImagem\":\"");
                string cidade = s11[0];

                //imagem
                string[] s12 = s11[1].Split("\",\"dataModificacaoEstado\":\"");
                string imagem = s12[0];

                //data Modificação
                string[] s13 = s12[1].Split("\",\"passwordU\":\"");
                string dataMod = s13[0];

                //passwordU
                string[] s14 = s13[1].Split("\"}");
                string pass = s14[0];


                UtilizadorDTO dto = new UtilizadorDTO(nome, descricao, email, numeroTelefone
                    , dataNasciemnto, int.Parse(estadoE) ,
                    perfilFace, perfilLink, Tags, cidade,
                    imagem, dataMod, pass);
                lista.Add(dto);
            }

            return lista;
        }

        public List<RelacaoDTO> convertRelacaoStringToDto(String s)
        {
            string[] s1 = s.Split("{\"relacaoId\":\"");

            List<RelacaoDTO> lista = new List<RelacaoDTO>();
            for (int i = 1; i < s1.Length; i++)
            {
                // id
                string[] s2 = s1[i].Split("\",\"utilizadorOrigem\":\"");
                string id = s2[0];
                
                // A
                string[] s3 = s2[1].Split("\",\"utilizadorDestino\":\"");
                string a = s3[0];
                
                // B
                string[] s4 = s3[1].Split("\",\"forcaLigacaoOrigDest\":");
                string b = s4[0];
                
                // AB
                string[] s5 = s4[1].Split(",\"forcaLigacaoDestOrig\":");
                string ab = s5[0];
                
                // BA
                string[] s6 = s5[1].Split(",\"tagsRelacaoAB\":");
                string ba = s6[0];

                // neste momento não está a inicializar as listas de Tags da relação 
                
                RelacaoDTO dto = new RelacaoDTO(id, a, b, int.Parse(ab), 
                    int.Parse(ba), new List<string>(), new List<string>());
                lista.Add(dto);
            }
            
            
            return lista;
        }
        private void restartClient()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}