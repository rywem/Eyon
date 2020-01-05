using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Eyon.Models.Relationship
{
    public class RecipeUserImage
    {

        public long RecipeId { get; set; }
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
        public long UserImageId { get; set; }
        [ForeignKey("UserImageId")]
        public UserImage UserImage { get; set; }
    }
}
