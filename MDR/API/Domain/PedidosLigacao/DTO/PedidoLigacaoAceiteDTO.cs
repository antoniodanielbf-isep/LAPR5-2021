using Newtonsoft.Json;

namespace DDDNetCore.Domain.PedidosLigacao.DTO
{
    public class PedidoLigacaoAceiteDTO
    {
        [JsonConstructor]
        public PedidoLigacaoAceiteDTO(string dest, int forca, string tagsL)
        {
            ForcaRelacao = forca;
            Tags = tagsL;
        }

        public int ForcaRelacao { get; set; }
        public string Tags { get; set; }
    }
}