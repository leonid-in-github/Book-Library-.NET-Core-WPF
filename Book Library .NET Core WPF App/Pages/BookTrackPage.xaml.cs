using Book_Library_.NET_Core_WPF_App.ViewModels;
using Book_Library_Repository_EF_Core.Models.Book;
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
    public partial class BookTrackPage : BookLibraryPage
    {
        private Page _previousPage;

        public BookTrackPage(Page previousPage, BookTrackList book)
        {
            InitializeComponent();
            _previousPage = previousPage;

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
            
            if (!(bool)bookTrackVM.Book.BookAvailability)
            {
                DataStore.Books.PutBook(AppUser.GetInstance().AccountId, bookTrackVM.Book.BookId);
                bookTrackVM.Book.BookAvailability = true;
                if (bookTrackVM != null)
                {
                    bookTrackVM.Book = DataStore.Books.GetBookTrack(AppUser.GetInstance().AccountId, (int)bookTrackVM.Book.BookId, "All");
                }
                btnActionBook.Content = "Take book";
            }
            else
            {
                DataStore.Books.TakeBook(AppUser.GetInstance().AccountId, bookTrackVM.Book.BookId);
                bookTrackVM.Book.BookAvailability = false;
                if (bookTrackVM != null)
                {
                    bookTrackVM.Book = DataStore.Books.GetBookTrack(AppUser.GetInstance().AccountId, (int)bookTrackVM.Book.BookId, "All");
                }
                btnActionBook.Content = "Put book";
            }
        }
    }
}
