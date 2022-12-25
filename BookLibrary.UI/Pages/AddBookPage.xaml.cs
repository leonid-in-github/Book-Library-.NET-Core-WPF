using Azure;
using BookLibrary.Repository.Models.Book;
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
            if (tbBookName.Text != string.Empty && tbBookAuthors.Text != string.Empty && dpBookDate.SelectedDate != null)
            {
                var book = new BookItem()
                {
                    Name = tbBookName.Text,
                    Authors = tbBookAuthors.Text,
                    Year = dpBookDate.SelectedDate.Value
                };
                DataStore.Books.AddBook(book);
                tbBookName.Text = string.Empty;
                tbBookAuthors.Text = string.Empty;
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
