using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Configuration
{
    public class BookLibraryProxyConfiguration
    {
        private static BookLibraryProxyConfiguration _instance;

        private string ConnectioString;

        private BookLibraryProxyConfiguration() { }

        public static BookLibraryProxyConfiguration GetInstanse()
        {
            if (_instance == null)
                _instance = new BookLibraryProxyConfiguration();
            return _instance;
        }

        public void SetupBookLibraryProxyConfiguration(string connectioString)
        {
            ConnectioString = connectioString;
        }

        public string ConnectionString
        {
            get 
            {
                return ConnectioString;
            }
        }
    }
}
