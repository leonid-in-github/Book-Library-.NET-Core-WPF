using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Models.Book.EntityBook
{
    public class GetBookTrackModel
    {
        [Key]
        public int BookId { get; set; }
        public string BookName { get; set; }
        public string Login { get; set; }
        public System.DateTime ActionTime { get; set; }
        public bool Action { get; set; }
    }
}
