using System;
using System.Collections.Generic;
using System.Text;

namespace Book_Library_Repository_EF_Core.Exceptions
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
