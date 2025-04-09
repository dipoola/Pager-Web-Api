using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Pager.Data
{
    public class AuthConnectionDbContext : IdentityDbContext<IdentityUser>
    {
        public AuthConnectionDbContext(DbContextOptions<AuthConnectionDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var readerRoleId = "19414b0a-2cf3-4e59-9778-140e04d19cbb";
            var WriterRoleId = "6ed031b6-f639-495d-b89c-fe66bbf9ff3d";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp= readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper(),    
                },


                new IdentityRole
                {
                    Id = WriterRoleId,
                    ConcurrencyStamp= WriterRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper(),
                },

            };

            builder.Entity<IdentityRole>().HasData(roles);
        }

    }
}
