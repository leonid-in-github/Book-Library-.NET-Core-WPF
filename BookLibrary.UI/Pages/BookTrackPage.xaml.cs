using BookLibrary.Storage.Models.Book;
using BookLibrary.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    public partial class BookTrackPage : Page
    {
        private BookTrackViewModel pageViewModel;

        public BookTrackPage(BookTrackList book)
        {
            InitializeComponent();

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;
            btnBackward.Click += btnBackward_Click;
            btnActionBook.Click += btnActionBook_Click;
            BooksGrid.AutoGeneratingColumn += BooksGrid_AutoGeneratingColumn;

            if ((bool)book.BookAvailability)
            {
                btnActionBook.Content = "Take book";
            }
            else
            {
                if (!book.CanBePuted)
                {
                    btnActionBook.Visibility = Visibility.Hidden;
                }
                btnActionBook.Content = "Put book";
            }

            DataContext = pageViewModel = new BookTrackViewModel(book);
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void btnActionBook_Click(object sender, RoutedEventArgs e)
        {

            if (!(bool)pageViewModel.Book.BookAvailability)
            {
                if (pageViewModel != null)
                {
                    await pageViewModel.PutBook();
                    await pageViewModel.LoadBookTrack();
                    BooksGrid.ItemsSource = pageViewModel.BookTracks;
                }
                btnActionBook.Content = "Take book";
            }
            else
            {
                if (pageViewModel != null)
                {
                    await pageViewModel.TakeBook();
                    await pageViewModel.LoadBookTrack();
                    BooksGrid.ItemsSource = pageViewModel.BookTracks;
                }
                btnActionBook.Content = "Put book";
            }
        }

        private void BooksGrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "BookId" || e.Column.Header.ToString() == "BookName")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "ActionTime")
            {
                e.Column.Header = "Date";
            }
            if (e.Column.Header.ToString() == "Action")
            {
                e.Column.Header = "Action";
            }
        }

        private async void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            await pageViewModel.LoadBookTrack();
            BooksGrid.ItemsSource = pageViewModel.BookTracks;
        }
    }
}
