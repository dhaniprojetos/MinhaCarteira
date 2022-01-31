using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;
using System;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class AgendamentoMap : IEntityTypeConfiguration<Agendamento>
    {
        public void Configure(EntityTypeBuilder<Agendamento> builder)
        {
            builder.ToTable(nameof(Agendamento));
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Descricao).HasMaxLength(200);
            builder.Property(p => p.Valor).HasPrecision(18,6);

            builder
                .HasMany(m => m.Items)
                .WithOne(o => o.Agendamento)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(o => o.Categoria)
                .WithMany()
                .HasForeignKey(f => f.CategoriaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(o => o.CentroClassificacao)
                .WithMany()
                .HasForeignKey(f => f.CentroClassificacaoId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(o => o.Pessoa)
                .WithMany()
                .HasForeignKey(f => f.PessoaId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(o => o.ContaBancaria)
                .WithMany()
                .HasForeignKey(f => f.ContaBancariaId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
