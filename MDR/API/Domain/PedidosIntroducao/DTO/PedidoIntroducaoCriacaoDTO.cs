namespace DDDNetCore.Domain.PedidosIntroducao.DTO
{
    public class PedidoIntroducaoCriacaoDTO
    {
        public PedidoIntroducaoCriacaoDTO(string descricao, string intermedio, string destino, int estado, int forca,
            string listTags)
        {
            Descricao = descricao;
            EmailIntermedio = intermedio;
            EmailDestino = destino;
            EstadoPedidoIntroducao = EstadoPedidoIntroducaoOperations.getEstadoPedidoIntroducaoById(estado).ToString();
            Forca = forca;
            Tags = listTags;
        }

        public string Descricao { get; set; }
        public string EmailIntermedio { get; set; }
        public string EmailDestino { get; set; }
        public string EstadoPedidoIntroducao { get; set; }
        public int Forca { get; set; }
        public string Tags { get; set; }
    }
}