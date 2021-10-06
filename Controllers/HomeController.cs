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
        [HttpPost]
        public ActionResult SearchPost(String BookName, String CourseCode)
        {
            var searchedList = _postController.GetQueriedPosts(CourseCode, BookName);
            ViewBag.Message = searchedList;
            ViewBag.Title = "Results";
            return View("QueriedPosts");
        }
        
        
        public async Task<IActionResult>Delete (Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var post = await _context.Post.FindAsync(id);
            
            return View(post);
        }

        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var post = await _context.Post.FindAsync(id);
            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return RedirectToAction("MyPosts");
        }

        [HttpGet]
        public async Task <IActionResult>EditPost(Guid id)
        {
            var post = await _context.Post.FindAsync(id);
           
            return View(post);
        }
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