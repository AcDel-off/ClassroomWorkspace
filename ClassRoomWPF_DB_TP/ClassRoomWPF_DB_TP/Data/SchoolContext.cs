using ClassRoomWPF_DB_TP.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassRoomWPF_DB_TP.Data
{
    public class SchoolContext : DbContext
    {
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Student> Students => Set<Student>();
        public DbSet<Teacher> Teachers => Set<Teacher>();
        public DbSet<Administrator> Administrators => Set<Administrator>();
        public DbSet<Subject> Subjects => Set<Subject>();
        public DbSet<Grade> Grades => Set<Grade>();

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseNpgsql("Host=localhost;Port=2510;Database=SchoolDatabase;Username=postgres;Password=12510");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasDiscriminator<string>("Role")
                .HasValue<Student>("Student")
                .HasValue<Teacher>("Teacher")
                .HasValue<Administrator>("Administrator");
        }
    }
}
