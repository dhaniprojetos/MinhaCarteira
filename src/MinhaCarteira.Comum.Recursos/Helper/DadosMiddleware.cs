using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio;

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

            return services;
        }
    }
}
