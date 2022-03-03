using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Filtro;
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

            services.AddScoped<ICrud<Pessoa>, PessoaRepositorio>();
            services.AddScoped<IServicoCrud<Pessoa>, PessoaServico>();

            services.AddScoped<ICrud<InstituicaoFinanceira>, InstituicaoFinanceiraRepositorio>();
            services.AddScoped<IServicoCrud<InstituicaoFinanceira>, InstituicaoFinanceiraServico>();

            services.AddScoped<ICrud<ContaBancaria>, ContaBancariaRepositorio>();
            services.AddScoped<IContaBancariaServico, ContaBancariaServico>();

            services.AddScoped<ICrud<Categoria>, CategoriaRepositorio>();
            services.AddScoped<IServicoCrud<Categoria>, CategoriaServico>();

            services.AddScoped<ICrud<CentroClassificacao>, CentroClassificacaoRepositorio>();
            services.AddScoped<IServicoCrud<CentroClassificacao>, CentroClassificacaoServico>();

            services.AddScoped<ICrud<MovimentoBancario>, MovimentoBancarioRepositorio>();
            services.AddScoped<IMovimentoBancarioServico, MovimentoBancarioServico>();

            services.AddScoped<ICrud<Agendamento>, AgendamentoRepositorio>();
            services.AddScoped<IAgendamentoServico, AgendamentoServico>();

            return services;
        }
    }
}
