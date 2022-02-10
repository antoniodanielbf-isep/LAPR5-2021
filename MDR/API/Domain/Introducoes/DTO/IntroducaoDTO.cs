using System.Collections.Generic;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.Introducoes.DTO
{
    public class IntroducaoDTO
    {
        [JsonConstructor]
        public IntroducaoDTO(string id, string descricao, string utilizadorOrigem, string utilizadorIntermediario,
            string utilizadorDestino, string estado, List<string> tags)
        {
            IntroducaoId = id;
            Descricao = descricao;
            UtilizadorOrigem = utilizadorOrigem;
            UtilizadorIntermediario = utilizadorIntermediario;
            UtilizadorDestino = utilizadorDestino;
            EstadoIntro = estado;
            Tags = tags;
        }

        public string IntroducaoId { get; set; }
        public string Descricao { get; set; }
        public string UtilizadorOrigem { get; set; }
        public string UtilizadorIntermediario { get; set; }
        public string UtilizadorDestino { get; set; }
        public string EstadoIntro { get; set; }
        public List<string> Tags { get; set; }
    }
}