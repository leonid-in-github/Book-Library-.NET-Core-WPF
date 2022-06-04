using Book_Library_Repository_EF_Core.Components;

namespace Book_Library_Repository_EF_Core.Repositories
{
    public interface IDataStore
    {
        AccountComponent Account { get; }

        BooksComponent Books { get; }
    }
}
