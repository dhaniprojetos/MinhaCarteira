using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class ExtratoDiarioMap : IEntityTypeConfiguration<ExtratoDiario>
    {
        public void Configure(EntityTypeBuilder<ExtratoDiario> builder)
        {
            builder.HasNoKey();
            //builder.HasNoKey().ToView(null);
            builder.Property(p => p.Valor).HasPrecision(18, 6);
            builder.Property(p => p.Saldo).HasPrecision(18, 6);
        }
    }
}
