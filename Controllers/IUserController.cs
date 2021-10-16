using System;
using System.Threading.Tasks;
using ChalmersBookExchange.Domain;

namespace ChalmersBookExchange.Controllers
{
    public interface IUserController
    {
        Task<bool> CreateUserAsync(User user);
        string RetrieveName(string email);
        string RetrieveName(Guid id);
        Guid RetrieveID(string email);
        void CreateContact(Guid loggedInUser, Guid userToAdd);
        User[] GetContacts(string email);
        string RetrieveNameById(Guid id);
    }
}