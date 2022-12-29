using System;
using System.ComponentModel.DataAnnotations;

namespace BookLibrary.Repository.Models.Book
{
    public class BookTrackItem
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime ActionTime { get; set; }
        public bool Action { get; set; }
        public string ActionString => GetActionString();

        private string GetActionString()
        {
            if (Action)
            {
                return "Took";
            }
            else
            {
                return "Put";
            }
        }
    }
}
