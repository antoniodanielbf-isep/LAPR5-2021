using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IA.Domain.Prolog;
using IA.Domain.Utilizadores;
using Microsoft.AspNetCore.Mvc;

namespace IA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RedeSocialController
    {
        private readonly RedeSocialService _service;

        public RedeSocialController(RedeSocialService serviceI)
        {
            _service = serviceI;
        }

        
        [HttpGet("XTagsEmComum/{numeroTags}")]
        public async Task<string> GetXTagsEmComum(int numeroTags)
        {
            return _service.XTagsEmComum(numeroTags);
        }
        
        
        [HttpGet("TamanhoRedeUtilizador/{utilizador}/{nivelMaximo}")]
        public async Task<TamanhoDTO> GetTamanhoRedeUtilizador(string utilizador, int nivelMaximo)
        {
            return _service.TamanhoRede(utilizador, nivelMaximo);
        }
        
        [HttpGet("NumeroLigacoesSegundoNivel/{utilizador}")]
        public async Task<TamanhoDTO> GetNumeroLigacoesSegundoNivel(string utilizador)
        {
            return _service.TamanhoRede(utilizador,2);
        }
        
        [HttpGet("TamanhoRedeTerceiroNivel/{utilizador}")]
        public async Task<TamanhoDTO> GetTamanhoRedeTerceiroNivel(string utilizador)
        {
            return _service.TamanhoRede(utilizador,3);
        }
        
        [HttpGet("AmigosEmComum/{utilizador1}/{utilizador2}")]
        public async Task<List<string>> AmigosEmComum(string utilizador1, string utilizador2)
        {
            return _service.GrafoAmigosEmComum(utilizador1, utilizador2);
        }
        
        [HttpGet("FortalezaRede/{utilizador1}")]
        public async Task<int> FortalezaRede(string utilizador1)
        {
            return _service.FortalezaDaRede(utilizador1);
        }
        
        [HttpPost("SugestaoGrupos/{utilizador}/{n}/{t}")]
        public async Task<GrupoDTO> GetSugestao(string utilizador, int n, int t, string listaTags)
        {
            return _service.SugestaoGrupos(utilizador, n, t,listaTags );
        }
    }
}