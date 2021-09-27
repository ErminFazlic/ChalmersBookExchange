using System;
using System.Threading.Tasks;
using ChalmersBookExchange.Domain;

namespace ChalmersBookExchange.Controllers
{
    public interface IPostController
    {
        Task<bool> CreatePostAsync(Post post);
        Post[] GetAllPosts();
        bool CreatePost(Post post);
        Post[] GetQueriedPosts(string courseCode, string bookName);
    }
}