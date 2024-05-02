using BookLibrary.Repository.Repositories;
using BookLibrary.Storage.Models.Book;
using BookLibrary.Storage.Repositories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : Page
    {
        private readonly IBooksRepository booksRepository = new BooksRepository();
        private Page _previousPage;

        private Book _book;

        public EditBookPage(Page previousPage, Book book)
        {
            InitializeComponent();

            _previousPage = previousPage;

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;

            _book = booksRepository.GetBook((int)book.ID).GetAwaiter().GetResult();

            if (book == null || _book == null)
                NavigationService.Navigate(_previousPage);

            bookView.BookName = _book.Name;
            bookView.BookAuthors = string.Join(", ", _book.Authors);
            bookView.BookYear = _book.Year;

            btnBackward.Click += btnBackward_Click;
            btnEditBook.Click += btnUpdateBook_Click;
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_previousPage);
        }

        private async void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(bookView.BookName) && !string.IsNullOrEmpty(bookView.BookAuthors))
            {
                _book.Name = bookView.BookName;
                _book.Authors = bookView.BookAuthors.Split(",");
                _book.Year = bookView.BookYear;
                await booksRepository.UpdateBook(_book);
                this.NavigationService.Navigated += NavigationService_Navigated;
                NavigationService.Navigate(_previousPage);
            }
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            var frame = sender as Frame;
            if (frame != null)
            {
                var page = frame.NavigationService?.Content as BookLibraryMainPage;
                if (page != null)
                {
                    page.LoadBooks();
                }
                frame.NavigationService.Navigated -= NavigationService_Navigated;
            }
        }
    }
}
