using System.Collections.Generic;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Relacoes
{
    public interface IRelacaoRepository : IRepository<Relacao, RelacaoId>
    {
        List<Relacao> GetRelacoesFromUser(string relacaoEmail);
        List<Relacao> GetRelacoesFromUserWithUser(string email, string dtoEmailDestino);
    }
}