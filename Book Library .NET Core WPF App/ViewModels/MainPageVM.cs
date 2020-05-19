using Book_Library_EF_Core_Proxy_Class_Library.Models.Book.LibraryInterfaceBook;
using Book_Library_EF_Core_Proxy_Class_Library.Proxy;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Book_Library_.NET_Core_WPF_App.ViewModels
{
    public class MainPageVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _userName;
        private List<DisplayBook> _books;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
            }
        }

        public List<DisplayBook> Books { 
            get 
            { 
                return _books;
            }
            set
            {
                _books = value;
                OnPropertyChanged("Books");
            }
        }
    }
}
