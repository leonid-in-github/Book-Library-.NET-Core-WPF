using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BookLibrary.UI.Windows
{
    public partial class BookLibraryWindow : Window
    {
        public BookLibraryWindow() : base()
        {
            Icon = (BitmapSource)new ImageSourceConverter()
                    .ConvertFrom(Properties.Resources.favicon);
        }

        protected void Window_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        protected void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        protected void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
