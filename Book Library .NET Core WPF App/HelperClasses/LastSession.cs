using System;
using System.Collections.Generic;
using System.Text;

namespace Book_Library_.NET_Core_WPF_App.HelperClasses
{
    class LastSession
    {
        internal static string Login
        {
            get
            {
                return Properties.Settings.Default.Login;
            }

            set
            {
                Properties.Settings.Default["Login"] = value;
                Properties.Settings.Default.Save();
            }
        }

        internal static string Password
        {
            get
            {
                return Properties.Settings.Default.Password;
            }

            set
            {
                Properties.Settings.Default["Password"] = value;
                Properties.Settings.Default.Save();
            }
        }

        internal static int AccountId
        {
            get
            {
                return Properties.Settings.Default.AccountId;
            }

            set
            {
                Properties.Settings.Default["AccountId"] = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
