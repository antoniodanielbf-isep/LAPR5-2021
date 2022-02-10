using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DDDNetCore.Domain.Missoes;
using DDDNetCore.Domain.PedidosIntroducao.DTO;
using DDDSample1.Domain.Shared;
using Newtonsoft.Json;

namespace DDDNetCore.Domain.PedidosIntroducao
{
    public class PedidoIntroducao : Entity<PedidoIntroducaoId>, IAggregateRoot
    {
        public PedidoIntroducao()
        {
            ListaTags = new List<TagPedidoIntroducao>();
        }

        [JsonConstructor]
        public PedidoIntroducao(string idPedidoIntro, string descricaoPedidoIntro, string origemPedidoIntro,
            string intermedioPedidoIntro,
            string destinoPedidoIntro, int forcaPedidoIntro, string listaTagsPedidoIntro)
        {
            Id = new PedidoIntroducaoId(idPedidoIntro);
            Descricao = new DescricaoPedidoIntroducao(descricaoPedidoIntro);
            Origem = new EmailsPedidosIntroducao(origemPedidoIntro);
            Intermedio = new EmailsPedidosIntroducao(intermedioPedidoIntro);
            Destino = new EmailsPedidosIntroducao(destinoPedidoIntro);
            Estado = EstadoPedidoIntroducao.Pendente;
            Forca = new ForcaLigacaoPedidoIntroducao(forcaPedidoIntro);
            var listaAux = new List<string>();
            foreach (var tag in listaTagsPedidoIntro.Split(",")) listaAux.Add(tag);

            ListaTags = listaAux.ConvertAll(ltp => new TagPedidoIntroducao(ltp));
        }

        [Key] public PedidoIntroducaoId Id { get; }

        public DescricaoPedidoIntroducao Descricao { get; }
        public EmailsPedidosIntroducao Origem { get; private set; }
        public EmailsPedidosIntroducao Intermedio { get; private set; }
        public EmailsPedidosIntroducao Destino { get; private set; }
        public EstadoPedidoIntroducao Estado { get; private set; }
        public ForcaLigacaoPedidoIntroducao Forca { get; }
        public List<TagPedidoIntroducao> ListaTags { get; }
        public List<Missao> ListaMissoes { get; set; }

        public void AddMissao(Missao missao)
        {
            ListaMissoes.Add(missao);
        }

        public void RemoveMissao(Missao missao)
        {
            ListaMissoes.Remove(missao);
        }

        public void setEstadoPedidoIntroducao(EstadoPedidoIntroducao novo)
        {
            Estado = novo;
        }

        public void setEmailErased(string old, string novo)
        {
            if (Origem.ToString().Equals(old))
            {
                Origem = new EmailsPedidosIntroducao(novo);
                if (Estado.Equals(EstadoPedidoIntroducao.Pendente))
                {
                    Estado = EstadoPedidoIntroducao.Recusada;
                }
            }
            else if (Destino.ToString().Equals(old))
            {
                Destino = new EmailsPedidosIntroducao(novo);
                if (Estado.Equals(EstadoPedidoIntroducao.Pendente))
                {
                    Estado = EstadoPedidoIntroducao.Recusada;
                }
            }
            else
            {
                Intermedio = new EmailsPedidosIntroducao(novo);
                if (Estado.Equals(EstadoPedidoIntroducao.Pendente))
                {
                    Estado = EstadoPedidoIntroducao.Recusada;
                }
            }
        }

        public PedidoIntroducaoDTO toDto()
        {
            var tagFinal = new StringBuilder();
            var i = 0;

            foreach (var tag in ListaTags)
            {
                tagFinal = tagFinal.Append(tag);
                if (i + 1 != ListaTags.Count) tagFinal = tagFinal.Append(", ");

                i += 1;
            }

            return new PedidoIntroducaoDTO(Id.ToString(), Descricao.ToString(), Origem.ToString(),
                Intermedio.ToString(), Destino.ToString(),
                EstadoPedidoIntroducaoOperations.getIDByEstadoPedidoIntroducao(Estado.ToString()),
                Forca.ForcaPedidoIntroducao, tagFinal.ToString());
        }

        public override string ToString()
        {
            var s = new StringBuilder();
            s.Append($"ID: {Id}").Append(" \n")
                .Append($"DESCRICAO: {Descricao}").Append(" \n")
                .Append($"ESTADO: {Estado}").Append(" \n")
                .Append($"ORIGEM: {Origem}").Append(" \n")
                .Append($"INTERMEDIO: {Intermedio}").Append(" \n")
                .Append($"DESTINO: {Destino}").Append(" \n")
                .Append($"FORÇA LIGAÇÃO: {Forca}").Append(" \n")
                .Append("TAGS:").Append(" \n");
            ListaTags.ForEach(u => s.Append(u).Append(" \n"));
            return s.ToString();
        }
    }
}