using Microsoft.EntityFrameworkCore;
using Sepideh1.Models;

namespace Sepideh1.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = default!;
    }
}
