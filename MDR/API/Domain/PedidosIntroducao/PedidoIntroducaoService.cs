using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.PedidosIntroducao.DTO;
using DDDNetCore.Domain.PedidosLigacao;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Domain.PedidosIntroducao
{
    public class PedidoIntroducaoService
    {
        private readonly IPedidoIntroducaoRepository _repoPi;
        private readonly IPedidoLigacaoRepository _repoPl;
        private readonly IRelacaoRepository _repoR;
        private readonly IUtilizadorRepository _repoU;
        private readonly IUnitOfWork _unitOfWork;

        public PedidoIntroducaoService(IUnitOfWork unitOfWork, IPedidoIntroducaoRepository repoPi,
            IPedidoLigacaoRepository repoPl, IRelacaoRepository repoR, IUtilizadorRepository repoU)
        {
            _unitOfWork = unitOfWork;
            _repoPi = repoPi;
            _repoPl = repoPl;
            _repoU = repoU;
            _repoR = repoR;
        }

        // GET: api/PedidoIntroducao/{email}/origin
        public async Task<List<PedidoIntroducaoDTO>> GetByEmailOrigemAsync(string email)
        {
            var list = _repoPi.getPedidosByEmailOrigem(email);

            if (list.Count == 0) return null;

            var listDto = list.ConvertAll(pedI =>
                pedI.toDto());

            return listDto;
        }

        // GET: api/PedidoIntroducao/{email}/intermediate
        public async Task<List<PedidoIntroducaoDTO>> GetByEmailInterAsync(string email)
        {
            var list = _repoPi.getPedidosByEmailInter(email);

            if (list.Count == 0) return null;

            var listDto = list.ConvertAll(pedI =>
                pedI.toDto());

            return listDto;
        }

        // GET: api/PedidoIntroducao/creating
        public async Task<PedidoIntroducaoDTO> GetByIdCreationAsync(PedidoIntroducaoId id)
        {
            await checkIfIsAValidPedido(id);

            var pedI = await _repoPi.GetByIdAsync(id);

            if (pedI == null) return null;

            return pedI.toDto();
        }

        // POST: api/PedidoIntroducao/{email}/makeRequest
        public async Task<PedidoIntroducaoDTO> AddAsync(string email, PedidoIntroducaoCriacaoDTO dto)
        {
            var checkUserDestiny = await _repoU.GetByIdAsync(new EmailUtilizador(dto.EmailDestino));

            if (checkUserDestiny == null) return null;

            var checkPedidosLigacao = _repoPl.GetPedidosFromUserWithUser(email, dto.EmailDestino);

            if (checkPedidosLigacao.Count > 0) return null;

            var checkPedidosIntroducao = _repoPi.GetPedidosFromUserWithUser(email, dto.EmailDestino);

            if (checkPedidosIntroducao.Count > 0) return null;

            var checkRelacoes = _repoR.GetRelacoesFromUserWithUser(email, dto.EmailDestino);

            if (checkRelacoes.Count > 0) return null;

            var checkRelacoesInter = _repoR.GetRelacoesFromUserWithUser(email, dto.EmailIntermedio);

            if (checkRelacoesInter.Count == 0) return null;

            var pedidoId = "PedidoIntro-" + (await _repoPi.GetAllAsync()).Count;

            var pedI = new PedidoIntroducao(pedidoId, dto.Descricao, email, dto.EmailIntermedio,
                dto.EmailDestino, dto.Forca, dto.Tags);

            await _repoPi.AddAsync(pedI);

            await _unitOfWork.CommitAsync();

            return pedI.toDto();
        }

        // POST: api/PedidoIntroducao/{email}/{pedidoId}/Decline
        public async Task<ActionResult<PedidoIntroducaoDTO>> UpdateEstadoAsync(string email, string pedidoId)
        {
            var pedido = await _repoPi.GetByIdAsync(new PedidoIntroducaoId(pedidoId));

            if (!pedido.Intermedio.ToString().Equals(email) ||
                pedido.Estado != EstadoPedidoIntroducao.Pendente) return null;

            pedido.setEstadoPedidoIntroducao(EstadoPedidoIntroducao.Recusada);

            await _unitOfWork.CommitAsync();

            return pedido.toDto();
        }


        // GET: api/PedidoIntroducao/obterIntermediario/{origem}/{destino}
        public async Task<IntermediarioDTO> obterIntermediario(string origem, string destino)
        {
            var relacoesOrigem = _repoR.GetRelacoesFromUser(origem);
            var relacoesDestino = _repoR.GetRelacoesFromUser(destino);
            var candidatos = new List<string>();
            var amigosC = new List<string>();

            foreach (var pos in relacoesOrigem)
                if (string.Compare(pos.UtilizadorOrigem.Email, origem, StringComparison.OrdinalIgnoreCase) != 0)
                    candidatos.Add(pos.UtilizadorOrigem.Email);
                else
                    candidatos.Add(pos.UtilizadorDestino.Email);

            foreach (var pos in relacoesDestino)
                if (string.Compare(pos.UtilizadorOrigem.Email, destino, StringComparison.OrdinalIgnoreCase) != 0)
                    amigosC.Add(pos.UtilizadorOrigem.Email);
                else
                    amigosC.Add(pos.UtilizadorDestino.Email);

            var candidatosFinal = new List<string>();
            foreach (var pos in candidatos)
                if (amigosC.Contains(pos))
                    candidatosFinal.Add(pos);


            var n = new Random();
            var x = n.Next(0, candidatosFinal.Count);

            var res = new IntermediarioDTO(candidatosFinal.ElementAt(x));

            return res;
        }

        // OTHER
        private async Task checkIfIsAValidPedido(PedidoIntroducaoId pedidoId)
        {
            var pedI = await _repoPi.GetByIdAsync(pedidoId);
            if (pedI == null)
                throw new BusinessRuleValidationException("Pedido nao existe.");
        }

        public async Task<PedidoIntroducaoDTO> Accept(string email, PedidoIntroducaoAceiteDTO dto)
        {
            var currentRequest = await _repoPi.GetByIdAsync(new PedidoIntroducaoId(dto.Id));

            currentRequest.setEstadoPedidoIntroducao(EstadoPedidoIntroducao.Aceite);
            await _unitOfWork.CommitAsync();
            return currentRequest.toDto();
        }

        public async Task ChangeUserPedI(string old, string novo)
        {
            var pedsIs = _repoPi.getAllByEmail(old);
            foreach (var pedI in pedsIs)
            {
                pedI.setEmailErased(old, novo);
            }
        }
    }
}