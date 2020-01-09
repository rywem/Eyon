﻿using Eyon.Models.Interfaces;
using Eyon.Models.Relationship;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eyon.Models
{
    public class UserImage : IHasOwners<ApplicationUserUserImage>, ICreated
    {
        [Key]
        public long Id { get; set; }
        [MaxLength(500)]
        [StringLength(500)]
        public string Description { get; set; }
        public string FileType { get; set; }
        public string Encoded { get; set; }
        public DateTime CreationDateTime { get; set; }
        public ICollection<ApplicationUserUserImage> ApplicationUserOwner { get; set; } 
        

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