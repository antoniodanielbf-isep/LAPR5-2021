using System.Collections.Generic;
using Newtonsoft.Json;

namespace IA.Domain.Utilizadores
{
    public class TamanhoDTO
    {
        [JsonConstructor]
        public TamanhoDTO(List<string> us, int valor)
        {
            users = us;
            tamanho = valor;
        }

        public List<string> users { get; set; }
        public int tamanho { get; set; }
  
    }
}