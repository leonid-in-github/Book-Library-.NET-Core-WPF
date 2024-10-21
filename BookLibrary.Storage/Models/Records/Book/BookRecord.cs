using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Storage.Models.Records.Book
{
    public class BookRecord
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime Year { get; set; }
        public bool IsAvailable { get; set; }

        public static BookRecord FromDomain(Storage.Models.Book.Book book)
        {
            return new BookRecord { Id = book.Id, Name = book.Name, Year = book.Year, IsAvailable = book.IsAvailable ?? true };

        }
    }
}
