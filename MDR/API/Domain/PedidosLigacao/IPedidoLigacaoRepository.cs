using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.PedidosLigacao
{
    public interface IPedidoLigacaoRepository : IRepository<PedidoLigacao, PedidoLigacaoId>
    {
        List<PedidoLigacao> getPedidosByEmailOrigem(string emailOrigem);
        List<PedidoLigacao> getPedidosByEmailDestino(string emailDestino);
        Task<List<PedidoLigacao>> GetPedidosByEmailPending(string emailPending);
        List<PedidoLigacao> GetPedidosFromUserWithUser(string email, string dtoEmailDestino);
        List<PedidoLigacao> getAllByEmail(string old);
    }
}