using System;
using System.Threading.Tasks;
using ChalmersBookExchange.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ChalmersBookExchange.Controllers
{
    public interface IPostController
    {
        Task<bool> CreatePostAsync(Post post);
        Post[] GetAllPosts();
        bool CreatePost(Post post);
        Post[] GetQueriedPosts(string courseCode, string bookName);
        Post[] GetMyPosts(Guid userid);
       
    }
}