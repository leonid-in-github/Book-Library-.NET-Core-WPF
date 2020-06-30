using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Book_Library_Repository_EF_Core.Models.Book
{
    public class BookTrackItem
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Login { get; set; }
        public DateTime ActionTime { get; set; }
        public bool Action { get; set; }
        public string ActionString => GetActionString();

        private string GetActionString()
        {
            if (Action)
            {
                return "Taked";
            }
            else
            {
                return "Puted";
            }
        }
    }
}
