using BookLibrary.Repository.Models.Book;
using BookLibrary.UI.Models.BooksModels;
using BookLibrary.UI.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for BookLibraryMainPage.xaml
    /// </summary>
    public partial class BookLibraryMainPage : BookLibraryPage
    {
        public BookLibraryMainPage()
        {
            InitializeComponent();

            Loaded += OnLoad;

            BooksGrid.SelectionChanged += Datagrid_Row_Click;
            BooksGrid.AutoGeneratingColumn += Datagrid_AutoGeneratingColumn;
            tbSearch.TextChanged += Search_Text_Changed;
            btnAddBook.Click += btnAddBook_Click;
            btnEditBook.Click += btnEditBook_Click;
            btnTrackBook.Click += btnTrackBook_Click;
            btnDeleteBook.Click += btnDeleteBook_Click;
        }

        public MainPageVM pageModel { get; set; }

        protected void OnLoad(object sender, RoutedEventArgs e)
        {
            DataContext = pageModel = new MainPageVM(this);
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBookPage(this));
        }

        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            var book = BooksGrid.SelectedItem as Book;
            if (book == null) return;
            var editBook = new BookItem()
            {
                ID = book.ID
            };
            NavigationService.Navigate(new EditBookPage(this, editBook));
        }

        private void btnTrackBook_Click(object sender, RoutedEventArgs e)
        {
            var book = BooksGrid.SelectedItem as Book;
            if (book == null) return;
            var trackBook = DataStore.Books.GetBookTrack(AppUser.GetInstance().AccountId, book.ID, "All");
            NavigationService.Navigate(new BookTrackPage(this, trackBook));
        }


        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
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
        }

        private void Datagrid_Row_Click(object sender, SelectionChangedEventArgs e)
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
        }

        private void Search_Text_Changed(object sender, TextChangedEventArgs e)
        {
            pageModel.FilterString = tbSearch.Text.ToLower();
        }

        private void Datagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "Name")
            {
                e.Column.Width = 715;
                var col = e.Column as DataGridTextColumn;

                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));

                col.ElementStyle = style;
            }
            if (e.Column.Header.ToString() == "Authors")
            {
                e.Column.Width = 220;
                var col = e.Column as DataGridTextColumn;

                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));

                col.ElementStyle = style;
            }
            if (e.Column.Header.ToString() == "Year")
            {
                var col = e.Column as DataGridTextColumn;

                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));

                col.ElementStyle = style;
            }
            if (e.Column.Header.ToString() == "Availability")
            {
                var col = e.Column as DataGridTextColumn;

                var style = new Style(typeof(TextBlock));
                style.Setters.Add(new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap));
                style.Setters.Add(new Setter(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center));

                col.ElementStyle = style;
            }
        }
    }
}
