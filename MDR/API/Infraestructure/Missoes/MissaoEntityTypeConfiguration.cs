using DDDNetCore.Domain.Missoes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infrastructure.Missoes
{
    public class MissaoEntityTypeConfiguration : IEntityTypeConfiguration<Missao>
    {
        public void Configure(EntityTypeBuilder<Missao> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            //agregado missao
            builder.HasKey(b => b.Id);
            builder.Property(b => b.EstadoMissao).HasConversion<string>();
            builder.OwnsOne(b => b.NivelDeDificuldade,
                nivelDif => { nivelDif.Property("NivelDificuldade").IsRequired(); });
            builder.OwnsOne(b => b.PontuacaoAtual,
                nivelDif => { nivelDif.Property("Pontuacao").IsRequired(); });

            //associacoes com o resto dos agregados
            builder.HasOne(p => p.Utilizador)
                .WithMany(b => b.MissoesUtilizador)
                .HasForeignKey(p => p.EmailUtilizador);

            builder.HasOne(p => p.PedidoIntroducao)
                .WithMany(b => b.ListaMissoes)
                .HasForeignKey(p => p.PedidoIntroducaoId);

            builder.HasOne(p => p.Introducao)
                .WithMany(b => b.ListaMissoes)
                .HasForeignKey(p => p.IntroducaoId);
        }
    }
}