using System;
using System.Collections.Generic;
using System.Linq;
using DDDNetCore.Domain.Utilizadores;
using DDDNetCore.Domain.Utilizadores.DTO;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace API.Infraestructure.Utilizadores
{
    public class UtilizadorRepository : BaseRepository<Utilizador, EmailUtilizador>, IUtilizadorRepository
    {
        public UtilizadorRepository(DDDSample1DbContext context) : base(context.Utilizadores)
        {
        }

        public List<Utilizador> pesquisaViaDTO(UtilizadorPesquisaDTO dto)
        {
            try
            {
                if (!dto.Email.Equals(""))
                {
                    var result = context()
                        .FromSqlInterpolated($"SELECT * FROM Utilizadores WHERE Utilizadores.Id = {dto.Email}")
                        .ToList();

                    return result;
                }

                if (!dto.Nome.Equals("") && !dto.CidadePais.Equals(""))
                {
                    var result = context()
                        .FromSqlInterpolated(
                            $"SELECT * FROM Utilizadores WHERE Utilizadores.NomeUtilizador_NomeUtilizador = {dto.Nome} AND Utilizadores.CidadePais_CidadeEPais = {dto.CidadePais}")
                        .ToList();

                    return result;
                }

                if (!dto.Nome.Equals(""))
                {
                    var result = context()
                        .FromSqlInterpolated(
                            $"SELECT * FROM Utilizadores WHERE Utilizadores.NomeUtilizador_NomeUtilizador = {dto.Nome}")
                        .ToList();

                    return result;
                }

                if (!dto.CidadePais.Equals(""))
                {
                    var result = context()
                        .FromSqlInterpolated(
                            $"SELECT * FROM Utilizadores WHERE Utilizadores.CidadePais_CidadeEPais = {dto.CidadePais}")
                        .ToList();

                    return result;
                }

                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public Utilizador tryToLogin(string email, string password)
        {
            try
            {
                var result = context()
                    .FromSqlInterpolated(
                        $"SELECT * FROM Utilizadores WHERE Utilizadores.Id = {email} AND Utilizadores.PasswordU_PasswordUtilizador = {password}")
                    .ToList();
                if (result.Count == 1) return result[0];

                return null;
            }
            catch (NullReferenceException)
            {
                return null;
            }
        }

        public int getTotalUsers()
        {
            var result = context().FromSqlInterpolated(
                $"SELECT *  FROM Utilizadores"
            ).Count();
            return result;
        }
    }
}