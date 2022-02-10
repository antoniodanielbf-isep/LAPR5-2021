namespace DDDNetCore.Domain.RedeSocial.DTO
{
    public class RedeSocialDto
    {
        public RedeSocialDto(global::RedeSocial rede)
        {
            Grafo = new GraphDto(rede.grafo);
        }

        public GraphDto Grafo { get; }
    }
}