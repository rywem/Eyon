﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Eyon.Utilities.Extensions
{
    public static class ImageExtensions
    {        
        public static Image Scale(this Image image, int maxWidth, int maxHeight )
        {
            var ratioX = (double)maxWidth / image.Width;
            var ratioY = (double)maxHeight / image.Height;
            var ratio = Math.Min(ratioX, ratioY);

            var newWidth = (int)( image.Width * ratio );
            var newHeight = (int)( image.Height * ratio );

            var newImage = new Bitmap(newWidth, newHeight);

            using ( var graphics = Graphics.FromImage(newImage) )
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            return newImage;
        }

        public static Image Resize(this Image image, int width, int height )
        {
            var newImage = new Bitmap(width, height);

            using ( var graphics = Graphics.FromImage(newImage) )
                graphics.DrawImage(image, 0, 0, width, height);

            return newImage;
        }
    }
}
