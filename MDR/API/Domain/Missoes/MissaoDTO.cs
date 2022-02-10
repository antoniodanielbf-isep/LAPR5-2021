namespace DDDNetCore.Domain.Missoes
{
    public class MissaoDTO
    {
        public MissaoDTO(int nivelDeDificuldade, string estadoMissao)
        {
            NivelDeDificuldade = nivelDeDificuldade;
            EstadoMissao = estadoMissao;
        }

        public int NivelDeDificuldade { get; set; }
        public string EstadoMissao { get; set; }
    }
}