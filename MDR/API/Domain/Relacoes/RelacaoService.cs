using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DDDNetCore.Domain.Introducoes;
using DDDNetCore.Domain.PedidosLigacao;
using DDDNetCore.Domain.PedidosLigacao.DTO;
using DDDNetCore.Domain.Relacoes.DTO;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Domain.Relacoes
{
    public class RelacaoService
    {
        private readonly IIntroducaoRepository _repoI;
        private readonly IPedidoLigacaoRepository _repoPL;
        private readonly IRelacaoRepository _repoR;
        private readonly IUnitOfWork _unitOfWork;

        public RelacaoService(IUnitOfWork unitOfWork, IRelacaoRepository repo, IIntroducaoRepository repoIntrod,
            IPedidoLigacaoRepository repoPedidosLigacao)
        {
            _unitOfWork = unitOfWork;
            _repoR = repo;
            _repoI = repoIntrod;
            _repoPL = repoPedidosLigacao;
        }

        // GET: api/Relacao
        public async Task<List<RelacaoDTO>> GetAllAsync()
        {
            var list = await _repoR.GetAllAsync();

            var listDto = list.ConvertAll(rel =>
                rel.toDto());

            return listDto;
        }

        // GET: api/Relacao/email
        public async Task<List<RelacaoDTO>> GetAllFromUserAsync(string email)
        {
            var list = _repoR.GetRelacoesFromUser(email);

            var listDto = list.ConvertAll(rel =>
                rel.toDto());

            return listDto;
        }

        // GET: api/Relacao/{introducaoId}
        public async Task<RelacaoDTO> GetByIdAsync(string relacaoId)
        {
            var relation = await _repoR.GetByIdAsync(new RelacaoId(relacaoId));

            if (relation == null) return null;

            return relation.toDto();
        }

        // GET: api/Relacao/{email}/getTagsAllUsers
        public async Task<ActionResult<RelacaoTagsDTO>> GetTagsFromAllUsers()
        {
            var relations = await _repoR.GetAllAsync();

            var tagFinal = new StringBuilder();
            var i = 0;

            foreach (var rel in relations)
            {
                foreach (var tag in rel.TagA)
                    if (!tagFinal.ToString().Contains(tag.ToString()))
                    {
                        tagFinal = tagFinal.Append(tag);
                        tagFinal = tagFinal.Append(", ");
                    }

                foreach (var tag in rel.TagB)
                    if (!tagFinal.ToString().Contains(tag.ToString()))
                    {
                        tagFinal = tagFinal.Append(tag);
                        tagFinal = tagFinal.Append(", ");
                    }

                if (i + 1 != relations.Count) tagFinal = tagFinal.Append(", ");

                i += 1;
            }

            return new RelacaoTagsDTO(tagFinal.ToString());
        }

        // GET: api/Relacao/{email}/getTagsUser
        public async Task<ActionResult<RelacaoTagsDTO>> GetTagsFromUser(string email)
        {
            var relations = _repoR.GetRelacoesFromUser(email);

            var tagFinal = new StringBuilder();
            var i = 0;

            foreach (var rel in relations)
            {
                if (rel.UtilizadorOrigem.ToString().Equals(email))
                {
                    foreach (var tag in rel.TagA)
                        if (!tagFinal.ToString().Contains(tag.ToString()))
                            tagFinal = tagFinal.Append(tag);
                }
                else
                {
                    foreach (var tag in rel.TagB)
                        if (!tagFinal.ToString().Contains(tag.ToString()))
                            tagFinal = tagFinal.Append(tag);
                }

                if (i + 1 != relations.Count) tagFinal = tagFinal.Append(", ");

                i += 1;
            }

            return new RelacaoTagsDTO(tagFinal.ToString());
        }

        // GET: api/Relacao/{introducaoId}/creating
        public async Task<ActionResult<RelacaoDTO>> GetByIdCreationAsync(RelacaoId relaId)
        {
            await checkIfIsAValidRelacao(relaId);

            var pedI = await _repoR.GetByIdAsync(relaId);

            if (pedI == null) return null;

            return pedI.toDto();
        }

        // POST: api/Relacao/{email}/{introducaoId}/AcceptFromIntroducao
        public async Task<RelacaoDTO> AddAsyncFromIntroducao(string email, string introId, RelacaoCriacaoDTO dto)
        {
            var numberId = _repoR.GetAllAsync().Result.Count + 1;
            var idRelacao = "Relacao" + numberId;

            var introducao = await _repoI.GetByIdAsync(new IntroducaoId(introId));

            if (!introducao.UtilizadorDestino.ToString().Equals(email))
                return null;

            var listaTags = introducao.ListaIntroducao.ConvertAll(tag => tag.ToString());

            var rela = new Relacao(idRelacao, introducao.UtilizadorOrigem.ToString(),
                introducao.UtilizadorDestino.ToString(), listaTags, dto.Tags,
                introducao.ForcaLig.getForca(), dto.Forca);

            introducao.setEstadoIntroducao(EstadoIntroducao.Aceite);

            await _repoR.AddAsync(rela);

            await _unitOfWork.CommitAsync();

            return rela.toDto();
        }


        // PUT: api/Relacao/{email}/{introducaoId}/changeAllInfo
        public async Task<RelacaoDTO> UpdateAllAsync(string email, string relacaoId, RelacaoChangeDTO dto)
        {
            try
            {
                var rel = await _repoR.GetByIdAsync(new RelacaoId(relacaoId));

                if (rel == null) return null;
                // change all fields

                if (rel.UtilizadorOrigem.ToString().Equals(email))
                {
                    rel.setForcaLigacaoRelacaoOrigDest(new ForcaLigacaoRelacao(dto.Forca));

                    rel.setTagRelacaoOrigem(dto.Tags);
                }

                if (rel.UtilizadorDestino.ToString().Equals(email))
                {
                    rel.setForcaLigacaoRelacaoDestOrig(new ForcaLigacaoRelacao(dto.Forca));

                    rel.setTagRelacaoDestino(dto.Tags);
                }

                await _unitOfWork.CommitAsync();

                return rel.toDto();
            }
            catch (NullReferenceException nre)
            {
                throw new BusinessRuleValidationException(nre.ToString());
            }
        }

        // OTHER
        private async Task checkIfIsAValidRelacao(RelacaoId relaId)
        {
            var pedI = await _repoR.GetByIdAsync(relaId);
            if (pedI == null)
                throw new BusinessRuleValidationException("Pedido nao existe.");
        }


        public async Task<RelacaoDTO> AddAsyncFromPedidoLigacao(string pedidoLigacaoId,
            PedidoLigacaoAceiteDTO dto)
        {
            var pedido = await _repoPL.GetByIdAsync(new PedidoLigacaoId(pedidoLigacaoId));

            var numberId = _repoR.GetAllAsync().Result.Count + 1;
            var idRelacao = "Relacao" + numberId;

            var aux = pedido.ListaTags.ConvertAll(p => p.ToString());

            var relacao = new Relacao(idRelacao, pedido.Origem.ToString(), pedido.Destino.ToString(), aux, dto.Tags,
                pedido.Forca.ForcaPedido, dto.ForcaRelacao);

            var res = await _repoR.AddAsync(relacao);
            await _unitOfWork.CommitAsync();


            return res.toDto();
        }

        public async Task ChangeUserRel(string old, string novo)
        {
            var rels = _repoR.GetRelacoesFromUser(old);
            foreach (var rel in rels)
            {
                rel.setEmailErased(old, novo);
            }
        }
    }
}