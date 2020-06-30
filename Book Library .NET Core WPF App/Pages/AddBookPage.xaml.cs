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
    /// <summary>
    /// Interaction logic for AddBookPage.xaml
    /// </summary>
    public partial class AddBookPage : BookLibraryPage
    {
        private Page _previousPage;

        public AddBookPage(Page previousPage)
        {
            InitializeComponent();

            _previousPage = previousPage;

            btnBackward.Background = PagesPropertiesProvider.BackwardImage;

            btnBackward.Click += btnBackward_Click;
            btnAddBook.Click += btnAddBook_Click;
        }

        private void AddBook()
        {
            if (tbBookName.Text != string.Empty && tbBookAuthors.Text != string.Empty && dpBookDate.SelectedDate != null)
            {
                var book = new BookItem()
                {
                    Name = tbBookName.Text,
                    Authors = tbBookAuthors.Text,
                    Year = dpBookDate.SelectedDate.Value
                };
                DataStore.Books.AddBook(book);
                tbBookName.Text = string.Empty;
                tbBookAuthors.Text = string.Empty;
                NavigationService.Navigate(_previousPage);
            }
        }

        private void btnBackward_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() => 
            {
                NavigationService.Navigate(_previousPage);
            });
        }

        private void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            TryCatchMessageTask(() =>
            {
                AddBook();
            });
        }

    }
}
