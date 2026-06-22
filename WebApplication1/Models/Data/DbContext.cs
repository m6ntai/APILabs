using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data  // ← важно: WebApplication1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Calculation> Calculations { get; set; }
    }
}