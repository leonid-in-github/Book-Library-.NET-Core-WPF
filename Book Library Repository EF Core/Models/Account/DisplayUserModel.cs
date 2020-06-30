using System;
using System.Collections.Generic;
using System.Text;

namespace Book_Library_Repository_EF_Core.Models.Account
{
    public class DisplayUserModel
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
