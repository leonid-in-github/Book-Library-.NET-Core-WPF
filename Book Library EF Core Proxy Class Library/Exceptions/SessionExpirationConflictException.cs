using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Library_EF_Core_Proxy_Class_Library.Exceptions
{
    [Serializable]
    public class SessionExpirationConflictException : Exception
    {
        public SessionExpirationConflictException() : base(massege)
        {

        }

        private const string massege = "Application and DB session expiration time conflict.";
    }
}
