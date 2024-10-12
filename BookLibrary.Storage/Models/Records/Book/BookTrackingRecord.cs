using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Storage.Models.Records.Book
{
    public class BookTrackingRecord
    {
        [Key]
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public Guid AccountId { get; set; }
        public DateTime ActionTime { get; set; }
        public string Action { get; set; }
    }
}
