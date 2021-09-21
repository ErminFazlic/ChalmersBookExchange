using System.Threading.Tasks;
using ChalmersBookExchange.Domain;

namespace ChalmersBookExchange.Controllers
{
    public interface IPostController
    {
        Task<bool> CreatePostAsync(Post post);
        Task<Post[]> GetAllPostsAsync();
        bool CreatePost(Post post);
    }
}