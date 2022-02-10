using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.PedidosIntroducao
{
    public interface IPedidoIntroducaoRepository : IRepository<PedidoIntroducao, PedidoIntroducaoId>
    {
        List<PedidoIntroducao> getPedidosByEmailOrigem(string emailOrigem);
        List<PedidoIntroducao> getPedidosByEmailInter(string emailIntermedio);
        List<PedidoIntroducao> GetPedidosFromUserWithUser(string email, string dtoEmailDestino);
        List<PedidoIntroducao> getAllByEmail(string old);
    }
}