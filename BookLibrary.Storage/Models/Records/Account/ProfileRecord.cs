using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Repository.Models.Records.Account
{
    public class ProfileRecord
    {
        [Key]
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
