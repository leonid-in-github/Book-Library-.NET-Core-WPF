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
    /// Interaction logic for EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : BookLibraryPage
    {
        private Page _previousPage;

        private UpdateBookModel _book;

        public EditBookPage(Page previousPage, UpdateBookModel book)
        {
            InitializeComponent();

            _previousPage = previousPage;

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;

            if (book == null)
                NavigationService.Navigate(_previousPage);
            _book = book;
            tbBookName.Text = _book.Name;
            tbBookAuthors.Text = _book.Authors;
            dpBookDate.SelectedDate = _book.Year;

            btnBackward.Click += btnBackward_Click;
            btnEditBook.Click += btnUpdateBook_Click;
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                NavigationService.Navigate(_previousPage);
            });
        }

        private void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                if (tbBookName.Text != string.Empty && tbBookAuthors.Text != string.Empty && dpBookDate.SelectedDate != null)
                {
                    _book.Name = tbBookName.Text;
                    _book.Authors = tbBookAuthors.Text;
                    _book.Year = dpBookDate.SelectedDate.Value;
                    dbBookLibraryProxy.Books.UpdateBook(_book);
                    NavigationService.Navigate(_previousPage);
                }
            });
        }
    }
}
