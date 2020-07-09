using Book_Library_.NET_Core_WPF_App.HelperClasses;
using Book_Library_.NET_Core_WPF_App.HelperClasses.Commands;
using Book_Library_Repository_EF_Core.Models.Book;
using Book_Library_Repository_EF_Core.Repositories;
using Book_Library_Repository_EF_Core.Servicies;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Book_Library_.NET_Core_WPF_App.ViewModels
{
    public class MainPageVM : INotifyPropertyChanged
    {
        private IDataStore DataStore => RepositoryService.Get<BookLibraryRepository>();

        private string _userName;
        private ICollectionView _booksView;
        private string _filterString = string.Empty;
        private bool _panelLoading;
        private string _panelMainMessage = "Loading";
        private string _panelSubMessage = "Please wait...";

        public MainPageVM(Page previousPage)
        {
            NavigateUserCabinet = new NavigateUserCabinetCommand(previousPage);
            ExecuteLoadBooksCommand();
        }

        private bool BooksFilter(object item)
        {
            var book = item as BookItem;
            return book.Name.ToString().ToLower().Contains(_filterString.ToLower())
                ||
                book.Authors.ToString().ToLower().Contains(_filterString.ToLower())
                ||
                book.Year.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).Contains(_filterString)
                ||
                book.Availability.ToString().ToLower().Contains(_filterString.ToLower())
                ||
                book.ID.ToString().ToLower().Contains(_filterString.ToLower());
        }

        public void ExecuteLoadBooksCommand()
        {
            ShowPanelCommand.Execute(null);
            UserName = AppUser.GetInstance().Login;
            var books = DataStore.Books.GetBooks();
            _booksView = CollectionViewSource.GetDefaultView(books);
            _booksView.Filter = BooksFilter;
            _booksView.SortDescriptions.Add(
                new SortDescription("Name", ListSortDirection.Ascending));
            _booksView.Refresh();
            HidePanelCommand.Execute(null);
        }

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public ICollectionView Books { 
            get 
            { 
                return _booksView;
            }
            set
            {
                _booksView = value;
                OnPropertyChanged("Books");
            }
        }

        public string FilterString
        {
            get { return _filterString; }
            set
            {
                _filterString = value;
                OnPropertyChanged("FilterString");
                _booksView.Refresh();
            }
        }

        public bool PanelLoading
        {
            get
            {
                return _panelLoading;
            }
            set
            {
                _panelLoading = value;
                OnPropertyChanged("PanelLoading");
            }
        }

        public string PanelMainMessage
        {
            get
            {
                return _panelMainMessage;
            }
            set
            {
                _panelMainMessage = value;
                OnPropertyChanged("PanelMainMessage");
            }
        }

        public string PanelSubMessage
        {
            get
            {
                return _panelSubMessage;
            }
            set
            {
                _panelSubMessage = value;
                OnPropertyChanged("PanelSubMessage");
            }
        }
        
        public ICommand PanelCloseCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PanelLoading = false;
                });
            }
        }

        public ICommand ShowPanelCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PanelLoading = true;
                });
            }
        }

        public ICommand HidePanelCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PanelLoading = false;
                });
            }
        }

        public ICommand ChangeSubMessageCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    PanelSubMessage = string.Format("Message: {0}", DateTime.Now);
                });
            }
        }

        public ICommand NavigateUserCabinet { get; }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
