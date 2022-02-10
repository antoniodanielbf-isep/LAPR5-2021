using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores.DTO
{
    public class UtilizadorSugestoesSendDTO
    {
        [JsonConstructor]
        public UtilizadorSugestoesSendDTO(string nomeUtilizador, string email)
        {
            NomeUtilizador = nomeUtilizador;
            Email = email;
        }

        public string NomeUtilizador { get; }
        public string Email { get; }
    }
}