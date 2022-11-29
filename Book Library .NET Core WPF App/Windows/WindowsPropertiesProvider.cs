using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BookLibrary.UI.Windows
{
    internal class WindowsPropertiesProvider
    {
        internal static BitmapSource DefaultIcon
        {
            get
            {
                return (BitmapSource)new ImageSourceConverter()
                    .ConvertFrom(Properties.Resources.favicon);
            }
        }

        internal static ImageBrush DefaultBackground
        {
            get
            {
                var backgroundImage = new ImageBrush(
                    (BitmapSource)new ImageSourceConverter()
                    .ConvertFrom(Properties.Resources.background_image_0)
                    );
                backgroundImage.Stretch = Stretch.UniformToFill;
                return backgroundImage;
            }
        }

        internal static ImageBrush LoginBackground
        {
            get
            {
                var backgroundImage = new ImageBrush(
                    (BitmapSource)new ImageSourceConverter()
                    .ConvertFrom(Properties.Resources.background_image_1)
                    );
                backgroundImage.Stretch = Stretch.UniformToFill;
                return backgroundImage;
            }
        }
    }
}
