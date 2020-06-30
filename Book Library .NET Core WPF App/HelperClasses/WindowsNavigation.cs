using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace Book_Library_.NET_Core_WPF_App.HelperClasses
{
    class WindowsNavigation
    {
        public static MainWindow MainWindow 
        { 
            get 
            {
                return App.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            } 
        }
    }
}
