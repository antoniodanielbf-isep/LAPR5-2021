using Newtonsoft.Json;

namespace DDDSample1.Domain.Autenticacao.DTO
{
    public class PedidoAutenticacaoDTO
    {
        [JsonConstructor]
        public PedidoAutenticacaoDTO(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}