using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Eyon.Utilities.Extensions
{
    public static class MemoryStreamExtensions
    {
        /// <summary> 
        /// Saves an image as a jpeg image, with the given quality 
        /// </summary> 
        /// <param name="path"> Path to which the image would be saved. </param> 
        /// <param name="quality"> An integer from 0 to 100, with 100 being the highest quality. </param> 
        public static MemoryStream ToJpeg( this MemoryStream stream, Image img, int quality )
        {
            //https://stackoverflow.com/questions/18134234/how-to-convert-system-io-stream-into-an-image
            if ( quality < 0 || quality > 100 )
                throw new ArgumentOutOfRangeException("quality must be between 0 and 100.");
            
            // Encoder parameter for image quality 
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
            // JPEG image codec 
            ImageCodecInfo jpegCodec = GetEncoderInfo("image/jpeg");
            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;
            
            img.Save(stream, jpegCodec, encoderParams);
            stream.Position = 0;
            return stream;
            //return img;
        }

        public static MemoryStream ToStream(this MemoryStream stream, Image img )
        {
            img.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;
            return stream;
        }

        public static Image ToImage(this MemoryStream stream )
        {
            return System.Drawing.Image.FromStream(stream);
        }        

        /// <summary> 
        /// Returns the image codec with the given mime type 
        /// </summary> 
        private static ImageCodecInfo GetEncoderInfo( string mimeType )
        {
            // Get image codecs for all image formats 
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec 
            for ( int i = 0; i < codecs.Length; i++ )
                if ( codecs[i].MimeType == mimeType )
                    return codecs[i];

            return null;
        }
    }
}
