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

        public UsersContext(DbContextOptions<UsersContext> options) : base(options) { }
    }
}
