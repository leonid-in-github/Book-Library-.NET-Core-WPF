using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Models.Book.EntityBook
{
    public class GetBookModel
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Authors { get; set; }
        public System.DateTime Year { get; set; }
        public bool Availability { get; set; }
    }
}
