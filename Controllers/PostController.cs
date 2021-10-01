using System;
using System.Linq;
using System.Threading.Tasks;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChalmersBookExchange.Controllers
{
    public class PostController : IPostController
    {
        private readonly MyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public PostController(MyDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public bool CreatePost(Post post)
        {
            var exists =  _context.Post.FirstOrDefault(x => x.ID == post.ID);

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

        public Post[] GetAllPostsAlphabetical()
        {
            //TODO Get all posts and sort them alphabetically
            return GetQueriedPosts("Math", "Math");
        }

        public Post[] GetAllPostsPriceAsc()
        {
            //TODO Get all posts and sort them by price ascending
            return GetAllPosts();
        }

        public Post[] GetAllPostsPriceDesc()
        {
            //TODO Get all posts and sort them by price descending
            return GetAllPosts();
        }
        
        public Post[] GetAllPostsOldest()
        {
            var posts = _context.Post.ToArray();
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

        public Post[] GetQueriedPosts(string courseCode, string bookName)
        {
            bookName ??= "afksnjasndfjasnfjkdankdfj";
            courseCode ??= "fnkasdjnasjkanjkfajk";
            var posts = _context.Post.Where(x => x.CourseCode.ToUpper().Contains(courseCode.ToUpper())  || x.BookName.ToUpper().Contains(bookName.ToUpper())).ToArray();
            posts = ReversePosts(posts);
            return posts;
        }

    }
}