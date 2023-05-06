using BookLibrary.Storage;
using System.Windows;

namespace BookLibrary.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        void App_Startup(object sender, StartupEventArgs e)
        {
            var _contentRootPath = System.AppDomain.CurrentDomain.BaseDirectory;
            var dbConnectionString = BookLibrary.UI.Properties.Settings.Default["ConnectionString"].ToString();
            dbConnectionString = dbConnectionString.Replace("%CONTENTROOTPATH%", _contentRootPath);
            StorageParameters.ConnectionString = dbConnectionString;
        }
    }
}
