namespace DDDNetCore.Domain.Relacoes.DTO
{
    public class RelacaoTagsDTO
    {
        public RelacaoTagsDTO(string tags)
        {
            Tags = tags;
        }

        public string Tags { get; }
    }
}