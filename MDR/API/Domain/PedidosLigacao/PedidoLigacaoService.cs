using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.PedidosIntroducao;
using DDDNetCore.Domain.PedidosLigacao.DTO;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Domain.PedidosLigacao
{
    public class PedidoLigacaoService
    {
        private readonly IPedidoIntroducaoRepository _repoPi;
        private readonly IPedidoLigacaoRepository _repoPl;
        private readonly IRelacaoRepository _repoR;
        private readonly IUtilizadorRepository _repoU;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoLigacaoService(IUnitOfWork unitOfWork, IPedidoLigacaoRepository repoPl, IRelacaoRepository repoR,
            IPedidoIntroducaoRepository repoPi, IUtilizadorRepository repoU)
        {
            _unitOfWork = unitOfWork;
            _repoPl = repoPl;
            _repoPi = repoPi;
            _repoU = repoU;
            _repoR = repoR;
        }

        // GET: api/PedidoLigacao/{email}/origin
        public async Task<List<PedidoLigacaoDTO>> GetByEmailOrigemAsync(string email)
        {
            var list = _repoPl.getPedidosByEmailOrigem(email);

            if (list.Count == 0) return null;

            var listDto = list.ConvertAll(pedL =>
                pedL.toDto());

            return listDto;
        }

        // GET: api/PedidoLigacao/{email}/destiny
        public async Task<List<PedidoLigacaoDTO>> GetByEmailInterAsync(string email)
        {
            var list = _repoPl.getPedidosByEmailDestino(email);

            if (list.Count == 0) return null;

            var listDto = list.ConvertAll(pedL =>
                pedL.toDto());

            return listDto;
        }

        // GET: api/PedidoLigacao/{email}/getConnectionRequest
        public async Task<List<PedidoLigacaoDTO>> GetPedidosLigacaoPendentesAsync(string email)
        {
            var list = await _repoPl.GetPedidosByEmailPending(email);

            var result = list.ConvertAll(p => p.toDto());
            return result;
        }

        //POST: api/PedidoLigacao/{email}/createConnectionRequest
        public async Task<PedidoLigacaoDTO> AddAsync(string email, PedidoLigacaoCriacaoDTO dto)
        {
            var checkUserDestiny = await _repoU.GetByIdAsync(new EmailUtilizador(dto.EmailDestino));

            if (checkUserDestiny == null) return null;

            var checkPedidosLigacao = _repoPl.GetPedidosFromUserWithUser(email, dto.EmailDestino);

            if (checkPedidosLigacao.Count > 0) return null;

            var checkPedidosIntroducao = _repoPi.GetPedidosFromUserWithUser(email, dto.EmailDestino);

            if (checkPedidosIntroducao.Count > 0) return null;

            var checkRelacoes = _repoR.GetRelacoesFromUserWithUser(email, dto.EmailDestino);

            if (checkRelacoes.Count > 0) return null;

            var pedidoId = "PedidoLiga-" + (await _repoPl.GetAllAsync()).Count;

            var pedidoLigacao = new PedidoLigacao(pedidoId, email, dto.EmailDestino,
                dto.ForcaRelacao, dto.Tags);

            var operation = _repoPl.AddAsync(pedidoLigacao);

            await _unitOfWork.CommitAsync();

            var result = operation.Result;

            return result.toDto();
        }


        public async Task<PedidoLigacaoDTO> AcceptPedidoLigacao(string pedidoLigacaoId)
        {
            var pedido = await _repoPl.GetByIdAsync(new PedidoLigacaoId(pedidoLigacaoId));

            pedido.setEstadoPedidoLigacao(EstadoPedidoLigacao.ACEITE);

            await _unitOfWork.CommitAsync();

            return pedido.toDto();
        }

        // PUT: api/Introducao/{email}/{introducaoId}/Decline
        public async Task<ActionResult<PedidoLigacaoDTO>> UpdateEstadoAsync(string email, string pedidoLigacaoId)
        {
            var pedidoLigacao = await _repoPl.GetByIdAsync(new PedidoLigacaoId(pedidoLigacaoId));

            if (!pedidoLigacao.Destino.ToString().Equals(email) ||
                pedidoLigacao.Estado != EstadoPedidoLigacao.A_AGUARDAR_RESPOSTA)
                return null;

            pedidoLigacao.setEstadoPedidoLigacao(EstadoPedidoLigacao.RECUSADO);

            await _unitOfWork.CommitAsync();

            return pedidoLigacao.toDto();
        }

        public async Task ChangeUserPedL(string old, string novo)
        {
            var pedsLs = _repoPl.getAllByEmail(old);
            foreach (var pedL in pedsLs)
            {
                pedL.setEmailErased(old, novo);
            }
        }
    }
}