using DDDNetCore.Domain.Introducoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Introducoes
{
    public class IntroducaoEntityTypeConfiguration : IEntityTypeConfiguration<Introducao>
    {
        public void Configure(EntityTypeBuilder<Introducao> builder)
        {
            //cf https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            //agregado Introducao
            builder.HasKey(c => c.Id);
            builder.Property(c => c.EstadoIntroducao).HasConversion<string>();
            builder.OwnsOne(c => c.DescricaoIntroducao,
                descricaoIntroducao => { descricaoIntroducao.Property("DescricaoIntroducao").IsRequired(); });
            builder.OwnsOne(c => c.UtilizadorDestino,
                emailDestino => { emailDestino.Property("Email").IsRequired(); });
            builder.OwnsOne(c => c.UtilizadorIntermediario,
                emailIntermediario => { emailIntermediario.Property("Email").IsRequired(false); });
            builder.OwnsOne(c => c.UtilizadorOrigem,
                emailOrigem => { emailOrigem.Property("Email").IsRequired(); });
            builder.OwnsOne(c => c.ForcaLig,
                forca => { forca.Property("Forca").IsRequired(); });
            builder.OwnsMany(c => c.ListaIntroducao, c => c.Property("TagIntro").IsRequired());
        }
    }
}