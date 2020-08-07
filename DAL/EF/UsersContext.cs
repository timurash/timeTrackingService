using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.EF
{
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }
        
        public UsersContext()
        {
            Database.EnsureCreated();
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host = localhost; Port = 5432; Database = TimeTrackingServiceDB; Username = postgres; Password = 1234");
        }
    }
}
