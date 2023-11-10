using Microsoft.EntityFrameworkCore;

namespace BabyNI.Models
{
    public class Context : DbContext
    {
        public DbSet<EntityModel>? Daily { get; set; }
        public DbSet<EntityModel>? Hourly { get; set; }

        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
    }
}