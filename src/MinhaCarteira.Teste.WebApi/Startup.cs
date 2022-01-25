using Microsoft.Extensions.DependencyInjection;
using MinhaCarteira.Comum.Recursos.Helper;

namespace MinhaCarteira.Teste.WebApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AdicionarTestes("Server=(localdb)\\MSSQLLocalDB;Database=MinhaCarteiraDb;Integrated Security=true;");
        }
    }
}
