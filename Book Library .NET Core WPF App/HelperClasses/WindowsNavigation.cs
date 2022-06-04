using System.Linq;

namespace Book_Library_.NET_Core_WPF_App.HelperClasses
{
    class WindowsNavigation
    {
        public static MainWindow MainWindow
        {
            get
            {
                return App.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            }
        }
    }
}
