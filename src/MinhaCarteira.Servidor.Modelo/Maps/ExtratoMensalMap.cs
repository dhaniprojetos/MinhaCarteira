using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade.Relatorio;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class ExtratoMensalMap : IEntityTypeConfiguration<ExtratoMensal>
    {
        public void Configure(EntityTypeBuilder<ExtratoMensal> builder)
        {
            //builder.HasNoKey();
            builder.HasNoKey().ToView("#extratoMensal");

            builder.Property(p => p.Valor).HasPrecision(18, 6);
            builder.Property(p => p.Saldo).HasPrecision(18, 6);
        }
    }
}
