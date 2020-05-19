using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook
{
    public class UpdateBookModel : ActionBookModel
    {
        public UpdateBookModel() { }

        public UpdateBookModel(ActionBookModel book)
        {
            Name = book.Name;
            Authors = book.Authors;
            Year = book.Year;
        }

        public int Id { get; set; }

        public override DateTime Year { get; set; }

    }
}
