using System;
using System.Collections.Generic;
using System.Linq;

namespace BookLibrary.Storage.Models.Book
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Authors { get; set; }
        public DateTime Year { get; set; }
        public bool? Availability { get; set; }

        public Book(string name, IEnumerable<string> authors, DateTime year, bool availability) : this(Guid.NewGuid(), name, authors, year, availability) { }

        public static Book FromPersistence(Guid id, string name, IEnumerable<string> authors, DateTime year, bool availability)
        {
            return new Book(id, name, authors, year, availability);
        }

        private Book(Guid id, string name, IEnumerable<string> authors, DateTime year, bool availability)
        {
            Id = id;
            Name = name;
            Authors = authors.Where(author => !string.IsNullOrEmpty(author)).Select(author => author.Trim());
            Year = year;
            Availability = availability;
        }
    }
}
