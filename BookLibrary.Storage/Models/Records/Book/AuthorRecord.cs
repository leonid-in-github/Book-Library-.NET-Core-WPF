using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Repository.Models.Records.Book
{
    public class AuthorRecord
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
