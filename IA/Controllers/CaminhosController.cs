using System.Net;
using System.Threading.Tasks;
using IA.Domain.Prolog;
using IA.Domain.Utilizadores;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CaminhosController : ControllerBase
    {
        private readonly CaminhosService _service;

        public CaminhosController(CaminhosService serviceI)
        {
            _service = serviceI;
        }


        [HttpGet("caminhoMaisCurto/{origem}/{destino}")]
        public async Task<CaminhoDTO> GetCaminhoMaisCurto(string origem, string destino)
        {
            return _service.CaminhoMaisCurtoPL(origem, destino);
        }


        [HttpGet("caminhoMaisForte/{origem}/{destino}")]
        public async Task<CaminhoDTO> GetCaminhoMaisForte(string origem, string destino)
        {
            return _service.CaminhoMaisFortePL(origem, destino);
        }


        [HttpGet("caminhoMaisSeguro/{origem}/{destino}/{valorMinimo}")]
        public async Task<CaminhoDTO> GetCaminhoMaisSeguro(string origem, string destino, int valorMinimo)
        {
            return _service.CaminhoMaisSeguroPL(origem, destino, valorMinimo);
        }

        [HttpGet("GetCaminhoAStar/{origem}/{destino}/{N}/forcaLigacao")]
        public async Task<CaminhoEForcaLigacaoDTO> GetCaminhoAStar(string origem, string destino, string N)
        {
            return _service.CaminhoAStar(origem, destino,N);
        }

        [HttpGet("GetCaminhoAStarMultiCriterio/{origem}/{destino}/{N}/multiCriterio")]
        public async Task<CaminhoFLigacaoRelacaoDTO> GetCaminhoAStarMultiCriterio(string origem, string destino, int N)
        {
            return _service.CaminhoAStarMultiCriterio(origem, destino, N);
        }
        
        [HttpGet("GetCaminhoEstadosEmocionais/{origem}/{destino}/{N}/a-star-sentimentos")]
        public async Task<CaminhoDTO> GetCaminhoEstadosEmocionais(string origem, string destino, int N)
        {
            return _service.CaminhoEstadosEmocionais(origem, destino, N);
        }
        
        [HttpGet("GetCaminhoEstadosEmocionais/{origem}/{destino}/{N}/best-first-sentimentos")]
        public async Task<CaminhoDTO> GetCaminhoEstadosEmocionaisBESTFIRST(string origem, string destino, int N)
        {
            return _service.CaminhoEstadosEmocionaisBESTFIRST(origem, destino, N);
        }

    }
}