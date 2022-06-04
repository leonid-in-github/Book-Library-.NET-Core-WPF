using Book_Library_Repository_EF_Core.Components;
using Book_Library_Repository_EF_Core.Contexts;
using System.Threading.Tasks;

namespace Book_Library_Repository_EF_Core.Repositories
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
