using Book_Library_Repository_EF_Core.Repositories;
using Book_Library_Repository_EF_Core.Servicies;
using System.Windows;

namespace Book_Library_.NET_Core_WPF_App.Windows
{
    public class BookLibraryWindow : Window
    {
        protected IDataStore DataStore => RepositoryService.Get<BookLibraryRepository>();
    }
}
