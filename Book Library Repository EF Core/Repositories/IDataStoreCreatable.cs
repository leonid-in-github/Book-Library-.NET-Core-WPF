using System.Threading.Tasks;

namespace Book_Library_Repository_EF_Core.Repositories
{
    public interface IDataStoreCreatable
    {
        Task<bool> EnsureCreated();
    }
}
