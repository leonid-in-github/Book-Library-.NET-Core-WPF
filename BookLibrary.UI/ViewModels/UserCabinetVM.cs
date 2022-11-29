using BookLibrary.Repository.Models.Account;
using BookLibrary.Repository.Repositories;
using BookLibrary.Repository.Servicies;

namespace BookLibrary.UI.ViewModels
{
    public class UserCabinetVM
    {
        private IDataStore DataStore => RepositoryService.Get<BookLibraryRepository>();

        public UserCabinetVM()
        {
            _user = DataStore.Account.GetUser(AppUser.GetInstance().AccountId);
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
    }
}
