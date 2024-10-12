using BookLibrary.UI.HelperClasses;
using System;

namespace BookLibrary.UI
{
    public class AppUser
    {
        private AppUser() { }

        private AppUser(string login, string password, Guid accountId)
        {
            Login = login;
            Password = password;
            AccountId = accountId;
        }

        private static AppUser _instance;

        public static AppUser GetInstance()
        {
            _instance ??= new AppUser();
            return _instance;
        }

        public static void SetInstance(string login, string password, Guid accountId)
        {
            SetLastSessionUser(login, password, accountId);
            _instance = new AppUser(login, password, accountId);
            return;
        }

        private static void SetLastSessionUser(string login, string password, Guid accountId)
        {
            LastSession.Login = login;
            LastSession.Password = password;
            LastSession.AccountId = accountId;
        }

        public string Login { get; }

        public string Password { get; }

        public Guid AccountId { get; }
    }
}
