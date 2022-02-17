using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class MovimentoBancarioMap : IEntityTypeConfiguration<MovimentoBancario>
    {
        public void Configure(EntityTypeBuilder<MovimentoBancario> builder)
        {
            builder.ToTable(nameof(MovimentoBancario));
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Descricao).HasMaxLength(200);
            builder.Property(p => p.Valor).HasPrecision(18, 6);

            builder.HasOne(o => o.Pessoa)
                .WithMany()
                .HasForeignKey(f => f.PessoaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.Categoria)
                .WithMany()
                .HasForeignKey(f => f.CategoriaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.CentroClassificacao)
                .WithMany()
                .HasForeignKey(f => f.CentroClassificacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.ContaBancaria)
                .WithMany()
                .HasForeignKey(f => f.ContaBancariaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.AgendamentoItem)
                .WithMany(m => m.Movimentos)
                .HasForeignKey(f => f.AgendamentoItemId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
