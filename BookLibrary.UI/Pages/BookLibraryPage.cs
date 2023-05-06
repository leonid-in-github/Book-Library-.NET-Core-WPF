using BookLibrary.Storage.Repositories;
using System.Windows.Controls;

namespace BookLibrary.UI.Pages
{
    public class BookLibraryPage : Page
    {
        protected readonly IBookLibraryRepository DataStore = new BookLibraryRepository();
    }
}
