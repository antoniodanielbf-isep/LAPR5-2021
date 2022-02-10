using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Leaderboards;
using DDDSample1.Domain.Leaderboards.DTO;
using Microsoft.AspNetCore.Mvc;

namespace DDDNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaderBoardController : ControllerBase
    {
        private readonly LeaderboardService _serviceLb;

        public LeaderBoardController(LeaderboardService serviceLb)
        {
            _serviceLb = serviceLb;
        }

        // Consultar leader board - fortaleza de rede
        // GET: api/LeaderBoard/{email}/strength
        [HttpGet("{email}/strength")]
        public async Task<ActionResult<IEnumerable<LeaderboardDTO>>> GetLeaderBoardByStrength(string email)
        {
            return await _serviceLb.GetTopTenStrength(email);
        }

        // Consultar leader board - dimensão de rede
        // GET: api/LeaderBoard/{email}/dimension
        [HttpGet("{email}/dimension")]
        public async Task<ActionResult<IEnumerable<LeaderboardDTO>>> GetLeaderBoardByDimension(string email)
        {
            return await _serviceLb.GetTopTenDimension(email);
        }
        
        // Consultar leader board - pontuacao
        // GET: api/LeaderBoard/{email}/points
        [HttpGet("{email}/points")]
        public async Task<ActionResult<IEnumerable<LeaderboardDTO>>> GetLeaderBoardByPoints(string email)
        {
            return await _serviceLb.GetTopTenPoints(email);
        }
    }
}