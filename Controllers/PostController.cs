using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace ChalmersBookExchange.Controllers
{
    public class PostController : IPostController
    {
        private readonly MyDbContext _context;

        public PostController(MyDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Creates a post in the DB from a Post instance
        /// </summary>
        /// <param name="post"></param>
        /// <returns>bool based on success</returns>
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
        
        /// <summary>
        /// This method finds all posts which have the same course code or book name as it's applied
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="courseCode"></param>
        /// <param name="bookName"></param>
        /// <returns>an array with requested posts</returns>
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
        
        /// <summary>
        /// This method finds the favorite posts belong to specific user given the user's email
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="email"></param>
        /// <returns> post array contains the favorite posts</returns>
        public Post[] GetFavorites(string email)
        {
            var user = _context.User.SingleOrDefault(u => u.Email == email);
            var posts = _context.Post.Where(p => user.Favorites.Contains(p.ID)).ToArray();

            return posts;
        }
        
        /// <summary>
        /// This method checks if the post (given its ID and the user it belongs to)
        /// is a favorite post or not
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns> True if the post is favorite otherwise false</returns>
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
        
        /// <summary>
        /// This method add the favorite post to the database.
        /// Checks for duplicate and if the post belongs to this user.
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <param name="email"></param>
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
            else if(!thisPost.Poster.Equals(thisUser.ID))        // Can't add your own post to favorite
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
        
        /// <summary>
        /// This method remove the favorite post from the database.
        /// It checks that the post and the user is not null.
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <param name="email"></param>
        
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
        /// <summary>
        /// This method will remove the current image from the post, from the database.
        /// It checks that the post and the user is not null
        /// </summary>
        /// <authors> Negin, Sven</authors>
        /// <param name="guid"></param>
        public void DeleteImage(Guid? guid)
        {
            if (guid is null) return;
            var post = _context.Post.FirstOrDefault(p => p.ID.Equals(guid));
            if (post is null) return;
            
            post.Images = null;
            _context.SaveChanges();
      
        }
        /// <summary>
        /// Retrieves the bytearray from the database and converts it into an image.
        /// It checks that the image is not null.
        /// </summary>
        /// <authors> Negin, Sven</authors>
        /// <param name="guid"></param>
        /// <returns>Returns a string that represent the image</returns>
        public String RetrieveImage(Guid guid)
        {
            var post = _context.Post.SingleOrDefault(p => p.ID.Equals(guid));
            byte[] img = post.Images;
            String imageDataURL = String.Empty;
            
            if (img != null)
            {
                string imageBase64Data = Convert.ToBase64String(img);
                imageDataURL = string.Format("data:image/jpg;base64,{0}", imageBase64Data);
               
            }

            return imageDataURL;
        }
        /// <summary>
        /// Converts an image file to a bytearray and saves it to database.
        /// Checks if the bytearray is not null ny checking the length of the bytearray.
        /// </summary>
        /// <authors> Negin, Sven</authors>
        /// <param name="Images"></param>
        /// <param name="post"></param>
        public async void ImageToByteArray(List<IFormFile> Images, Post post)
        {
            foreach (var item in Images)
            {
                if (item.Length > 0)
                {
                    using (var stream = new MemoryStream())
                    {
                        await item.CopyToAsync(stream);
                        post.Images = stream.ToArray();
                        
                    }
                }
            }

        }
        
    }
}