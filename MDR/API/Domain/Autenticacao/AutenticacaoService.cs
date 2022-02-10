using System.Threading.Tasks;
using DDDNetCore.Domain.Utilizadores;
using DDDSample1.Domain.Autenticacao.DTO;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Autenticacao
{
    public class AutenticacaoService
    {
        private readonly IUtilizadorRepository _repoU;
        private readonly IUnitOfWork _unitOfWork;

        public AutenticacaoService(IUnitOfWork unitOfWork, IUtilizadorRepository repoU)
        {
            _unitOfWork = unitOfWork;
            _repoU = repoU;
        }

        // GET: api/Autenticacao/{email}
        public async Task<PermissaoAutenticacaoDTO> TryToLogin(PedidoAutenticacaoDTO dto)
        {
            var user = _repoU.tryToLogin(dto.Email, dto.Password);

            if (user == null) return null;

            return new PermissaoAutenticacaoDTO(dto.Email);
        }
    }
}