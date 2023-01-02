using BookLibrary.Repository.Models.Book;
using BookLibrary.Repository.Repositories;
using BookLibrary.Repository.Servicies;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace BookLibrary.UI.ViewModels
{
    public class BookTrackViewModel : INotifyPropertyChanged
    {
        private IDataStore DataStore => RepositoryService.Get<IDataStore>();
        private string _filter = "10";
        private Visibility _filterVisibility = Visibility.Hidden;

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BookTrackViewModel() { }

        public BookTrackViewModel(BookTrackList book)
        {
            Book = book;
        }

        private ICollectionView bookTracksView { get; set; }

        private BookTrackList _book;

        public BookTrackList Book
        {
            get
            {
                return _book;
            }
            set
            {
                _book = value;
                var bookTrackItems = GetBookTrackItems(_book);
                FilterVisibility = bookTrackItems.Count() >= 10 ? Visibility.Visible : Visibility.Hidden;
                bookTracksView = CollectionViewSource.GetDefaultView(bookTrackItems);
                bookTracksView.Refresh();
                OnPropertyChanged("Book");
            }
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

        public Visibility FilterVisibility
        {
            get
            {
                return _filterVisibility;
            }
            set
            {
                _filterVisibility = value;
                OnPropertyChanged("FilterVisibility");
            }
        }

        public ICollectionView BookTracks
        {
            get { return bookTracksView; }
        }

        public void LoadBookTrack()
        {
            Book = DataStore.Books.GetBookTrack(AppUser.GetInstance().AccountId, _book.BookId, "All");
        }

        public void PutBook()
        {
            DataStore.Books.PutBook(AppUser.GetInstance().AccountId, _book.BookId);
            Book.BookAvailability = true;
        }

        public void TakeBook()
        {
            DataStore.Books.TakeBook(AppUser.GetInstance().AccountId, _book.BookId);
            Book.BookAvailability = false;
        }

        private IEnumerable<BookTrackItem> GetBookTrackItems(BookTrackList book)
        {
            return _filter switch
            {
                "10" => book.TracksList.Take(10),
                "25" => book.TracksList.Take(25),
                "50" => book.TracksList.Take(50),
                "all" => book.TracksList,
                _ => book.TracksList
            };
        }
    }
}
