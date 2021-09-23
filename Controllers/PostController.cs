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
            Post[] posts = _context.Post.Where(x => x.CourseCode == courseCode || x.BookName == bookName).ToArray();
            posts = ReversePosts(posts);
            return posts;
        }

       /* public Post GetQuieredPost(String courseCode)
        {
            var posts = _context.Post.ToArray();
            Post searchPost;
            //var currentpost = new Post[posts.Length];

            var j = 0;
            for (var i = posts.Length - 1; i > -1; i--)
            {
                if(searchPost.CourseCode == _courseCode)
                posts.courseCode= courseCode;
                j++;
            }

            return reversedPosts;
        }*/
        public Post[] GetQueriedPostCC(String coursec)
        {
            if (coursec is null)
            {
                GetQueriedPostBN(coursec);
            }

            
            var posts = _context.Post.ToArray();
            Post[] resultArray = new Post[posts.Length];
            int j = 0;
            for (var i = 0 ; i < posts.Length ; i++)
            {
               
                if (coursec.Equals(posts[i].CourseCode)) 
                {
                    resultArray[j] = posts[i];
                    j++;
                }
            }
            
            return resultArray;
        }

        public Post[] GetQueriedPostBN(String bookName)
        {
            if (bookName is null)
            {
                GetQueriedPostCC(bookName);
            }

            
            var posts = _context.Post.ToArray();
            Post[] resultArray = new Post[posts.Length];
            int j = 0;
            for (var i = 0 ; i < posts.Length ; i++)
            {
                if (bookName.Equals(posts[i].BookName))
                {
                    resultArray[j] = posts[i];
                    j++;
                }
            }

            return resultArray;
        }

    }
}