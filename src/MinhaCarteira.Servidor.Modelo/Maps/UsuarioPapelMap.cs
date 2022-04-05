using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;
using System.Collections.Generic;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class UsuarioPapelMap : IEntityTypeConfiguration<UsuarioPapel>
    {
        private static IList<UsuarioPapel> ObterUsuariosPapeisInicial()
        {
            var usuariosPapeis = new List<UsuarioPapel> {
                new UsuarioPapel{ Id=1, PapelId = 1, UsuarioId = 1 },
                new UsuarioPapel{ Id=2, PapelId = 2, UsuarioId = 1 }
            };

            return usuariosPapeis;
        }

        public void Configure(EntityTypeBuilder<UsuarioPapel> builder)
        {
            builder.ToTable(nameof(UsuarioPapel));

            builder.HasKey(k => k.Id);

            builder.HasOne(o => o.Usuario)
                .WithMany(w => w.Papeis);

            builder.HasOne(o => o.Papel)
                .WithMany(w => w.Usuarios);

            builder.HasData(ObterUsuariosPapeisInicial());
        }
    }
}
