using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IA.Domain.Utilizadores
{
    public class CaminhoDTO2
    {
        [JsonConstructor]
        public CaminhoDTO2(List<string> caminho, double valor)
        {
            this.caminho = caminho;
            this.valor = valor;
        }

        public List<string> caminho { get; set; }
        public double valor { get; set; }
  
    }
}