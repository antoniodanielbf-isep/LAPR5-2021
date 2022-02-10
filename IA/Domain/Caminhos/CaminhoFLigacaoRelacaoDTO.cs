using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IA.Domain.Utilizadores
{
    public class CaminhoFLigacaoRelacaoDTO
    {
        [JsonConstructor]
        public CaminhoFLigacaoRelacaoDTO(CaminhoDTO caminhoDto,List<string>forcasAB, List<string>forcasBA, List<string>relAB, List<string>relBA)
        {
            this.caminho = caminhoDto.caminho;
            this.valor = caminhoDto.valor;
            this.forcasLigacaoDestinoOrigem = forcasBA;
            this.forcasLigacaoOrigemDestino = forcasAB;
            this.forcasRelacaoDestinoOrigem = relBA;
            this.forcasRelacaoOrigemDestino = relAB;
        }

        public List<string> caminho { get; set; }
        public int valor { get; set; }
        
        public List<string> forcasLigacaoOrigemDestino { get;  }
        public List<string> forcasLigacaoDestinoOrigem{ get;  }
        
        public List<string> forcasRelacaoOrigemDestino { get;  }
        public List<string> forcasRelacaoDestinoOrigem{ get; }
  
    }
}