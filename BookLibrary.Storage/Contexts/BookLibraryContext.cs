using BookLibrary.Storage.Models.Book;
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

        public virtual DbSet<Book> GetBook { get; set; }
        public virtual DbSet<Book> GetBooksAvaliableDistinct { get; set; }
        public virtual DbSet<Book> GetBooksDistinct { get; set; }
        public virtual DbSet<BookTrack> GetBookTrack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StorageParameters.ConnectionString);
        }
    }
}
