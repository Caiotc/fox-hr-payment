using fox_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fox_web_api
{
    public class AppDbContext:DbContext
    {
        public DbSet<User> Users { get; set; } = default!;
        public DbSet<Role> Roles { get; set; } = default!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }




    }
}
