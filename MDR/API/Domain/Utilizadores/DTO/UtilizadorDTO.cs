using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores.DTO
{
    public class UtilizadorDTO
    {
        [JsonConstructor]
        public UtilizadorDTO(string nomeUtilizador, string breveDescricaoUtilizador, string email,
            string numeroDeTelefoneUtilizador, string dataDeNascimentoUtilizador, int estadoEmocionalUtilizador,
            string perfilFacebookUtilizador, string perfilLinkedinUtilizador, string tagsUtilizador,
            string cidadePais, string urlImagem, string password, string dataModificacao)
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
            UrlImagem = urlImagem;
            CidadePaisResidencia = cidadePais;
            DataModificacaoEstado = dataModificacao;
            PasswordU = password;
        }

        public string NomeUtilizador { get; set; }
        public string BreveDescricaoUtilizador { get; set; }
        public string Email { get; }
        public string NumeroDeTelefoneUtilizador { get; set; }
        public string DataDeNascimentoUtilizador { get; set; }
        public int EstadoEmocionalUtilizador { get; set; }
        public string PerfilFacebookUtilizador { get; set; }
        public string PerfilLinkedinUtilizador { get; set; }
        public string TagsUtilizador { get; set; }
        public string CidadePaisResidencia { get; set; }
        public string UrlImagem { get; set; }
        public string DataModificacaoEstado { get; set; }
        public string PasswordU { get; set; }
    }
}