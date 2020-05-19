using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.EntityBook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook
{
    public class DisplayBook : Book
    {
        public DisplayBook()
        {

        }

        public int ID { get; set; }

        public bool Availability { get; set; }

        public static implicit operator DisplayBook(GetBookModel result)
        {
            return new DisplayBook { ID = result.ID, Name = result.Name, Authors = result.Authors, Year = result.Year, Availability = result.Availability };
        }
    }
}
