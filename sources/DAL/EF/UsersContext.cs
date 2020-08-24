using Microsoft.EntityFrameworkCore;
using DAL.Entities;

namespace DAL.EF
{
    /// <summary>
    /// Контекст данных для работы с БД.
    /// </summary>
    public class UsersContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Report> Reports { get; set; }

        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
