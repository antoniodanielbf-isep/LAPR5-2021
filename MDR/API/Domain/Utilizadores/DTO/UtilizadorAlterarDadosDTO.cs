using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores.DTO
{
    public class UtilizadorAlterarDadosDTO
    {
        [JsonConstructor]
        public UtilizadorAlterarDadosDTO(string nomeUtilizador, string breveDescricaoUtilizador,
            string perfilFacebookUtilizador, string perfilLinkedinUtilizador, string tagsUtilizador,
            string cidadePais, string urlImagem, string password)
        {
            NomeUtilizador = nomeUtilizador;
            BreveDescricaoUtilizador = breveDescricaoUtilizador;
            PerfilFacebookUtilizador = perfilFacebookUtilizador;
            PerfilLinkedinUtilizador = perfilLinkedinUtilizador;
            TagsUtilizador = tagsUtilizador;
            UrlImagem = urlImagem;
            CidadePaisResidencia = cidadePais;
            PasswordU = password;
        }

        public string NomeUtilizador { get; set; }
        public string BreveDescricaoUtilizador { get; set; }
        public string PerfilFacebookUtilizador { get; set; }
        public string PerfilLinkedinUtilizador { get; set; }
        public string TagsUtilizador { get; set; }
        public string CidadePaisResidencia { get; set; }
        public string UrlImagem { get; set; }
        public string PasswordU { get; set; }
    }
}