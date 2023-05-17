using BookLibrary.Storage.Models.Book;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : BookLibraryPage
    {
        private Page _previousPage;

        public AddBookPage(Page previousPage)
        {
            InitializeComponent();

            _previousPage = previousPage;

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;

            btnBackward.Click += btnBackward_Click;
            btnAddBook.Click += btnAddBook_Click;
        }

        private void AddBook()
        {
            if (!string.IsNullOrEmpty(bookView.BookName) && !string.IsNullOrEmpty(bookView.BookAuthors))
            {
                var book = new Book()
                {
                    Name = bookView.BookName,
                    Authors = bookView.BookAuthors,
                    Year = bookView.BookYear
                };
                DataStore.Books.AddBook(book);
                bookView.BookName = string.Empty;
                bookView.BookAuthors = string.Empty;
                this.NavigationService.Navigated += NavigationService_Navigated;
                NavigationService.Navigate(_previousPage);
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
            NavigationService.Navigate(_previousPage);
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            AddBook();
        }
    }
}
