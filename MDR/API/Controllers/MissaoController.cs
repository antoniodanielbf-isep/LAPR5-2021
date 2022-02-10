using DDDNetCore.Domain.Missoes;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MissaoController
    {
        private readonly MissaoService _missaoService;

        public MissaoController(MissaoService serviceMissao)
        {
            _missaoService = serviceMissao;
        }
    }
}