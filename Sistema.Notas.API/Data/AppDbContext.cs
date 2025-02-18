using Microsoft.EntityFrameworkCore; 
using Sistema.Notas.API.Shared.Entities;

namespace Sistema.Notas.API.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<StudentCourse> StudenstCourses { get; set; }
        public DbSet<Grade> Grades { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasIndex(u => u.Email).IsUnique();

            modelBuilder.Entity<Grade>()
                .Property(c => c.Value)
                .HasPrecision(5, 2);
        }
    }
}
