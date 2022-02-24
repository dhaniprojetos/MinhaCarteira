using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class AgendamentoItemMap : IEntityTypeConfiguration<AgendamentoItem>
    {
        public void Configure(EntityTypeBuilder<AgendamentoItem> builder)
        {
            builder.ToTable(nameof(AgendamentoItem));
            builder.HasKey(k => k.Id);

            builder.Property(p => p.EstahPaga).HasDefaultValue(false);
            builder.Property(p => p.EstahConciliada).HasDefaultValue(false);
            builder.Property(p => p.Valor).HasPrecision(18, 6);
            builder.Property(p => p.ValorPago).HasPrecision(18, 6);
            builder.Property(p => p.Data).HasColumnType("datetime2").HasPrecision(0);
            builder.Property(p => p.DataPagamento).HasColumnType("datetime2").HasPrecision(0);

            builder.HasOne(o => o.Agendamento)
                .WithMany(m => m.Items)
                .HasForeignKey(f => f.AgendamentoId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Pessoa)
                .WithMany()
                .HasForeignKey(f => f.PessoaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.ContaBancaria)
                .WithMany()
                .HasForeignKey(f => f.ContaBancariaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(m => m.Movimentos)
                .WithOne(o => o.AgendamentoItem)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
