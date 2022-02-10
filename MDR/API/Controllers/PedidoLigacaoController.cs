using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.PedidosLigacao;
using DDDNetCore.Domain.PedidosLigacao.DTO;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Relacoes.DTO;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PedidoLigacaoController : ControllerBase
    {
        private readonly PedidoLigacaoService _servicePl;
        private readonly RelacaoService _serviceRel;

        public PedidoLigacaoController(PedidoLigacaoService servicePl, RelacaoService serviceR)
        {
            _servicePl = servicePl;
            _serviceRel = serviceR;
        }

        // Obter todos os pedidos de ligacao de um utilizador origem
        // GET: api/PedidoLigacao/{email}/origin
        [HttpGet("{email}/origin")]
        public async Task<ActionResult<List<PedidoLigacaoDTO>>> GetPedidosByEmailOrigem(string email)
        {
            var pedL = await _servicePl.GetByEmailOrigemAsync(email);

            if (pedL == null) return NotFound();

            return pedL;
        }

        // Obter todos os pedidos de ligacao de um utilizador destino
        // GET: api/PedidoLigacao/{email}/destiny
        [HttpGet("{email}/destiny")]
        public async Task<ActionResult<List<PedidoLigacaoDTO>>> GetPedidosByEmailIntermedio(string email)
        {
            var pedL = await _servicePl.GetByEmailInterAsync(email);

            if (pedL == null) return NotFound();

            return pedL;
        }

        // Dar update ao estado do pedido de introducao quando o mesmo é recusado
        // GET: api/PedidoLigacao/{email}/getConnectionRequest
        [HttpGet("{email}/getConnectionRequest")]
        public async Task<ActionResult<List<PedidoLigacaoDTO>>> GetPedidosLigacaoPendentes(string email)
        {
            var usr = await _servicePl.GetPedidosLigacaoPendentesAsync(email);

            if (usr == null) return NotFound();

            return usr;
        }

        // Criar um pedido de ligacao atraves de um DTO
        //POST: api/PedidoLigacao/{email}/createConnectionRequest
        [HttpPost("{email}/createConnectionRequest")]
        public async Task<ActionResult<PedidoLigacaoDTO>> criarPedidoLigacao(string email,
            PedidoLigacaoCriacaoDTO pedido)
        {
            try
            {
                var pedidoCriado = await _servicePl.AddAsync(email, pedido);

                if (pedidoCriado == null) return NotFound();
                return Ok(pedidoCriado);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        // Aceitar o Pedido Ligação e criar relação
        //POST: api/PedidoLigacao/{emailDestinatario}/Aceitar
        [HttpPost("{emailDestinatario}/{pedidoLigacaoId}/Aceitar")]
        public async Task<ActionResult<RelacaoDTO>> AceitarPedidoLigacao(string emailDestinatario,
            string pedidoLigacaoId, PedidoLigacaoAceiteDTO dto)
        {
            try
            {
                var wait = await _servicePl.AcceptPedidoLigacao(pedidoLigacaoId);
                if (wait == null) return BadRequest("Erro ao Aceitar Pedido de Ligação");

                var relacao = await _serviceRel.AddAsyncFromPedidoLigacao(pedidoLigacaoId, dto);
                return relacao;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        // Dar update ao estado do pedido de ligacao quando o mesmo é recusado
        // PUT: api/PedidoLigacao/{email}/{pedidoLigacaoId}/Decline
        [HttpPut("{email}/{pedidoLigacaoId}/Decline")]
        public async Task<ActionResult<PedidoLigacaoDTO>> DeclineIntroducao(string email, string pedidoLigacaoId)
        {
            try
            {
                var pedido = await _servicePl.UpdateEstadoAsync(email, pedidoLigacaoId);

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