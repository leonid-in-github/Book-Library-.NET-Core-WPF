using Book_Library_Repository_EF_Core.Repositories;
using Book_Library_Repository_EF_Core.Servicies;
using System;
using System.Windows;
using System.Windows.Controls;

namespace Book_Library_.NET_Core_WPF_App.Pages
{
    public class BookLibraryPage : Page
    {
        protected IDataStore DataStore => RepositoryService.Get<BookLibraryRepository>();

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
