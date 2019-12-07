using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eyon.Site.Errors
{
    /// <summary>
    /// Throws an exception where the error message is safe to display to the end user.
    /// </summary>
    public class UserSafeException : Exception 
    {
        public UserSafeException(string message) : base(message)
        {

        }       
    }
}
