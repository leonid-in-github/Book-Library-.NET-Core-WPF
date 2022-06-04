using Book_Library_Repository_EF_Core.Models.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book_Library_.NET_Core_WPF_App.Models.BooksModels
{
    public class Book
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public string Year { get; set; }
        public string Availability { get; set; }

        public Book(BookItem bookitem)
        {
            ID = bookitem.ID;
            Name = bookitem.Name;
            Authors = bookitem.Authors;
            Year = bookitem.Year.Year.ToString();
            Availability = (bool)bookitem.Availability ? "Available" : "Not available";
        }
    }
}
