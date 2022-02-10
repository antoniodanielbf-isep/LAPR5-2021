using System.Collections.Generic;
using System.Text;
using DDDNetCore.Domain.PedidosLigacao.DTO;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.PedidosLigacao
{
    public class PedidoLigacao : Entity<PedidoLigacaoId>, IAggregateRoot
    {
        public PedidoLigacao()
        {
        }

        [JsonConstructor]
        public PedidoLigacao(string id, string origem, string destino, int forca, string tags)
        {
            Id = new PedidoLigacaoId(id);
            Origem = new EmailsPedidosLigacao(origem);
            Destino = new EmailsPedidosLigacao(destino);
            Estado = EstadoPedidoLigacao.A_AGUARDAR_RESPOSTA;
            Forca = new ForcaLigacaoPedido(forca);
            var listaAux = new List<string>();
            foreach (var tag in tags.Split(",")) listaAux.Add(tag);
            ListaTags = listaAux.ConvertAll(t => new TagPedidoLigacao(t));
        }

        public PedidoLigacaoId Id { get; }

        public EmailsPedidosLigacao Origem { get; private set; }
        public EmailsPedidosLigacao Destino { get; private set; }
        public EstadoPedidoLigacao Estado { get; private set; }
        public ForcaLigacaoPedido Forca { get; }
        public List<TagPedidoLigacao> ListaTags { get; }

        public void setEstadoPedidoLigacao(EstadoPedidoLigacao novo)
        {
            Estado = novo;
        }

        public void setEmailErased(string old, string novo)
        {
            if (Origem.ToString().Equals(old))
            {
                Origem = new EmailsPedidosLigacao(novo);
                if (Estado.Equals(EstadoPedidoLigacao.A_AGUARDAR_RESPOSTA))
                {
                    Estado = EstadoPedidoLigacao.RECUSADO;
                }
            }
            else
            {
                Destino = new EmailsPedidosLigacao(novo);
                if (Estado.Equals(EstadoPedidoLigacao.A_AGUARDAR_RESPOSTA))
                {
                    Estado = EstadoPedidoLigacao.RECUSADO;
                }
            }
        }

        public PedidoLigacaoDTO toDto()
        {
            var tagFinal = new StringBuilder();
            var i = 0;

            foreach (var tag in ListaTags)
            {
                tagFinal = tagFinal.Append(tag);
                if (i + 1 != ListaTags.Count) tagFinal = tagFinal.Append(", ");

                i += 1;
            }

            return new PedidoLigacaoDTO(Id.AsString(), Origem.ToString(), Destino.ToString(),
                Forca.ForcaPedido, tagFinal.ToString());
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"ID: {Id}").Append(" \n")
                .Append($"ESTADO: {Estado}").Append(" \n")
                .Append($"ORIGEM: {Origem}").Append(" \n")
                .Append($"DESTINO: {Destino}").Append(" \n")
                .Append($"FORÇA LIGAÇÃO: {Forca}").Append(" \n")
                .Append("TAGS:").Append(" \n");
            ListaTags.ForEach(u => s.Append(u).Append(" \n"));
            return s.ToString();
        }
    }
}