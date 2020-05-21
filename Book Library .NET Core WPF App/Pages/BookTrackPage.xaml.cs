using Book_Library_.NET_Core_WPF_App.ViewModels;
using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook;
using Book_Library_EF_Core_Proxy_Class_Library.Proxy;
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
    /// Interaction logic for BookTrackPage.xaml
    /// </summary>
    public partial class BookTrackPage : BookLibraryPage
    {
        private Page _previousPage;

        private BookTrackModel _book;

        public BookTrackPage(Page previousPage, BookTrackModel book)
        {
            InitializeComponent();
            _previousPage = previousPage;
            _book = book;

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;
            btnBackward.Click += btnBackward_Click;
            btnActionBook.Click += btnActionBook_Click;

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

            var pageModel = new BookTrackVM();
            pageModel.Book = book;

            DataContext = pageModel;
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                NavigationService.Navigate(_previousPage);
            });
        }

        private void btnActionBook_Click(object sender, RoutedEventArgs e)
        {
            BookTrackVM bookTrackVM = DataContext as BookTrackVM;
            if (!(bool)_book.BookAvailability)
            {
                dbBookLibraryProxy.Books.PutBook(AppUser.GetInstance().AccountId, _book.BookId);
                _book.BookAvailability = true;
                if (bookTrackVM != null)
                {
                    bookTrackVM.Book = dbBookLibraryProxy.Books.GetBookTrack(AppUser.GetInstance().AccountId, (int)_book.BookId, "All");
                }
                btnActionBook.Content = "Take book";
            }
            else
            {
                dbBookLibraryProxy.Books.TakeBook(AppUser.GetInstance().AccountId, _book.BookId);
                _book.BookAvailability = false;
                if (bookTrackVM != null)
                {
                    bookTrackVM.Book = dbBookLibraryProxy.Books.GetBookTrack(AppUser.GetInstance().AccountId, (int)_book.BookId, "All");
                }
                btnActionBook.Content = "Put book";
            }
        }
    }
}
