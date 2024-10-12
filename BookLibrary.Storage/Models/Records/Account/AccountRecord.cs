using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Storage.Models.Records.Account
{
    public class AccountRecord
    {
        public Guid Id { get; set; }
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
        public Guid ProfileId { get; set; }
    }
}
