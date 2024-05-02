using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Repository.Models.Records.Account
{
    public class AccountRecord
    {
        public int ID { get; set; }
        [Key]
        public string Login { get; set; }
        public string Password { get; set; }
        public int ProfileId { get; set; }
    }
}
