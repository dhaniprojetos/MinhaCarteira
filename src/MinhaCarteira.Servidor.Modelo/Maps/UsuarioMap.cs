using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MinhaCarteira.Comum.Definicao.Entidade;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        private static Usuario ObterUsuarioInicial()
        {
            var usuario = "admin";
            var email = "administrador@dhaniprojetos.com";
            //var passwordHasher = new PasswordHasher<Usuario>();
            var user = new Usuario
            {
                Id = 1,
                Nome = "Administrador",
                Sobrenome = "do Sistema",
                Username = usuario,
                Email = email,
                Senha = "admin"
                //SenhaHash = passwordHasher.HashPassword(null, "admin"),

                //NormalizedUserName = usuario.ToUpper(),
                //NormalizedEmail = email.ToUpper(),
                //LockoutEnabled = false,
                //EmailConfirmed = true,
                //PhoneNumberConfirmed = true,
                //SecurityStamp = string.Empty
            };

            return user;
        }

        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable(nameof(Usuario));

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Nome).HasMaxLength(200);
            builder.Property(p => p.Sobrenome).HasMaxLength(200);
            builder.Property(p => p.Username).HasMaxLength(200);
            builder.Property(p => p.Senha).HasMaxLength(200);
            builder.Property(p => p.Email).HasMaxLength(200);

            builder.HasData(ObterUsuarioInicial());
        }
    }
}
