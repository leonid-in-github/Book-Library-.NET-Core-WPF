using System;

namespace BookLibrary.Storage.Models.Book
{
    public class Book
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public DateTime Year { get; set; }
        public bool? Availability { get; set; }
    }
}
