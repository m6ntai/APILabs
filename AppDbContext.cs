using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2

{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<Calculation> Calculations { get; set; }
    }
}
