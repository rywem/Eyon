using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Eyon.Models.Interfaces;
using Eyon.Utilities.Extensions;
using Microsoft.Extensions.Configuration;

namespace Eyon.Site.Images
{
    public class ImageHelper
    {
        private readonly IConfiguration _config;
        public ImageHelper( IConfiguration config)
        {
            this._config = config;
        }

        
        public async Task<IImageFile> PrepareForUpload(byte[] imageAsBytes, IImageFile image )
        {
            // Convert file to image
            using ( var ms = new MemoryStream(imageAsBytes) )
            {
                // Create an image file
                var thumb = ms.ToImage().Resize(128, 128);
                image.FileName = Guid.NewGuid().ToString();
                image.FileNameThumb = image.FileNameThumb + "_thumb";
                // Upload main image
                using ( Eyon.Utilities.API.AmazonWebService service = new Utilities.API.AmazonWebService(_config.GetValue<string>("AWS:AccessKey")
                                                                                                    , _config.GetValue<string>("AWS:AccessSecret")) )
                {
                    //ms.Position = 0;
                    await service.PutAsync(ms, _config.GetValue<string>("AWS:Bucket"), image.FileName);
                    using ( var newMs = new MemoryStream(ms.Capacity) )
                    {
                        newMs.ToStream(thumb);
                        await service.PutAsync(newMs, _config.GetValue<string>("AWS:Bucket"), image.FileNameThumb);
                    }
                }
                // Upload thumb

            }
        }

        public void Upload()
        {

        }


    }
}
