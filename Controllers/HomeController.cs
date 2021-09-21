using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ChalmersBookExchange.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChalmersBookExchange.Models;
using ChalmersBookExchange.Views.Home;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace ChalmersBookExchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostController _postController;
        private readonly IUserController _userController;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(ILogger<HomeController> logger, IPostController postController, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IUserController userController)
        {
            _logger = logger;
            _postController = postController;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userController = userController;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreatePost()
        {
            ViewBag.Title = "Posts";
            var model = new CreatePostModel(_postController);
            return View(model);
        }
        public IActionResult Posts()
        {
            ViewBag.Title = "Browse Posts";
            var model = new CreatePostModel(_postController);
            return View(model);
        }
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
    }
}