using System;
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

        public Post[] GetQueriedPosts(string courseCode, string bookName)
        {
            Post[] posts = _context.Post.Where(x => x.CourseCode == courseCode || x.BookName == bookName).ToArray();
            posts = ReversePosts(posts);
            return posts;
        }


        public Post[] GetMyPosts(Guid userid)
        {

            var posts = _context.Post.Where(x => x.Poster == userid).ToArray();
            posts = ReversePosts(posts);
            return posts;

        }

        // POST: Movies/Delete/5
        public Post[] DeleteConfirmed(Guid id)
        {
            var post =  _context.Post.Find(id);
            _context.Post.Remove(post);
             _context.SaveChanges();
            return GetMyPosts(id);
        }
    }
}


/*public async Task<IActionResult> Delete(Guid id)
        {

            var post = await _context.Post.FirstOrDefaultAsync(x => x.Poster == id);
            post = ReversePosts(post);
            return post;
            return View("MyPosts");
        }*/
/*public Post[] DeletePost(Guid postid)
{
    Post post = _context.Post.Find(postid);
    _context.Post.Remove(post);
    _context.SaveChanges();
    return (GetAllPosts());
}
[HttpDelete]
public ActionResult Delete(Guid postid)
{  
    if (postid == null)
    { 
        return NotFound();
    }  
    var deletedpost = _postController.DeletePost(postid);
     
    ViewBag.Message = deletedpost;
    //ViewBag.Title = "";
    return View("MyPosts");
}
/*public ActionResult Edit(Guid id)
{
    var editedpost = _postController.EditPost(id);

    return View("EditPost");
}*/
       /*[HttpPost,ActionName("Edit")]
       public ActionResult Edit()
       {
       }*/

       /*public Post[] EditPost(Guid postid)
       {
           var post = _context.Post.Where(x => x.ID == postid).FirstOrDefault();
           return ;           
       }*/
       /*[HttpDelete("{id:Guid}")]
       public async Task<Post>DeletePost(Guid id)
       {
           try
           {
               var postToDelete = await _context.Post.FirstOrDefault(x => x.ID == id);

               if (postToDelete == null)
               {
                   return NotFound($"Employee with Id = {id} not found");
               }

               return await postToDelete;
           }
           catch (Exception)
           {
               return StatusCode(StatusCodes.Status500InternalServerError,
                   "Error deleting data");
           }
       }
    }*/
       /*public async Task<Post>Delete(Guid id)
       {
           var post = await _context.Post.FindAsync(id);
           

           _context.Post.Remove(post);
           await _context.SaveChangesAsync();

           return post;
       }*/
       /*public void Delete(Guid? id)
       {
           if (id == null)
           {
               return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
           }
           Post post = _context.Post.Find(id);
           if (post == null)
           {
               return HttpNotFound();
           }*/
           
       

        
    
