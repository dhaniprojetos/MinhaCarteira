using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class InstituicaoFinanceiraMap : IEntityTypeConfiguration<InstituicaoFinanceira>
    {
        public void Configure(EntityTypeBuilder<InstituicaoFinanceira> builder)
        {
            builder.ToTable("InstituicaoFinanceira");

            builder.HasKey(x => x.Id);
            builder.Property(p => p.Nome).HasMaxLength(200);
        }
    }
}
