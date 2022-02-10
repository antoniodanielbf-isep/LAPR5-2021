namespace DDDNetCore.Domain.PedidosIntroducao.DTO
{
    public class PedidoIntroducaoAceiteDTO
    {
        public PedidoIntroducaoAceiteDTO(string id, string descricaoPedido, string origem, string intermedio,
            string destino,
            int estado, int forca, string listTags, string descricaoIntroducao)
        {
            Id = id;
            DescricaoPedido = descricaoPedido;
            EmailOrigem = origem;
            EmailIntermedio = intermedio;
            EmailDestino = destino;
            EstadoPedidoIntroducao = EstadoPedidoIntroducaoOperations.getEstadoPedidoIntroducaoById(estado).ToString();
            Forca = forca;
            Tags = listTags;
            DescricaoIntroducao = descricaoIntroducao;
        }

        public string Id { get; set; }
        public string DescricaoPedido { get; set; }
        public string EmailOrigem { get; set; }
        public string EmailIntermedio { get; set; }
        public string EmailDestino { get; set; }
        public string EstadoPedidoIntroducao { get; set; }
        public int Forca { get; set; }
        public string Tags { get; set; }
        public string DescricaoIntroducao { get; set; }
    }
}