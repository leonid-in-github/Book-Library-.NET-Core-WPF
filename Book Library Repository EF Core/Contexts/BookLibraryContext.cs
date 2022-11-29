using BookLibrary.Repository.Models.Book;
using BookLibrary.Repository.Repositories;
using BookLibrary.Repository.Servicies;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Repository.Contexts
{
    public class BookLibraryContext : DbContext
    {
        private string CONNECTIONSTRING => RepositoryService.ConnectionString<BookLibraryRepository>();

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
            optionsBuilder.UseSqlServer(CONNECTIONSTRING);
        }
    }
}
