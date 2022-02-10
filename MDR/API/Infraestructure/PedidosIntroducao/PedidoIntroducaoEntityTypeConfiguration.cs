using DDDNetCore.Domain.PedidosIntroducao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.PedidosIntroducao
{
    public class PedidoIntroducaoEntityTypeConfiguration : IEntityTypeConfiguration<PedidoIntroducao>
    {
        public void Configure(EntityTypeBuilder<PedidoIntroducao> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            //agregado Pedido
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Estado).HasConversion<string>();
            builder.OwnsOne(b => b.Descricao,
                descricao => { descricao.Property("DescPedidoIntroducao").IsRequired(); });
            builder.OwnsOne(b => b.Origem,
                origem => { origem.Property("Email").IsRequired(); });
            builder.OwnsOne(b => b.Intermedio,
                origem => { origem.Property("Email").IsRequired(); });
            builder.OwnsOne(b => b.Destino,
                destino => { destino.Property("Email").IsRequired(); });
            builder.OwnsOne(b => b.Forca,
                forca => { forca.Property("ForcaPedidoIntroducao").IsRequired(); });
            builder.OwnsMany(b => b.ListaTags, b => b.Property("TagPIntroducao").IsRequired());
        }
    }
}