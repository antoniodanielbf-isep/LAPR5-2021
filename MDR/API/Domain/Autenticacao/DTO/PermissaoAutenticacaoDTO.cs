using Newtonsoft.Json;

namespace DDDSample1.Domain.Autenticacao.DTO
{
    public class PermissaoAutenticacaoDTO
    {
        [JsonConstructor]
        public PermissaoAutenticacaoDTO(string email)
        {
            Email = email;
        }

        public string Email { get; set; }
    }
}