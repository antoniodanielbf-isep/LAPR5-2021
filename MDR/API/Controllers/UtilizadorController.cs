using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.Introducoes;
using DDDNetCore.Domain.Missoes;
using DDDNetCore.Domain.PedidosIntroducao;
using DDDNetCore.Domain.PedidosLigacao;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores;
using DDDNetCore.Domain.Utilizadores.DTO;
using DDDSample1.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilizadorController : ControllerBase
    {
        private readonly UtilizadorService _serviceU;
        private readonly MissaoService _serviceM;
        private readonly IntroducaoService _serviceI;
        private readonly PedidoIntroducaoService _servicePi;
        private readonly PedidoLigacaoService _servicePl;
        private readonly RelacaoService _serviceR;
        
        public UtilizadorController(UtilizadorService serviceU, MissaoService serviceM, 
            IntroducaoService serviceI, PedidoIntroducaoService servicePi,
            PedidoLigacaoService servicePl, RelacaoService serviceR)
        {
            _serviceU = serviceU;
            _serviceM = serviceM;
            _serviceI = serviceI;
            _servicePi = servicePi;
            _servicePl = servicePl;
            _serviceR = serviceR;
        }

        // GET: api/Utilizador
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UtilizadorDTO>>> GetAll()
        {
            return await _serviceU.GetAllAsync();
        }

        // GET: api/Utilizador/{email}
        [HttpGet("{email}")]
        public async Task<ActionResult<UtilizadorDTO>> GetGetById(string email)
        {
            var usr = await _serviceU.GetByIdAsync(new EmailUtilizador(email));

            if (usr == null) return NotFound();

            return usr;
        }

        // GET: api/Utilizador/{email}/allTagsAllUsers
        [HttpGet("{email}/allTagsAllUsers")]
        public async Task<ActionResult<UtilizadorTagsDTO>> SearchTagsAllUsers()
        {
            return await _serviceU.GetAllTagsAllUsers();
        }

        // GET: api/Utilizador/{email}/allTagsUser
        [HttpGet("{email}/allTagsUser")]
        public async Task<ActionResult<UtilizadorTagsDTO>> SearchTagsUser(string email)
        {
            return await _serviceU.GetAllTagsUser(email);
        }

        // GET: api/Utilizador/{email}/search
        [HttpPost("{email}/search")]
        public async Task<ActionResult<IEnumerable<UtilizadorDTO>>> SearchUser(UtilizadorPesquisaDTO dto)
        {
            return await _serviceU.GetAllFromSearch(dto);
        }

        // GET: api/Utilizador/{email}/creating
        [HttpGet("{email}/creating")]
        public async Task<ActionResult<UtilizadorSugestoesDTO>> GetGetByCreatingId(string email)
        {
            var usr = await _serviceU.GetByIdCreationAsync(new EmailUtilizador(email));

            if (usr == null) return NotFound();

            return usr;
        }

        // GET: api/Utilizador/{email}/sugestoesAmizade
        [HttpGet("{email}/sugestoesAmizade")]
        public async Task<ActionResult<IEnumerable<UtilizadorSugestoesSendDTO>>> GetSugestoesAmizade(string email)
        {
            var usr = await _serviceU.GetSugestoesAmizade(email);

            if (usr == null) return NotFound();

            return usr;
        }

        // GET: api/Utilizador/{email}/sugestoesAmizade
        [HttpGet("{email}/sugestoesUtilizadoresParaPedirIntroducao")]
        public async Task<ActionResult<IEnumerable<UtilizadorSugestoesSendDTO>>> GetSugestoesUtilizadorParaPedirIntroducao(string email)
        {
            var usr = await _serviceU.GetSugestoesUtilizadoresParaPedirIntroducao(email);

            if (usr == null) return NotFound();

            return usr;
        }

        // POST: api/Utilizador
        [HttpPost]
        public async Task<ActionResult<UtilizadorDTO>> Create(UtilizadorDTO dto)
        {
            try
            {
                var usr = await _serviceU.AddAsync(dto);
                return CreatedAtAction(nameof(GetGetByCreatingId), new {usr.Email}, usr);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        // PUT: api/Utilizador/{email}/changeHumor
        [HttpPut("{email}/changeHumor")]
        public async Task<ActionResult<UtilizadorDTO>> Update(string email, UtilizadorAlterarHumorDTO dto)
        {
            try
            {
                var usr = await _serviceU.UpdateAsync(email, dto);

                if (usr == null) return NotFound();
                return Ok(usr);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        // PUT: api/Utilizador/{email}/changeAllInfo
        [HttpPut("{email}/changeAllInfo")]
        public async Task<ActionResult<UtilizadorDTO>> HardUpdate(string email, UtilizadorAlterarDadosDTO dto)
        {
            try
            {
                var usr = await _serviceU.UpdateAllAsync(email, dto);

                if (usr == null) return NotFound();

                return Ok(usr);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
        
        // PUT: api/Utilizador/{email}/eraseUser
        [HttpPut("{email}/eraseUser")]
        public async Task<ActionResult<UtilizadorEraseDTO>> EraseUser(string email)
        {
            try
            {
                var usr = await _serviceU.EraseAllAsync(email);
                await _serviceM.ChangeUserMissions(email, usr.Email);
                await _serviceI.ChangeUserIntro(email, usr.Email);
                await _servicePi.ChangeUserPedI(email, usr.Email);
                await _servicePl.ChangeUserPedL(email, usr.Email);
                await _serviceR.ChangeUserRel(email, usr.Email);

                return Ok(usr);
            }
            catch (BusinessRuleValidationException ex)
            {
                return BadRequest(new {ex.Message});
            }
        }
    }
}