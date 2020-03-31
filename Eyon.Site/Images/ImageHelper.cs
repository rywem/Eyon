using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Eyon.Models.Interfaces;
using Eyon.Utilities.Extensions;
using Microsoft.Extensions.Configuration;
using Eyon.Models.Errors;

namespace Eyon.Site.Images
{
    public class ImageHelper
    {
        private readonly IConfiguration _config;
        public ImageHelper( IConfiguration config)
        {
            this._config = config;
        }        
        public async Task<IImageFile> ProcessIImageFile(byte[] imageAsBytes, IImageFile image )
        {
            List<Task> tasks = new List<Task>();
            try
            {
                // Convert bytes to image
                using ( var ms = new MemoryStream(imageAsBytes) )
                {
                    // Create an image file
                    var sysImage = ms.ToImage();
                    //create the thumbnail image
                    var sysImageThumb = sysImage.Resize(128, 128);
                    
                    image.FileName = Guid.NewGuid().ToString();
                    image.FileNameThumb = image.FileNameThumb + "_thumb";
                    // Upload main image
                    using ( Eyon.Utilities.API.AmazonWebService service = new Utilities.API.AmazonWebService(_config.GetValue<string>("AWS:AccessKey")
                                                                                                        , _config.GetValue<string>("AWS:AccessSecret")) )
                    {
                        using ( var newMs = new MemoryStream(ms.Capacity) )
                        {
                            newMs.ToStream(sysImage);  // Call ToStream to converts the file to jpeg
                            tasks.Add(service.PutAsync(ms, _config.GetValue<string>("AWS:Bucket"), $"{image.FileName}"));
                        }
                        //ms.Position = 0;
                        using ( var newMs = new MemoryStream(ms.Capacity) )
                        {
                            newMs.ToStream(sysImageThumb);
                            tasks.Add(service.PutAsync(newMs, _config.GetValue<string>("AWS:Bucket"), image.FileNameThumb));
                        }
                        image.FileType = "jpg";
                        await Task.WhenAll(tasks.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                await TryDeleteAsync(image.FileName);
                await TryDeleteAsync(image.FileNameThumb);
                throw new SafeException(Models.Enums.ErrorType.AnErrorOccurred, ex);
            }
            return image;
        }

        public async Task<bool> TryDeleteAsync(string key)
        {
            try
            {
                using ( Eyon.Utilities.API.AmazonWebService service = new Utilities.API.AmazonWebService(_config.GetValue<string>("AWS:AccessKey")
                                                                                                           , _config.GetValue<string>("AWS:AccessSecret")) )
                {                    
                    return await service.TryDeleteAsync(_config.GetValue<string>("AWS:Bucket"), key);
                }
            }
            catch
            {
                return false;
            }
        }

    }
}
