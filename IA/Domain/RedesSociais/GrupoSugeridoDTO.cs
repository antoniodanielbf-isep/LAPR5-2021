using System.Collections.Generic;

namespace IA.Domain.RedesSociais
{
    public class GrupoSugeridoDTO
    {
        public List<string> Tags { get; set; }
        public int tamanhoMinimo { get; set; }
        public int tamanhoMaximo { get; set; }

        public GrupoSugeridoDTO(List<string> tags, int tamanhoMinimo, int tamanhoMaximo)
        {
            Tags = tags;
            this.tamanhoMinimo = tamanhoMinimo;
            this.tamanhoMaximo = tamanhoMaximo;
        }
    }
}