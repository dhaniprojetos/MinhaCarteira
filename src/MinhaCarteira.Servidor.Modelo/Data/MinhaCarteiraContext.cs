using Microsoft.EntityFrameworkCore;
using MinhaCarteira.Comum.Definicao.Entidade;
using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;
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
            modelBuilder.Entity<ExtratoDiario>().HasNoKey();
            modelBuilder.Entity<ExtratoMensal>().HasNoKey();

            modelBuilder.ApplyConfiguration(new PessoaMap());
            modelBuilder.ApplyConfiguration(new InstituicaoFinanceiraMap());
            modelBuilder.ApplyConfiguration(new ContaBancariaMap());
            modelBuilder.ApplyConfiguration(new CategoriaMap());
            modelBuilder.ApplyConfiguration(new CentroClassificacaoMap());
            modelBuilder.ApplyConfiguration(new MovimentoBancarioMap());
            modelBuilder.ApplyConfiguration(new AgendamentoMap());
            modelBuilder.ApplyConfiguration(new AgendamentoItemMap());

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Pessoa> Pessoas { get; set; }
        public DbSet<InstituicaoFinanceira> InstituicoesFinanceira { get; set; }
        public DbSet<ContaBancaria> ContasBancaria { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<CentroClassificacao> CentrosClassificacao { get; set; }
        public DbSet<MovimentoBancario> MovimentosBancarios { get; set; }
        public DbSet<Agendamento> Agendamentos { get; set; }
        public DbSet<AgendamentoItem> AgendamentoItens { get; set; }

        public DbSet<ExtratoDiario> ExtratoDiario { get; set; }
        public DbSet<ExtratoMensal> ExtratoMensal { get; set; }
    }
}
