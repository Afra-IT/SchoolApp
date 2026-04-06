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
        public DbSet<Lesson> Lessons { get; set; } = default!;
        public DbSet<Teacher> Teachers { get; set; } = default!;
    }
}
