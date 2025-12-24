using Microsoft.EntityFrameworkCore;
using WebApiByJessica.Models;

namespace WebApiByJessica.Data
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options) { }

        public DbSet<Student> Students { get; set; }
    }
}
