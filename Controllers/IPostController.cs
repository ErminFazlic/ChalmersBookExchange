using System;
using System.Threading.Tasks;
using ChalmersBookExchange.Domain;

namespace ChalmersBookExchange.Controllers
{
    public interface IPostController
    {
        Task<bool> CreatePostAsync(Post post);
        Post[] GetAllPosts();
        Post[] GetAllPostsAlphabetical();
        Post[] GetAllPostsPriceAsc();
        Post[] GetAllPostsPriceDesc();
        Post[] GetAllPostsOldest();
        bool CreatePost(Post post);
        Post GetPostById(Guid id);
        Post[] GetQueriedPosts(string courseCode, string bookName, int minPrice, int maxPrice, bool shippable, bool meetUp);
        Post[] GetAllPostsByUserId(Guid id);
        void DeletePost(Guid id);
    }
}