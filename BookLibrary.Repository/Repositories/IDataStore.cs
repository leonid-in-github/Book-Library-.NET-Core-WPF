namespace BookLibrary.Repository.Repositories
{
    public interface IDataStore
    {
        AccountRepository Account { get; }

        BooksRepository Books { get; }
    }
}
