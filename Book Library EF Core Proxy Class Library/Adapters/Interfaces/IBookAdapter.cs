using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Adapters.Interfaces
{
    internal interface IBookAdapter
    {
        ActionBookModel GetBook(int bookId);
        List<DisplayBook> GetBooks();
        List<DisplayBook> GetAvaliableBooks();
        List<DisplayBook> GetBooksByUser(int userId);
        void DeleteBook(int bookId);
        void AddBook(AddBookModel book);
        void UpdateBook(UpdateBookModel book);
        BookTrackModel GetBookTrack(int accountId, int bookId, string tracksCount);
        void TakeBook(int accountId, int? bookId);
        void PutBook(int accountId, int? bookId);
    }
}
