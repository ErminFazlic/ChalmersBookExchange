using System.Linq;
using System.Threading.Tasks;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChalmersBookExchange.Controllers
{
    public class PostController : IPostController
    {
        private readonly MyDbContext _context;

        public PostController(MyDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            var exists = await _context.Post.FirstOrDefaultAsync(x => x.ID == post.ID);

            if (exists is null)
            {
                _context.Post.Add(post);
                var created = await _context.SaveChangesAsync();
                return created > 0;
            }

            return false;
        }

        public async Task<Post[]> GetAllPostsAsync()
        {
            var posts = await _context.Post.ToArrayAsync();
            return posts;
        }
    }
}