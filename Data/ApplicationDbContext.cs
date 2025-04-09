using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pager.Model.Domain;
using Pager.Model.Entities;

namespace Pager.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Details> GetDetails{ get; set; }
        public DbSet<Region> Regions { get; set; }  


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Seed data for Details domain model

            var details = new List<Details>()
            {
                new Details()
                {
                    Id= Guid.Parse("7ea29bba-45c9-4ddf-888d-71ce369e87ab"),
                   Name="Kamaljones",
                    Age = 24,
                    Salary = 1200,
                    Email= "Kamalajones@gmail.com"
                }

            };
            // Seed Details to the database
               modelBuilder.Entity<Details>().HasData(details);

            //seed data for Rgion Domain Model
            var regions = new List<Region>()
            {
                new Region()
                {
                    Id= Guid.Parse("91fe5c8d-deb1-4403-a9e1-f0017b933609"),
                    Name= "Alfred Bonkey",
                    Email= "AlfredBonkey@gmail.com",
                    Location = "Ohio",
                    Phone= "9378456112",

                }
            };

            //seed Region to the datbase
               modelBuilder.Entity<Region>().HasData(regions);  
          


        }

           




            
    }
}