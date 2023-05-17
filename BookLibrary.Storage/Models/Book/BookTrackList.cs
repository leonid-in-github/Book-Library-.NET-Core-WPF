using System;
using System.Collections.Generic;

namespace BookLibrary.Storage.Models.Book
{
    public class BookTrackList
    {
        public BookTrackList()
        {
            TracksList = new List<BookTrack>();
        }

        public int? BookId { get; set; }
        public String BookName { get; set; }
        public bool? BookAvailability { get; set; }
        public bool CanBePuted { get; set; }
        public List<BookTrack> TracksList { get; set; }
    }
}
