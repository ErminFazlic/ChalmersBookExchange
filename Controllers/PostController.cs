using System;
using System.Collections;
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
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
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
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="userid"></param>
        /// <returns>posts created by a user with a specific id</returns>
        public Post[] GetMyPosts(Guid userid)
        {
            var posts = _context.Post.Where(x => x.Poster == userid).ToArray();
            posts = ReversePosts(posts);
            return posts;
        }

        public Post[] GetFavorites(string email)
        {
            var user = _context.User.SingleOrDefault(u => u.Email == email);
            var posts = _context.Post.Where(p => user.Favorites.Contains(p.ID)).ToArray();

            return posts;
        }

        public bool IsAFavorite(Guid id, string email)
        {
            var thisPost = _context.Post.FirstOrDefault(p => p.ID == id);
            var thisUser = _context.User.SingleOrDefault(u => u.Email == email);
            if (thisPost is not null || thisUser is not null)
                if (thisUser?.Favorites == null)
                {
                    return false;
                }
                else
                {
                    foreach (var favorite in thisUser.Favorites)
                    {
                        if (favorite.Equals(thisPost?.ID))
                        {
                                return true;
                        }
                        
                    }
                }
            return false;
        }


        public void AddFavoriteToDb(Guid id, string email)
        {
            var thisPost = _context.Post.FirstOrDefault(p => p.ID == id);
            var thisUser = _context.User.SingleOrDefault(u => u.Email == email);
            
            if (thisPost is null || thisUser is null) return;
            
            if (thisUser.Favorites == null && !thisPost.Poster.Equals(thisUser.ID))
            {
                    var newArr = new Guid?[1];
                    newArr[0] = thisPost.ID;
                    thisUser.Favorites = newArr;
                    _context.SaveChanges();
            }
            else if(!thisPost.Poster.Equals(thisUser.ID))        // Can't add you own post to favorite
            {
                var guidlist = new ArrayList(thisUser.Favorites);
                if (!guidlist.Contains(thisPost.ID))              // Don't add duplicates
                {
                    guidlist.Add(thisPost.ID);
                    var newGuidArray = new Guid?[guidlist.Count];
                    for (int i = 0; i < newGuidArray.Length; i++)
                    {
                        newGuidArray[i] = (Guid?) guidlist[i];
                    }
                    thisUser.Favorites = newGuidArray;
                }
                _context.SaveChanges();
            }
        }

        public void RemoveFavoriteFromDb(Guid id, string email)
        {
            var thisPost = _context.Post.FirstOrDefault(p => p.ID == id);
            var thisUser = _context.User.SingleOrDefault(u => u.Email == email);
            
            if (thisPost is null || thisUser is null) return;
            
            var guidlist = new ArrayList(thisUser.Favorites);
                guidlist.Remove(thisPost.ID);
                var newGuidArray = new Guid?[guidlist.Count];
                for (int i = 0; i < newGuidArray.Length; i++)
                {
                    newGuidArray[i] = (Guid?) guidlist[i];
                }
                thisUser.Favorites = newGuidArray;
                _context.SaveChanges();
        }
    }
}


      

