using BookLibrary.Repository.Models.Book;
using BookLibrary.Repository.Repositories;
using BookLibrary.Repository.Servicies;
using System.ComponentModel;
using System.Windows.Data;

namespace BookLibrary.UI.ViewModels
{
    public class BookTrackVM : INotifyPropertyChanged
    {
        private IDataStore DataStore => RepositoryService.Get<BookLibraryRepository>();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BookTrackVM() { }

        public BookTrackVM(BookTrackList book)
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
                bookTracksView = CollectionViewSource.GetDefaultView(_book.TracksList);
                bookTracksView.Refresh();
                OnPropertyChanged("Book");
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
    }
}
