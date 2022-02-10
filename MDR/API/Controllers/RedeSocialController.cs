using System.Collections.Generic;
using System.Threading.Tasks;
using DDDNetCore.Domain.RedeSocial;
using DDDNetCore.Domain.RedesSociais.DTO;
using DDDNetCore.Domain.Relacoes.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedeSocialController : ControllerBase
    {
        private readonly RedeSocialService _serviceRs;

        public RedeSocialController(RedeSocialService service)
        {
            _serviceRs = service;
        }

        // GET: api/RedeSocial/{email}/{nivel}
        [HttpGet("{email}/{nivel}")]
        public async Task<ActionResult<List<List<RelacaoDTO>>>> getRedeSocialPerspetiva(string email, int nivel)
        {
            return await _serviceRs.getRedePerspetivaUser(email, nivel);
        }
        
        // GET: api/RedeSocial/{email}/{nivel}/dto
        [HttpGet("{email}/{nivel}/dto")]
        public async Task<ActionResult<RedeSocialArrayArrayDTO>> getRedeSocialPerspetivaDTO(string email, int nivel)
        {
            List<List<RelacaoDTO>> lista = await _serviceRs.getRedePerspetivaUser(email, nivel);
            return new RedeSocialArrayArrayDTO(lista);
        }

        // GET: api/RedeSocial/{email}/tamanhoRede
        [HttpGet("{email}/tamanhoRede")]
        public async Task<ActionResult<TamanhoRedeSocialTotalDTO>> getTamanhoRedeSocial(string email)
        {
            return await _serviceRs.getTamanhoRedeSocial();
        }
    }
}