using System.Collections.Generic;
using Newtonsoft.Json;

namespace IA.Domain.Utilizadores
{
    public class GrupoDTO
    {
        [JsonConstructor]
        public GrupoDTO(List<string> us)
        {
            listaUsers = us;
        }

        public List<string> listaUsers { get; set; }

  
    }
}