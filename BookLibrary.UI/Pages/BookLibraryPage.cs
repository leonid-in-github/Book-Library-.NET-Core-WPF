using BookLibrary.Repository.Repositories;
using BookLibrary.Repository.Servicies;
using System.Windows.Controls;

namespace BookLibrary.UI.Pages
{
    public class BookLibraryPage : Page
    {
        protected IDataStore DataStore => RepositoryService.Get<IDataStore>();
    }
}
