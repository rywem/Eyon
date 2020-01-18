using System;

namespace Eyon.Models.Errors
{    
    /// <summary>
    /// Throws an exception where the error message is safe to display to the end user.
    /// </summary>
    public class SafeException : Exception
    {
        public string SafeMessage { get; set; }
        public SafeException(string safeMessage) : base(safeMessage)
        {
            this.SafeMessage = safeMessage;
        }

        public SafeException( string safeMessage, Exception innerException ) : base(safeMessage, innerException)
        {
            
        }
    }
}
