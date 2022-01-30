using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Teste;
using MinhaCarteira.Servidor.Recursos.Helper;
using MinhaCarteira.Teste.Mock.Faker;

namespace MinhaCarteira.Teste.WebApi
{
    public class Startup
    {
#pragma warning disable CA1822 // Marcar membros como estáticos
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Marcar membros como estáticos
        {
            services.AdicionarDados("Server=(localdb)\\MSSQLLocalDB;Database=MinhaCarteiraDb;Integrated Security=true;");

            services.AddScoped<IBuilder<Pessoa>, PessoaBuilder>();
            services.AddScoped<IBuilder<InstituicaoFinanceira>, InstituicaoFinanceiraBuilder>();
            services.AddScoped<IBuilder<ContaBancaria>, ContaBancariaBuilder>();
            services.AddScoped<IBuilder<Categoria>, CategoriaBuilder>();
            services.AddScoped<IBuilder<CentroClassificacao>, CentroClassificacaoBuilder>();
            services.AddScoped<IBuilder<MovimentoBancario>, MovimentoBancarioBuilder>();
        }
    }
}
