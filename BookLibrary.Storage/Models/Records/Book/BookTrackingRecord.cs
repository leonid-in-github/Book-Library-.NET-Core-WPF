using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Storage.Models.Records.Book
{
    public class BookTrackingRecord
    {
        [Key]
        public int ID { get; set; }
        public int BookId { get; set; }
        public int AccountId { get; set; }
        public DateTime ActionTime { get; set; }
        public string Action { get; set; }
    }
}
