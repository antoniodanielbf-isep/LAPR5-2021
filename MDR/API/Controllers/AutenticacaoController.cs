using System.Threading.Tasks;
using DDDSample1.Domain.Autenticacao;
using DDDSample1.Domain.Autenticacao.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticacaoController : ControllerBase
    {
        private readonly AutenticacaoService _service;

        public AutenticacaoController(AutenticacaoService service)
        {
            _service = service;
        }

        // GET: api/Autenticacao
        [HttpPost]
        public async Task<ActionResult<PermissaoAutenticacaoDTO>> TryToLogIn(
            PedidoAutenticacaoDTO pedidoAutenticacaoDto)
        {
            var result = await _service.TryToLogin(pedidoAutenticacaoDto);

            if (result == null) return BadRequest("Email ou password Invalidos");

            return result;
        }
    }
}