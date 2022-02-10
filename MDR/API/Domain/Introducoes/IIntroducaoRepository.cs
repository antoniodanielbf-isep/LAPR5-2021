using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Introducoes
{
    public interface IIntroducaoRepository : IRepository<Introducao, IntroducaoId>
    {
        List<Introducao> getPendentesByEmail(string emailUtilizador);
        List<Introducao> getAllByEmail(string emailUtilizador);
    }
}