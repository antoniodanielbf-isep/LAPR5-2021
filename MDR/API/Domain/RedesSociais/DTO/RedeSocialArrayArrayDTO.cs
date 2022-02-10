using System.Collections.Generic;
using DDDNetCore.Domain.Relacoes.DTO;

namespace DDDNetCore.Domain.RedesSociais.DTO
{
    public class RedeSocialArrayArrayDTO
    {
        public RedeSocialArrayArrayDTO(List<List<RelacaoDTO>> rede)
        {
            Rede = rede;
        }

        public List<List<RelacaoDTO>> Rede { get; }
    }
}