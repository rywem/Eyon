using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eyon.Utilities.Extensions;
using System.Drawing;

namespace Eyon.Site.Extensions
{
    public static class UploadExtensions
    {
        public static string ConvertToBase64(this IFormFile file)
        {
            string encoded = string.Empty; 
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                

                var img = ms.ToImage();
                var scaledImg = img.ScaleImage(400, 400);

                //Image newImg = null;
                long newLength = ms.Length;
                int newCapacity = ms.Capacity;
                using (var newMs = new MemoryStream(ms.Capacity) )
                {
                    newMs.ToStream(scaledImg);
                    //newCapacity = newMs.Capacity;
                    //newLength = newMs.Length;
                    //newImg = newMs.ToImage();
                    var fileBytes = newMs.ToArray();
                    encoded = Convert.ToBase64String(fileBytes);
                }

                //Image newNewImage = newImg;
                //int qualityCounter = 100;
                
                //while (newLength > 100001 )
                //{
                //    qualityCounter--;
                //    using ( var newMs = new MemoryStream(newCapacity) )
                //    {
                //        newMs.ToJpeg(newNewImage, qualityCounter);
                //        newCapacity = newMs.Capacity;
                //        newLength = newMs.Length;
                //        newNewImage = newMs.ToImage();
                //    }
                //}

                //using ( var newMs = new MemoryStream(newCapacity) )
                //{
                //    newMs.ToStream(newNewImage);
                //    var fileBytes = newMs.ToArray();
                //    encoded = Convert.ToBase64String(fileBytes);
                //}
            }
            return encoded; 
        }        
    }
}
