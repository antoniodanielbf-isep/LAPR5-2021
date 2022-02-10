using Newtonsoft.Json;

namespace DDDNetCore.Domain.PedidosLigacao.DTO
{
    public class PedidoLigacaoCriacaoDTO
    {
        [JsonConstructor]
        public PedidoLigacaoCriacaoDTO(string dest, int forca, string tagsL)
        {
            EmailDestino = dest;
            ForcaRelacao = forca;
            Tags = tagsL;
        }

        public string EmailDestino { get; set; }
        public int ForcaRelacao { get; set; }
        public string Tags { get; set; }
    }
}