using BookLibrary.Repository.Repositories;
using BookLibrary.Storage.Models.Book;
using BookLibrary.Storage.Repositories;
using BookLibrary.UI.Models.BooksModels;
using BookLibrary.UI.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for BookLibraryMainPage.xaml
    /// </summary>
    public partial class BookLibraryMainPage : Page
    {
        private readonly IBooksRepository booksRepository = new BooksRepository();

        public BookLibraryMainPage()
        {
            InitializeComponent();

            DataContext = pageModel = new MainPageViewModel();

            BooksGrid.SelectionChanged += Datagrid_Row_Click;
            BooksGrid.AutoGeneratingColumn += Datagrid_AutoGeneratingColumn;
            tbSearch.TextChanged += Search_Text_Changed;
            tbFilter.TextChanged += Filter_Text_Changed;
            btnAddBook.Click += btnAddBook_Click;
            btnEditBook.Click += btnEditBook_Click;
            btnTrackBook.Click += btnTrackBook_Click;
            btnDeleteBook.Click += btnDeleteBook_Click;
            btnFirstPage.Click += btnFirstPage_Click;
            btnPreviousPage.Click += btnPreviousPage_Click;
            tbCurrentPage.TextChanged += tbCurrentPage_Text_Changed;
            btnNextPage.Click += btnNextPage_Click;
            btnLastPage.Click += btnLastPage_Click;
            cbRecordsPerPage.SelectionChanged += cbRecordsPerPage_SelectionChanged;
            btnRefreshBooksGrid.Click += btnRefreshBooksGrid_Click;
            Loaded += OnLoaded;

        }

        public MainPageViewModel pageModel { get; set; }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddBookPage());
        }

        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            var book = BooksGrid.SelectedItem as Models.BooksModels.Book;
            if (book == null) return;
            var editBook = Storage.Models.Book.Book.FromPersistence(book.ID ?? 0, book.Name, book.Authors.Split(","), DateTime.Parse($"01/01/{book.Year}"), book.Availability == "Available");
            NavigationService.Navigate(new EditBookPage(editBook));
        }

        private async void btnTrackBook_Click(object sender, RoutedEventArgs e)
        {
            var book = BooksGrid.SelectedItem as Models.BooksModels.Book;
            if (book == null) return;
            var trackBook = await booksRepository.GetBookTrack(AppUser.GetInstance().AccountId, book.ID ?? 0, "All");
            NavigationService.Navigate(new BookTrackPage(trackBook));
        }


        private async void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            var messageBoxResult = System.Windows.MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                var booksGridEnumerator = BooksGrid.SelectedItems.GetEnumerator();
                while (booksGridEnumerator.MoveNext())
                {
                    var bookId = (booksGridEnumerator.Current as Models.BooksModels.Book)?.ID;
                    if (bookId != null)
                    {
                        await booksRepository.DeleteBook((int)bookId);
                    }
                }
                LoadBooks();
            }
        }

        private void btnFirstPage_Click(object sender, RoutedEventArgs e)
        {
            pageModel.CurrentPage = 1;
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            var currentPage = pageModel.CurrentPage - 1;
            if (currentPage > 0)
            {
                pageModel.CurrentPage = currentPage;
            }
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            var currentPage = pageModel.CurrentPage + 1;
            if (currentPage <= pageModel.NumberOfPages)
            {
                pageModel.CurrentPage = currentPage;
            }
        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            pageModel.CurrentPage = pageModel.NumberOfPages;
        }

        private void btnRefreshBooksGrid_Click(object sender, RoutedEventArgs e)
        {
            CalculateNamberOfPages();
            LoadBooks();
        }

        private void tbCurrentPage_Text_Changed(object sender, TextChangedEventArgs e)
        {
            LoadBooks();
        }

        private void cbRecordsPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageModel.CurrentPage = 1;
            LoadBooks();
            CalculateNamberOfPages();
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
            CalculateNamberOfPages();
            pageModel.CurrentPage = 1;
            LoadBooks();
        }

        private void Filter_Text_Changed(object sender, TextChangedEventArgs e)
        {
            pageModel.FilterString = tbFilter.Text;
        }

        private void Datagrid_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (e.Column.Header.ToString() == "ID")
            {
                e.Cancel = true;
            }
            if (e.Column.Header.ToString() == "Name")
            {
                e.Column.Width = 700;
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

        public void LoadBooks()
        {
            LoadBooksPage(pageModel.CurrentPage);
        }

        public async void LoadBooksPage(int pageNumber)
        {
            var searchText = tbSearch.Text;
            var recordsPerPage = pageModel.RecordsPerPage;
            var currentPage = pageNumber;
            var from = recordsPerPage * (currentPage - 1);
            await pageModel.LoadBooks(searchText, from, recordsPerPage, pageModel.Filter);
        }

        private void CalculateNamberOfPages()
        {
            var pageCountDouble = (double)pageModel.BooksTotalCount / (double)pageModel.RecordsPerPage;
            pageModel.NumberOfPages = (int)Math.Ceiling(pageCountDouble);
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageModel.CurrentPage = 1;
            LoadBooks();
            CalculateNamberOfPages();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            CalculateNamberOfPages();
        }
    }
}
