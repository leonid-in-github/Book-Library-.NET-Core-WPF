using BookLibrary.Repository.Contexts;
using System.Threading.Tasks;

namespace BookLibrary.Repository.Repositories
{
    public class BookLibraryRepository : IDataStore, IDataStoreCreatable
    {
        public BookLibraryRepository() 
        {
            Account = new AccountRepository();
            Books = new BooksRepository();
        }

        public AccountRepository Account { get; private set; }

        public BooksRepository Books { get; private set; }

        public async Task<bool> EnsureCreated()
        {
            using var dbCtx = new BookLibraryContext();

            return await Task.FromResult(dbCtx.Database.EnsureCreated());
        }
    }
}
