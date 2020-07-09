using Book_Library_.NET_Core_WPF_App.Models.PagesModels;
using Book_Library_.NET_Core_WPF_App.ViewModels;
using Book_Library_.NET_Core_WPF_App.Windows;
using Book_Library_Repository_EF_Core.Models.Book;
using Book_Library_Repository_EF_Core.Repositories;
using Book_Library_Repository_EF_Core.Servicies;
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

            Loaded += OnLoad;

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
                var book = BooksGrid.SelectedItem as BookItem;
                if (book == null) return;
                var editBook = new BookItem()
                {
                    Name = book.Name,
                    Authors = book.Authors,
                    Year = book.Year,
                    ID = book.ID
                };
                NavigationService.Navigate(new EditBookPage(this, editBook));

            });
        }

        private void btnTrackBook_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                var book = BooksGrid.SelectedItem as BookItem;
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
                        var bookId = (booksGridEnumerator.Current as BookItem)?.ID;
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
    }
}
