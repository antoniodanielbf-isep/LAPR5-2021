using API.Infraestructure.PedidosLigacao;
using API.Infraestructure.Utilizadores;
using API.Infrastructure.Introducoes;
using API.Infrastructure.Missoes;
using API.Infrastructure.PedidosIntroducao;
using API.Infrastructure.Relacoes;
using DDDNetCore.Domain.Introducoes;
using DDDNetCore.Domain.Missoes;
using DDDNetCore.Domain.PedidosIntroducao;
using DDDNetCore.Domain.PedidosLigacao;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores;
using Microsoft.EntityFrameworkCore;

namespace DDDSample1.Infrastructure
{
    public class DDDSample1DbContext : DbContext
    {
        public DDDSample1DbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Utilizador> Utilizadores { get; set; }
        public DbSet<Introducao> Introducoes { get; set; }
        public DbSet<Missao> Missoes { get; set; }
        public DbSet<Relacao> Relacoes { get; set; }
        public DbSet<PedidoLigacao> PedidosLigacao { get; set; }
        public DbSet<PedidoIntroducao> PedidosIntroducao { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UtilizadorEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new IntroducaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new MissaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RelacaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoLigacaoEntityTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PedidoIntroducaoEntityTypeConfiguration());
        }
    }
}