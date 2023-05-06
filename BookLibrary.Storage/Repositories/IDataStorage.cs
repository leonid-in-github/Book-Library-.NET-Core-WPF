namespace BookLibrary.Storage.Repositories
{
    public interface IDataStorage
    {
        AccountRepository Account { get; }

        BooksRepository Books { get; }
    }
}
