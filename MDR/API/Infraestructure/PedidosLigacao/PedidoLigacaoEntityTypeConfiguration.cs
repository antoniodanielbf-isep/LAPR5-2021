using DDDNetCore.Domain.PedidosLigacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infraestructure.PedidosLigacao
{
    public class PedidoLigacaoEntityTypeConfiguration : IEntityTypeConfiguration<PedidoLigacao>
    {
        public void Configure(EntityTypeBuilder<PedidoLigacao> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            //agregado Pedido
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Estado).HasConversion<string>();
            builder.OwnsOne(b => b.Destino,
                destino => { destino.Property("Email").IsRequired(); });
            builder.OwnsOne(b => b.Origem,
                origem => { origem.Property("Email").IsRequired(); });
            builder.OwnsOne(b => b.Forca,
                forca => { forca.Property("ForcaPedido").IsRequired(); });
            builder.OwnsMany(b => b.ListaTags, b => b.Property("TagsPedido").IsRequired());
        }
    }
}