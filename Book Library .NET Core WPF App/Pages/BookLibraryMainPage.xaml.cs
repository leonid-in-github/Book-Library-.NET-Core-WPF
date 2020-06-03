using Book_Library_.NET_Core_WPF_App.Models.PagesModels;
using Book_Library_.NET_Core_WPF_App.ViewModels;
using Book_Library_.NET_Core_WPF_App.Windows;
using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook;
using Book_Library_EF_Core_Proxy_Class_Library.Repository;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

            this.Loaded += OnLoad;

            BooksGrid.SelectionChanged += Datagrid_Row_Click;
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
            pageModel = new MainPageVM(NavigationService, this);

            DataContext = pageModel;

            TryCatchMessageTask(async () =>
            {
                pageModel.ShowPanelCommand.Execute(null);
                var loadDataContextTask = new Task(() =>
                {
                    LoadDataContext();
                });
                loadDataContextTask.Start();
                await Task.WhenAll(loadDataContextTask);
                pageModel.HidePanelCommand.Execute(null);
            });
        }

        private void LoadDataContext()
        {
            pageModel.UserName = AppUser.GetInstance().Login;
            var books = DbBookLibraryRepository.Books.GetBooks();
            pageModel.Books = books;
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
                var book = BooksGrid.SelectedItem as DisplayBook;
                if (book == null) return;
                var editBook = new UpdateBookModel()
                {
                    Name = book.Name,
                    Authors = book.Authors,
                    Year = book.Year,
                    Id = book.ID
                };
                NavigationService.Navigate(new EditBookPage(this, editBook));

            });
        }

        private void btnTrackBook_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                var book = BooksGrid.SelectedItem as DisplayBook;
                if (book == null) return;
                var trackBook = DbBookLibraryRepository.Books.GetBookTrack(AppUser.GetInstance().AccountId, book.ID, "All");
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
                        var bookId = (booksGridEnumerator.Current as DisplayBook)?.ID;
                        if (bookId != null)
                        {
                            DbBookLibraryRepository.Books.DeleteBook((int)bookId);
                        }
                    }
                    var books = DbBookLibraryRepository.Books.GetBooks();
                    pageModel.Books = books;
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
                var dataGridContext = DataContext as MainPageVM;
                if (dataGridContext != null)
                {
                    dataGridContext.Books = DbBookLibraryRepository.Books.GetBooks();
                    var searchBooksResult = dataGridContext.Books.Where(i =>
                        i.Name.ToString().ToLower().Contains(tbSearch.Text.ToLower())
                        ||
                        i.Authors.ToString().ToLower().Contains(tbSearch.Text.ToLower())
                        ||
                        i.Year.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture).Contains(tbSearch.Text)
                        ||
                        i.Availability.ToString().ToLower().Contains(tbSearch.Text.ToLower())
                        ||
                        i.ID.ToString().ToLower().Contains(tbSearch.Text.ToLower())
                    ).OrderBy(a => a.Name).ToList();
                    dataGridContext.Books = searchBooksResult;
                }
            });
        }
    }
}
