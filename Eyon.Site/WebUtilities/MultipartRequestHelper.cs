using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eyon.Site.WebUtilities
{
    /// <summary>
    /// https://github.com/aspnet/AspNetCore.Docs/blob/master/aspnetcore/mvc/models/file-uploads.md
    /// https://github.com/aspnet/AspNetCore.Docs/tree/master/aspnetcore/mvc/models/file-uploads/samples/3.x/SampleApp
    /// </summary>
    public static class MultipartRequestHelper
    {

        public static bool IsMultipartContentType( string contentType )
        {
            return !string.IsNullOrEmpty(contentType)
                   && contentType.IndexOf("multipart/", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}
