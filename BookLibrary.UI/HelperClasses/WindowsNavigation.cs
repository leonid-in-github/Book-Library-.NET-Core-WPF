using System.Linq;

namespace BookLibrary.UI.HelperClasses
{
    class WindowsNavigation
    {
        public static MainWindow MainWindow
        {
            get
            {
                return System.Windows.Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();
            }
        }
    }
}
