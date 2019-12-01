using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Eyon.Site.Extensions
{
    public static class ImageExtensions
    {
        public static string ConvertToBase64(this IFormFile file)
        {
            string encoded = string.Empty; 
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                var fileBytes = ms.ToArray();
                encoded = Convert.ToBase64String(fileBytes);
            }
            return encoded; 
        }
    }
}
