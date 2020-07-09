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

        private BookTrackVM pageViewModel;

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

            DataContext = pageViewModel = new BookTrackVM(book);
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
           
            if (!(bool)pageViewModel.Book.BookAvailability)
            {
                if (pageViewModel != null)
                {
                    pageViewModel.PutBook();
                    pageViewModel.LoadBookTrack();
                    BooksGrid.ItemsSource = pageViewModel.BookTracks;
                }
                btnActionBook.Content = "Take book";
            }
            else
            {
                if (pageViewModel != null)
                {
                    pageViewModel.TakeBook();
                    pageViewModel.LoadBookTrack();
                    BooksGrid.ItemsSource = pageViewModel.BookTracks;
                }
                btnActionBook.Content = "Put book";
            }
        }
    }
}
