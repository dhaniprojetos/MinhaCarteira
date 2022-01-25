using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Recursos.Helper;
using MinhaCarteira.Teste.Mock.Faker;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AdicionarDados("Server=(localdb)\\MSSQLLocalDB;Database=MinhaCarteiraDb;Integrated Security=true;");

            services.AddScoped<IBuilder<Pessoa>, PessoaBuilder>();
            services.AddScoped<IBuilder<InstituicaoFinanceira>, InstituicaoFinanceiraBuilder>();
            services.AddScoped<IBuilder<ContaBancaria>, ContaBancariaBuilder>();
        }
    }
}
