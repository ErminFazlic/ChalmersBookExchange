using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Net;
using System.Security.Claims;

using System.Threading.Tasks;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using ChalmersBookExchange.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore.Internal;
using Npgsql.Replication;
using Index = System.Index;


namespace ChalmersBookExchange.Controllers
{
    public class PostController : IPostController
    {
        private readonly MyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;



        public PostController(MyDbContext context, UserManager<IdentityUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }



        public bool CreatePost(Post post)
        {
            var exists = _context.Post.FirstOrDefault(x => x.ID == post.ID);

            if (exists is null)
            {
                _context.Post.Add(post);
                var created = _context.SaveChanges();
                return created > 0;
            }

            return false;
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

        public Post[] GetAllPosts()
        {
            var posts = _context.Post.ToArray();
            posts = ReversePosts(posts);
            return posts;
        }

        private Post[] ReversePosts(Post[] posts)
        {
            Post[] reversedPosts = new Post[posts.Length];
            var j = 0;
            for (var i = posts.Length - 1; i > -1; i--)
            {
                reversedPosts[i] = posts[j];
                j++;
            }
            return reversedPosts;
        }
        /// <summary>
        /// This method finds all posts which have the same course code or book name as it's applied
        /// </summary>
        /// <param name="courseCode"></param>
        /// <param name="bookName"></param>
        /// <returns>an array with requested posts</returns>
        public Post[] GetQueriedPosts(string courseCode, string bookName)
        {
            Post[] posts = _context.Post.Where(x => x.CourseCode == courseCode || x.BookName == bookName).ToArray();
            posts = ReversePosts(posts);
            return posts;
        }

        /// <summary>
        /// This method finds all posts created by a user with a specific id 
        /// </summary>
        /// <param name="userid"></param>
        /// <returns>posts created by a user with a specific id</returns>
        public Post[] GetMyPosts(Guid userid)
        {
            var posts = _context.Post.Where(x => x.Poster == userid).ToArray();
            posts = ReversePosts(posts);
            return posts;
        }
        
    }
}


      

