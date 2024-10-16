﻿using BookLibrary.Storage.Models.Book;
using BookLibrary.Storage.Repositories;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace BookLibrary.UI.Pages
{
    /// <summary>
    /// Interaction logic for EditBookPage.xaml
    /// </summary>
    public partial class EditBookPage : Page
    {
        private readonly IBooksRepository booksRepository = new BooksRepository();

        private Book _book;

        public EditBookPage(Book book)
        {
            InitializeComponent();

            BtnBackward.Background = PagesPropertiesProvider.BackwardImage;

            _book = booksRepository.GetBook(book.Id).GetAwaiter().GetResult();

            if (book == null || _book == null)
                NavigationService.GoBack();

            bookView.BookName = _book.Name;
            bookView.BookAuthors = string.Join(", ", _book.Authors);
            bookView.BookYear = _book.Year;

            BtnBackward.Click += BtnBackward_Click;
            BtnEditBook.Click += BtnUpdateBook_Click;
        }

        private void BtnBackward_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private async void BtnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(bookView.BookName) && !string.IsNullOrEmpty(bookView.BookAuthors))
            {
                _book.Name = bookView.BookName;
                _book.Authors = bookView.BookAuthors.Split(",");
                _book.Year = bookView.BookYear;
                await booksRepository.UpdateBook(_book);
                this.NavigationService.Navigated += NavigationService_Navigated;
                NavigationService.GoBack();
            }
        }

        private void NavigationService_Navigated(object sender, NavigationEventArgs e)
        {
            if (sender is Frame frame)
            {
                if (frame.NavigationService?.Content is BookLibraryMainPage page)
                {
                    page.LoadBooks();
                }
                frame.NavigationService.Navigated -= NavigationService_Navigated;
            }
        }
    }
}
