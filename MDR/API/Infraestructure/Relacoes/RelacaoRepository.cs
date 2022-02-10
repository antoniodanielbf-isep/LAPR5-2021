using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Domain.Relacoes;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Relacoes
{
    public class RelacaoRepository : BaseRepository<Relacao, RelacaoId>, IRelacaoRepository
    {
        public RelacaoRepository(DDDSample1DbContext context) : base(context.Relacoes)
        {
        }

        public List<Relacao> GetRelacoesFromUser(string relacaoEmail)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM Relacoes WHERE Relacoes.UtilizadorOrigem_Email = {relacaoEmail} OR Relacoes.UtilizadorDestino_Email = {relacaoEmail}")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }

        public List<Relacao> GetRelacoesFromUserWithUser(string emailOr, string emailDest)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM Relacoes WHERE (Relacoes.UtilizadorOrigem_Email = {emailOr} AND Relacoes.UtilizadorDestino_Email = {emailDest}) OR (Relacoes.UtilizadorOrigem_Email = {emailDest} AND Relacoes.UtilizadorDestino_Email = {emailOr})")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }
    }
}