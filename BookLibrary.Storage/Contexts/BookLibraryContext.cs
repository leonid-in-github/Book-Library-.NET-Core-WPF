using BookLibrary.Repository.Models.Records.Account;
using BookLibrary.Repository.Models.Records.Book;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Storage.Contexts
{
    public class BookLibraryContext : DbContext
    {
        public BookLibraryContext()
        : base()
        { }

        public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
        : base(options)
        { }

        public virtual DbSet<AccountRecord> Accounts { get; set; }
        public virtual DbSet<ProfileRecord> Profiles { get; set; }
        public virtual DbSet<AuthorRecord> Authors { get; set; }
        public virtual DbSet<BookRecord> Books { get; set; }
        public virtual DbSet<BookAuthorRecord> BooksAuthors { get; set; }
        public virtual DbSet<BookTrackingRecord> BookTracking { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StorageParameters.ConnectionString);
        }
    }
}
