using Book_Library_Repository_EF_Core.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace Book_Library_Repository_EF_Core.Repositories
{
    public interface IDataStore
    {
        AccountComponent Account { get; }

        BooksComponent Books { get; }
    }
}
