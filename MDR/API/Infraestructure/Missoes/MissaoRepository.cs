using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Missoes;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Missoes
{
    public class MissaoRepository : BaseRepository<Missao, MissaoId>, IMissaoRepository
    {
        public MissaoRepository(DDDSample1DbContext context) : base(context.Missoes)
        {
        }

        public List<Missao> GetByEmailID(string email)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM MISSOES WHERE MISSOES.EmailUtilizador = {email}")
                    .ToList();
                
                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }

        public async Task<Missao> GetByPedidoIntroducaoID(string pedidoIntroducaoId)
        {
            return context().FromSqlInterpolated(
                $"SELECT * FROM MISSOES WHERE MISSOES.PedidoIntroducaoId = {pedidoIntroducaoId}").First();
        }

        public async Task<Missao> GetByIntroducaoID(string introducaoId)
        {
            return context().FromSqlInterpolated(
                $"SELECT * FROM MISSOES WHERE MISSOES.IntroducaoId = {introducaoId}").First();
        }
    }
}