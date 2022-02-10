using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IA.Domain.Utilizadores
{
    public class CaminhoDTO
    {
        [JsonConstructor]
        public CaminhoDTO(List<string> caminho, int valor)
        {
            this.caminho = caminho;
            this.valor = valor;
        }

        public List<string> caminho { get; set; }
        public int valor { get; set; }
  
    }
}