using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Introducoes;
using DDDNetCore.Domain.Introducoes.DTO;
using DDDNetCore.Domain.Missoes;
using DDDNetCore.Domain.PedidosIntroducao;
using DDDNetCore.Domain.PedidosIntroducao.DTO;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoIntroducaoController : ControllerBase
    {
        private readonly IntroducaoService _serviceInt;
        private readonly MissaoService _serviceMi;
        private readonly PedidoIntroducaoService _servicePi;

        public PedidoIntroducaoController(PedidoIntroducaoService serviceP, IntroducaoService serviceI,
            MissaoService serviceMi)
        {
            _servicePi = serviceP;
            _serviceInt = serviceI;
            _serviceMi = serviceMi;
        }

        // Obter todos os pedidos de introducao de um utilizador origem
        // GET: api/PedidoIntroducao/{email}/origin
        [HttpGet("{email}/origin")]
        public async Task<ActionResult<List<PedidoIntroducaoDTO>>> GetPedidosByEmailOrigem(string email)
        {
            var pedI = await _servicePi.GetByEmailOrigemAsync(email);

            if (pedI == null) return NotFound();

            return pedI;
        }

        // Obter Sugestão de Intermediário de Pedido de Introdução
        // GET: api/PedidoIntroducao/obterIntermediario/{origem}/{destino}
        [HttpGet("obterIntermediario/{origem}/{destino}")]
        public async Task<ActionResult<IntermediarioDTO>> GetPedidosByEmailOrigem(string origem, string destino)
        {
            var res = await _servicePi.obterIntermediario(origem, destino);

            if (res == null) return NotFound();

            return res;
        }

        // Obter todos os pedidos de introducao de um utilizador intermedio
        // GET: api/PedidoIntroducao/{email}/intermediate
        [HttpGet("{email}/intermediate")]
        public async Task<ActionResult<List<PedidoIntroducaoDTO>>> GetPedidosByEmailIntermedio(string email)
        {
            var pedI = await _servicePi.GetByEmailInterAsync(email);

            if (pedI == null) return NotFound();

            return pedI;
        }

        // Auxiliar para criar um pedido de introducao
        // GET: api/PedidoIntroducao/{pedIId}/creating
        [HttpGet("{pedIId}/creating")]
        public async Task<ActionResult<PedidoIntroducaoDTO>> GetByCreatingId(string pedIId)
        {
            var pedI = await _servicePi.GetByIdCreationAsync(new PedidoIntroducaoId(pedIId));

            if (pedI == null) return NotFound();

            return pedI;
        }

        // Criar um pedido de introducao atraves de um DTO
        // POST: api/PedidoIntroducao/{email}/makeRequest
        [HttpPost("{email}/makeRequest")]
        public async Task<ActionResult<PedidoIntroducaoDTO>> Create(string email, PedidoIntroducaoCriacaoDTO dto)
        {
            try
            {
                var pedI = await _servicePi.AddAsync(email, dto);
                var wait = await _serviceMi.addAssync(email, pedI);

                return GetByCreatingId(pedI.Id).Result;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        // Aceitar um pedido de introducao atraves de um DTO
        // POST: api/PedidoIntroducao/{email}/acceptRequest
        [HttpPost("{email}/acceptRequest")]
        public async Task<ActionResult<IntroducaoDTO>> Accept(string email, PedidoIntroducaoAceiteDTO dto)
        {
            try
            {
                var wait = await _servicePi.Accept(email, dto);
                if (wait == null) return BadRequest("Erro ao Aceitar Pedido Introdução");
                var res = await _serviceInt.AddAsync(email, dto);
                var wait2 = await _serviceMi.AceitarPedidoIntroducao(email, dto.Id, res.IntroducaoId);
                return res;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }


        // Dar update ao estado do pedido de introducao quando o mesmo é recusado
        // PUT: api/PedidoIntroducao/{email}/{pedidoId}/Decline
        [HttpPut("{email}/{pedidoId}/Decline")]
        public async Task<ActionResult<PedidoIntroducaoDTO>> UpdatePedido(string email, string pedidoId)
        {
            try
            {
                var pedido = await _servicePi.UpdateEstadoAsync(email, pedidoId);
                var wait = await _serviceMi.RecusarPedidoIntroducao(email, pedidoId);

                if (pedido == null) return NotFound();

                return pedido;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
    }
}