using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Introducoes;
using DDDNetCore.Domain.Introducoes.DTO;
using DDDNetCore.Domain.Missoes;
using DDDNetCore.Domain.PedidosIntroducao.DTO;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Relacoes.DTO;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntroducaoController : ControllerBase
    {
        private readonly IntroducaoService _serviceI;
        private readonly MissaoService _serviceMi;
        private readonly RelacaoService _serviceR;

        public IntroducaoController(IntroducaoService serviceI, RelacaoService serviceR,
            MissaoService serviceMi)
        {
            _serviceI = serviceI;
            _serviceR = serviceR;
            _serviceMi = serviceMi;
        }

        // Obter todas as introducoes de um utilizador
        // GET: api/Introducao/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<IntroducaoDTO>>> GetIntroducoesFromUser(string email)
        {
            return await _serviceI.GetAllAsync(email);
        }

        // Auxiliar para criar uma introducao
        // GET:  api/Introducao/{pedIId}/creating
        [HttpGet("{introId}/creating")]
        public async Task<ActionResult<IntroducaoDTO>> GetByCreatingId(string introId)
        {
            var pedI = await _serviceI.GetByIdCreationAsync(new IntroducaoId(introId));

            if (pedI == null) return NotFound();

            return pedI;
        }

        // Criar introducao
        // POST:  api/Introducao/{email}/{introID}/Accept
        [HttpPost("{email}/create")]
        public async Task<ActionResult<IntroducaoDTO>> AddIntroducao(string email, PedidoIntroducaoAceiteDTO dto)
        {
            try
            {
                var intro = await _serviceI.AddAsync(email, dto);
                return intro;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }


        // Criar uma relacao atraves da aceitacao de uma introducao
        // POST:  api/Introducao/{email}/{introID}/Accept
        [HttpPost("{email}/{introID}/Accept")]
        public async Task<ActionResult<RelacaoDTO>> AcceptIntroducao(string email, string introID,
            RelacaoCriacaoDTO dto)
        {
            try
            {
                var wait = await _serviceI.AcceptAsync(email, introID, dto);
                
                var rel = await _serviceR.AddAsyncFromIntroducao(email, introID, dto);

                var wait2 = await _serviceMi.AceitarIntroducao(email, introID);


                return rel;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        // Dar update ao estado da introducao quando a mesma é recusada
        // PUT: api/Introducao/{email}/{introducaoId}/Decline
        [HttpPut("{email}/{introducaoId}/Decline")]
        public async Task<ActionResult<IntroducaoDTO>> DeclineIntroducao(string email, string introducaoId)
        {
            try
            {
                var pedido = await _serviceI.UpdateEstadoAsync(email, introducaoId);
                var wait2 = await _serviceMi.RecusarIntroducao(email, introducaoId);

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