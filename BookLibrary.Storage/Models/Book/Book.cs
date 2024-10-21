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
        public bool? IsAvailable { get; set; }

        public Book(string name, IEnumerable<string> authors, DateTime year, bool isAvailable) : this(Guid.NewGuid(), name, authors, year, isAvailable) { }

        public static Book FromPersistence(Guid id, string name, IEnumerable<string> authors, DateTime year, bool isAvailable)
        {
            return new Book(id, name, authors, year, isAvailable);
        }

        private Book(Guid id, string name, IEnumerable<string> authors, DateTime year, bool isAvailable)
        {
            Id = id;
            Name = name;
            Authors = authors.Where(author => !string.IsNullOrEmpty(author)).Select(author => author.Trim());
            Year = year;
            IsAvailable = isAvailable;
        }
    }
}
