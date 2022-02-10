namespace DDDNetCore.Domain.Relacoes.DTO
{
    public class RelacaoChangeDTO
    {
        public RelacaoChangeDTO(int forca, string tags)
        {
            Forca = forca;
            Tags = tags;
        }

        public int Forca { get; }
        public string Tags { get; }
    }
}