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
            modelBuilder.ApplyConfiguration(new InstituicaoFinanceiraMap());
            modelBuilder.ApplyConfiguration(new ContaBancariaMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<InstituicaoFinanceira> InstituicaoFinanceira { get; set; }
        public DbSet<ContaBancaria> ContaBancaria { get; set; }
    }
}
