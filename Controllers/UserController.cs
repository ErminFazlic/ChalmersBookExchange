using System;
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
        
        public string RetrieveName(Guid id)
        {
            var user = _context.User.FirstOrDefault(x => x.ID == id);
            return user.Name;
        }

        public Guid RetrieveID(string email)
        {
            var user = _context.User.FirstOrDefault(x => x.Email == email);
            return user.ID;
            
        }
        
    }
}