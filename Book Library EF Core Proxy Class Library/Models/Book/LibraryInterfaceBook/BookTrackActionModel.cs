using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.EntityBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook
{
    public class BookTrackActionModel
    {
        public BookTrackActionModel(GetBookTrackModel bookTrackModel)
        {
            BookId = bookTrackModel.BookId;
            Name = bookTrackModel.BookName;
            ActionLogin = bookTrackModel.Login;
            ActionDateTime = bookTrackModel.ActionTime;

            if (bookTrackModel.Action)
            {
                Action = "Taked";
            }
            else
            {
                Action = "Puted";
            }
        }

        public int BookId { get; set; }

        public string Name { get; set; }

        public string ActionLogin { get; set; }

        public DateTime ActionDateTime { get; set; }

        public string Action { get; set; }
    }
}
