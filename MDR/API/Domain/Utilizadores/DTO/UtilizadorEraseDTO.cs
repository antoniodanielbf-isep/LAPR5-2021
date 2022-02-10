using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores.DTO
{
    public class UtilizadorEraseDTO
    {
        [JsonConstructor]
        public UtilizadorEraseDTO(string emailD, string nomeD)
        {
            Email = emailD;
            Nome = nomeD;
        }

        public string Email { get; set; }
        public string Nome { get; set; }
    }
}