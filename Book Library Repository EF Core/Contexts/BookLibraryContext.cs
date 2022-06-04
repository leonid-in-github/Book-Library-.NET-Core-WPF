using Book_Library_Repository_EF_Core.Models.Book;
using Book_Library_Repository_EF_Core.Repositories;
using Book_Library_Repository_EF_Core.Servicies;
using Microsoft.EntityFrameworkCore;

namespace Book_Library_Repository_EF_Core.Contexts
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
