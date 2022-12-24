using BookLibrary.Repository.Contexts;
using BookLibrary.Repository.Models.Book;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace BookLibrary.Repository.Repositories
{
    public class BooksRepository
    {
        public BookItem GetBook(int bookId)
        {
            using (var dbContext = new BookLibraryContext())
            {
                var inID = new SqlParameter
                {
                    ParameterName = "BookID",
                    Value = bookId,
                    DbType = System.Data.DbType.Int32,
                    Direction = System.Data.ParameterDirection.Input
                };
                var books = dbContext.GetBook.FromSqlRaw("EXECUTE GetBook @BookID", inID).ToList();
                var book = books.FirstOrDefault();
                return book;
            }
        }

        public List<BookItem> GetBooks(string spName, SqlParameter[] pArr)
        {
            using (var dbContext = new BookLibraryContext())
            {
                List<BookItem> booksResult;
                if (pArr == null)
                {
                    booksResult = dbContext.GetBook.FromSqlRaw(string.Format("EXECUTE {0}", spName)).ToList();
                }
                else
                {
                    booksResult = dbContext.GetBook.FromSqlRaw(string.Format("EXECUTE {0}", spName), pArr).ToList();
                }
                var booksList = new List<BookItem>();

                foreach (var book in booksResult)
                {
                    booksList.Add(book);
                }

                return booksList;
            }
        }

        public List<BookItem> GetBooks(string searchString = "", uint from = 0, uint count = 10)
        {
            var searchStringParameter = new SqlParameter
            {
                ParameterName = "SearchString",
                Value = searchString,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var fromParameter = new SqlParameter
            {
                ParameterName = "From",
                Value = from,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var countParameter = new SqlParameter
            {
                ParameterName = "Count",
                Value = count,
                DbType = System.Data.DbType.Int32,
                Direction = System.Data.ParameterDirection.Input
            };
            var sqlParameters = new SqlParameter[] { searchStringParameter, fromParameter, countParameter };

            return GetBooks("GetBooks @SearchString, @From, @Count", sqlParameters);
        }

        public int GetBooksTotalCount(string searchString = "")
        {
            var searchStringParameter = new SqlParameter
            {
                ParameterName = "SearchString",
                Value = searchString,
                DbType = System.Data.DbType.String,
                Direction = System.Data.ParameterDirection.Input
            };
            var sqlParameters = new SqlParameter[] { searchStringParameter };
            var sqlString = "EXECUTE GetBooksTotalCount @SearchString";
            using (var dbContext = new BookLibraryContext())
            {
                var result = dbContext.Database.SqlQueryRaw<int>(sqlString, sqlParameters).AsEnumerable<int>().FirstOrDefault();
                return result;
            }
        }

        public List<BookItem> GetAvaliableBooks()
        {
            return GetBooks("GetBooksAvaliable", null);
        }

        public List<BookItem> GetBooksByUser(int userId)
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

        public void AddBook(BookItem book)
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

        public void UpdateBook(BookItem book)
        {
            var inID = new SqlParameter
            {
                ParameterName = "ID",
                Value = book.ID,
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

        public BookTrackList GetBookTrack(int accountId, int? bookId, string tracksCount)
        {
            var result = new BookTrackList();
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

                if (!bool.TryParse(outResult.Value.ToString(), out bool canBePuted)) return result;
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
                result.TracksList = dbContext.GetBookTrack.FromSqlRaw("EXECUTE GetBookTrack @BookID, @TracksCount", inBookId, inTracksCount).AsNoTracking().ToList();
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
            var sql = string.Format("exec {0} @AccountId, @BookId", action);
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
