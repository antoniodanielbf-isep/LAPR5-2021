using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Domain.Introducoes;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Introducoes
{
    public class IntroducaoRepository : BaseRepository<Introducao, IntroducaoId>, IIntroducaoRepository
    {
        public IntroducaoRepository(DDDSample1DbContext context) : base(context.Introducoes)
        {
        }

        public List<Introducao> getPendentesByEmail(string email)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM Introducoes WHERE Introducoes.UtilizadorDestino_Email = {email} AND Introducoes.EstadoIntroducao = {EstadoIntroducao.Pendente.ToString()}")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }
        
        public List<Introducao> getAllByEmail(string email)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM Introducoes WHERE Introducoes.UtilizadorDestino_Email = {email} OR Introducoes.UtilizadorOrigem_Email = {email} OR Introducoes.UtilizadorIntermediario_Email = {email}")
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