using System;
using System.Collections.Generic;

namespace BookLibrary.Storage.Models.Book
{
    public class BookTrackList
    {
        public Guid BookId { get; set; }
        public string BookName { get; set; }
        public bool? BookAvailability { get; set; }
        public bool CanBePutted { get; set; }
        public List<BookTrack> TracksList { get; set; } = new List<BookTrack>();
    }
}
