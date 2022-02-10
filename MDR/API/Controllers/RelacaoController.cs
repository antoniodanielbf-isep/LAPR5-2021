using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Relacoes.DTO;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RelacaoController : ControllerBase
    {
        private readonly RelacaoService _serviceR;

        public RelacaoController(RelacaoService serviceR)
        {
            _serviceR = serviceR;
        }

        // GET: api/Relacao
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelacaoDTO>>> GetAll()
        {
            return await _serviceR.GetAllAsync();
        }

        // GET: api/Relacao/email
        [HttpGet("{email}")]
        public async Task<ActionResult<IEnumerable<RelacaoDTO>>> GetAllFromUser(string email)
        {
            return await _serviceR.GetAllFromUserAsync(email);
        }

        // GET: api/Relacao/{introducaoId}/getById
        [HttpGet("{introducaoId}/getById")]
        public async Task<ActionResult<RelacaoDTO>> GetRelacaoById(string introducaoId)
        {
            var relacao = await _serviceR.GetByIdAsync(introducaoId);

            if (relacao == null) return NotFound();

            return relacao;
        }

        // GET: api/Relacao/{email}/getTagsAllUsers
        [HttpGet("{email}/getTagsAllUsers")]
        public async Task<ActionResult<RelacaoTagsDTO>> GetTagsFromAllUsers()
        {
            var relacao = await _serviceR.GetTagsFromAllUsers();

            if (relacao == null) return NotFound();

            return relacao;
        }

        // GET: api/Relacao/{email}/getTagsUser
        [HttpGet("{email}/getTagsUser")]
        public async Task<ActionResult<RelacaoTagsDTO>> GetTagsFromUser(string email)
        {
            var relacao = await _serviceR.GetTagsFromUser(email);

            if (relacao == null) return NotFound();

            return relacao;
        }

        // GET: api/Relacao/{introducaoId}/creating
        [HttpGet("{introducaoId}/creating")]
        public async Task<ActionResult<RelacaoDTO>> GetByCreatingId(string relaId)
        {
            var rel = await _serviceR.GetByIdCreationAsync(new RelacaoId(relaId));

            if (rel == null) return NotFound();

            return rel;
        }

        // POST: api/Relacao/{email}/{introducaoId}/AcceptFromIntroducao
        [HttpPost("{email}/{introducaoId}/AcceptFromIntroducao")]
        public async Task<ActionResult<RelacaoDTO>> CreateRelation(string email, string introducaoId,
            RelacaoCriacaoDTO dto)
        {
            try
            {
                var rela = await _serviceR.AddAsyncFromIntroducao(email, introducaoId, dto);
                return GetByCreatingId(rela.RelacaoId).Result;
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }


        // PUT: api/Relacao/{email}/{relacaoId}/changeAllInfo
        [HttpPut("{email}/{relacaoId}/changeAllInfo")]
        public async Task<ActionResult<RelacaoDTO>> UpdateRelation(string email, string relacaoId,
            RelacaoChangeDTO dto)
        {
            try
            {
                var rel = await _serviceR.UpdateAllAsync(email, relacaoId, dto);

                if (rel == null) return NotFound();

                return Ok(rel);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
    }
}