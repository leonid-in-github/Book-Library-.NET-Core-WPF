using BookLibrary.Storage.Models.Account;
using System.Threading.Tasks;

namespace BookLibrary.Repository.Repositories
{
    public interface IAccountRepository
    {
        Task<bool> ChangeAccountPassword(int accountId, string accountPassword, string newAccountPassword);
        Task<bool> DeleteAccount(int accountId, string accountPassword);
        Task<User> GetUser(int userId);
        Task<int> Login(string login, string password);
        Task<int> Register(string login, string password, string firstName, string lastName, string email);
    }
}
