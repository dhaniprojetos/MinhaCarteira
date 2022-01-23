using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Interface.Modelo;
using MinhaCarteira.Comum.Definicao.Interface.Servico;
using MinhaCarteira.Servidor.Controle.Servico;
using MinhaCarteira.Servidor.Modelo.Data;
using MinhaCarteira.Servidor.Modelo.Repositorio;
using MinhaCarteira.Teste.Mock.Faker;
using MinhaCarteira.Teste.Mock.Interface;

namespace MinhaCarteira.Teste.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MinhaCarteiraContext>(options =>
            {
                options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MinhaCarteiraDb;Integrated Security=true;");
            });

            services.AddScoped<IBuilder<Pessoa>, PessoaBuilder>();
            services.AddScoped<ICrud<Pessoa>, PessoaRepositorio>();
            services.AddScoped<IServicoCrud<Pessoa>, PessoaServico>();
        }
    }
}
