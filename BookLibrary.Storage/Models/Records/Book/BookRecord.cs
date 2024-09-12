using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Storage.Models.Records.Book
{
    public class BookRecord
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime Year { get; set; }
        public bool Availability { get; set; }

        public static BookRecord FromDomain(Storage.Models.Book.Book book)
        {
            return new BookRecord { ID = book.ID, Name = book.Name, Year = book.Year, Availability = book.Availability ?? true };

        }
    }
}
