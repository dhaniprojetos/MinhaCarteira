using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class UserIdentityInRolesMap : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = "fab4fac1-c546-41de-aebc-a14da6895711",
                    UserId = "7aede9ac-6431-40d2-9375-1f81bdbbb764"
                });
        }
    }
}
