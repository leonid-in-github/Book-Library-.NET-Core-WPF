using BookLibrary.Storage.Models.Book;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookLibrary.Storage.Repositories
{
    public interface IBooksRepository
    {
        Task AddBook(Book book);
        Task DeleteBook(int bookId);
        Task DoBookAction(BookAction action, int accountId, int bookId);
        Task<List<Book>> GetAvailableBooks(string searchString = "", int from = 0, int count = 10);
        Task<int> GetAvailableBooksTotalCount(string searchString = "");
        Task<Book> GetBook(int bookId);
        Task<List<Book>> GetBooks(string searchString = "", bool onlyAvailable = false, int userId = -1, int from = 0, int count = 10);
        Task<List<Book>> GetBooksByUser(int userId, string searchString = "", int from = 0, int count = 10);
        Task<int> GetBooksByUserTotalCount(int userId, string searchString = "");
        Task<int> GetBooksTotalCount(string searchString = "");
        Task<BookTrackList> GetBookTrack(int accountId, int bookId, string tracksCount);
        Task PutBook(int accountId, int bookId);
        Task TakeBook(int accountId, int bookId);
        Task UpdateBook(Book book);
    }
}
