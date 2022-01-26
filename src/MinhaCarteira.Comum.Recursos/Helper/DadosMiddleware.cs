using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Comum.Definicao.Interface.Teste;
using MinhaCarteira.Servidor.Controle.Servico;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio;
using MinhaCarteira.Teste.Mock.Faker;

namespace MinhaCarteira.Comum.Recursos.Helper
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
            services.AddScoped<IServicoCrud<ContaBancaria>, ContaBancariaServico>();

            services.AddScoped<ICrud<Categoria>, CategoriaRepositorio>();
            services.AddScoped<IServicoCrud<Categoria>, CategoriaServico>();

            services.AddScoped<ICrud<CentroClassificacao>, CentroClassificacaoRepositorio>();
            services.AddScoped<IServicoCrud<CentroClassificacao>, CentroClassificacaoServico>();

            services.AddScoped<ICrud<MovimentoBancario>, MovimentoBancarioRepositorio>();
            services.AddScoped<IServicoCrud<MovimentoBancario>, MovimentoBancarioServico>();

            return services;
        }

        public static IServiceCollection AdicionarTestes(
            this IServiceCollection services, string connectionString)
        {
            services.AdicionarDados(connectionString);

            services.AddScoped<IBuilder<Pessoa>, PessoaBuilder>();
            services.AddScoped<IBuilder<InstituicaoFinanceira>, InstituicaoFinanceiraBuilder>();
            services.AddScoped<IBuilder<ContaBancaria>, ContaBancariaBuilder>();
            services.AddScoped<IBuilder<Categoria>, CategoriaBuilder>();
            services.AddScoped<IBuilder<CentroClassificacao>, CentroClassificacaoBuilder>();
            services.AddScoped<IBuilder<MovimentoBancario>, MovimentoBancarioBuilder>();

            return services;
        }
    }
}
