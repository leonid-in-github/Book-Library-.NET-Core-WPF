using Book_Library_.NET_Core_WPF_App.Models.BooksModels;
using Book_Library_.NET_Core_WPF_App.ViewModels;
using Book_Library_Repository_EF_Core.Models.Book;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Book_Library_.NET_Core_WPF_App.Pages
{
    /// <summary>
    /// Interaction logic for BookLibraryMainPage.xaml
    /// </summary>
    public partial class BookLibraryMainPage : BookLibraryPage
    {
        public BookLibraryMainPage()
        {
            InitializeComponent();

            SetupPage();

            Loaded += OnLoad;

            BooksGrid.SelectionChanged += Datagrid_Row_Click;
            BooksGrid.AutoGeneratingColumn += Datagrid_AutoGeneratingColumn;
            tbSearch.TextChanged += Search_Text_Changed;
            btnAddBook.Click += btnAddBook_Click;
            btnEditBook.Click += btnEditBook_Click;
            btnTrackBook.Click += btnTrackBook_Click;
            btnDeleteBook.Click += btnDeleteBook_Click;
        }

        private void SetupPage()
        {

        }

        public MainPageVM pageModel { get; set; }

        protected void OnLoad(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                DataContext = pageModel = new MainPageVM(this);
            });
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                NavigationService.Navigate(new AddBookPage(this));
            });
        }

        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                var book = BooksGrid.SelectedItem as Book;
                if (book == null) return;
                var editBook = new BookItem()
                {
                    ID = book.ID
                };
                NavigationService.Navigate(new EditBookPage(this, editBook));

            });
        }

        private void btnTrackBook_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                var book = BooksGrid.SelectedItem as Book;
                if (book == null) return;
                var trackBook = DataStore.Books.GetBookTrack(AppUser.GetInstance().AccountId, book.ID, "All");
                NavigationService.Navigate(new BookTrackPage(this, trackBook));
            });
        }


        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                var messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    var booksGridEnumerator = BooksGrid.SelectedItems.GetEnumerator();
                    while (booksGridEnumerator.MoveNext())
                    {
                        var bookId = (booksGridEnumerator.Current as Book)?.ID;
                        if (bookId != null)
                        {
                            DataStore.Books.DeleteBook((int)bookId);
                        }
                    }
                    pageModel.ExecuteLoadBooksCommand();
                }
            });
        }

        private void Datagrid_Row_Click(object sender, SelectionChangedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                if (btnEditBook.IsEnabled == false || btnDeleteBook.IsEnabled == false || btnTrackBook.IsEnabled == false)
                {
                    btnEditBook.IsEnabled = true;
                    btnTrackBook.IsEnabled = true;
                    btnDeleteBook.IsEnabled = true;
                }
                if (BooksGrid.SelectedItems.Count == 0)
                {
                    btnEditBook.IsEnabled = false;
                    btnTrackBook.IsEnabled = false;
                    btnDeleteBook.IsEnabled = false;
                }
                if (BooksGrid.SelectedItems.Count > 1)
                {
                    btnEditBook.IsEnabled = false;
                    btnTrackBook.IsEnabled = false;
                }
            });
        }

        private void Search_Text_Changed(object sender, TextChangedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                pageModel.FilterString = tbSearch.Text.ToLower();
            });
        }

        private void Datagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID")
            {
                e.Cancel = true;
            }
        }
    }
}
