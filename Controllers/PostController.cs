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
        /// <summary>
        /// Creates a post in the DB from a Post instance
        /// </summary>
        /// <param name="post"></param>
        /// <returns>bool based on success</returns>
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
        /// <summary>
        /// Retrieves the post based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The post</returns>
        public Post GetPostById(Guid id)
        {
           return _context.Post.FirstOrDefault(x => x.ID == id);
        }
        /// <summary>
        /// Creates a post asynchronously in the DB from a Post instance
        /// </summary>
        /// <param name="post"></param>
        /// <returns>bool based on success</returns>
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
        /// <summary>
        /// Retrieves all posts from the DB
        /// </summary>
        /// <returns>Array of Posts</returns>
        public Post[] GetAllPosts()
        {
            var posts = _context.Post.ToArray();
            posts = ReversePosts(posts);
            return posts;
        }
        /// <summary>
        /// Retrieves all posts from the DB in alphabetical order
        /// </summary>
        /// <returns>Array of Posts</returns>
        public Post[] GetAllPostsAlphabetical()
        {
            
            var posts = _context.Post.OrderBy(p => p.BookName).ToArray();
            
            return posts;
        }
        /// <summary>
        /// Retrieves all posts from the DB in order by price ascending
        /// </summary>
        /// <returns>Array of Posts</returns>
        public Post[] GetAllPostsPriceAsc()
        {
            
            var posts = _context.Post.OrderBy(p => p.Price).ToArray();
            
            return posts;
        }
        /// <summary>
        /// Retrieves all posts from the DB in order by price descending
        /// </summary>
        /// <returns>Array of Posts</returns>
        public Post[] GetAllPostsPriceDesc()
        {
            
            var posts = _context.Post.OrderByDescending(p => p.Price).ToArray();
            
            return posts;
        } 
        /// <summary>
        /// Retrieves all posts from the DB in order of oldest first
        /// </summary>
        /// <returns>Array of Posts</returns>       
        public Post[] GetAllPostsOldest()
        {
            var posts = _context.Post.ToArray();
            return posts;
        }
        /// <summary>
        /// Helper method that reverses an array of posts
        /// </summary>
        /// <param name="posts"></param>
        /// <returns>Array of Posts</returns>
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

        public Post[] GetQueriedPosts(string courseCode, string bookName, int minPrice, int maxPrice, bool shippable, bool meetUp)
        {
            bookName ??= "afksnjasndfjasnfjkdankdfj";
            courseCode ??= "fnkasdjnasjkanjkfajk";
            
            var MaxPrice = 100000;
            if (maxPrice > minPrice) MaxPrice = maxPrice;

            var Shippable = true;
            var MeetUp = true;
            if (!shippable && meetUp) Shippable = false;
            if (!meetUp && shippable) MeetUp = true;

            var postsList = _context.Post.Where(x => x.CourseCode.ToUpper().Contains(courseCode.ToUpper()) || x.BookName.ToUpper().Contains(bookName.ToUpper())).ToList();
            
            postsList.RemoveAll(x => x.Price < minPrice);
            postsList.RemoveAll(x => x.Price > MaxPrice);
            postsList.RemoveAll(x => x.Shippable != Shippable && x.Meetup != MeetUp);
            var posts = postsList.ToArray();
            posts = ReversePosts(posts);
            return posts;
        }
        
    }
}