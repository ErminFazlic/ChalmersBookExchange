using System.Threading.Tasks;
using ChalmersBookExchange.Domain;

namespace ChalmersBookExchange.Controllers
{
    public interface IUserController
    {
        Task<bool> CreateUserAsync(User user);
        string RetrieveName(string email);
    }
}