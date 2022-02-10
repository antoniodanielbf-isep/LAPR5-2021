using DDDNetCore.Domain.Utilizadores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Infraestructure.Utilizadores
{
    internal class UtilizadorEntityTypeConfiguration : IEntityTypeConfiguration<Utilizador>
    {
        public void Configure(EntityTypeBuilder<Utilizador> builder)
        {
            // cf. https://www.entityframeworktutorial.net/efcore/fluent-api-in-entity-framework-core.aspx

            //agregado utilizador
            builder.HasKey(b => b.Id);
            builder.Property(b => b.EstadoEmocionalUtilizador).HasConversion<string>();
            builder.OwnsOne(b => b.BreveDescricaoUtilizador,
                breveDescricao => { breveDescricao.Property("Descricao").IsRequired(false); });
            builder.OwnsOne(b => b.CidadePais, cidadePais => { cidadePais.Property("CidadeEPais").IsRequired(false); });
            builder.OwnsOne(b => b.PerfilFacebookUtilizador,
                perfilF => { perfilF.Property("UrlPerfilFacebook").IsRequired(false); });
            builder.OwnsOne(b => b.PerfilLinkedinUtilizador,
                perfilL => { perfilL.Property("UrlPerfilLinked").IsRequired(false); });
            builder.OwnsOne(b => b.ImagemU, imagem => { imagem.Property("UrlImagem").IsRequired(false); });
            builder.OwnsOne(b => b.NomeUtilizador, nome => { nome.Property("NomeUtilizador").IsRequired(); });
            builder.OwnsOne(b => b.NumeroDeTelefoneUtilizador, numero => { numero.Property("Telefone").IsRequired(); });
            builder.OwnsOne(b => b.DataDeNascimentoUtilizador,
                dataNascimento => { dataNascimento.Property("Data").IsRequired(); });
            builder.OwnsMany(b => b.TagsUtilizador, tags => { tags.Property("TagText").IsRequired(); });
            builder.OwnsOne(b => b.PasswordU, password => { password.Property("PasswordUtilizador").IsRequired(); });
            builder.OwnsOne(b => b.DataModificacao,
                dataModificacaoEstado => { dataModificacaoEstado.Property("DataMod").IsRequired(); });
            builder.OwnsOne(b => b.DataTermos,
                dataTermos => { dataTermos.Property("DataMod").IsRequired(); });
        }
    }
}