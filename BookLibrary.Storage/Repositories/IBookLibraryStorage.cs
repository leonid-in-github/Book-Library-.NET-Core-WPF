namespace BookLibrary.Storage.Repositories
{
    public interface IBookLibraryStorage
    {
        AccountRepository Account { get; }

        BooksRepository Books { get; }
    }
}
