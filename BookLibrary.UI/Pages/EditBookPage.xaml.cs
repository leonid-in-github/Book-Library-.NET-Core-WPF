using BookLibrary.Repository.Models.Book;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : BookLibraryPage
    {
        private Page _previousPage;

        private BookItem _book;

        public EditBookPage(Page previousPage, BookItem book)
        {
            InitializeComponent();

            _previousPage = previousPage;

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;

            _book = DataStore.Books.GetBook((int)book.ID);

            if (book == null || _book == null)
                NavigationService.Navigate(_previousPage);

            tbBookName.Text = _book.Name;
            tbBookAuthors.Text = _book.Authors;
            dpBookDate.SelectedDate = _book.Year;

            btnBackward.Click += btnBackward_Click;
            btnEditBook.Click += btnUpdateBook_Click;
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(_previousPage);
        }

        private void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            if (tbBookName.Text != string.Empty && tbBookAuthors.Text != string.Empty && dpBookDate.SelectedDate != null)
            {
                _book.Name = tbBookName.Text;
                _book.Authors = tbBookAuthors.Text;
                _book.Year = dpBookDate.SelectedDate.Value;
                DataStore.Books.UpdateBook(_book);
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
