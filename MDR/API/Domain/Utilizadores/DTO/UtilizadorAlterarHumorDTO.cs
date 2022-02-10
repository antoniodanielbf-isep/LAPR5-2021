using Newtonsoft.Json;

namespace DDDNetCore.Domain.Utilizadores.DTO
{
    public class UtilizadorAlterarHumorDTO
    {
        [JsonConstructor]
        public UtilizadorAlterarHumorDTO(int estadoEmocionalUtilizador)
        {
            EstadoEmocionalUtilizador = estadoEmocionalUtilizador;
        }

        public int EstadoEmocionalUtilizador { get; set; }
    }
}