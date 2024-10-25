using Microsoft.EntityFrameworkCore;
using NastyaKupcovakt_42_21.Configurations;
using NastyaKupcovakt_42_21.Models;

namespace NastyaKupcovakt_42_21.Database
{
    public class StudentDbContext : DbContext
    {
       

        public DbSet<Student> Students { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new GroupConfiguration());
            modelBuilder.ApplyConfiguration(new SubjectConfiguration());
        }
        public StudentDbContext(DbContextOptions<StudentDbContext> options)
           : base(options)
        {
        }
    }
}
