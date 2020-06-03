using Book_Library_EF_Core_Proxy_Class_Library.Models.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Adapters.Interfaces
{
    internal interface IAccountAdapter
    {
        int Login(string login, string password);

        int Register(string login, string password, string firstName, string lastName, string email);

        DisplayUserModel GetUser(int userId);

        bool ChangeAccountPassword(int accountId, string accountPassword, string newAccountPassword);

        bool DeleteAccount(int accountId, string accountPassword);
    }
}
