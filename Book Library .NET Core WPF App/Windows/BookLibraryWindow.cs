using BookLibrary.Repository.Repositories;
using BookLibrary.Repository.Servicies;
using System.Windows;

namespace BookLibrary.UI.Windows
{
    public class BookLibraryWindow : Window
    {
        protected IDataStore DataStore => RepositoryService.Get<BookLibraryRepository>();
    }
}
