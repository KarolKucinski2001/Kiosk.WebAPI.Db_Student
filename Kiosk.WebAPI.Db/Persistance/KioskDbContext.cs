using Microsoft.EntityFrameworkCore;
using Kiosk.WebAPI.Models;

namespace Kiosk.WebAPI.Persistance
{
    public class KioskDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public KioskDbContext(DbContextOptions<KioskDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
