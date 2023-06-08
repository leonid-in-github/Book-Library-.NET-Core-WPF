using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Storage.Models.Book
{
    public class BookTrack
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime ActionTime { get; set; }
        public string Action { get; set; }
    }
}
