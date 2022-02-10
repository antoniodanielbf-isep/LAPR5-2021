using System.Threading.Tasks;
using DDDNetCore.Domain.Introducoes;
using DDDNetCore.Domain.PedidosIntroducao;
using DDDNetCore.Domain.PedidosIntroducao.DTO;
using DDDNetCore.Domain.Utilizadores;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Missoes
{
    public class MissaoService
    {
        private readonly IUnitOfWork _iUnitOfWork;
        private readonly IMissaoRepository _repoM;

        public MissaoService(IMissaoRepository repoM, IUnitOfWork iUnitOfWork)
        {
            _repoM = repoM;
            _iUnitOfWork = iUnitOfWork;
        }

        public async Task<MissaoDTO> addAssync(string email, PedidoIntroducaoDTO pedI)
        {
            var idMissao = "MISSAO-" + _repoM.GetAllAsync().Result.Count;

            var missao = new Missao(idMissao, new NivelDeDificuldade(5), EstadoMissao.EmProgresso);
            missao.setUtilizadorID(new EmailUtilizador(pedI.EmailOrigem));
            missao.setPedidoIntroducaoID(new PedidoIntroducaoId(pedI.Id));

            var result = await _repoM.AddAsync(missao);

            await _iUnitOfWork.CommitAsync();
            return result.toDto();
        }

        public async Task<MissaoDTO> AceitarPedidoIntroducao(string email, string idPedidoIntro, string introducaoID)
        {
            var missao = await _repoM.GetByPedidoIntroducaoID(idPedidoIntro);
            missao.PontuacaoAceitarPedido();
            missao.setIntroducaoID(new IntroducaoId(introducaoID));
            await _iUnitOfWork.CommitAsync();
            return missao.toDto();
        }

        public async Task<MissaoDTO> RecusarPedidoIntroducao(string email, string pedidoId)
        {
            var missao = await _repoM.GetByPedidoIntroducaoID(pedidoId);
            missao.PontuacaoRecusarPedido();
            missao.setEstadoMissao(EstadoMissao.Falhada);

            await _iUnitOfWork.CommitAsync();
            return missao.toDto();
        }

        public async Task<MissaoDTO> AceitarIntroducao(string email, string introId)
        {
            var missao = await _repoM.GetByIntroducaoID(introId);
            missao.PontuacaoAceitarIntroducao();
            missao.setEstadoMissao(EstadoMissao.Sucesso);

            await _iUnitOfWork.CommitAsync();
            return missao.toDto();
        }

        public async Task<MissaoDTO> RecusarIntroducao(string email, string introducaoId)
        {
            var missao = await _repoM.GetByIntroducaoID(introducaoId);
            missao.PontuacaoRecusarIntroducao();
            missao.setEstadoMissao(EstadoMissao.Falhada);

            await _iUnitOfWork.CommitAsync();
            return missao.toDto();
        }

        public async Task ChangeUserMissions(string email, string usrEmail)
        {
            var missoes = _repoM.GetByEmailID(email);
            if (missoes.Count > 0)
            {
                foreach (var missao in missoes)
                {
                    if (missao.EstadoMissao.Equals(EstadoMissao.EmProgresso))
                    {
                        missao.setEstadoMissao(EstadoMissao.Falhada);
                    }
                    missao.setUtilizadorID(new EmailUtilizador(usrEmail));
                    await _iUnitOfWork.CommitAsync();
                }
            }
        }
    }
}