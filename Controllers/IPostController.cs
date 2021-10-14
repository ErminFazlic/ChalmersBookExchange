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
        Post[] GetMyPosts(Guid userid);
        Post[] GetFavorites(string email);
        void AddFavoriteToDb(Guid id, string email);
        void RemoveFavoriteFromDb(Guid id, string email);
        bool IsAFavorite(Guid id, string email);
        Post[] GetQueriedPosts(string courseCode, string bookName, int minPrice, int maxPrice, bool shippable, bool meetUp);
        Post GetPostById(Guid id);
        Post[] GetAllPostsAlphabetical();
        Post[] GetAllPostsPriceAsc();
        Post[] GetAllPostsPriceDesc();
        Post[] GetAllPostsOldest();
        
    }
}