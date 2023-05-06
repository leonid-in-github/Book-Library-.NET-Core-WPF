using BookLibrary.Storage.Repositories;
using BookLibrary.UI.HelperClasses;
using BookLibrary.UI.HelperClasses.Commands;
using BookLibrary.UI.Models.BooksModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace BookLibrary.UI.ViewModels
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        private readonly IDataStorage DataStore = new BookLibraryRepository();

        private string _userName;
        private ICollectionView _booksView;
        private string _filterString = string.Empty;
        private bool _panelLoading;
        private string _panelMainMessage = "Loading";
        private string _panelSubMessage = "Please wait...";
        private int _currentPage = 1;
        private int _numberOfPages = 2;
        private int _recordsPerPage = 4;
        private int _booksTotalCount = 0;
        private string _filter = "all";

        public MainPageViewModel(Page previousPage)
        {
            NavigateUserCabinet = new NavigateUserCabinetCommand(previousPage);
        }

        private bool BooksFilter(object item)
        {
            var book = item as Book;
            return book.Name.ToString().ToLower().Contains(_filterString.ToLower())
                ||
                book.Authors.ToString().ToLower().Contains(_filterString.ToLower())
                ||
                book.Year.Contains(_filterString)
                ||
                book.Availability.ToString().ToLower().Contains(_filterString.ToLower())
                ||
                book.ID.ToString().ToLower().Contains(_filterString.ToLower());
        }

        public void LoadBooks(string searchString = "", int from = 0, int count = 10, string filter = "all")
        {
            ShowPanelCommand.Execute(null);
            UserName = AppUser.GetInstance().Login;
            var books = new List<Book>();
            switch (filter)
            {
                case "all":
                    books = DataStore.Books.GetBooks(searchString, from, count).Select(book => new Book(book)).ToList();
                    BooksTotalCount = DataStore.Books.GetBooksTotalCount(searchString);
                    break;
                case "available":
                    books = DataStore.Books.GetAvaliableBooks(searchString, from, count).Select(book => new Book(book)).ToList();
                    BooksTotalCount = DataStore.Books.GetAvaliableBooksTotalCount(searchString);
                    break;
                case "taken by user":
                    books = DataStore.Books.GetBooksByUser(AppUser.GetInstance().AccountId, searchString, from, count).Select(book => new Book(book)).ToList();
                    BooksTotalCount = DataStore.Books.GetBooksByUserTotalCount(AppUser.GetInstance().AccountId, searchString);
                    break;
            }

            _booksView = CollectionViewSource.GetDefaultView(books);
            _booksView.Filter = BooksFilter;
            _booksView.SortDescriptions.Add(
                new SortDescription("ID", ListSortDirection.Descending));
            _booksView.Refresh();
            OnPropertyChanged("Books");
            HidePanelCommand.Execute(null);
        }

        public string Filter
        {
            get
            {
                return _filter;
            }
            set
            {
                _filter = value;
                OnPropertyChanged("Filter");
            }
        }

        public int BooksTotalCount
        {
            get
            {
                return _booksTotalCount;
            }
            set
            {
                _booksTotalCount = value;
                OnPropertyChanged("BooksTotalCount");
            }
        }

        public int CurrentPage
        {
            get
            {
                return _currentPage;
            }
            set
            {
                _currentPage = value;
                OnPropertyChanged("CurrentPage");
            }
        }

        public int NumberOfPages
        {
            get
            {
                return _numberOfPages;
            }
            set
            {
                _numberOfPages = value;
                OnPropertyChanged("NumberOfPages");
            }
        }

        public int RecordsPerPage
        {
            get
            {
                return _recordsPerPage;
            }
            set
            {
                _recordsPerPage = value;
                OnPropertyChanged("RecordsPerPage");
            }
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

        public ICollectionView Books
        {
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
