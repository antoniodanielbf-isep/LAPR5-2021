using System.Collections.Generic;

namespace DDDNetCore.Domain.Relacoes.DTO
{
    public class RelacaoDTO
    {
        public RelacaoDTO(string relacaoId, string utilizadorOrigem, string utilizadorDestino,
            int forcaLigacaoOrigDest, int forcaLigacaoDestOrig, List<string> TagsOD, List<string> tagsDO)
        {
            RelacaoId = relacaoId;
            UtilizadorOrigem = utilizadorOrigem;
            UtilizadorDestino = utilizadorDestino;
            ForcaLigacaoOrigDest = forcaLigacaoOrigDest;
            ForcaLigacaoDestOrig = forcaLigacaoDestOrig;
            TagsRelacaoAB = TagsOD;
            TagsRelacaoBA = tagsDO;
        }

        public string RelacaoId { get; }
        public string UtilizadorOrigem { get; }
        public string UtilizadorDestino { get; }
        public int ForcaLigacaoOrigDest { get; }
        public int ForcaLigacaoDestOrig { get; }
        public List<string> TagsRelacaoAB { get; }
        public List<string> TagsRelacaoBA { get; }
    }
}