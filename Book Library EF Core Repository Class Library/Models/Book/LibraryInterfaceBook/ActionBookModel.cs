using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.EntityBook;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook
{
    public class ActionBookModel : Book
    {
        public ActionBookModel() { }

        public override string Name { get; set; }

        public override string Authors { get; set; }

        public override DateTime Year { get; set; }

        public static explicit operator ActionBookModel(GetBookModel result)
        {
            return new ActionBookModel { Name = result.Name, Authors = result.Authors, Year = result.Year };
        }
    }
}
