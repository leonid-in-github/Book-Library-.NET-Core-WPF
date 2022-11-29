using BookLibrary.Repository.Components;

namespace BookLibrary.Repository.Repositories
{
    public interface IDataStore
    {
        AccountComponent Account { get; }

        BooksComponent Books { get; }
    }
}
