using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            // Retrieve connection
            var Connection = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json").Build().GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(Connection);
        }
    }
}
