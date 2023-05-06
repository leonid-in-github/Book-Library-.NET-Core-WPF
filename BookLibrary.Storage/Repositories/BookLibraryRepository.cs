namespace BookLibrary.Storage.Repositories
{
    public class BookLibraryRepository : IBookLibraryRepository
    {
        public BookLibraryRepository()
        {
            Account = new AccountRepository();
            Books = new BooksRepository();
        }

        public AccountRepository Account { get; private set; }

        public BooksRepository Books { get; private set; }
    }
}
