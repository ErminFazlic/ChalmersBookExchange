using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ChalmersBookExchange.Controllers
{
    public class ChatController
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

        public JsonResult ConversationWithContact(Guid contact, Guid loggedInUser)
        {

            var currentUser = _context.User.SingleOrDefault(x => x.ID == loggedInUser);

            var conversations = new List<Conversation>();

            
            conversations = _context.Conversation.
                    Where(c => (c.Receiver == currentUser.ID
                                && c.Sender == contact) || 
                               (c.Receiver == contact 
                                && c.Sender == currentUser.ID))
                    .OrderBy(c => DateTime.Parse(c.TimeStamp))
                    .ToList();
            

            return new JsonResult(
                new { status = "success", data = conversations }, Json
                JsonRequestBehavior.AllowGet
            );
        }
    }
}