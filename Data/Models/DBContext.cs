using Microsoft.EntityFrameworkCore;

namespace BabyAPI.Data.Models
{
    public class BabyContext : DbContext
    {
        public DbSet<BabyModel> Daily { get; set; }
        public DbSet<BabyModel> Hourly { get; set; }

        public BabyContext(DbContextOptions<BabyContext> options) : base(options)
        {

        }
    }
}