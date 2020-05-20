using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Book_Library_.NET_Core_WPF_App.Windows
{
    public class BookLibraryWindow : Window
    {
        protected delegate void Delegate();

        protected void TryCatchMessageTask(Delegate _delegate)
        {
            try
            {
                _delegate();
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message, "Book Library Alert", MessageBoxButton.OK, MessageBoxImage.Information);
                Application.Current.Shutdown();
            }
        }

    }
}
