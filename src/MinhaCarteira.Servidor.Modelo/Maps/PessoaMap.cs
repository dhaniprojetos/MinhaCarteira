using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class PessoaMap : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.ToTable(nameof(Pessoa));

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Nome).HasMaxLength(200);

            //builder.HasOne(x => x.Categoria)
            //    .WithMany()
            //    .HasForeignKey(x => x.IdCategoria);
        }
    }
}
