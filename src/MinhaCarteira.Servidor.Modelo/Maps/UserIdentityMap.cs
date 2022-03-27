using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class UserIdentityMap : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            var usuario = "admin";
            var email = "administrador@dhaniprojetos.com";
            var passwordHasher = new PasswordHasher<IdentityUser>();
            var user = new IdentityUser
            {
                Id = "7aede9ac-6431-40d2-9375-1f81bdbbb764",
                UserName = usuario,
                NormalizedUserName = usuario.ToUpper(),
                Email = email,
                NormalizedEmail = email.ToUpper(),
                LockoutEnabled = false,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PasswordHash = passwordHasher.HashPassword(null, "admin"),
                SecurityStamp = string.Empty
            };

            builder.HasData(user);
        }
    }
}
