using System.Collections.Generic;
using Newtonsoft.Json;

namespace IA.Domain.Utilizadores
{
    public class ListaDTO
    {
        [JsonConstructor]
        public ListaDTO(List<string> us)
        {
            listaUsers = us;
        }
        
        public ListaDTO(string us)
        {
            string []aux= us.Split(",");
            listaUsers = new List<string>();
            foreach (var s in aux)
            {
                listaUsers.Add(s);
            }
        }

        public List<string> listaUsers { get; set; }

        public string toString()
        {
            string ret = "";
            for (int i = 0; i < listaUsers.Count; i++)
            {
                if (i == listaUsers.Count - 1)
                {
                    ret += listaUsers[i];
                }
                else
                {
                    ret += listaUsers[i] + ", ";
                }
            }

            return ret;
        }
  
    }
}