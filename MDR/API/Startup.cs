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
using DDDNetCore.Domain.RedeSocial;
using DDDNetCore.Domain.Relacoes;
using DDDNetCore.Domain.Utilizadores;
using DDDSample1.Domain.Autenticacao;
using DDDSample1.Domain.Leaderboards;
using DDDSample1.Domain.Shared;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDDSample1
{
    public class Startup
    {
        private static string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DDDSample1DbContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("BloggingDatabase"))
                    .ReplaceService<IValueConverterSelector, StronglyEntityIdValueConverterSelector>());

            ConfigureCors(services);
            ConfigureMyServices(services);
            services.AddControllers().AddNewtonsoftJson();

            services.AddSwaggerGen();
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors("AllowAll");

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            app.UseSwagger(options => { options.SerializeAsV2 = true; });

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.InjectStylesheet("/swagger-ui/SwaggerDark.css");
                options.RoutePrefix = string.Empty;
            });

            app.UseOpenApi();
            app.UseSwaggerUi3();

            app.UseAuthentication();
        }

        public void ConfigureMyServices(IServiceCollection services)
        {
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            //UTILIZADOR
            services.AddTransient<UtilizadorService>();
            services.AddTransient<IUtilizadorRepository, UtilizadorRepository>();

            //PEDIDO LIGACAO
            services.AddTransient<PedidoLigacaoService>();
            services.AddTransient<IPedidoLigacaoRepository, PedidoLigacaoRepository>();

            //RELACAO
            services.AddTransient<RelacaoService>();
            services.AddTransient<IRelacaoRepository, RelacaoRepository>();

            //INTRODUCAO
            services.AddTransient<IntroducaoService>();
            services.AddTransient<IIntroducaoRepository, IntroducaoRepository>();

            //PEDIDO INTRODUCAO
            services.AddTransient<PedidoIntroducaoService>();
            services.AddTransient<IPedidoIntroducaoRepository, PedidoIntroducaoRepository>();

            //MISSAO
            services.AddTransient<MissaoService>();
            services.AddTransient<IMissaoRepository, MissaoRepository>();

            //REDE SOCIAL
            services.AddTransient<RedeSocialService>();
            
            //LEADERBOARD
            services.AddTransient<LeaderboardService>();

            //AUTENTICACAO
            services.AddTransient<AutenticacaoService>();
        }

        public void ConfigureCors(IServiceCollection services)

        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
            });
        }
    }
}