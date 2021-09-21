using ChalmersBookExchange.Data;

namespace ChalmersBookExchange.Controllers
{
    public class PostController
    {
        private readonly MyDbContext _context;

        public PostController(MyDbContext context)
        {
            _context = context;
        }
    }
}