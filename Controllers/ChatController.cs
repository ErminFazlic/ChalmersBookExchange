using System;
using System.Linq;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChalmersBookExchange.Controllers
{
    public class ChatController : IChatController
    {
        private readonly MyDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserController _userController;
        
        public ChatController(MyDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IUserController userController)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _userController = userController;
        }

        /// <summary>
        /// Retrieves the previous messages between the logged in user and the user with same id as contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>JsonResult with the messages</returns>
        [HttpGet]
        [Route("Chat/ConversationWithContact/{contact}")]
        public JsonResult ConversationWithContact([FromRoute]string contact)
        {
            var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var loggedInUser = _userController.RetrieveID(userName);
            var currentUser = _context.User.SingleOrDefault(x => x.ID == loggedInUser);
            var contactID = new Guid(contact);
            

            
            var conversations = _context.Conversation.
                    Where(c => (c.Receiver == currentUser.ID
                                && c.Sender == contactID) || 
                               (c.Receiver == contactID 
                                && c.Sender == currentUser.ID)).ToList();
            conversations.OrderByDescending(x => DateTime.Parse(x.Timestamp)).ToList();

            return new JsonResult(
                new { status = "success", data = conversations }
            );
        }
        /// <summary>
        /// Stores the message between the logged in user and the contact
        /// </summary>
        /// <param name="message"></param>
        /// <param name="contact"></param>
        /// <returns>JsonResult</returns>
        [HttpGet]
        [Route("Chat/SendMessage/{message}/{contact}")]
        public JsonResult SendMessage([FromRoute]string message, [FromRoute]string contact) 
        {
            var userName = _userManager.GetUserName(_httpContextAccessor.HttpContext.User);
            var loggedInUser = _userController.RetrieveID(userName);
            var currentUser = _context.User.SingleOrDefault(x => x.ID == loggedInUser);

            Conversation convo = new Conversation
            {
                Sender = currentUser.ID,
                Message = message,
                Receiver = new Guid(contact),
                ID = System.Guid.NewGuid(),
                Timestamp = DateTime.Now.ToString()
            };

            
                _context.Conversation.Add(convo);
                _context.SaveChanges();
            

            
                return new JsonResult(
                    new { status = "success" }
                );
        }
    }
}