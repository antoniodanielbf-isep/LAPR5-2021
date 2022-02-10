using Newtonsoft.Json;

namespace DDDNetCore.Domain.PedidosLigacao.DTO
{
    public class PedidoLigacaoDTO
    {
        [JsonConstructor]
        public PedidoLigacaoDTO(string id, string or, string dest, int forca, string tagsL)
        {
            Id = id;
            EmailOrigem = or;
            EmailDestino = dest;
            ForcaRelacao = forca;
            Tags = tagsL;
        }

        public string Id { get; set; }
        public string EmailOrigem { get; set; }
        public string EmailDestino { get; set; }
        public int ForcaRelacao { get; set; }
        public string Tags { get; set; }
    }
}