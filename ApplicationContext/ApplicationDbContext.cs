using EFCoreConnectedDisconnectedDemo.Model;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConnectedDisconnectedDemo.ApplicationContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
    }
}