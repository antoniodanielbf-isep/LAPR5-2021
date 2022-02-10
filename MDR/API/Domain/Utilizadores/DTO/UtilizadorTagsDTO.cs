using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores.DTO
{
    public class UtilizadorTagsDTO
    {
        [JsonConstructor]
        public UtilizadorTagsDTO(string tags)
        {
            Tags = tags;
        }

        public string Tags { get; set; }
    }
}