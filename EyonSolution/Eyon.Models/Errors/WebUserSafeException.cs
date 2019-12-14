using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Errors
{    
    /// <summary>
    /// Throws an exception where the error message is safe to display to the end user.
    /// </summary>
    public class WebUserSafeException : Exception
    {
        public WebUserSafeException(string message) : base(message)
        {

        }
    }
}
