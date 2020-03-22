using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.Enums
{
    public enum ErrorType
    {
        Denied = 403,
        NotFound = 404,
        Server = 500,
        AnErrorOccurred = 999
    }
}
