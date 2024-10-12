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

            BtnBackward.Background = PagesPropertiesProvider.BackwardImage;
            BtnBackward.Click += BtnBackward_Click;
            BtnActionBook.Click += BtnActionBook_Click;
            BooksGrid.AutoGeneratingColumn += BooksGrid_AutoGeneratingColumn;

            if ((bool)book.BookAvailability)
            {
                BtnActionBook.Content = "Take book";
            }
            else
            {
                if (!book.CanBePutted)
                {
                    BtnActionBook.Visibility = Visibility.Hidden;
                }
                BtnActionBook.Content = "Put book";
            }

            DataContext = pageViewModel = new BookTrackViewModel(book);
        }

        private void BtnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void BtnActionBook_Click(object sender, RoutedEventArgs e)
        {

            if (!(bool)pageViewModel.Book.BookAvailability)
            {
                if (pageViewModel != null)
                {
                    await pageViewModel.PutBook();
                    await pageViewModel.LoadBookTrack();
                    BooksGrid.ItemsSource = pageViewModel.BookTracks;
                }
                BtnActionBook.Content = "Take book";
            }
            else
            {
                if (pageViewModel != null)
                {
                    await pageViewModel.TakeBook();
                    await pageViewModel.LoadBookTrack();
                    BooksGrid.ItemsSource = pageViewModel.BookTracks;
                }
                BtnActionBook.Content = "Put book";
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
