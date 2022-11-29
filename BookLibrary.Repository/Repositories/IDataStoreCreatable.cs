using System.Threading.Tasks;

namespace BookLibrary.Repository.Repositories
{
    public interface IDataStoreCreatable
    {
        Task<bool> EnsureCreated();
    }
}
