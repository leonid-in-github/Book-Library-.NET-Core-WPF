using Book_Library_.NET_Core_WPF_App.Models.PagesModels;
using Book_Library_.NET_Core_WPF_App.ViewModels;
using Book_Library_.NET_Core_WPF_App.Windows;
using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook;
using Book_Library_EF_Core_Proxy_Class_Library.Proxy;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Text;
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
    public partial class BookLibraryMainPage : Page
    {
        public BookLibraryMainPage()
        {
            InitializeComponent();

            SetupPage();

            this.Loaded += OnLoad;

            BooksGrid.SelectionChanged += Datagrid_Row_Click;
            btnUser.Click += btnUser_Click;
            btnAddBook.Click += btnAddBook_Click;
            btnEditBook.Click += btnEditBook_Click;
            btnDeleteBook.Click += btnDeleteBook_Click;
        }

        private void SetupPage()
        {
            pageModel = new MainPageVM();

            DataContext = pageModel;
        }

        public MainPageVM pageModel { get; set; }

        protected void OnLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                pageModel.UserName = AppUser.GetInstance().Login;
                var books = dbBookLibraryProxy.Books.GetBooks();
                pageModel.Books = books;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        private void btnUser_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new UserCabinetPage(this));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                NavigationService.Navigate(new AddBookPage(this));
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }

        }

        private void btnEditBook_Click(object sender, RoutedEventArgs e)
        {
            try
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
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }

        }

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var booksGridEnumerator = BooksGrid.SelectedItems.GetEnumerator();
                while (booksGridEnumerator.MoveNext())
                {
                    var bookId = (booksGridEnumerator.Current as DisplayBook)?.ID;
                    if (bookId != null)
                    {
                        dbBookLibraryProxy.Books.DeleteBook((int)bookId);
                    }
                }
                var books = dbBookLibraryProxy.Books.GetBooks();
                pageModel.Books = books;
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }

        }


        private void Datagrid_Row_Click(object sender, SelectionChangedEventArgs e)
        {
            //if (e.AddedItems.Count > 0)
            //{
            //    var enumerator = e.AddedItems.GetEnumerator();
            //    enumerator.MoveNext();
            //    if(enumerator.Current is DisplayBook)
            //    {
            //        var selectedItem = enumerator.Current as DisplayBook;
            //        MessageBox.Show(selectedItem.Name, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
            //    }
            //}
            if (btnEditBook.IsEnabled == false || btnDeleteBook.IsEnabled == false)
            {
                btnEditBook.IsEnabled = true;
                btnDeleteBook.IsEnabled = true;
            }
            if (BooksGrid.SelectedItems.Count == 0)
            {
                btnEditBook.IsEnabled = false;
                btnDeleteBook.IsEnabled = false;
            }
            if (BooksGrid.SelectedItems.Count > 1)
            {
                btnEditBook.IsEnabled = false;
            }

        }
    }
}
