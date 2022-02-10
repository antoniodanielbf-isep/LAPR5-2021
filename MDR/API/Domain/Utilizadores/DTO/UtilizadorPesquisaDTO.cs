using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores.DTO
{
    public class UtilizadorPesquisaDTO
    {
        [JsonConstructor]
        public UtilizadorPesquisaDTO(string emailD, string cidadePaisD, string nomeD)
        {
            if (string.IsNullOrEmpty(emailD.Trim())
                && string.IsNullOrEmpty(cidadePaisD.Trim())
                && string.IsNullOrEmpty(nomeD.Trim()))
                throw new BusinessRuleValidationException("Pelo menos um atributo requerido para a pesquisa!");

            if (string.IsNullOrEmpty(emailD.Trim()))
                Email = "";
            else
                Email = emailD;

            if (string.IsNullOrEmpty(cidadePaisD.Trim()))
                CidadePais = "";
            else
                CidadePais = cidadePaisD;

            if (string.IsNullOrEmpty(nomeD.Trim()))
                Nome = "";
            else
                Nome = nomeD;
        }

        public string Email { get; set; }
        public string CidadePais { get; set; }
        public string Nome { get; set; }
    }
}