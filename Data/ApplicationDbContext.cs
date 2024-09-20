using Microsoft.EntityFrameworkCore;
using Pager.Model.Entities;

namespace Pager.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Details> GetDetails{ get; set; }
    }
}