namespace BookLibrary.UI.HelperClasses
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
                Properties.Settings.Default[nameof(Login)] = value;
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
                Properties.Settings.Default[nameof(Password)] = value;
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
                Properties.Settings.Default[nameof(AccountId)] = value;
                Properties.Settings.Default.Save();
            }
        }
    }
}
