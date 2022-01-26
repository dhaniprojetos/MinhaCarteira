using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Recursos.Helper;

namespace MinhaCarteira.Teste.WebApi
{
    public class Startup
    {
#pragma warning disable CA1822 // Marcar membros como estáticos
        public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822 // Marcar membros como estáticos
        {
            services.AdicionarTestes("Server=(localdb)\\MSSQLLocalDB;Database=MinhaCarteiraDb;Integrated Security=true;");
        }
    }
}
