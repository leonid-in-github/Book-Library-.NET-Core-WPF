using Book_Library_EF_Core_Proxy_Class_Library.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
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
            BookLibraryProxyConfiguration
                .GetInstanse()
                .SetupBookLibraryProxyConfiguration(Book_Library_.NET_Core_WPF_App.Properties.Settings.Default["ConnectionString"].ToString());
        }
    }
}
