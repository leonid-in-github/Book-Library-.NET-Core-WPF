using Book_Library_EF_Core_Proxy_Class_Library.Adapters.Interfaces;
using Book_Library_EF_Core_Proxy_Class_Library.Context;
using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.EntityBook;
using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Adapters
{
    public class BookAdapter : IBookAdapter
    {
        public ActionBookModel GetBook(int bookId)
        {
            using (var dbContext = new BookLibraryContext())
            {
                var book = (ActionBookModel)dbContext.GetBook.FromSqlRaw("EXECUTE GetBook {0}", bookId).ToListAsync().Result.FirstOrDefault();
                return book;
            }
        }

        public List<DisplayBook> GetBooks(string spName, SqlParameter[] pArr)
        {
            using (var dbContext = new BookLibraryContext())
            {
                List<GetBookModel> booksResult;
                if (pArr == null)
                {
                    booksResult = dbContext.GetBook.FromSqlRaw(String.Format("EXECUTE {0}", spName)).ToList();
                }
                else
                {
                    booksResult = dbContext.GetBook.FromSqlRaw(String.Format("EXECUTE {0}", spName), pArr).ToListAsync().Result;
                }
                var booksList = new List<DisplayBook>();

                foreach (var book in booksResult)
                {
                    booksList.Add(book);
                }

                return booksList;
            }
        }

        public List<DisplayBook> GetBooks()
        {
            return GetBooks("GetBooks", null);
        }

        public List<DisplayBook> GetAvaliableBooks()
        {
            return GetBooks("GetBooksAvaliable", null);
        }

        public List<DisplayBook> GetBooksByUser(int userId)
        {
            var inID = new SqlParameter
            {
                ParameterName = "ID",
                Value = userId,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };

            return GetBooks("GetBooksByAccount @ID", new SqlParameter[] { inID });
        }

        public void DeleteBook(int bookId)
        {
            var inID = new SqlParameter
            {
                ParameterName = "ID",
                Value = bookId,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var sql = "exec DeleteBook @ID";
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inID);
            }
            return;
        }

        public void AddBook(AddBookModel book)
        {
            var inName = new SqlParameter
            {
                ParameterName = "Name",
                Value = book.Name,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inAuthors = new SqlParameter
            {
                ParameterName = "Authors",
                Value = book.Authors,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inYear = new SqlParameter
            {
                ParameterName = "Year",
                Value = book.Year,
                DbType = System.Data.DbType.DateTime,
                Direction = System.Data.ParameterDirection.Input
            };
            var inQuantity = new SqlParameter
            {
                ParameterName = "Quantity",
                Value = 1,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var sql = "exec AddBook @Name, @Authors, @Year, @Quantity";
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inName, inAuthors, inYear, inQuantity);
            }
            return;
        }

        public void UpdateBook(UpdateBookModel book)
        {
            var inID = new SqlParameter
            {
                ParameterName = "ID",
                Value = book.Id,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var inNewName = new SqlParameter
            {
                ParameterName = "NewName",
                Value = book.Name,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inNewAuthors = new SqlParameter
            {
                ParameterName = "NewAuthors",
                Value = book.Authors,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var inNewYear = new SqlParameter
            {
                ParameterName = "NewYear",
                Value = book.Year,
                DbType = System.Data.DbType.DateTime,
                Direction = System.Data.ParameterDirection.Input
            };
            var sql = "exec UpdateBook @ID, @NewName, @NewAuthors, @NewYear";
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inID, inNewName, inNewAuthors, inNewYear);
            }
            return;
        }

        public BookTrackModel GetBookTrack(int accountId, int bookId, string tracksCount)
        {
            var result = new BookTrackModel();
            using (var dbContext = new BookLibraryContext())
            {
                var book = dbContext.GetBook.FromSqlRaw("EXECUTE GetBook {0}", bookId).ToList().FirstOrDefault();

                result.BookId = book?.ID;
                result.BookName = book?.Name;
                result.BookAvailability = book?.Availability;

                var inAccountId = new SqlParameter
                {
                    ParameterName = "AccountId",
                    Value = accountId,
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Input
                };
                var inBookId = new SqlParameter
                {
                    ParameterName = "BookId",
                    Value = bookId,
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Input
                };
                var outResult = new SqlParameter
                {
                    ParameterName = "Result",
                    DbType = System.Data.DbType.Boolean,
                    Direction = System.Data.ParameterDirection.Output
                };

                var sql = "exec CanPutBook @AccountId, @BookId, @Result OUT";
                _ = dbContext.Database.ExecuteSqlRaw(sql, inAccountId, inBookId, outResult);

                if (!Boolean.TryParse(outResult.Value.ToString(), out bool canBePuted)) return result;
                result.CanBePuted = canBePuted;

                inBookId = new SqlParameter
                {
                    ParameterName = "BookID",
                    Value = bookId,
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Input
                };

                var inTracksCount = new SqlParameter
                {
                    ParameterName = "TracksCount",
                    Value = tracksCount.ToString(),
                    DbType = System.Data.DbType.String,
                    Direction = System.Data.ParameterDirection.Input
                };
                var bookTrackItemsArray = dbContext.GetBookTrack.FromSqlRaw("EXECUTE GetBookTrack @BookID, @TracksCount", inBookId, inTracksCount).AsNoTracking().ToList();

                foreach (var resultItem in bookTrackItemsArray)
                {
                    var bookTrackActionItem = new BookTrackActionModel(resultItem);
                    result.BookTrackList.Add(bookTrackActionItem);
                }

            }
            return result;
        }

        public void ActionBook(string action, int accountId, int? bookId)
        {
            var inAccountId = new SqlParameter
            {
                ParameterName = "AccountId",
                Value = accountId,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var inBookId = new SqlParameter
            {
                ParameterName = "BookId",
                Value = bookId,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var sql = String.Format("exec {0} @AccountId, @BookId", action);
            using (var dbContext = new BookLibraryContext())
            {
                _ = dbContext.Database.ExecuteSqlRaw(sql, inAccountId, inBookId);
            }
            return;
        }

        public void TakeBook(int accountId, int? bookId)
        {
            ActionBook("TakeBook", accountId, bookId);
        }

        public void PutBook(int accountId, int? bookId)
        {
            ActionBook("PutBook", accountId, bookId);
        }
    }
}
