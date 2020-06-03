using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook
{
    public class BookTrackModel
    {
        public BookTrackModel()
        {
            BookTrackList = new List<BookTrackActionModel>();
        }

        public int? BookId { get; set; }
        public String BookName { get; set; }
        public bool? BookAvailability { get; set; }
        public bool CanBePuted { get; set; }
        public List<BookTrackActionModel> BookTrackList { get; set; }
    }
}
