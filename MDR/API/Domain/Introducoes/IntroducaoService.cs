using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Introducoes.DTO;
using DDDNetCore.Domain.PedidosIntroducao.DTO;
using DDDNetCore.Domain.Relacoes.DTO;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Domain.Introducoes
{
    public class IntroducaoService
    {
        private readonly IIntroducaoRepository _repoI;
        private readonly IUnitOfWork _unitOfWork;

        public IntroducaoService(IIntroducaoRepository repoI, IUnitOfWork unitOfWork)
        {
            _repoI = repoI;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Introducao/{email}
        public async Task<List<IntroducaoDTO>> GetAllAsync(string emailUtilizador)
        {
            var list = _repoI.getPendentesByEmail(emailUtilizador);
            if (list.Count == 0)
                return null;

            var listOut = new List<IntroducaoDTO>();

            foreach (var intro in list) listOut.Add(intro.toDto());

            return listOut;
        }

        // GET:  api/Introducao/{pedIId}/creating
        public async Task<ActionResult<IntroducaoDTO>> GetByIdCreationAsync(IntroducaoId introducaoId)
        {
            await checkIfIsAValidIntroducao(introducaoId);

            var pedI = await _repoI.GetByIdAsync(introducaoId);

            if (pedI == null) return null;

            return pedI.toDto();
        }


        //POST : adicionar Introducao
        public async Task<IntroducaoDTO> AddAsync(string email, PedidoIntroducaoAceiteDTO dto)
        {
            var introducaoId = "Intro-" + (await _repoI.GetAllAsync()).Count;

            var introducao = new Introducao(introducaoId, dto.DescricaoIntroducao,
                dto.EmailOrigem, dto.EmailIntermedio, dto.EmailDestino, dto.Forca,
                dto.Tags);

            var intro = await _repoI.AddAsync(introducao);

            await _unitOfWork.CommitAsync();

            return intro.toDto();
        }

        // POST:  api/Introducao/{email}/{introID}/Accept
        public async Task<IntroducaoDTO> AcceptAsync(string email, string introID, RelacaoCriacaoDTO dto)
        {
            var intro = await _repoI.GetByIdAsync(new IntroducaoId(introID));

            intro.setEstadoIntroducao(EstadoIntroducao.Aceite);
            await _unitOfWork.CommitAsync();
            return intro.toDto();
        }

        // PUT: api/Introducao/{email}/{introducaoId}/Decline
        public async Task<ActionResult<IntroducaoDTO>> UpdateEstadoAsync(string email, string introducaoId)
        {
            var intro = await _repoI.GetByIdAsync(new IntroducaoId(introducaoId));

            if (!intro.UtilizadorDestino.ToString().Equals(email) ||
                intro.EstadoIntroducao != EstadoIntroducao.Pendente)
                return null;

            intro.setEstadoIntroducao(EstadoIntroducao.Recusada);

            await _unitOfWork.CommitAsync();

            return intro.toDto();
        }

        // OTHER
        private async Task checkIfIsAValidIntroducao(IntroducaoId introducaoId)
        {
            var pedI = await _repoI.GetByIdAsync(introducaoId);
            if (pedI == null)
                throw new BusinessRuleValidationException("Pedido nao existe.");
        }

        public async Task ChangeUserIntro(string old, string novo)
        {
            var intros = _repoI.getAllByEmail(old);
            foreach (var intro in intros)
            {
                intro.setEmailErased(old, novo);
            }
        }
    }
}