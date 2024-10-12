using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Storage.Models.Records.Book
{
    public class AuthorRecord
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
