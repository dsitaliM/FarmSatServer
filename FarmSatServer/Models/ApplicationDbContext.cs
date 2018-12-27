using Microsoft.EntityFrameworkCore;

namespace FarmSatServer.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<SatelliteImage> SatelliteImages { get; set; }
    }
}