using System.Collections.Generic;
using DDDNetCore.Domain.Utilizadores.DTO;
using DDDSample1.Domain.Shared;

namespace DDDNetCore.Domain.Utilizadores
{
    public interface IUtilizadorRepository : IRepository<Utilizador, EmailUtilizador>
    {
        public List<Utilizador> pesquisaViaDTO(UtilizadorPesquisaDTO dto);
        public Utilizador tryToLogin(string email, string password);
        int getTotalUsers();
    }
}