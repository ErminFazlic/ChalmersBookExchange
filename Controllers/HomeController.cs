using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChalmersBookExchange.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ChalmersBookExchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPostController _postController;
        private readonly IUserController _userController;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyDbContext _context;

        public HomeController(IPostController postController, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IUserController userController, MyDbContext context)
        {
            _postController = postController;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userController = userController;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreatePost()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost([Bind("BookName,CourseCode,Description,Price,Location,Shipment,Meetup")] Post post, List<IFormFile> Images)
        {
            if (ModelState.IsValid)
            {
                _postController.ByteArrayToImage(Images, post);
                
                post.Timestamp = DateTime.Now.ToString();
                post.ID = System.Guid.NewGuid();
                var userID = _userController.RetrieveID(User.Identity.Name);
                post.Poster = userID;
                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyPosts");
            }
            ViewBag.Message = post.ID.ToString();
            return View("MyPosts");
        }
        
        public IActionResult Posts()
        {
            ViewBag.Title = "Browse Posts";
            ViewBag.ThisUser = User.Identity.Name;
            return View("Posts");
        }

        public IActionResult Chat()
        {
            return View("Chat");
        }
        public IActionResult Search()
        {
            ViewBag.Title = "Search";
            return View();
        }
        public IActionResult MyPosts()
        {
            ViewBag.Title = "MyPosts";
            return View("MyPosts");
        }
        
        public IActionResult Favorites()
        {
            ViewBag.Title = "Favorites";
            ViewBag.ThisUser = User.Identity.Name;

          return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        
        /// <summary>
        /// This method gets called when submitting the create post form and uses the values to create a post then uses post controller to save it to DB.
        /// </summary>
        /// <param name="BookName"></param>
        /// <param name="CourseCode"></param>
        /// <param name="Description"></param>
        /// <param name="Price"></param>
        /// <param name="Location"></param>
        /// <param name="Shippable"></param>
        /// <param name="Meetup"></param>
        /// <returns>Redirection to the page for the post</returns>
        [HttpPost]
        public ActionResult CreatePostForm(string BookName, string CourseCode, string Description, int Price, string Location, string Shippable, string Meetup)
        {
            var email = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var ship = true;
            var meet = true;
            byte[] images = null; 
            if (Shippable is null && Meetup is not null)
            {
                ship = false;
            }
            if (Meetup is null && Shippable is not null)
            {
                meet = false;
            }
            var post = new Post
            {
                ID = System.Guid.NewGuid(),
                Poster = _userController.RetrieveID(email),
                BookName = BookName,
                CourseCode = CourseCode,
                Description = Description,
                Price = Price,
                Location = Location,
                Timestamp = DateTime.Now.ToString(),
                Images = images,
                Shippable = ship,
                Meetup = meet
            };
            var created = _postController.CreatePost(post);
            ViewBag.Message = post.ID.ToString();
            return View("Post");

        }
        /// <summary>
        /// this is the search action method which activates by search button.
        /// it calls GetQueriedPosts method to find the desired posts 
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="BookName"></param>
        /// <param name="CourseCode"></param>
        /// <returns>the generated view with the desired posts</returns>
        [HttpPost]
        public ActionResult SearchPost(string BookName, string CourseCode, int MinPrice, int MaxPrice, string Shippable, string Meetup)
        {
            var ship = true;
            var meet = true;
            if (Shippable is null) ship = false;
            if (Meetup is null) meet = false;
            
            var searchedList = _postController.GetQueriedPosts(CourseCode, BookName, MinPrice, MaxPrice, ship, meet);
            ViewBag.Message = searchedList;
            ViewBag.Title = "Results";
            ViewBag.ThisUser = User.Identity.Name;
            return View("QueriedPosts");
        }
        
        /// <summary>
        /// This method check if the given id is not null in purpose to be able to
        /// find it in the database
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <returns>the generated view with the specific post</returns>
        public async Task<IActionResult>Delete (Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _context.Post.FindAsync(id);
            
            return View(post);
        }
        /// <summary>
        /// This is an action delete method which delete a post given it's id.
        /// It find the post given it's id, removes it and finally saves the changes
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <returns>Redirection to My post page</returns>
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyPosts");
        }
        /// <summary>
        /// It's an Http Get request.
        /// This method check if the given id is not null in purpose to be able to
        /// find it in the database
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <returns>the generated view with the specific post</returns>
        [HttpGet]
        public async Task <IActionResult>EditPost(Guid id)
        {
            var post = await _context.Post.FindAsync(id);
           
            return View(post);
        }
        /// <summary>
        /// This is an edit action method, which saves the old post's values in a post variable
        /// to be able to replace the old values with the new ones. 
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns>NotFound if the given id and the post id doesn't match,
        /// otherwise a redirection to my posts page with the edited post</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id, Post post, List<IFormFile> Images)
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
            
            if (id != post.ID)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                var oldPost = await _context.Post
                    .FirstOrDefaultAsync(p => p.ID == id);

                if (oldPost.Images != null)
                {
                    post.Images = oldPost.Images;
                }

                oldPost.BookName = post.BookName;
                oldPost.CourseCode = post.CourseCode;
                oldPost.Description = post.Description;
                oldPost.Price = post.Price;
                oldPost.Location = post.Location;
                oldPost.Images = post.Images;
                oldPost.Shippable = post.Shippable;
                oldPost.Meetup = post.Meetup;
                //  _context.Post.Update(oldPost);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyPosts");
            }
            else
            {
                return NotFound();
            }
            
        }

        /// <summary>
        /// In this method we add favorite posts to the database
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns> redirect to the posts view</returns> 
        public IActionResult AddFavorite(Guid id, string email)
        {
            _postController.AddFavoriteToDb(id, email);
          
             return RedirectToAction("Posts");
        }
        /// <summary>
        /// This method remove the favorite post from the database
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns> redirect to the posts view</returns>
        public ActionResult RemoveFavorite(Guid id, string email)
        {
            _postController.RemoveFavoriteFromDb(id, email);
            return RedirectToAction("Posts");
        }
        /// <summary>
        /// this method remove favorite posts from favorite page
        /// </summary>
        /// <authors> Cynthia, Negin, Petra, Sven</authors>
        /// <param name="id"></param>
        /// <param name="email"></param>
        /// <returns> Redirect to the favorites view</returns>
        public ActionResult RemoveFavoriteFromFavorite(Guid id, string email)
        {
            _postController.RemoveFavoriteFromDb(id, email);
            return RedirectToAction("Favorites");
        }
        
        
        /// <summary>
        /// This gets called when selecting a sorting method from the dropdown menu while browsing posts. Switch based on the value from dropdown
        /// </summary>
        /// <param name="Sort"></param>
        /// <returns>Sorts the posts and reloads the page</returns>
        [HttpPost]
        public ActionResult Sort(string Sort)
        {
            ViewBag.ThisUser = User.Identity.Name;
            ViewBag.Title = "Browse Posts";
            switch (Sort)
            {
                case "alphabetical":
                    return View("PostsAlphabetical"); 
                case "newest":
                    return View("Posts");
                case "priceAsc":
                    return View("PostsPriceAsc");
                case "priceDesc":
                    return View("PostsPriceDesc");
                case "oldest":
                    return View("PostsOldest");
            }
            ViewBag.Message = _postController.GetAllPosts();
            return View("Posts");
        }
        /// <summary>
        /// This gets called when pressing the button to contact seller on a post page. Adds the two to eachother's contacts
        /// </summary>
        /// <param name="contactToAdd"></param>
        /// <param name="loggedInUser"></param>
        /// <returns>Redirection to chat page</returns>
        public IActionResult AddContact(Guid contactToAdd, Guid loggedInUser)
        {
            _userController.CreateContact(loggedInUser, contactToAdd);
            return View("Chat");
        }
        /// <summary>
        /// This gets called when pressing to go to a post when browsing posts.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Redirection to the selected post's page</returns>
        public IActionResult GoToPost(Guid? id)
        {
            if (id is null) return View("Posts");
            ViewBag.Message = id.ToString();
            return View("Post");
        }
        public ActionResult DeleteImg(Guid id)
        {
            _postController.DeleteImage(id);
            return RedirectToAction("MyPosts");
        }
    }
}