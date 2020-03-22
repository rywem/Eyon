using Eyon.Models.Enums;
using System;

namespace Eyon.Models.Errors
{    
    /// <summary>
    /// Throws an exception where the error message is safe to display to the end user.
    /// </summary>
    public class SafeException : Exception
    {
        public string SafeMessage { get; set; }
        public ErrorType ErrorType { get; set; } = ErrorType.Server;
        public SafeException(string safeMessage) : base(safeMessage)
        {
            this.SafeMessage = safeMessage;
        }

        public SafeException( string safeMessage, Exception innerException ) : base(safeMessage, innerException)
        {
            
        }

        public SafeException( ErrorType errorType ) : base(errorType.ToString())
        {
            this.ErrorType = errorType;
        }
        public SafeException( ErrorType errorType, Exception innerException ) : base(errorType.ToString(), innerException)
        {
            this.ErrorType = errorType;
        }
    }
}
