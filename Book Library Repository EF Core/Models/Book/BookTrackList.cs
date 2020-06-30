using System;
using System.Collections.Generic;
using System.Text;

namespace Book_Library_Repository_EF_Core.Models.Book
{
    public class BookTrackList
    {
        public BookTrackList()
        {
            TracksList = new List<BookTrackItem>();
        }

        public int? BookId { get; set; }
        public String BookName { get; set; }
        public bool? BookAvailability { get; set; }
        public bool CanBePuted { get; set; }
        public List<BookTrackItem> TracksList { get; set; }
    }
}
