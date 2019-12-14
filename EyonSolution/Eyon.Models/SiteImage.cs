using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class SiteImage
    {
        [Key]
        public long Id { get; set; }
        public string Title { get; set; }
        public string FileType { get; set; }
        public string Encoded { get; set; }
        public string Alt { get; set; }
        
        [NotMapped]
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
            if (!string.IsNullOrEmpty(Encoded) && !string.IsNullOrEmpty(FileType))
            {
                var base64 = Encoded;
                string type = string.Empty;
                if (FileType.Contains("."))
                    type = FileType.Trim('.');

                imgSrc = string.Format("data:image/{0};base64,{1}", type, base64);
            }

            return imgSrc;
        }
    }
}
