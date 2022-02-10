using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.PedidosLigacao;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace API.Infraestructure.PedidosLigacao
{
    public class PedidoLigacaoRepository : BaseRepository<PedidoLigacao, PedidoLigacaoId>, IPedidoLigacaoRepository
    {
        public PedidoLigacaoRepository(DDDSample1DbContext context) : base(context.PedidosLigacao)
        {
        }

        public List<PedidoLigacao> getPedidosByEmailOrigem(string emailOrigem)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM PedidosLigacao WHERE PedidosLigacao.Origem_Email = {emailOrigem}")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }

        public List<PedidoLigacao> getPedidosByEmailDestino(string emailDestino)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM PedidosLigacao WHERE PedidosLigacao.Destino_Email = {emailDestino} AND PedidosLigacao.Estado = {EstadoPedidoLigacao.A_AGUARDAR_RESPOSTA.ToString()}")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }

        public async Task<List<PedidoLigacao>> GetPedidosByEmailPending(string emailPending)
        {
            var list = context()
                .FromSqlInterpolated(
                    $"SELECT * FROM PedidosLigacao WHERE PedidosLigacao.Destino_Email = {emailPending} AND PedidosLigacao.Estado = {EstadoPedidoLigacao.A_AGUARDAR_RESPOSTA.ToString()}")
                .ToList();

            return list;
        }

        public List<PedidoLigacao> GetPedidosFromUserWithUser(string email, string destino)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM PedidosLigacao WHERE ((PedidosLigacao.Origem_Email = {email} AND PedidosLigacao.Destino_Email = {destino}) OR (PedidosLigacao.Origem_Email = {destino} AND PedidosLigacao.Destino_Email = {email}))")
                    .ToList();

                return result;
            }
            catch (NullReferenceException nre)
            {
                return null;
            }
        }
        
        public List<PedidoLigacao> getAllByEmail(string old)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM PedidosLigacao WHERE PedidosLigacao.Origem_Email = {old} OR PedidosLigacao.Destino_Email = {old}")
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