using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IA.Domain.Utilizadores
{
    public class CaminhoEForcaLigacaoDTO
    {
        [JsonConstructor]
        public CaminhoEForcaLigacaoDTO(CaminhoDTO caminhoDto,List<string>forcasAB, List<string>forcasBA)
        {
            this.caminho = caminhoDto.caminho;
            this.valor = caminhoDto.valor;
            this.forcasLigacaoDestinoOrigem = forcasBA;
            this.forcasLigacaoOrigemDestino = forcasAB;
        }

        public List<string> caminho { get; set; }
        public int valor { get; set; }
        
        public List<string> forcasLigacaoOrigemDestino { get; set; }
        
        public List<string> forcasLigacaoDestinoOrigem{ get; set; }
  
    }
}