namespace DDDNetCore.Domain.PedidosIntroducao.DTO
{
    public class PedidoIntroducaoDTO
    {
        public PedidoIntroducaoDTO(string id, string descricao, string origem, string intermedio, string destino,
            int estado, int forca, string listTags)
        {
            Id = id;
            Descricao = descricao;
            EmailOrigem = origem;
            EmailIntermedio = intermedio;
            EmailDestino = destino;
            EstadoPedidoIntroducao = EstadoPedidoIntroducaoOperations.getEstadoPedidoIntroducaoById(estado).ToString();
            Forca = forca;
            Tags = listTags;
        }

        public string Id { get; set; }
        public string Descricao { get; set; }
        public string EmailOrigem { get; set; }
        public string EmailIntermedio { get; set; }
        public string EmailDestino { get; set; }
        public string EstadoPedidoIntroducao { get; set; }
        public int Forca { get; set; }
        public string Tags { get; set; }
    }
}