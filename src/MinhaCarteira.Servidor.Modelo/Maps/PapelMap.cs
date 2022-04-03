using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;
using System.Collections.Generic;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class PapelMap : IEntityTypeConfiguration<Papel>
    {
        private static IList<Papel> ObterPapeisIniciais()
        {
            var papeis = new List<Papel>()
            {
                new Papel{ Id = 1, Nome = "Admin" },
                new Papel{ Id = 2, Nome = "SuperAdmin" }
            };

            return papeis;
        }

        public void Configure(EntityTypeBuilder<Papel> builder)
        {
            builder.ToTable(nameof(Papel));

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Nome).HasMaxLength(200);

            builder.Property(p => p.Id)
                .HasDefaultValueSql("NEXT VALUE FOR PapelIds");

            builder.HasData(ObterPapeisIniciais());
        }
    }
}
