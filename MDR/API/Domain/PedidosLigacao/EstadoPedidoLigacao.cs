using System;

namespace DDDNetCore.Domain.PedidosLigacao
{
    public enum EstadoPedidoLigacao
    {
        A_AGUARDAR_RESPOSTA,
        ACEITE,
        RECUSADO
    }

    public class EstadoPedidoLigacaoOperations
    {
        public static EstadoPedidoLigacao getEstadoPedidoLigacaoById(int id)
        {
            var l = Enum.GetValues<EstadoPedidoLigacao>();

            return (EstadoPedidoLigacao) l.GetValue(id);
        }

        public static int getIDByEstadoPedidoLigacao(string estado)
        {
            var l = Enum.GetValues<EstadoPedidoLigacao>();
            var x = 0;
            foreach (var pos in l)
            {
                if (estado == pos.ToString()) return x;
                x++;
            }

            return 0;
        }

        public static int getIdPedidoLigacaoByString(string dtoEstadoPedidoLigacao)
        {
            var l = Enum.GetValues<EstadoPedidoLigacao>();
            var x = 0;
            foreach (var pos in l)
            {
                if (dtoEstadoPedidoLigacao == pos.ToString()) return x;
                x++;
            }

            return 0;
        }
    }
}