using Book_Library_Repository_EF_Core.Repositories;
using Book_Library_Repository_EF_Core.Servicies;
using System.Windows.Controls;

namespace Book_Library_.NET_Core_WPF_App.Pages
{
    public class BookLibraryPage : Page
    {
        protected IDataStore DataStore => RepositoryService.Get<BookLibraryRepository>();
    }
}
