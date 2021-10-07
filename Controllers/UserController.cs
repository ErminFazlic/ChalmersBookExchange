using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChalmersBookExchange.Data;
using ChalmersBookExchange.Domain;
using Microsoft.EntityFrameworkCore;

namespace ChalmersBookExchange.Controllers
{
    public class UserController : IUserController 
    {
        private readonly MyDbContext _context;

        public UserController(MyDbContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateUserAsync(User user)
        {
            var exists = _context.User.FirstOrDefault(x => x.Email == user.Email);
            if (exists is null)
            {
                _context.User.Add(user);
                var created = await _context.SaveChangesAsync(); 
                return created > 0;
            }

            return false;
        }

        public string RetrieveName(string email)
        {
            var user = _context.User.FirstOrDefault(x => x.Email == email);
            return user.Name;
        }

        public Guid RetrieveID(string email)
        {
            var user = _context.User.FirstOrDefault(x => x.Email == email);
            return user.ID;
        }

        public void CreateContact(Guid loggedInUser, Guid userToAdd)
        {
            var userLoggedIn = _context.User.FirstOrDefault(x => x.ID == loggedInUser);
            var otherUser = _context.User.FirstOrDefault(x => x.ID == userToAdd);

            var contactArray = userLoggedIn.Contacts;
            if (contactArray is null)
            {
                var newContactList = new List<Guid?>();
                newContactList.Add(userToAdd);
                userLoggedIn.Contacts = newContactList.ToArray();
            }
            else
            {
                if (contactArray.Contains(userToAdd)) return;
                var newContactList = contactArray.ToList();
                newContactList.Add(userToAdd);
                userLoggedIn.Contacts = newContactList.ToArray(); 
            }

            contactArray = otherUser.Contacts;
            if (contactArray is null)
            {
                var newContactList = new List<Guid?>();
                newContactList.Add(loggedInUser);
                otherUser.Contacts = newContactList.ToArray();
            }
            else
            {
                var newContactList = contactArray.ToList();
                newContactList.Add(loggedInUser);
                otherUser.Contacts = newContactList.ToArray(); 
            }

            _context.User.Update(userLoggedIn);
            _context.User.Update(otherUser);
            _context.SaveChanges();
        }
    }
}