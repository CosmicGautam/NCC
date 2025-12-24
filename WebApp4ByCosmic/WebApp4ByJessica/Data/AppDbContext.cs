using Microsoft.EntityFrameworkCore;
using WebApp4ByJessica.Models;

namespace WebApp4ByJessica.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
