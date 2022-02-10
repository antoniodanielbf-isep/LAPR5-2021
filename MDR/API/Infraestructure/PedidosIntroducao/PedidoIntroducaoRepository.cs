using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Domain.PedidosIntroducao;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.PedidosIntroducao
{
    public class PedidoIntroducaoRepository : BaseRepository<PedidoIntroducao, PedidoIntroducaoId>,
        IPedidoIntroducaoRepository
    {
        public PedidoIntroducaoRepository(DDDSample1DbContext context) : base(context.PedidosIntroducao)
        {
        }

        public List<PedidoIntroducao> getPedidosByEmailOrigem(string emailUtilizador)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM PedidosIntroducao WHERE PedidosIntroducao.Origem_Email = {emailUtilizador}")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }

        public List<PedidoIntroducao> getPedidosByEmailInter(string emailIntermedio)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM PedidosIntroducao WHERE PedidosIntroducao.Intermedio_Email = {emailIntermedio} AND PedidosIntroducao.Estado = {EstadoPedidoIntroducao.Pendente.ToString()}")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }

        public List<PedidoIntroducao> GetPedidosFromUserWithUser(string email, string destino)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM PedidosIntroducao WHERE ((PedidosIntroducao.Origem_Email = {email} AND PedidosIntroducao.Destino_Email = {destino}) OR (PedidosIntroducao.Origem_Email = {destino} AND PedidosIntroducao.Destino_Email = {email}))")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }
        
        public List<PedidoIntroducao> getAllByEmail(string old)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM PedidosIntroducao WHERE PedidosIntroducao.Origem_Email = {old} OR PedidosIntroducao.Destino_Email = {old} OR PedidosIntroducao.Intermedio_Email = {old}")
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