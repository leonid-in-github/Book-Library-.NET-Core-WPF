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

        public virtual DbSet<BookItem> GetBook { get; set; }
        public virtual DbSet<BookItem> GetBooksAvaliableDistinct { get; set; }
        public virtual DbSet<BookItem> GetBooksDistinct { get; set; }
        public virtual DbSet<BookTrackItem> GetBookTrack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(StorageParameters.ConnectionString);
        }
    }
}
