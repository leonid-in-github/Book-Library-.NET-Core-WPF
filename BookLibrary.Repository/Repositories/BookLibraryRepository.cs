using BookLibrary.Repository.Components;
using BookLibrary.Repository.Contexts;
using System.Threading.Tasks;

namespace BookLibrary.Repository.Repositories
{
    public class BookLibraryRepository : IDataStore, IDataStoreCreatable
    {
        public AccountComponent Account => new AccountComponent();

        public BooksComponent Books => new BooksComponent();

        public async Task<bool> EnsureCreated()
        {
            using var dbCtx = new BookLibraryContext();

            return await Task.FromResult(dbCtx.Database.EnsureCreated());
        }
    }
}
