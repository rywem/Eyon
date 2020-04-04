using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Eyon.Models.Errors;
using Eyon.Models.Interfaces;
using Eyon.Utilities.Extensions;
using Microsoft.Extensions.Configuration;

namespace Eyon.DataAccess.Images
{
    public class ImageHelper
    {
        private readonly IConfiguration _config;
        public ImageHelper( IConfiguration config )
        {
            this._config = config;
        }

        public string GetAWSImageUrl( string key )
        {
            using ( Eyon.Utilities.API.AmazonWebService service = new Utilities.API.AmazonWebService(_config.GetValue<string>("AWS:AccessKey")
                                                                                                        , _config.GetValue<string>("AWS:SecretKey")) )
            {
                return service.GetPreSignedUrl(_config.GetValue<string>("AWS:Bucket"), key);
            }
        }

        public async Task<IImageFile> ProcessIImageFile( byte[] imageAsBytes, IImageFile image )
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
                    var sysImageScaled = sysImage.ScaleImage(1000, 1000);
                    string guid = Guid.NewGuid().ToString();
                    image.FileType = "jpg";
                    image.FileName = $"{guid}.{image.FileType}";
                    image.FileNameThumb = $"{guid}_thumb.{image.FileType}";
                    // Upload main image
                    using ( Eyon.Utilities.API.AmazonWebService service = new Utilities.API.AmazonWebService(_config.GetValue<string>("AWS:AccessKey")
                                                                                                        , _config.GetValue<string>("AWS:SecretKey")) )
                    {
                        using ( var newMs = new MemoryStream(ms.Capacity) )
                        {
                            newMs.ToStream(sysImageScaled);  // Call ToStream to converts the file to jpeg
                            tasks.Add(service.PutAsync(newMs, _config.GetValue<string>("AWS:Bucket"), image.FileName));
                            using ( var newMsThumb = new MemoryStream(ms.Capacity) )
                            {
                                newMsThumb.ToStream(sysImageThumb);
                                tasks.Add(service.PutAsync(newMsThumb, _config.GetValue<string>("AWS:Bucket"), image.FileNameThumb));
                                await Task.WhenAll(tasks.ToArray());
                            }
                        }
                        //ms.Position = 0;
                    }
                }
            }
            catch ( Exception ex )
            {
                await TryDeleteAsync(image.FileName);
                await TryDeleteAsync(image.FileNameThumb);
                throw new SafeException(Models.Enums.ErrorType.AnErrorOccurred, ex);
            }
            return image;
        }

        public async Task<bool> TryDeleteAsync( string key )
        {
            try
            {
                using ( Eyon.Utilities.API.AmazonWebService service = new Utilities.API.AmazonWebService(_config.GetValue<string>("AWS:AccessKey")
                                                                                                           , _config.GetValue<string>("AWS:SecretKey")) )
                {
                    return await service.TryDeleteAsync(_config.GetValue<string>("AWS:Bucket"), key);
                }
            }
            catch
            {
                // TODO Log exception
                return false;
            }
        }
    }
}
