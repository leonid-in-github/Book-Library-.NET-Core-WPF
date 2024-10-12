using Microsoft.EntityFrameworkCore;
using System;

namespace BookLibrary.Storage.Models.Records.Book
{
    [PrimaryKey(nameof(BookId), nameof(AuthorId))]
    public class BookAuthorRecord
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }
    }
}
