using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class CategoriaMap : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categoria");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome).HasMaxLength(200);

            builder
                .HasOne(s => s.CategoriaPai)
                .WithMany(m => m.SubCategoria)
                .HasForeignKey(f => f.IdCategoriaPai);
        }
    }
}
