using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class ContaBancariaMap : IEntityTypeConfiguration<ContaBancaria>
    {
        public void Configure(EntityTypeBuilder<ContaBancaria> builder)
        {
            builder.ToTable("ContaBancaria");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Nome).HasMaxLength(200);
            builder.Property(p => p.Agencia).HasMaxLength(50);
            builder.Property(p => p.Conta).HasMaxLength(50);

            builder
                .HasOne(h => h.InstituicaoFinanceira)
                .WithMany()
                .HasForeignKey(f => f.IdInstituicaoFinanceira);
        }
    }
}
