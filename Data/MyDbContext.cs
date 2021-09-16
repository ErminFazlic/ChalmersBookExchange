using ChalmersBookExchange.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChalmersBookExchange.Data
{
    public class MyDbContext : ApplicationDbContext
    {
        public MyDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public Microsoft.EntityFrameworkCore.DbSet<User> User { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Post> Post { get; set; }
        public Microsoft.EntityFrameworkCore.DbSet<Conversation> Conversation { get; set; }
    }
}