using System;

namespace DDDNetCore.Domain.PedidosIntroducao
{
    public enum EstadoPedidoIntroducao
    {
        Recusada,
        Aceite,
        Pendente
    }

    public class EstadoPedidoIntroducaoOperations
    {
        public static EstadoPedidoIntroducao getEstadoPedidoIntroducaoById(int id)
        {
            var l = Enum.GetValues<EstadoPedidoIntroducao>();

            return (EstadoPedidoIntroducao) l.GetValue(id);
        }

        public static int getIDByEstadoPedidoIntroducao(string estado)
        {
            var l = Enum.GetValues<EstadoPedidoIntroducao>();
            var x = 0;
            foreach (var pos in l)
            {
                if (estado == pos.ToString()) return x;
                x++;
            }

            return 0;
        }
    }
}