using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DDDNetCore.Domain.Utilizadores.DTO
{
    public class UtilizadorSugestoesDTO
    {
        [JsonConstructor]
        public UtilizadorSugestoesDTO(string nomeUtilizador, string breveDescricaoUtilizador, string email,
            string numeroDeTelefoneUtilizador, string dataDeNascimentoUtilizador, string estadoEmocionalUtilizador,
            string perfilFacebookUtilizador, string perfilLinkedinUtilizador, string tagsUtilizador,
            string cidadePais, string urlImagem, string password, string dataModificacao,
            List<string> sugestoesDeAmizade)
        {
            NomeUtilizador = nomeUtilizador;
            BreveDescricaoUtilizador = breveDescricaoUtilizador;
            Email = email;
            NumeroDeTelefoneUtilizador = numeroDeTelefoneUtilizador;
            DataDeNascimentoUtilizador = dataDeNascimentoUtilizador;
            PerfilFacebookUtilizador = perfilFacebookUtilizador;
            PerfilLinkedinUtilizador = perfilLinkedinUtilizador;
            EstadoEmocionalUtilizador = estadoEmocionalUtilizador;
            TagsUtilizador = tagsUtilizador;
            URLImagem = urlImagem;
            CidadePaisResidencia = cidadePais;
            DataModificacaoEstado = dataModificacao;
            PasswordU = password;
            SugestoesAmizade = sugestoesDeAmizade;
        }

        public string NomeUtilizador { get; set; }
        public string BreveDescricaoUtilizador { get; set; }
        public string Email { get; }
        public string NumeroDeTelefoneUtilizador { get; set; }
        public string DataDeNascimentoUtilizador { get; set; }
        public string EstadoEmocionalUtilizador { get; set; }
        public string PerfilFacebookUtilizador { get; set; }
        public string PerfilLinkedinUtilizador { get; set; }
        public string TagsUtilizador { get; set; }
        public string CidadePaisResidencia { get; set; }
        public string URLImagem { get; set; }
        public string DataModificacaoEstado { get; set; }
        public string PasswordU { get; set; }
        public List<string> SugestoesAmizade { get; set; }
    }
}