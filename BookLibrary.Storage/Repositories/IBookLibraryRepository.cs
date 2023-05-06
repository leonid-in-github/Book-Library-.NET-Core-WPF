namespace BookLibrary.Storage.Repositories
{
    public interface IBookLibraryRepository
    {
        AccountRepository Account { get; }

        BooksRepository Books { get; }
    }
}
