using System;

namespace BookLibrary.Storage.Models.Book
{
    public class BookTrack
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public DateTime ActionTime { get; set; }
        public string Action { get; set; }

        public static BookTrack FromPersistence(Guid bookId, string bookName, string login, string email, DateTime actionTime, string action)
        {
            return new BookTrack
            {
                BookId = bookId,
                BookName = bookName,
                Login = login,
                Email = email,
                ActionTime = actionTime,
                Action = action
            };
        }
    }
}
