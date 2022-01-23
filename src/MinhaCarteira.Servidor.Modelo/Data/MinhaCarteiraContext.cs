using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Servidor.Modelo.Maps;

namespace MinhaCarteira.Servidor.Modelo.Data
{
    public class MinhaCarteiraContext : DbContext
    {
        public MinhaCarteiraContext(
            DbContextOptions<MinhaCarteiraContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PessoaMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pessoa> Pessoas { get; set; }
    }
}
