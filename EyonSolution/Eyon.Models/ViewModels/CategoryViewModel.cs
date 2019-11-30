using System;
using System.Collections.Generic;
using System.Text;

namespace Eyon.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public SiteImage SiteImage { get; set; }

        public string Image 
        {
            get
            {
                return GetImage();
            }            
        }

        public string GetImage()
        {
            string imgSrc = string.Empty;
            if (SiteImage != null)
            {
                var base64 = SiteImage.Encoded;

                string type = SiteImage.FileType.Trim('.');
                imgSrc = string.Format("data:image/{0};base64,{1}", type, base64);
            }

            return imgSrc;
        }
    }
}
