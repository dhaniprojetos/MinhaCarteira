using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MinhaCarteira.Servidor.Modelo.Maps
{
    public class UserIdentityRolesMap : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole()
                {
                    Id = "fab4fac1-c546-41de-aebc-a14da6895711",
                    Name = "Admin",
                    ConcurrencyStamp = "1",
                    NormalizedName = "Admin"
                });
        }
    }
}
