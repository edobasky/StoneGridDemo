using Microsoft.EntityFrameworkCore;
using StoneGridDemo.Models;

namespace StoneGridDemo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }

      //  public DbSet<App> Apps { get; set; }

    }
}
