using Book_Library_Repository_EF_Core.Repositories;
using Book_Library_Repository_EF_Core.Servicies;
using System.Windows;

namespace Book_Library_.NET_Core_WPF_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            var _contentRootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            var dbConnectionString = Book_Library_.NET_Core_WPF_App.Properties.Settings.Default["ConnectionString"].ToString();
            dbConnectionString = dbConnectionString.Replace("%CONTENTROOTPATH%", _contentRootPath);
            RepositoryService.Register<BookLibraryRepository>(dbConnectionString);
        }
    }
}
