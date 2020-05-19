using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook
{
    public abstract class Book
    {
        public virtual string Name { get; set; }

        public virtual string Authors { get; set; }

        public virtual DateTime Year { get; set; }
    }
}
