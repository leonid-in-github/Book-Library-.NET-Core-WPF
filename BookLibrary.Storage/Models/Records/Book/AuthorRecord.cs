using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Storage.Models.Records.Book
{
    public class AuthorRecord
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
