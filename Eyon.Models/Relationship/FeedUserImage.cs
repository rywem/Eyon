using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class FeedUserImage
    {
        [Key]
        public long FeedId { get; set; }
        [ForeignKey("FeedId")]
        public Feed Feed { get; set; }
        public long UserImageId { get; set; }
        [ForeignKey("UserImageId")]
        public UserImage UserImage { get; set; }
    }
}
