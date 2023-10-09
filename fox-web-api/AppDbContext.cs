using fox_web_api.Models;
using Microsoft.EntityFrameworkCore;

namespace fox_web_api
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<User> Users { get; set; } = default!;
    }
}
