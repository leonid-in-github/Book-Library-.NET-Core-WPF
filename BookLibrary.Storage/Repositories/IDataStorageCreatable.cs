using System.Threading.Tasks;

namespace BookLibrary.Storage.Repositories
{
    public interface IDataStorageCreatable
    {
        Task<bool> EnsureCreated();
    }
}
