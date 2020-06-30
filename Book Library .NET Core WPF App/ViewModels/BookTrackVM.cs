using Book_Library_Repository_EF_Core.Models.Book;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Book_Library_.NET_Core_WPF_App.ViewModels
{
    public class BookTrackVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private BookTrackList _book;

        public BookTrackList Book
        {
            get
            {
                return _book;
            }
            set
            {
                _book = value;
                OnPropertyChanged("Book");
            }
        }
    }
}
