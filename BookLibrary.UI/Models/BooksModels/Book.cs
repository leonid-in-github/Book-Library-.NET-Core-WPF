using System;

namespace BookLibrary.UI.Models.BooksModels
{
    public class Book
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public string Year { get; set; }
        public bool IsAvailable { get; set; }

        public Book(Storage.Models.Book.Book bookitem)
        {
            Id = bookitem.Id;
            Name = bookitem.Name;
            Authors = string.Join(", ", bookitem.Authors);
            Year = bookitem.Year.Year.ToString();
            IsAvailable = (bool)bookitem.IsAvailable;
        }
    }
}
