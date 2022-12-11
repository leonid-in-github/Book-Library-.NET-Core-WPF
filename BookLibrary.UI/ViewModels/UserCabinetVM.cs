using BookLibrary.Repository.Models.Account;
using BookLibrary.Repository.Repositories;
using BookLibrary.Repository.Servicies;
using BookLibrary.UI.HelperClasses.Commands;
using BookLibrary.UI.Windows;
using System.Windows;
using System.Windows.Input;

namespace BookLibrary.UI.ViewModels
{
    public class UserCabinetVM
    {
        private IDataStore DataStore => RepositoryService.Get<IDataStore>();

        public UserCabinetVM()
        {
            _user = DataStore.Account.GetUser(AppUser.GetInstance().AccountId);
            foreach (Window window in Application.Current.Windows)
            {
                if (window is LoginWindow)
                {
                    Logout = new LogoutCommand(window);
                    break;
                }
            }
        }

        private DisplayUserModel _user;

        public string FirstName
        {
            get
            {
                return _user.FirstName;
            }
        }

        public string LastName
        {
            get
            {
                return _user.LastName;
            }
        }

        public string Login
        {
            get
            {
                return _user.Login;
            }
        }

        public string Email
        {
            get
            {
                return _user.Email;
            }
        }

        public ICommand Logout { get; }
    }
}
