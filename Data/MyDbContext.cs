using ChalmersBookExchange.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChalmersBookExchange.Data
{
    public class MyDbContext : ApplicationDbContext
    {
        public MyDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<User> User { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
    }
}