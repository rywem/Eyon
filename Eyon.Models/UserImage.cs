using Eyon.Models.Enums;
using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class UserImage : IHasOwners<ApplicationUserUserImage>, IFeedItem, IImageFile
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        [MaxLength(500)]
        [StringLength(500)]
        public string Description { get; set; }
        public string FileType { get; set; }        
        public DateTime CreationDateTime { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public Privacy Privacy { get; set; }
        public string FileName { get; set; }
        public string FileNameThumb { get; set; }
        public ICollection<ApplicationUserUserImage> ApplicationUserOwner { get; set; }

        public ICollection<FeedUserImage> FeedUserImage { get; set; }

        [NotMapped] 
        public string Thumb { get; set; }
        [NotMapped]
        public string Image { get; set; }

        //public string GetImage()
        //{
        //    string imgSrc = string.Empty;
        //    if (!string.IsNullOrEmpty(Encoded) && !string.IsNullOrEmpty(FileType))
        //    {
        //        var base64 = Encoded;
        //        string type = string.Empty;
        //        if (FileType.Contains("."))
        //            type = FileType.Trim('.');

        //        imgSrc = string.Format("data:image/{0};base64,{1}", type, base64);
        //    }

        //    return imgSrc;
        //}
    }
}
