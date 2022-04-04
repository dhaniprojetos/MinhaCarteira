using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio;

namespace MinhaCarteira.Servidor.Recursos.Helper
{
    public static class DadosMiddleware
    {
        public static IServiceCollection AdicionarDados(
            this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MinhaCarteiraContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped(typeof(RelatorioServico));
            services.AddScoped(typeof(RelatorioRepositorio));

            services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
            services.AddScoped<IPessoaServico, PessoaServico>();

            services.AddScoped<IInstituicaoFinanceiraRepositorio, InstituicaoFinanceiraRepositorio>();
            services.AddScoped<IInstituicaoFinanceiraServico, InstituicaoFinanceiraServico>();

            services.AddScoped<IContaBancariaRepositorio, ContaBancariaRepositorio>();
            services.AddScoped<IContaBancariaServico, ContaBancariaServico>();

            services.AddScoped<ICategoriaRepositorio, CategoriaRepositorio>();
            services.AddScoped<ICategoriaServico, CategoriaServico>();

            services.AddScoped<ICentroClassificacaoRepositorio, CentroClassificacaoRepositorio>();
            services.AddScoped<ICentroClassificacaoServico, CentroClassificacaoServico>();

            services.AddScoped<IMovimentoBancarioRepositorio, MovimentoBancarioRepositorio>();
            services.AddScoped<IMovimentoBancarioServico, MovimentoBancarioServico>();

            services.AddScoped<IAgendamentoRepositorio, AgendamentoRepositorio>();
            services.AddScoped<IAgendamentoServico, AgendamentoServico>();

            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            services.AddScoped<IUsuarioServico, UsuarioServico>();

            return services;
        }
    }
}
