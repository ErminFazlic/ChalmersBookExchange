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

namespace ChalmersBookExchange.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPostController _postController;

        public HomeController(ILogger<HomeController> logger, IPostController postController)
        {
            _logger = logger;
            _postController = postController;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Posts()
        {
            ViewBag.Title = "Posts";
            var model = new PostsModel(_postController);
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
        public ActionResult CreatePostForm(string BookName, string CourseCode, string Description, int Price, string Location, bool Shippable, bool Meetup)
        {
            var email = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var post = new Post
            {
                ID = _context.User.FirstOrDefault(x=> x.Email==email).ID,
                BookName = BookName,
                CourseCode = CourseCode,
                Description = Description,
                Price = Price,
                Location = Location,
                Shippable = Shippable,
                Meetup = Meetup
            };
            var created = CreatePost(post);
            return Microsoft.EntityFrameworkCore.Metadata.Internal.View("Index");

        }
    }
}