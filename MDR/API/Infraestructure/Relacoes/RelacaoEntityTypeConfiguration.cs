using DDDNetCore.Domain.Relacoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Relacoes
{
    public class RelacaoEntityTypeConfiguration : IEntityTypeConfiguration<Relacao>
    {
        public void Configure(EntityTypeBuilder<Relacao> builder)
        {
            //cf https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            //agregado Relacao
            builder.HasKey(b => b.Id);
            builder.OwnsOne(b => b.UtilizadorDestino,
                destino => { destino.Property("Email").IsRequired(); });
            builder.OwnsOne(b => b.UtilizadorOrigem,
                origem => { origem.Property("Email").IsRequired(); });
            builder.OwnsOne(b => b.ForcaLigacaoOrigDest,
                forca => { forca.Property("ForcaLigacaoRel").IsRequired(); });
            builder.OwnsOne(b => b.ForcaLigacaoDestOrig,
                forca => { forca.Property("ForcaLigacaoRel").IsRequired(); });
            builder.OwnsMany(b => b.TagA, b => b.Property("TagsRelacao").IsRequired());
            builder.OwnsMany(b => b.TagB, b => b.Property("TagsRelacao").IsRequired());
        }
    }
}