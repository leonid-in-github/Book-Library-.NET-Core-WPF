using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook;
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

        private BookTrackModel _book;

        public BookTrackModel Book
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
