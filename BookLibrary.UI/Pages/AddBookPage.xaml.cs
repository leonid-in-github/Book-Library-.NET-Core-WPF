using BookLibrary.Repository.Repositories;
using BookLibrary.Storage.Models.Book;
using BookLibrary.Storage.Repositories;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : Page
    {
        private readonly IBooksRepository booksRepository = new BooksRepository();

        public AddBookPage()
        {
            InitializeComponent();

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;

            btnBackward.Click += btnBackward_Click;
            btnAddBook.Click += btnAddBook_Click;
        }

        private async Task AddBook()
        {
            if (!string.IsNullOrEmpty(bookView.BookName) && !string.IsNullOrEmpty(bookView.BookAuthors))
            {
                var book = new Book(bookView.BookName, bookView.BookAuthors.Split(","), bookView.BookYear, true);
                await booksRepository.AddBook(book);
                bookView.BookName = string.Empty;
                bookView.BookAuthors = string.Empty;
                NavigationService.Navigated += NavigationService_Navigated;
                NavigationService.GoBack();
            }
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            var frame = sender as Frame;
            if (frame != null)
            {
                var page = frame.NavigationService.Content as BookLibraryMainPage;
                if (page != null)
                {
                    page.LoadBooksPage(1);
                }
                frame.NavigationService.Navigated -= NavigationService_Navigated;
            }
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            await AddBook();
        }
    }
}
