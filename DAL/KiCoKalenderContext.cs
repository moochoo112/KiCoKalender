using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class KiCoKalenderContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public KiCoKalenderContext(DbContextOptions<KiCoKalenderContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=KiCoKalender;Trusted_Connection=True;MultipleActiveResultSets=true");
        }
    }

}

