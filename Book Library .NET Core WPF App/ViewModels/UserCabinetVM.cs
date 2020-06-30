using Book_Library_Repository_EF_Core.Models.Account;
using Book_Library_Repository_EF_Core.Repositories;
using Book_Library_Repository_EF_Core.Servicies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Book_Library_.NET_Core_WPF_App.ViewModels
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
