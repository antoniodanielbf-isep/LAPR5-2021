using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDNetCore.Domain.Missoes;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores;
using DDDSample1.Domain.Leaderboards.DTO;
using DDDSample1.Domain.Shared;

namespace DDDSample1.Domain.Leaderboards
{
    public class LeaderboardService
    {
        private readonly IUtilizadorRepository _repoU;
        private readonly IRelacaoRepository _repoR;
        private readonly IMissaoRepository _repoM;
        private readonly IUnitOfWork _unitOfWork;

        public LeaderboardService(IUtilizadorRepository repoU, IRelacaoRepository repoR,
            IMissaoRepository repoM, IUnitOfWork unitOfWork)
        {
            _repoU = repoU;
            _repoR = repoR;
            _repoM = repoM;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LeaderboardDTO>> GetTopTenStrength(string emailUtilizador)
        {
            var listUsers = await _repoU.GetAllAsync();
            if (listUsers.Count == 0)
                return null;
            
            var listHelp = new List<LeaderboardDTO>();

            foreach (var user in listUsers)
            {
                var relacoes = _repoR.GetRelacoesFromUser(user.Id.ToString());
                int valor = 0;
                foreach (var rel in relacoes)
                {
                    if (rel.UtilizadorOrigem.Email.Equals(user.Id.ToString()))
                    {
                        valor += rel.ForcaLigacaoOrigDest.ForcaLigacaoRel;
                    }
                    else
                    {
                        valor += rel.ForcaLigacaoDestOrig.ForcaLigacaoRel;
                    }
                }
                
                listHelp.Add(new LeaderboardDTO(user.Id.ToString(), valor));
            }
            
            var list10 = listHelp.OrderByDescending(i => i.Valor).Take(10).ToList();
            
            bool checkUser = true;
            foreach (var users in list10)
            {
                if (users.Email.Equals(emailUtilizador))
                {
                    checkUser = false;
                }
            }

            if (checkUser)
            {
                var relacoes = _repoR.GetRelacoesFromUser(emailUtilizador);
                int valor = 0;
                foreach (var rel in relacoes)
                {
                    if (rel.UtilizadorOrigem.Email.Equals(emailUtilizador))
                    {
                        valor += rel.ForcaLigacaoOrigDest.ForcaLigacaoRel;
                    }
                    else
                    {
                        valor += rel.ForcaLigacaoDestOrig.ForcaLigacaoRel;
                    }
                }
                
                list10.Add(new LeaderboardDTO(emailUtilizador, valor));
            }

            return list10;
        }

        public async Task<List<LeaderboardDTO>> GetTopTenDimension(string emailUtilizador)
        {
            var listUsers = await _repoU.GetAllAsync();
            if (listUsers.Count == 0)
                return null;
            
            var listHelp = new List<LeaderboardDTO>();

            foreach (var user in listUsers)
            {
                int relacoes = _repoR.GetRelacoesFromUser(user.Id.ToString()).Count;
                listHelp.Add(new LeaderboardDTO(user.Id.ToString(), relacoes));
            }
            
            var list10 = listHelp.OrderByDescending(i => i.Valor).Take(10).ToList();
            
            bool checkUser = true;
            foreach (var users in list10)
            {
                if (users.Email.Equals(emailUtilizador))
                {
                    checkUser = false;
                }
            }

            if (checkUser)
            {
                list10.Add(new LeaderboardDTO(emailUtilizador, _repoR.GetRelacoesFromUser(emailUtilizador).Count));
            }

            return list10;
        }
        
        public async Task<List<LeaderboardDTO>> GetTopTenPoints(string emailUtilizador)
        {
            var listUsers = await _repoU.GetAllAsync();
            if (listUsers.Count == 0)
                return null;
            
            var listHelp = new List<LeaderboardDTO>();

            foreach (var user in listUsers)
            {
                int pontos = 0;
                var missoes = _repoM.GetByEmailID(user.Id.ToString());
                foreach (var missao in missoes)
                {
                    pontos += missao.PontuacaoAtual.Pontuacao;
                }
                listHelp.Add(new LeaderboardDTO(user.Id.ToString(), pontos));
            }
            
            var list10 = listHelp.OrderByDescending(i => i.Valor).Take(10).ToList();
            
            bool checkUser = true;
            foreach (var users in list10)
            {
                if (users.Email.Equals(emailUtilizador))
                {
                    checkUser = false;
                }
            }

            if (checkUser)
            {
                int pontos = 0;
                var missoes = _repoM.GetByEmailID(emailUtilizador);
                foreach (var missao in missoes)
                {
                    pontos += missao.PontuacaoAtual.Pontuacao;
                }
                list10.Add(new LeaderboardDTO(emailUtilizador, pontos));
            }

            return list10;
        }
    }
}