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
            ViewBag.Title = "Create a new post";
            var model = new CreatePostModel(_postController);
            return View(model);
        }
        public IActionResult Posts()
        {
            ViewBag.Title = "Browse Posts";
            return View("Posts");
        }
        
        public IActionResult Post()
        {
            ViewBag.Title = "Browse Posts";
            ViewBag.Message = "0485ac66-a33a-4441-bb43-f288ee329951";
            return View("Post");
        }

        public IActionResult MyPosts()
        {
            ViewBag.Title = "My posts";
            return View("Post");
        }

        public IActionResult Chat()
        {
            return View("Chat");
        }
        public IActionResult Search()
        {
            ViewBag.Title = "Search";
            //var model = new CreatePostModel(_postController);
            return View();
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
            return View("QueriedPosts");
        }
        /// <summary>
        /// This gets called when selecting a sorting method from the dropdown menu while browsing posts. Switch based on the value from dropdown
        /// </summary>
        /// <param name="Sort"></param>
        /// <returns>Sorts the posts and reloads the page</returns>
        [HttpPost]
        public ActionResult Sort(string Sort)
        {
            switch (Sort)
            {
                case "alphabetical":
                    ViewBag.Title = "Browse Posts";
                    return View("PostsAlphabetical"); 
                case "newest":
                    ViewBag.Title = "Browse Posts";
                    return View("Posts");
                case "priceAsc":
                    ViewBag.Title = "Browse Posts";
                    return View("PostsPriceAsc");
                case "priceDesc":
                    ViewBag.Title = "Browse Posts";
                    return View("PostsPriceDesc");
                case "oldest":
                    ViewBag.Title = "Browse Posts";
                    return View("PostsOldest");
            }

            ViewBag.Title = "Default";
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
    }
}