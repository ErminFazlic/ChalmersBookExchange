using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChalmersBookExchange.Models;
using ChalmersBookExchange.Views.Home;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;

namespace ChalmersBookExchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostController _postController;
        private readonly IUserController _userController;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly MyDbContext _context;

        public HomeController(ILogger<HomeController> logger, IPostController postController, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IUserController userController, MyDbContext context)
        {
            _logger = logger;
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
            ViewBag.Title = "Create a new post";
            var model = new CreatePostModel(_postController);
            return View(model);
        }
        public IActionResult Posts()
        {
            ViewBag.Title = "Browse Posts";
            var model = new CreatePostModel(_postController);
            return View(model);
        }
        public IActionResult Search()
        {
            ViewBag.Title = "Search";
            //var model = new CreatePostModel(_postController);
            return View();
        }
        public IActionResult MyPosts()
        {
            ViewBag.Title = "MyPosts";
            //UserPosts();
            //var model = new MyPostModel(_postController, _userManager, _httpContextAccessor, _userController);
            return View("MyPosts");
        }
        /*public IActionResult EditPost()
        {
            ViewBag.Title = "Edit post";
            //var model = new CreatePostModel(_postController);
            return View();
        }*/
        public IActionResult Favorites()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
        [HttpPost]
        public ActionResult CreatePostForm(string BookName, string CourseCode, string Description, int Price, string Location, string Shippable, string Meetup)
        {
            var email = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var ship = true;
            var meet = true;
            byte[] images = null; 
            if (Shippable is null)
            {
                ship = false;
            }
            if (Meetup is null)
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
            return View("OnePost");

        }
        /// <summary>
        /// this is the search action method which activates by search button.
        /// it calls GetQueriedPosts method to find the desired posts 
        /// </summary>
        /// <param name="BookName"></param>
        /// <param name="CourseCode"></param>
        /// <returns>the generated view with the desired posts</returns>
        [HttpPost]
        public ActionResult SearchPost(String BookName, String CourseCode)
        {
            var searchedList = _postController.GetQueriedPosts(CourseCode, BookName);
            ViewBag.Message = searchedList;
            ViewBag.Title = "Results";
            return View("QueriedPosts");
        }
        
        /// <summary>
        /// This method check if the given id is not null in purpose to be able to
        /// find it in the database
        /// </summary>
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
        /// <param name="id"></param>
        /// <param name="post"></param>
        /// <returns>NotFound if the given id and the post id doesn't match,
        /// otherwise a redirection to my posts page with the edited post</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(Guid id, Post post)
        {
            
            if (id != post.ID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var oldPost = await _context.Post
                    .FirstOrDefaultAsync(p => p.ID == id);

                oldPost.BookName = post.BookName;
                oldPost.CourseCode = post.CourseCode;
                oldPost.Price = post.Price;
                oldPost.Location = post.Location;
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
        
    }
}