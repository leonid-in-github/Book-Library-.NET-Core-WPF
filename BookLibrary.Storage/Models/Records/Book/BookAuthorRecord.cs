using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Repository.Models.Records.Book
{
    [PrimaryKey(nameof(BookId), nameof(AuthorId))]
    public class BookAuthorRecord
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
