using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Book_Library_.NET_Core_WPF_App.ExtensionMethods
{
    internal static class WindowExtensions
    {
        internal static void CenterWindowOnScreen(this Window window)
        {
            double screenWidth = System.Windows.SystemParameters.PrimaryScreenWidth;
            double screenHeight = System.Windows.SystemParameters.PrimaryScreenHeight;
            double windowWidth = window.Width;
            double windowHeight = window.Height;
            window.Left = (screenWidth / 2) - (windowWidth / 2);
            window.Top = (screenHeight / 2) - (windowHeight / 2);
        }
    }
}
