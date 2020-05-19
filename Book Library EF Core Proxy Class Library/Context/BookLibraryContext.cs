using Book_Library_EF_Core_Proxy_Class_Library.Constants;
using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.EntityBook;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Context
{
    public class BookLibraryContext : DbContext
    {
        public BookLibraryContext()
        : base()
        { }

        public BookLibraryContext(DbContextOptions<BookLibraryContext> options)
        : base(options)
        { }

        public virtual DbSet<GetBookModel> GetBook { get; set; }
        public virtual DbSet<GetBooksAvaliableDistinctModel> GetBooksAvaliableDistinct { get; set; }
        public virtual DbSet<GetBooksDistinctModel> GetBooksDistinct { get; set; }
        public virtual DbSet<GetBookTrackModel> GetBookTrack { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(LibraryConstants.CONNECTIONSTRING);
        }
    }
}
