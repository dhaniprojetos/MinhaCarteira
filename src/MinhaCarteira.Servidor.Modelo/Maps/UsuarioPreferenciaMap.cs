using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class UsuarioPreferenciaMap : IEntityTypeConfiguration<UsuarioPreferencia>
    {
        private static IList<UsuarioPreferencia> ObterPreferenciasUsuarioInicial()
        {
            var usuariosPapeis = new List<UsuarioPreferencia> {
                new UsuarioPreferencia{
                    Id=1,
                    UsuarioId=1,
                    Nome = "remember.lte.pushmenu",
                    Valor = "sidebar-collapse"
                    //Valor = "sidebar-open"
                }
            };

            return usuariosPapeis;
        }
        public void Configure(EntityTypeBuilder<UsuarioPreferencia> builder)
        {
            builder.ToTable(nameof(UsuarioPreferencia));

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Nome).HasMaxLength(120);
            builder.Property(p => p.Valor).HasMaxLength(255);

            builder.HasOne(o => o.Usuario)
                .WithMany(w => w.Preferencias);

            builder.HasData(ObterPreferenciasUsuarioInicial());
        }
    }
}
