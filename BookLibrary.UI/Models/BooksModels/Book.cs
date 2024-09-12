namespace BookLibrary.UI.Models.BooksModels
{
    public class Book
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public string Year { get; set; }
        public string Availability { get; set; }

        public Book(Storage.Models.Book.Book bookitem)
        {
            ID = bookitem.ID;
            Name = bookitem.Name;
            Authors = string.Join(", ", bookitem.Authors);
            Year = bookitem.Year.Year.ToString();
            Availability = (bool)bookitem.Availability ? "Available" : "Not available";
        }
    }
}
