using System.Linq;

namespace BookLibrary.UI.HelperClasses
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
