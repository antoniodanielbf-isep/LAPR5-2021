using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Missoes
{
    public interface IMissaoRepository : IRepository<Missao, MissaoId>
    {
        Task<Missao> GetByPedidoIntroducaoID(string pedidoIntroducaoId);
        Task<Missao> GetByIntroducaoID(string IntroducaoId);
        List<Missao> GetByEmailID(string email);
    }
}