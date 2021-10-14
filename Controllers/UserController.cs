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

        /// <summary>
        /// This gets called when registering a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>True if successful, otherwise false</returns>
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
        /// <summary>
        /// Retrieves the name based on the email for a user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The users name</returns>
        public string RetrieveName(string email)
        {
            var user = _context.User.FirstOrDefault(x => x.Email == email);
            return user.Name;
        }
        
        public string RetrieveName(Guid id)
        {
            var user = _context.User.FirstOrDefault(x => x.ID == id);
            return user.Name;
        }

        /// <summary>
        /// Retrieves the name based on the id for a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The users name</returns>
        public string RetrieveNameById(Guid id)
        {
            var user = _context.User.FirstOrDefault(x => x.ID == id);
            return user.Name;
        }
        /// <summary>
        /// Retrieves the id based on the email for a user
        /// </summary>
        /// <param name="email"></param>
        /// <returns>The users id</returns>
        public Guid RetrieveID(string email)
        {
            var user = _context.User.FirstOrDefault(x => x.Email == email);
            return user.ID;
        }
        /// <summary>
        /// Adds two users to eachother's contact lists
        /// </summary>
        /// <param name="loggedInUser"></param>
        /// <param name="userToAdd"></param>
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
        /// <summary>
        /// Retrieves a list of users from a user's contact list based on the user's email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Array of Users</returns>
        public User[] GetContacts(string email)
        {
            var user = _context.User.SingleOrDefault(x => x.Email == email);
            if (user.Contacts is null) return new User[0];
            var contactList = user.Contacts.Select(contactId => _context.User.FirstOrDefault(x => x.ID == contactId)).ToArray();
            return contactList;
        }
    }
}